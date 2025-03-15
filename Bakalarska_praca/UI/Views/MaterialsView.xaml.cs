using System.Windows;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views
{
    public partial class MaterialsView : Window
    {
        public MaterialsView()
        {
            InitializeComponent();
            DataContext = new MaterialsViewModel();
        }
    }
}
