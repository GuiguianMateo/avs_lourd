using System.Windows;

namespace AVS.Wpf.Windows
{
    public partial class WindowsLogin : Window
    {
        public WindowsLogin()
        {
            InitializeComponent();
            this.DataContext = new ViewModelLogin();
        }

        private void Button_Login_Click(object sender, RoutedEventArgs e)
        {
            if (this.DataContext is ViewModelLogin vm)
            {
                vm.Password = PasswordBoxLogin.Password;
                vm.Login();
            }
        }
    }
}
