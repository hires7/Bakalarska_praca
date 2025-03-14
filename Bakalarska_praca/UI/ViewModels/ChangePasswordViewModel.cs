using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Services;


namespace Bakalarska_praca.UI.ViewModels;

public class ChangePasswordViewModel : BaseViewModel
{
    private string _oldPassword = string.Empty;
    private string _newPassword = string.Empty;
    private string _confirmPassword = string.Empty;
    private string _errorMessage = string.Empty;
    private bool _showError;
    private readonly UserService _userService;

    public string OldPassword
    {
        get => _oldPassword;
        set { _oldPassword = value; OnPropertyChanged(); }
    }

    public string NewPassword
    {
        get => _newPassword;
        set { _newPassword = value; OnPropertyChanged(); }
    }

    public string ConfirmPassword
    {
        get => _confirmPassword;
        set { _confirmPassword = value; OnPropertyChanged(); }
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

    public ICommand ChangePasswordCommand { get; }

    public ChangePasswordViewModel()
    {
        _userService = new UserService();
        ChangePasswordCommand = new RelayCommand(ExecuteChangePassword);
    }

    private void ExecuteChangePassword(object? parameter)
    {
        if (NewPassword != ConfirmPassword)
        {
            ErrorMessage = "Nové heslo sa nezhoduje!";
            ShowError = true;
            return;
        }

        bool success = _userService.ChangePassword(UserService.CurrentUser, OldPassword, NewPassword);

        if (success)
        {
            MessageBox.Show("Heslo bolo úspešne zmenené!", "Úspech", MessageBoxButton.OK, MessageBoxImage.Information);
            if (parameter is Window window)
            {
                window.Close();
            }
        }
        else
        {
            ErrorMessage = "Nesprávne staré heslo!";
            ShowError = true;
        }
    }
}
