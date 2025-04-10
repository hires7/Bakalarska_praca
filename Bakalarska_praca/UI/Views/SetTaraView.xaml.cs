using System.Windows;
using Bakalarska_praca.UI.ViewModels;

namespace Bakalarska_praca.UI.Views
{
    public partial class SetTaraView : Window
    {
        public SetTaraView()
        {
            InitializeComponent();
            DataContext = new SetTaraViewModel(this);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
