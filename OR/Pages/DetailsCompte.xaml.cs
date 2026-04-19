using Or.Business;
using Or.Models;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Or.Pages
{
    public partial class DetailsCompte : PageFunction<Int64>
    {
        public DetailsCompte(long numCarte, int compte)
        {
            // INIT PAGE
            InitializeComponent();

            // INIT COMPTE CONCERNE
            Compte c = SqlRequests.ListeComptesAssociesCarte(numCarte).Find(x => x.Id == compte);
            IdCompte.Text = c.Id.ToString();
            TypeCompte.Text = c.TypeDuCompte.ToString();
            Solde.Text = c.Solde.ToString("C2");

            // INIT LISTE TRANSACTION DU COMPTE
            List<Transaction> transactions = SqlRequests.ListeTransactionsAssociesCompte(compte);
                // - SI DEBIT, + SI CREDIT
            transactions.ForEach(x => x.Montant = x.Expediteur == c.Id ? -x.Montant : x.Montant);
                // INIT TYPE OPERATION
            foreach (var transaction in transactions)
            {
                if (transaction.TypeOperation == "Virement")
                {
                    // Pas nécessaire
                    //transaction.TypeOperation = transaction.Montant < 0 ? "Virement\nsortant" : "Virement\nentrant";
                }
            }
                // INSERTION DES DONNEES DANS LE TABLEAU D'AFFICHAGE
            listView.ItemsSource = transactions;
        }

        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }

        private void ListView_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            GridView gridView = listView.View as GridView;
            if (gridView != null)
            {
                double totalWidth = listView.ActualWidth - SystemParameters.VerticalScrollBarWidth;
                gridView.Columns[0].Width = totalWidth * 0.10;
                gridView.Columns[1].Width = totalWidth * 0.20;
                gridView.Columns[2].Width = totalWidth * 0.45;
                gridView.Columns[3].Width = totalWidth * 0.25;
            }
        }
    }
}
