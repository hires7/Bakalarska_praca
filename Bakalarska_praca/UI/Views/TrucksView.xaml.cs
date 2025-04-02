using System.Windows;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views
{
    public partial class TrucksView : Window
    {
        public TrucksView()
        {
            InitializeComponent();
            DataContext = new TrucksViewModel();
        }
    }
}
