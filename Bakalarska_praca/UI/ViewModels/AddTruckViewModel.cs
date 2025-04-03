using System.ComponentModel;
using System.IO.Ports;
using System.Text.RegularExpressions;
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
        private readonly BackgroundWorker _portReader;

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
        public ICommand LoadTaraCommand { get; }

        public AddTruckViewModel(Window window)
        {
            _window = window;

            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            LoadTaraCommand = new RelayCommand(ExecuteLoadTara);

            _portReader = new BackgroundWorker();
            _portReader.DoWork += PortReader_DoWork;
            _portReader.RunWorkerCompleted += PortReader_RunWorkerCompleted;
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

        private void ExecuteLoadTara(object? parameter)
        {
            if (!_portReader.IsBusy)
            {
                Tara = "Načítavam...";
                _portReader.RunWorkerAsync();
            }
        }

        private void PortReader_DoWork(object? sender, DoWorkEventArgs e)
        {
            try
            {
                using var port = new SerialPort("COM4", 9600)
                {
                    ReadTimeout = 5000
                };

                port.Open();
                string data = port.ReadLine();

                MatchCollection matches = Regex.Matches(data, @"(\d+)kg");

                if (matches.Count > 0)
                {
                    string raw = matches[^1].Groups[1].Value;

                    if (raw.Length >= 5)
                    {
                        string poslednych5 = raw[^5..];
                        if (int.TryParse(poslednych5, out int hmotnost))
                            e.Result = hmotnost.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                e.Result = $"Chyba: {ex.Message}";
            }
        }

        private void PortReader_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Result is string result)
            {
                if (result.StartsWith("Chyba:"))
                {
                    MessageBox.Show(result, "Chyba", MessageBoxButton.OK, MessageBoxImage.Error);
                    Tara = "";
                }
                else
                {
                    Tara = result;
                }
            }
        }
    }
}
