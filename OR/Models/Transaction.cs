using System;
using System.Xml.Serialization;

namespace Or.Models
{
    [XmlRoot]
    public class Transaction
    {
        [XmlAttribute] public int IdTransaction { get; set; }
        [XmlAttribute] public DateTime Horodatage { get; set; }
        [XmlAttribute] public decimal Montant { get; set; }
        [XmlAttribute] public int Expediteur { get; set; }
        [XmlAttribute] public int Destinataire { get; set; }
        [XmlAttribute] public string TypeOperation { get; set; }

        public Transaction(int idTransaction, DateTime horodatage, decimal montant, int expediteur, int destinataire)
        {
            IdTransaction = idTransaction;
            Horodatage = horodatage;
            Montant = montant;
            Expediteur = expediteur;
            Destinataire = destinataire;
            if(Expediteur != 0 && Destinataire != 0) TypeOperation = "Virement";
            else if (Expediteur == 0) TypeOperation = "Depôt";
            else if (Destinataire == 0) TypeOperation = "Retrait";
        }

    }
}
