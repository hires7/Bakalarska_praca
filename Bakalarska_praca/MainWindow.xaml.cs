using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.ViewModels;
using Bakalarska_praca.UI.Views;
using System;
using System.Linq;
using System.Windows;

namespace Bakalarska_praca
{
    public partial class MainWindow : Window
    {
        private readonly MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = new MainViewModel();
            DataContext = _viewModel;
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            LoginView loginWindow = new LoginView();
            loginWindow.ShowDialog();

            _viewModel.RefreshUser();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            UserService.Logout();
            Console.WriteLine("Používateľ odhlásený.");

            _viewModel.RefreshUser();
        }

        private void ChangePassword_Click(object sender, RoutedEventArgs e)
        {
            ChangePasswordView changePasswordView = new ChangePasswordView();
            changePasswordView.ShowDialog();
        }

        private void Users_Click(object sender, RoutedEventArgs e)
        {
            UsersView usersView = new UsersView();
            usersView.ShowDialog();
        }

        private void OpenMaterialsView(object sender, RoutedEventArgs e)
        {
            MaterialsView materialsView = new MaterialsView();
            materialsView.ShowDialog();
        }
        private void OpenDriversView(object sender, RoutedEventArgs e)
        {
            var driversView = new DriversView();
            driversView.ShowDialog();
        }

        private void OpenPartnersClick(object sender, RoutedEventArgs e)
        {
            var partnersView = new PartnersView();
            partnersView.ShowDialog();
        }

        private void Trucks_Click(object sender, RoutedEventArgs e)
        {
            TrucksView trucksView = new TrucksView();
            trucksView.ShowDialog();
        }


    }
}
