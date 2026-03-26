using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Collections;

namespace Serie_IV
{
    public static class Morpion
    {
        public static void MorpionGame()
        {
            Dictionary<string,char> grille = new Dictionary<string,char>();
            grille["A1"] = '_'; grille["A2"] = '_'; grille["A3"] = '_';
            grille["B1"] = '_'; grille["B2"] = '_'; grille["B3"] = '_';
            grille["C1"] = '_'; grille["C2"] = '_'; grille["C3"] = '_';

            Console.WriteLine("Début de la parti de Morpion !");
            DisplayMorpion(grille);

            int end = -1; int num = 1; char type = 'X'; string input;

            while (end == -1)
            {
                while (true)
                {
                    Console.Write($"Coup du joueur {num} : ");
                    input = Console.ReadLine().ToUpper().Trim().Replace(@"[ ]{1,}","");
                    if (grille.ContainsKey(input)) 
                    {
                        if (grille[input] == '_') { grille[input] = type; break; }
                        else { Console.WriteLine("Cette position a déja été joué!"); }
                    }
                    Console.WriteLine("Réponse invalide !");
                }
                DisplayMorpion(grille);
                
                end = CheckMorpion(grille, type);
                if (end == 1) { Console.WriteLine($"Partie Fini : Joueur {num} a gagné la partie."); }
                else if (end == 0) { Console.WriteLine($"Partie Fini : Match nul."); }
                else 
                {
                    if (num == 1) { num = 2; type = 'O'; }
                    else { num = 1; type = 'X'; }
                }
            }
        }

        public static void DisplayMorpion(Dictionary<string,char> grille)
        {
            Console.WriteLine($"{grille["A1"]} {grille["A2"]} {grille["A3"]}");
            Console.WriteLine($"{grille["B1"]} {grille["B2"]} {grille["B3"]}");
            Console.WriteLine($"{grille["C1"]} {grille["C2"]} {grille["C3"]}");
        }

        public static int CheckMorpion(Dictionary<string, char> grille, char type)
        {
            string[,] combiGagnant = {  { "A1", "A2", "A3" },
                                        { "B1", "B2", "B3" },
                                        { "C1", "C2", "C3" },
                                        { "A1", "B1", "C1" },
                                        { "A2", "B2", "C2" },
                                        { "A3", "B3", "C3" },
                                        { "A1", "B2", "C3" },
                                        { "A3", "B2", "C1" }};
            int nbVide = 0;
            for (int i = 0; i < 8; i++)
            {
                if (grille[combiGagnant[i, 0]] == type && grille[combiGagnant[i, 1]] == type && grille[combiGagnant[i, 2]] == type) { return 1; }
            }
            foreach (var paire in grille) { if (paire.Value == '_') { nbVide++; } }
            if (nbVide == 0) { return 0; }
            return -1;
        }
    }
}