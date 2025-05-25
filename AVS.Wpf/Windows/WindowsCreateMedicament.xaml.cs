using AVS.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AVS.Wpf.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowsCreateMedicament.xaml
    /// </summary>
    public partial class WindowsCreateMedicament : Window 
    {

        public WindowsCreateMedicament(ViewModelMedicament context)
        {
            InitializeComponent();
            context.ResetNewMedicament(); // Réinitialiser les données du formulaire à l'ouverture
            this.DataContext = context;
        }

        private void Create_Medicament_Click(object sender, RoutedEventArgs e)
        {
            ((ViewModelMedicament)this.DataContext).CreateMedicament();
            this.Close();
        }

        private void Annuler_Create_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void NumericTextBox(object sender, TextCompositionEventArgs e)
        {
            foreach (var c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true; // Ignore tous les caractères qui ne sont pas des chiffres
                    return;
                }
            }
        }
    }
}

