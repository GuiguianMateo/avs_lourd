using AVS.DBLib.Class;
using System.Windows;

namespace AVS.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Logout_Click(object sender, RoutedEventArgs e)
        {
            ((App)System.Windows.Application.Current).Logout();
        }
    }
}