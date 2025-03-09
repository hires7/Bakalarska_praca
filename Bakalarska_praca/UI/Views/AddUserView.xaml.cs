using System.Windows;
using System.Windows.Controls;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views;

public partial class AddUserView : Window
{
    public AddUserView()
    {
        InitializeComponent();
        DataContext = new AddUserViewModel();
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        if (DataContext is AddUserViewModel viewModel)
        {
            viewModel.Password = ((PasswordBox)sender).Password;
        }
    }
}
