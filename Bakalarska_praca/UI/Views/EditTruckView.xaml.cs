using System.Windows;
using Bakalarska_praca.Core.Models;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views
{
    public partial class EditTruckView : Window
    {
        public EditTruckView(Truck truck)
        {
            InitializeComponent();
            DataContext = new EditTruckViewModel(truck, this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
