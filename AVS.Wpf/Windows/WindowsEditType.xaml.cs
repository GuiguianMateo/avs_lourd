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
    /// Logique d'interaction pour WindowsEditType.xaml
    /// </summary>
    public partial class WindowsEditType : Window
    {
        public int MyProperty { get; set; }
        public WindowsEditType(ViewModelType context)
        {
            InitializeComponent();
            context.SaveOriginalSelectedType();  // Sauvegarder l'état initial
            this.DataContext = context;
        }

        private void Edit_Type_Click(object sender, RoutedEventArgs e)
        {
            if (((ViewModelType)this.DataContext).SelectedType != null)
            {
                ((ViewModelType)this.DataContext).EditType();
                this.Close();
            }
        }

        private void Annuler_Edit_Click(object sender, RoutedEventArgs e)
        {
            var test = sender;

            ((ViewModelType)this.DataContext).RestoreOriginalSelectedType();  // Restaurer l'état initial si annulé
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
