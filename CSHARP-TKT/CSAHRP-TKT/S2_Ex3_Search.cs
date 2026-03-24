using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Q2 : Dans le pire des cas, on doit lire N éléments donc 10 pour un tab de 10
//Q4 : Dans le pire des cas, il faut lire (N-1)/4 étapes

namespace Serie_II
{
    public static class Search
    {
        public static int LinearSearch(int[] tableau, int valeur)
        {
            for (int i = 0; i < tableau.Length; i++)
            {
                if (tableau[i] == valeur) { return i; }
            }
            return -1;
        }

        public static int BinarySearch(int[] tableau, int valeur)
        {
            int min = 0; int max = tableau.Length - 1; int nbBoucle = 0;
            while (min <= max)
            {
                int milieu = (min + max) / 2;
                nbBoucle += 1;
                if (tableau[milieu] == valeur) { Console.WriteLine($"Nombre de boucle : {nbBoucle}"); return milieu; }
                else if (tableau[milieu] < valeur) { min = milieu + 1; }
                else { max = milieu - 1; }
            }
            Console.WriteLine($"Nombre de boucle : {nbBoucle}");
            return -1;
        }
    }
}
