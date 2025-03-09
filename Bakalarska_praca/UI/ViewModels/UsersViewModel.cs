using System.Collections.ObjectModel;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels;

public class UsersViewModel : BaseViewModel
{
    private ObservableCollection<User> _users;
    public ObservableCollection<User> Users
    {
        get => _users;
        set { _users = value; OnPropertyChanged(); }
    }

    public UsersViewModel()
    {
        LoadUsers();
    }

    private void LoadUsers()
    {
        Users = new ObservableCollection<User>(UserService.GetAllUsers());
    }
}
