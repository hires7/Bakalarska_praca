using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels
{
    public class EditMaterialViewModel : BaseViewModel
    {
        private string _name;
        private double _humidityType;
        private double _coefficient;
        private readonly Material _originalMaterial;
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

        public EditMaterialViewModel(Material material, Window window)
        {
            _originalMaterial = material;
            _window = window;
            _name = material.Name;
            _humidityType = material.HumidityType;
            _coefficient = material.Coefficient;
            SaveCommand = new RelayCommand(ExecuteSave);
        }

        private void ExecuteSave(object? parameter)
        {
            _originalMaterial.Name = Name;
            _originalMaterial.HumidityType = HumidityType;
            _originalMaterial.Coefficient = Coefficient;

            MaterialService.UpdateMaterial(_originalMaterial);
            _window.DialogResult = true;
            _window.Close();
        }
    }
}
