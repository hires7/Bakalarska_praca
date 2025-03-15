using System.Windows;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views
{
    public partial class AddMaterialView : Window
    {
        public AddMaterialView()
        {
            InitializeComponent();
            DataContext = new AddMaterialViewModel(this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
