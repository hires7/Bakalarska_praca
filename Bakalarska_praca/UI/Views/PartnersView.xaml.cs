using Bakalarska_praca.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Bakalarska_praca.UI.Views
{
    /// <summary>
    /// Interaction logic for PartnersView.xaml
    /// </summary>
    public partial class PartnersView : Window
    {
        public PartnersView()
        {
            InitializeComponent();
            DataContext = new PartnersViewModel();
        }
    }

}
