using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public static class TasksTables
    {
        public static int SumTab(int[] tab)
        {
            int resultat = 0;
            foreach (int item in tab) { resultat += item; }
            return resultat;
        }

        public static int[] OpeTab(int[] tab, char ope, int b)
        {
            int[] resultat = new int[tab.Length];
            switch (ope)
            {
                case '+': for (int i = 0; i < tab.Length; i++) { resultat[i] = tab[i] + b; } break;
                case '-': for (int i = 0; i < tab.Length; i++) { resultat[i] = tab[i] - b; } break;
                case '*': for (int i = 0; i < tab.Length; i++) { resultat[i] = tab[i] * b; } break;
                default: resultat = null; break;
            }
            return resultat;
        }

        public static int[] ConcatTab(int[] tab1, int[] tab2)
        {
            int[] resultat = new int[5];
            return resultat;
        }
    }
}
