using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels
{
    public class AddMaterialViewModel : BaseViewModel
    {
        private string _name = string.Empty;
        private double _humidityType;
        private double _coefficient;
        private readonly Window _window;

        public string Name
        {
            get => _name;
            set { _name = value; OnPropertyChanged(); }
        }

        public double HumidityType
        {
            get => _humidityType;
            set { _humidityType = value; OnPropertyChanged(); }
        }

        public double Coefficient
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
            var material = new Material
            {
                Name = Name,
                HumidityType = HumidityType,
                Coefficient = Coefficient
            };

            MaterialService.AddMaterial(material);
            _window.DialogResult = true;
            _window.Close();
        }
    }
}
