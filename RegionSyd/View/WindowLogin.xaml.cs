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
using RegionSydTeam16.Model;

namespace RegionSydTeam16
{


    public partial class WindowLogin : Window
    {
        private LoginService _loginService;

        public WindowLogin()
        {
            InitializeComponent();
            _loginService = new LoginService(); // Opretter instans af LoginService
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text;
            string password = PasswordBox.Password;

            // Tjekker om brugeren findes og adgangskoden er korrekt
            bool isAuthenticated = _loginService.Authenticate(username, password);

            if (isAuthenticated)
            {
                LoginResultTextBlock.Text = "Login successful!";
                // Du kan åbne dit hovedvindue her, hvis login er succesfuldt
            }
            else
            {
                LoginResultTextBlock.Text = "Login failed!";
            }
        }
    }
}
