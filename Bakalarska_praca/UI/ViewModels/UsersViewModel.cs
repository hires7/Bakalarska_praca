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

    public ICommand AddUserCommand { get; }

    public UsersViewModel()
    {
        LoadUsers();
        AddUserCommand = new RelayCommand(ExecuteAddUser);
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
}
