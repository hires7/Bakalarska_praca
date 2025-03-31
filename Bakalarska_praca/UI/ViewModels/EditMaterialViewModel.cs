using System;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels
{
    public class EditMaterialViewModel : BaseViewModel
    {
        private string _name;
        private string _humidityType;
        private string _coefficient;
        private readonly Material _originalMaterial;
        private readonly Window _window;

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
                ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public string HumidityType
        {
            get => _humidityType;
            set
            {
                _humidityType = value;
                OnPropertyChanged();
                ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public string Coefficient
        {
            get => _coefficient;
            set
            {
                _coefficient = value;
                OnPropertyChanged();
                ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
            }
        }

        public ICommand SaveCommand { get; }

        public EditMaterialViewModel(Material material, Window window)
        {
            _originalMaterial = material;
            _window = window;

            _name = material.Name;
            _humidityType = material.HumidityType.ToString();
            _coefficient = material.Coefficient.ToString();

            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
        }

        private bool CanExecuteSave(object? parameter)
        {
            return !string.IsNullOrWhiteSpace(Name) &&
                   double.TryParse(HumidityType, out _) &&
                   double.TryParse(Coefficient, out _);
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

            _originalMaterial.Name = Name;
            _originalMaterial.HumidityType = humidity;
            _originalMaterial.Coefficient = coefficient;

            MaterialService.UpdateMaterial(_originalMaterial);

            _window.DialogResult = true;
            _window.Close();
        }
    }
}
