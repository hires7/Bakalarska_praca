using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.Views;

namespace Bakalarska_praca.UI.ViewModels;

public class UsersViewModel : BaseViewModel
{
    private ObservableCollection<User> _users;
    private User? _selectedUser;

    public ObservableCollection<User> Users
    {
        get => _users;
        set { _users = value; OnPropertyChanged(); }
    }

    public User? SelectedUser
    {
        get => _selectedUser;
        set
        {
            _selectedUser = value;
            OnPropertyChanged();
            ((RelayCommand)EditUserCommand).RaiseCanExecuteChanged();
            ((RelayCommand)DeleteUserCommand).RaiseCanExecuteChanged();
        }
    }

    public ICommand AddUserCommand { get; }
    public ICommand EditUserCommand { get; }
    public ICommand DeleteUserCommand { get; }

    public UsersViewModel()
    {
        _users = new ObservableCollection<User>();
        LoadUsers();

        AddUserCommand = new RelayCommand(ExecuteAddUser);
        EditUserCommand = new RelayCommand(ExecuteEditUser, CanExecuteEditDelete);
        DeleteUserCommand = new RelayCommand(ExecuteDeleteUser, CanExecuteEditDelete);
    }

    private void LoadUsers()
    {
        Users = new ObservableCollection<User>(UserService.GetAllUsers());
    }

    private void ExecuteAddUser(object? parameter)
    {
        AddUserView addUserView = new AddUserView();
        addUserView.ShowDialog();
        LoadUsers();
    }

    private bool CanExecuteEditDelete(object? parameter)
    {
        return SelectedUser != null;
    }

    private void ExecuteEditUser(object? parameter)
    {
        if (SelectedUser == null) return;

        EditUserView editUserView = new EditUserView(SelectedUser);
        editUserView.ShowDialog();
        LoadUsers();
    }

    private void ExecuteDeleteUser(object? parameter)
    {
        if (SelectedUser == null) return;

        MessageBoxResult result = MessageBox.Show($"Naozaj chcete vymazať používateľa {SelectedUser.Username}?",
            "Potvrdenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            UserService.DeleteUser(SelectedUser.Id);
            LoadUsers();
        }
    }
}
