using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;

using CARTE;
using COMPTE;
using TRANSACTION;

namespace ARGENT
{
    class ProgramArgent
    {
        static void Main(string[] args)
        {
            //CULTURE INFO
            CultureInfo cultureInfo = new CultureInfo("en-US", true);

            //ENTETE
            string entete = "                    PROJET ARGENT - TRAITEMENT BANCAIRE                    ";
            for (int i = 0; i < entete.Length; i++) { Console.Write("="); } Console.WriteLine();
            Console.WriteLine(entete);
            for (int i = 0; i < entete.Length; i++) { Console.Write("="); } Console.WriteLine();
            Console.WriteLine();

            //AFFECTATION FICHIER ENTREE ET SORTIE
            //Directory.SetCurrentDirectory(@"C:\Users\artib\Downloads");   //MAISON
            Directory.SetCurrentDirectory(@"C:\Users\Formation\Desktop");   //INTM
            string inCarte = "carteTest.txt";
            string inCompte = "compteTest.txt";
            string inTransaction = "transactionTest.txt";
            string outStatus = "statusTest.txt";

            //STOCKAGE DES DONNEES D'ENTREE
            List<Carte> listeCarte = new List<Carte>();
            List<Compte> listeCompte = new List<Compte>();
            List<Transaction> listeTransaction = new List<Transaction>();

            //LECTURE DU FICHIER CARTE
            Console.WriteLine("============== CARTE ======================================================");
            using (FileStream fileCarte = new FileStream(inCarte, FileMode.Open, FileAccess.Read))
            using (StreamReader readerCarte = new StreamReader(fileCarte))
            {
                foreach (var ligne in File.ReadLines(inCarte))
                {
                    var contenu = ligne.Split(';');
                    string numeroCarte = contenu[0];
                    uint plafond;

                    //VERIF DES FORMATS : NUMERO DE CARTE, PLAFOND
                    if (numeroCarte.Length != 16
                        || numeroCarte.All(c => c == '0')
                        || numeroCarte.Any(char.IsLetter))
                        continue;
                    else if (contenu[1] == "") plafond = 500;
                    else if (uint.TryParse(contenu[1], NumberStyles.Any, cultureInfo, out plafond))
                    {
                        if (plafond < 500 || plafond > 3000) continue;
                        plafond = plafond / 100 * 100;
                    }
                    else continue;

                    //VERIF DOUBLON DE NUMERO DE CARTE
                    if (!listeCarte.Exists(x => x.Numero == numeroCarte)) listeCarte.Add(new Carte(numeroCarte, plafond));
                }
                Carte.AfficherListeCarte(listeCarte);
            }
            Console.WriteLine();


            //LECTURE DU FICHIER COMPTE
            Console.WriteLine("============== COMPTE =====================================================");
            using (FileStream fileCompte = new FileStream(inCompte, FileMode.Open, FileAccess.Read))
            using (StreamReader readerCompte = new StreamReader(fileCompte))
            {
                foreach (var ligne in File.ReadLines(inCompte))
                {
                    var contenu = ligne.Split(';');
                    uint ID;
                    string numeroCarte = contenu[1];
                    string type = contenu[2];
                    decimal solde = 0;

                    //VERIF DES FORMATS : ID, NUMERO DE CARTE, TYPE ET SOLDE
                    if (!uint.TryParse(contenu[0], out ID))
                        continue;
                    else if (numeroCarte.Length != 16
                        || numeroCarte.All(c => c == '0')
                        || numeroCarte.Any(char.IsLetter))
                        continue;
                    else if (type != "Courant" && type != "Livret") continue;
                    else if (contenu[3] != "")
                    {
                        if (!decimal.TryParse(contenu[3], NumberStyles.Any, cultureInfo, out solde)
                            || solde < 0)
                            continue;
                    }

                    //VERIF SI NUMERO DE CARTE EXISTE ET DOUBLON DE ID
                    Carte carteAssociee = listeCarte.Find(x => x.Numero == numeroCarte);
                    if (carteAssociee == null) continue;

                    if (!listeCompte.Exists(x => x.ID == ID))
                    {
                        listeCompte.Add(new Compte(ID, numeroCarte, type, solde));
                        carteAssociee.AjouterCompte(ID);
                    }
                }
                Compte.AfficherListeCompte(listeCompte);
            }
            Console.WriteLine();

            //LECTURE DU FICHIER TRANSACTION
            Console.WriteLine("============== TRANSACTIONS ===============================================");
            using (FileStream fileTransaction = new FileStream(inTransaction, FileMode.Open, FileAccess.Read))
            using (StreamReader readerTransaction = new StreamReader(fileTransaction))
            {
                foreach (var ligne in File.ReadLines(inTransaction))
                {
                    var contenu = ligne.Split(';');
                    uint ID;
                    DateTime horodatage;
                    decimal montant;
                    uint expediteur;
                    uint destinataire;

                    //VERIF DES FORMATS : ID, HORODATAGE, MONTANT, EXPEDITEUR ET DESTINATAIRE
                    if (uint.TryParse(contenu[0], out ID) == false)
                        continue;
                    else if (!DateTime.TryParseExact(contenu[1], "dd/MM/yyyy HH:mm:ss", cultureInfo, DateTimeStyles.None, out horodatage))
                        continue;
                    else if (!decimal.TryParse(contenu[2].Trim(), NumberStyles.Any, cultureInfo, out montant) || montant < 0)
                        continue;
                    else if (!uint.TryParse(contenu[3].Trim(), out expediteur))
                        continue;
                    else if (!uint.TryParse(contenu[4].Trim(), out destinataire))
                        continue;

                    //VERIF SI EXPEDITEUR DESTINATAIRE ET ID TRANSACTION EXISTE
                    if (expediteur != 0 && !listeCompte.Exists(x => x.ID == expediteur)) continue;
                    if (destinataire != 0 && !listeCompte.Exists(x => x.ID == destinataire)) continue;

                    if (!listeTransaction.Exists(x => x.ID == ID))
                        listeTransaction.Add(new Transaction(ID, horodatage, montant, expediteur, destinataire));
                }
                Transaction.AfficherListeTransaction(listeTransaction);
            }
            Console.WriteLine();


            //TRAITEMENT DES TRANSACTIONS ET ECRITURE DU FICHIER STATUS
            Console.WriteLine("============== STATUS =====================================================");
            List<(uint id, string statut)> listeStatuts = new List<(uint, string)>();
            int retourLigne = 0;

            foreach (var transaction in listeTransaction)
            {
                string statut = "KO";
                uint expId = transaction.Expediteur;
                uint destId = transaction.Destinataire;
                decimal montant = transaction.Montant;

                Compte compteExp = expId != 0 ? listeCompte.Find(c => c.ID == expId) : null;
                Compte compteDest = destId != 0 ? listeCompte.Find(c => c.ID == destId) : null;

                // DEPOT
                if (expId == 0 && compteDest != null) { if (compteDest.Deposer(montant)) statut = "OK"; }

                //RETRAIT
                else if (expId != 0 && destId == 0 && compteExp != null)
                {
                    Carte carteExp = listeCarte.Find(c => c.Numero == compteExp.Carte);
                    if (carteExp != null
                        && carteExp.VerifierPlafond(transaction.Horodatage, montant)
                        && compteExp.Retirer(montant))
                    {
                        carteExp.EnregistrerDebit(transaction.Horodatage, montant);
                        statut = "OK";
                    }
                }

                //VIREMENT
                else if (expId != 0 && destId != 0 && compteExp != null && compteDest != null)
                {
                    Carte carteExp = listeCarte.Find(c => c.Numero == compteExp.Carte);
                    Carte carteDest = listeCarte.Find(c => c.Numero == compteDest.Carte);
                    if (carteExp != null && carteDest != null
                        && VerifierContraintesCarte(compteExp, compteDest, carteExp, carteDest)
                        && carteExp.VerifierPlafond(transaction.Horodatage, montant)
                        && compteExp.Retirer(montant))
                    {
                        compteDest.Deposer(montant);
                        carteExp.EnregistrerDebit(transaction.Horodatage, montant);
                        statut = "OK";
                    }
                }
                listeStatuts.Add((transaction.ID, statut));
                Console.Write($"TRANSACTION {transaction.ID} : {statut}\t");
                retourLigne++;
                if (retourLigne == 3) { Console.WriteLine(); retourLigne = 0; }
            }
            Console.WriteLine();

            using (FileStream fileStatus = new FileStream(outStatus, FileMode.Create, FileAccess.Write))
            using (StreamWriter writerStatus = new StreamWriter(fileStatus))
            {
                foreach (var (id, statuts) in listeStatuts) writerStatus.WriteLine($"{id};{statuts}");
            } 
            Console.WriteLine($"ECRITURE DANS : {outStatus}");
            Console.WriteLine();

            Console.WriteLine("============== SOLDES FINAUX ==============================================");
            Compte.AfficherListeCompte(listeCompte);
            Console.WriteLine();

            Console.WriteLine("============== FIN TRAITEMENT =============================================");
            Console.ReadKey();
        }
        static bool VerifierContraintesCarte( Compte compteExp, Compte compteDest, Carte carteExp, Carte carteDest)
        {
            // MEME CARTE : TOUT
            if (carteExp.Numero == carteDest.Numero) return true;

            // CARTE DIFFERENTE : VIREMENT ENTRE COMPTES COURANTS UNIQUEMENT
            return compteExp.Type == "Courant" && compteDest.Type == "Courant";
        }
    }
}
