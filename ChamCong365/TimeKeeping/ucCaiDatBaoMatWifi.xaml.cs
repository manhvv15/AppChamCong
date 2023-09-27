using ChamCong365.Popup.ChamCong;
using ChamCong365.Popup.ChamCong.CaiDatWifi;
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
        private MainWindow Main;
        BrushConverter bcWifi = new BrushConverter();
        List<Wifi> itemsWifi = new List<Wifi>();
        public ucCaiDatBaoMatWifi(MainWindow main)
        {
            InitializeComponent();
            Main = main;
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

        private void Border_MouseLeftButtonUp_Ip(object sender, MouseButtonEventArgs e)
        {
            ucDanhSachIP ucL = new ucDanhSachIP(Main);
            grLoadListWifiIp.Children.Clear();
            object Content = ucL.Content;
            ucL.Content = null;
            grLoadListWifiIp.Children.Add(Content as UIElement);
            txbTextIP.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            bodTextIP.BorderThickness = new Thickness(0, 0, 0, 5);
            bodTextIP.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            //if (lsvLoadIP.Visibility == Visibility.Collapsed)
            //{
            //    bodTextLableIp.Visibility = Visibility.Visible;
            //    bodTextLabletWifi.Visibility = Visibility.Collapsed;
            //    borAddIp.Visibility = Visibility.Visible;
            //    bodAddWifi.Visibility = Visibility.Collapsed;
            //    lsvLoadIP.Visibility = Visibility.Visible;
            //    lsvLoadWifi.Visibility = Visibility.Collapsed;
            //    txbTextIP.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
            //    txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
            //    bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
            //    bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
            //    bodTextIP.BorderThickness = new Thickness(0, 0, 0, 5);
            //    bodTextIP.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

            //}
            //else
            //{
            //    lsvLoadWifi.Visibility = Visibility.Visible;
            //    lsvLoadIP.Visibility = Visibility.Collapsed;
            //}


        }

        private void Border_MouseLeftButtonUp_Wifi(object sender, MouseButtonEventArgs e)
        {
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
            //bodTextLabletWifi.Visibility = Visibility.Visible;
            //bodTextLableIp.Visibility = Visibility.Collapsed;
            //borAddIp.Visibility = Visibility.Collapsed;
            //bodAddWifi.Visibility = Visibility.Visible;
            //lsvLoadWifi.Visibility= Visibility.Visible;
            //lsvLoadIP.Visibility= Visibility.Collapsed;


        }

        private void borAddIp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemDiaChiIP());
        }

        private void Border_MouseLeftButtonUp_UpdateCollapsed(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi());
        }

        private void bodAddWifi_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiWifi(Main));
        }

        private void Border_MouseLeftButtonUp_UpdateIP(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi());
        }

        private void Border_MouseLeftButtonUp_UpdateWifi(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi());
        }

        private void Border_MouseLeftButtonUp_DeleteIP(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDelete());
        }

        private void Border_MouseLeftButtonUp_DeleteWifi(object sender, MouseButtonEventArgs e)
        {
            
            Main.grShowPopup.Children.Add(new ucDelete());

        }

        private void bodAddWifi_MouseEnter(object sender, MouseEventArgs e)
        {
            //bodAddWifi.BorderThickness = new Thickness(1);
        }

        private void bodAddWifi_MouseLeave(object sender, MouseEventArgs e)
        {
            //bodAddWifi.BorderThickness = new Thickness(0);
        }

        private void borAddIp_MouseEnter(object sender, MouseEventArgs e)
        {
            //borAddIp.BorderThickness = new Thickness(1);
        }

        private void borAddIp_MouseLeave(object sender, MouseEventArgs e)
        {
            //borAddIp.BorderThickness = new Thickness(0);
        }

       
    }
}
