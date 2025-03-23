using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.Views;

namespace Bakalarska_praca.UI.ViewModels;

public class DriversViewModel : BaseViewModel
{
    private Driver? _selectedDriver;

    public ObservableCollection<Driver> Drivers { get; set; } = new();

    public Driver? SelectedDriver
    {
        get => _selectedDriver;
        set
        {
            _selectedDriver = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(CanEditDelete));
        }
    }

    public bool CanEditDelete => SelectedDriver != null;

    public ICommand AddDriverCommand { get; }
    public ICommand EditDriverCommand { get; }
    public ICommand DeleteDriverCommand { get; }

    public DriversViewModel()
    {
        LoadDrivers();
        AddDriverCommand = new RelayCommand(ExecuteAddDriver);
        EditDriverCommand = new RelayCommand(ExecuteEditDriver, _ => CanEditDelete);
        DeleteDriverCommand = new RelayCommand(ExecuteDeleteDriver, _ => CanEditDelete);
    }

    private void LoadDrivers()
    {
        Drivers.Clear();
        foreach (var driver in DriverService.GetAllDrivers())
        {
            Drivers.Add(driver);
        }
    }

    private void ExecuteAddDriver(object? parameter)
    {
        var view = new AddDriverView();
        if (view.ShowDialog() == true)
            LoadDrivers();
    }

    private void ExecuteEditDriver(object? parameter)
    {
        if (SelectedDriver == null) return;
        var view = new EditDriverView(SelectedDriver);
        if (view.ShowDialog() == true)
            LoadDrivers();
    }

    private void ExecuteDeleteDriver(object? parameter)
    {
        if (SelectedDriver == null) return;

        var result = MessageBox.Show($"Naozaj chcete vymazať vodiča {SelectedDriver.FirstName} {SelectedDriver.LastName}?",
            "Potvrdenie", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            DriverService.DeleteDriver(SelectedDriver.Id);
            LoadDrivers();
        }
    }
}
