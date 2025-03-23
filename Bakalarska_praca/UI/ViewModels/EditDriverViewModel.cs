using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels;

public class EditDriverViewModel : BaseViewModel
{
    private string _firstName;
    private string _lastName;
    private readonly Driver _originalDriver;

    public string FirstName
    {
        get => _firstName;
        set { _firstName = value; OnPropertyChanged(); }
    }

    public string LastName
    {
        get => _lastName;
        set { _lastName = value; OnPropertyChanged(); }
    }

    public ICommand SaveCommand { get; }

    public EditDriverViewModel(Driver driver)
    {
        _originalDriver = driver;
        _firstName = driver.FirstName;
        _lastName = driver.LastName;

        SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
    }

    private bool CanExecuteSave(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(FirstName) &&
               !string.IsNullOrWhiteSpace(LastName) &&
               (FirstName != _originalDriver.FirstName || LastName != _originalDriver.LastName);
    }

    private void ExecuteSave(object? parameter)
    {
        _originalDriver.FirstName = FirstName;
        _originalDriver.LastName = LastName;

        DriverService.UpdateDriver(_originalDriver);

        if (parameter is Window window)
        {
            window.DialogResult = true;
            window.Close();
        }
    }
}
