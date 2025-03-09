using System.Windows;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views;

public partial class UsersView : Window
{
    public UsersView()
    {
        InitializeComponent();
        DataContext = new UsersViewModel();
    }
}
