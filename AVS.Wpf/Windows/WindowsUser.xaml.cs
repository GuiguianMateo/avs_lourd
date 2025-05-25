using System.Windows;
using System.Windows.Controls;
using AVS.DBLib.Class;
using AVS.Wpf.ViewModels;

namespace AVS.Wpf.Windows
{
    /// <summary>
    /// Logique d'interaction pour WindowUser.xaml
    /// </summary>
    public partial class WindowsUser : UserControl
    {
        public WindowsUser()
        {
            InitializeComponent();
            DataContext = new ViewModelUser();
        }

        private void Edit_User_Click(object sender, RoutedEventArgs e)
        {
            WindowsEditUser formModif = new WindowsEditUser(((ViewModelUser)this.DataContext));
            formModif.ShowDialog();
        }

        private void Delete_User_Click(object sender, RoutedEventArgs e)
        {
            if (((ViewModelUser)this.DataContext).SelectedUser != null)
            {
                using (AvsContext context = new AvsContext())
                {

                    if (context.Consultations.Any(c => c.UserId == ((ViewModelUser)this.DataContext).SelectedUser.Id))
                    {
                        MessageBox.Show("Impossible de supprimer cet utilisateur car il est lié à une ou plusieurs consultations", "Avertissement", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        ((ViewModelUser)this.DataContext).DeleteUser();
                    }
                }
            }
        }
    }
}
