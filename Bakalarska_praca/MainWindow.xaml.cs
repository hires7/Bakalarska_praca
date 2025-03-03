using Bakalarska_praca.Core.Services;
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
        // Reset používateľa
        UserService.Logout();
    }

}