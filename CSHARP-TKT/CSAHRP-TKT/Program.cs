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
            string input_str; int input_int;
            
            //EXECICE 1 SERIE I
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

            //EXECICE 2 SERIE I
            Console.WriteLine("\n\nExercice II SI - Horloge parlante");
            
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(10));
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(0));
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(20));
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(-2));
            Console.WriteLine(Serie_I.SpeakingClock.GoodDay(25));

            //EXECICE 3 SERIE I
            Console.WriteLine("\n\nExercice III SI - Construction d'une pyramide");
            
            Serie_I.Pyramid.PyramidConstruction(10, true);
            Serie_I.Pyramid.PyramidConstruction(10, false);
            Serie_I.Pyramid.PyramidConstruction(0, true);

            //EXECICE 4 SERIE I
            Console.WriteLine("\n\nExercice IV SI - Factorielle");
            do
            {
                Console.WriteLine("Saisir un nombre");
                input_str = Console.ReadLine();
            }   while (!int.TryParse(input_str, out input_int));
            Console.WriteLine($"Factorielle de {input_int} : {Factorial.Factorial_(input_int)}");
            Console.WriteLine($"Factorielle de {input_int} : {Factorial.FactorialRecursive(input_int)} [R]");

            //EXECICE 5 SERIE I
            Console.WriteLine("\n\nExercice V SI - Les nombres premiers");
            Serie_I.PrimeNumbers.DisplayPrimes();

            //EXECICE 6 SERIE I
            Console.WriteLine("\n\nExercice VI SI - Algorithme d'Euclide");
            int a, b;
            do
            {
                Console.Write("Saisir le premier nombre : ");
                input_str = Console.ReadLine();
            }   while (!int.TryParse(input_str, out a));
            do
            {
                Console.Write("Saisir le second nombre : ");
                input_str = Console.ReadLine();
            }   while (!int.TryParse(input_str, out b));
            Console.WriteLine($"PGCD de {a} et {b} : {Serie_I.Euclide.Pgcd(a, b)}");
            

            //EXERCICE I SERIE II
            Console.WriteLine("\n\nExercice I SII - Atelier autour des tableaux");

            int[] tab1 = { -1, 4, 7, 12, -6, 5 };
            int resultat1 = Serie_II.TasksTables.SumTab(tab1);
            Console.WriteLine("Somme des éléments d'un tableau :");
            Console.WriteLine("Tableau  : [" + string.Join(", ", tab1) + "]");
            Console.WriteLine("Resultat : [" + string.Join(", ", resultat1) + "]");
            Console.WriteLine();

            int c = 2;
            int[] resultat2 = Serie_II.TasksTables.OpeTab(tab1, '+', c);
            Console.WriteLine("Opération sur un tableau : ");
            Console.WriteLine("Tableau   : [" + string.Join(", ", tab1) + "]");
            Console.WriteLine($"Operateur : + {c}");
            Console.WriteLine("Resultat  : [" + string.Join(", ", resultat2) + "]");
            Console.WriteLine();

            resultat2 = Serie_II.TasksTables.OpeTab(tab1, '-', c);
            Console.WriteLine("Opération sur un tableau : ");
            Console.WriteLine("Tableau   : [" + string.Join(", ", tab1) + "]");
            Console.WriteLine($"Operateur : - {c}");
            Console.WriteLine("Resultat  : [" + string.Join(", ", resultat2) + "]");
            Console.WriteLine();

            resultat2 = Serie_II.TasksTables.OpeTab(tab1, '*', c);
            Console.WriteLine("Opération sur un tableau : ");
            Console.WriteLine("Tableau   : [" + string.Join(", ", tab1) + "]");
            Console.WriteLine($"Operateur : * {c}");
            Console.WriteLine("Resultat  : [" + string.Join(", ", resultat2) + "]");
            Console.WriteLine();

            int[] tab2 = { -2, 8 };
            int[] resultat3 = Serie_II.TasksTables.ConcatTab(tab1, tab2);
            Console.WriteLine("Concarénation de deux tableux :");
            Console.WriteLine("Tableau 1  : [" + string.Join(", ", tab1) + "]");
            Console.WriteLine("Tableau 2  : [" + string.Join(", ", tab2) + "]");
            Console.WriteLine("Resultat   : [" + string.Join(", ", resultat3) + "]");
            Console.WriteLine();

            //EXERCICE 2 SERIE II
            Console.WriteLine("\n\nExercice II SII - Morpion");

            char[,] grille =
            {
                {' ',' ',' '},
                {' ',' ',' '},
                {' ',' ',' '}
            };
            int end = -1; int num = 1; char type = 'X';
            string input_str1; int input_int1;
            string input_str2; int input_int2;

            while (end == -1)
            {
                Serie_II.Morpion.DisplayMorpion(grille);
                do
                {
                    Console.Write($"Joueur {num} saisissez un numero de ligne : ");
                    input_str1 = Console.ReadLine();
                } while (!int.TryParse(input_str1, out input_int1));
                do
                {
                    Console.Write($"Joueur {num} saisissez un numero de colonne : ");
                    input_str2 = Console.ReadLine();
                } while (!int.TryParse(input_str2, out input_int2));

                grille[input_int1 - 1, input_int2 - 1] = type;
                end = Serie_II.Morpion.CheckMorpion(grille, type);
                if (end == 1)
                {
                    Serie_II.Morpion.DisplayMorpion(grille);
                    Console.WriteLine($"Partie Fini : Joueur {num} a gagné la partie.");
                }
                else if (end == 0)
                {
                    Serie_II.Morpion.DisplayMorpion(grille);
                    Console.WriteLine($"Partie Fini : Match nul.");
                }
                else
                {
                    if (num == 1) { num = 2; type = 'O'; }
                    else { num = 1; type = 'X'; }
                }
            }
            

            //EXECICE 3 SERIE II
            Console.WriteLine("\n\nExercice III SII - Recherche d'un élément");

            int valeur = 2; int resultatE3S2;
            int[] tableau1 = {1, -5, 10, -3, 0, 4, 2, -7};
            resultatE3S2 = Serie_II.Search.LinearSearch(tableau1, valeur);
            Console.WriteLine("Liste : [" + string.Join(", ", tableau1) + "]");
            if (resultatE3S2 != -1) { Console.WriteLine($"La valeur {valeur} a été trouvée en position {resultatE3S2 + 1}."); }
            else { Console.WriteLine($"La valeur {valeur} n'a pas été trouvée, soit elle n'est pas dans la liste soit la liste est vide."); }
            Console.WriteLine();

            int[] tableau2 = new int[0];
            resultatE3S2 = Serie_II.Search.LinearSearch(tableau2, valeur);
            Console.WriteLine("Liste : [" + string.Join(", ", tableau2) + "]");
            if (resultatE3S2 != -1) { Console.WriteLine($"La valeur {valeur} a été trouvée en position {resultatE3S2 + 1}."); }
            else { Console.WriteLine($"La valeur {valeur} n'a pas été trouvée, soit elle n'est pas dans la liste soit la liste est vide."); }
            Console.WriteLine();

            int[] tableau3 = { 1, -5, 10, -3, 0, 4, 9, -7 };
            resultatE3S2 = Serie_II.Search.LinearSearch(tableau3, valeur);
            Console.WriteLine("Liste : [" + string.Join(", ", tableau3) + "]");
            if (resultatE3S2 != -1) { Console.WriteLine($"La valeur {valeur} a été trouvée en position {resultatE3S2 + 1}."); }
            else { Console.WriteLine($"La valeur {valeur} n'a pas été trouvée, soit elle n'est pas dans la liste soit la liste est vide."); }
            Console.WriteLine();

            valeur = -10;
            int[] tableau4 = {-10, -9, -8, -7, -6, -5, -4, -3, -2, -1, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            resultatE3S2 = Serie_II.Search.BinarySearch(tableau4, valeur);
            Console.WriteLine("Liste : [" + string.Join(", ", tableau4) + "]");
            if (resultatE3S2 != -1) { Console.WriteLine($"La valeur {valeur} a été trouvée en position {resultatE3S2 + 1}."); }
            else { Console.WriteLine($"La valeur {valeur} n'a pas été trouvée, soit elle n'est pas dans la liste soit la liste est vide."); }
            Console.WriteLine();

            //EXECICE 4 SERIE II
            Console.WriteLine("\n\nExercice IV SII - Matrice");

            int[] vecteur1 = { 1, 2, 3 };
            int[] vecteur2 = { 4, 5, 6 };
            int[] vecteur3 = { 4, 5 };

            int[][] resultatE4S2 = Serie_II.Matrice.BuildingMatrix(vecteur1, vecteur2);
            Console.WriteLine("Resultat :"); Serie_II.Matrice.AffMatrice(resultatE4S2);
            Console.WriteLine();
            resultatE4S2 = Serie_II.Matrice.BuildingMatrix(vecteur1, vecteur3);
            Console.WriteLine("Resultat :"); Serie_II.Matrice.AffMatrice(resultatE4S2);
            Console.WriteLine();


            int[][] matrice1 = { new int[] { 1, 2 }, new int[] { 4, 6 }, new int[] { -1, 8 } };
            int[][] matrice2 = { new int[] { -1, 5 }, new int[] { -4, 0 }, new int[] { 0, 2 } };

            resultatE4S2 = Serie_II.Matrice.AddSous(matrice1, matrice2, '+');
            Console.WriteLine("Resultat :"); Serie_II.Matrice.AffMatrice(resultatE4S2);
            Console.WriteLine();
            resultatE4S2 = Serie_II.Matrice.AddSous(matrice1, matrice2, '-');
            Console.WriteLine("Resultat :"); Serie_II.Matrice.AffMatrice(resultatE4S2);
            Console.WriteLine();

            int[][] matrice3 = { new int[] { 1, 2 }, new int[] { 4, 6 }, new int[] { -1, 8 } };
            int[][] matrice4 = { new int[] { -1, 5, 0 }, new int[] { -4, 0, 1 } };
            resultatE4S2 = Serie_II.Matrice.Multiplication(matrice3, matrice4);
            Console.WriteLine("Resultat :"); Serie_II.Matrice.AffMatrice(resultatE4S2);
            Console.WriteLine();

            //EXECICE 5 SERIE II
            Console.WriteLine("\n\nExercice V SII - Crible d'Eratosthène");
            
            do
            {
                Console.Write($"Donnez un nombre N, on cherchera l'ensemble de nombres premiers inférieurs : ");
                input_str = Console.ReadLine();
            } while (!int.TryParse(input_str, out input_int));
            int[] resultatE5S2 = Serie_II.CribleErastosthne.EratosthenesSieve(input_int);
            Console.WriteLine($"Nb premiers jusqu'à {input_int} : [" + string.Join(", ", resultatE5S2) + "]");
            Console.WriteLine();
            

            //EXECICE 6 SERIE II
            Console.WriteLine("\n\nExercice VI SII - QCM");

            QCM[] QCM = Serie_II.QCM.chargementQCM();
            Serie_II.QCM.AskQuestions(QCM);
            Console.ReadKey();
        }
    }
}
