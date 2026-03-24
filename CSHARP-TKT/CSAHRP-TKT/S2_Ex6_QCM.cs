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
    public class QCM
    {
        private struct QCM
        {
            public string Question;
            public string[] Reponse;
            public int Solution;
            public int Poids;
        }
        bool QcmValidity(QCM qcm)
        {
            if (qcm.Reponse == null || qcm.Reponse.Length == 0)
                return false;

            if (qcm.Solution < 0 || qcm.Solution >= qcm.Reponse.Length)
                return false;

            if (qcm.Poids <= 0)
                return false;

            return true;
        }
        int AskQuestion(QCM qcm)
        {
            if (!QcmValidity(qcm))
                throw new ArgumentException("QCM invalide");

            Console.WriteLine(qcm.Question);

            for (int i = 0; i < qcm.Reponse.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {qcm.Reponse[i]}");
            }

            int userAnswer;

            while (true)
            {
                Console.Write("Réponse : ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out userAnswer) &&
                    userAnswer >= 1 && userAnswer <= qcm.Reponse.Length)
                {
                    break;
                }

                Console.WriteLine("Réponse invalide !");
            }

            if (userAnswer - 1 == qcm.Solution)
                return qcm.Poids;

            return 0;
        }
        void AskQuestions(QCM[] qcms)
        {
            int totalScore = 0;
            int maxScore = 0;

            foreach (var qcm in qcms)
            {
                totalScore += AskQuestion(qcm);
                maxScore += qcm.Poids;
                Console.WriteLine();
            }

            Console.WriteLine($"Résultats du questionnaire : {totalScore} / {maxScore}");
        }
    }
}
