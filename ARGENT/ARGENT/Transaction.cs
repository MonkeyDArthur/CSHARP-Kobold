using System;

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
        public uint ID { get => _ID; set => _ID = value; }
        public DateTime Horodatage { get => _horodatage; set => _horodatage = value; }
        public decimal Montant { get => _montant; set => _montant = value; }
        public uint Expediteur { get => _expediteur; set => _expediteur = value; }
        public uint Destinataire { get => _destinataire; set => _destinataire = value; }
        public void AfficherTransaction() { Console.WriteLine($"ID : {_ID}\tHorodatage : {_montant}\tMontant : {_montant}\tExpediteur : {_expediteur}\tDestinataire : {_destinataire}"); }
    }
}
