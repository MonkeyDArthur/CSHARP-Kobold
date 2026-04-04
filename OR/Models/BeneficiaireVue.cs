namespace Or.Models
{
    public class BeneficiaireVue
    {
        public int IdCompte { get; set; }
        public string NomClient { get; set; }
        public string PrenomClient { get; set; }

        public BeneficiaireVue(int idCompte, string nomClient, string prenomClient)
        {
            IdCompte = idCompte;
            NomClient = nomClient;
            PrenomClient = prenomClient;
        }
    }
}
