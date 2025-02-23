using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;

namespace GestionTache.ViewModels
{
    public class LoginViewModel
    {
        private readonly HttpClient _httpClient = new HttpClient { BaseAddress = new System.Uri("http://localhost:3306/api/") };

        public string Username { get; set; }
        public string Password { get; set; }

        public async Task<bool> LoginAsync()
        {
            try
            {
                MessageBox.Show($"Tentative de connexion avec {Username} / {Password}");

                // Simuler un appel API (remplace par ton appel réel)
                await Task.Delay(1000);

                if (Username == "admin" && Password == "1234") // Exemple test
                {
                    MessageBox.Show("Authentification réussie !");
                    return true;
                }

                MessageBox.Show("Échec de l'authentification !");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}");
                return false;
            }
        }

    }
}
