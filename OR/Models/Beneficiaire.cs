using System;
using System.Collections.Generic;

using Or.Business;

namespace Or.Models
{
    public class Beneficiaire
    {
        public int NumCarte { get; set; }
        public int IdBneficiaire { get; set; }

        public Beneficiaire(int compte, int beneficiaire)
        {
            NumCarte = compte;
            IdBneficiaire = beneficiaire;
        }
        public List<long> ListeBeneficiaireAssocieClient(long numCarte)
        {
            return SqlRequests.ListeBeneficiaireAssicieCarte(numCarte);
        }
        public void AjoutBeneficiaire(long numCarte, int idtCpt)
        {
            SqlRequests.AjouterBeneficiaire(NumCarte, IdBneficiaire);
        }
        public void SuppressionBeneficiaire(long numCarte, int idtCpt)
        {
            SqlRequests.SupprimerBeneficiaire(NumCarte, IdBneficiaire);
        }
        public void EstBeneficiairePotenciel(int idtCpt)
        {
            //verif si peut etre beneficiaire
        }
    }
}
