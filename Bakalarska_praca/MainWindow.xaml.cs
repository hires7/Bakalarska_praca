using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.ViewModels;
using Bakalarska_praca.UI.Views;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bakalarska_praca;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Login_Click(object sender, RoutedEventArgs e)
    {
        LoginView loginWindow = new LoginView();
        loginWindow.ShowDialog();
    }

    private void Logout_Click(object sender, RoutedEventArgs e)
    {
        UserService.Logout();
        Console.WriteLine(" Používateľ odhlásený.");

        if (Application.Current.Windows.OfType<LoginView>().FirstOrDefault() is LoginView loginWindow)
        {
            loginWindow.Show();
        }
        else
        {
            loginWindow = new LoginView();
            loginWindow.Show();
        }

        Application.Current.MainWindow.Hide();

        if (loginWindow.DataContext is LoginViewModel loginViewModel)
        {
            loginViewModel.ShowError = false;
        }

        if (loginWindow.DataContext is MainViewModel mainViewModel)
        {
            mainViewModel.RefreshUser();
        }
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


}


