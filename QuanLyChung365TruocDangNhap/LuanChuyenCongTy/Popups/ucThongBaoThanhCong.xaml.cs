using QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan.Entities;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace QuanLyChung365TruocDangNhap.LuanChuyenCongTy.Popups
{
    /// <summary>
    /// Interaction logic for ucThongBaoThanhCong.xaml
    /// </summary>
    public partial class ucThongBaoThanhCong : UserControl
    {
        string TenNhanVien;
        private int countdownValue = 3;
        private DispatcherTimer countdownTimer;
        public event EventHandler CountdownFinished;
        BrushConverter br = new BrushConverter();
        frmMain Main;
        List_NhanVien lstLuanChuyen = new List_NhanVien();
        public ucThongBaoThanhCong(frmMain main, List_NhanVien lstluanlhuyen)
        {
            InitializeComponent();
            Main = main;
            lstLuanChuyen = lstluanlhuyen;
            tb_TenNhanVien.Text = lstluanlhuyen.userName;
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0, 20, 0, 0);
            StartCountdown();
        }
        Data_Company inforCom = new Data_Company();
        Data_InforSaft inforSaft = new Data_InforSaft();
        public ucThongBaoThanhCong(frmMain main, Data_Company inforcom, Data_InforSaft inforSaft)
        {
            InitializeComponent();
            Main = main;
            inforCom = inforcom;
            this.inforSaft = inforSaft;
            tb_TienToThongBao.Text = "Đổi mật khẩu tài khoản";
            tb_HauToThongBao.Text = "thành công";
            if (main.Type == 1)
            { 
                tb_TenNhanVien.Text = inforcom.com_name;
            }
            else
            {
                tb_TenNhanVien.Text = inforSaft.userName;
            }
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0, 20, 0, 0);
            StartCountdown(); 
        }

        public ucThongBaoThanhCong()
        {
            InitializeComponent();
            tb_HauToThongBao.Text = "mật khẩu nhập lại không trùng khớp";
            tb_TenNhanVien.Text = "hoặc";
            tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#20744A");
            tb_TienToThongBao.Text = "Mật hiện tại khẩu không đúng ";
            StartCountdown();
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0, 20, 0, 0);
        }

        public ucThongBaoThanhCong(Data_InforSaft inforsaft)
        {
            InitializeComponent();
            inforSaft = inforsaft;
            tb_HauToThongBao.Text = " thành công";
            tb_TenNhanVien.Text = inforsaft.userName;
            tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
            tb_TienToThongBao.Text = "Chỉnh sửa tài khoản ";
            StartCountdown();
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0, 20, 0, 0);
        }

        public ucThongBaoThanhCong(Data_Company inforcom)
        {
            InitializeComponent();
            inforCom = inforcom;
            tb_HauToThongBao.Text = " thành công";
            tb_TenNhanVien.Text = inforcom.com_name;
            tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
            tb_TienToThongBao.Text = "Chỉnh sửa tài khoản công ty ";
            StartCountdown();
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0, 20, 0, 0);
        }

        string DanhGia;
        public ucThongBaoThanhCong(string danhgia)
        {
            InitializeComponent();
            DanhGia = danhgia;
            tb_TienToThongBao.Text = "Đánh giá thành công. ";
            tb_TenNhanVien.Text = "Cảm ơn bạn đã đánh giá về phần mềm của chúng tôi";
            tb_HauToThongBao.Text = "";
            tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#20744A");
            StartCountdown();
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0, 20, 0, 0);
        }
        byte[] imageBytes;
        public ucThongBaoThanhCong(byte[] image)
        {
            InitializeComponent();
            imageBytes = image;
            tb_TienToThongBao.Text = "Báo lỗi thành công. ";
            tb_TenNhanVien.Text = "Cảm ơn bạn đã đóng góp ý kiến cho phần mềm của chúng tôi";
            tb_HauToThongBao.Text = "";
            tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#20744A");
            StartCountdown();
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0, 20, 0, 0);
        }

        decimal Error;
        public ucThongBaoThanhCong(decimal error)
        {
            InitializeComponent();
            Error = error;
            tb_TienToThongBao.Text = "Hệ thống lỗi! ";
            tb_TenNhanVien.Text = "Bạn vui lòng quay lại đánh giá sau";
            tb_HauToThongBao.Text = "";
            tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#20744A");
            StartCountdown();
            VerticalAlignment = VerticalAlignment.Top;
            HorizontalAlignment = HorizontalAlignment.Center;
            Margin = new Thickness(0, 20, 0, 0);
        }
        private void StartCountdown()
        {
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();

        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            //tb_TenNhanVien.Text = countdownValue.ToString();
            progressBar.Value = countdownValue;

            countdownValue--;
           
            if (countdownValue < 0)
            {
                countdownTimer.Stop();
                this.Visibility = Visibility.Collapsed;
                CountdownFinished?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Hyperlink_RequestNavigate(object sender, System.Windows.Navigation.RequestNavigateEventArgs e)
        {
            try
            {
                // Mở URL được chỉ định khi hyperlink được nhấp
                Process.Start(new ProcessStartInfo(e.Uri.AbsoluteUri));
                e.Handled = true;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
