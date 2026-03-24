using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_I
{
    public static class ElementaryOperations
    {
        public static void BasicOperation(int a, int b, char ope)
        {
            switch (ope)
            {
                case '+': Console.WriteLine($"{a} {ope} {b} = {a + b}"); break;
                case '-': Console.WriteLine($"{a} {ope} {b} = {a - b}"); break;
                case '*': Console.WriteLine($"{a} {ope} {b}  = {a * b}"); break;
                case '/':
                    if (b == 0) { Console.WriteLine($"{a} {ope} {b} = Opération invalide"); }
                    else { Console.WriteLine($"{a} / {b} = {(double)a / b}"); }
                    break;
                default: Console.WriteLine($"{a} {ope} {b} = Opération invalide"); break;
            }
        }

        public static void IntegerDivision(int a, int b)
        {
            if (b == 0) { Console.WriteLine($"{a} : {b} = Opération invalide"); }
            else
            {
                int r = a % b;
                int q = a / b;
                if (r == 0) { Console.WriteLine($"{a} = {q} * {b}"); }
                else { Console.WriteLine($"{a} = {q} * {b} + {r}"); }
            }

        }

        public static void Pow(int a, int b)
        { 
            if (b < 0) { Console.WriteLine($"{a} ^ {b} = Opération invalide"); }
            else
            {
                int resultat = a ^ b;
                Console.WriteLine($"{a} ^ {b} = {resultat}");
            }
        }
    }
}
