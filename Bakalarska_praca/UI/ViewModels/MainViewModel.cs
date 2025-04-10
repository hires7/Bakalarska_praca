using Bakalarska_praca.Core.Services;
using System.Collections.ObjectModel;

namespace Bakalarska_praca.UI.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public bool CanLogin => !UserService.IsLoggedIn;
        public bool CanLogout => UserService.IsLoggedIn;
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
        public string CurrentUserStatus => $"Prihlásený používateľ: {UserService.CurrentUser}";

        public WeighingListViewModel WeighingsVM { get; } = new();

        public void RefreshUser()
        {
            IsLoggedIn = UserService.CurrentUser != "Neprihlásený";
            Console.WriteLine($"Aktualizácia IsLoggedIn: {IsLoggedIn}, CurrentUser: {UserService.CurrentUser}");
            OnPropertyChanged(nameof(IsLoggedIn));
            OnPropertyChanged(nameof(CanLogin));
            OnPropertyChanged(nameof(CanLogout));
        }


    }
}
