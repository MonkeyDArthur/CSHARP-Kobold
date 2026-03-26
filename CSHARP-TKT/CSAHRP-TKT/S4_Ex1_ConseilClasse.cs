using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Serie_IV
{
    public static class ConseilClasse
    {
        public static void SchoolMeans(string input, string output)
        {
            using (FileStream fileIn = new FileStream(input, FileMode.Open, FileAccess.Read))
            using (StreamReader reader = new StreamReader(fileIn)) 
            {
                Dictionary<string, List<double>> matieres = new Dictionary<string, List<double>>();
                foreach (var ligne in File.ReadLines(input))
                {
                    var contenu = ligne.Split(';');
                    string matiere = contenu[1];
                    double note = double.Parse(contenu[2]);

                    if (!matieres.ContainsKey(matiere)) { matieres[matiere] = new List<double>(); }
                    matieres[matiere].Add(note);
                }

                using (FileStream fileOut = new FileStream(output, FileMode.Create, FileAccess.Write))
                using (StreamWriter writer = new StreamWriter(fileOut))
                {
                    foreach (var paire in matieres)
                    {
                        double moyenne = paire.Value.Average();
                        writer.WriteLine($"{paire.Key};{moyenne:F1};");
                    }
                }
                Console.WriteLine($"Fichier d'entréé : {input}\nFichier de sortie : {output}\nLes moyenne ont été calculé pour chaque matières.");
            }
        }
    }
}
