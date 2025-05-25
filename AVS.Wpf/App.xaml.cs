using AVS.DBLib.Class;
using AVS.Wpf.Windows;
using System.Windows;

namespace AVS.Wpf
{
    public partial class App : Application
    {
        public User? Auth { get; set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var loginWindow = new WindowsLogin();
            this.MainWindow = loginWindow;
            loginWindow.Show();
        }

        public void Login(User user)
        {
            Auth = user;
            var mainWindow = new MainWindow();
            this.MainWindow.Close();
            this.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public void Logout()
        {
            Auth = null;
            var loginWindow = new WindowsLogin();
            this.MainWindow.Close();
            this.MainWindow = loginWindow;
            loginWindow.Show();
        }
    }
}
