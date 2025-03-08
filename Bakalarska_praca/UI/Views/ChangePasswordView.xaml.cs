using System.Windows;
using System.Windows.Controls;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views;

public partial class ChangePasswordView : Window
{
    public ChangePasswordView()
    {
        InitializeComponent();
        DataContext = new ChangePasswordViewModel();
    }

    private void OldPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is ChangePasswordViewModel viewModel)
        {
            viewModel.OldPassword = ((PasswordBox)sender).Password;
            Console.WriteLine($"Zadané staré heslo: {viewModel.OldPassword}");
        }
    }

    private void NewPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is ChangePasswordViewModel viewModel)
        {
            viewModel.NewPassword = ((PasswordBox)sender).Password;
        }
    }

    private void ConfirmPasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is ChangePasswordViewModel viewModel)
        {
            viewModel.ConfirmPassword = ((PasswordBox)sender).Password;
        }
    }
}
