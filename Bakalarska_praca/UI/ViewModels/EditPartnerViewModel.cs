using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels;

public class EditPartnerViewModel : BaseViewModel
{
    private readonly Partner _originalPartner;
    private readonly Window _window;

    public string Name
    {
        get => _originalPartner.Name;
        set { _originalPartner.Name = value; OnPropertyChanged(); }
    }

    public string Street
    {
        get => _originalPartner.Street;
        set { _originalPartner.Street = value; OnPropertyChanged(); }
    }

    public string City
    {
        get => _originalPartner.City;
        set { _originalPartner.City = value; OnPropertyChanged(); }
    }

    public string ZipCode
    {
        get => _originalPartner.ZipCode;
        set { _originalPartner.ZipCode = value; OnPropertyChanged(); }
    }

    public string ICO
    {
        get => _originalPartner.ICO;
        set { _originalPartner.ICO = value; OnPropertyChanged(); }
    }

    public string DIC
    {
        get => _originalPartner.DIC;
        set { _originalPartner.DIC = value; OnPropertyChanged(); }
    }

    public string IC_DPH
    {
        get => _originalPartner.IC_DPH;
        set { _originalPartner.IC_DPH = value; OnPropertyChanged(); }
    }

    public bool IsSupplier
    {
        get => _originalPartner.IsSupplier;
        set { _originalPartner.IsSupplier = value; OnPropertyChanged(); }
    }

    public bool IsCustomer
    {
        get => _originalPartner.IsCustomer;
        set { _originalPartner.IsCustomer = value; OnPropertyChanged(); }
    }

    public ICommand SaveCommand { get; }

    public EditPartnerViewModel(Partner partner, Window window)
    {
        _originalPartner = partner;
        _window = window;
        SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
    }

    private bool CanExecuteSave(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Name) && !string.IsNullOrWhiteSpace(ICO);
    }

    private void ExecuteSave(object? parameter)
    {
        PartnerService.UpdatePartner(_originalPartner);

        _window.DialogResult = true;
        _window.Close();
    }
}
