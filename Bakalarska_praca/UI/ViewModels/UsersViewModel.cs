using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.Views;

namespace Bakalarska_praca.UI.ViewModels;

public class UsersViewModel : BaseViewModel
{
    private User? _selectedUser;

    public ObservableCollection<User> Users { get; set; } = new();

    public User? SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanEditDelete));

            ((RelayCommand)EditUserCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteUserCommand).RaiseCanExecuteChanged();
            ((RelayCommand)ChangePasswordCommand).RaiseCanExecuteChanged();
        }
    }


    public bool CanEditDelete => SelectedUser != null;

    public ICommand AddUserCommand { get; }
    public ICommand EditUserCommand { get; }
    public ICommand DeleteUserCommand { get; }
    public ICommand ChangePasswordCommand { get; }

    public UsersViewModel()
    {
        LoadUsers();
        AddUserCommand = new RelayCommand(ExecuteAddUser);
        EditUserCommand = new RelayCommand(ExecuteEditUser, _ => CanEditDelete);
        DeleteUserCommand = new RelayCommand(ExecuteDeleteUser, _ => CanEditDelete);
        ChangePasswordCommand = new RelayCommand(ExecuteChangePassword, _ => CanEditDelete);
    }

    private void LoadUsers()
    {
        Users.Clear();
        foreach (var user in UserService.GetAllUsers())
        {
            Users.Add(user);
        }
    }

    private void ExecuteAddUser(object? parameter)
    {
        var addUserView = new AddUserView();
        if (addUserView.ShowDialog() == true)
        {
            LoadUsers();
        }
    }

    private void ExecuteEditUser(object? parameter)
    {
        if (SelectedUser == null) return;

        var editUserView = new EditUserView(SelectedUser);
        if (editUserView.ShowDialog() == true)
        {
            LoadUsers();
        }
    }

    private void ExecuteDeleteUser(object? parameter)
    {
        if (SelectedUser == null) return;

        var result = MessageBox.Show($"Naozaj chcete vymazať používateľa {SelectedUser.Username}?",
            "Potvrdenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            UserService.DeleteUser(SelectedUser.Id);
            LoadUsers();
        }
    }

    private void ExecuteChangePassword(object? parameter)
    {
        if (SelectedUser == null) return;

        ChangePasswordView changePasswordView = new ChangePasswordView();
        changePasswordView.ShowDialog();
    }
}
