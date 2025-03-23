using System.Windows;
using System.Windows.Input;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.Core.Services;

namespace Bakalarska_praca.UI.ViewModels;

public class AddDriverViewModel : BaseViewModel
{
    private string _firstName = string.Empty;
    private string _lastName = string.Empty;

    public string FirstName
    {
        get => _firstName;
        set
        {
            _firstName = value;
            OnPropertyChanged();
            ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }

    public string LastName
    {
        get => _lastName;
        set
        {
            _lastName = value;
            OnPropertyChanged();
            ((RelayCommand)SaveCommand).RaiseCanExecuteChanged();
        }
    }


    public ICommand SaveCommand { get; }

    public AddDriverViewModel()
    {
        SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
    }

    private bool CanExecuteSave(object? parameter)
    {
        return !string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName);
    }

    private void ExecuteSave(object? parameter)
    {
        var driver = new Driver
        {
            FirstName = FirstName,
            LastName = LastName
        };

        DriverService.AddDriver(driver);

        if (parameter is Window window)
        {
            window.DialogResult = true;
            window.Close();
        }
    }
}
