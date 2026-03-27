using System;
using System.IO;

namespace ARGENT
{
    class ProgramArgent
    {
        static void Main(string[] args)
        {
            //ENTETE
            string entete = "PROJET ARGENT - TRAITEMENT BANCAIRE";
            for (int i = 0; i < entete.Length; i++) { Console.Write("-"); } Console.WriteLine();
            Console.WriteLine(entete);
            for (int i = 0; i < entete.Length; i++) { Console.Write("-"); } Console.WriteLine();

            //AFFECTATION FICHIER ENTREE ET SORTIE
            Directory.SetCurrentDirectory(@"C:\Users\Formation\Desktop\");
            string inCarte = "carte.txt";
            string inCompte = "compte.txt";
            string inTransaction = "transaction.txt";
            string outStatus = "status.txt";

            //LECTURE DU FICHIER CARTE
            using (FileStream fileCarte = new FileStream(inCarte, FileMode.Open, FileAccess.Read))
            using (StreamReader readerCarte = new StreamReader(fileCarte))
            {

            }


            //LECTURE DU FICHIER COMPTE
            using (FileStream fileCompte = new FileStream(inCompte, FileMode.Open, FileAccess.Read))
            using (StreamReader readerCompte = new StreamReader(fileCompte))
            {

            }

            //LECTURE DU FICHIER TRANSACTION
            using (FileStream fileTransaction = new FileStream(inTransaction, FileMode.Open, FileAccess.Read))
            using (StreamReader readerTransaction = new StreamReader(fileTransaction))
            {

            }

            //LECTURE DU FICHIER TRANSACTION
            using (FileStream fileStatus = new FileStream(outStatus, FileMode.Create, FileAccess.Write))
            using (StreamWriter writerStatus = new StreamWriter(fileStatus))
            {

            }




            Console.ReadKey();
        }
    }
}
