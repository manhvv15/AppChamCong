using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using System.Windows.Media.Imaging;
using System.Globalization;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ChamCong365.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for ucHoSoNhanVien.xaml
    /// </summary>
    /// 
    public class LuongNhanVien
    {
        public int luongcoban { get; set; }
        public int luongdongbaohiem { get; set; }
        public int phucapbaohiem { get; set; }
        public int tanggiamluong { get; set; }
        public DateTime thoigianapdung { get; set; }
    }

    public class HopDong
    {
        public string hopdongnhanvien { get; set; }
        public DateTime ngaythuchien { get; set; }
        public DateTime ngayhethan { get; set; }
        public string luong { get; set; }
        public int tepdinhkem { get; set; }
       
    }
    public partial class ucHoSoNhanVien : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        List<LuongNhanVien> luongNhanVien = new List<LuongNhanVien>();
        List<HopDong> hopdong = new List<HopDong>();
        public List<clsLuongBaoHiem.DataSalary> lstLuongBH = new List<clsLuongBaoHiem.DataSalary>();
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        private clsLuongCoBan.ListResult clsLuongCB = new clsLuongCoBan.ListResult();
        private HoSoNV.InfoEmpStart EpStar = new HoSoNV.InfoEmpStart();
        private string IdNV = "";
        public ucHoSoNhanVien(MainWindow main,clsLuongCoBan.ListResult cls)
        {
            InitializeComponent();
            
            try
            {
                Main = main;
                textTenNV.Text = cls.ep_name;
                textChucVuNV.Text = cls.infoposition;
                textHoVaTen.Text = cls.ep_name;
                textDiaChi.Text = cls.ep_address;
                textSDT.Text = cls.ep_phone;
                textChucVu.Text = cls.infoposition;
                textMaNhanVien.Text = cls.ep_id.ToString();
                textNhapHoTen.Text = cls.ep_name;
                textSoDT.Text = cls.ep_phone;
                textNgayBatDauLam.Text = cls.ngay_bat_dau_lv;

                textDiaChi2.Text = cls.ep_address;
                textEmail2.Text = cls.ep_email;
                if (cls.ep_avatar != null)
                {
                    Uri DuongDan = new Uri(cls.ep_avatar);
                    BitmapImage bitmap = new BitmapImage(DuongDan);
                    ImgAvatar.ImageSource = bitmap;
                }
                if (cls.infordepartment != null)
                {
                    textPhongBan.Text = cls.infordepartment.dep_name;
                }
                textNgayBatDauLam.Text = DateTimeOffset.FromUnixTimeSeconds(int.Parse(cls.ngay_bat_dau_lv)).ToString();
                txbNhapNgayBatDau.Text = DateTimeOffset.FromUnixTimeSeconds(int.Parse(cls.ngay_bat_dau_lv)).ToString();
                textEmail.Text = cls.ep_email;
                using (WebClient web = new WebClient())
                {
                    web.QueryString.Add("token", Properties.Settings.Default.Token);
                    web.QueryString.Add("ep_id", cls.ep_id.ToString());
                    web.QueryString.Add("cp", Main.IdAcount.ToString());
                    web.UploadValuesCompleted += (s, e) =>
                    {

                        try
                        {
                            string x = UnicodeEncoding.UTF8.GetString(e.Result);
                            HoSoNV.Root api = JsonConvert.DeserializeObject<HoSoNV.Root>(x);
                            if (api.data.info_emp_start != null)
                            {
                                EpStar = api.data.info_emp_start;
                                txbNhapNgayTinhLuong.Text = EpStar.st_create.ToString();

                            }
                        }
                        catch { }
                    };
                    web.UploadValuesTaskAsync("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan",
                        web.QueryString);
                }
                foreach (var item in main.lstNhanVienThuocCongTy)
                {
                    if (item.idQLC == cls.ep_id)
                    {
                        if (item.inForPerson.account.birthday != null)
                        {
                            textNgaySinh.Text = DateTimeOffset.FromUnixTimeSeconds(int.Parse(item.inForPerson.account.birthday.ToString())).ToString();
                            dtpNgaySinh.Text = DateTimeOffset.FromUnixTimeSeconds(int.Parse(item.inForPerson.account.birthday.ToString())).ToString();

                        }
                        if (item.inForPerson.account.married == 2)
                        {
                            cboHonNhan.Text = "Đã kết hôn";
                        }
                        else
                        {
                            cboHonNhan.Text = "Độc thân";
                        }
                        if (item.inForPerson.account.gender == 1)
                        {
                            textGioiTinh.Text = "Nam";
                            cboGioiTinh.Text = "Nam";
                        }
                        else if (item.inForPerson.account.gender == 2)
                        {
                            textGioiTinh.Text = "Nữ";
                            cboGioiTinh.Text = "Nữ";
                        }
                    }
                }
                clsLuongCB = cls;

                LoadDLLuongBH();
                getData();
            }
            catch
            {

            }
            
        }
        private ChiTietNV _ChiTietNV;
        public ChiTietNV ChiTietNV
        {
            get { return _ChiTietNV; }
            set
            {
                _ChiTietNV = value;
                OnPropertyChanged();
            }
        }
        private void getData()
        {
            try
            {
                this.Dispatcher.Invoke(() =>
                {
                    using (WebClient web = new WebClient())
                    {
                        web.QueryString.Add("token", Properties.Settings.Default.TokenTL);
                        web.QueryString.Add("id_comp", Main.IdAcount.ToString());
                        web.QueryString.Add("id", textMaNhanVien.Text);
                        //if (Main.MainType == 0)
                        //{
                        //    web.QueryString.Add("token", Main.CurrentCompany.token);
                        //    web.QueryString.Add("id_comp", Main.CurrentCompany.com_id);
                        //    web.QueryString.Add("id", user_id);
                        //}

                        web.UploadValuesCompleted += (s, e) =>
                        {
                            try
                            {
                                string x = UnicodeEncoding.UTF8.GetString(e.Result);
                                API_ChiTietNV api = JsonConvert.DeserializeObject<API_ChiTietNV>(x);
                                if (api.data != null)
                                {
                                    ChiTietNV = api.data.list;
                                    txbHienThiGioiThieu.Text = ChiTietNV.ep_description;
                                    txbChinhSuaGioiThieu.Text = ChiTietNV.ep_description;

                                }
                            }
                            catch { }
                        };
                        web.UploadValuesTaskAsync("https://tinhluong.timviec365.vn/api_app/company/profile_ep.php",
                            web.QueryString);
                    }
                });
            }
            catch
            {
            }
        }
        public void LoadDLLuongBH()
        {
            lstLuongBH = new List<clsLuongBaoHiem.DataSalary>();
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_salary_em")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                request.AddParameter("ep_id", textMaNhanVien.Text);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                clsLuongBaoHiem.Root luongBH = JsonConvert.DeserializeObject<clsLuongBaoHiem.Root>(b);
                if (luongBH.data.data_salary != null)
                {
                    foreach (var item in luongBH.data.data_salary)
                    {
                        lstLuongBH.Add(item);
                    }
                    dgDanhSachLuong.ItemsSource = lstLuongBH;
                }
            }

        }
        private void bodSuaLuongCoBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clsLuongBaoHiem.DataSalary cls = (sender as Border).DataContext as clsLuongBaoHiem.DataSalary;
            if (cls != null)
            {
                Main.grShowPopup.Children.Add(new ucThemLuongCoBan(Main, clsLuongCB, this, cls));
            }
           
        }

        private void bodXoaLuongCoBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clsLuongBaoHiem.DataSalary cls = (sender as Border).DataContext as clsLuongBaoHiem.DataSalary;
            if (cls != null)
            {
                Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaLCB(Main, cls, this));
            }
        }
        private void ChinhSuaGioiThieu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           stpChinhSuaGioiThieu.Visibility  = Visibility.Visible;
            bodHienThiGioiThieu.Visibility = Visibility.Collapsed;
            
        }

        private void bodHuyChinhSua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            stpChinhSuaGioiThieu.Visibility = Visibility.Collapsed;
            bodHienThiGioiThieu.Visibility = Visibility.Visible;
        }

        private void bodHuyChinhSua_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuyChinhSua.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbHuyChinhSua.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodHuyChinhSua_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuyChinhSua.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbHuyChinhSua.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodChinhSuaThongTinNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           grHienThiThongTinNhanVien.Visibility = Visibility.Collapsed;
           stpChinhSuaThongTinNhanVien.Visibility = Visibility.Visible;
        }

        private void bodHuyNhapThongTin_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            grHienThiThongTinNhanVien.Visibility = Visibility.Visible;
            stpChinhSuaThongTinNhanVien.Visibility = Visibility.Collapsed;

        }

        private void bodHuyNhapThongTin_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuyNhapThongTin.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbHuyNhapThongTin.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodHuyNhapThongTin_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuyNhapThongTin.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbHuyNhapThongTin.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodLuuThongTinNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            grHienThiThongTinNhanVien.Visibility = Visibility.Visible;
            stpChinhSuaThongTinNhanVien.Visibility = Visibility.Collapsed;
            //txbHoTen.Text = txbNhapHoTen.Text;
            //txbGioiTinh.Text = txbNhapGioiTinh.Text;
            //txbNgaySinh.Text = txbNhapNgaySinh.Text;
            //txbMaNhanVien.Text = txbNhapMaNhanVien.Text;
            //txbNhapTinhTrangHonNhan.Text = txbNhapTinhTrangHonNhan.Text;
            //txbDiaChi.Text = txbNhapDiaChi.Text;
            //txbSDT.Text = txbNhapSoDienThoai.Text;
            //txbHienThiEmail.Text = txbNhapEmail.Text;
            //txbNgayBatDauLam.Text = txbNhapNgayBatDau.Text;
            //txbNgayBatDauTinhLuong.Text = txbNhapNgayTinhLuong.Text;
            //txbNganHangNhanLuong.Text = txbNhapNganHang.Text;
            //txbSoTaiKhoanNhanLuong.Text = txbNhapSoTaiKhoan.Text;
        }

        private void bodLuuThongTinNhanVien_MouseEnter(object sender, MouseEventArgs e)
        {
            bodLuuThongTinNhanVien.BorderThickness = new Thickness(2);
        }

        private void bodLuuThongTinNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            bodLuuThongTinNhanVien.BorderThickness = new Thickness(0);
        }

        private void bodThemLuongCoBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemLuongCoBan(Main, clsLuongCB, this));
        }

        private void bodThemHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemHopDongNhanVien());
        }

        private void bodbodSuaHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucChinhSuaHopDong(Main));
        }

        private void dgDanhSachLuong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void dgDanhSachHopDong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnLuuTT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            using (WebClient web = new WebClient())
            {
                web.QueryString.Add("token", Properties.Settings.Default.TokenTL);
                web.QueryString.Add("id_comp", Main.IdAcount.ToString());
                web.Headers.Add("Authorization", Properties.Settings.Default.TokenTL);

                web.QueryString.Add("ep_name", ChiTietNV.ep_name);
                web.QueryString.Add("ep_phone", ChiTietNV.ep_phone);
                web.QueryString.Add("ep_address", ChiTietNV.ep_address);
                web.QueryString.Add("id_ep", ChiTietNV.ep_id);
                web.QueryString.Add("description", txbChinhSuaGioiThieu.Text);
                web.UploadValuesCompleted += (s, ee) =>
                {
                    txbHienThiGioiThieu.Text = txbChinhSuaGioiThieu.Text;
                    stpChinhSuaGioiThieu.Visibility = Visibility.Collapsed;
                    bodHienThiGioiThieu.Visibility = Visibility.Visible;
                };
                web.UploadValuesTaskAsync("https://chamcong.24hpay.vn/service/update_user_info_employee.php",
                    web.QueryString);
            }
        }
    }
}
