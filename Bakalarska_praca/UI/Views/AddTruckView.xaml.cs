using System.Windows;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views
{
    public partial class AddTruckView : Window
    {
        public AddTruckView()
        {
            InitializeComponent();
            DataContext = new AddTruckViewModel(this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
