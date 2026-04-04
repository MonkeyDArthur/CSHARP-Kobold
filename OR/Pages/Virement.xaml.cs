using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Navigation;
using Or.Business;
using Or.Models;

namespace Or.Pages
{
    public partial class Virement : PageFunction<long>
    {
        Carte CartePorteur { get; set; }
        Compte ComptePorteur { get; set; }
        public Virement(long numCarte)
        {
            InitializeComponent();

            Montant.Text = 0M.ToString("C2");

            CartePorteur = SqlRequests.InfosCarte(numCarte);
            CartePorteur.AlimenterHistoriqueEtListeComptes(SqlRequests.ListeTransactionsAssociesCarte(numCarte),SqlRequests.ListeComptesAssociesCarte(CartePorteur.Id).Select(x => x.Id).ToList());

            ComptePorteur = SqlRequests.ListeComptesAssociesCarte(CartePorteur.Id).Find(x => x.TypeDuCompte == TypeCompte.Courant);

            var viewExpediteur = CollectionViewSource.GetDefaultView(SqlRequests.ListeComptesAssociesCarte(numCarte));
            viewExpediteur.GroupDescriptions.Add(new PropertyGroupDescription("TypeDuCompte"));
            viewExpediteur.SortDescriptions.Add(new SortDescription("TypeDuCompte", ListSortDirection.Ascending));
            viewExpediteur.SortDescriptions.Add(new SortDescription("IdentifiantCarte", ListSortDirection.Ascending));
            Expediteur.ItemsSource = viewExpediteur;
        }
        private void MettreAJourDestinataires(int idCompteExpediteur)
        {
            var comptes = SqlRequests.ListeComptesDispoAvecBeneficiaires(idCompteExpediteur, CartePorteur.Id);
            var view = CollectionViewSource.GetDefaultView(comptes);
            view.GroupDescriptions.Add(new PropertyGroupDescription("IdentifiantCarte"));
            view.SortDescriptions.Add(new SortDescription("IdentifiantCarte", ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription("TypeDuCompte", ListSortDirection.Ascending));
            Destinataire.ItemsSource = view;
        }

        private void Expediteur_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Expediteur.SelectedItem is Compte ex)
                MettreAJourDestinataires(ex.Id);
        }
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }
        private void AjouterBeneficiaire_Click(object sender, RoutedEventArgs e)
        {
            AjouterBeneficiaire page = new AjouterBeneficiaire(CartePorteur.Id);
            page.Return += PageFunction_Return;
            NavigationService.Navigate(page);
        }
        private void PageFunction_Return(object sender, ReturnEventArgs<long> e)
        {
            if (Expediteur.SelectedItem is Compte ex)
                MettreAJourDestinataires(ex.Id);
        }
        private void ValiderVirement_Click(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(
                    Montant.Text.Replace(".", ",").Trim(new char[] { '€', ' ' }),
                    out decimal montant))
            {
                Compte ex = Expediteur.SelectedItem as Compte;
                Compte de = Destinataire.SelectedItem as Compte;

                if (ex == null || de == null)
                {
                    MessageBox.Show($"{CodeErreur.MontantInvalide}");
                    return;
                }

                Transaction t = new Transaction(0, DateTime.Now, montant, ex.Id, de.Id);

                if (ex.EstRetraitValide(t) && CartePorteur.EstRetraitAutoriseNiveauCarte(t, ex, de))
                {
                    SqlRequests.EffectuerModificationOperationInterCompte(
                        t, ex.IdentifiantCarte, de.IdentifiantCarte);
                    OnReturn(null);
                }
                else
                {
                    if (ex.Solde < t.Montant && CartePorteur.Plafond < t.Montant)
                        MessageBox.Show($"{CodeErreur.PlafondMaxDepasse} et {CodeErreur.SoldeInsuffisant} de l'expéditeur");
                    else if (ex.Solde < t.Montant)
                        MessageBox.Show($"{CodeErreur.SoldeInsuffisant} de l'expéditeur");
                    else
                        MessageBox.Show($"{CodeErreur.PlafondMaxDepasse} de l'expéditeur");
                }
            }
            else
            {
                MessageBox.Show($"{CodeErreur.MontantInvalide}");
            }
        }
    }
}