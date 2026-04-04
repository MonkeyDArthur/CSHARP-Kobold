using Microsoft.Data.Sqlite;
using Or.Models;
using System;
using System.Collections.Generic;
using System.IO;

namespace Or.Business
{
    static public class SqlRequests
    {
        static readonly string fileDb = "BaseAppBancaire.db";

        static readonly string queryComptesDispo = "SELECT IdtCpt, NumCarte, Solde, TypeCompte FROM COMPTE WHERE NOT IdtCpt=@IdtCpt";

        static readonly string queryComptesCarte = "SELECT IdtCpt, NumCarte, Solde, TypeCompte FROM COMPTE WHERE NumCarte=@Carte";
        static readonly string queryTransacCompte = "SELECT IdtTransaction, Horodatage, Montant, CptExpediteur, CptDestinataire, Statut FROM \"TRANSACTION\" WHERE Statut = 'O' AND (CptExpediteur=@IdtCptEx OR CptDestinataire=@IdtCptDest)";
        static readonly string queryCarte = "SELECT NumCarte, PrenomClient, NomClient, PlafondRetrait from CARTE WHERE NumCarte=@Carte";
        static readonly string queryTransacCarte = "SELECT tr.IdtTransaction, tr.Horodatage, tr.Montant, tr.CptExpediteur, tr.CptDestinataire, tr.Statut FROM \"TRANSACTION\" tr INNER JOIN HISTTRANSACTION t ON t.IdtTransaction = tr.IdtTransaction WHERE tr.Statut = 'O' AND t.NumCarte=@Carte;";

        static readonly string queryInsertTransac = "INSERT INTO \"TRANSACTION\" (Horodatage, Montant, CptExpediteur, CptDestinataire, Statut) VALUES (@Horodatage,@Montant,@CptExp,@CptDest,\"O\")";
        static readonly string queryIdtTransac = "select seq from sqlite_sequence where name=\"TRANSACTION\"";
        static readonly string queryInsertHistTransac = "INSERT INTO HISTTRANSACTION (IdtTransaction,NumCarte) VALUES (@IdtTrans,@Carte)";

        static readonly string queryUpdateCompte = "UPDATE COMPTE SET Solde=Solde-@Montant WHERE IdtCpt=@IdtCompte";




