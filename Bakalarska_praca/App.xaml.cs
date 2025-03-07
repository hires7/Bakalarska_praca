using System.Runtime.InteropServices;
using System.Windows;
using Bakalarska_praca.Data.Database;
using Bakalarska_praca.UI.Views;

namespace Bakalarska_praca;

public partial class App : Application
{

    [DllImport("kernel32.dll")]                                     // OTVORENIE KONZOLE https://stackoverflow.com/questions/67642721/open-a-console-in-the-wpf-application
    private static extern bool AllocConsole();

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        AllocConsole(); 
        Console.WriteLine("Konzola bola spustená! Môžeš sledovať výstupy tu.");
        DatabaseInitializer.Initialize();
        DatabaseInitializer.CreateAdminUser();
    }
}
