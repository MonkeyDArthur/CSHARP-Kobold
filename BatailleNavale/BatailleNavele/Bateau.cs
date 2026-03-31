using System.Collections.Generic;
using System.Linq;

namespace Bataille_Navale
{
    internal class Bateau
    {
        public string Nom { get; private set; }
        public int Taille { get; private set; }
        public List<Position> Positions { get; private set; }

        public Bateau(string nom, int taille, List<Position> position)
        {
            Nom = nom;
            Taille = taille;
            Positions = position;
        }

        /// <summary>
        /// Case à l'état touché si elle appartient au bateau
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public void Touché(int x, int y)
        {
            Position pos = Positions.FirstOrDefault(p => p.X == x && p.Y == y);
            if (pos != null)
            {
                pos.Touché();
                if (EstCoulé())
                {
                    foreach (Position p in Positions)
                        p.Coulé();
                }
            }
        }

        /// <summary>
        /// Le bateau est-il coulé ? 
        /// </summary>
        public bool EstCoulé()
        {
            return Positions.Count > 0
                && Positions.All(p => p.Statut == Position.Etat.Touché
                                   || p.Statut == Position.Etat.Coulé);
        }

        /// <summary>
        /// Renvoie la position si celle-ci appartient au Bateau
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public Position Cible(int x, int y) => Positions.Find(p => p.X == x && p.Y == y);

    }
}