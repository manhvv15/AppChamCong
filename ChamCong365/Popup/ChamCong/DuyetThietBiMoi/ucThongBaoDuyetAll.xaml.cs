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
    /// Interaction logic for ucThongBaoDuyetAll.xaml
    /// </summary>
    public partial class ucThongBaoDuyetAll : UserControl
    {
        MainWindow Main;
        BrushConverter br = new BrushConverter();
        public ucThongBaoDuyetAll(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void OKAll(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
