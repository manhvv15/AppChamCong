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

namespace ChamCong365.Popup.ChamCong.CapNhapKhuonMat
{
    /// <summary>
    /// Interaction logic for ucThongBaoBoDuyetAll.xaml
    /// </summary>
    public partial class ucThongBaoBoDuyetAll : UserControl
    {
        MainWindow Main;
        public ucThongBaoBoDuyetAll(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void BoDuyetAll(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
