using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.LichSu.Function;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.TinhLuong.Function;
//using DocumentFormat.OpenXml.Spreadsheet;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.TinhLuong
{
    /// <summary>
    /// Interaction logic for ucListTinhLuong.xaml
    /// </summary>
    public partial class ucListTinhLuong : UserControl
    {
        MainChamCong Main;
        public ucListTinhLuong(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void btnXemBangLuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucXemBangLuong uc = new ucXemBangLuong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.txbLoadChamCong.Text = txbLoadNameFuction.Text + " / " + "Xem bảng lương";
        }

        private void btnXemLichLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucXemLichLamViec uc = new ucXemLichLamViec(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.txbLoadChamCong.Text = txbLoadNameFuction.Text + " / " + "Xem lịch làm việc";
        }

        private void btnXemBangCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucXemBangCong uc = new ucXemBangCong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.txbLoadChamCong.Text = txbLoadNameFuction.Text + " / " + "Xem bảng công";
        }
    }
}
