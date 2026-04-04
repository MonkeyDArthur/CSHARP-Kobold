using System;
using System.Xml.Serialization;

namespace Or.Models
{
    public class ExportTransaction
    {
        [XmlElement("Identifiant")] public int Identifiant { get; set; }
        [XmlElement("Date")] public string Date { get; set; }
        [XmlElement("Montant")] public string Montant { get; set; }
        [XmlElement("CompteExpediteur")] public string CompteExpediteur { get; set; }
        [XmlElement("CompteDestinataire")] public string CompteDestinataire { get; set; }
        [XmlElement("Operation")] public string Operation { get; set; }

        public ExportTransaction() { }
        public ExportTransaction(Transaction t)
        {
            Identifiant = t.IdTransaction;
            Date = t.Horodatage.ToString("dd/MM/yyyy HH:mm:ss");
            Montant = Math.Abs(t.Montant).ToString("C2");
            Operation = t.TypeOperation;
            // CompteExpediteur : uniquement Retrait ou Virement
            if (t.TypeOperation == "Retrait" || t.TypeOperation == "Virement") CompteExpediteur = t.Expediteur.ToString();
            // CompteDestinataire : uniquement Dépôt ou Virement
            if (t.TypeOperation == "Depôt" || t.TypeOperation == "Virement") CompteDestinataire = t.Destinataire.ToString();
        }
        // Méthodes ShouldSerialize : permet d'omettre les balises nulles dans le XML
        public bool ShouldSerializeCompteExpediteur() => CompteExpediteur != null;
        public bool ShouldSerializeCompteDestinataire() => CompteDestinataire != null;
    }
}