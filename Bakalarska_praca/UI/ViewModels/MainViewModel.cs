using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels;

public class MainViewModel : BaseViewModel
{
    private string _currentUser;
    public string CurrentUser
    {
        get => _currentUser;
        set { _currentUser = value; OnPropertyChanged(); }
    }

    public MainViewModel()
    {
        CurrentUser = UserService.CurrentUser;
    }

    public void RefreshUser()
    {
        CurrentUser = UserService.CurrentUser;
    }

}
