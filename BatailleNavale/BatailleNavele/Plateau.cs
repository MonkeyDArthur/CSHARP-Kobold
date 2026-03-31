using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Bataille_Navale
{
    internal class Plateau
    {
        public Position[,] PlateauJeu { get; set; }

        public List<Bateau> Bateaux { get; set; }

        public Plateau(int taille)
        {
            PlateauJeu = new Position[taille, taille];
            Bateaux = new List<Bateau>()
            {
               new Bateau("A", 5, new List<Position>()),
               new Bateau("B", 4, new List<Position>()),
               new Bateau("C", 3, new List<Position>()),
               new Bateau("D", 3, new List<Position>()),
               new Bateau("E", 2, new List<Position>())
            };
        }

        public void CreationPlateau()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++) PlateauJeu[i, j] = new Position(i, j);
            }

            Random rng = new Random();
            foreach (Bateau bateau in Bateaux)
            {
                bool place = false;
                while (!place)
                {
                    int x = rng.Next(0, 10);
                    int y = rng.Next(0, 10);
                    bool estVertical = rng.Next(0, 2) == 0;

                    if (PlacerBateau(x, y, bateau.Taille, estVertical))
                    {
                        for (int i = 0; i < bateau.Taille; i++)
                        {
                            int casex = estVertical ? x + i : x;
                            int casey = estVertical ? y : y + i;
                            bateau.Positions.Add(new Position(casex, casey));
                        }
                        place = true;
                    }
                }
            }
        }
        public void LancementPartie()
        {
            int cpt = 0;
            while (!FindePartie())
            {
                Console.Clear();
                AfficherPlateau();

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("Quelle case visez-vous : (format: ");
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.Write("ligne");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(",");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("colonne");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(")");
                Console.WriteLine();

                string val = Console.ReadLine();
                string[] position = val.Split(',', '.');

                if (position.Length >= 2
                    && int.TryParse(position[0], out int ligne)
                    && int.TryParse(position[1], out int colonne))
                {
                    ligne--; colonne--;
                    if (ligne >= 0 && ligne < 10 && colonne >= 0 && colonne < 10)
                    {
                        Viser(ligne, colonne);
                        cpt++;
                    }
                }
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            AfficherPlateau();
            Console.Write($"GG {cpt} coups effectués !");
        }

        /// <summary>
        /// Peut-on placer le navire sur la grille sans qu'il dépasse les bords et qu'il ne touche les autres bateaux ? 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <param name="taille"></param>
        /// <param name="estVertical"></param>
        /// <returns></returns>
        private bool PlacerBateau(int x, int y, int taille, bool estVertical)
        {
            List<Position> occupees = new List<Position>();
            foreach (Bateau b in Bateaux)
                occupees.AddRange(b.Positions);

            for (int i = 0; i < taille; i++)
            {
                int casex = estVertical ? x + i : x;
                int casey = estVertical ? y : y + i;

                if (casex < 0 || casex >= 10 || casey < 0 || casey >= 10)
                    return false;

                for (int contactx = -1; contactx <= 1; contactx++)
                    for (int contacty = -1; contacty <= 1; contacty++)
                        if (occupees.Any(p => p.X == casex + contactx && p.Y == casey + contacty))
                            return false;
            }
            return true;
        }

        /// <summary>
        /// Choix de la case (x , y) 
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Viser(int x, int y)
        {
            bool touche = false;
            foreach (Bateau bateau in Bateaux)
            {
                if (bateau.Positions.Any(position => position.X == x && position.Y == y))
                {
                    bateau.Touché(x, y);
                    touche = true;
                    break;
                }
            }
            if (!touche)
                PlateauJeu[x, y].Plouf();
        }

        /// <summary>
        /// Affichage de l'état de la grille et de la situation de la partie
        /// </summary>
        public void AfficherPlateau()
        {
            List<Position> list = new List<Position>();
            foreach (Bateau b in Bateaux)
            {
                list.AddRange(b.Positions);
                Console.WriteLine($"{b.Nom}: {b.Taille} de long, coulé: {b.EstCoulé()}");
            }

            foreach (Position p in list)
            {
                PlateauJeu.SetValue(p, p.X, p.Y);
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("   1 2 3 4 5 6 7 8 9 10");
            int cpt = 0, tmp = 0;
            foreach (Position p in PlateauJeu)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                if (p.X != tmp || cpt == 0)
                {
                    if (cpt > 0)
                    {
                        Console.WriteLine();
                    }
                    Console.Write(string.Format("{0,-3}", ++cpt));
                }

                ConsoleColor foreground;
                switch (p.Statut)
                {
                    case Position.Etat.Plouf:
                        foreground = ConsoleColor.Blue;
                        break;
                    case Position.Etat.Touché:
                        foreground = ConsoleColor.Red;
                        break;
                    case Position.Etat.Coulé:
                        foreground = ConsoleColor.Green;
                        break;
                    default:
                        foreground = ConsoleColor.White;
                        break;
                }
                Console.ForegroundColor = foreground;
                Console.Write((char)p.Statut + " ");

                tmp = p.X;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
        }

        /// <summary>
        /// La partie est-elle finie ? 
        /// </summary>
        /// <returns></returns>
        internal bool FindePartie()
        {
            return Bateaux.All(b => b.EstCoulé());
        }
    }
}