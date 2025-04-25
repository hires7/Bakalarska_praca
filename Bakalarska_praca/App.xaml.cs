using System.Runtime.InteropServices;
using System.Windows;
using Bakalarska_praca.Data.Database;
using Bakalarska_praca.UI.Views;

namespace Bakalarska_praca;

public partial class App : Application
{


    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);
        DatabaseInitializer.Initialize();
        DatabaseInitializer.CreateAdminUser();
    }
}
