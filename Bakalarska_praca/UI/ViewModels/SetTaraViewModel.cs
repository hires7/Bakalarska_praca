using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels
{
    public class SetTaraViewModel : BaseViewModel
    {
        private ObservableCollection<Truck> _trucks = new(TruckService.GetAllTrucks());
        private Truck? _selectedTruck;
        private string _tara = "";

        private readonly Window _window;
        private readonly BackgroundWorker _portReader;

        public ObservableCollection<Truck> Trucks => _trucks;

        public Truck? SelectedTruck
        {
            get => _selectedTruck;
            set
            {
                _selectedTruck = value;
                Tara = value?.Tara.ToString() ?? "";
                OnPropertyChanged();
                ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
            }
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

        public ICommand LoadTaraCommand { get; }
        public ICommand SaveCommand { get; }

        public SetTaraViewModel(Window window)
        {
            _window = window;

            LoadTaraCommand = new RelayCommand(_ => ExecuteLoadTara());
            SaveCommand = new RelayCommand(_ => ExecuteSave(), _ => CanExecuteSave());

            _portReader = new BackgroundWorker();
            _portReader.DoWork += PortReader_DoWork;
            _portReader.RunWorkerCompleted += PortReader_RunWorkerCompleted;
        }

        private bool CanExecuteSave()
        {
            return SelectedTruck != null && double.TryParse(Tara, out _);
        }

        private void ExecuteSave()
        {
            if (SelectedTruck == null) return;

            if (!double.TryParse(Tara, out double parsed))
            {
                MessageBox.Show("Zadaj platnú taru.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SelectedTruck.Tara = parsed;
            TruckService.UpdateTruck(SelectedTruck);
            MessageBox.Show("Tara bola úspešne aktualizovaná.", "Hotovo", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ExecuteLoadTara()
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
