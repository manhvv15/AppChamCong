using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.HoaHongNhanDuoc;
//using DocumentFormat.OpenXml.Bibliography;
//using DocumentFormat.OpenXml.Office2016.Excel;
//using DocumentFormat.OpenXml.Office2019.Excel.RichData2;
//using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.clsNhanVienThuocCongTy;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.ThemSuaHoaHong
{
    /// <summary>
    /// Interaction logic for ucThemMoi_HoaHong_DT_LN_VT.xaml
    /// </summary>
    public partial class ucThemMoi_HoaHong_DT_LN_VT : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class ThemThongTin
        {
            public string dt_rose_id { get; set; }
            public int id { get; set; }
            public string dt_money { get; set; }
            public string dt_sl { get; set; }
            public string dt_time { get; set; }
            public object selectedDate { get; set; }
        }
        ObservableCollection<String> themThongTin = new ObservableCollection<String>();
        ObservableCollection<ThemThongTin> AddInfor = new ObservableCollection<ThemThongTin>();
        MainWindow Main;
        string Next2;
        BrushConverter br = new BrushConverter();
        List<List_Rose_DoanhThu> lstDoanhThuCaNhan = new List<List_Rose_DoanhThu>();
        public List<ListThietLap> lstRose2 = new List<ListThietLap>();
        private List<ThemThongTin> _listDT = new List<ThemThongTin>();

        public List<ThemThongTin> listDT
        {
            get { return _listDT; }
            set
            {
                _listDT = value; OnPropertyChanged();
            }
        }
        ucHoaHongNhanDuoc ucRose;
        public ucThemMoi_HoaHong_DT_LN_VT(MainWindow main, string next2, List<List_Rose_DoanhThu> list_Roses, List<ListThietLap> lstrose2, ucHoaHongNhanDuoc ucrose)
        {
            InitializeComponent();
            Main = main;
            lstDoanhThuCaNhan = list_Roses;
            this.lstRose2 = lstrose2;
            this.ucRose = ucrose;
            lsvAddInfo.ItemsSource = themThongTin;
            //lsvAddInfo.ItemsSource = AddInfor;
            Main = main;
            Next2 = next2;
            LoadNhanVien();
            LoadSanPham();
            tb_TieuDeHoaHong.Text = "Thêm mới hoa hồng doanh thu";
            gr_HienThiLoiNhuan.Visibility = Visibility.Collapsed;
            searchBarThoiGianApDung.Part_TextBox.Text = $"{DateTime.Now.Year}-{DateTime.Now.Month}-{DateTime.Now.Day}";

        }

        double LoiNhuan;
        public ucThemMoi_HoaHong_DT_LN_VT(MainWindow main, double loinhuan, List<ListThietLap> lstrose2, ucHoaHongNhanDuoc ucrose)
        {
            InitializeComponent();
            lsvAddInfo.ItemsSource = themThongTin;
            Main = main;
            LoiNhuan = loinhuan;
            lstRose2 = lstrose2;
            this.ucRose = ucrose;
            LoadNhanVien();
            LoadSanPham();
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();
            tb_TieuDeHoaHong.Text = "Thêm mới hoa hồng lợi nhuận";
            gr_HienThiLoiNhuan.Visibility = Visibility.Visible;
            stp_ThoiGianApDung.Visibility = Visibility.Collapsed;
            stp_ChonSanPham.Visibility = Visibility.Visible;
            stp_ChuKyApDung.Visibility = Visibility.Visible;
            stp_AddInfoHoaHongDoanhThu.Visibility = Visibility.Collapsed;
        }

        #region Popup Lich
        
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
        decimal ViTri;
        public ucThemMoi_HoaHong_DT_LN_VT(MainWindow main, decimal vitri, List<ListThietLap> lstrose2, ucHoaHongNhanDuoc ucrose)
        {
            InitializeComponent();
            lsvAddInfo.ItemsSource = themThongTin;
            Main = main;
            ViTri = vitri;
            lstRose2 = lstrose2;
            this.ucRose = ucrose;
            LoadNhanVien();
            LoadSanPham();
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();
            tb_TieuDeHoaHong.Text = "Thêm mới hoa hồng lệ phí vị trí";
            stp_ChonSanPham.Visibility = Visibility.Visible;
            stp_ThoiGianApDung.Visibility = Visibility.Collapsed;
            gr_HienThiLoiNhuan.Visibility = Visibility.Visible;
            tb_TongDoanhThu.Visibility = Visibility.Collapsed;
            stp_ChuKyApDung.Visibility = Visibility.Visible;
            stp_AddInfoHoaHongDoanhThu.Visibility = Visibility.Collapsed;
        }

        public void LoadNhanVien()
        {
            searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
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
        List<ListThietLap> lstThietLapDT = new List<ListThietLap>();
        List<ListThietLap> lstThietLapLN = new List<ListThietLap>();
        List<ListThietLap> lstThietLapVT = new List<ListThietLap>();
        public void LoadSanPham()
        {
            foreach (var item in lstRose2)
            {
                if (item.tl_id_rose == 2)
                {
                    lstThietLapDT.Add(item);
                }
                if (item.tl_id_rose == 3)
                {
                    lstThietLapLN.Add(item);
                }
                if (item.tl_id_rose == 4)
                {
                    lstThietLapVT.Add(item);
                }
            }
            if (ViTri == 24)
            {
                searchBarChonSanPham.ItemsSource = lstThietLapVT;
            }
            else if (LoiNhuan == 23)
            {
                searchBarChonSanPham.ItemsSource = lstThietLapLN;
            }
            else 
            {
                cbo_ChonDoanhThu.ItemsSource = lstThietLapDT;
            }
        }
        string Ro_Id_Tl;
        private void ChonSanPham(object sender, SelectionChangedEventArgs e)
        {
            if (cbo_ChonDoanhThu.SelectedItem != null)
            {
                cbo_ChonDoanhThu.Foreground = (Brush)br.ConvertFrom("#474747");
                var chonsp = ((ListThietLap)cbo_ChonDoanhThu.SelectedItem).tl_id.ToString();
                Ro_Id_Tl = chonsp;
            }
            else
            {
                cbo_ChonDoanhThu.Foreground = (Brush)br.ConvertFrom("#ACACAC");
            }
        }
        private void searchBarChonSanPham_LN(object sender, SelectionChangedEventArgs e)
        {
            if (searchBarChonSanPham.SelectedItem != null)
            {
                searchBarChonSanPham.Foreground = (Brush)br.ConvertFrom("#474747");
                searchBarChonSanPham.PlaceHolderForground = "#474747";
                var chonsp = ((ListThietLap)searchBarChonSanPham.SelectedItem).tl_id.ToString();
                Ro_Id_Tl = chonsp;
            }
            else
            {
                searchBarChonSanPham.Foreground = (Brush)br.ConvertFrom("#ACACAC");
                searchBarChonSanPham.PlaceHolderForground = "#ACACAC";
            }
        }
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindChild<T>(child, childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void tb_Thoat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void XoaDoiTuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = lsvAddInfo.SelectedItem as ThemThongTin;
            if (listDT.Count > 0)
                listDT.RemoveAt(listDT.IndexOf(selectedItem));
            lsvAddInfo.Items.Refresh();
        }

        private void btn_ThemDoanhThu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon.DatePicker dp = new QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon.DatePicker
            {
                FontSize = 16,
                Foreground = Application.Current.Resources["#474747"] as SolidColorBrush,
                SelectedDateChange = DatePicker_SelectedDateChanged,
                SelectedDate = DateTime.Now
            };
            listDT.Add(new ThemThongTin() { id = listDT.Count, dt_money = "", dt_time = DateTime.Now.ToString("dd/MM/yyyy"), selectedDate = dp });
            listDT = listDT.ToList();
            lsvAddInfo.ItemsSource = listDT;
        }
        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon.DatePicker dp = sender as QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon.DatePicker;
            ContentControl b = dp.Parent as ContentControl;
            if (b != null)
            {
                ThemThongTin dt = b.DataContext as ThemThongTin;
                foreach (var item in listDT)
                {
                    if (item.id == dt.id)
                        item.dt_time = dp.SelectedDate.Value.ToString("dd/MM/yyyy");
                }
            }
        }

        string DoanhThu;
        private void LoadThongTinListView(object sender, RoutedEventArgs e)
        {

            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                StackPanel stp_SoLuongSanPham = FindChild<StackPanel>(row, "stp_SoLuongSanPham");
                StackPanel stp_DoanhThu = FindChild<StackPanel>(row, "stp_DoanhThu");
                StackPanel stp_ThoiGian = FindChild<StackPanel>(row, "stp_ThoiGian");
                TextBox tb_SoLuongSanPham = FindChild<TextBox>(row, "tb_SoLuongSanPham");
                TextBox tb_DoanhThu1 = FindChild<TextBox>(row, "tb_DoanhThu1");
                TextBox tb_DoanhThu = FindChild<TextBox>(row, "tb_DoanhThu");
                if (stp_SoLuongSanPham != null || stp_DoanhThu != null || stp_ThoiGian != null || tb_SoLuongSanPham != null)
                {
                    if (Next2 == "RoseMoney2")
                    {
                        stp_SoLuongSanPham.Visibility = Visibility.Collapsed;
                        tb_DoanhThu.Visibility = Visibility.Visible;
                        //tb_DoanhThu1.Visibility = Visibility.Visible;
                        stp_DoanhThu.Margin = new Thickness(0, 0, 10, 0);
                        stp_ThoiGian.Margin = new Thickness(10, 0, 0, 0);
                        stp_DoanhThu.Width = 269;
                        stp_ThoiGian.Width = 269;
                        DoanhThu = tb_DoanhThu.Text;
                    }
                    if (LoiNhuan == 23)
                    {
                        stp_SoLuongSanPham.Visibility = Visibility.Visible;
                        stp_DoanhThu.Visibility = Visibility.Visible;
                        stp_ThoiGian.Visibility = Visibility.Visible;
                    }
                    if (ViTri == 24)
                    {
                        stp_SoLuongSanPham.Visibility = Visibility.Visible;
                        stp_DoanhThu.Visibility = Visibility.Collapsed;
                        stp_ThoiGian.Visibility = Visibility.Visible;
                        stp_SoLuongSanPham.Margin = new Thickness(0, 0, 10, 0);
                        stp_ThoiGian.Margin = new Thickness(10, 0, 0, 0);
                        stp_SoLuongSanPham.Width = 269;
                        stp_ThoiGian.Width = 269;
                        tb_TongSanPham.Text = tb_SoLuongSanPham.Text;
                    }
                }
            }
        }
        public string Text
        {
            get { return tb_TongSanPham.Text; }
            set { tb_TongSanPham.Text = value; }
        }
        public string Text1
        {
            get { return tb_TongDoanhThu1.Text; }
            set { tb_TongDoanhThu1.Text = value; }
        }
         string Api_Them_DoanhThu = "https://api.timviec365.vn/api/tinhluong/congty/insert_rose_dt_ca_nhan";
         string Api_Them_LoiNhuan = "https://api.timviec365.vn/api/tinhluong/congty/insert_rose_personal_ln";
        string Api_Them_ViTri = "https://api.timviec365.vn/api/tinhluong/congty/add_lp_vt";
        string Api_Them = "";
        int time_add;
        private async void btn_ThemHoaHongCacLoai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (stp_TenNhanVien_Chung.Visibility == Visibility.Visible)
            {
                if (searchBarNhanVien.Text == "" || string.IsNullOrEmpty(searchBarNhanVien.Text) || searchBarNhanVien.SelectedIndex == 0)
                {
                    tb_ValidateTenNhanVien.Visibility = Visibility.Visible;
                    tb_ValidateTenNhanVien.Text = "Bạn vui lòng chọn nhân viên";
                    allow = false;
                }
                else
                {
                    tb_ValidateTenNhanVien.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_ThoiGianApDung.Visibility == Visibility.Visible)
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
            if (stp_ChonSanPham.Visibility == Visibility.Visible)
            {
                if (searchBarChonSanPham.Text == "" || string.IsNullOrEmpty(searchBarChonSanPham.Text))
                {
                    tb_ValidateChonSanPham.Visibility = Visibility.Visible;
                    tb_ValidateChonSanPham.Text = "Bạn vui lòng chọn sản phẩm";
                    allow = false;
                }
                else
                {
                    tb_ValidateChonSanPham.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_AddInfoHoaHongDoanhThu.Visibility == Visibility.Visible)
            {
                if (cbo_ChonDoanhThu.Text == "" || string.IsNullOrEmpty(cbo_ChonDoanhThu.Text) || cbo_ChonDoanhThu.SelectedItem == null)
                {
                    tb_ValidateChonDoanhThu.Visibility = Visibility.Visible;
                    tb_ValidateChonDoanhThu.Text = "Bạn vui lòng chọn doanh thu";
                    allow = false;
                }
                else
                {
                    tb_ValidateChonDoanhThu.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_GhiChu_Chung.Visibility == Visibility.Visible)
            {
                if (tb_GhiChu.Text == "" || string.IsNullOrEmpty(tb_GhiChu.Text))
                {
                    tb_ValidateGhiChu.Visibility = Visibility.Visible;
                    tb_ValidateGhiChu.Text = "Bạn vui lòng nhập ghi chú cho hoa hồng được thêm";
                    allow = false;
                }
                else
                {
                    tb_ValidateGhiChu.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_ChuKyApDung.Visibility == Visibility.Visible)
            {
                if (textHienThiTGBatDau.Text == "---- --- ----" || textHienThiTGBatDau.Text == null)
                {
                    tb_ValidateThangApDung.Visibility = Visibility.Visible;
                    tb_ValidateThangApDung.Text = "Bạn vui lòng chọn tháng áp dụng";
                    allow = false;
                }
                else
                {
                    tb_ValidateThangApDung.Visibility = Visibility.Collapsed;
                }
            }
            if (allow)
            {
                
                try
                {
                    var client = new HttpClient();
                    if (LoiNhuan == 23)
                    {
                        Api_Them = Api_Them_LoiNhuan;
                    }
                    else if (ViTri == 24)
                    {
                        Api_Them = Api_Them_ViTri;
                    }
                    else if (Next2 == "RoseMoney2")
                    {
                        Api_Them = Api_Them_DoanhThu;
                    }
                    var request = new HttpRequestMessage(HttpMethod.Post, Api_Them);
                    var content = new MultipartFormDataContent();
                    string[] myArray1 = new string[] { };
                    string[] myArray2 = new string[] { };
                    string[] myArray3 = new string[] { };
                    for (int i = 0; i < listDT.Count; i++)
                    {
                        if (listDT[i].dt_money != null)
                        {
                            string time = "";
                            string[] time1 = listDT[i].dt_time.Replace("/", " - ").Split(' ');
                            for (int j = time1.Length - 1; j > -1; j--)
                            {
                                time += time1[j];
                            }
                            System.Array.Resize(ref myArray1, myArray1.Length + 1);
                            myArray1[myArray1.Length - 1] = listDT[i].dt_money;
                            System.Array.Resize(ref myArray3, myArray3.Length + 1);
                            myArray3[myArray1.Length - 1] = listDT[i].dt_sl;
                            System.Array.Resize(ref myArray2, myArray2.Length + 1);
                            myArray2[myArray2.Length - 1] = time;
                        }
                    }
                    StringBuilder resultBuilder = new StringBuilder();
                    resultBuilder.Append("[");
                    for (int i = 0; i < myArray2.Length; i++)
                    {
                        resultBuilder.Append("\""); resultBuilder.Append(myArray2[i]); resultBuilder.Append("\"");
                        if (i < myArray2.Length - 1) { resultBuilder.Append(","); }
                    }
                    resultBuilder.Append("]");

                    string result1 = "[" + string.Join(",", myArray1.Select(x => $"{x}")) + "]";
                    string result3 = "[" + string.Join(",", myArray3.Select(x => $"{x}")) + "]";
                    if (LoiNhuan == 23)
                    {
                        content.Add(new StringContent(result1), "dt_money");
                        content.Add(new StringContent(result3), "dt_sl");
                        content.Add(new StringContent(resultBuilder.ToString()), "dt_time");
                    }
                    else if (ViTri == 24)
                    {
                        Random random = new Random();
                        long randomNumber = (long)(random.NextDouble() * long.MaxValue);
                        content.Add(new StringContent(tb_TongSanPham.Text), "sl_add");
                        content.Add(new StringContent(randomNumber.ToString()), "time_add");
                    }
                    else if (Next2 == "RoseMoney2")
                    {
                        content.Add(new StringContent(result1), "dt_money");
                        content.Add(new StringContent(resultBuilder.ToString()), "dt_time");
                    }

                    if (ViTri == 24)
                    {
                        content.Add(new StringContent(Main.IdAcount.ToString()), "cp");
                    }
                    else
                    {
                        content.Add(new StringContent(Main.IdAcount.ToString()), "ro_id_com");
                    }
                    

                    if (LoiNhuan == 23)
                    {
                        content.Add(new StringContent("3"), "ro_id_lr");
                    }
                    else if (Next2 == "RoseMoney2")
                    {
                        content.Add(new StringContent("2"), "ro_id_lr");
                    }
                  
                    if (ViTri == 24)
                    {
                        content.Add(new StringContent(Ro_Id_Tl), "sanpham_tl");
                        content.Add(new StringContent(ID_NhanVien), "select_user");
                    }
                    if(Next2 == "RoseMoney2" || LoiNhuan == 23)
                    {
                        content.Add(new StringContent(Ro_Id_Tl), "ro_id_tl");
                        content.Add(new StringContent(ID_NhanVien), "ro_id_user");
                        content.Add(new StringContent(tb_GhiChu.Text), "ro_note");
                    }

                    if (LoiNhuan == 23)
                    {
                        content.Add(new StringContent(tb_TongDoanhThu1.Text), "ro_price");
                        content.Add(new StringContent(tb_TongSanPham.Text), "ro_so_luong");
                    }
                    else if (ViTri == 24)
                    {
                        content.Add(new StringContent(tb_TongSanPham.Text), "lp_amount");
                    }
                    else if (Next2 == "RoseMoney2")
                    {
                        content.Add(new StringContent(tb_TongDoanhThu2.Text), "ro_price");
                    }

                    if (Next2 == "RoseMoney2")
                    {
                        if (searchBarThoiGianApDung.SelectedDate != null)
                        {
                            content.Add(new StringContent($"{searchBarThoiGianApDung.SelectedDate.Value.Year}-{searchBarThoiGianApDung.SelectedDate.Value.Month}-{searchBarThoiGianApDung.SelectedDate.Value.Day}"), "ro_time");
                        }
                        else
                        {
                            content.Add(new StringContent(searchBarThoiGianApDung.Part_TextBox.Text), "ro_time");
                        }
                    }
                    else if (LoiNhuan == 23)
                    {
                        string time = "";
                        string[] time1 = textHienThiTGBatDau.Text.Replace("/", " - ").Split(' ');
                        for (int j = time1.Length - 1; j > -1; j--)
                        {
                            time += time1[j];
                        }
                        content.Add(new StringContent(time), "ro_time");
                    }
                    else if (ViTri == 24)
                    {
                        string time = "";
                        string[] time1 = textHienThiTGBatDau.Text.Replace("/", " - ").Split(' ');
                        for (int j = time1.Length - 1; j > -1; j--)
                        {
                            time += time1[j];
                        }
                        string chuky = $"{time}-{DateTime.Now.Day}";
                        content.Add(new StringContent(chuky), "chuky_lp");
                        content.Add(new StringContent(chuky), "ro_time");
                    }
                    
                    content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resConten = await response.Content.ReadAsStringAsync();
                        if (Next2 == "RoseMoney2")
                        {
                            ucRose.LoadHoaHongDoanhThuCaNhan();
                            this.Visibility = Visibility.Collapsed;
                        }
                        else if (LoiNhuan == 23)
                        {
                            ucRose.LoadHoaHongLoiNhuan();
                            this.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            ucRose.LoadHoaHongViTri();
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                catch (Exception)
                {
                }
               
            }
        }

        private void tb_DoanhThu_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                TextBox tb_DoanhThu = FindChild<TextBox>(row, "tb_DoanhThu");
                TextBlock tb_ValidateDoanhThu = FindChild<TextBlock>(row, "tb_ValidateDoanhThu");
                if (tb_ValidateDoanhThu != null)
                {
                    if (!IsNumeric(e.Text))
                    {
                        e.Handled = true;
                        tb_ValidateDoanhThu.Visibility = Visibility.Visible;
                        tb_ValidateDoanhThu.Text = "Chỉ được nhập số";
                    }
                    else
                    {
                        tb_ValidateDoanhThu.Visibility = Visibility.Collapsed;
                    }
                }
            }
            e.Handled = !IsAllowed(sender as TextBox, e.Text);

        }
        private static readonly Regex _regex = new Regex(@"^[0-9]\d*(\.\d{0,2})?$");
        private static bool IsTextAllowed(string text)
        {
            return _regex.IsMatch(text);
        }
        private bool IsAllowed(TextBox tb, string text)
        {
            bool isAllowed = true;
            if (tb != null)
            {
                string currentText = tb.Text;
                if (!string.IsNullOrEmpty(tb.SelectedText))
                    currentText = currentText.Remove(tb.CaretIndex, tb.SelectedText.Length);
                isAllowed = IsTextAllowed(currentText.Insert(tb.CaretIndex, text));
            }
            return isAllowed;
        }

        private void tb_SoLuongSanPham_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                TextBox tb_SoLuongSanPham = FindChild<TextBox>(row, "tb_SoLuongSanPham");
                TextBlock tb_ValidateSoLuongSanPham = FindChild<TextBlock>(row, "tb_ValidateSoLuongSanPham");
                if (tb_SoLuongSanPham != null)
                {
                    if (!IsNumeric(e.Text))
                    {
                        e.Handled = true;
                        tb_ValidateSoLuongSanPham.Visibility = Visibility.Visible;
                        tb_ValidateSoLuongSanPham.Text = "Chỉ được nhập số";
                    }
                    else
                    {
                        tb_ValidateSoLuongSanPham.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }
        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        private void tb_DoanhThu1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                TextBox tb_DoanhThu1 = FindChild<TextBox>(row, "tb_DoanhThu1");
                TextBlock tb_ValidateDoanhThu = FindChild<TextBlock>(row, "tb_ValidateDoanhThu");
                if (tb_ValidateDoanhThu != null)
                {
                    if (!IsNumeric(e.Text))
                    {
                        e.Handled = true;
                        tb_ValidateDoanhThu.Visibility = Visibility.Visible;
                        tb_ValidateDoanhThu.Text = "Chỉ được nhập số";
                    }
                    else
                    {
                        tb_ValidateDoanhThu.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void tb_DoanhThu_TextInput(object sender, TextCompositionEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);
            if (row != null)
            {

                TextBox tb_DoanhThu = FindChild<TextBox>(row, "tb_DoanhThu");
                if (tb_DoanhThu != null)
                {
                    for (int i = 0; i < lsvAddInfo.Items.Count; i++)
                    {
                        if (double.TryParse(tb_DoanhThu.Text, out double number))
                        {
                            //sum += number;
                        }

                    }
                }
            }
        }

        private void money_textchanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            ThemThongTin data = (ThemThongTin)tb.DataContext;
            foreach (var item in listDT)
            {
                if (item.id == data.id)
                    item.dt_money = tb.Text;
            }
            long tong = 0;
            try
            {
                foreach (var item in listDT)
                {
                    if (!string.IsNullOrEmpty(item.dt_money))
                    {
                        long x = long.Parse(item.dt_money);
                        if (tong + x <= long.MaxValue)
                        {
                            tong += x;
                            if (Next2 == "RoseMoney2")
                            {
                                tb_TongDoanhThu2.Text = tong + "";
                            }
                            else
                            {
                                tb_TongDoanhThu1.Text = tong + "";
                            }
                           
                        }
                    }
                }
            }
            catch (Exception)
            {
                tb.Text = tb.Text.Remove(tb.Text.Length - 3);
                tb.SelectionStart = tb.Text.Length;
            }
        }

        private void TextBoxPasting(object sender, DataObjectPastingEventArgs e)
        {
            if (e.DataObject.GetDataPresent(typeof(String)))
            {
                String text = (String)e.DataObject.GetData(typeof(String));
                if (!IsAllowed(sender as TextBox, text))
                    e.CancelCommand();
            }
            else
                e.CancelCommand();
        }

        private void sl_textchanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            ThemThongTin data = (ThemThongTin)tb.DataContext;
            foreach (var item in listDT)
            {
                if (item.id == data.id)
                    item.dt_sl = tb.Text;
            }
            long tong = 0;
            try
            {
                foreach (var item in listDT)
                {
                    if (!string.IsNullOrEmpty(item.dt_sl))
                    {
                        long x = long.Parse(item.dt_sl);
                        if (tong + x <= long.MaxValue)
                        {
                            tong += x;
                            tb_TongSanPham.Text = tong + "";
                        }
                    }
                }
            }
            catch (Exception)
            {
                tb.Text = tb.Text.Remove(tb.Text.Length - 3);
                tb.SelectionStart = tb.Text.Length;
            }
        }
        
    }
}
