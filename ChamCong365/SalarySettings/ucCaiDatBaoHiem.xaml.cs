using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;
using ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem;
using ChamCong365.Popup.ListTabInsurance;
using ChamCong365.Popup.PopupSalarySettings;

namespace ChamCong365.SalarySettings
{
    /// <summary>
    /// Interaction logic for ucInsuranceSettings.xaml
    /// </summary>
    public partial class ucCaiDatBaoHiem : Page
    {
        BrushConverter bru = new BrushConverter();
        MainWindow Main;
        public ucCaiDatBaoHiem( MainWindow main)
        {
            InitializeComponent();
            Main = main;
            //ucLoaiBaoHiem ucI = new ucLoaiBaoHiem(Main);
            //grLoadFormInsurance.Children.Clear();
            //object Content = ucI.Content;
            //ucI.Content = null;
            //grLoadFormInsurance.Children.Add(Content as UIElement);
            //bodInsurancePolicy.BorderThickness = new Thickness(0, 0, 0, 5);
            //bodInsurancePolicy.BorderBrush = (Brush)bru.ConvertFrom("#4C5BD4");
            //txbTextInsurancePolicy.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");
            //txbListSaffYesSettings.Foreground = (Brush)bru.ConvertFrom("#474747");
            //txbListSaffNotSettings.Foreground = (Brush)bru.ConvertFrom("#474747");
            //bodAddInsurance.Visibility = Visibility.Visible;
            BrushConverter bc = new BrushConverter();
            textChinhSachBaoHiem.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
            lineChinhSachBaoHiem.Visibility = Visibility.Visible;
            textDSNhanSuChuaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuChuaThietLap.Visibility = Visibility.Collapsed;
            textDSNhanSuDaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuDaThietLap.Visibility = Visibility.Collapsed;
            //textDanhSachNghiSaiQD.Foreground = (Brush)bc.ConvertFrom("#666666");
            //lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
            ucLoaiBaoHiem ucI = new ucLoaiBaoHiem(Main);
            pnlHienThi.Children.Clear();
            object Content = ucI.Content;
            ucI.Content = null;
            pnlHienThi.Children.Add(Content as UIElement);
        }

        private void bodInsurancePolicy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ucLoaiBaoHiem ucI = new ucLoaiBaoHiem(Main);
            //grLoadFormInsurance.Children.Clear();
            //object Content = ucI.Content;
            //ucI.Content = null;
            //grLoadFormInsurance.Children.Add(Content as UIElement);
            //bodInsurancePolicy.BorderThickness = new Thickness(0, 0, 0, 5);
            //bodInsurancePolicy.BorderBrush = (Brush)bru.ConvertFrom("#4C5BD4");
            //txbTextInsurancePolicy.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");
            //bodAddInsurance.Visibility = Visibility.Visible;

            //bodListSaffNotSettings.BorderThickness = new Thickness(0, 0, 0, 1);
            //bodListSaffNotSettings.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            //txbListSaffNotSettings.Foreground = (Brush)bru.ConvertFrom("#474747");
             
            //bodListSaffYesSettings.BorderThickness = new Thickness(0, 0, 0, 1);
            //bodListSaffYesSettings.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            //txbListSaffYesSettings.Foreground = (Brush)bru.ConvertFrom("#474747");
        }
    

        private void bodListSaffNotSettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ucDanhSachNhanVienChuaThietLapBH ucI = new ucDanhSachNhanVienChuaThietLapBH(Main);
            //grLoadFormInsurance.Children.Clear();
            //object Content = ucI.Content;
            //ucI.Content = null;
            //grLoadFormInsurance.Children.Add(Content as UIElement);
            //bodListSaffNotSettings.BorderThickness = new Thickness(0, 0, 0, 5);
            //bodListSaffNotSettings.BorderBrush = (Brush)bru.ConvertFrom("#4C5BD4");
            //txbListSaffNotSettings.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");
            //bodAddInsurance.Visibility = Visibility.Collapsed;

            //bodListSaffYesSettings.BorderThickness = new Thickness(0, 0, 0, 1);
            //bodListSaffYesSettings.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            //txbListSaffYesSettings.Foreground = (Brush)bru.ConvertFrom("#474747");

