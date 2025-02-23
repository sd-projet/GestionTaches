using System.Windows;
using System.Windows.Controls;
using GestionTache.ViewModels;
using GestionTache.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;


namespace GestionTache
{
    public partial class LoginWindow : Window
    {
       // private readonly LoginViewModel _viewModel = new LoginViewModel();
        private LoginViewModel _viewModel;

        public LoginWindow()
        {
            InitializeComponent();
            MessageBox.Show(DataContext == null ? "DataContext est NULL" : "DataContext est OK");
            _viewModel = new LoginViewModel();
            this.DataContext = _viewModel; // ✅ Associer le ViewModel à la vue
           // DataContext = _viewModel;
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Password = PasswordBox.Password;

            bool isLogged = await _viewModel.LoginAsync();

            if (isLogged)
            {
                MessageBox.Show("Connexion réussie !");
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect.");
            }
        }

    }
}
