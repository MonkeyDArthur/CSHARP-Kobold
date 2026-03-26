using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Serie_I;
using Serie_II;
using Serie_III;
using Serie_IV;


namespace ExerciceS1
{
    class Program
    {
        public static void Main(string[] args)
        {
            string input_str; int input_int;
            /*
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

            int[][] matrice5 = {    new int[] {-1,-4,0,1,3,7},
                                    new int[] {-2,-8,0,2,6,14},
                                    new int[] {-3,-12,0,3,9,21},
                                    new int[] {-5,-20,0,5,15,35},
                                    new int[] {-6,-24,0,6,18,42},
                                    new int[] {-7,-24,0,7,18,20} };
            Console.WriteLine("Resultat :"); Serie_II.Matrice.AffMatrice(matrice5);

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
            

            //EXECICE 1 SERIE III
            Console.WriteLine("\n\nExercice I SIII - Traitement Administratif");

            string Texte = "Nikolai, où as-tu caché mes dollars ? Je dois aller à l'ouest ! L'armée m'appelle pour aller en Afghanistan.";
            string[] motInterdit = { "dollars", "regan", "afghanistan", "ouest", "crime", "défaite" };
            Serie_III.TraitementString.EliminateSeditiousThoughts(Texte, motInterdit);
            Console.WriteLine();

            string Id1 = "M.  Burak       ARTI        26";
            string Id2 = "Mme Anna        POLOSKA     28";
            string Id3 = "Mr  Guillaume   DERANGER    25";
            string Id4 = "M.  Aubin       LAGARDE     xx";
            string Id5 = "M.  3630        TAMERE      26";
            string Id6 = "M.  Corentin    3630        26";
            string Id7 = "M.   Burak       ARTI        26";

            bool estOK = Serie_III.TraitementString.ControlFormat(Id1);
            if (estOK) { Console.WriteLine($"{Id1}\nFormat OK"); } else { Console.WriteLine($"{Id1}\nFormat KO"); }
            estOK = Serie_III.TraitementString.ControlFormat(Id2);
            if (estOK) { Console.WriteLine($"{Id2}\nFormat OK"); } else { Console.WriteLine($"{Id2}\nFormat KO"); }
            estOK = Serie_III.TraitementString.ControlFormat(Id3);
            if (estOK) { Console.WriteLine($"{Id3}\nFormat OK"); } else { Console.WriteLine($"{Id3}\nFormat KO"); }
            estOK = Serie_III.TraitementString.ControlFormat(Id4);
            if (estOK) { Console.WriteLine($"{Id4}\nFormat OK"); } else { Console.WriteLine($"{Id4}\nFormat KO"); }
            estOK = Serie_III.TraitementString.ControlFormat(Id5);
            if (estOK) { Console.WriteLine($"{Id5}\nFormat OK"); } else { Console.WriteLine($"{Id5}\nFormat KO"); }
            estOK = Serie_III.TraitementString.ControlFormat(Id6);
            if (estOK) { Console.WriteLine($"{Id6}\nFormat OK"); } else { Console.WriteLine($"{Id6}\nFormat KO"); }
            estOK = Serie_III.TraitementString.ControlFormat(Id7);
            if (estOK) { Console.WriteLine($"{Id7}\nFormat OK"); } else { Console.WriteLine($"{Id7}\nFormat KO"); }
            Console.WriteLine();

            Texte = "2026-03-25 : Nikolai, où as-tu caché mes dollars ? Je dois aller à l'ouest le 2026-04-05 ! L'armée m'appelle pour aller en Afghanistan pour le 2026-04-20.";
            Console.WriteLine($"Texte de départ  : {Texte}");
            Texte = Serie_III.TraitementString.ChangeDate(Texte);
            Console.WriteLine($"Texte de modifié : {Texte}");
            Console.WriteLine();

            //EXECICE 2 SERIE III
            Console.WriteLine("\n\nExercice II SIII - Code César");

            Console.WriteLine("Codage Cesar");
            Texte = "Nikolai, où as-tu caché mes dollars ? Je dois aller à l'ouest ! L'armée m'appelle pour aller en Afghanistan.";
            Console.WriteLine($"Texte de départ: {Texte}");
            Texte = Serie_III.CodeCesar.CesarCode(Texte);
            Console.WriteLine($"Texte coder    : {Texte}");
            Texte = Serie_III.CodeCesar.DecryptCesarCode(Texte);
            Console.WriteLine($"Texte decoder  : {Texte}");
            Console.WriteLine();

            Console.WriteLine("Codage Cesar avec décalage de X");
            Texte = "Nikolai, où as-tu caché mes dollars ? Je dois aller à l'ouest ! L'armée m'appelle pour aller en Afghanistan.";
            Console.WriteLine($"Texte de départ: {Texte}");
            Texte = Serie_III.CodeCesar.GeneralCesarCode(Texte,5);
            Console.WriteLine($"Texte coder    : {Texte}");
            Texte = Serie_III.CodeCesar.GeneralDecryptCesarCode(Texte,5);
            Console.WriteLine($"Texte decoder  : {Texte}");
            Console.WriteLine();

            //EXECICE 3 SERIE III
            Console.WriteLine("\n\nExercice III SIII - Morse");

            Texte = "=.===.=.=...=.....===.===...===.===.===...===.=...===.....=.=.=...=.===...=.=...===.=...===.....===.===...=.=...===.=.===.=...=.=.=.=...=...=.===.=.=";
            Console.WriteLine($"Texte de départ: {Texte}");
            int nbLettre = Serie_III.Morse.LettersCount(Texte);
            Console.WriteLine($"Il y a {nbLettre} lettres dans la phrase.");
            int nbMot = Serie_III.Morse.WordsCount(Texte);
            Console.WriteLine($"Il y a {nbMot} mots dans la phrase.");
            string traduction = Serie_III.Morse.MorseTranslation(Texte);
            Console.WriteLine($"Traduction de la phrase V1: {traduction}");
            Console.WriteLine();

            Texte = "===..=.===..=....===.===..===...===.=..=....=......===.===...===.===.===...=.===.=...=.=.=...=";
            Console.WriteLine($"Texte de départ: {Texte}");
            traduction = Serie_III.Morse.EfficientMorseTranslation(Texte);
            Console.WriteLine($"Traduction de la phrase V2: {traduction}");

            Texte = "NIQUEZ VOUS JE MEN BATS LES COUILLES EN GUILLAUME TES UNE MERDE";
            Console.WriteLine($"Texte de départ: {Texte}");
            Texte = Serie_III.Morse.MorseEncryption(Texte);
            Console.WriteLine($"Traduction en morse: {Texte}");
            traduction = Serie_III.Morse.MorseTranslation(Texte);
            Console.WriteLine($"Traduction de la phrase V1: {traduction}");
            traduction = Serie_III.Morse.EfficientMorseTranslation(Texte);
            Console.WriteLine($"Traduction de la phrase V2: {traduction}");

            //EXECICE 1 SERIE IV
            Console.WriteLine("\n\nExercice I SIV - Conseil de Classe");
            Directory.SetCurrentDirectory(@"C:\Users\Formation\Desktop\");
            string cheminEntree = "fileEntree.txt";
            string cheminSortie = "fileSortie.txt";
            Serie_IV.ConseilClasse.SchoolMeans(cheminEntree, cheminSortie);
            Console.WriteLine();

            //EXECICE 2 SERIE IV
            Console.WriteLine("\n\nExercice II SIV - Morpion");
            Serie_IV.Morpion.MorpionGame();
            Console.WriteLine();
            */

            //EXECICE 3 SERIE IV
            Console.WriteLine("\n\nExercice III SIV - Contact téléphonique");
            PhoneBook pageJaune = new PhoneBook();
            pageJaune.DisplayPhoneBook();
            Console.WriteLine();

            Console.WriteLine(pageJaune.IsValidPhoneNumber("0612345678"));
            Console.WriteLine(pageJaune.IsValidPhoneNumber("00612345678"));
            Console.WriteLine(pageJaune.IsValidPhoneNumber("0061234567"));
            Console.WriteLine();


            pageJaune.AddPhoneNumber("0612345678", "Lucas");
            pageJaune.AddPhoneNumber("0678452391", "Emma");
            pageJaune.AddPhoneNumber("0623984756", "Noah");
            pageJaune.AddPhoneNumber("0691827364", "Lina");
            pageJaune.AddPhoneNumber("0654738291", "Hugo");
            pageJaune.AddPhoneNumber("0638291745", "Chloé");
            pageJaune.AddPhoneNumber("0683749201", "Nathan");
            pageJaune.AddPhoneNumber("0648291736", "Sarah");
            pageJaune.AddPhoneNumber("0671928345", "Yanis");
            pageJaune.AddPhoneNumber("0628471935", "Inès");
            pageJaune.DisplayPhoneBook();
            Console.WriteLine();

            Console.WriteLine(pageJaune.ContainsPhoneContact("0623984756"));
            pageJaune.PhoneContact("0623984756");
            Console.WriteLine(pageJaune.DeletePhoneNumber("0623984756"));
            Console.WriteLine();

            pageJaune.DisplayPhoneBook();
            Console.WriteLine();





            Console.ReadKey();
        }
    }
}
