using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Serie_I;
using Serie_II;


namespace ExerciceS1
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Exercice I SI - Opérations élémentaires");
                //Opérations de base
                Serie_I.ElementaryOperations.BasicOperation(3, 4, '+');
                Serie_I.ElementaryOperations.BasicOperation(6, 2, '/');
                Serie_I.ElementaryOperations.BasicOperation(3, 0, '/');
                Serie_I.ElementaryOperations.BasicOperation(6, 9, 'L');

                //Division entière
                Serie_I.ElementaryOperations.IntegerDivision(12, 4);
                Serie_I.ElementaryOperations.IntegerDivision(13, 4);
                Serie_I.ElementaryOperations.IntegerDivision(12, 0);

                //Puissance entière
                Serie_I.ElementaryOperations.Pow(3, 4);
                Serie_I.ElementaryOperations.Pow(2, 0);
                Serie_I.ElementaryOperations.Pow(6, -2);

            Console.WriteLine("\n\nExercice II SI - Horloge parlante");
                Console.WriteLine(Serie_I.SpeakingClock.GoodDay(10));
                Console.WriteLine(Serie_I.SpeakingClock.GoodDay(0));
                Console.WriteLine(Serie_I.SpeakingClock.GoodDay(20));
                Console.WriteLine(Serie_I.SpeakingClock.GoodDay(-2));
                Console.WriteLine(Serie_I.SpeakingClock.GoodDay(25));

            Console.WriteLine("\n\nExercice III SI - Construction d'une pyramide");
                Serie_I.Pyramid.PyramidConstruction(10, true);
                Serie_I.Pyramid.PyramidConstruction(10, false);
                Serie_I.Pyramid.PyramidConstruction(0, true);

            Console.WriteLine("\n\nExercice IV SI - Factorielle");
                int number; string input;
                do
                {
                    Console.WriteLine("Saisir un nombre");
                    input = Console.ReadLine();
                } while (!int.TryParse(input, out number));

                Console.WriteLine($"Factorielle de {number} : {Factorial.Factorial_(number)}");
                Console.WriteLine($"Factorielle de {number} : {Factorial.FactorialRecursive(number)} [R]");

            Console.WriteLine("\n\nExercice V SI - Les nombres premiers");
                Serie_I.PrimeNumbers.DisplayPrimes();

            Console.WriteLine("\n\nExercice VI SI - Algorithme d'Euclide");
                int a, b;
                do
                {
                    Console.Write("Saisir le premier nombre : ");
                    input = Console.ReadLine();
                } while (!int.TryParse(input, out a));
                do
                {
                    Console.Write("Saisir le second nombre : ");
                    input = Console.ReadLine();
                } while (!int.TryParse(input, out b));

                Console.WriteLine($"PGCD de {a} et {b} : {Serie_I.Euclide.Pgcd(a, b)}");
            
            Console.WriteLine("\n\nExercice I SII - elier autour des tableaux");
                int [] tab1 =  {-1, 4, 7, 12, -6, 5};
                int resultat1 = Serie_II.TasksTables.SumTab(tab1);
                Console.WriteLine("Somme des éléments d'un tableau :");
                Console.Write("Tableau : [");
                foreach (int item in tab1) { Console.Write($"{item}, "); }
                Console.Write($"]\nSomme : {resultat1}");
                
                b = 2; char ope = '+';
                int[] resultat2 = Serie_II.TasksTables.OpeTab(tab1, ope, b);
                Console.WriteLine("Opération sur un tableau : ");
                Console.Write("Tableau : [");
                foreach (int item in tab1) { Console.Write($"{item}, "); }
                Console.Write($"]\nOperateur : {ope} {b}\n");
                Console.Write("resultat : [");
                foreach (int item in resultat2) { Console.Write($"{item}, "); }
                Console.Write($"]");

                ope = '-';
                int[] resultat2 = Serie_II.TasksTables.OpeTab(tab1, ope, b);
                Console.WriteLine("Opération sur un tableau : ");
                Console.Write("Tableau : [");
                foreach (int item in tab1) { Console.Write($"{item}, "); }
                Console.Write($"]\nOperateur : {ope} {b}\n");
                Console.Write("resultat : [");
                foreach (int item in resultat2) { Console.Write($"{item}, "); }
                Console.Write($"]");

                ope = '*';
                int[] resultat2 = Serie_II.TasksTables.OpeTab(tab1, ope, b);
                Console.WriteLine("Opération sur un tableau : ");
                Console.Write("Tableau : [");
                foreach (int item in tab1) { Console.Write($"{item}, "); }
                Console.Write($"]\nOperateur : {ope} {b}\n");
                Console.Write("resultat : [");
                foreach (int item in resultat2) { Console.Write($"{item}, "); }
                Console.Write($"]");

                int tab2 = {-2, 8};
                int[] resultat3 = Serie_II.TasksTables.ConcatTab(tab1, tab2);
                Console.WriteLine("Concarénation de deux tableux :");
                Console.WriteLine($"Tab1 : {tab1}");
                Console.WriteLine($"Tab2 : {tab2}");
                Console.WriteLine($"Resultat : {resultat3}");










            Console.ReadKey();
        }
    }
}
