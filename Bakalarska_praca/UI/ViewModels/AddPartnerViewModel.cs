using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels;

public class AddPartnerViewModel : BaseViewModel
{
    private string _name = string.Empty;
    private string _street = string.Empty;
    private string _city = string.Empty;
    private string _zipCode = string.Empty;
    private string _ico = string.Empty;
    private string _dic = string.Empty;
    private string _icDph = string.Empty;
    private bool _isSupplier;
    private bool _isCustomer;

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); ((RelayCommand)SaveCommand).RaiseCanExecuteChanged(); }
    }

    public string Street
    {
        get => _street;
        set { _street = value; OnPropertyChanged(); }
    }

    public string City
    {
        get => _city;
        set { _city = value; OnPropertyChanged(); }
    }

    public string ZipCode
    {
        get => _zipCode;
        set { _zipCode = value; OnPropertyChanged(); }
    }

    public string ICO
    {
        get => _ico;
        set { _ico = value; OnPropertyChanged(); ((RelayCommand)SaveCommand).RaiseCanExecuteChanged(); }
    }

    public string DIC
    {
        get => _dic;
        set { _dic = value; OnPropertyChanged(); }
    }

    public string IC_DPH
    {
        get => _icDph;
        set { _icDph = value; OnPropertyChanged(); }
    }

    public bool IsSupplier
    {
        get => _isSupplier;
        set { _isSupplier = value; OnPropertyChanged(); }
    }

    public bool IsCustomer
    {
        get => _isCustomer;
        set { _isCustomer = value; OnPropertyChanged(); }
    }

    public ICommand SaveCommand { get; }

    private readonly Window _window;

    public AddPartnerViewModel(Window window)
    {
        _window = window;
        SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
    }

    private bool CanExecuteSave(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(Name) &&
               !string.IsNullOrWhiteSpace(ICO);
    }

    private void ExecuteSave(object? parameter)
    {
        var partner = new Partner
        {
            Name = Name,
            Street = Street,
            City = City,
            ZipCode = ZipCode,
            ICO = ICO,
            DIC = DIC,
            IC_DPH = IC_DPH,
            IsSupplier = IsSupplier,
            IsCustomer = IsCustomer
        };

        PartnerService.AddPartner(partner);

        _window.DialogResult = true;
        _window.Close();
    }
}
