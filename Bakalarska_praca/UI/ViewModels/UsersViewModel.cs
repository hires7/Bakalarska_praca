using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.Commands;
using Bakalarska_praca.UI.Views;

namespace Bakalarska_praca.UI.ViewModels;

public class UsersViewModel : BaseViewModel
{
    private ObservableCollection<User> _users;
    public ObservableCollection<User> Users
    {
        get => _users;
        set { _users = value; OnPropertyChanged(); }
    }

    private User? _selectedUser;
    public User? SelectedUser
    {
        get => _selectedUser;
        set { _selectedUser = value; OnPropertyChanged(); }
    }

    public ICommand AddUserCommand { get; }
    public ICommand DeleteUserCommand { get; }

    public UsersViewModel()
    {
        _users = new ObservableCollection<User>();
        LoadUsers();
        AddUserCommand = new RelayCommand(ExecuteAddUser);
        DeleteUserCommand = new RelayCommand(ExecuteDeleteUser, CanDeleteUser);
    }

    private void LoadUsers()
    {
        Users = new ObservableCollection<User>(UserService.GetAllUsers());
    }

    private void ExecuteAddUser(object? parameter)
    {
        Console.WriteLine("Kliknuté na 'Pridať' - otváram okno.");
        AddUserView addUserView = new AddUserView();
        addUserView.ShowDialog();
        LoadUsers();
    }

    private bool CanDeleteUser(object? parameter)
    {
        return SelectedUser != null;
    }

    private void ExecuteDeleteUser(object? parameter)
    {
        if (SelectedUser == null)
            return;

        MessageBoxResult result = MessageBox.Show(
            $"Naozaj chcete odstrániť používateľa {SelectedUser.Username}?",
            "Potvrdenie",
            MessageBoxButton.YesNo,
            MessageBoxImage.Warning
        );

        if (result == MessageBoxResult.Yes)
        {
            UserService.DeleteUser(SelectedUser.Id);
            Users.Remove(SelectedUser);
        }
    }
}
