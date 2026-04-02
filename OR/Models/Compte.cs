using System.Xml.Serialization;

namespace Or.Models
{
    [XmlRoot]
    public enum TypeCompte { Courant, Livret }

    public class Compte
    {
        [XmlAttribute] public int Id { get; set; }
        [XmlIgnore] public long IdentifiantCarte { get; set; }
        [XmlAttribute] public TypeCompte TypeDuCompte { get; set; }
        [XmlAttribute] public decimal Solde { get; private set; }

        public Compte(int id, long identifiantCarte, TypeCompte type, decimal soldeInitial)
        {
            Id = id;
            IdentifiantCarte = identifiantCarte;
            TypeDuCompte = type;
            Solde = soldeInitial;
        }

        /// <summary>
        /// Action de dépôt d'argent sur le compte bancaire
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Statut du dépôt</returns>
        public bool EstDepotValide(Transaction transaction)
        {
            if (transaction.Montant > 0) return true;
            else return false;
        }

        /// <summary>
        /// Action de retrait d'argent sur le compte bancaire
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>Statut du retrait</returns>
        public bool EstRetraitValide(Transaction transaction)
        {
            if (EstRetraitAutorise(transaction.Montant)) return true;
            else return false; 
        }

        private bool EstRetraitAutorise(decimal montant)
        {
            return Solde >= montant && montant > 0;
        } 
    }
}
