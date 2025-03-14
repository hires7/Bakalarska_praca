using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels;

public class EditUserViewModel : BaseViewModel
{
    private string _username;
    private bool _isAdmin;
    private readonly User _originalUser;

    public string Username
    {
        get => _username;
        set
        {
            _username = value;
            OnPropertyChanged();
            ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }

    public bool IsAdmin
    {
        get => _isAdmin;
        set
        {
            _isAdmin = value;
            OnPropertyChanged();
            ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }

    public ICommand SaveCommand { get; }

    public EditUserViewModel(User user)
    {
        _originalUser = user;
        _username = user.Username;
        _isAdmin = user.Role == "Admin";
        SaveCommand = new RelayCommand(ExecuteSaveUser, CanExecuteSave);
    }

    private bool CanExecuteSave(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Username) &&
               (Username != _originalUser.Username || IsAdmin != (_originalUser.Role == "Admin"));
    }

    private void ExecuteSaveUser(object? parameter)
    {
        _originalUser.Username = Username;
        _originalUser.Role = IsAdmin ? "Admin" : "User";

        if (UserService.UpdateUser(_originalUser))
        {
            MessageBox.Show("Používateľ bol úspešne aktualizovaný.", "Úspech", MessageBoxButton.OK, MessageBoxImage.Information);

            if (parameter is Window window)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
        else
        {
            MessageBox.Show("Chyba pri aktualizácii používateľa.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}
