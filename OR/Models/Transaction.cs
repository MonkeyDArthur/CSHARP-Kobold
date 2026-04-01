using System;

namespace Or.Models
{
    public class Transaction
    {
        public int IdTransaction { get; set; }
        public DateTime Horodatage { get; set; }
        public decimal Montant { get; set; }
        public int Expediteur { get; set; }
        public int Destinataire { get; set; }
        public string TypeOperation { get; set; }

        public Transaction(int idTransaction, DateTime horodatage, decimal montant, int expediteur, int destinataire)
        {
            IdTransaction = idTransaction;
            Horodatage = horodatage;
            Montant = montant;
            Expediteur = expediteur;
            Destinataire = destinataire;
            if (Expediteur != 0 && Destinataire != 0) TypeOperation = "Virement";
            else if (Expediteur == 0 && Destinataire != 0) TypeOperation = "Depôt";
            else if (Expediteur != 0 && Destinataire == 0) TypeOperation = "Retrait";
        }
    }
}
