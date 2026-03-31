using CARTE;
using System;
using System.Collections.Generic;

namespace COMPTE
{
    public class Compte
    {
        private uint _ID;
        private string _numeroCarte;
        private string _type;
        private decimal _solde;

        public Compte(uint ID, string numeroCarte, string type)
        { 
            _ID = ID;
            _numeroCarte = numeroCarte;
            _type = type;
            _solde = 0;
        }
        public Compte(uint ID, string numeroCarte, string type, decimal solde)
        {
            _ID = ID;
            _numeroCarte = numeroCarte;
            _type = type;
            _solde = solde;
        }
        public uint ID { get => _ID; set => _ID = value; }
        public string Carte { get => _numeroCarte; set => _numeroCarte = value; }
        public string Type { get => _type; set => _type = value; }
        public decimal Solde { get => _solde; set => _solde = value; }
        public bool Deposer(decimal montant)
        {
            if (montant <= 0) return false;
            _solde += montant;
            return true;
        }
        public bool Retirer(decimal montant)
        {
            if (montant <= 0) return false;
            if (_solde < montant) return false;
            _solde -= montant;
            return true;
        }
        public void AfficherCompte() { Console.WriteLine($"| {_ID}\t| {_numeroCarte}\t| {_type}\t| ${_solde.ToString("0.00").PadLeft(23, ' ')}"); }
        public static void AfficherListeCompte(List<Compte> listCarte)
        {
            Console.WriteLine($"| ID\t| Carte\t\t\t| Type\t\t| Solde");
            foreach (var elem in listCarte) { elem.AfficherCompte(); }
        }
    }
}