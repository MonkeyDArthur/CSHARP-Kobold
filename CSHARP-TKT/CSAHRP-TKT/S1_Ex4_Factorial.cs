using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Q2 b : Le version itérative est plus efficase car plus simple et plus leger

namespace Serie_I
{
    public static class Factorial
    {
        public static int Factorial_(int n)
        {
            int resultat = 1;
            if (n == 0) { resultat = 1; } else { for (int i = 1; i <= n; i++) { resultat *= i; } }
            return resultat;
        }

        public static int FactorialRecursive(int n)
        {
            int resultat = 1;
            if (n == 0) { return resultat; } else { return n * FactorialRecursive(n - 1); }
        }
    }
}
