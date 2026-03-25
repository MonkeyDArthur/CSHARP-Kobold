using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serie_II
{
    public static class CribleErastosthne
    {
        public static int[] EratosthenesSieve(int n)
        {
            if (n < 2) { return new int[0]; }
            bool[] estPremier = new bool[n + 1];
            for (int i = 2; i <= n; i++) { estPremier[i] = true; }

            for (int i = 2; i * i <= n; i++)
            {
                if (estPremier[i]) { for (int j = i * i; j <= n; j += i) { estPremier[j] = false; } }
            }
            List<int> res = new List<int>();
            for (int i = 2; i <= n; i++) { if (estPremier[i] == true) { res.Add(i); } }
            return res.ToArray();
        }
    }
}
