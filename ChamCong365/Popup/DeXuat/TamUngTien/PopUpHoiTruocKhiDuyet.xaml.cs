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

namespace ChamCong365.Popup.DeXuat.TamUngTien
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiDuyet.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiDuyet : UserControl
    {
        public PopUpHoiTruocKhiDuyet(MainWindow main,string noidung)
        {
            InitializeComponent();
            textNoiDung.Text = noidung;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
