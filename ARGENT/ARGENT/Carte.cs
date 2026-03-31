using System;
using System.Collections.Generic;
using System.Linq;

namespace CARTE
{
    public class Carte
    {
        private string _numero;
        private uint _plafond;
        private List<uint> _comptes;
        // Intéressante utilisation des Tuples
        private List<(DateTime horodatage, decimal montant)> _historique;

        public Carte(string numero, uint plafond)
        {
            _numero = numero;
            _plafond = plafond;
            _comptes = new List<uint>();
            _historique = new List<(DateTime, decimal)>();
        }
        // Pourquoi définir un set pour les deux propriétés suivantes ? 
        public string Numero { get => _numero; } //set => _numero = value; }
        public uint Plafond { get => _plafond; } //set => _plafond = value; }
        public List<uint> Comptes { get => _comptes; set => _comptes = value; }
        public void AjouterCompte(uint idCompte)
        {
            if (!_comptes.Contains(idCompte)) _comptes.Add(idCompte);
        }
        public bool PossedeCompte(uint idCompte) => _comptes.Contains(idCompte);
        public bool VerifierPlafond(DateTime horodatage, decimal montant)
        {
            DateTime limite = horodatage.AddDays(-10);
            // Bonne utilisation du Linq 
            decimal totalDejaDebite = _historique
                .Where(t => t.horodatage >= limite && t.horodatage < horodatage)
                .Sum(t => t.montant);
            return (totalDejaDebite + montant) <= _plafond;
        }
        public void EnregistrerDebit(DateTime horodatage, decimal montant)
        {
            _historique.Add((horodatage, montant));
        }
        public void AfficherCarte() { Console.WriteLine($"| {_numero}\t| ${_plafond.ToString("0.00").PadLeft(20, ' ')}"); }
        public static void AfficherListeCarte(List<Carte> listCarte)
        {
            Console.WriteLine($"| NUMERO DE CARTE\t| PLAFOND");
            foreach (var elem in listCarte) { elem.AfficherCarte(); }
        }
    }
}
