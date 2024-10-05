using System;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using RegionSyd.View;

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
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Hent forbindelsesstrengen
            ConnectionString = Configuration.GetConnectionString("DefaultConnection");

            // Opretter og viser loginvinduet
            WindowLogin loginWindow = new WindowLogin();
            loginWindow.Show();
        }
    }
}