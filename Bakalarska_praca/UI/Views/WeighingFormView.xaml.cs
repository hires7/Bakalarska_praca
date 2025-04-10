using System.Windows;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views
{
    public partial class WeighingFormView : Window
    {
        public WeighingFormView(bool isIncoming)
        {
            InitializeComponent();
            DataContext = new WeighingFormViewModel(isIncoming, this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
