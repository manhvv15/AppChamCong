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

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for LuongHienTai.xaml
    /// </summary>
    public partial class LuongHienTai : Window
    {
        MainChamCong Main;
        public LuongHienTai(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void luongCoBanAn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LuongCoBan uc = new LuongCoBan(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void LuongHienTaiAn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LuongHienTai uc = new LuongHienTai(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LuongDieuChinh uc = new LuongDieuChinh(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
