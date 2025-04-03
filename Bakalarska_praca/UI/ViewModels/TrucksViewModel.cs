using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.Views;

namespace Bakalarska_praca.UI.ViewModels
{
    public class TrucksViewModel : BaseViewModel
    {
        private Truck? _selectedTruck;

        public ObservableCollection<Truck> Trucks { get; set; } = new();

        public Truck? SelectedTruck
        {
            get => _selectedTruck;
            set
            {
                _selectedTruck = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanEditDelete));
                ((RelayCommand)EditTruckCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeleteTruckCommand).RaiseCanExecuteChanged();

            }
        }

        public bool CanEditDelete => SelectedTruck != null;

        public ICommand AddTruckCommand { get; }
        public ICommand EditTruckCommand { get; }
        public ICommand DeleteTruckCommand { get; }

        public TrucksViewModel()
        {
            LoadTrucks();
            AddTruckCommand = new RelayCommand(ExecuteAddTruck);
            EditTruckCommand = new RelayCommand(ExecuteEditTruck, _ => CanEditDelete);
            DeleteTruckCommand = new RelayCommand(ExecuteDeleteTruck, _ => CanEditDelete);
        }

        private void LoadTrucks()
        {
            Trucks.Clear();
            foreach (var truck in TruckService.GetAllTrucks())
            {
                Trucks.Add(truck);
            }
        }

        private void ExecuteAddTruck(object? parameter)
        {
            var view = new AddTruckView();
            if (view.ShowDialog() == true)
            {
                LoadTrucks();
            }
        }

        private void ExecuteEditTruck(object? parameter)
        {
            if (SelectedTruck == null) return;

            var view = new EditTruckView(SelectedTruck);
            if (view.ShowDialog() == true)
            {
                LoadTrucks();
            }
        }

        private void ExecuteDeleteTruck(object? parameter)
        {
            if (SelectedTruck == null) return;

            var result = MessageBox.Show(
                $"Naozaj chcete vymazať vozidlo {SelectedTruck.LicensePlate}?",
                "Potvrdenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                TruckService.DeleteTruck(SelectedTruck.Id);
                LoadTrucks();
            }
        }
    }
}
