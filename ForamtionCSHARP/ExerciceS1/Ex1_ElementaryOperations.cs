using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class ElementaryOperations
    {
        public static void BasicOperation(int a, int b, char operation)
        {
            int resultat;
            if (operation == '+')
            {
                resultat = a + b;
                Console.WriteLine($"{a} {operation} {b} = {resultat}");
            }
            else if (operation == '-')
            {
                resultat = a - b;
                Console.WriteLine($"{a} {operation} {b} = {resultat}");
            }
            else
            {
                Console.WriteLine($"{a} {operation} {b} = Opération invalide");
            }
        }

        public static void IntegerDivision(int a, int b)
        {
            int q, r;
            if (b == 0)
            {
                Console.WriteLine($"{a} : {b} = Opération invalide");
            }
            else
            {
                r = a % b;
                q = a / (b - r);
                if (r == 0)
                {
                    Console.WriteLine($"{a} = {q} * {b}");
                }
                else
                {
                    Console.WriteLine($"{a} = {q} * {b} + {r}");
                }
            }

        }

        public static void Pow(int a, int b)
        {
            int resultat;
            if (b < 0)
            {
                Console.WriteLine($"{a} ^ {b} = Opération invalide");
            }
            else
            {
                resultat = a ^ b;
                Console.WriteLine($"{a} ^ {b} = {resultat}");
            }
        }
    }
}
