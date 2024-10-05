using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using RegionSyd.Model;
using BCrypt.Net;



namespace RegionSyd.View // Sørg for, at dette matcher den faktiske placering af filen
{
    public partial class WindowLogin : Window
    {
        private LoginService _loginService;

        public WindowLogin()
        {
            InitializeComponent(); // Sørg for at dette kaldes korrekt
            _loginService = new LoginService(); // Opretter instans af LoginService
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Input validering
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                LoginResultTextBlock.Text = "Please enter both username and password.";
                return;
            }

            // Tjekker om brugeren findes og adgangskoden er korrekt
            bool isAuthenticated = _loginService.Authenticate(username, password);

            if (isAuthenticated)
            {
                LoginResultTextBlock.Text = "Login successful!";
                this.Hide();
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
            }
            else
            {
                LoginResultTextBlock.Text = "Login failed!";
            }
        }
    }
}
