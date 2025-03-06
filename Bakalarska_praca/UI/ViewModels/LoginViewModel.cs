using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.Commands;

namespace Bakalarska_praca.UI.ViewModels;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _username;
    private string _password;
    private string _errorMessage;
    private bool _showError;
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

    public string ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(); }
    }

    public bool ShowError
    {
        get => _showError;
        set { _showError = value; OnPropertyChanged(); }
    }

    public ICommand LoginCommand { get; }

    public event PropertyChangedEventHandler? PropertyChanged;


    public LoginViewModel()
    {
        _username = string.Empty;
        _password = string.Empty;
        _errorMessage = string.Empty;
        _userService = new UserService();
        LoginCommand = new RelayCommand(ExecuteLogin);
    }


    private void ExecuteLogin(object? parameter)
    {
        if (_userService.ValidateUser(Username, Password))
        {
            MainWindow mainWindow = new MainWindow();
            Application.Current.MainWindow.Close();
            Application.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }
        else
        {
            ErrorMessage = "Nesprávne meno alebo heslo!";
            ShowError = true;
        }
    }


    protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
