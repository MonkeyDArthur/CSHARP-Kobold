using System.Xml.Serialization;
using System.Collections.Generic;

namespace Or.Models
{
    [XmlRoot]
    class ExportCompte
    {
        [XmlAttribute] public Compte Compte { get; set; }
        [XmlAttribute] public List<Transaction> listeTransaction { get; set; }
        public ExportCompte(Compte compte, List<Transaction> listetransaction)
        {
            Compte = compte;
            listeTransaction = listetransaction;
        }
    }
}
