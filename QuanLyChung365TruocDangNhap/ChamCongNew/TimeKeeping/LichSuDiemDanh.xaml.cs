using QuanLyChung365TruocDangNhap.ChamCongNew.Core;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.DuyetThietBiMoi;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatNhapLuongCoBan;
using QuanLyChung365TruocDangNhap.ChamCongNew.Salarysettings;
//using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for LichSuDiemDanh.xaml
    /// </summary>
    public partial class LichSuDiemDanh : System.Windows.Controls.UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MainWindow Main;
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNVTheoPhongBan2 = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNVTheoPhongBan = new List<OOP.clsNhanVienThuocCongTy.ListUser>();

        List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        private List<ListLichSuDiemDanh> lstLuongCB = new List<ListLichSuDiemDanh>();
        private List<ListLichSuDiemDanh> lstBasicSalaryExcel = new List<ListLichSuDiemDanh>();
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstLuongCBPT = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstLuongCBFilter = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstLuongCBFilter22222 = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<CaLamViec> _lstCa;
        public List<CaLamViec> lstCa
        {
            get { return _lstCa; }
            set { _lstCa = value; OnPropertyChanged(); }
        }
        public static DataTable tb_LuongCB = new DataTable();
        public OOP.OrganizeData SelectedOrganize = new OOP.OrganizeData();
        private int TongSoTrang = 0;
        private int PageNumberCurrent = 1;
        private int SoDu = 0;
        BrushConverter br = new BrushConverter();
        public LichSuDiemDanh(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            SearchPB = "0";
            SearchNV = "0";
            ngayBatDau.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM") + "/01");
            dtpNgayThanhLap.SelectedDate = DateTime.Now;
            LoadDLLuongCaBan();
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDSCalamViec();
        }
        public class DataInput
        {
            public string curPage { get; set; }
            public string start_time { get; set; }
            public string end_time { get; set; }
            public string ep_id { get; set; }
        }

        private async Task LoadBasicSalaryUserForExportExcel()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/timekeeping/getHistoryCheckin");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                DataInput dataInput = new DataInput();
                dataInput.curPage = PageNumberCurrent.ToString();
                dataInput.start_time = $"{ngayBatDau.SelectedDate.Value.ToString("yyyy-MM-dd")}T00:00:00.000Z";
                dataInput.end_time = $"{dtpNgayThanhLap.SelectedDate.Value.ToString("yyyy-MM-dd")}T23:59:59.000Z";
                int? ep_id = 0;
                if (searchBarNhanVien.SelectedIndex != -1) ep_id = ((OOP.clsNhanVienThuocCongTy.ListUser)searchBarNhanVien.SelectedItem).idQLC;
                var content = new StringContent("{ \r\n  \"pageSize\":" + 10000000 + ",\r\n  \"curPage\":" + PageNumberCurrent.ToString() + ",\r\n  \"start_time\": \"" + $"{ngayBatDau.SelectedDate.Value.ToString("yyyy-MM-dd")}T00:00:00.000Z" + "\",\r\n  \"end_time\": \"" + $"{dtpNgayThanhLap.SelectedDate.Value.ToString("yyyy-MM-dd")}T23:59:59.000Z" + "\",\r\n  \"ep_id\": " + ep_id + "\r\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                API_LichSuDiemDanh luongCB = JsonConvert.DeserializeObject<API_LichSuDiemDanh>(await response.Content.ReadAsStringAsync());
                lstBasicSalaryExcel = luongCB.data.data;

            }
            catch { }
        }
        private async void LoadDLLuongCaBan()
        {
            try
            {
                if (searchBarNhanVien.SelectedItem != null && ((OOP.clsNhanVienThuocCongTy.ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                {
                    loading.Visibility = Visibility.Visible;
                    lstLuongCB.Clear();
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/timekeeping/getHistoryCheckin");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    DataInput dataInput = new DataInput();
                    dataInput.curPage = PageNumberCurrent.ToString();
                    dataInput.start_time = $"{ngayBatDau.SelectedDate.Value.ToString("yyyy-MM-dd")}T00:00:00.000Z";
                    dataInput.end_time = $"{dtpNgayThanhLap.SelectedDate.Value.ToString("yyyy-MM-dd")}T23:59:59.000Z";
                    dataInput.ep_id = ((OOP.clsNhanVienThuocCongTy.ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                    var content = new StringContent("{ \r\n  \"curPage\":" + PageNumberCurrent.ToString() + ",\r\n  \"start_time\": \"" + $"{ngayBatDau.SelectedDate.Value.ToString("yyyy-MM-dd")}T00:00:00.000Z" + "\",\r\n  \"end_time\": \"" + $"{dtpNgayThanhLap.SelectedDate.Value.ToString("yyyy-MM-dd")}T23:59:59.000Z" + "\",\r\n  \"ep_id\": " + ((OOP.clsNhanVienThuocCongTy.ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString() + "\r\n}", null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    API_LichSuDiemDanh luongCB = JsonConvert.DeserializeObject<API_LichSuDiemDanh>(await response.Content.ReadAsStringAsync());
                    if (luongCB.data != null)
                    {
                        foreach (var item in luongCB.data.data)
                        {
                            item.image = "https://api.timviec365.vn//timviec365/" + item.image;
                            lstLuongCB.Add(item);
                        }
                        //lstLuongCB = luongCB.listResult;
                        if (luongCB.data.total <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                        else DpPhanTRang.Visibility = Visibility.Visible;
                        TongSoTrang = luongCB.data.total / 10;
                        SoDu = 10 - (luongCB.data.total % 10);
                        if (luongCB.data.total % 10 > 0)
                        {
                            TongSoTrang++;
                        }

                        //lstLuongCB = luongCB.listResult;
                        dgvListSalaryBasic.ItemsSource = lstLuongCB;
                        dgvListSalaryBasic.Items.Refresh();
                        if (TongSoTrang < 3)
                        {
                            if (TongSoTrang == 2)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                                borPage2.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                                borPageCuoi.Visibility = Visibility.Visible;
                            }
                            else if (TongSoTrang == 1)
                            {
                                borPage2.Visibility = Visibility.Collapsed;
                                borPage3.Visibility = Visibility.Collapsed;
                                borLen1.Visibility = Visibility.Collapsed;
                                borPageCuoi.Visibility = Visibility.Collapsed;
                            }
                        }
                        else
                        {
                            borPage2.Visibility = Visibility.Visible;
                            borPage3.Visibility = Visibility.Visible;
                        }
                        if (PageNumberCurrent == 1)
                        {
                            borLui1.Visibility = Visibility.Collapsed;
                            borPageDau.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            borLui1.Visibility = Visibility.Visible;
                            borPageDau.Visibility = Visibility.Visible;
                        }
                        if (PageNumberCurrent == TongSoTrang)
                        {
                            borLen1.Visibility = Visibility.Collapsed;
                            borPageCuoi.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }
                        tb_LuongCB = Function.clsExPortExcel.NewTables("tb_LuongCB", new string[] { "colMaNV", "colTenNhanVien", "colCaLamViec", "colThoiGian", "colDiaDiem", "colThietBi" });

                    }
                    loading.Visibility = Visibility.Collapsed;
                }
                else
                {
                    using (WebClient request = new WebClient())
                    {
                        loading.Visibility = Visibility.Visible;
                        lstLuongCB.Clear();
                        request.QueryString.Add("curPage", PageNumberCurrent.ToString());
                        request.QueryString.Add("start_time", $"{ngayBatDau.SelectedDate.Value.ToString("yyyy-MM-dd")}T00:00:00.000Z");
                        request.QueryString.Add("end_time", $"{dtpNgayThanhLap.SelectedDate.Value.ToString("yyyy-MM-dd")}T23:59:59.000Z");
                        var check = JsonConvert.SerializeObject(request.QueryString);
                        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                        request.UploadValuesCompleted += (s, e) =>
                        {
                            try
                            {
                                var check1 = UnicodeEncoding.UTF8.GetString(e.Result);
                                API_LichSuDiemDanh luongCB = JsonConvert.DeserializeObject<API_LichSuDiemDanh>(UnicodeEncoding.UTF8.GetString(e.Result));
                                if (luongCB.data != null)
                                {
                                    foreach (var item in luongCB.data.data)
                                    {
                                        item.image = "https://api.timviec365.vn//timviec365/" + item.image;
                                        lstLuongCB.Add(item);
                                    }
                                    //lstLuongCB = luongCB.listResult;
                                    if (luongCB.data.total <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                                    else DpPhanTRang.Visibility = Visibility.Visible;
                                    TongSoTrang = luongCB.data.total / 10;
                                    SoDu = 10 - (luongCB.data.total % 10);
                                    if (luongCB.data.total % 10 > 0)
                                    {
                                        TongSoTrang++;
                                    }

                                    //lstLuongCB = luongCB.listResult;
                                    dgvListSalaryBasic.ItemsSource = lstLuongCB;
                                    dgvListSalaryBasic.Items.Refresh();
                                    if (TongSoTrang < 3)
                                    {
                                        if (TongSoTrang == 2)
                                        {
                                            borPage3.Visibility = Visibility.Collapsed;
                                            borPage2.Visibility = Visibility.Visible;
                                            borLen1.Visibility = Visibility.Visible;
                                            borPageCuoi.Visibility = Visibility.Visible;
                                        }
                                        else if (TongSoTrang == 1)
                                        {
                                            borPage2.Visibility = Visibility.Collapsed;
                                            borPage3.Visibility = Visibility.Collapsed;
                                            borLen1.Visibility = Visibility.Collapsed;
                                            borPageCuoi.Visibility = Visibility.Collapsed;
                                        }
                                    }
                                    else
                                    {
                                        borPage2.Visibility = Visibility.Visible;
                                        borPage3.Visibility = Visibility.Visible;
                                    }
                                    if (PageNumberCurrent == 1)
                                    {
                                        borLui1.Visibility = Visibility.Collapsed;
                                        borPageDau.Visibility = Visibility.Collapsed;
                                    }
                                    else
                                    {
                                        borLui1.Visibility = Visibility.Visible;
                                        borPageDau.Visibility = Visibility.Visible;
                                    }
                                    if (PageNumberCurrent == TongSoTrang)
                                    {
                                        borLen1.Visibility = Visibility.Collapsed;
                                        borPageCuoi.Visibility = Visibility.Collapsed;
                                    }
                                    else
                                    {
                                        borLen1.Visibility = Visibility.Visible;
                                        borPageCuoi.Visibility = Visibility.Visible;
                                    }
                                    tb_LuongCB = Function.clsExPortExcel.NewTables("tb_LuongCB", new string[] { "colMaNV", "colTenNhanVien", "colCaLamViec", "colThoiGian", "colDiaDiem", "colThietBi" });

                                }
                                loading.Visibility = Visibility.Collapsed;
                            }
                            catch { loading.Visibility = Visibility.Collapsed; }

                        };
                        request.UploadValuesTaskAsync("https://api.timviec365.vn/api/qlc/timekeeping/getHistoryCheckin", "POST", request.QueryString);
                    }
                }

            }
            catch (Exception)
            { }
        }

        private async Task LoadDataInDataTable()
        {
            try
            {
                await LoadBasicSalaryUserForExportExcel();
                DataRow dr1 = tb_LuongCB.NewRow();

                dr1["colTenNhanVien"] = "Tên nhân viên";
                dr1["colMaNV"] = "Mã NV";
                dr1["colDiaDiem"] = "Địa điểm";
                dr1["colCaLamViec"] = "Ca làm việc";
                dr1["colThoiGian"] = "Thời gian điểm danh";
                dr1["colThietBi"] = "Thiết bị";
                tb_LuongCB.Rows.Add(dr1);
                foreach (var item in lstBasicSalaryExcel)
                {
                    DataRow dr = tb_LuongCB.NewRow();

                    dr["colTenNhanVien"] = item.userName;
                    dr["colMaNV"] = item.ep_id;
                    dr["colDiaDiem"] = item.ts_location_name;
                    dr["colCaLamViec"] = item.shift_name;
                    dr["colThoiGian"] = item.at_time.AddHours(7).ToString("dd-MM-yyyy HH:mm:ss");
                    dr["colThietBi"] = item.device;
                    tb_LuongCB.Rows.Add(dr);
                }
            }
            catch (Exception) { }

        }

        private void LoadDLNhanVien()
        {

            searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
            lstNhanVien = Main.lstNhanVienThuocCongTy;
        }

        private void LoadDLPhongBan()
        {
            searchBarToChuc.ItemsSource = Main.lstOrganizeData;
        }

        private async void LoadDSCalamViec()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "https://api.timviec365.vn/api/qlc/shift/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                API_CaLamViec api = JsonConvert.DeserializeObject<API_CaLamViec>(await response.Content.ReadAsStringAsync());
                if (api != null && api.data != null)
                {
                    lstCa = api.data.items;
                    cboCaLamViec.ItemsSource = lstCa;
                }
            }
            catch (Exception) { }
        }

        private void dapDay_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
        }

        private void bodSelectDaySalary_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }
        private void dapDay_SelectedDatesChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void bodImportExeclSalary_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void bodImportExeclSalary_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
        }

        private void ExportExcelSalary_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ExportExcelSalary.BorderThickness = new Thickness(1);
        }

        private void ExportExcelSalary_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ExportExcelSalary.BorderThickness = new Thickness(0);
        }

        private void bodThongTinNhanVien_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListResult_CDL cls = (sender as System.Windows.Controls.Border).DataContext as ListResult_CDL;
            if (cls != null)
            {
                Main.Back = 10;
                ucHoSoNhanVien uch = new ucHoSoNhanVien(Main, cls);
                ucListSalarySettings ucd = new ucListSalarySettings(Main);
                ucBodyHome ucb = new ucBodyHome(Main);
                Main.dopBody.Children.Clear();
                object Content = uch.Content;
                uch.Content = null;
                Main.dopBody.Children.Add(Content as UIElement);
                Main.LableFunction.Visibility = Visibility.Visible;
                //Main.txbLoadNameFunction.Text = ucb.txbSalarySettings.Text + " / " + ucd.txbFuncSalary01.Text + " / " + "Hồ Sơ Nhân Viên";
            }

        }

        private void cboloadName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dgvListSalaryBasic_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (Main.keyDown != Key.LeftShift && Main.keyDown != Key.RightShift)
                Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
            else
                dgvListSalaryBasic.GetFirstChildOfType<ScrollViewer>().ScrollToHorizontalOffset(dgvListSalaryBasic.GetFirstChildOfType<ScrollViewer>().HorizontalOffset - e.Delta);
        }

        private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lstSearchNV = new List<clsLuongCoBan.ListUser>();
            //foreach (var str in Main.lstNhanVienThuocCongTy)
            //{
            //    if (str.userName.ToLower().RemoveUnicode().Contains(textSearchNhanVien.Text.ToLower().RemoveUnicode()) || str.idQLC.ToString().Contains(textSearchNhanVien.Text.ToString()))
            //    {
            //        lstSearchNV.Add(str);

            //    }
            //}
            //lsvNhanVien.ItemsSource = lstSearchNV;
            //if (tb.Text == "")
            //{
            //    lsvNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
            //}
        }

        private string SearchNV = "";
        private string SearchPB = "";
        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            textPage1.Text = "1";
            textPage2.Text = "2";
            textPage3.Text = "3";
            PageNumberCurrent = 1;
            LoadDLLuongCaBan();
        }

        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            textPage1.Text = "1";
            textPage2.Text = "2";
            textPage3.Text = "3";
            borLen1.Visibility = Visibility.Visible;
            if (TongSoTrang > 2)
            {
                borPage2.Visibility = Visibility.Visible;
                borPage3.Visibility = Visibility.Visible;
            }
            else if (TongSoTrang > 1)
            {
                borPage2.Visibility = Visibility.Visible;
                borPage3.Visibility = Visibility.Collapsed;
            }
            else
            {
                borPage2.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
            }
            if (TongSoTrang > 1)
            {
                borPageCuoi.Visibility = Visibility.Visible;
                borLen1.Visibility = Visibility.Visible;
            }
            PageNumberCurrent = 1;
            LoadDLLuongCaBan();
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (PageNumberCurrent > 2)
            {
                textPage1.Text = (PageNumberCurrent - 2).ToString();
                textPage2.Text = (PageNumberCurrent - 1).ToString();
                textPage3.Text = PageNumberCurrent.ToString();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            }
            else if (TongSoTrang > 2)
            {
                textPage1.Text = (PageNumberCurrent - 1).ToString();
                textPage2.Text = (PageNumberCurrent).ToString();
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageDau.Visibility = Visibility.Collapsed;
                borLui1.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPage1.Text = "1";
                textPage2.Text = "2";
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageDau.Visibility = Visibility.Collapsed;
                borLui1.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
            }
            borPageCuoi.Visibility = Visibility.Visible;
            borLen1.Visibility = Visibility.Visible;
            PageNumberCurrent--;
            LoadDLLuongCaBan();
        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (textPage1.Text != PageNumberCurrent.ToString() && int.Parse(textPage1.Text) == PageNumberCurrent - 1)
            {
                if (PageNumberCurrent > 2)
                {
                    textPage1.Text = (PageNumberCurrent - 2).ToString();
                    textPage2.Text = (PageNumberCurrent - 1).ToString();
                    textPage3.Text = PageNumberCurrent.ToString();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                }
                else if (TongSoTrang > 2)
                {
                    textPage1.Text = (PageNumberCurrent - 1).ToString();
                    textPage2.Text = (PageNumberCurrent).ToString();
                    textPage3.Text = (PageNumberCurrent + 1).ToString();
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageDau.Visibility = Visibility.Collapsed;
                    borLui1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    textPage1.Text = "1";
                    textPage2.Text = "2";
                    textPage3.Text = (PageNumberCurrent + 1).ToString();
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageDau.Visibility = Visibility.Collapsed;
                    borLui1.Visibility = Visibility.Collapsed;
                    borPage3.Visibility = Visibility.Collapsed;
                }
                borPageCuoi.Visibility = Visibility.Visible;
                borLen1.Visibility = Visibility.Visible;
                PageNumberCurrent--;
                LoadDLLuongCaBan();
            }
            else
            {
                if (PageNumberCurrent == 3)
                {
                    borPageDau.Visibility = Visibility.Collapsed;
                    borLui1.Visibility = Visibility.Collapsed;
                    borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    textPage1.Text = "1";
                    textPage2.Text = "2";
                    textPage3.Text = "3";
                    borLen1.Visibility = Visibility.Visible;
                    if (TongSoTrang > 2)
                    {
                        borPage2.Visibility = Visibility.Visible;
                        borPage3.Visibility = Visibility.Visible;
                    }
                    else if (TongSoTrang > 1)
                    {
                        borPage2.Visibility = Visibility.Visible;
                        borPage3.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        borPage2.Visibility = Visibility.Collapsed;
                        borPage3.Visibility = Visibility.Collapsed;
                        borPageCuoi.Visibility = Visibility.Collapsed;
                        borLen1.Visibility = Visibility.Collapsed;
                    }
                    if (TongSoTrang > 1)
                    {
                        borPageCuoi.Visibility = Visibility.Visible;
                        borLen1.Visibility = Visibility.Visible;
                    }
                    PageNumberCurrent = 1;
                    LoadDLLuongCaBan();
                }
                else
                {
                    textPage1.Text = (PageNumberCurrent - 3).ToString();
                    textPage2.Text = (PageNumberCurrent - 2).ToString();
                    textPage3.Text = (PageNumberCurrent - 1).ToString();
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    PageNumberCurrent -= 2;
                    LoadDLLuongCaBan();
                }
            }
        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrushConverter brus = new BrushConverter();
                if (PageNumberCurrent.ToString() != textPage2.Text)
                {
                    PageNumberCurrent = int.Parse(textPage2.Text);
                    if (TongSoTrang >= 3)
                    {
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageDau.Visibility = Visibility.Visible;
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Visibility = Visibility.Collapsed;
                        borPageCuoi.Visibility = Visibility.Collapsed;
                        borLen1.Visibility = Visibility.Collapsed;
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                    }
                    LoadDLLuongCaBan();
                }
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            }
            catch
            {

            }

        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (PageNumberCurrent.ToString() != textPage3.Text && PageNumberCurrent > int.Parse(textPage3.Text) - 2)
            {
                if (PageNumberCurrent < TongSoTrang - 1)
                {
                    textPage1.Text = PageNumberCurrent.ToString();
                    textPage2.Text = (PageNumberCurrent + 1).ToString();
                    textPage3.Text = (PageNumberCurrent + 2).ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                }
                else if (TongSoTrang >= 3)
                {
                    textPage1.Text = (PageNumberCurrent - 1).ToString();
                    textPage2.Text = (PageNumberCurrent).ToString();
                    textPage3.Text = (PageNumberCurrent + 1).ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                }
                else
                {
                    textPage1.Text = "1";
                    textPage2.Text = "2";
                    textPage3.Text = (PageNumberCurrent + 1).ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    borPage3.Visibility = Visibility.Collapsed;
                }
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                PageNumberCurrent++;
                LoadDLLuongCaBan();
            }
            else
            {
                if (TongSoTrang == 3)
                {
                    textPage3.Text = TongSoTrang.ToString();
                    textPage2.Text = (TongSoTrang - 1).ToString();
                    textPage1.Text = (TongSoTrang - 2).ToString();
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    PageNumberCurrent = TongSoTrang;
                    LoadDLLuongCaBan();
                }
                else
                {
                    BrushConverter brus = new BrushConverter();
                    textPage1.Text = (PageNumberCurrent + 1).ToString();
                    textPage2.Text = (PageNumberCurrent + 2).ToString();
                    textPage3.Text = (PageNumberCurrent + 3).ToString();
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    PageNumberCurrent += 2;
                    LoadDLLuongCaBan();
                }
            }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (PageNumberCurrent < TongSoTrang - 1)
            {
                textPage1.Text = PageNumberCurrent.ToString();
                textPage2.Text = (PageNumberCurrent + 1).ToString();
                textPage3.Text = (PageNumberCurrent + 2).ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            }
            else if (TongSoTrang >= 3)
            {
                textPage1.Text = (PageNumberCurrent - 1).ToString();
                textPage2.Text = (PageNumberCurrent).ToString();
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPage1.Text = "1";
                textPage2.Text = "2";
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
            }
            borPageDau.Visibility = Visibility.Visible;
            borLui1.Visibility = Visibility.Visible;
            PageNumberCurrent++;
            LoadDLLuongCaBan();
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TongSoTrang >= 3)
            {
                textPage3.Text = TongSoTrang.ToString();
                textPage2.Text = (TongSoTrang - 1).ToString();
                textPage1.Text = (TongSoTrang - 2).ToString();
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            }
            else if (TongSoTrang == 2)
            {
                textPage3.Text = TongSoTrang.ToString();
                textPage2.Text = "2";
                textPage1.Text = "1";
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage3.Visibility = Visibility.Collapsed;
            }
            borPageCuoi.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            PageNumberCurrent = TongSoTrang;
            LoadDLLuongCaBan();
        }
        #region Hover Event
        private void gr_ThongTinNhanVien_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                System.Windows.Controls.Border bodThongTinNhanVien = FindChild<System.Windows.Controls.Border>(row, "bodThongTinNhanVien");

                if (bodThongTinNhanVien != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodThongTinNhanVien.Visibility = Visibility.Visible;
                }
            }
        }

        private void gr_ThongTinNhanVien_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                System.Windows.Controls.Border bodThongTinNhanVien = FindChild<System.Windows.Controls.Border>(row, "bodThongTinNhanVien");

                if (bodThongTinNhanVien != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodThongTinNhanVien.Visibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        #region Comons
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
        #endregion
        List<OOP.clsNhanVienThuocCongTy.ListUser> listSaftSearch = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSelectSaft = new List<OOP.clsNhanVienThuocCongTy.ListUser>();

        private void bod_TenNhanVien_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.ListViewItem row = FindAncestor<System.Windows.Controls.ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {

                System.Windows.Controls.Border bod_TenNhanVien = FindChild<System.Windows.Controls.Border>(row, "bod_TenNhanVien");
                TextBlock tb_TenNhanVien = FindChild<TextBlock>(row, "tb_TenNhanVien");
                if (bod_TenNhanVien != null && tb_TenNhanVien != null)
                {
                    bod_TenNhanVien.Background = (Brush)br.ConvertFrom("#4C5BD4");
                    tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                }
            }
        }

        private void bod_TenNhanVien_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.ListViewItem row = FindAncestor<System.Windows.Controls.ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {

                System.Windows.Controls.Border bod_TenNhanVien = FindChild<System.Windows.Controls.Border>(row, "bod_TenNhanVien");
                TextBlock tb_TenNhanVien = FindChild<TextBlock>(row, "tb_TenNhanVien");
                if (bod_TenNhanVien != null && tb_TenNhanVien != null)
                {
                    bod_TenNhanVien.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#474747");
                }
            }
        }

        private void btn_SelectListSafff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnThongKe_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnThongKe.Background = (Brush)br.ConvertFrom("#4AA7FF");
        }

        private void btnThongKe_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnThongKe.Background = (Brush)br.ConvertFrom("#4c5bd4");
        }
        public Key keyDown { get; set; }
        private void ucSalary_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                keyDown = e.Key;
            }
        }

        private void ucSalary_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == keyDown)
            {
                keyDown = Key.Cancel;
            }
        }

        private void bodImportExeclSalary_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucThemFileLuong ucs = new ucThemFileLuong(Main);
            Main.grShowPopup.Children.Add(new ucThemFileLuong(Main));
        }

        private void ExportExcelSalary_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ExportExcel();
        }
        public async void ExportExcel()
        {
            try
            {
                await LoadDataInDataTable();
                string PathTemplate = "";
                var file = Properties.Resources.FileLuongCB;
                string path = $"{Environment.GetEnvironmentVariable("APPDATA")}/ChamCong365/";
                if (!System.IO.Directory.Exists(path))
                {
                    System.IO.Directory.CreateDirectory(path);
                }
                if (!File.Exists(path + "FileLuongCB.xlsx"))
                {
                    File.WriteAllBytes(path + "FileLuongCB.xlsx", file);
                }
                PathTemplate = path + "FileLuongCB.xlsx";
                if (File.Exists(PathTemplate))
                {
                    Microsoft.Office.Interop.Excel.Application Ex = new Microsoft.Office.Interop.Excel.Application();
                    Microsoft.Office.Interop.Excel.Workbook wb = Ex.Workbooks.Open(PathTemplate);
                    Microsoft.Office.Interop.Excel.Worksheet ws_HoaDon = wb.Worksheets["ChamCong"];
                    ws_HoaDon.Name = "DS lịch sử điểm danh " + dtpNgayThanhLap.SelectedDate.Value.ToString("MM");
                    ws_HoaDon.Cells[1, 1] = Main.data.data.user_info.com_name;
                    int DongBatDau = 2;
                    foreach (DataRow row in tb_LuongCB.Rows)
                    {
                        for (int i = 0; i < tb_LuongCB.Columns.Count; i++)
                        {
                            ws_HoaDon.Cells[DongBatDau, i + 1] = row[i];
                        }
                        DongBatDau++;
                    }
                    System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
                    frm.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";
                    frm.FileName = ws_HoaDon.Name;
                    if (frm.ShowDialog() == DialogResult.OK)
                    {
                        wb.SaveAs(frm.FileName);
                        Main.grShowPopup.Children.Add(new Popup.ExportExcelSuccess(frm.FileName));
                        wb.Close();
                        Ex.Quit();
                    }
                }
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }
        private void Organize_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (searchBarToChuc.SelectedItem != null)
            {
                searchBarToChuc.PlaceHolderForground = "#474747";
            }
            else
            {
                searchBarToChuc.PlaceHolderForground = "#ACACAC";
            }
            OOP.OrganizeData tc = (OOP.OrganizeData)searchBarToChuc.SelectedItem;
            if (tc != null)
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                SelectedOrganize = tc;
                Main.ToChuc = tc.organizeDetailName;
                SearchPB = tc.organizeDetailName.ToString();
                if (tc.organizeDetailName == "Tổ chức (tất cả)")
                {
                    searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
                }
                else
                {
                    lstSearchNVTheoPhongBan = new List<OOP.clsNhanVienThuocCongTy.ListUser>();

                    foreach (var nv in Main.lstNhanVienThuocCongTy)
                    {
                        if (nv.organizeDetail != null)
                        {
                            if (nv.organizeDetail.listOrganizeDetailId.Find(x => x.organizeDetailId == tc.id) != null)
                            {
                                lstSearchNVTheoPhongBan.Add(nv);
                            }
                        }
                    }
                    OOP.clsNhanVienThuocCongTy.ListUser user = new OOP.clsNhanVienThuocCongTy.ListUser();
                    user.idQLC = 0;
                    user.userName = "Tất cả nhân viên";
                    lstSearchNVTheoPhongBan2.Insert(0, user);
                    searchBarNhanVien.ItemsSource = lstSearchNVTheoPhongBan;
                    lstNhanVien = lstSearchNVTheoPhongBan;
                }
            }
        }
        private void Staff_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (searchBarNhanVien.SelectedItem != null)
            {
                searchBarNhanVien.PlaceHolderForground = "#474747";
                listSaftSearch = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
                var chonca = ((OOP.clsNhanVienThuocCongTy.ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                SearchNV = ((OOP.clsNhanVienThuocCongTy.ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                if (!Main.lstNhanVienThuocCongTy.Any(item => item.idQLC.ToString() == chonca))
                {
                    listSaftSearch = Main.lstNhanVienThuocCongTy.ToList();
                }
            }
            else
            {
                searchBarNhanVien.PlaceHolderForground = "#ACACAC";
            }
        }
        public ListLichSuDiemDanh BhSelected { get; set; }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BhSelected = (sender as System.Windows.Controls.Border).DataContext as ListLichSuDiemDanh;
            var z = Mouse.GetPosition(Main.dopBody);
            popTuyChonBaoHiem.Margin = new Thickness(z.X - 295, z.Y, 0, 0);
            popTuyChonBaoHiem.Visibility = Visibility.Visible;
            popup.Visibility = Visibility.Visible;
        }

        private void popup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popTuyChonBaoHiem.Visibility = Visibility.Hidden;
            popup.Visibility = Visibility.Hidden;
        }

        private void SelectedCa(object sender, SelectionChangedEventArgs e)
        {
            if (cboCaLamViec.SelectedItem != null)
            {
                cboCaLamViec.PlaceHolderForground = "#474747";
            }
            else
            {
                cboCaLamViec.PlaceHolderForground = "#ACACAC";
            }
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (cboCaLamViec.SelectedItem != null)
                {
                    using (WebClient web = new WebClient())
                    {
                        web.QueryString.Add("sheet_id", BhSelected.sheet_id.ToString());
                        web.QueryString.Add("shift_id", ((CaLamViec)cboCaLamViec.SelectedItem).shift_id.ToString());
                        web.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                        web.UploadValuesAsync(new Uri("https://api.timviec365.vn/api/qlc/shift/updateTimesheet"), "POST", web.QueryString);
                        web.UploadValuesCompleted += (ss, ee) =>
                        {
                            try
                            {
                                var check = UTF8Encoding.UTF8.GetString(ee.Result);
                                if (check.Contains("true"))
                                {
                                    LoadDLLuongCaBan();
                                    popTuyChonBaoHiem.Visibility = Visibility.Hidden;
                                    popup.Visibility = Visibility.Hidden;
                                }
                            }
                            catch { }
                        };
                    }
                }

            }
            catch (Exception ex) { }
        }
    }
}
