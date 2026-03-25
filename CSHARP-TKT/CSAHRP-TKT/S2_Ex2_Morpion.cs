using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Q1 : char [] (1 dimension), char [,] (2 dimensions) ou une liste

namespace Serie_II
{
    public static class Morpion
    {
        public static void DisplayMorpion(char[,] grille)
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++) 
                { 
                    Console.Write(grille[i, j]);
                    if (j != 2) { Console.Write("|"); }
                    else { Console.WriteLine(); }
                }
                if (i != 2) { Console.WriteLine("-----"); }
            }
        }
        public static int CheckMorpion(char[,] grille, char type)
        {
            int avance = 0;
            for (int i = 0; i < 3; i++)
            {
                if ( grille[i,0] == type & grille[i, 1] == type & grille[i, 2] == type ) { return 1; }
            }
            for (int j = 0; j < 3; j++)
            {
                if (grille[0, j] == type & grille[1, j] == type & grille[2, j] == type) { return 1; }
            }
            if (grille[0, 0] == type & grille[1, 1] == type & grille[2, 2] == type) { return 1; }
            if (grille[0, 2] == type & grille[1, 1] == type & grille[2, 0] == type) { return 1; }
            for ( int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++) { if (grille[i, j] != ' ') { avance += 1; } }
            }
            if (avance == 9) { return 0; } else { return -1; }
        }                
    }
}
