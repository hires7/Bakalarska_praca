using System.Windows;
using Bakalarska_praca.UI.ViewModels;
using Bakalarska_praca.Core.Models;

namespace Bakalarska_praca.UI.Views
{
    public partial class EditMaterialView : Window
    {
        public EditMaterialView(Material material)
        {
            InitializeComponent();
            DataContext = new EditMaterialViewModel(material, this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
