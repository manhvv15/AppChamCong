using ChamCong365.Popups.ChamCong;
using ChamCong365.Popups.ChamCong.CaiDatWifi;
using ChamCong365.Popups.ChamCong.ViTri;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.TimeKeeping
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
            ucDanhSachWii ucw = new ucDanhSachWii(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucw.Content;
            ucw.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);
            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            txbTextIP.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 5);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodTextIP.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextIP.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
        } 

        private void borAddIp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemDiaChiIP(Main,null,null));
        }

        private void Border_MouseLeftButtonUp_UpdateCollapsed(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi(Main, null, null));
        }

        private void bodAddWifi_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiWifi(Main,null,null));
        }

        private void Border_MouseLeftButtonUp_UpdateIP(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi(Main, null, null));
        }

        private void Border_MouseLeftButtonUp_UpdateWifi(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi(Main,null,null));
        }

        private void Border_MouseLeftButtonUp_DeleteIP(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucXoaWifi(null, null));
        }

        private void Border_MouseLeftButtonUp_DeleteWifi(object sender, MouseButtonEventArgs e)
        {
            
            Main.grShowPopup.Children.Add(new ucXoaWifi(null,null));

        }

 
        #region Chuyển màn popup
        private void Border_MouseLeftButtonUp_Wifi(object sender, MouseButtonEventArgs e)
        {
            ucDanhSachWii ucw = new ucDanhSachWii(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucw.Content;
            ucw.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 5);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            txbTextIP.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextIP.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextIP.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");

        }

        private void bodViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
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
            bodTextIP.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextIP.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTextIP.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
        }

        private void Border_MouseLeftButtonUp_Ip(object sender, MouseButtonEventArgs e)
        {

            ucDanhSachIP ucL = new ucDanhSachIP(Main, Main.IdAcount);
            grLoadListWifiIp.Children.Clear();
            object Content = ucL.Content;
            ucL.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);

            txbTextIP.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            bodTextIP.BorderThickness = new Thickness(0, 0, 0, 5);
            bodTextIP.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txtCamXuc.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodCamXuc.BorderThickness = new Thickness(0, 0, 0, 0);
            bodCamXuc.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");

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
            bodTextIP.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextIP.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            txbTextIP.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            txbViTri.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodViTri.BorderThickness = new Thickness(0, 0, 0, 0);
            bodViTri.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
        }
        #endregion
    }
}
