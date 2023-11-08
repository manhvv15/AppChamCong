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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChamCong365.Popup.PopupSalarySettings
{
    /// <summary>
    /// Interaction logic for ucNextSaffInsurance.xaml
    /// </summary>
    public partial class ucNextSaffInsurance : UserControl
    {
        MainWindow Main;
        public ucNextSaffInsurance(MainWindow main)
        {
            InitializeComponent();
            txbInputMoney.Focus();
            Main = main;
        }

        private void ExitNextSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucAddSaffForInsurance uca = new ucAddSaffForInsurance(Main);
            this.Visibility = Visibility.Collapsed;
   
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed; 
        }
    }
}
