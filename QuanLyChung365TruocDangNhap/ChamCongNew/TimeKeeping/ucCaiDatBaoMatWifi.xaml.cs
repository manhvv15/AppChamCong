using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatQR;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.LoaiHinhChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.QR;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ThongBao;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ViTri;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.PopupTimeKeeping;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.CaiDatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.ViTri;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucSecurityWifi.xaml
    /// </summary>
    public class Wifi
    {
        public string NameWifi { get; set; }
        public string AddressWifi { get; set; }
        public String AddressIP { get; set; }
        public DateTime UpdateWifi { get; set; }
    }

    public partial class ucCaiDatBaoMatWifi : UserControl
    {
        public MainWindow Main;
        int Idcom;
        private ucDanhSachChucNangChamCong ucDanhSachChucNangChamCong;
        BrushConverter bcWifi = new BrushConverter();
        List<Wifi> itemsWifi = new List<Wifi>();
        public ucCaiDatBaoMatWifi(MainWindow main, int com_id)
        {
            InitializeComponent();
            Main = main;
            Idcom = com_id;
            ucDanhSachWifi ucw = new ucDanhSachWifi(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucw.Content;
            ucw.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);
            ucw.LoadListWifi();
            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 5);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
        }

        private void borAddIp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemDiaChiIP(Main, null, null));
        }

        private void Border_MouseLeftButtonUp_UpdateCollapsed(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi(Main, null, null));
        }

        private void bodAddWifi_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiWifi(Main, null, null));
        }

        private void Border_MouseLeftButtonUp_UpdateIP(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi(Main, null, null));
        }

        private void Border_MouseLeftButtonUp_UpdateWifi(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi(Main, null, null));
        }

        private void Border_MouseLeftButtonUp_DeleteIP(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucXoaWifi(null, null, null));
        }

        private void Border_MouseLeftButtonUp_DeleteWifi(object sender, MouseButtonEventArgs e)
        {

            Main.grShowPopup.Children.Add(new ucXoaWifi(null, null, null));

        }


        #region Chuyển màn popup
        private void Border_MouseLeftButtonUp_Wifi(object sender, MouseButtonEventArgs e)
        {
            ucDanhSachWifi ucw = new ucDanhSachWifi(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucw.Content;
            ucw.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 5);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");


            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTypeTimeSheet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTypeTimeSheet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTypeTimeSheet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
        }

        private void bodViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            //ucViTri uc = new ucViTri();
            ucViTri uc = new ucViTri();
            grLoadListWifiIp.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 5);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTypeTimeSheet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTypeTimeSheet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTypeTimeSheet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");

        }

        private void Border_MouseLeftButtonUp_Ip(object sender, MouseButtonEventArgs e)
        {

            ucDanhSachIP ucL = new ucDanhSachIP(Main, Main.IdAcount);
            grLoadListWifiIp.Children.Clear();
            object Content = ucL.Content;
            ucL.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);



            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTypeTimeSheet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTypeTimeSheet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTypeTimeSheet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
        }

        private void bodCamXuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucCamXuc ucc1 = new ucCamXuc(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucc1.Content;
            ucc1.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 5);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");

            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTypeTimeSheet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTypeTimeSheet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTypeTimeSheet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
        }
        #endregion

        private void Bor_ChiTiet(object sender, MouseButtonEventArgs e)
        {
            ucDanhSachChiTiet ucc1 = new ucDanhSachChiTiet(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucc1.Content;
            ucc1.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 5);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTypeTimeSheet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTypeTimeSheet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTypeTimeSheet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
        }

        private void bodQR_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucListQR ucc1 = new ucListQR(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucc1.Content;
            ucc1.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            txbQR.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodQR.BorderThickness = new Thickness(0, 0, 0, 5);
            bodQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTypeTimeSheet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTypeTimeSheet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTypeTimeSheet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
        }
        private void bodChamCongQR_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucDanhSachQR ucc1 = new ucDanhSachQR(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucc1.Content;
            ucc1.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 5);
            bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbNotify.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodNotify.BorderThickness = new Thickness(0, 0, 0, 0);
            bodNotify.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTypeTimeSheet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTypeTimeSheet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTypeTimeSheet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
        }

        private void bodNotify_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucCaiDatThongBaoChamCong ucc1 = new ucCaiDatThongBaoChamCong(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucc1.Content;
            ucc1.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            txbNotify.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodNotify.BorderThickness = new Thickness(0, 0, 0, 5);
            bodNotify.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");


            txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");

            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            txbTypeTimeSheet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTypeTimeSheet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTypeTimeSheet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
        }

        private void bodTypeTimeSheet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucLoaiHinhChamCong ucc1 = new ucLoaiHinhChamCong();
            grLoadListWifiIp.Children.Clear();
            object Content = ucc1.Content;
            ucc1.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            txbTypeTimeSheet.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodTypeTimeSheet.BorderThickness = new Thickness(0, 0, 0, 5);
            bodTypeTimeSheet.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            txbNotify.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodNotify.BorderThickness = new Thickness(0, 0, 0, 0);
            bodNotify.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");

            TextChiTiet.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextChiTiet.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextChiTiet.BorderBrush = (Brush)bcWifi.ConvertFrom("#474747");
            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbQR.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodQR.BorderThickness = new Thickness(0, 0, 0, 0);
            bodQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
        }
    }
}
