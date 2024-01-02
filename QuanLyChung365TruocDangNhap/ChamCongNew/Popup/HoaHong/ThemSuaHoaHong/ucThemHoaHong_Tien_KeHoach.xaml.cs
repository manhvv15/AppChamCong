using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.HoaHongNhanDuoc;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.clsNhanVienThuocCongTy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolTip;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.ThemSuaHoaHong
{
    /// <summary>
    /// Interaction logic for ucThemHoaHong_Tien_KeHoach.xaml
    /// </summary>
    public partial class ucThemHoaHong_Tien_KeHoach : UserControl
    {
        MainWindow Main;
        ucHoaHongNhanDuoc ucRose;
        BrushConverter br = new BrushConverter();
        public ucThemHoaHong_Tien_KeHoach(MainWindow main, ucHoaHongNhanDuoc ucrose)
        {
            InitializeComponent();
            Main = main;
            this.ucRose = ucrose;
            tb_TieuDeHoaHong.Text = "Thêm mới hoa hồng tiền";
            searchBarThoiGianApDung.SelectedDate = DateTime.Now;
            LoadNhanVien();
        }
        uint KeHoach;
        int DanhGia;
        List<Datum_kh> lstHoaHongKeHoach;
        List<ListThietLap> lstRose5;
        public ucThemHoaHong_Tien_KeHoach(MainWindow main, uint kehoach, List<Datum_kh> lsthoahongkehoach, ucHoaHongNhanDuoc ucrose, List<ListThietLap> lstrose5)
        {
            InitializeComponent();
            Main = main;
            KeHoach = kehoach;
            ucRose = ucrose;
            lstHoaHongKeHoach = lsthoahongkehoach;
            lstRose5 = lstrose5;
            LoadNhanVien();
            LoadKeHoach();
            if (cbo_DanhGia.Text == "Đạt kế hoạch")
            {
                DanhGia = 1;
            }
            else if (cbo_DanhGia.Text == "Không đạt kế hoạch")
            {
                DanhGia = 0;
            }
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();
            tb_TieuDeHoaHong.Text = "Thêm mới hoa hồng kế hoạch";
            stp_ThoiGianApDung_HoaHongTien.Visibility = Visibility.Collapsed;
            stp_SoTien_HoaHongTien.Visibility= Visibility.Collapsed;
            stp_DanhGia_HoaHongKeHoach.Visibility = Visibility.Visible;
            stp_KeHoach_HoaHongKeHoach.Visibility = Visibility.Visible;
            stp_ThangApDung.Visibility = Visibility.Visible;
        }

        Datum_kh Rose;
        public ucThemHoaHong_Tien_KeHoach(MainWindow main, Datum_kh rose, List<Datum_kh> lsthoahongkehoach, ucHoaHongNhanDuoc ucrose)
        {
            InitializeComponent();
            Main = main;
            Rose = rose;
            ucRose = ucrose;
            lstHoaHongKeHoach = lsthoahongkehoach;
            LoadNhanVien();
            LoadKeHoach();
            //foreach (var item in rose.detail)
            //{
                searchBarNhanVien.Text = rose.ro_name_user;
                ID_NhanVien = rose.ro_id_user.ToString();
            //}
            searchBarNhanVien.PlaceHolderForground = "#474747";
            cbo_DanhGia.SelectedIndex = rose.ro_kpi_active;
            //foreach (var item in rose.TinhluongThietLap)
            //{
                cbo_KeHoach.Text = rose.TinhluongThietLap.tl_name;
                ID_KeHoach = rose.TinhluongThietLap.tl_id.ToString();
                cbo_KeHoach.SelectedItem = rose.TinhluongThietLap;
            //}
            textHienThiTGBatDau.Text = rose.ro_time.ToString("yyyy-MM-dd");
            tb_GhiChu.Text = rose.ro_note.ToString();
            if (cbo_DanhGia.Text == "Đạt kế hoạch")
            {
                DanhGia = 1;
            }
            else if (cbo_DanhGia.Text == "Không đạt kế hoạch")
            {
                DanhGia = 0;
            }
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();
            cbo_KeHoach.Foreground = (Brush)br.ConvertFrom("#474747");
            searchBarNhanVien.Foreground = (Brush)br.ConvertFrom("#474747");
            searchBarNhanVien.PlaceHolder = "#474747";
            tb_TieuDeHoaHong.Text = "Chỉnh sửa hoa hồng kế hoạch";
            tb_ThemHoaHongCacLoai.Text = "Sửa hoa hồng";
            stp_ThoiGianApDung_HoaHongTien.Visibility = Visibility.Collapsed;
            stp_SoTien_HoaHongTien.Visibility = Visibility.Collapsed;
            stp_DanhGia_HoaHongKeHoach.Visibility = Visibility.Visible;
            stp_KeHoach_HoaHongKeHoach.Visibility = Visibility.Visible;
            stp_ThangApDung.Visibility = Visibility.Visible;
        }

        int Back;
        List<RoseUser> lstHoaHongTien;
        RoseUser Rose1;
        public ucThemHoaHong_Tien_KeHoach(MainWindow main, RoseUser rose, List<RoseUser> lsthoahongkehoach, ucHoaHongNhanDuoc ucrose, int back)
        {
            InitializeComponent();
            Main = main;
            Rose1 = rose;
            ucRose = ucrose;
            lstHoaHongTien = lsthoahongkehoach;
            Back = back;
            searchBarThoiGianApDung.Part_TextBox.Text = rose.ro_time.ToString("yyyy-MM-dd");
            tb_GhiChu.Text = rose.ro_note.ToString();
            textTienTP.Text = rose.ro_price.ToString();
            tb_TieuDeHoaHong.Text = "Chỉnh sửa hoa hồng tiền";
            tb_ThemHoaHongCacLoai.Text = "Sửa hoa hồng";
            stp_TenNhanVien_Chung.Visibility = Visibility.Collapsed;
            stp_ThoiGianApDung_HoaHongTien.Visibility = Visibility.Visible;
            stp_SoTien_HoaHongTien.Visibility = Visibility.Visible;
            stp_GhiChu_Chung.Visibility = Visibility.Visible;
        }
        #region Popup Lich
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private void borTGBatDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lsvChonThangBatDau.Visibility == Visibility.Collapsed)
            {
                lsvChonThangBatDau.Visibility = Visibility.Visible;
            }

            if (lsvChonThangBatDau.Visibility == Visibility.Visible)
            {
                dteSelectedMonthBD.Visibility = dteSelectedMonthBD.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                lsvChonThangBatDau.ItemsSource = clBD;
                flag = 1;
            }
        }

        private void lsvChonThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lsvChonThangBatDau.Visibility = Visibility.Collapsed;
        }

        int flag = 0;
        private void dteSelectedMonthBD_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            lsvChonThangBatDau.Visibility = Visibility.Collapsed;
            var x = dteSelectedMonthBD.DisplayDate.ToString("MM/yyyy");
            if (flag == 0)
                x = "";
            else
                x = dteSelectedMonthBD.DisplayDate.ToString("MM/yyyy");
            if (textHienThiTGBatDau != null && !string.IsNullOrEmpty(x))
            {
                textHienThiTGBatDau.Text = x;
                DateTime a = DateTime.Parse(x);
            }
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonthBD.DisplayDate != null && flag > 0)
            {
                dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }

        Calendar dteSelectedMonthBD { get; set; }
       
        private List<Calendar> _clBD;

        public List<Calendar> clBD
        {
            get { return _clBD; }
            set
            {
                _clBD = value; OnPropertyChanged();
            }
        }

        #endregion
        List<TinhluongThietLap> lstThietLap = new List<TinhluongThietLap>();

        List<ListThietLap> lstRose = new List<ListThietLap>();
        public void LoadKeHoach()
        {
            try
            {
                if (lstRose5 != null)
                {
                    foreach (var item in lstRose5)
                    {
                        if (item.tl_id_rose == 5)
                        {
                            lstRose.Add(item);
                        }
                    }
                    cbo_KeHoach.ItemsSource = lstRose;
                }
                else
                {
                    cbo_KeHoach.Text = "Đang tải...";
                }
                
            }
            catch (Exception)
            {
            }
            
        }
        string ID_KeHoach;
        private void ChonKeHoach(object sender, SelectionChangedEventArgs e)
        {
            if (cbo_KeHoach.SelectedItem != null)
            {
                cbo_KeHoach.Foreground = (Brush)br.ConvertFrom("#474747");
                var Tl_Id = ((ListThietLap)cbo_KeHoach.SelectedItem).tl_id.ToString();
                ID_KeHoach = Tl_Id;
            }
            else
            {
                cbo_KeHoach.Foreground = (Brush)br.ConvertFrom("#ACACAC");
            }
        }
        public void LoadNhanVien()
        {
            searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        string ID_NhanVien;
        private void ChonNhanVien(object sender, SelectionChangedEventArgs e)
        {
            if (searchBarNhanVien.SelectedItem != null)
            {
                searchBarNhanVien.PlaceHolderForground = "#474747";
                var chonca = ((ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                ID_NhanVien = chonca;
            }
            else
            {
                searchBarNhanVien.PlaceHolderForground = "#ACACAC";
            }
        }
        private void textTienTP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                tb_validateSoTien.Visibility = Visibility.Visible;
                tb_validateSoTien.Text = "Hãy nhập đúng định dạng số tiền!";
            }
            else
            {
                tb_validateSoTien.Visibility = Visibility.Collapsed;
            }
        }

        private void ChonDanhGia(object sender, SelectionChangedEventArgs e)
        {
            if (cbo_DanhGia.SelectedItem != null)
            {
                cbo_DanhGia.Foreground = (Brush)br.ConvertFrom("#474747");
                if (cbo_DanhGia.SelectedIndex == 0)
                {
                    DanhGia = 1;
                }
                else
                {
                    DanhGia = 0;
                }
            }
            else
            {
                searchBarNhanVien.PlaceHolderForground = "#ACACAC";
            }
        }
        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        private  void tb_Thoat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private async void btn_ThemHoaHongCacLoai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (stp_TenNhanVien_Chung.Visibility == Visibility.Visible)
            {
                if (searchBarNhanVien.SelectedIndex == 0 || searchBarNhanVien.Text == null || searchBarNhanVien.Text == "")
                {
                    tb_ValidateTenNhanVien.Visibility = Visibility.Visible;
                    tb_ValidateTenNhanVien.Text = "Bạn vui lòng nhập tên nhân viên";
                    allow = false;
                }
                else
                {
                    tb_ValidateTenNhanVien.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_ThoiGianApDung_HoaHongTien.Visibility == Visibility.Visible)
            {
                if (searchBarThoiGianApDung.Part_TextBox.Text == "")
                {
                    tb_ValidateTime.Visibility = Visibility.Visible;
                    tb_ValidateTime.Text = "Bạn vui lòng chọn thời gian áp dụng";
                    allow = false;
                }
                else
                {
                        tb_ValidateTime.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_DanhGia_HoaHongKeHoach.Visibility == Visibility.Visible)
            {
                if (cbo_DanhGia.SelectedItem == null || cbo_DanhGia.Text == null)
                {
                    tb_ValidateDanhGia.Visibility = Visibility.Visible;
                    tb_ValidateDanhGia.Text = "Bạn vui lòng chọn trạng thái kế hoạch";
                    allow = false;
                }
                else
                {
                    tb_ValidateDanhGia.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_KeHoach_HoaHongKeHoach.Visibility == Visibility.Visible)
            {
                if ( cbo_KeHoach.SelectedItem == null || cbo_KeHoach.Text == null)
                {
                    tb_ValidateKeHoach.Visibility = Visibility.Visible;
                    tb_ValidateKeHoach.Text = "Bạn vui lòng chọn kế hoạch";
                    allow = false;
                }
                else
                {
                    tb_ValidateKeHoach.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_ThangApDung.Visibility == Visibility.Visible)
            {
                if (textHienThiTGBatDau.Text == "---- --- ----" || textHienThiTGBatDau.Text == null)
                {
                    tb_ValidateThangApDung.Visibility = Visibility.Visible;
                    tb_ValidateThangApDung.Text = "Bạn vui lòng chọn tháng áp dụng";
                    allow = false;
                }
                else
                {
                    tb_ValidateThangApDung.Visibility= Visibility.Collapsed;
                }
            }
            if (stp_SoTien_HoaHongTien.Visibility == Visibility.Visible)
            {
                if (textTienTP.Text == "" || string.IsNullOrEmpty(textTienTP.Text))
                {
                    tb_validateSoTien.Visibility = Visibility.Visible;
                    tb_validateSoTien.Text = "Bạn vui lòng nhập số tiền";
                    allow = false;
                }
                else
                {
                    tb_validateSoTien.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_GhiChu_Chung.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(tb_GhiChu.Text) || tb_GhiChu.Text == "")
                {
                    tb_validateGhiChu.Visibility = Visibility.Visible;
                    tb_validateGhiChu.Text = "Bạn vui lòng nhập ghi chú cho hoa hồng";
                    allow = false;
                }
                else
                {
                    tb_validateGhiChu.Visibility= Visibility.Collapsed;
                }
            }
            if (allow)
            {
                if (tb_TieuDeHoaHong.Text == "Thêm mới hoa hồng tiền")
                {
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3009/api/tinhluong/congty/insert_rose");
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(ID_NhanVien), "ro_id_user");
                        content.Add(new StringContent(Main.IdAcount.ToString()), "ro_id_com");
                        content.Add(new StringContent("1"), "ro_id_lr");
                        content.Add(new StringContent( $"{searchBarThoiGianApDung.SelectedDate.Value.Year}-{searchBarThoiGianApDung.SelectedDate.Value.Month}-{searchBarThoiGianApDung.SelectedDate.Value.Day}"), "ro_time");
                        content.Add(new StringContent(tb_GhiChu.Text), "ro_note");
                        content.Add(new StringContent(textTienTP.Text), "ro_price");
                        content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                        {
                            var resConten = await response.Content.ReadAsStringAsync();
                            ucRose.LoadListRoseSaff();
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                    catch (Exception)
                    {}
                }
                else if (tb_TieuDeHoaHong.Text == "Chỉnh sửa hoa hồng tiền")
                {
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/edit_rose_chung");
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(Rose.ro_id.ToString()), "ro_id");
                        if (searchBarThoiGianApDung.SelectedDate != null)
                        {
                            content.Add(new StringContent($"{searchBarThoiGianApDung.SelectedDate.Value.Year}-{searchBarThoiGianApDung.SelectedDate.Value.Month}-{searchBarThoiGianApDung.SelectedDate.Value.Day}"), "ro_time");

                        }
                        else
                        {
                            content.Add(new StringContent(searchBarThoiGianApDung.Part_TextBox.Text), "ro_time");

                        }
                        content.Add(new StringContent(tb_GhiChu.Text), "ro_note");
                        content.Add(new StringContent(textTienTP.Text), "ro_price");
                        content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                        {
                            var resConten = await response.Content.ReadAsStringAsync();
                            ucRose.LoadListRoseSaff();
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                    catch (Exception)
                    { }
                }
                else if (tb_TieuDeHoaHong.Text == "Thêm mới hoa hồng kế hoạch")
                {
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/add_hh_kh");
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(Main.IdAcount.ToString()), "cp");
                        content.Add(new StringContent(ID_KeHoach), "ro_id_tl");
                        content.Add(new StringContent(ID_NhanVien), "ro_id_user");
                        content.Add(new StringContent(DanhGia.ToString()), "ro_kpi_active");
                        content.Add(new StringContent(tb_GhiChu.Text), "ro_note");
                        string[] partsStart = textHienThiTGBatDau.Text.Split('/');
                        if (partsStart.Length == 2) { content.Add(new StringContent($"{partsStart[1]}/{partsStart[0]}/{DateTime.Now.Day}"), "ro_time"); }
                        content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                        {
                            var rescontent = await response.Content.ReadAsStringAsync();
                            ucRose.LoadHoaHongKeHoach();
                            this.Visibility = Visibility.Collapsed;
                        }
                       
                    }
                    catch (Exception)
                    {
                    }
                }
                else if (tb_TieuDeHoaHong.Text == "Chỉnh sửa hoa hồng kế hoạch")
                {
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/edit_hh_kh");
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(Main.IdAcount.ToString()), "cp");
                        content.Add(new StringContent(ID_KeHoach), "ro_id_tl");
                        content.Add(new StringContent(Rose.ro_id.ToString()), "ro_id");
                        content.Add(new StringContent(ID_NhanVien), "ro_id_user");
                        content.Add(new StringContent(DanhGia.ToString()), "ro_kpi_active");
                        content.Add(new StringContent(tb_GhiChu.Text), "ro_note");
                        if (lsvChonThangBatDau.SelectedItem != null)
                        {
                            string[] partsStart = textHienThiTGBatDau.Text.Split('/');
                            if (partsStart.Length == 2) { content.Add(new StringContent($"{partsStart[1]}/{partsStart[0]}/{DateTime.Now.Day}"), "ro_time"); }
                        }
                        else
                        {
                            content.Add(new StringContent(textHienThiTGBatDau.Text), "ro_time");
                        }
                        content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                        {
                            var rescontent = await response.Content.ReadAsStringAsync();
                            ucRose.LoadHoaHongKeHoach();
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                    catch (Exception)
                    {}
                }
            }
        }

        
    }
}
