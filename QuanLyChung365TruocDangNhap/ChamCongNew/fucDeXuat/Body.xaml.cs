﻿

using QuanLyChung365TruocDangNhap.ChamCongNew.CaiDatDeX;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew
{
    /// <summary>

    /// </summary>
    public partial class Body : UserControl
    {
        BrushConverter bcBody = new BrushConverter();
        MainWindow Main;
        public Body(MainWindow main)

        {
            InitializeComponent();
            Main = main;
            ucBodyHome ucbh = new ucBodyHome(Main);
            //txbNumber3.Text = ucbh.txbF3.Text + ". " + txbPropose.Text;
            //txbPropose.Text = ucbh.txbPropose.Text;
        }

        private void wapbuttonucNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucNhanVien uc = new ucNhanVien(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            //Main.txbLoadChamCong.Text = ucbodyhome.txbPropose.Text + " / " + txbFunction1.Text;
        }

        private void wapucInstallTime_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            DeXuat uc = new DeXuat(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadChamCong.Text = ucbodyhome.txbPropose.Text + " / " + txbFunction2.Text;

        }
        private void wapucAdvancemoney_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            DanhSachDeXuatCongTy uc = new DanhSachDeXuatCongTy(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadChamCong.Text = ucbodyhome.txbPropose.Text + " / " + txbFunction3.Text;

        }

        private void wapucOrganizationalchart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucOrganizationalchart uc = new ucOrganizationalchart(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            //Main.txbLoadChamCong.Text = ucbodyhome.txbPropose.Text + " / " + txbFunction4.Text;
        }

        private void wapUcDanhSachDeXuat_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}

