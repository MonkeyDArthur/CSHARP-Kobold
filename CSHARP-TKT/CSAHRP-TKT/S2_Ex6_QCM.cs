using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Q1 b ;    Question : string car phrase unique et simple
//          Reponse  : string[] car plusieurs reponse possible
//          Solution : int car indice dans string[] reponse
//          Poids    : int car valeur numerique pour un poids

namespace Serie_II
{ 
    public struct QCM
    {
        public string Question;
        public string[] Reponse;
        public int Solution;
        public int Poids;

        public static bool QcmValidity(QCM qcm)
        {
            if (qcm.Reponse == null || qcm.Reponse.Length == 0) return false;
            if (qcm.Solution < 0 || qcm.Solution >= qcm.Reponse.Length) return false;
            if (qcm.Poids <= 0) return false;
            return true;
        }
        public static int AskQuestion(QCM qcm)
        {
            if (QcmValidity(qcm) == false) throw new ArgumentException("QCM invalide");

            Console.WriteLine(qcm.Question);
            for (int i = 0; i < qcm.Reponse.Length; i++) { Console.WriteLine($"{i + 1}. {qcm.Reponse[i]}"); }
            
            int reponseJoueur;
            while (true)
            {
                Console.Write("Réponse : ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out reponseJoueur) && 
                    reponseJoueur >= 1 
                    && reponseJoueur <= qcm.Reponse.Length)
                {
                    break;
                }
                Console.WriteLine("Réponse invalide !");
            }

            if (reponseJoueur - 1 == qcm.Solution) return qcm.Poids;
            return 0;
        }
        public static void AskQuestions(QCM[] qcms)
        {
            int scoreTotal = 0;
            int scoreMax = 0;

            foreach (var qcm in qcms)
            {
                scoreTotal += AskQuestion(qcm);
                scoreMax += qcm.Poids;
                Console.WriteLine();
            }
            Console.WriteLine($"Résultats du questionnaire : {scoreTotal} / {scoreMax}");
        }

        public static QCM[] chargementQCM()
        {
            QCM[] QCM = new QCM[] {
                new QCM {
                    Question = "Quel type est utilisé pour stocker un nombre entier ?",
                    Reponse = new string[] { "float", "int", "string", "bool" },
                    Solution = 1,
                    Poids = 1 },
                new QCM {
                    Question = "Quel est le résultat de 5 / 2 en C# (int) ?",
                    Reponse = new string[] { "2.5", "2", "3", "Erreur" },
                    Solution = 1,
                    Poids = 1 },
                new QCM {
                    Question = "Quel mot-clé permet de déclarer une constante ?",
                    Reponse = new string[] { "readonly", "const", "static", "final" },
                    Solution = 1,
                    Poids = 1 },
                new QCM {
                    Question = "Que fait Console.WriteLine() ?",
                    Reponse = new string[] {"Lire une entrée",
                                            "Afficher avec saut de ligne",
                                            "Afficher sans saut de ligne",
                                            "Supprimer du texte"},
                    Solution = 1,
                    Poids = 1 },
                new QCM {
                    Question = "Quelle boucle s'exécute tant qu'une condition est vraie ?",
                    Reponse = new string[] { "if", "while", "switch", "break" },
                    Solution = 1,
                    Poids = 1 },
                new QCM {
                    Question = "Quel est le résultat de true && false ?",
                    Reponse = new string[] { "true", "false", "null", "Erreur" },
                    Solution = 1,
                    Poids = 1 },
                new QCM {
                    Question = "Comment accéder au premier élément d’un tableau ?",
                    Reponse = new string[] { "tab[1]", "tab[0]", "tab.first()", "tab[-1]" },
                    Solution = 1,
                    Poids = 1 },
                new QCM {
                    Question = "Quelle instruction permet de gérer plusieurs cas ?",
                    Reponse = new string[] { "if", "switch", "for", "while" },
                    Solution = 1,
                    Poids = 1 },
                new QCM {
                    Question = "Que signifie static pour une méthode ?",
                    Reponse = new string[] {"Elle est privée",
                                            "Elle appartient à la classe",
                                            "Elle ne retourne rien",
                                            "Elle est lente" },
                    Solution = 1,
                    Poids = 1 },
                new QCM {
                    Question = "Quelle méthode permet de lire une saisie utilisateur ?",
                    Reponse = new string[] {"Console.Read()",
                                            "Console.ReadLine()",
                                            "Console.Input()",
                                            "Console.Get()" },
                    Solution = 1,
                    Poids = 1 }
                };
            return QCM;
        }
    }
}
