using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels
{
    public class EditTruckViewModel : BaseViewModel
    {
        private string _licensePlate;
        private string _description;
        private string _tara;
        private bool _isInHouse;

        private readonly Truck _originalTruck;
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

        public EditTruckViewModel(Truck truck, Window window)
        {
            _originalTruck = truck;
            _window = window;

            // inicializácia hodnôt
            _licensePlate = truck.LicensePlate;
            _description = truck.Description;
            _tara = truck.Tara.ToString();
            _isInHouse = truck.IsInHouse;

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

            _originalTruck.LicensePlate = LicensePlate;
            _originalTruck.Description = Description;
            _originalTruck.Tara = taraValue;
            _originalTruck.IsInHouse = IsInHouse;

            TruckService.UpdateTruck(_originalTruck);

            _window.DialogResult = true;
            _window.Close();
        }
    }
}
