using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.Commands;

namespace Bakalarska_praca.UI.ViewModels;

public class AddUserViewModel : BaseViewModel
{
    private string _username;
    private string _password;
    private bool _isAdmin;
    private readonly UserService _userService;

    public string Username
    {
        get => _username;
        set { _username = value; OnPropertyChanged(); }
    }

    public string Password
    {
        private get => _password;
        set { _password = value; OnPropertyChanged(); }
    }

    public bool IsAdmin
    {
        get => _isAdmin;
        set { _isAdmin = value; OnPropertyChanged(); }
    }

    public ICommand AddCommand { get; }

    public AddUserViewModel()
    {
        _userService = new UserService();
        AddCommand = new RelayCommand(ExecuteAddUser);
    }

    private void ExecuteAddUser(object? parameter)
    {
        if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
        {
            MessageBox.Show("Login a heslo musia byť vyplnené!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
            return;
        }

        bool success = UserService.AddUser(Username, Password, IsAdmin);

        if (success)
        {
            MessageBox.Show("Používateľ bol pridaný!", "Úspech", MessageBoxButton.OK, MessageBoxImage.Information);
            if (parameter is Window window)
            {
                window.Close();
            }
        }
        else
        {
            MessageBox.Show("Používateľ s týmto loginom už existuje!", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