            //bodInsurancePolicy.BorderThickness = new Thickness(0, 0, 0, 1);
            //bodInsurancePolicy.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            //txbTextInsurancePolicy.Foreground = (Brush)bru.ConvertFrom("#474747");
        }

        private void bodListSaffYesSettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ucDanhSachNhanVienDaThietLapBH ucI = new ucDanhSachNhanVienDaThietLapBH(Main);
            //grLoadFormInsurance.Children.Clear();
            //object Content = ucI.Content;
            //ucI.Content = null;
            //grLoadFormInsurance.Children.Add(Content as UIElement);
            //bodListSaffYesSettings.BorderThickness = new Thickness(0, 0, 0, 5);
            //bodListSaffYesSettings.BorderBrush = (Brush)bru.ConvertFrom("#4C5BD4");
            //txbListSaffYesSettings.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");

            //bodListSaffNotSettings.BorderThickness = new Thickness(0, 0, 0, 1);
            //bodListSaffNotSettings.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            //txbListSaffNotSettings.Foreground = (Brush)bru.ConvertFrom("#474747");

            //bodInsurancePolicy.BorderThickness = new Thickness(0, 0, 0, 1);
            //bodInsurancePolicy.BorderBrush = (Brush)bru.ConvertFrom("#DDDDDD");
            //txbTextInsurancePolicy.Foreground = (Brush)bru.ConvertFrom("#474747");

        }

        private void bodAddInsurance_MouseEnter(object sender, MouseEventArgs e)
        {
            //bodAddInsurance.BorderThickness = new Thickness(1);
        }

        private void bodAddInsurance_MouseLeave(object sender, MouseEventArgs e)
        {
           // bodAddInsurance.BorderThickness = new Thickness(0);
        }

        private void bodAddInsurance_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Main.grShowPopup.Children.Add(new ucThemMoiChinhSachBH(Main));
        }

        private void tabChinhSachThue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            textChinhSachBaoHiem.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
            lineChinhSachBaoHiem.Visibility = Visibility.Visible;
            textDSNhanSuChuaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuChuaThietLap.Visibility = Visibility.Collapsed;
            textDSNhanSuDaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuDaThietLap.Visibility = Visibility.Collapsed;
            //textDanhSachNghiSaiQD.Foreground = (Brush)bc.ConvertFrom("#666666");
            //lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
            ucLoaiBaoHiem ucI = new ucLoaiBaoHiem(Main);
            pnlHienThi.Children.Clear();
            object Content = ucI.Content;
            ucI.Content = null;
            pnlHienThi.Children.Add(Content as UIElement);
        }

        private void tabDSNhanSuChuaThietLap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            textChinhSachBaoHiem.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineChinhSachBaoHiem.Visibility = Visibility.Collapsed;
            textDSNhanSuChuaThietLap.Foreground = (Brush)bc.ConvertFrom("#4c5bd4");
            lineDSNhanSuChuaThietLap.Visibility = Visibility.Visible;
            textDSNhanSuDaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuDaThietLap.Visibility = Visibility.Collapsed;
            //textDanhSachNghiSaiQD.Foreground = (Brush)bc.ConvertFrom("#666666");
            //lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
            ucDanhSachNhanVienChuaThietLapBH ucI = new ucDanhSachNhanVienChuaThietLapBH(Main);
            pnlHienThi.Children.Clear();
            object Content = ucI.Content;
            ucI.Content = null;
            pnlHienThi.Children.Add(Content as UIElement);
        }

        private void tabDSNhanSuDaThietLap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucDanhSachNhanVienDaThietLapBH ucI = new ucDanhSachNhanVienDaThietLapBH(Main);
            pnlHienThi.Children.Clear();
            object Content = ucI.Content;
            ucI.Content = null;
            pnlHienThi.Children.Add(Content as UIElement);
            BrushConverter bc = new BrushConverter();
            textChinhSachBaoHiem.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineChinhSachBaoHiem.Visibility = Visibility.Collapsed;
            textDSNhanSuChuaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuChuaThietLap.Visibility = Visibility.Collapsed;
            textDSNhanSuDaThietLap.Foreground = (Brush)bc.ConvertFrom("#4c5bd4");
            lineDSNhanSuDaThietLap.Visibility = Visibility.Visible;
        }
    }
}
