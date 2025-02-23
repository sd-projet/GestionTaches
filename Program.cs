using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Windows;
using GestionTache.Data;
using System.Configuration;
using GestionTache.ViewModels;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;


namespace GestionTache
{
    public partial class App : Application
    {
        public static IConfiguration Configuration { get; private set; }
        public static AppDbContext DbContext { get; private set; }


        /*public App()
        {
            try
            {
                // Charger appsettings.json
                var builder = new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json"), optional: false, reloadOnChange: true);

                Configuration = builder.Build();


                // Configurer EF Core
                ConfigureDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'initialisation de l'application : {ex.Message}", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                Environment.Exit(1);
            }
        }*/

        private void ConfigureDatabase()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));

            DbContext = new AppDbContext(optionsBuilder.Options);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            DbContext?.Dispose();
            base.OnExit(e);
        }
    }
}
