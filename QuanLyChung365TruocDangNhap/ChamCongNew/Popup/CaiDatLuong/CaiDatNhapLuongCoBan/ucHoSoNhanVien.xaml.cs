using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using System.Windows.Media;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using System.Windows.Media.Imaging;
using System.Globalization;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net.Http;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong;
using System.CodeDom;
using System.Text.RegularExpressions;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
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
        //private OOP.clsNhanVienThuocCongTy.ListUser clsLuongCB = new OOP.clsNhanVienThuocCongTy.ListUser();
        private ListResult_CDL clsLuongCB = new ListResult_CDL();
        private HoSoNV.InfoEmpStart EpStar = new HoSoNV.InfoEmpStart();
        private string IdNV = "";
        //OOP.clsNhanVienThuocCongTy.ListUser
        private ListResult_CDL Cls;
        public ucHoSoNhanVien(MainWindow main, ListResult_CDL cls)
        {
            InitializeComponent();
            
            try
            {
                Main = main;
                Cls = cls;
                LoadLuongAndBaoHiem();
                getData();
                txbHoTen.Text = cls.userName;
                txbMaNhanVien.Text = cls.ep_id.ToString();
                textTenNV.Text = cls.userName;
                textNhapHoTen.Text = cls.userName;
                txbNhapMaNhanVien.Text = cls.ep_id.ToString();
                foreach (var item in Main.lstNhanVienThuocCongTy)
                {
                    if (cls.ep_id == item.idQLC)
                    {
                        if(item.pos != null && !string.IsNullOrEmpty(cls.positionName)) { textChucVuNV.Text = txbChucVu.Text = cls.positionName; } else { textChucVuNV.Text = txbChucVu.Text = "Chưa cập nhật"; }
                        if (!string.IsNullOrEmpty(item.address)) { txbDiaChi.Text = item.address; } else { txbDiaChi.Text = "Chưa cập nhật"; }
                        if (!string.IsNullOrEmpty(item.phone)) { txbSDT.Text = item.phone; } else { txbSDT.Text = "Chưa cập nhật"; }
                        if (!string.IsNullOrEmpty(item.email)) { txbHienThiEmail.Text = item.email; } else { txbHienThiEmail.Text = "Chưa cập nhật"; }
                        if (!string.IsNullOrEmpty(item.address)) { textDiaChi2.Text = item.address; } else { textDiaChi2.Text = "Chưa cập nhật"; }
                        if (!string.IsNullOrEmpty(item.phone)) { textSoDT.Text = item.phone; } else { textSoDT.Text = "Chưa cập nhật"; }
                        if (!string.IsNullOrEmpty(item.email)) { textEmail2.Text = item.email; } else { textEmail2.Text = "Chưa cập nhật"; }
                        if (!string.IsNullOrEmpty(cls.avatarUser))
                        {
                            Uri DuongDan = new Uri(cls.avatarUser);
                            BitmapImage bitmap = new BitmapImage(DuongDan);
                            ImgAvatar.ImageSource = bitmap;
                        }
                        else
                        {
                            Uri DuongDan = new Uri("icon_avt.png");
                            BitmapImage bitmap = new BitmapImage(DuongDan);
                            ImgAvatar.ImageSource = bitmap;
                        }
                        if (cls.organizeDetailName != null)
                        {
                            txbPhongBan.Text = cls.organizeDetailName;
                        }
                        else
                        {
                            txbPhongBan.Text = "Chưa cập nhập";
                        }
                        if (long.TryParse(item.inForPerson.employee.start_working_time, out long createdTime))
                        {
                            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                            dateTime = dateTime.AddSeconds(createdTime).ToLocalTime();
                            item.inForPerson.employee.start_working_time = dateTime.ToString("dd/MM/yyyy");
                            dtpNgayBatDau.SelectedDate = dateTime;
                        }
                        else
                        {
                            throw new ArgumentException("Định dạng thời gian không hợp lệ");
                        }
                        txbNgayBatDauLam.Text = item.inForPerson.employee.start_working_time;
                    }
                }
                

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
                                textNganHang.Text = EpStar.st_bank.ToString();
                                txbNganHangNhanLuong.Text = EpStar.st_bank.ToString();
                                txbSoTaiKhoanNhanLuong.Text = EpStar.st_stk.ToString();

                            }
                        }
                        catch { }
                    };
                    web.UploadValuesTaskAsync("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan",
                        web.QueryString);
                }
                //if (cls.inForPerson.account.birthday != null)
                //{
                //    string format = "dd/MM/yyyy";
                //    DateTime parsedDate;
                //    if (long.TryParse(cls.inForPerson.account.birthday, out long createdTime1) && !DateTime.TryParseExact(cls.inForPerson.account.birthday, format, System.Globalization.CultureInfo.InvariantCulture, System.Globalization.DateTimeStyles.None, out parsedDate))
                //    {
                //        DateTime dateTime1 = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                //        dateTime1 = dateTime1.AddSeconds(createdTime1).ToLocalTime();
                //        cls.inForPerson.account.birthday = dateTime1.ToString("dd/MM/yyyy");
                //        txbNgaySinh.Text = cls.inForPerson.account.birthday;
                //        dtpNgaySinh.Text = cls.inForPerson.account.birthday;
                //    }
                //    else
                //    {
                //        txbNgaySinh.Text = cls.inForPerson.account.birthday;
                //        dtpNgaySinh.Text = cls.inForPerson.account.birthday;
                //    }

                //}
                //if (cls.inForPerson.account.married == 2)
                //{
                //    cboHonNhan.Text = "Đã kết hôn";
                //}
                //else
                //{
                //    cboHonNhan.Text = "Độc thân";
                //}
                //if (cls.inForPerson.account.gender == 1)
                //{
                //    txbGioiTinh.Text = "Nam";
                //    cboGioiTinh.Text = "Nam";
                //}
                //else if (cls.inForPerson.account.gender == 2)
                //{
                //    txbGioiTinh.Text = "Nữ";
                //    cboGioiTinh.Text = "Nữ";
                //}
                //else if (cls.inForPerson.account.gender == 0)
                //{
                //    txbGioiTinh.Text = "Khác";
                //    cboGioiTinh.Text = "Khác";
                //}
                clsLuongCB = cls;
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
                        web.QueryString.Add("id", txbMaNhanVien.Text);
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
        private List<clsLuongBaoHiem.DataSalary> _lstLuongNv;
        public List<clsLuongBaoHiem.DataSalary> lstLuongNv
        {
            get { return _lstLuongNv; }
            set { _lstLuongNv = value; OnPropertyChanged();}
        }
        private List<clsLuongBaoHiem.DataContract> _lstHopDongNv;
        public List<clsLuongBaoHiem.DataContract> lstHopDongNv
        {
            get { return _lstHopDongNv; }
            set { _lstHopDongNv = value; OnPropertyChanged(); }
        }
        public async void LoadLuongAndBaoHiem()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3009/api/tinhluong/congty/take_salary_em");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Cls.ep_id.ToString()), "ep_id");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();

                clsLuongBaoHiem.Root luongAndHopDong = JsonConvert.DeserializeObject<clsLuongBaoHiem.Root>(resContent);
                if (luongAndHopDong.data != null)
                {
                    if (luongAndHopDong.data.data_salary != null)
                    {
                        lstLuongNv = luongAndHopDong.data.data_salary;
                        for (int i = 0; i < lstLuongNv.Count; i++)
                        {
                            lstLuongNv[i].sb_time_up_date = $"{lstLuongNv[i].sb_time_up.Day}-{lstLuongNv[i].sb_time_up.Month}-{lstLuongNv[i].sb_time_up.Year}";
                            if(i < lstLuongNv.Count-1)
                            {
                                if(lstLuongNv[i].sb_salary_basic - lstLuongNv[i+1].sb_salary_basic <= 0)
                                {
                                    lstLuongNv[i].isTang = 0;
                                }
                                else
                                {
                                    lstLuongNv[i].isTang = 1;
                                }
                                lstLuongNv[i].salary_tang_giam = (lstLuongNv[i].sb_salary_basic - lstLuongNv[i + 1].sb_salary_basic).ToString("N0") + " VNĐ";
                            }
                            else
                            {
                                lstLuongNv[i].salary_tang_giam = "0 VNĐ";
                                lstLuongNv[i].isTang = 0;
                            }
                        }
                        dgDanhSachLuong.ItemsSource = lstLuongNv;
                    }
                    if (luongAndHopDong.data.data_contract != null)
                    {
                        lstHopDongNv = luongAndHopDong.data.data_contract;
                        foreach (var item in lstHopDongNv)
                        {
                            item.con_time_up_date = $"{item.con_time_up.Day}-{item.con_time_up.Month}-{item.con_time_up.Year}";
                            item.con_time_end_date = $"{item.con_time_end.Day}-{item.con_time_end.Month}-{item.con_time_end.Year}";
                            item.con_time_end_date = "-";
                        }
                        dgDanhSachHopDong.ItemsSource = lstHopDongNv;
                    }
                }

            }
            catch (Exception)
            {}
        }
        public void LoadDLLuongBH()
        {
            lstLuongBH = new List<clsLuongBaoHiem.DataSalary>();
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_salary_em")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                request.AddParameter("ep_id", txbMaNhanVien.Text);
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
                    //dgDanhSachLuong.ItemsSource = lstLuongBH;
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

        private void bodXoaHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        { 
            clsLuongBaoHiem.DataContract cls = (sender as Border).DataContext as clsLuongBaoHiem.DataContract;
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
            
            using(WebClient web = new WebClient())
            {
                bool allow = true;
                string email = textEmail2.Text;
                if (string.IsNullOrEmpty(textNhapHoTen.Text))
                {
                    allow = false;
                    validateName.Visibility= Visibility.Visible;
                }
                else
                {
                    validateName.Visibility= Visibility.Collapsed;
                }
                if (string.IsNullOrEmpty(textDiaChi2.Text))
                {
                    allow = false;
                    validateAddress.Visibility= Visibility.Visible;
                }
                else
                {
                    validateAddress.Visibility = Visibility.Collapsed;
                }
                if (dtpNgaySinh.SelectedDate == null)
                {
                    allow = false;
                    validateBirthDay.Visibility= Visibility.Visible;
                }
                else
                {
                    validateBirthDay.Visibility = Visibility.Collapsed;
                }
                if (cboGioiTinh.SelectedIndex < 0)
                {
                    allow = false;
                    validateGender.Visibility= Visibility.Visible;
                }
                else
                {
                    validateGender.Visibility = Visibility.Collapsed;
                }
                if (cboHonNhan.SelectedIndex < 0)
                {
                    allow = false;
                    validateMarried.Visibility= Visibility.Visible;
                }
                else
                {
                    validateMarried.Visibility = Visibility.Collapsed;
                }
                if (string.IsNullOrEmpty(textSoDT.Text))
                {
                    allow = false;
                    validatePhone.Visibility= Visibility.Visible;
                }
                else
                {
                    validatePhone.Visibility = Visibility.Collapsed;
                }
                if (dtpNgayBatDau.SelectedDate == null)
                {
                    allow = false;
                    validateStartWorking.Visibility= Visibility.Visible;
                }
                else
                {
                    validateStartWorking.Visibility = Visibility.Collapsed;
                }
                if (!string.IsNullOrWhiteSpace(email))
                {
                    Regex regex = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
                    if (!regex.IsMatch(email))
                    {
                        allow = false;
                        validateEmail.Visibility = Visibility.Visible;
                        validateEmail.Text = "Email trống hoặc không đúng định dạng!";
                    }
                }
                else
                {
                    validateEmail.Visibility = Visibility.Collapsed;
                }
                if (allow)
                {
                    web.QueryString.Add("userName", textNhapHoTen.Text);
                    web.QueryString.Add("idQLC", txbMaNhanVien.Text);
                    web.QueryString.Add("address", textDiaChi2.Text);
                    web.QueryString.Add("email", textEmail2.Text);
                    var check2 = ((long)dtpNgaySinh.SelectedDate.Value.Subtract(new DateTime(1970, 1, 1,0,0,0)).TotalMilliseconds).ToString();
                    web.QueryString.Add("birthday", ((long)dtpNgaySinh.SelectedDate.Value.Subtract(new DateTime(1970, 1, 1,0,0,0)).TotalMilliseconds).ToString());
                    web.QueryString.Add("gender", cboGioiTinh.SelectedIndex.ToString());
                    web.QueryString.Add("married", (cboHonNhan.SelectedIndex+1).ToString());
                    web.QueryString.Add("phone", textSoDT.Text);
                    if(!string.IsNullOrEmpty(textNganHang.Text))
                        web.QueryString.Add("st_bank", textNganHang.Text);
                    if(!string.IsNullOrEmpty(textTaiKhoanNH.Text))
                        web.QueryString.Add("st_stk", textTaiKhoanNH.Text);
                    var check1 = ((long)dtpNgayBatDau.SelectedDate.Value.Subtract(new DateTime(1970, 1, 1, 0, 0, 0)).TotalMilliseconds).ToString();
                    web.QueryString.Add("startWorkingTime", ((long)dtpNgayBatDau.SelectedDate.Value.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds).ToString());
                    web.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    web.UploadValuesAsync(new Uri("https://api.timviec365.vn/api/qlc/employee/updateInfoEmployeeComp"), "POST", web.QueryString);
                    web.UploadValuesCompleted += (s1, e1) =>
                    {
                        try
                        {
                            var check = UTF8Encoding.UTF8.GetString(e1.Result);
                            if (UTF8Encoding.UTF8.GetString(e1.Result).Contains("true"))
                            {
                                grHienThiThongTinNhanVien.Visibility = Visibility.Visible;
                                stpChinhSuaThongTinNhanVien.Visibility = Visibility.Collapsed;
                                txbHoTen.Text = textNhapHoTen.Text;
                                txbGioiTinh.Text = cboGioiTinh.SelectedValue.ToString();
                                txbNgaySinh.Text = dtpNgaySinh.SelectedDate.Value.ToString("dd/MM/yyyy");
                                txbDiaChi.Text = textDiaChi2.Text;
                                txbSDT.Text = textSoDT.Text;
                                txbHienThiEmail.Text = textEmail2.Text;
                                txbNgayBatDauLam.Text = dtpNgayBatDau.SelectedDate.Value.ToString("dd/MM/yyyy");
                                txbNganHangNhanLuong.Text = textNganHang.Text;
                                txbSoTaiKhoanNhanLuong.Text = textTaiKhoanNH.Text;
                            }
                        }
                        catch { }
                    };
                }
                
            }
            
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
            Main.grShowPopup.Children.Add(new ucThemLuongCoBan(Main, Cls, this));
        }

        private void bodThemHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Main.grShowPopup.Children.Add(new ucThemHopDongNhanVien(Main, Cls, this));
        }

        private void bodbodSuaHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            clsLuongBaoHiem.DataContract hd = (sender as Border).DataContext as clsLuongBaoHiem.DataContract;
            if (hd != null)
            {
                Main.grShowPopup.Children.Add(new ucThemHopDongNhanVien(Main, clsLuongCB, this, hd));
            }
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
