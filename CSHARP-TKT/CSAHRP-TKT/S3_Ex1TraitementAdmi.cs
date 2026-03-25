using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Xml.Schema;

namespace Serie_III
{
    public static class TraitementString
    {
        public static void EliminateSeditiousThoughts(string texte, string[] liste)
        {
            string texteCensure = texte;
            for(int i = 0; i < liste.Length; i++)
            {
                int taille = liste[i].Length;
                string censure = new string('X', taille);
                texteCensure = Regex.Replace(texteCensure, Regex.Escape(liste[i]), censure, RegexOptions.IgnoreCase);
            }
            Console.WriteLine($"Texte original :\n{texte}");
            Console.WriteLine($"Texte censuré :\n{texteCensure}");
        }
        public static bool ControlFormat(string line)
        {
            string[] civilite = { "M.  ", "Mme ", "Mlle" };
            
            if(line.Length > 30) return false;
            else
            {
                string[] infos = new string[4];
                infos[0] = line.Substring(0,4); infos[1] = line.Substring(4,12); infos[2] = line.Substring(16,12); infos[3] = line.Substring(28,2);
                
                if (infos[0] != "M.  " && infos[0] != "Mme " && infos[0] != "Mlle") { return false; }
                foreach (char lettre in infos[1].Trim()) { if (!char.IsLetter(lettre)) return false; }
                foreach (char lettre in infos[2].Trim()) { if (!char.IsLetter(lettre)) return false; }
                foreach (char chiffre in infos[3].Trim()) { if (!char.IsDigit(chiffre)) return false; }
                return true;
            }
        }
        public static string ChangeDate(string texte)
        {
            string date;
            string pattern = @"[0-9]{4}-[0-9]{2}-[0-9]{2}";
            foreach ( Match match in Regex.Matches(texte, pattern))
            {
                date = match.ToString();
                texte = Regex.Replace(texte, date, $"{date[8]}{date[9]}-{date[5]}{date[6]}-{date[0]}{date[1]}{date[2]}{date[3]}");
            }
            return texte;
        }
    }
}
