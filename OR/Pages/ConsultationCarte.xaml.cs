using Or.Business;
using Or.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Xml.Serialization;

namespace Or.Pages
{
    /// <summary>
    /// Logique ht'interaction pour ConsultationCarte.xaml
    /// </summary>
    public partial class ConsultationCarte : PageFunction<long>
    {
        public ConsultationCarte(long numCarte)
        {
            InitializeComponent();
            Carte c = SqlRequests.InfosCarte(numCarte);
            
            Numero.Text = c.Id.ToString();
            Prenom.Text = c.PrenomClient;
            Nom.Text = c.NomClient;

            listView.ItemsSource = SqlRequests.ListeComptesAssociesCarte(numCarte);
        }
        private void GoDetailsCompte(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new DetailsCompte(long.Parse(Numero.Text), (int)(sender as Button).CommandParameter));
        }
        private void GoHistoTransactions(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new HistoriqueTransactions(long.Parse(Numero.Text)));
        }
        private void GoVirement(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Virement(long.Parse(Numero.Text)));
        }
        private void GoRetrait(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Retrait(long.Parse(Numero.Text)));
        }
        private void GoDepot(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Depot(long.Parse(Numero.Text)));
        }
        private void GoListeBeneficiaires(object sender, RoutedEventArgs e)
        {
            PageFunctionNavigate(new Beneficiaires(long.Parse(Numero.Text)));
        }
        void PageFunctionNavigate(PageFunction<long> page)
        {
            page.Return += new ReturnEventHandler<long>(PageFunction_Return);
            NavigationService.Navigate(page);
        }
        void PageFunction_Return(object sender, ReturnEventArgs<long> e)
        {
            listView.ItemsSource = SqlRequests.ListeComptesAssociesCarte(long.Parse(Numero.Text));
        }
        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = listView.View as GridView;
            if (gridView != null)
            {
                double totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
                gridView.Columns[0].Width = totalWidth * 0.10; // 10%
                gridView.Columns[1].Width = totalWidth * 0.30; // 40%
                gridView.Columns[2].Width = totalWidth * 0.30; // 20%
                gridView.Columns[3].Width = totalWidth * 0.30; // 20%
            }
        }

        private void SerialiserCompteTransaction(string nomFichier)
        {
            long numCarte = long.Parse(Numero.Text);
            List<Compte> comptes = SqlRequests.ListeComptesAssociesCarte(numCarte);
            ExportComptes exportComptes = new ExportComptes();
            foreach (Compte compte in comptes)
            {
                List<Transaction> transactions = SqlRequests.ListeTransactionsAssociesCompte(compte.Id);
                exportComptes.Comptes.Add(new ExportCompteTransactions(compte, transactions));
            }
            
            
            string cheminBureau = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string cheminComplet = Path.Combine(cheminBureau, nomFichier);
            XmlSerializer serializer = new XmlSerializer(typeof(ExportComptes));
            using (StreamWriter writer = new StreamWriter(cheminComplet))
            {
                serializer.Serialize(writer, exportComptes);
            }
        }
        private void ExporterTransactions_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string nomFichier = $"export_carte_{Numero.Text}_{DateTime.Now:yyyyMMdd_HHmmss}.xml";
                SerialiserCompteTransaction(nomFichier);

                string chemin = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop),nomFichier);

                MessageBox.Show($"Export réussi :\n{chemin}", "Export XML", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'export : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private List<Transaction> DeSerialiserTransactions(string nomFichier)
        {
            List<Transaction> transactions = new List<Transaction>();
            return transactions;
        }
        private void TraitementTransactionsImportees(List<Transaction> transactions)
        {
            
        }
        private void ImporterTransactions_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
