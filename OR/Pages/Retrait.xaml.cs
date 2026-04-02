using Or.Business;
using Or.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Navigation;

namespace Or.Pages
{
    public partial class Retrait : PageFunction<long>
    {
        Carte CartePorteur { get; set; }
        Compte ComptePorteur { get; set; }
        public Retrait(long numCarte)
        {
            // INIT PAGE
            InitializeComponent();

            // INIT VARIABLE
            Montant.Text = 0M.ToString("C2");
                // RECUP ET INIT INFO CARTE
            CartePorteur = SqlRequests.InfosCarte(numCarte);
            PlafondMaxRetrait.Text = CartePorteur.Plafond.ToString("C2");
                // RECUP ET INIT SOLDE CARTE
            ComptePorteur = SqlRequests.ListeComptesAssociesCarte(CartePorteur.Id).Find(x => x.TypeDuCompte == TypeCompte.Courant);
            Solde.Text = ComptePorteur.Solde.ToString("C2");
                // RECUP HISTORIQUE CARTE POUR CALCULER PLAFOND MAX
            List<Transaction> transac = SqlRequests.ListeTransactionsAssociesCarte(numCarte);
            List<int> cpts = SqlRequests.ListeComptesAssociesCarte(numCarte).Select(x => x.Id).ToList();
            CartePorteur.AlimenterHistoriqueEtListeComptes(transac, cpts);
                // INIT PLAFOND AUTORISE SELON HISTORIQUE
            PlafondActualise.Text = CartePorteur.SoldeCarteActuel(transac[transac.Count() - 1].Horodatage, numCarte).ToString("C2");
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }

        private void ValiderRetrait_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(Montant.Text.Replace(".", ",").Trim(new char[] { '€', ' ' }), out decimal montant) && montant > 0)
            {
                //Compte fictif pour permettre la transaction
                Compte compteBanque = new Compte(0, 0, TypeCompte.Courant, 0);
                Transaction t = new Transaction(0, DateTime.Now, montant, ComptePorteur.Id, compteBanque.Id);

                if (CartePorteur.EstRetraitAutoriseNiveauCarte(t, compteBanque, ComptePorteur) && ComptePorteur.EstRetraitValide(t))
                {
                    SqlRequests.EffectuerModificationOperationSimple(t, CartePorteur.Id);

                    OnReturn(null);
                }
                else
                {
                    // VERIF CONDITITON POUR MESSAGE ERREUR PRECIS
                    if (CartePorteur.Plafond < t.Montant && ComptePorteur.Solde < t.Montant)
                        MessageBox.Show($"{CodeErreur.PlafondMaxDepasse} et {CodeErreur.SoldeInsuffisant}");
                    else if (CartePorteur.Plafond < t.Montant)
                        MessageBox.Show($"{CodeErreur.PlafondMaxDepasse}");
                    else MessageBox.Show($"{CodeErreur.SoldeInsuffisant}");
                }
            }
            else
            {
                MessageBox.Show($"{CodeErreur.MontantInvalide}");
            }
        }
    }
}
