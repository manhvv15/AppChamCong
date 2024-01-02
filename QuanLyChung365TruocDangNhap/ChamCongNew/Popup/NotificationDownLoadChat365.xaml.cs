using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup
{
    /// <summary>
    /// Interaction logic for NotificationDownLoadChat365.xaml
    /// </summary>
    public partial class NotificationDownLoadChat365 : UserControl
    {
        ChamCongBangQR uc { get; set; }
        public NotificationDownLoadChat365(ChamCongBangQR uc)
        {
            InitializeComponent();
            this.uc = uc;
        }

        private void Run_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://apps.apple.com/us/app/chat365-nh%E1%BA%AFn-tin-nhanh-ch%C3%B3ng/id1623353330");
            Process.Start("https://play.google.com/store/apps/details?id=vn.timviec365.chat_365&hl=en_US&pli=1");
            uc.popup.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            uc.popup.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            uc.popup.Visibility = Visibility.Collapsed;
        }
    }
}