        public static List<BeneficiaireVue> ListeBeneficiairesAssocieClient(long numCarte)
        {
            List<BeneficiaireVue> liste = new List<BeneficiaireVue>();
            string connectionString = ConstructionConnexionString(fileDb);
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query =
                    @"SELECT co.IdtCpt, ca.NomClient, ca.PrenomClient
                      FROM   BENEFICIAIRE b
                      JOIN   COMPTE co ON co.IdtCpt = b.IdBeneficiaire
                      JOIN   CARTE  ca ON ca.NumCarte = co.NumCarte
                      WHERE  b.numCarte = @numCarte";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@numCarte", numCarte);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) { liste.Add(new BeneficiaireVue(reader.GetInt32(0), reader.GetString(1), reader.GetString(2))); }
                    }
                }
            }
            return liste;
        }
        public static List<Compte> ListeComptesDispoAvecBeneficiaires(int idCompteExpediteur, long numCarte)
        {
            List<Compte> liste = new List<Compte>();
            string connectionString = ConstructionConnexionString(fileDb);
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query =
                    @"  SELECT co.IdtCpt, ca.NomClient, ca.PrenomClient
                        FROM   BENEFICIAIRE b
                        JOIN   COMPTE co ON co.IdtCpt = b.IdBeneficiaire
                        JOIN   CARTE  ca ON ca.NumCarte = co.NumCarte
                        WHERE  b.numCarte = @numCarte";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@numCarte", numCarte);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) { liste.Add(new Compte(reader.GetInt32(0), reader.GetInt64(1), (TypeCompte)reader.GetInt32(2), reader.GetDecimal(3))); }
                    }
                }
            }
            return liste;
        }
        public static void AjoutBeneficiaire(long numCarte, int idtCpt)
        {
            string connectionString = ConstructionConnexionString(fileDb);
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query =
                    @"  SELECT IdtCpt, NumCarte, TypeCompte, Solde
                        FROM   COMPTE
                        WHERE  NumCarte = @numCarte AND IdtCpt <> @idExp
                        UNION
                        SELECT co.IdtCpt, co.NumCarte, co.TypeCompte, co.Solde
                        FROM   BENEFICIAIRE b
                        JOIN   COMPTE co ON co.IdtCpt = b.IdBeneficiaire
                        WHERE  b.numCarte = @numCarte AND co.IdtCpt <> @idExp";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@numCarte", numCarte);
                    command.Parameters.AddWithValue("@idtCpt", idtCpt);
                }
            }
        }
        public static void SuppressionBeneficiaire(long numCarte, int idtCpt)
        {
            string connectionString = ConstructionConnexionString(fileDb);
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM BENEFICIAIRE WHERE numCarte = @numCarte AND IdBeneficiaire = @idtCpt";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@numCarte", numCarte);
                    command.Parameters.AddWithValue("@idtCpt", idtCpt);
                }
            }
        }
        public static bool EstBeneficiairePotentiel(int idtCpt)
        {
            bool isBeneficiaire = false;
            string connectionString = ConstructionConnexionString(fileDb);
            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM COMPTE WHERE IdtCpt = @idtCpt AND TypeCompte = 'Courant'";
                using (var command = new SqliteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@idtCpt", idtCpt);
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read()) { isBeneficiaire = reader.GetInt32(0) > 0; }
                    }
                }
            }
            return isBeneficiaire;
        }

        /// <summary>
        /// Obtention des infos d'une carte
        /// </summary>
        /// <param name="numCarte"></param>
        /// <returns></returns>
        public static Carte InfosCarte(long numCarte)
        {
            Carte carte = null;

            string connectionString = ConstructionConnexionString(fileDb);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(queryCarte, connection))
                {
                    command.Parameters.AddWithValue("@Carte", numCarte);

                    using (var reader = command.ExecuteReader())
                    {
                        long idtCarte;
                        string prenom;
                        string nom;
                        int plafondRetrait;

                        if (reader.Read())
                        {
                            idtCarte = reader.GetInt64(0);
                            prenom = reader.GetString(1);
                            nom = reader.GetString(2);
                            plafondRetrait = reader.GetInt32(3);

                            carte = new Carte(idtCarte, prenom, nom, plafondRetrait);
                        }
                    }
                }
            }

            return carte;
        }

        /// <summary>
        /// Obtention du dernier identifiant de transaction
        /// </summary>
        /// <returns></returns>
        public static int InfosIdtTrans()
        {
            int idtTransac = 0;

            string connectionString = ConstructionConnexionString(fileDb);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(queryIdtTransac, connection))
                {

                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            idtTransac = reader.GetInt32(0);
                        }
                    }
                }
            }

            return idtTransac;
        }


        /// <summary>
        /// Liste des comptes associés à une carte donnée
        /// </summary>
        /// <param name="numCarte"></param>
        /// <returns></returns>
        public static List<Compte> ListeComptesAssociesCarte(long numCarte)
        {
            List<Compte> comptes = new List<Compte>();

            string connectionString = ConstructionConnexionString(fileDb);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(queryComptesCarte, connection))
                {
                    command.Parameters.AddWithValue("@Carte", numCarte);

                    using (var reader = command.ExecuteReader())
                    {
                        int idtCpt;
                        long carte;
                        decimal solde;
                        string typeCompte;

                        while (reader.Read())
                        {
                            idtCpt = reader.GetInt32(0);
                            carte = reader.GetInt64(1);
                            solde = reader.GetDecimal(2);
                            typeCompte = reader.GetString(3);

                            Compte compte = new Compte(idtCpt, carte, typeCompte == "Courant" ? TypeCompte.Courant : TypeCompte.Livret, solde);
                            comptes.Add(compte);
                        }
                    }
                }
            }

            return comptes;
        }


        /// <summary>
        /// Liste des comptes associés dispos
        /// </summary>
        /// <param name="idtCpt"></param>
        /// <returns></returns>
        public static List<Compte> ListeComptesDispo(int idtCpt)
        {
            List<Compte> comptes = new List<Compte>();

            string connectionString = ConstructionConnexionString(fileDb);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(queryComptesDispo, connection))
                {
                    command.Parameters.AddWithValue("@IdtCpt", idtCpt);

                    using (var reader = command.ExecuteReader())
                    {
                        int idt;
                        long carte;
                        decimal solde;
                        string typeCompte;

                        while (reader.Read())
                        {
                            idt = reader.GetInt32(0);
                            carte = reader.GetInt64(1);
                            solde = reader.GetDecimal(2);
                            typeCompte = reader.GetString(3);

                            Compte compte = new Compte(idt, carte, typeCompte == "Courant" ? TypeCompte.Courant : TypeCompte.Livret, solde);
                            comptes.Add(compte);
                        }
                    }
                }
            }

            return comptes;
        }

        /// <summary>
        /// Liste des transactions associées à une carte donnée
        /// </summary>
        /// <param name="numCarte"></param>
        /// <returns></returns>
        public static List<Transaction> ListeTransactionsAssociesCarte(long numCarte)
        {
            List<Transaction> transactions = new List<Transaction>();

            string connectionString = ConstructionConnexionString(fileDb);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(queryTransacCarte, connection))
                {
                    command.Parameters.AddWithValue("@Carte", numCarte);

                    using (var reader = command.ExecuteReader())
                    {
                        int idtTransaction;
                        string horodatage;
                        decimal montant;
                        int cptDest;
                        int cptExt;

                        while (reader.Read())
                        {
                            idtTransaction = reader.GetInt32(0);
                            horodatage = reader.GetString(1);
                            montant = reader.GetDecimal(2);
                            cptDest = reader.GetInt32(3);
                            cptExt = reader.GetInt32(4);

                            Transaction trans = new Transaction(idtTransaction, Tools.ConversionDate(horodatage), montant, cptDest, cptExt);
                            transactions.Add(trans);
                        }
                    }
                }
            }

            return transactions;
        }

        /// <summary>
        /// Liste des transactions associées à un compte donné
        /// </summary>
        /// <param name="numCarte"></param>
        /// <returns></returns>
        public static List<Transaction> ListeTransactionsAssociesCompte(int idtCpt)
        {
            List<Transaction> transactions = new List<Transaction>();

            string connectionString = ConstructionConnexionString(fileDb);

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                using (var command = new SqliteCommand(queryTransacCompte, connection))
                {
                    command.Parameters.AddWithValue("@IdtCptEx", idtCpt);
                    command.Parameters.AddWithValue("@IdtCptDest", idtCpt);

                    using (var reader = command.ExecuteReader())
                    {
                        int idtTransaction;
                        string horodatage;
                        decimal montant;
                        int cptDest;
                        int cptExt;

                        while (reader.Read())
                        {
                            idtTransaction = reader.GetInt32(0);
                            horodatage = reader.GetString(1);
                            montant = reader.GetDecimal(2);
                            cptDest = reader.GetInt32(3);
                            cptExt = reader.GetInt32(4);

                            Transaction trans = new Transaction(idtTransaction, Tools.ConversionDate(horodatage), montant, cptDest, cptExt);
                            transactions.Add(trans);
                        }
                    }
                }
            }

            return transactions;
        }


        /// <summary>
        /// Procédure pour mettre à jour les données pour un retrait
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static bool EffectuerModificationOperationSimple(Transaction trans, long numCarte)
        {
            string connectionString = ConstructionConnexionString(fileDb);

            Operation typeOpe = Tools.TypeTransaction(trans.Expediteur, trans.Destinataire);

            if (typeOpe != Operation.DepotSimple && typeOpe != Operation.RetraitSimple)
            {
                return false;
            }

            int idtTrans = InfosIdtTrans() + 1;

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // Démarrer une transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insertion de la transaction
                        var insertTransac = ConstructionInsertionTransaction(connection, trans);
                        insertTransac.Transaction = transaction;
                        insertTransac.ExecuteNonQuery();

                        // Insertion de l'historique de transaction
                        var insertHistTransac = ConstructionInsertionHistTransaction(connection, idtTrans, numCarte);
                        insertHistTransac.Transaction = transaction;
                        insertHistTransac.ExecuteNonQuery();

                        // Mise à jour du solde du compte de l'opération simple
                        decimal montant = typeOpe == Operation.RetraitSimple ? trans.Montant : -trans.Montant;
                        int idtCpt = typeOpe == Operation.DepotSimple ? trans.Destinataire : trans.Expediteur;

                        var updateCompte = ConstructionUpdateSolde(connection, idtCpt, montant);
                        updateCompte.Transaction = transaction;
                        updateCompte.ExecuteNonQuery();

                        // Valider la transaction
                        transaction.Commit();
                        Console.WriteLine("Transaction validée.");
                    }
                    catch (Exception ex)
                    {
                        // En cas d’erreur, annuler la transaction
                        Console.WriteLine("Erreur : " + ex.Message);
                        transaction.Rollback();
                        Console.WriteLine("Transaction annulée.");
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Procédure pour mettre à jour les données pour un retrait
        /// </summary>
        /// <param name="trans"></param>
        /// <returns></returns>
        public static bool EffectuerModificationOperationInterCompte(Transaction trans, long numCarteExp, long numCarteDest)
        {
            string connectionString = ConstructionConnexionString(fileDb);

            Operation typeOpe = Tools.TypeTransaction(trans.Expediteur, trans.Destinataire);

            if (typeOpe != Operation.InterCompte)
            {
                return false;
            }

            int idtTrans = InfosIdtTrans() + 1;

            using (var connection = new SqliteConnection(connectionString))
            {
                connection.Open();

                // Démarrer une transaction
                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        // Insertion de la transaction
                        var insertTransac = ConstructionInsertionTransaction(connection, trans);
                        insertTransac.Transaction = transaction;
                        insertTransac.ExecuteNonQuery();

                        // Insertion de l'historique de transaction
                        var insertHistTransac = ConstructionInsertionHistTransaction(connection, idtTrans, numCarteExp);
                        insertHistTransac.Transaction = transaction;
                        insertHistTransac.ExecuteNonQuery();

                        if (numCarteDest != numCarteExp)
                        {
                            // Insertion de l'historique de transaction - côté destinataire
                            var insertHistTransacDest = ConstructionInsertionHistTransaction(connection, idtTrans, numCarteDest);
                            insertHistTransacDest.Transaction = transaction;
                            insertHistTransacDest.ExecuteNonQuery();
                        }

                        // Mise à jour du solde du compte de l'opération inter-compte 
                        // côté expéditeur
                        var updateCompteExp = ConstructionUpdateSolde(connection, trans.Expediteur, trans.Montant);
                        updateCompteExp.Transaction = transaction;
                        updateCompteExp.ExecuteNonQuery();

                        // côté destinataire
                        var updateCompteDest = ConstructionUpdateSolde(connection, trans.Destinataire, -trans.Montant);
                        updateCompteDest.Transaction = transaction;
                        updateCompteDest.ExecuteNonQuery();

                        // Valider la transaction
                        transaction.Commit();
                        Console.WriteLine("Transaction validée.");
                    }
                    catch (Exception ex)
                    {
                        // En cas d’erreur, annuler la transaction
                        Console.WriteLine("Erreur : " + ex.Message);
                        transaction.Rollback();
                        Console.WriteLine("Transaction annulée.");
                    }
                }
            }

            return true;
        }

        private static string ConstructionConnexionString(string fileDb)
        {
            string dossierRef = Directory.GetCurrentDirectory();
            string dossierProjet = Path.GetFullPath(Path.Combine(dossierRef, @"..\..\.."));

            string chemin = Path.Combine(dossierProjet, fileDb);
            return "Data Source=" + chemin;
        }

        private static SqliteCommand ConstructionInsertionTransaction(SqliteConnection connection, Transaction trans)
        {
            // Insertion de la transaction
            var insertTransac = connection.CreateCommand();
            insertTransac.CommandText = queryInsertTransac;

            insertTransac.Parameters.AddWithValue("@Horodatage", trans.Horodatage.ToString("dd/MM/yyyy hh:mm:ss"));
            insertTransac.Parameters.AddWithValue("@Montant", trans.Montant);
            insertTransac.Parameters.AddWithValue("@CptExp", trans.Expediteur);
            insertTransac.Parameters.AddWithValue("@CptDest", trans.Destinataire);
            
            return insertTransac;
        }

        private static SqliteCommand ConstructionInsertionHistTransaction(SqliteConnection connection, int idtTrans, long numCarte)
        {
            // Insertion de la transaction
            var insertHistTransac = connection.CreateCommand();
            insertHistTransac.CommandText = queryInsertHistTransac;

            insertHistTransac.Parameters.AddWithValue("@IdtTrans", idtTrans);
            insertHistTransac.Parameters.AddWithValue("@Carte", numCarte);

            return insertHistTransac;
        }

        /// <summary>
        /// COnstruction de la commande de mise à jour du solde du compte
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="idtCpt"></param>
        /// <param name="montant">Montant à soustraire au solde</param>
        /// <returns></returns>
        private static SqliteCommand ConstructionUpdateSolde(SqliteConnection connection, int idtCpt, decimal montant)
        {
            // Mise à jour du solde du compte
            var updateCompte = connection.CreateCommand();
            updateCompte.CommandText = queryUpdateCompte;
            updateCompte.Parameters.AddWithValue("@Montant", montant);
            updateCompte.Parameters.AddWithValue("@IdtCompte", idtCpt);

            return updateCompte;
        }

    }
}
