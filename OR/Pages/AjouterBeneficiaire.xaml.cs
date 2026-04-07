using System.Windows;
using System.Windows.Navigation;
using Or.Business;

namespace Or.Pages
{
    public partial class AjouterBeneficiaire : PageFunction<long>
    {
        private readonly long _numCarte;
        public AjouterBeneficiaire(long numCarte)
        {
            InitializeComponent();
            _numCarte = numCarte;
        }
        private void Retour_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(null);
        }
        private void Valider_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(NumeroCpt.Text.Trim(), out int idCompte))
            {
                MessageBox.Show("Saisie bénéficiaire invalide\nVeuillez entrez un N° de compte valide",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (!SqlRequests.EstBeneficiairePotentiel(idCompte))
            {
                MessageBox.Show("Saisie bénéficiaire invalide\nVeuillez entrez un N° de compte existant",
                    "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            SqlRequests.AjoutBeneficiaire(_numCarte, idCompte);
            OnReturn(null);
        }
    }
}