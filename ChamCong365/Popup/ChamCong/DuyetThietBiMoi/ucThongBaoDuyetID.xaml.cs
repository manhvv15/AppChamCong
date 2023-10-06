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

namespace ChamCong365.Popup.ChamCong.DuyetThietBiMoi
{
    /// <summary>
    /// Interaction logic for ucThongBaoDuyet.xaml
    /// </summary>
    public partial class ucThongBaoDuyetID : UserControl
    {
        MainWindow Main;
        private string ep_name;
        BrushConverter br = new BrushConverter();
        public ucThongBaoDuyetID(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            txtTenNVDuyet.Text = ep_name;
        }

        private void OK_Ed_Id(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
