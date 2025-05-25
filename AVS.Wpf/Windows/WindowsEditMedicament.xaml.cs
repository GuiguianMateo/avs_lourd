using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVS.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace AVS.Wpf.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowsEditMedicament.xaml
    /// </summary>
    public partial class WindowsEditMedicament : Window
    {
        public int MyProperty { get; set; }
        public WindowsEditMedicament(ViewModelMedicament context)
        {
            InitializeComponent();
            context.SaveOriginalSelectedMedicament();  // Sauvegarder l'état initial
            this.DataContext = context;
        }

        private void Edit_Medicament_Click(object sender, RoutedEventArgs e)
        {
            if (((ViewModelMedicament)this.DataContext).SelectedMedicament != null)
            {
                ((ViewModelMedicament)this.DataContext).EditMedicament();
                this.Close();
            }
        }

        private void Annuler_Edit_Click(object sender, RoutedEventArgs e)
        {
            var test = sender;

            ((ViewModelMedicament)this.DataContext).RestoreOriginalSelectedMedicament();  // Restaurer l'état initial si annulé
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
