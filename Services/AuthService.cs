using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

public class AuthService
{
    private readonly HttpClient _httpClient = new HttpClient();

    public async Task<bool> LoginAsync(string username, string password)
    {
        try
        {
            var data = new { Username = username, Password = password };
            string json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await _httpClient.PostAsync("https://tonapi.com/auth", content);

            MessageBox.Show($"Réponse HTTP : {response.StatusCode}");

            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Connexion API réussie !");
                return true;
            }
            else
            {
                MessageBox.Show("Échec de la connexion API.");
                return false;
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur API : {ex.Message}");
            return false;
        }
    }
}
