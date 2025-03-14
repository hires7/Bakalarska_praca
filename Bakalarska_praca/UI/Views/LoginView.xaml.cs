using System.Windows;
using System.Windows.Controls;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views;

public partial class LoginView : Window
{
    public LoginView()
    {
        InitializeComponent();
        var viewModel = new LoginViewModel();
        DataContext = viewModel;

    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is LoginViewModel viewModel)
        {
            viewModel.Password = ((PasswordBox)sender).Password;
        }
    }
}
