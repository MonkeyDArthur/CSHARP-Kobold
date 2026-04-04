using System;
using System.Xml.Serialization;
using System.Collections.Generic;

namespace Or.Models
{
    [XmlRoot("Comptes")]
    public class ExportComptes
    {
        [XmlElement("Compte")] public List<ExportCompteTransactions> Comptes { get; set; }
        public ExportComptes() { Comptes = new List<ExportCompteTransactions>(); }
    }
}