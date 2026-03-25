using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Reflection.Emit;

//Q1 : Etant donnée que le decalage de la lettre est de 3, le choix d'un dictionnaire est pertinent pour stocker l'alphabet Morse, car plus rapide pour les recherche que les autres structures

namespace Serie_III
{
    public class Morse
    {
        private const string Taah = "===";
        private const string Ti = "=";
        private const string Point = ".";
        private const string PointLetter = "...";
        private const string PointWord = ".....";

        public static readonly Dictionary<string, char> morseAlphabet;

        static Morse()
        {
            morseAlphabet = new Dictionary<string, char>() {    {$"{Ti}.{Taah}",                'A'},
                                                                {$"{Taah}.{Ti}.{Ti}.{Ti}",      'B'},
                                                                {$"{Taah}.{Ti}.{Taah}.{Ti}",    'C'},
                                                                {$"{Taah}.{Ti}.{Ti}",           'D'},
                                                                {$"{Ti}",                       'E'},
                                                                {$"{Ti}.{Ti}.{Taah}.{Ti}",      'F'},
                                                                {$"{Taah}.{Taah}.{Ti}",         'G'},
                                                                {$"{Ti}.{Ti}.{Ti}.{Ti}",        'H'},
                                                                {$"{Ti}.{Ti}",                  'I'},
                                                                {$"{Ti}.{Taah}.{Taah}.{Taah}",  'J'},
                                                                {$"{Taah}.{Ti}.{Taah}",         'K'},
                                                                {$"{Ti}.{Taah}.{Ti}.{Ti}",      'L'},
                                                                {$"{Taah}.{Taah}",              'M'},
                                                                {$"{Taah}.{Ti}",                'N'},
                                                                {$"{Taah}.{Taah}.{Taah}",       'O'},
                                                                {$"{Ti}.{Taah}.{Taah}.{Ti}",    'P'},
                                                                {$"{Taah}.{Taah}.{Ti}.{Taah}",  'Q'},
                                                                {$"{Ti}.{Taah}.{Ti}",           'R'},
                                                                {$"{Ti}.{Ti}.{Ti}",             'S'},
                                                                {$"{Taah}",                     'T'},
                                                                {$"{Ti}.{Ti}.{Taah}",           'U'},
                                                                {$"{Ti}.{Ti}.{Ti}.{Taah}",      'V'},
                                                                {$"{Ti}.{Taah}.{Taah}",         'W'},
                                                                {$"{Taah}.{Ti}.{Ti}.{Taah}",    'X'},
                                                                {$"{Taah}.{Ti}.{Taah}.{Taah}",  'Y'},
                                                                {$"{Taah}.{Taah}.{Ti}.{Ti}",    'Z'} };
        }

        public static int LettersCount(string code)
        {
            string[] lettre = code.Split(new string[] { PointLetter }, StringSplitOptions.None);
            int nbLettre = lettre.Length;
            return nbLettre;
        }

        public static int WordsCount(string code)
        {
            string[] mots = code.Split(new string[] { PointWord }, StringSplitOptions.None);
            int nbMot = mots.Length;
            return nbMot;
        }

        public static string MorseTranslation(string code)
        {
            StringBuilder resultat = new StringBuilder();

            string[] mots = code.Split(new string[] { PointWord }, StringSplitOptions.None);
            foreach (string mot in mots)
            {
                string[] lettres = mot.Split(new string[] { PointLetter }, StringSplitOptions.None);
                foreach (string lettre in lettres) { if (morseAlphabet.ContainsKey(lettre)) { resultat.Append(morseAlphabet[lettre]); } }
                resultat.Append(" ");
            }
            return resultat.ToString();
        }

        public static string EfficientMorseTranslation(string code)
        {
            StringBuilder resultat = new StringBuilder();
            string rectificationImpulsion; string rectificationLettre; string rectificationMot;
            rectificationMot = Regex.Replace(code, "[.]{6,}", PointWord);

            string[] mots = rectificationMot.Split(new string[] { PointWord }, StringSplitOptions.None);
            foreach (string mot in mots)
            {
                rectificationLettre = Regex.Replace(mot, "[.]{3,4}", PointLetter);
                string[] lettres = rectificationLettre.Split(new string[] { PointLetter }, StringSplitOptions.None);
                foreach (string lettre in lettres)
                {
                    rectificationImpulsion = Regex.Replace(lettre, "[.]{1,2}", Point);
                    if (morseAlphabet.ContainsKey(rectificationImpulsion)) { resultat.Append(morseAlphabet[rectificationImpulsion]); }
                }
                resultat.Append(" ");
            }
            return resultat.ToString();
        }

        public static string MorseEncryption(string sentence)
        {
            sentence = Regex.Replace(sentence.ToUpper(), "[ ]{1,}", PointWord);
            char[] aCoder = sentence.ToCharArray();
            string lettreCoder = "";
            string msgCoder = "";

            foreach (char c in aCoder)
            {
                foreach (var pair in morseAlphabet)
                {
                    if (pair.Value == c) { lettreCoder = pair.Key + "..."; break; }
                    else { lettreCoder = c.ToString(); }
                }
                msgCoder += lettreCoder;
            }
            return msgCoder;
        }
    }
}