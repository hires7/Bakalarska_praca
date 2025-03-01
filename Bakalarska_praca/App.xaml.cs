using System.Windows;
using Bakalarska_praca.Data.Database;

namespace Bakalarska_praca;

public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        DatabaseInitializer.Initialize();
        DatabaseInitializer.CreateAdminUser();
        base.OnStartup(e);
    }
}
