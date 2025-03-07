using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels;

public class MainViewModel : BaseViewModel
{
    private bool _isLoggedIn;
    public bool IsLoggedIn
    {
        get => _isLoggedIn;
        set
        {
            _isLoggedIn = value;
            Console.WriteLine($"Aktualizácia IsLoggedIn: {_isLoggedIn}");
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanLogin));
            OnPropertyChanged(nameof(CanLogout));
        }
    }

    public bool CanLogin => !IsLoggedIn;
    public bool CanLogout => IsLoggedIn;

    public MainViewModel()
    {
        RefreshUser();
    }

    public void RefreshUser()
    {
        IsLoggedIn = UserService.CurrentUser != "Neprihlásený";
        Console.WriteLine($"Aktualizácia IsLoggedIn: {IsLoggedIn}, CurrentUser: {UserService.CurrentUser}");
        OnPropertyChanged(nameof(IsLoggedIn));
        OnPropertyChanged(nameof(CanLogin));
        OnPropertyChanged(nameof(CanLogout));
    }


}
