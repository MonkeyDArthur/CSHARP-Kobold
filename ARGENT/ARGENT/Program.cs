using System;
using System.IO;
using System.Linq;
using System.Globalization;
using System.Collections.Generic;

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
            for (int i = 0; i < entete.Length; i++) { Console.Write("-"); } Console.WriteLine();
            Console.WriteLine(entete);
            for (int i = 0; i < entete.Length; i++) { Console.Write("-"); } Console.WriteLine();
            Console.WriteLine();

            //AFFECTATION FICHIER ENTREE ET SORTIE
            Directory.SetCurrentDirectory(@"C:\Users\Formation\Desktop\");
            string inCarte = "carteTest.txt";
            string inCompte = "compteTest.txt";
            string inTransaction = "transactionTest.txt";
            string outStatus = "statusTest.txt";

            //STOCKAGE DES DONNEES D'ENTREE
            List<Carte> listeCarte = new List<Carte>();
            List<Compte> listeCompte = new List<Compte>();
            List<Transaction> listeTransaction = new List<Transaction>();

            //LECTURE DU FICHIER CARTE
            using (FileStream fileCarte = new FileStream(inCarte, FileMode.Open, FileAccess.Read))
            using (StreamReader readerCarte = new StreamReader(fileCarte))
            {
                foreach (var ligne in File.ReadLines(inCarte))
                {
                    var contenu = ligne.Split(';');
                    string numeroCarte = contenu[0]; uint plafond;
                    //VERIF DES FORMATS : NUMERO DE CARTE, PLAFOND
                    if (numeroCarte.Length != 16 || numeroCarte.All(c => c == '0') || numeroCarte.Any(char.IsLetter)) continue;
                    else if (uint.TryParse(contenu[1], NumberStyles.Any, cultureInfo, out plafond))
                    {
                        if (contenu[1] == "") { plafond = 500; }
                        else if (plafond < 500 || plafond > 3000) continue;
                    }
                    //INIT DU PLAFOND AU CENTAINE UNIQUEMENT
                    plafond = plafond / 100 * 100;
                    //VERIF SI NUMERO DE CARTE EXISTE DEJA
                    if (!listeCarte.Exists(x => x.Numero == numeroCarte))
                    {
                        Carte carte = new Carte(numeroCarte, plafond);
                        listeCarte.Add(carte);
                    }
                }
                foreach (var elem in listeCarte) { elem.AfficherCarte(); }
            }
            Console.WriteLine();


            //LECTURE DU FICHIER COMPTE
            using (FileStream fileCompte = new FileStream(inCompte, FileMode.Open, FileAccess.Read))
            using (StreamReader readerCompte = new StreamReader(fileCompte))
            {
                foreach (var ligne in File.ReadLines(inCompte))
                {
                    var contenu = ligne.Split(';');
                    uint ID; string numeroCarte = contenu[1]; string type = contenu[2]; decimal solde = 0;
                    //VERIF DES FORMATS : ID, NUMERO DE CARTE, TYPE ET SOLDE
                    if (uint.TryParse(contenu[0], out ID) == false) continue;
                    else if (numeroCarte.Length != 16 || numeroCarte.All(c => c == '0') || numeroCarte.Any(char.IsLetter)) continue;
                    else if (type != "Courant" && type != "Livret") continue;
                    else if (decimal.TryParse(contenu[3], NumberStyles.Any, cultureInfo, out solde))
                    {
                        if (solde < 0) continue;
                    }
                    //VERIF SI NUMERO DE CARTE ET ID COMPTE EXISTE
                    if (!listeCarte.Exists(x => x.Numero == numeroCarte)) continue;
                    if (!listeCompte.Exists(x => x.ID == ID))
                    {
                        Compte compte = new Compte(ID, numeroCarte, type, solde);
                        listeCompte.Add(compte);
                    }
                }
                foreach(var elem in listeCompte) { elem.AfficherCompte(); }
            }
            Console.WriteLine();

            //LECTURE DU FICHIER TRANSACTION
            using (FileStream fileTransaction = new FileStream(inTransaction, FileMode.Open, FileAccess.Read))
            using (StreamReader readerTransaction = new StreamReader(fileTransaction))
            {
                foreach (var ligne in File.ReadLines(inTransaction))
                {
                    var contenu = ligne.Split(';');
                    Console.WriteLine(ligne);
                    uint ID; DateTime horodatage; decimal montant; uint expediteur = 0; uint destinataire = 0;
                    //VERIF DES FORMATS : ID, HORODATAGE, MONTANT, EXPEDITEUR ET DESTINATAIRE
                    if (uint.TryParse(contenu[0], out ID) == false) continue;
                    else if (DateTime.TryParse(contenu[1], out horodatage) == false) continue;
                    else if (decimal.TryParse(contenu[2], NumberStyles.Any, cultureInfo, out montant)) { if (montant < 0) continue; }
                    else if (uint.TryParse(contenu[3], out expediteur)) continue;
                    else if (uint.TryParse(contenu[4], out destinataire)) continue;
                    //VERIF SI EXPEDITEUR DESTINATAIRE ET ID TRANSACTION EXISTE 
                    if (!listeCompte.Exists(x => x.ID == expediteur)) continue;
                    if (!listeCompte.Exists(x => x.ID == destinataire)) continue;
                    if (!listeTransaction.Exists(x => x.ID == ID))
                    {
                        Transaction transaction = new Transaction(ID, horodatage, montant, expediteur, destinataire);
                        listeTransaction.Add(transaction);
                    }
                }
                foreach (var elem in listeTransaction) { elem.AfficherTransaction(); }
            }
            Console.WriteLine();


            //LECTURE DU FICHIER TRANSACTION
            using (FileStream fileStatus = new FileStream(outStatus, FileMode.Create, FileAccess.Write))
            using (StreamWriter writerStatus = new StreamWriter(fileStatus))
            {

            }




            Console.ReadKey();
        }
    }
}
