using Bakalarska_praca.Core.Models;
using Bakalarska_praca.UI.ViewModels;
using System.Windows;

namespace Bakalarska_praca.UI.Views
{
    public partial class EditPartnerView : Window
    {
        public EditPartnerView(Partner partner)
        {
            InitializeComponent();
            DataContext = new EditPartnerViewModel(partner, this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
