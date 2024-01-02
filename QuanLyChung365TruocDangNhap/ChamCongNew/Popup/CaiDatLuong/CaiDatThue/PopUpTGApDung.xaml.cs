using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.Tax;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.CaiDatThue;
using Newtonsoft.Json;
using NPOI.SS.Formula.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpTGApDung.xaml
    /// </summary>
    public partial class PopUpTGApDung : UserControl
    {
        MainWindow Main;
        public static int static_month, static_year;
        int month, year;
        BrushConverter bc = new BrushConverter();
        private string Month = "";
        private string IdNV = "";
        private string AvataUS = "";
        private string NameUS = "";
        private int IdThue = 0;
        private List<OOP.clsNhanVienThuocCongTy.ListUser> LstUs;
        private clsStaffInTax.ListUserInTax LstInTax = new clsStaffInTax.ListUserInTax();
        List<clsStaffInTax.ListUserInTax> lstNVEdit = new List<clsStaffInTax.ListUserInTax>();
        private clsNSuChuaTL.ListUserFinal lstNSCTL = new clsNSuChuaTL.ListUserFinal();
        List<clsNSuChuaTL.ListUserFinal> listNSCTL = new List<clsNSuChuaTL.ListUserFinal>();
        PopUpAddStaffInTax PopUpThemNVCST;
        PopUpDanhSachNVTrongThue PopupDSNVInTax;
        BrushConverter br = new BrushConverter();
        public PopUpTGApDung(MainWindow main,PopUpAddStaffInTax pop,string Idnv,int IdT, string name, string avatar, List<OOP.clsNhanVienThuocCongTy.ListUser> lstUs)
        {
            InitializeComponent();
            Main = main;
            PopUpThemNVCST = pop;
            IdNV = Idnv;
            NameUS = name;
            AvataUS = avatar;
            IdThue = IdT;
            LstUs = lstUs;
            lsvThongTinThueNV.Visibility = Visibility.Collapsed;
            lsvThongTinEditNV.Visibility = Visibility.Collapsed;
            lsvThongTinEditThueNV.Visibility = Visibility.Collapsed;
            lsvThongTinNVTGAD.Visibility = Visibility.Visible;
            LoadThongTinNVAdd();
            bodExitNextSaff.Text = "Thời gian áp dụng";
            tb_NameButton.Text = "Lưu";
            tb_KhongBatBuoc.Text = "(Không bắt buộc)";
            tb_KhongBatBuoc.Foreground = (Brush)br.ConvertFrom("#474747");
            bodSaveTime.HorizontalAlignment = HorizontalAlignment.Center;
            bodSaveTime.Margin = new Thickness(250,0,0,20);
            tb_Huy.Margin = new Thickness(30, 0, 30, 0);
            #region Popup Lịch
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();

            dteSelectedMonthKT = new Calendar();
            dteSelectedMonthKT.Visibility = Visibility.Collapsed;
            dteSelectedMonthKT.DisplayMode = CalendarMode.Year;
            dteSelectedMonthKT.MouseLeftButtonDown += borTGKetThuc_MouseLeftButtonUp;
            dteSelectedMonthKT.DisplayModeChanged += dteSelectedMonthKT_DisplayModeChanged;
            clKT = new List<Calendar>();
            clKT.Add(dteSelectedMonthKT);
            clKT = clKT.ToList();
            #endregion
        }

        public PopUpTGApDung(MainWindow main, clsNSuChuaTL.ListUserFinal lstnsctl)
        {
            InitializeComponent();
            Main = main;
            lstNSCTL = lstnsctl;
            bodExitNextSaff.Text = "Thuế nhân viên";
            tb_NameButton.Text = "Lưu thuế";
            tb_KhongBatBuoc.Text = "(Không bắt buộc)";
            tb_KhongBatBuoc.Foreground = (Brush)br.ConvertFrom("#474747");
            tb_Huy.Margin = new Thickness(30, 0, 30, 0);
            bodHuy.Margin = new Thickness(200, 20, 20, 20);
            lsvThongTinThueNV.Visibility = Visibility.Visible;
            lsvThongTinEditNV.Visibility = Visibility.Collapsed;
            lsvThongTinNVTGAD.Visibility = Visibility.Collapsed;
            lsvThongTinEditThueNV.Visibility = Visibility.Collapsed;
            LoadThongTinNVThietLap();
            LoadCSThue();
            bodHuy.Visibility = Visibility.Visible;
            stp_LoaiThue.Visibility = Visibility.Visible;
            stp_TienThue.Visibility = Visibility.Collapsed;
            #region Popup Lịch
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();

            dteSelectedMonthKT = new Calendar();
            dteSelectedMonthKT.Visibility = Visibility.Collapsed;
            dteSelectedMonthKT.DisplayMode = CalendarMode.Year;
            dteSelectedMonthKT.MouseLeftButtonDown += borTGKetThuc_MouseLeftButtonUp;
            dteSelectedMonthKT.DisplayModeChanged += dteSelectedMonthKT_DisplayModeChanged;
            clKT = new List<Calendar>();
            clKT.Add(dteSelectedMonthKT);
            clKT = clKT.ToList();
            #endregion
        }

        private string Cls_Day;
        private string Cls_Day_End;
        public PopUpTGApDung(MainWindow main, PopUpDanhSachNVTrongThue dsnvintax, clsStaffInTax.ListUserInTax lstInTax)
        {
            InitializeComponent();
            Main = main;
            this.DataContext = this;
            LstInTax = lstInTax;
            this.PopupDSNVInTax = dsnvintax;
            Cls_Day = lstInTax.cls_day_format;
            Cls_Day_End = lstInTax.cls_day_end_format;
            lsvThongTinThueNV.Visibility = Visibility.Collapsed;
            lsvThongTinEditNV.Visibility = Visibility.Visible;
            lsvThongTinNVTGAD.Visibility = Visibility.Collapsed;
            lsvThongTinEditThueNV.Visibility = Visibility.Collapsed;
            bodSaveTime.Margin = new Thickness(240, 0, 0, 20);
            LoadThongTinNVEdit();
            bodExitNextSaff.Text = "Chỉnh sửa nhân viên";
            tb_NameButton.Text = "Cập nhật";
            tb_KhongBatBuoc.Text = "(Không bắt buộc)";
            tb_KhongBatBuoc.Foreground = (Brush)br.ConvertFrom("#474747");
            stp_TienThue.Visibility = Visibility.Collapsed;
            textHienThiTGBatDau.Text = $"{lstInTax.cls_day.Month}/{lstInTax.cls_day.Year}";
            textHienThiTGKetThuc.Text = $"{lstInTax.cls_day_end.Month}/{lstInTax.cls_day_end.Year}";
            #region Popup Lịch
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();

            dteSelectedMonthKT = new Calendar();
            dteSelectedMonthKT.Visibility = Visibility.Collapsed;
            dteSelectedMonthKT.DisplayMode = CalendarMode.Year;
            dteSelectedMonthKT.MouseLeftButtonDown += borTGKetThuc_MouseLeftButtonUp;
            dteSelectedMonthKT.DisplayModeChanged += dteSelectedMonthKT_DisplayModeChanged;
            clKT = new List<Calendar>();
            clKT.Add(dteSelectedMonthKT);
            clKT = clKT.ToList();
            #endregion
        }

        private OOP.CaiDatLuong.Tax.clsNSDaTL.ListU lstUs = new OOP.CaiDatLuong.Tax.clsNSDaTL.ListU();
        List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU> ListUs = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
        private frmDanhSachNhanSuDaThietLap frmNSDTL;
        public PopUpTGApDung(MainWindow main, OOP.CaiDatLuong.Tax.clsNSDaTL.ListU lstus, frmDanhSachNhanSuDaThietLap frmnsdtl)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            lstUs = lstus;
            frmNSDTL = frmnsdtl;
            textHienThiTGBatDau.Text = lstus.cls_day_format; /*$"{lstus.cls_day.Month}/{lstus.cls_day.Year}"*/;
            textHienThiTGKetThuc.Text = lstus.cls_day_end_format; /*$"{lstus.cls_day_end.Month}/{lstus.cls_day_end.Year}"*/;
            foreach (var item in lstus.TinhluongListClass)
            {
                tb_ShowLoaiThue.Text = item.cl_name;
            }
            tb_KhongBatBuoc.Text = " (không bắt buộc)";
            tb_KhongBatBuoc.Foreground = (Brush)br.ConvertFrom("#474747");
            lsvThongTinThueNV.Visibility = Visibility.Collapsed;
            lsvThongTinEditNV.Visibility = Visibility.Collapsed;
            lsvThongTinNVTGAD.Visibility = Visibility.Collapsed;
            lsvThongTinEditThueNV.Visibility = Visibility.Visible;
            LoadThongTinNVSuaThue();
            bodExitNextSaff.Text = "Chỉnh sửa thuế nhân viên";
            tb_NameButton.Text = "Cập nhật thuế";
            bodHuy.Visibility = Visibility.Visible;
            stp_ShowLoaiThue.Visibility = Visibility.Visible;
            stp_TienThue.Visibility = Visibility.Collapsed;
            tb_Huy.Margin = new Thickness(50, 0, 50, 0);
            #region Popup Lịch
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();

            dteSelectedMonthKT = new Calendar();
            dteSelectedMonthKT.Visibility = Visibility.Collapsed;
            dteSelectedMonthKT.DisplayMode = CalendarMode.Year;
            dteSelectedMonthKT.MouseLeftButtonDown += borTGKetThuc_MouseLeftButtonUp;
            dteSelectedMonthKT.DisplayModeChanged += dteSelectedMonthKT_DisplayModeChanged;
            clKT = new List<Calendar>();
            clKT.Add(dteSelectedMonthKT);
            clKT = clKT.ToList();
            #endregion
        }
        List<OOP.CaiDatLuong.Tax.clsTax.TaxListDetail> listDetails = new List<clsTax.TaxListDetail>();
        public async void LoadCSThue()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_tax_com");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();

                OOP.CaiDatLuong.Tax.clsTax.Root tax = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsTax.Root>(resContent);

                if (tax.tax_list_detail != null)
                {
                    listDetails = tax.tax_list_detail;
                    cboLoaiThue.ItemsSource = listDetails;
                }

            }
            catch (Exception)
            {}
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

        private void borTGKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lsvChonThangKetThuc.Visibility == Visibility.Collapsed)
            {
                lsvChonThangKetThuc.Visibility = Visibility.Visible;
            }

            if (lsvChonThangKetThuc.Visibility == Visibility.Visible)
            {
                dteSelectedMonthKT.Visibility = dteSelectedMonthKT.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                lsvChonThangKetThuc.ItemsSource = clKT;
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
                //dpEnd.SelectedDate = a;
            }
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonthBD.DisplayDate != null && flag > 0)
            {
                dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }

        private void dteSelectedMonthKT_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            lsvChonThangBatDau.Visibility = Visibility.Collapsed;
            var x = dteSelectedMonthKT.DisplayDate.ToString("MM/yyyy");
            if (flag == 0)
                x = "";
            else
                x = dteSelectedMonthKT.DisplayDate.ToString("MM/yyyy");
            if (textHienThiTGKetThuc != null && !string.IsNullOrEmpty(x))
            {
                textHienThiTGKetThuc.Text = x;
                DateTime a = DateTime.Parse(x);
                //dpEnd.SelectedDate = a;
            }
            dteSelectedMonthKT.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonthKT.DisplayDate != null && flag > 0)
            {
                dteSelectedMonthKT.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }
        Calendar dteSelectedMonthBD { get; set; }
        Calendar dteSelectedMonthKT { get; set; }

        private List<Calendar> _clBD;

        public List<Calendar> clBD
        {
            get { return _clBD; }
            set
            {
                _clBD = value; OnPropertyChanged();
            }
        }

        private List<Calendar> _clKT;

        public List<Calendar> clKT
        {
            get { return _clKT; }
            set
            {
                _clKT = value; OnPropertyChanged();
            }
        }
        #endregion
       
        public void LoadThongTinNVAdd()
        {
            if (LstUs != null)
            {
                lsvThongTinNVTGAD.ItemsSource = LstUs;
                lsvThongTinNVTGAD.Items.Refresh();
            }
        }

        public void LoadThongTinNVEdit()
        {
            lstNVEdit.Add(LstInTax);
            lsvThongTinEditNV.ItemsSource = lstNVEdit;
        }

        public void LoadThongTinNVThietLap()
        {
            listNSCTL.Add(lstNSCTL);
            lsvThongTinThueNV.ItemsSource = listNSCTL;
        }

        public void LoadThongTinNVSuaThue()
        {
            ListUs.Add(lstUs);
            lsvThongTinEditThueNV.ItemsSource = ListUs;
        }
        int months,years;
        bool CheckCountNv = true;
        string ErrorSytem;
        List<string> lstNhanVienAD = new List<string>();
        private async void bodSaveTime_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (string.IsNullOrEmpty(textHienThiTGBatDau.Text) || textHienThiTGBatDau.Text == "---- --- ----")
            {
                tb_ValidateThoiGianApDung.Visibility = Visibility.Visible;
                tb_ValidateThoiGianApDung.Text = "Bạn vui lòng chọn tháng áp dụng!";
                allow = false;
            }
            else
            {
                tb_ValidateThoiGianApDung.Visibility = Visibility.Hidden;
            }
            if (lstNSCTL != null)
            {
                allow = true;
            }
            else
            {
                if (string.IsNullOrEmpty(textHienThiTGKetThuc.Text) || textHienThiTGKetThuc.Text == "---- --- ----")
                {
                    tb_ValidateThoiGianKetThuc.Visibility = Visibility.Visible;
                    tb_ValidateThoiGianKetThuc.Text = "Bạn vui lòng chọn tháng kết thúc!";
                    allow = false;
                }
                else
                {
                    tb_ValidateThoiGianKetThuc.Visibility = Visibility.Hidden;
                }
            }
            if (allow)
            {
                if (bodExitNextSaff.Text == "Thời gian áp dụng")
                {
                    if (string.IsNullOrEmpty(textNhapTienBH.Text) || textNhapTienBH.Text == "")
                    {
                        tb_ValidateTienBaoHiem.Visibility = Visibility.Visible;
                        tb_ValidateTienBaoHiem.Text = "Bạn vui lòng nhập tiền Thuế!";
                        allow = false;
                    }
                    else
                    {
                        tb_ValidateTienBaoHiem.Visibility = Visibility.Hidden;
                    }
                    if (allow)
                    {
                        try
                        {
                            foreach (var item in LstUs)
                            {
                                var client = new HttpClient();
                                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/them_nv_nhom_other_money");
                                var content = new MultipartFormDataContent();
                                content.Add(new StringContent(IdThue.ToString()), "cls_id_cl");
                                content.Add(new StringContent(Main.IdAcount.ToString()), "cls_id_com");
                                string[] partsStart = textHienThiTGBatDau.Text.Split('/');
                                if (partsStart.Length == 2) { content.Add(new StringContent($"{partsStart[1]}/{partsStart[0]}"), "cls_day"); }
                                string[] partsEnd = textHienThiTGKetThuc.Text.Split('/');
                                if (partsEnd.Length == 2) { content.Add(new StringContent($"{partsEnd[1]}/{partsEnd[0]}"), "cls_day_end");}
                                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                                content.Add(new StringContent(item.idQLC.ToString()), "cls_id_user");
                                content.Add(new StringContent(textNhapTienBH.Text), "x");
                                request.Content = content;
                                var response = await client.SendAsync(request);
                                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                                {
                                    var resContent = await response.Content.ReadAsStringAsync();
                                    this.Visibility = Visibility.Collapsed;
                                    PopUpThemNVCST.Visibility = Visibility.Collapsed;
                                    foreach (var us in LstUs)
                                    {
                                        NameUS = us.userName;
                                    }
                                }
                            }
                            Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, CheckCountNv, NameUS));
                        }
                        catch (Exception)
                        {
                            ErrorSytem = "error";
                            Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                        }
                    } 
                }
                else if (bodExitNextSaff.Text == "Chỉnh sửa nhân viên")
                {
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3009/api/tinhluong/congty/edit_nv_tax");
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(Main.IdAcount.ToString()), "cls_id_com");
                        string[] partsStart = textHienThiTGBatDau.Text.Split('/');
                        if (partsStart.Length == 2) { content.Add(new StringContent($"{partsStart[1]}/{partsStart[0]}"), "cls_day"); }
                        string[] partsEnd = textHienThiTGKetThuc.Text.Split('/');
                        if (partsEnd.Length == 2) { content.Add(new StringContent($"{partsEnd[1]}/{partsEnd[0]}"), "cls_day_end"); }
                        content.Add(new StringContent(LstInTax.cls_id.ToString()), "cls_id");
                        content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                        {
                            var resContent = await response.Content.ReadAsStringAsync();
                            this.Visibility = Visibility.Collapsed;
                            PopupDSNVInTax.LoadDataStaffInTax();
                            Main.grShowPopup.Children.Add(new ucThongBaoAll(PopupDSNVInTax));
                        }
                    }
                    catch (Exception)
                    {
                        ErrorSytem = "error";
                        Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                    }
                }
                else if (bodExitNextSaff.Text == "Thuế nhân viên")
                {
                    if (string.IsNullOrEmpty(cboLoaiThue.Text) || cboLoaiThue.Text == "")
                    {
                        tb_ValidateTienBaoHiem.Visibility = Visibility.Visible;
                        tb_ValidateTienBaoHiem.Text = "Bạn vui lòng chọn loại thuế!";
                        allow = false;
                    }
                    else
                    {
                        tb_ValidateTienBaoHiem.Visibility = Visibility.Hidden;
                    }
                    if (allow)
                    {
                        try
                        {
                            var client = new HttpClient();
                            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/them_nv_nhom_other_money");
                            var content = new MultipartFormDataContent();
                            string[] partsStart = textHienThiTGBatDau.Text.Split('/');
                            if (partsStart.Length == 2) { content.Add(new StringContent($"{partsStart[1]}/{partsStart[0]}"), "cls_day"); }
                            string[] partsEnd = textHienThiTGKetThuc.Text.Split('/');
                            if (partsEnd.Length == 2) { content.Add(new StringContent($"{partsEnd[1]}/{partsEnd[0]}"), "cls_day_end"); }
                            OOP.CaiDatLuong.Tax.clsTax.TaxListDetail thue = new OOP.CaiDatLuong.Tax.clsTax.TaxListDetail();
                            thue = (OOP.CaiDatLuong.Tax.clsTax.TaxListDetail)cboLoaiThue.SelectedItem;
                            content.Add(new StringContent(thue.cl_id.ToString()), "cls_id_cl");
                            content.Add(new StringContent(Main.IdAcount.ToString()), "cls_id_com");
                            content.Add(new StringContent(lstNSCTL.idQLC.ToString()), "cls_id_user");
                            content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                            request.Content = content;
                            var response = await client.SendAsync(request);
                            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                            {
                                var resContent = await response.Content.ReadAsStringAsync();
                                this.Visibility = Visibility.Collapsed;
                                Main.grShowPopup.Children.Add(new ucThongBaoAll(lstNSCTL));
                            }
                        }
                        catch (Exception)
                        {
                            ErrorSytem = "error";
                            Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                        }
                    }
                }
                else
                {
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3009/api/tinhluong/congty/edit_nv_tax");
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(Main.IdAcount.ToString()), "cls_id_com");
                        string[] partsStart = textHienThiTGBatDau.Text.Split('/');
                        string tg1 = $"{partsStart[1]}/{partsStart[0]}";
                        if (partsStart.Length == 2) { content.Add(new StringContent(tg1), "cls_day");}
                        string[] partsEnd = textHienThiTGKetThuc.Text.Split('/');
                        if (partsEnd.Length == 2) { content.Add(new StringContent($"{partsEnd[1]}/{partsEnd[0]}"), "cls_day_end"); }
                        content.Add(new StringContent(lstUs.cls_id.ToString()), "cls_id");
                        content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                        {
                            var resContent = await response.Content.ReadAsStringAsync();
                            this.Visibility = Visibility.Collapsed;
                            frmNSDTL.LoadDLDSNhanSuDaThietLap($"{DateTime.Now.Year}-{DateTime.Now.Month}");
                            Main.grShowPopup.Children.Add(new ucThongBaoAll(lstUs));
                        }
                    }
                    catch (Exception)
                    {
                        ErrorSytem = "error";
                        Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                    }
                }
               
            }
        }

        private void textNhapTienBH_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                tb_ValidateTienBaoHiem.Visibility = Visibility.Visible;
                tb_ValidateTienBaoHiem.Text = "Bạn vui lòng nhập đúng định dạng tiền bảo hiểm";
            }
            else
            {
                tb_ValidateTienBaoHiem.Visibility = Visibility.Collapsed;
            }
        }

        private void bod_HuySuaThue(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }
        private void ExitNextSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodExitNextSaff.Text == "Thời gian áp dụng")
            {
                this.Visibility = Visibility.Collapsed;
                PopUpThemNVCST.bod_Closed.Fill = (Brush)br.ConvertFrom("#000000");
                PopUpThemNVCST.bod_Closed.Opacity = 0.5;
                PopUpThemNVCST.Visibility = Visibility.Collapsed;
                PopUpThemNVCST.loadNV();
                ListUs.Clear();
                LoadThongTinNVAdd();
            }
            else if (bodExitNextSaff.Text == "Thuế nhân viên")
            {
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
                LoadThongTinNVEdit();
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (bodExitNextSaff.Text == "Thời gian áp dụng")
            {
                PopUpThemNVCST.bod_Closed.Fill = (Brush)br.ConvertFrom("#000000");
                PopUpThemNVCST.bod_Closed.Opacity = 0.5;
                this.Visibility = Visibility.Collapsed;
                LstUs.Clear();
                PopUpThemNVCST.loadNV();
                
                LoadThongTinNVAdd();
            }
            else if (bodExitNextSaff.Text == "Thuế nhân viên")
            {
                this.Visibility = Visibility.Collapsed;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
                LoadThongTinNVEdit();
            }
            
        }
    }
}
