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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.ChamCongBangQRRR.Function
{
    /// <summary>
    /// Interaction logic for PC365NhanVien.xaml
    /// </summary>
    public partial class PC365NhanVien : Window
    {
        MainChamCong Main;
        public PC365NhanVien(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChamCong365 uc = new ChamCong365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Chat365 uc = new Chat365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            PC365NhanVien uc = new PC365NhanVien(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
        void InstructionImageCollapsedAll()
        {
            DienThoai33.Visibility = Visibility.Collapsed;
            DienThoai32.Visibility = Visibility.Collapsed;
            DienThoai31.Visibility = Visibility.Collapsed;
        }
        private void Buoc11Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            InstructionImageCollapsedAll();
            DienThoai31.Visibility = Visibility.Visible;


        }

        private void Buoc12Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            InstructionImageCollapsedAll();
            Buoc12Hien.Visibility = Visibility.Collapsed;
            Buoc12An.Visibility = Visibility.Visible;
            DienThoai32.Visibility = Visibility.Visible;
        }

        private void Buoc13Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            InstructionImageCollapsedAll();
            Buoc13Hien.Visibility = Visibility.Collapsed;
            Buoc13An.Visibility = Visibility.Visible;
            DienThoai33.Visibility = Visibility.Visible;
        }


    }
}
