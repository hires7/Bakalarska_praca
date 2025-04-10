using System;
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
    public class WeighingFormViewModel : BaseViewModel
    {
        private readonly bool _isIncoming;
        private readonly Window _window;
        private readonly BackgroundWorker _portReader;

        public ObservableCollection<Truck> Trucks { get; set; } = new(TruckService.GetAllTrucks());
        public ObservableCollection<Partner> Partners { get; set; } = new(PartnerService.GetAllPartners());
        public ObservableCollection<Material> Materials { get; set; } = new(MaterialService.GetAllMaterials());

        public Truck? SelectedTruck
        {
            get => _selectedTruck;
            set { _selectedTruck = value; OnPropertyChanged(); }
        }
        private Truck? _selectedTruck;

        public Partner? SelectedPartner
        {
            get => _selectedPartner;
            set { _selectedPartner = value; OnPropertyChanged(); }
        }
        private Partner? _selectedPartner;

        public Material? SelectedMaterial
        {
            get => _selectedMaterial;
            set { _selectedMaterial = value; OnPropertyChanged(); }
        }
        private Material? _selectedMaterial;


        private string _tara = "";
        public string Tara
        {
            get => _tara;
            set
            {
                _tara = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Netto));
            }
        }

        private string _brutto = "";
        public string Brutto
        {
            get => _brutto;
            set
            {
                _brutto = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Netto));
            }
        }

        public string Netto =>
            (double.TryParse(Brutto, out var b) && double.TryParse(Tara, out var t))
                ? (b - t).ToString("F2")
                : "";

        public DateTime Date { get; set; } = DateTime.Today;
        public DateTime BruttoTime { get; set; } = DateTime.Now;
        public DateTime TaraTime { get; set; } = DateTime.Now;
        public string Note { get; set; } = "";

        public ICommand SaveCommand { get; }
        public ICommand LoadBruttoCommand { get; }
        public ICommand LoadTaraCommand { get; }

        public WeighingFormViewModel(bool isIncoming, Window window)
        {
            _isIncoming = isIncoming;
            _window = window;

            SaveCommand = new RelayCommand(ExecuteSave);
            LoadBruttoCommand = new RelayCommand(_ => ExecuteLoad("brutto"));
            LoadTaraCommand = new RelayCommand(_ => ExecuteLoad("tara"));

            _portReader = new BackgroundWorker();
            _portReader.DoWork += PortReader_DoWork;
            _portReader.RunWorkerCompleted += PortReader_RunWorkerCompleted;
        }

        private void ExecuteSave(object? parameter)
        {
            if (SelectedTruck == null || SelectedPartner == null || SelectedMaterial == null)
            {
                MessageBox.Show("Vyplňte všetky polia!", "Upozornenie", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (!double.TryParse(Brutto, out var bruttoVal) || !double.TryParse(Tara, out var taraVal))
            {
                MessageBox.Show("Zadajte platné čísla pre brutto a taru.", "Chyba", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var weighing = new Weighing
            {
                Date = Date,
                BruttoTime = BruttoTime,
                TaraTime = TaraTime,
                IsIncoming = _isIncoming,
                Brutto = bruttoVal,
                Tara = taraVal,
                Truck_Id = SelectedTruck.Id,
                Partner_Id = SelectedPartner.Id,
                Material_Id = SelectedMaterial.Id,
                User_Id = UserService.LoggedInUser!.Id, //uzivatel tu VZDY bude prihlaseny https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/null-forgiving
                Note = Note
            };

            WeighingService.AddWeighing(weighing);

            _window.DialogResult = true;
            _window.Close();
        }

        private string _activeField = "brutto";

        private void ExecuteLoad(string field)
        {
            if (!_portReader.IsBusy)
            {
                _activeField = field;
                if (field == "brutto") Brutto = "Načítavam...";
                else Tara = "Načítavam...";

                _portReader.RunWorkerAsync();
            }
        }

        private void PortReader_DoWork(object? sender, DoWorkEventArgs e)
        {
            try
            {
                using var port = new SerialPort("COM4", 9600) { ReadTimeout = 5000 };
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
                    if (_activeField == "brutto") Brutto = "";
                    else Tara = "";
                }
                else
                {
                    if (_activeField == "brutto") Brutto = result;
                    else Tara = result;
                }

                OnPropertyChanged(nameof(Brutto));
                OnPropertyChanged(nameof(Tara));
            }
        }
    }
}
