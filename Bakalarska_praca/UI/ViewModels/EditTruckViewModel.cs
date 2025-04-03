using System.ComponentModel;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels;

public class EditTruckViewModel : BaseViewModel
{
    private readonly Truck _originalTruck;
    private readonly Window _window;
    private readonly BackgroundWorker _worker;

    public string LicensePlate { get; set; }
    public string Description { get; set; }
    public string Tara { get; set; }
    public bool IsInHouse { get; set; }

    public ICommand SaveCommand { get; }
    public ICommand LoadTaraCommand { get; }

    public EditTruckViewModel(Truck truck, Window window)
    {
        _originalTruck = truck;
        _window = window;

        LicensePlate = truck.LicensePlate;
        Description = truck.Description;
        Tara = truck.Tara.ToString();
        IsInHouse = truck.IsInHouse;

        SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
        LoadTaraCommand = new RelayCommand(_ => LoadTara());

        _worker = new BackgroundWorker();
        _worker.DoWork += Worker_DoWork;
        _worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
    }

    private bool CanExecuteSave(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(LicensePlate) && double.TryParse(Tara, out _);
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

    private void LoadTara()
    {
        if (!_worker.IsBusy)
            _worker.RunWorkerAsync();
    }

    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        try
        {
            using SerialPort port = new SerialPort("COM4", 9600);
            port.Open();
            string data = port.ReadLine();

            Match match = Regex.Match(data, @"(\d+)kg");
            if (match.Success && match.Groups[1].Value.Length >= 5)
            {
                string raw = match.Groups[1].Value;
                string poslednych5 = raw.Substring(raw.Length - 5);
                e.Result = poslednych5;
            }
        }
        catch
        {
            e.Result = null;
        }
    }

    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        if (e.Result is string result)
        {
            Tara = result;
            OnPropertyChanged(nameof(Tara));
        }
        else
        {
            MessageBox.Show("Nepodarilo sa načítať hmotnosť.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
