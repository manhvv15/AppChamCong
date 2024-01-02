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
    /// Interaction logic for ChamCong365.xaml
    /// </summary>
    public partial class ChamCong365 : Window
    {
        MainChamCong Main;
        public ChamCong365(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void scroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
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
        void CollapsedAll()
        {
            DienThoai11.Visibility = Visibility.Collapsed;
            DienThoai12.Visibility = Visibility.Collapsed;
            DienThoai13.Visibility = Visibility.Collapsed;
        }
        private void Buoc12Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CollapsedAll();
            Buoc12Hien.Visibility = Visibility.Collapsed;
            Buoc12An.Visibility = Visibility.Visible;
            DienThoai12.Visibility = Visibility.Visible;
        }

        private void Buoc13Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CollapsedAll();
            Buoc13Hien.Visibility = Visibility.Collapsed;
            Buoc13An.Visibility = Visibility.Visible;
            DienThoai13.Visibility = Visibility.Visible;
        }

        private void Buoc11Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CollapsedAll();
            DienThoai11.Visibility = Visibility.Visible;
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

        }




        //private void Window_SizeChanged_1(object sender, SizeChangedEventArgs e)
        //{
        //    if (ChamCongQR.Width < 1366)
        //    {

        //        bodyHien.Visibility = Visibility.Collapsed;
        //        bodyAn.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        bodyHien.Visibility = Visibility.Visible;
        //        bodyAn.Visibility = Visibility.Collapsed;

        //    }
        //}
    }
}

