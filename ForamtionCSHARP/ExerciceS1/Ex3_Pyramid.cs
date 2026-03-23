using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Q1 a : nb bloc par niveau j = 2j-1
//Q1 b : nb bloc total = j^2
//Q2 a : position du sommet = (n-1)/2
//Q2 b : formule gauche = (n-1)/2 - i et formule droite = (n-1)/2 + i

namespace Serie_I
{
    public static class Pyramid
    {
        public static void PyramidConstruction(int n, bool isSmooth)
        {
            for (int j = 1; j <= n; j++)
            {
                int espace = n - j;
                int bloc = 2 * j - 1;
                char c;
                if (isSmooth == true) { c = '+'; }
                else 
                {
                    if (j % 2 == 0) { c = '-'; }
                    else { c = '+'; }
                }

                for (int i = 0; i < espace; i++)
                {
                    Console.Write(" ");
                }

                for (int i = 0; i < bloc; i++)
                {
                    Console.Write(c);
                }

                Console.WriteLine();
            }
        }
    }
}
