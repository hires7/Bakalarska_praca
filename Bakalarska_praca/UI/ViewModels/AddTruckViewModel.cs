using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels
{
    public class AddTruckViewModel : BaseViewModel
    {
        private string _licensePlate = string.Empty;
        private string _description = string.Empty;
        private string _tara = string.Empty;
        private bool _isInHouse;

        private readonly Window _window;

        public string LicensePlate
        {
            get => _licensePlate;
            set
            {
                _licensePlate = value;
                OnPropertyChanged();
                ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public string Description
        {
            get => _description;
            set { _description = value; OnPropertyChanged(); }
        }

        public string Tara
        {
            get => _tara;
            set
            {
                _tara = value;
                OnPropertyChanged();
                ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public bool IsInHouse
        {
            get => _isInHouse;
            set { _isInHouse = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public AddTruckViewModel(Window window)
        {
            _window = window;
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
        }

        private bool CanExecuteSave(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(LicensePlate) &&
                   double.TryParse(Tara, out _);
        }

        private void ExecuteSave(object? parameter)
        {
            if (!double.TryParse(Tara, out double taraValue))
            {
                MessageBox.Show("Zadaj platnú taru (číslo).", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var truck = new Truck
            {
                LicensePlate = LicensePlate,
                Description = Description,
                Tara = taraValue,
                IsInHouse = IsInHouse
            };

            TruckService.AddTruck(truck);

            _window.DialogResult = true;
            _window.Close();
        }
    }
}
