using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Or.Models
{
    public class ExportCompteTransactions
    {
        [XmlElement("Identifiant")] public int Identifiant { get; set; }
        [XmlElement("Type")] public string Type { get; set; }
        [XmlElement("Solde")] public string Solde { get; set; }
        [XmlArray("Transactions")] [XmlArrayItem("Transaction")] public List<ExportTransaction> Transactions { get; set; }

        public ExportCompteTransactions() { }
        public ExportCompteTransactions(Compte compte, List<Transaction> transactions)
        {
            Identifiant = compte.Id;
            Type = compte.TypeDuCompte.ToString();
            Solde = compte.Solde.ToString("C2");
            Transactions = transactions.OrderByDescending(t => t.Horodatage).Take(10).Select(t => new ExportTransaction(t)).ToList();
        }
    }
}