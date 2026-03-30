using System;

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
        public void AfficherCompte() { Console.WriteLine($"ID : {_ID}\tCarte : {_numeroCarte}\tType : {_type}\tSolde : {_solde}\t"); }
    }
}