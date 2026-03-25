using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Serie_III
{
    public static class CodeCesar
    {
        public static string CesarCode(string line)
        {
            Dictionary<char, char> cesarTable = new Dictionary<char, char> {
                { 'A', 'D' }, { 'B', 'E' }, { 'C', 'F' }, { 'D', 'G' }, { 'E', 'H' },
                { 'F', 'I' }, { 'G', 'J' }, { 'H', 'K' }, { 'I', 'L' }, { 'J', 'M' },
                { 'K', 'N' }, { 'L', 'O' }, { 'M', 'P' }, { 'N', 'Q' }, { 'O', 'R' },
                { 'P', 'S' }, { 'Q', 'T' }, { 'R', 'U' }, { 'S', 'V' }, { 'T', 'W' },
                { 'U', 'X' }, { 'V', 'Y' }, { 'W', 'Z' }, { 'X', 'A' }, { 'Y', 'B' },
                { 'Z', 'C' } };

            line = line.ToUpper();
            char[] aCoder = line.ToCharArray();
            char lettreCoder;
            string msgCoder = "";
            foreach (char c in aCoder)
            {
                if (cesarTable.ContainsKey(c)) { lettreCoder = cesarTable[c]; }
                else { lettreCoder = c; }
                msgCoder += lettreCoder;
            }
            return msgCoder;
        }
        public static string DecryptCesarCode(string line)
        {
            Dictionary<char, char> cesarTable = new Dictionary<char, char> {
                { 'A', 'D' }, { 'B', 'E' }, { 'C', 'F' }, { 'D', 'G' }, { 'E', 'H' },
                { 'F', 'I' }, { 'G', 'J' }, { 'H', 'K' }, { 'I', 'L' }, { 'J', 'M' },
                { 'K', 'N' }, { 'L', 'O' }, { 'M', 'P' }, { 'N', 'Q' }, { 'O', 'R' },
                { 'P', 'S' }, { 'Q', 'T' }, { 'R', 'U' }, { 'S', 'V' }, { 'T', 'W' },
                { 'U', 'X' }, { 'V', 'Y' }, { 'W', 'Z' }, { 'X', 'A' }, { 'Y', 'B' },
                { 'Z', 'C' } };

            line = line.ToUpper();
            char[] aDecoder = line.ToCharArray();
            char lettreDecoder = ' ';
            string msgDecoder = "";
            foreach (char c in aDecoder)
            {
                foreach (var pair in cesarTable)
                {
                    if (pair.Value == c) { lettreDecoder = pair.Key; break; }
                    else { lettreDecoder = c; }
                }
                msgDecoder += lettreDecoder;
            }
            return msgDecoder;
        }
        public static string GeneralCesarCode(string line, int x)
        {
            char[] alphabet = {'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'};
            
            line = line.ToUpper();
            char[] aCoder = line.ToCharArray();
            char lettreCoder = ' ';
            string msgCoder = "";

            foreach (char c in aCoder)
            {
                for (int i = 0; i < alphabet.Length; i++)
                {
                    if (alphabet[i] == c) { lettreCoder = alphabet[(i + x)%26]; break; }
                    else { lettreCoder = c; }
                }
                msgCoder += lettreCoder;
            }
            return msgCoder;
        }
        public static string GeneralDecryptCesarCode(string line, int x)
        {
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };

            line = line.ToUpper();
            char[] aDecoder = line.ToCharArray();
            char lettreDecoder = ' ';
            string msgDecoder = "";

            foreach (char c in aDecoder)
            {
                for (int i = 0; i < alphabet.Length; i++)
                {
                    if (alphabet[i] == c) { lettreDecoder = alphabet[(i - x) % 26]; break; }
                    else { lettreDecoder = c; }
                }
                msgDecoder += lettreDecoder;
            }
            return msgDecoder;
        }
    }
}
