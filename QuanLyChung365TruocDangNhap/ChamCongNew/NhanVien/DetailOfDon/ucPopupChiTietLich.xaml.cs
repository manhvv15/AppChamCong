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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.DetailOfDon
{
    /// <summary>
    /// Interaction logic for ucPopupChiTietLich.xaml
    /// </summary>
    public partial class ucPopupChiTietLich : UserControl
    {
        public ucPopupChiTietLich()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void selectNgay(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
