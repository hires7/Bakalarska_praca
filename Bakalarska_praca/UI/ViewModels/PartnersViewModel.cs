using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;
using Bakalarska_praca.UI.Views;

namespace Bakalarska_praca.UI.ViewModels
{
    public class PartnersViewModel : BaseViewModel
    {
        private Partner? _selectedPartner;
        public ObservableCollection<Partner> Partners { get; set; } = new();
        public Partner? SelectedPartner
        {
            get => _selectedPartner;
            set
            {
                _selectedPartner = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanEditDelete));
                ((RelayCommand)EditPartnerCommand).RaiseCanExecuteChanged();
                ((RelayCommand)DeletePartnerCommand).RaiseCanExecuteChanged();
            }
        }

        public bool CanEditDelete => SelectedPartner != null;

        public ICommand AddPartnerCommand { get; }
        public ICommand EditPartnerCommand { get; }
        public ICommand DeletePartnerCommand { get; }

        public PartnersViewModel()
        {
            LoadPartners();

            AddPartnerCommand = new RelayCommand(ExecuteAddPartner);
            EditPartnerCommand = new RelayCommand(ExecuteEditPartner, _ => CanEditDelete);
            DeletePartnerCommand = new RelayCommand(ExecuteDeletePartner, _ => CanEditDelete);
        }

        private void LoadPartners()
        {
            Partners.Clear();
            foreach (var partner in PartnerService.GetAllPartners())
            {
                Partners.Add(partner);
            }
        }

        private void ExecuteAddPartner(object? parameter)
        {
            var view = new AddPartnerView();
            if (view.ShowDialog() == true)
                LoadPartners();
        }

        private void ExecuteEditPartner(object? parameter)
        {
            if (SelectedPartner == null) return;

            var view = new EditPartnerView(SelectedPartner);
            if (view.ShowDialog() == true)
                LoadPartners();
        }

        private void ExecuteDeletePartner(object? parameter)
        {
            if (SelectedPartner == null) return;

            var result = MessageBox.Show(
                $"Naozaj chcete vymazať partnera {SelectedPartner.Name}?",
                "Potvrdenie",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                PartnerService.DeletePartner(SelectedPartner.Id);
                LoadPartners();
            }
        }
    }
}
