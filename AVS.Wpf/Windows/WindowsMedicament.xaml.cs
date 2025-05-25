using System.Windows;
using System.Windows.Controls;
using AVS.DBLib.Class;
using AVS.Wpf.ViewModels;

namespace AVS.Wpf.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowMedicament.xaml
    /// </summary>
    public partial class WindowsMedicament : UserControl
    {
        public WindowsMedicament()
        {
            InitializeComponent();
            DataContext = new ViewModelMedicament();
        }

        private void Create_Medicament_Click(object sender, RoutedEventArgs e)
        {
            // Initialiser la vue FormCreateProduitView en transmettant le ViewModelProduit
            WindowsCreateMedicament formCreateProduitView = new WindowsCreateMedicament((ViewModelMedicament)this.DataContext);

            // Afficher la vue
            formCreateProduitView.ShowDialog();
        }

        private void Edit_Medicament_Click(object sender, RoutedEventArgs e)
        {
            WindowsEditMedicament formModif = new WindowsEditMedicament(((ViewModelMedicament)this.DataContext));
            formModif.ShowDialog();
        }

        private void Delete_Medicament_Click(object sender, RoutedEventArgs e)
        {
            if (((ViewModelMedicament)this.DataContext).SelectedMedicament != null)
            {
                using (AvsContext context = new AvsContext())
                {

                    if (context.Prescriptions.Any(c => c.MedicamentId == ((ViewModelMedicament)this.DataContext).SelectedMedicament.Id))
                    {
                        MessageBox.Show("Impossible de supprimer ce médicament car il est lié à une ou plusieurs Prescriptions", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        ((ViewModelMedicament)this.DataContext).DeleteMedicament();
                    }
                }
            }
        }
    }
}
