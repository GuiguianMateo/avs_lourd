using AVS.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AVS.Wpf.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowsCreateType.xaml
    /// </summary>
    public partial class WindowsCreateType : Window 
    {

        public WindowsCreateType(ViewModelType context)
        {
            InitializeComponent();
            context.ResetNewType(); // Réinitialiser les données du formulaire à l'ouverture
            this.DataContext = context;
        }

        private void Create_Type_Click(object sender, RoutedEventArgs e)
        {
            ((ViewModelType)this.DataContext).CreateType();
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

