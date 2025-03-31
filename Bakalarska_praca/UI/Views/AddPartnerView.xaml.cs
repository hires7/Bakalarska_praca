using System.Windows;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views
{
    public partial class AddPartnerView : Window
    {
        public AddPartnerView()
        {
            InitializeComponent();
            DataContext = new AddPartnerViewModel(this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
