using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels
{
    public class AddMaterialViewModel : BaseViewModel
    {
        private string _name = string.Empty;
        private string _humidityType = "";
        private string _coefficient = "";
        private readonly Window _window;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public string HumidityType
        {
            get => _humidityType;
            set { _humidityType = value; OnPropertyChanged(); }
        }

        public string Coefficient
        {
            get => _coefficient;
            set { _coefficient = value; OnPropertyChanged(); }
        }

        public ICommand SaveCommand { get; }

        public AddMaterialViewModel(Window window)
        {
            _window = window;
            SaveCommand = new RelayCommand(ExecuteSave);
        }

        private void ExecuteSave(object? parameter)
        {
            if (!double.TryParse(HumidityType, out var humidity))
            {
                MessageBox.Show("Zadaj platné číslo pre typ vlhkosti.");
                return;
            }

            if (!double.TryParse(Coefficient, out var coefficient))
            {
                MessageBox.Show("Zadaj platné číslo pre koeficient.");
                return;
            }

            var material = new Material
            {
                Name = Name,
                HumidityType = humidity,
                Coefficient = coefficient
            };

            MaterialService.AddMaterial(material);

            _window.DialogResult = true;
            _window.Close();
        }


    }
}
