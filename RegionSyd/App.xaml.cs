using System;
using System.Windows;
using Microsoft.Extensions.Configuration; // Til indlæsning af konfigurationsindstillinger fra appsettings.json
using Microsoft.Data.SqlClient; // Til at arbejde med SQL Server via ADO.NET

namespace RegionSyd
{
    public partial class App : Application
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static string? ConnectionString { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Indlæs konfigurationen fra appsettings.json
            Configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory) // Angiv stien til konfigurationsfilen
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Hent forbindelsesstrengen
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            // Her kan du bruge forbindelsesstrengen, f.eks. oprette en SqlConnection
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                try
                {
                    connection.Open();
                    // Arbejd med databasen her...
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Fejl ved oprettelse af forbindelse: {ex.Message}");
                }
            }

            // Opretter og viser hovedvinduet
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}