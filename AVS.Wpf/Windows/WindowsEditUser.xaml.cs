using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVS.Wpf.ViewModels;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;

namespace AVS.Wpf.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowsEditUser.xaml
    /// </summary>
    public partial class WindowsEditUser : Window
    {
        public int MyProperty { get; set; }
        public WindowsEditUser(ViewModelUser context)
        {
            InitializeComponent();
            context.SaveOriginalSelectedUser();
            this.DataContext = context;

            // Initialiser les RadioButtons selon la valeur actuelle
            UpdateGenreRadioButtons();
        }

        private void Edit_User_Click(object sender, RoutedEventArgs e)
        {
            if (((ViewModelUser)this.DataContext).SelectedUser != null)
            {
                ((ViewModelUser)this.DataContext).EditUser();
                this.Close();
            }
        }

        private void Annuler_Edit_Click(object sender, RoutedEventArgs e)
        {
            var test = sender;

            ((ViewModelUser)this.DataContext).RestoreOriginalSelectedUser();
            this.Close();
        }

        private void NumericTextBox(object sender, TextCompositionEventArgs e)
        {
            foreach (var c in e.Text)
            {
                if (!char.IsDigit(c))
                {
                    e.Handled = true;
                    return;
                }
            }
        }

        private void Genre_Changed(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton rb && this.DataContext is ViewModelUser vm)
            {
                vm.SelectedUser.Genre = rb.Tag.ToString();
            }
        }

        private void UpdateGenreRadioButtons()
        {
            if (this.DataContext is ViewModelUser vm && vm.SelectedUser != null)
            {
                RadioMasculin.IsChecked = vm.SelectedUser.Genre == "Masculin";
                RadioFeminin.IsChecked = vm.SelectedUser.Genre == "Feminin";
                RadioAutre.IsChecked = vm.SelectedUser.Genre == "Autre";
            }
        }
    }
}
