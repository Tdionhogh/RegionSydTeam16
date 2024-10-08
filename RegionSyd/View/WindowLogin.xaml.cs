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


namespace RegionSyd.View
{
    public partial class WindowLogin : Window
    {
        private readonly LoginService _loginService;

        public WindowLogin()
        {
            InitializeComponent();
            _loginService = new LoginService(); // Initialize the login service
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Input validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                LoginResultTextBlock.Text = "Please enter both username and password.";
                return;
            }

            // Check if the user exists and the password is correct
            bool isAuthenticated = _loginService.Authenticate(username, password);

            if (isAuthenticated)
            {
                LoginResultTextBlock.Text = "Login successful!";
                this.Hide();

                // Show MainWindow and close this window
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();

                // Optionally, you can close the login window or set it to be hidden
                // this.Close(); // Uncomment if you want to close instead of hiding
            }
            else
            {
                LoginResultTextBlock.Text = "Login failed! Please check your username and password.";
            }
        }
    }
}
