using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.TinhLuong.Function;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.Tax;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.TinhLuong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ThuongPhat;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager.ShiftWorkPopups;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.CaiDatHoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.DiMuonVeSom;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.HoaHong;
//using DocumentFormat.OpenXml.Wordprocessing;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Common
{
    /// <summary>
    /// Interaction logic for ucThongBaoAll.xaml
    /// </summary>
    public partial class ucThongBaoAll : UserControl
    {
        private int countdownValue = 3;
        private DispatcherTimer countdownTimer;
        public event EventHandler CountdownFinished;
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        #region Ca Làm việc
        private Shift Shift;
        ucCreateShiftWork CreateShiftWork;
        public ucThongBaoAll(MainWindow main, Shift shift)
        {
            InitializeComponent();
            Main = main;
            Shift = shift;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Sửa ca làm việc";
            tb_ThongBao.Text = shift.shift_name;
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
            tb_HauToThongBao.Text = "thành công";
        }

        public ucThongBaoAll(MainWindow main, ucCreateShiftWork createshiftwork)
        {
            InitializeComponent();
            Main = main;
            CreateShiftWork = createshiftwork;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Thêm ca làm việc";
            tb_ThongBao.Text = createshiftwork.txtShiftName.Text;
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
            tb_HauToThongBao.Text = "thành công";
        }

        public ucThongBaoAll(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Xóa thành công";
            tb_TextThongBaoAll.Margin = new Thickness(40,0,40,0); 
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
        }

        List_CamXuc CamXuc;
        public ucThongBaoAll(List_CamXuc camXuc)
        {
            InitializeComponent();
            CamXuc = camXuc;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Collapsed;
            icon_ThatBai.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Thang điểm cảm xúc không được vượt quá 7";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
        }

        public ucThongBaoAll(MainWindow main ,List_CamXuc camXuc)
        {
            InitializeComponent();
            CamXuc = camXuc;
            Main = main;    
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            icon_ThatBai.Visibility = Visibility.Collapsed;
            tb_ThongBao.Text = "Cập nhật thang điểm cảm xúc thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
        }

        double OnOff;
        public ucThongBaoAll(MainWindow main, double onoff)
        {
            InitializeComponent();
            OnOff = onoff;
            Main = main;
            StartCountdown();
            if (onoff == 1)
            {
                icon_ThanhCong.Visibility = Visibility.Visible;
                icon_ThatBai.Visibility = Visibility.Collapsed;
                tb_ThongBao.Text = "Kích hoạt chế độ cảm xúc thành công";
                tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
            }
            else if(onoff == 2)
            {
                icon_ThanhCong.Visibility = Visibility.Visible;
                icon_ThatBai.Visibility = Visibility.Collapsed;
                tb_ThongBao.Text = "Tắt chế độ cảm xúc thành công";
                tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
            }
            else if(onoff == 3)
            {
                icon_ThanhCong.Visibility = Visibility.Visible;
                icon_ThatBai.Visibility = Visibility.Collapsed;
                tb_ThongBao.Text = "Cập nhật điểm chuẩn cảm xúc thành công";
                tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
            }
            else if (onoff == 4)
            {
                icon_ThanhCong.Visibility = Visibility.Visible;
                icon_ThatBai.Visibility = Visibility.Collapsed;
                tb_ThongBao.Text = "Xóa thang điểm cảm xúc thành công";
                tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
            }
            else
            {
                icon_ThanhCong.Visibility = Visibility.Visible;
                icon_ThatBai.Visibility = Visibility.Collapsed;
                tb_ThongBao.Text = "Thêm mới thang điểm cảm xúc thành công";
                tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
            }
            
        }

        ucCaiDatHoaHong ucCaiDatHH;
        public ucThongBaoAll(MainWindow main, ucCaiDatHoaHong uccaidathh)
        {
            InitializeComponent(); 
            Main = main;
            ucCaiDatHH = uccaidathh;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            icon_ThatBai.Visibility = Visibility.Collapsed;
            tb_ThongBao.Text = "Thêm mới hoa hồng thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
        }
        ucCaiDatHoaHongDoanhThu ucDoanhThu;
        public ucThongBaoAll(MainWindow main, ucCaiDatHoaHongDoanhThu ucdoanhthu)
        {
            InitializeComponent();
            Main = main;
            ucDoanhThu = ucdoanhthu;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            icon_ThatBai.Visibility = Visibility.Collapsed;
            tb_ThongBao.Text = "Sửa hoa hồng doanh thu thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
        }

        ucCaiDatHoaHongLoiNhuan ucLoiNhuan;
        public ucThongBaoAll(MainWindow main, ucCaiDatHoaHongLoiNhuan ucloinhuan)
        {
            InitializeComponent();
            Main = main;
            ucLoiNhuan = ucloinhuan;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            icon_ThatBai.Visibility = Visibility.Collapsed;
            tb_ThongBao.Text = "Sửa hoa hồng Lợi nhuận thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
        }

        ucCaiDatHoaHongLePhiViTri ucViTri;
        public ucThongBaoAll(MainWindow main, ucCaiDatHoaHongLePhiViTri ucvitri)
        {
            InitializeComponent();
            Main = main;
            ucViTri = ucvitri;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            icon_ThatBai.Visibility = Visibility.Collapsed;
            tb_ThongBao.Text = "Sửa hoa hồng lệ phí vị trí thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
        }

        ucCaiDatHoaHongKeHoach ucKeHoach;
        public ucThongBaoAll(MainWindow main, ucCaiDatHoaHongKeHoach uckehoach)
        {
            InitializeComponent();
            Main = main;
            ucKeHoach = uckehoach;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            icon_ThatBai.Visibility = Visibility.Collapsed;
            tb_ThongBao.Text = "Sửa hoa hồng kế hoạch thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#20744A");
        }
        #endregion

        #region Thuế
        string TenCST;
        public ucThongBaoAll(MainWindow main, string tenCST)
        {
            InitializeComponent();
            Main = main;
            TenCST = tenCST;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Thêm chính sách thuế";
            tb_ThongBao.Text = tenCST;
            tb_HauToThongBao.Text = "thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
        }

        OOP.CaiDatLuong.Tax.clsTax.TaxListDetail clsThue;
        public ucThongBaoAll(MainWindow main, OOP.CaiDatLuong.Tax.clsTax.TaxListDetail clsthue)
        {
            InitializeComponent();
            Main = main;
            clsThue = clsthue;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Sửa chính sách thuế";
            tb_ThongBao.Text = clsthue.cl_name;
            tb_HauToThongBao.Text = "thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
        }

        bool CheckCountNv; string NameUS;
        public ucThongBaoAll(MainWindow main,bool checkcountnv, string nameus)
        {
            InitializeComponent();
            Main = main;
            CheckCountNv = checkcountnv;
            NameUS = nameus;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Thêm Thành công nhân viên";
            tb_HauToThongBao.Text = $"vào chính sách thuế";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
        }

        PopUpDanhSachNVTrongThue PopupDSNVInTax;
        public ucThongBaoAll(PopUpDanhSachNVTrongThue DSNVInTax)
        {
            InitializeComponent();
            PopupDSNVInTax = DSNVInTax;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Sửa Thành công nhân viên";
            tb_HauToThongBao.Text = $"trong chính sách thuế";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
        }

        clsNSuChuaTL.ListUserFinal lstNSCTL;
        public ucThongBaoAll(clsNSuChuaTL.ListUserFinal lstnsctl)
        {
            InitializeComponent();
            lstNSCTL = lstnsctl;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Thiết lập thuế cho nhân viên";
            tb_ThongBao.Text = lstnsctl.userName;
            tb_HauToThongBao.Text = $"thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
        }

        OOP.CaiDatLuong.Tax.clsNSDaTL.ListU lstUs;
        public ucThongBaoAll(OOP.CaiDatLuong.Tax.clsNSDaTL.ListU lstus)
        {
            InitializeComponent();
            lstUs = lstus;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Chỉnh sửa thuế cho nhân viên";
            tb_ThongBao.Text = lstus.userName;
            tb_HauToThongBao.Text = $"thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
        }

        frmThuongPhat ucTP;
        public ucThongBaoAll(MainWindow main, frmThuongPhat uctp)
        {
            InitializeComponent();
            ucTP = uctp;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Xuất file excel danh sách thưởng phạt thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
        }

        ucCaiDatLuongCoBan LCB;
        public ucThongBaoAll(MainWindow main, ucCaiDatLuongCoBan lcb)
        {
            InitializeComponent();
            LCB = lcb;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Xuất file excel danh sách lương cơ bản thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
        }
        List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTL;
        public ucThongBaoAll(MainWindow main, frmThuongPhat uctp, List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstchuatl)
        {
            InitializeComponent();
            lstChuaTL = lstchuatl;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Dữ liệu thưởng phạt không có bản ghi nào";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }

        List<ListResult_CDL> lstLCB;
        public ucThongBaoAll(MainWindow main, ucCaiDatLuongCoBan uclcb, List<ListResult_CDL> lstlcb)
        {
            InitializeComponent();
            lstLCB = lstlcb;
            LCB = uclcb;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Dữ liệu lương cơ bản không có bản ghi nào";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }

        frmDanhSachNghiSaiQuyDinh NSQD;
        public ucThongBaoAll(MainWindow main, frmDanhSachNghiSaiQuyDinh nsqd)
        {
            InitializeComponent();
            NSQD = nsqd;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Xuất file excel danh sách nghỉ sai quy định thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#4080FF");
        }

        List<List_NSQD> lstNSQD;
        public ucThongBaoAll(MainWindow main, frmDanhSachNghiSaiQuyDinh nsqd, List<List_NSQD> lstnsqd)
        {
            InitializeComponent();
            lstNSQD = lstnsqd;
            NSQD = nsqd;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Dữ liệu nghỉ sai quy định không có bản ghi nào";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }
        PopUpHoiTruocKhiXoaCaiDatThue Pop;
        string Notication;
        public ucThongBaoAll(MainWindow main ,string notication, PopUpHoiTruocKhiXoaCaiDatThue pop)
        {
            InitializeComponent();
            Main = main;
            Pop = pop;
            Notication = notication;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_ThongBao.Text = notication;
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }

        List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> dataFinals;
        public ucThongBaoAll(MainWindow main, List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> datafinals, string idnv)
        {
            InitializeComponent();
            Main = main;
            dataFinals = datafinals;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Thêm thưởng phạt cho nhân viên ";
            foreach (var item in datafinals)
            {
                if (item.inforUser.idQLC.ToString() == idnv)
                {
                    tb_ThongBao.Text = item.inforUser.userName;
                }
            }
            tb_HauToThongBao.Text = " thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }

        PopUpThemMoiThuongPhat FrmTP;
        public ucThongBaoAll(MainWindow main, PopUpThemMoiThuongPhat frmtp, string name_saff)
        {
            InitializeComponent();
            FrmTP = frmtp;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Thêm mới phạt công cho";
            tb_ThongBao.Text = name_saff;
            tb_HauToThongBao.Text = "Thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }

      
        public ucThongBaoAll(MainWindow main, PopUpThemMoiThuongPhat frmtp, string name_saff, string notication)
        {
            InitializeComponent();
            FrmTP = frmtp;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Visible;
            tb_TienToThongBao.Text = "Chỉnh sửa phạt công cho";
            tb_ThongBao.Text = name_saff;
            tb_HauToThongBao.Text = "Thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }
        #endregion

        string ThongBao;
        public ucThongBaoAll(string thongbao, char OK)
        {
            InitializeComponent();
            ThongBao = thongbao;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Collapsed;
            icon_ThatBai.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Xóa thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }

        MainChamCong Maincc;
        public ucThongBaoAll(MainChamCong maincc, ucXemBangLuong ucxbl)
        {
            InitializeComponent();
            Maincc = maincc;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Collapsed;
            icon_ThatBai.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Xuất bảng lương cá nhân thành công";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }

        public ucThongBaoAll(MainChamCong maincc, ucXemBangLuong ucxbl, Data_LuongNV lnv)
        {
            InitializeComponent();
            Maincc = maincc;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Collapsed;
            icon_ThatBai.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Bảng lương không có dữ liệu";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }
        // Thông báo thất bại
        string ErrorSytem;
        public ucThongBaoAll(string errorSytem)
        {
            InitializeComponent();
            ErrorSytem = errorSytem;
            StartCountdown();
            icon_ThanhCong.Visibility = Visibility.Collapsed;
            icon_ThatBai.Visibility = Visibility.Visible;
            tb_ThongBao.Text = "Lỗi hệ thống, bạn vui lòng thử lại sau ít phút";
            tb_ThongBao.Foreground = (Brush)br.ConvertFrom("#FF5B4D"); 
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
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
