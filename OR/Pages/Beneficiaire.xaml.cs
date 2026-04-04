using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Or.Business;
using Or.Models;

namespace Or.Pages
{
    public partial class Beneficiaires : PageFunction<long>
    {
        public Beneficiaires(long numCarte)
        {
            InitializeComponent();

            Carte c = SqlRequests.InfosCarte(numCarte);
            Numero.Text = c.Id.ToString();
            Nom.Text = c.NomClient;
            Prenom.Text = c.PrenomClient;

            RafraichirListe();
        }
        private long NumCarte => long.Parse(Numero.Text);

        private void RafraichirListe()
        {
            listView.ItemsSource = SqlRequests.ListeBeneficiairesAssocieClient(NumCarte);
        }
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }
        private void Supprimer_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button)?.CommandParameter is int idCompte)
            {
                SqlRequests.SuppressionBeneficiaire(NumCarte, idCompte);
                RafraichirListe();
            }
        }
        private void AjouterBeneficiaire_Click(object sender, RoutedEventArgs e)
        {
            AjouterBeneficiaire page = new AjouterBeneficiaire(NumCarte);
            page.Return += PageFunction_Return;
            NavigationService.Navigate(page);
        }
        private void PageFunction_Return(object sender, ReturnEventArgs<long> e)
        {
            RafraichirListe();
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = listView.View as GridView;
            if (gridView != null)
            {
                double totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
                gridView.Columns[0].Width = totalWidth * 0.28;
                gridView.Columns[1].Width = totalWidth * 0.28;
                gridView.Columns[2].Width = totalWidth * 0.22;
                gridView.Columns[3].Width = totalWidth * 0.22;
            }
        }
    }
}