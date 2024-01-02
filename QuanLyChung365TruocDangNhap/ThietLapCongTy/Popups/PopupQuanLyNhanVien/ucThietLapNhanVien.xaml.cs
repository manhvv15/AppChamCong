using QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupQuanLyNhanVien;
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
using static QRCoder.PayloadGenerator;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Them_Xoa_NhanVien
{
    /// <summary>
    /// Interaction logic for ucThietLapNhanVien.xaml
    /// </summary>
    public partial class ucThietLapNhanVien : UserControl
    {
        BrushConverter br = new BrushConverter();
        frmMain Main;
        ucTatCaNhanVien ucTatCaNhanVien;
        int ep_id = 0;
        string ep_name;
        public ucThietLapNhanVien()
        {
            InitializeComponent();
        }
        public ucThietLapNhanVien(frmMain Main, ucTatCaNhanVien ucTatCaNhanVien, int ep_id, string ep_name)
        {
            InitializeComponent();
            this.Main = Main;
            this.ucTatCaNhanVien = ucTatCaNhanVien;
            this.ep_id = ep_id;
            this.ep_name = ep_name;
        }

        private void Rectangel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitCreateWifi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        bool Validate()
        {
            txtValidateQuitJobDate.Visibility = Visibility.Collapsed;
            txtValidateQuitJobDate.Visibility = Visibility.Collapsed;
            if (QuitJobDate.SelectedDate == null)
            {
                txtValidateQuitJobDate.Text = "Ngày nghỉ việc không được để trống!";
                txtValidateQuitJobDate.Visibility = Visibility.Visible;
                return false;
            }
            if (cbxQuitJobType.SelectedIndex == -1)
            {
                txtvalidateQuitJobType.Text = "Loại nghỉ việc không được để trống!";
                txtvalidateQuitJobType.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }

        private void bodThietLapXoaNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Validate())
            {
                int typeQuitJob = 0;
                if (cbxQuitJobType.SelectedIndex == 0) typeQuitJob = 1;
                if (cbxQuitJobType.SelectedIndex == 1) typeQuitJob = 2;
                Main.pnlShowPopUp.Children.Add(new ucXoaNhanVien(ucTatCaNhanVien, ep_id, ep_name, (DateTime)QuitJobDate.SelectedDate, typeQuitJob));
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
