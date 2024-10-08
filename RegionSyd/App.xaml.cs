using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Windows;

namespace RegionSyd
{
    public partial class App : Application
    {
        public static string ConnectionString { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Build configuration from appsettings.json
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            var configuration = builder.Build();
            ConnectionString = configuration.GetConnectionString("DefaultConnection");
        }
    }
}