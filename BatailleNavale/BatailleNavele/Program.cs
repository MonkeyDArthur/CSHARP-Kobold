using System;


//Q2a:  Quand toutes les positions du bateaux sont touché
//Q7a:  Quand toutes les positions des differents bateaux sont en statut coulé

namespace Bataille_Navale
{
    class ProgramNavale
    {
        static void Main(string[] args)
        {
            string entete = "                    BATAILLE NAVALE                    ";
            for (int i = 0; i < entete.Length; i++) { Console.Write("="); }
            Console.WriteLine();
            Console.WriteLine(entete);
            for (int i = 0; i < entete.Length; i++) { Console.Write("="); }
            Console.WriteLine();
            Console.WriteLine();


            Plateau grille = new Plateau(10);
            grille.CreationPlateau();
            grille.LancementPartie();






            Console.ReadKey();
        }
    }
}