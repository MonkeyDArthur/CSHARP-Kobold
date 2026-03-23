using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class Pyramid
    {
        public static void PyramidConstruction(int n, bool isSmooth)
        {
            int i = 0;
            char caractere;
            char[] ligne = new char[2 * n - 1];
            char[] gauche = new char[n-1];
            char[] droite = new char[n-1];

            for (int k = 0; k < n-1; k++)
            {
                gauche[k] = ' ';
                droite[k] = ' ';
            }

            if (isSmooth == true)
            {
                while (i < n)
                {
                    if ( i%2 == 0) { caractere = '+'; } else { caractere = '-'; }
                    for (int j = 0; j < (n*2-1); j++)
                    {
                        ligne = gauche + caractere + droite;

                        if (j < n) 
                        {
                            gauche[n - 1] = caractere;
                            Console.Write($"{gauche[j]}");
                        }
                        else if (j == n) { Console.Write($"{caractere}"); }
                        else if (j > n) 
                        {
                            droite[j-n-1] = caractere;
                            Console.Write($"{droite[j-n]}");
                        }
                        Console.Write($"\n");
                    }
                    i += 1;
                }
            }

            


        

        
        }
    }
}
