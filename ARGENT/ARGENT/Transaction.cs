using System;
using System.Collections.Generic;

namespace TRANSACTION
{
    public class Transaction
    {
        private uint _ID;
        private DateTime _horodatage;
        private decimal _montant;
        private uint _expediteur;
        private uint _destinataire;

        public Transaction(uint ID, DateTime horodatage, decimal montant, uint expediteur, uint destinataire)
        {
            _ID = ID;
            _horodatage = horodatage;
            _montant = montant;
            _expediteur = expediteur;
            _destinataire = destinataire;
        }
        // Ici pas de soucis, mais les propriétés auto-implémentées auraient été suffisantes
        public uint ID { get => _ID; set => _ID = value; }
        public DateTime Horodatage { get => _horodatage; set => _horodatage = value; }
        public decimal Montant { get => _montant; set => _montant = value; }
        public uint Expediteur { get => _expediteur; set => _expediteur = value; }
        public uint Destinataire { get => _destinataire; set => _destinataire = value; }

        // BUG CORRIGE : _horodatage était affiché comme _montant
        public void AfficherTransaction()
        {
            Console.WriteLine($"| {_ID}\t| {_horodatage}\t| ${_montant.ToString("0.00").PadLeft(20, ' ')}\t| {_expediteur}\t| {_destinataire}");
        }
        public static void AfficherListeTransaction(List<Transaction> listTransaction)
        {
            Console.WriteLine($"| ID\t| HORODATAGE\t\t| MONTANT\t\t| EXPE\t| DEST");
            foreach (var elem in listTransaction) { elem.AfficherTransaction(); }
        }
    }
}