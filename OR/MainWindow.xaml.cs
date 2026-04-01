using System.Windows.Navigation;
using Or.Business;

namespace Or
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : NavigationWindow
    {
        public MainWindow()
        {
            //SqlRequests.createTable();
            SqlRequests.AjouterBeneficiaire(1234567890123456, 3);
            SqlRequests.AjouterBeneficiaire(1234567890123456, 4);
            SqlRequests.AjouterBeneficiaire(4567891012345678, 1);
            SqlRequests.AjouterBeneficiaire(4567891012345678, 2);
            SqlRequests.SupprimerBeneficiaire(1234567890123456, 3);
            //SqlRequests.dropTable();
            InitializeComponent();
        }
    }
}
