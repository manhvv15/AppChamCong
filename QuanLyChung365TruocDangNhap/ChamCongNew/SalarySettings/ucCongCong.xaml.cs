using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CongCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CongCong;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.clsNhanVienThuocCongTy;
//using static NPOI.HSSF.Util.HSSFColor;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings
{
    /// <summary>
    /// Interaction logic for ucCongCong.xaml
    /// </summary>
    /// 
    public class NhanVienCongCong
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhongBan { get; set; }
        public string ChucVu { get; set; }
        public string CaCong { get; set; }
        public DateTime ThoiGianNhanCong { get; set; }
        public DateTime NgayGhiNhan { get; set; }
        public string NguoiXetDuyet { get; set; }
        public string GhiChu { get; set; }

    }
    public class Thang
    {
        public string thang { get; set; }
    }
    public class Nam
    {
        public string nam { get; set; }
    }
    public partial class ucCongCong : UserControl
    {
        MainWindow Main;
        List<NhanVienCongCong > nhanViens = new List<NhanVienCongCong> ();
        List<string> thang = new List<string> ();
        int month, year;
        BrushConverter brus = new BrushConverter ();
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        private int TongSoTrang = 0;
        private int SoDu = 0;
        private int PageNumberCurrent = 1;
        private List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat> lstChuaTL = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
        private List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat> lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
        private List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat> lstChuaTLFilter = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
        private List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat> lstChuaTLFilterPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
        public ucCongCong(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDLNam();
            LoadDLThang();
            LoadDLNhanVien();
            LoadDLCongCong();
        }
        private void ChonNamCongCong_SelectionChange(object sender, SelectionChangedEventArgs e)
        {

        }
        private void ChonThangCongCong_SelectionChange(object sender, SelectionChangedEventArgs e)
        {

        }
        string IdNV;
        private string SearchNV = "";
        List<string> interpolatedString;
        string abc;
        List<string> nguoi_xet_duyet = new List<string>();
        List<ListUser> listSaftSearch = new List<ListUser>();
        private void searchBarNhanVien_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (searchBarNhanVien.SelectedItem != null)
                {
                    searchBarNhanVien.PlaceHolderForground = "#474747";
                    listSaftSearch = new List<ListUser>();
                    var chonca = ((ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                    IdNV = chonca;
                    SearchNV = ((ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                    if (!Main.lstNhanVienThuocCongTy.Any(item => item.idQLC.ToString() == chonca))
                    {
                        listSaftSearch = Main.lstNhanVienThuocCongTy.ToList();
                        foreach (var item in listSaftSearch)
                        {
                            Main.NhanVien = item.userName;
                            IdNV = item.idQLC.ToString();
                        }
                    }
                }
                else
                {
                    searchBarNhanVien.PlaceHolderForground = "#ACACAC";
                }
            }
            catch (Exception)
            {
            }
        }
        private void LoadDLCongCong()
        {
            try
            {
                lstChuaTL.Clear();
                lstChuaTLPT.Clear();
                using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/list_user_cong_cong")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    int Thang = int.Parse(cbo_ChonThang.Text.Split(' ')[1]);
                    int Nam = int.Parse(cbo_ChonNam.Text.Split(' ')[1]);
                    if (Thang < 10)
                    {
                        request.AddParameter("start_date", $"{Nam}-0{Thang}-01");
                        request.AddParameter("end_date", $"{Nam}-0{Thang + 1}-01");
                    }
                    if (Thang == 12)
                    {
                        request.AddParameter("start_date", $"{Nam}-{Thang}-01");
                        request.AddParameter("end_date", $"{Nam + 1}-01-01");
                    }
                    else
                    {
                        request.AddParameter("start_date", $"{Nam}-{Thang}-01");
                        request.AddParameter("end_date", $"{Nam}-{Thang + 1}-01");
                    }
                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CongCong.clsDSCongCong.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CongCong.clsDSCongCong.Root>(b);
                    int totalPages = (int)Math.Ceiling((double)chuaTL.data.list_dexuat.Count / 10);
                    var STTCC = ((1 - 1) * totalPages) + 1;
                    if (chuaTL.data.list_dexuat != null)
                    {
                        foreach (var item in chuaTL.data.list_dexuat)
                        {
                            nguoi_xet_duyet.Clear();
                            foreach (var it in item.userDuyet)
                            {
                                nguoi_xet_duyet.Add(it.idQLC + "-" + it.userName);
                            }
                            item.nguoi_xet_duyet_format = (string.Join("\n", nguoi_xet_duyet));
                            if (item.noi_dung.bo_nhiem.chucvu_hientai == null)
                            {
                                item.pos_name = "Chưa cập nhật";
                            }
                            else
                            {
                                item.pos_name = item.noi_dung.bo_nhiem.chucvu_hientai;
                            }
                            item.STT = STTCC++;
                            item.time_xacnhan_cong = $"{item.noi_dung.xac_nhan_cong.time_vao_ca} - {item.noi_dung.xac_nhan_cong.time_het_ca}";
                            if (item.noi_dung.xac_nhan_cong.time_xnc.Day < 10)
                            {
                                item.ngay_xacnhan_cong = $"0{item.noi_dung.xac_nhan_cong.time_xnc.Day}-{item.noi_dung.xac_nhan_cong.time_xnc.Month}-{item.noi_dung.xac_nhan_cong.time_xnc.Year}";
                            }
                            else
                            {
                                item.ngay_xacnhan_cong = $"{item.noi_dung.xac_nhan_cong.time_xnc.Day}-{item.noi_dung.xac_nhan_cong.time_xnc.Month}-{item.noi_dung.xac_nhan_cong.time_xnc.Year}";
                            }
                            lstChuaTL.Add(item);
                        }
                        if (chuaTL.data.list_dexuat.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                        else DpPhanTRang.Visibility = Visibility.Visible;
                        TongSoTrang = chuaTL.data.list_dexuat.Count / 10;
                        SoDu = 10 - (chuaTL.data.list_dexuat.Count % 10);
                        if (chuaTL.data.list_dexuat.Count % 10 > 0)
                        {
                            TongSoTrang++;
                        }
                        for (int i = 0; i < 10 && i < chuaTL.data.list_dexuat.Count; i++)
                        {
                            lstChuaTLPT.Add(chuaTL.data.list_dexuat[i]);
                        }
                        dgvCongCong.ItemsSource = lstChuaTLPT;
                        dgvCongCong.Items.Refresh();
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
                            borLui1.Visibility = Visibility.Collapsed;
                            borPageDau.Visibility = Visibility.Collapsed;
                            borPage2.Visibility = Visibility.Visible;
                            borPage3.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
            catch
            {
            }
        }
        List<clsDSCongCong.ListDexuat> lstSearchCongCong = new List<clsDSCongCong.ListDexuat>();
        private void bodThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lstSearchCongCong.Clear();
                if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                {
                    foreach (var item in lstChuaTL)
                    {
                        if (IdNV == item.idQLC.ToString())
                        {
                            lstSearchCongCong.Add(item);
                        }
                        dgvCongCong.ItemsSource = lstSearchCongCong;
                        dgvCongCong.Items.Refresh();
                    }
                }
                else if (searchBarNhanVien.SelectedIndex == 0)
                {
                    LoadDLCongCong();
                }
                else
                {
                    LoadDLCongCong();
                }
            }
            catch (Exception)
            { }
          
        }
        private void LoadDLNhanVien()
        {
            try { searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy; } catch (Exception) { }
        }
        private void LoadDLNam()
        {
            cbo_ChonNam.Text = "Năm " + DateTime.Now.Year.ToString();
            cbo_ChonThang.PlaceHolder = "Năm " + DateTime.Now.Year.ToString();
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 5).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 4).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 3).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 2).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 2).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 3).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 4).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 5).ToString() });
            cbo_ChonNam.ItemsSource = lstNam;
        }
        private void LoadDLThang()
        {
            cbo_ChonThang.Text = "Tháng " + DateTime.Now.Month.ToString();
            cbo_ChonThang.PlaceHolder = "Tháng " + DateTime.Now.Month.ToString();
            lstThang.Add(new Thang { thang = "Tháng 1" });
            lstThang.Add(new Thang { thang = "Tháng 2" });
            lstThang.Add(new Thang { thang = "Tháng 3" });
            lstThang.Add(new Thang { thang = "Tháng 4" });
            lstThang.Add(new Thang { thang = "Tháng 5" });
            lstThang.Add(new Thang { thang = "Tháng 6" });
            lstThang.Add(new Thang { thang = "Tháng 7" });
            lstThang.Add(new Thang { thang = "Tháng 8" });
            lstThang.Add(new Thang { thang = "Tháng 9" });
            lstThang.Add(new Thang { thang = "Tháng 10" });
            lstThang.Add(new Thang { thang = "Tháng 11" });
            lstThang.Add(new Thang { thang = "Tháng 12" });
            cbo_ChonThang.ItemsSource = lstThang;
        }
        private void bodXoaCongCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucXoaCongCong());
        }

        private void XacNhanCongCongTatCa_Checked(object sender, RoutedEventArgs e)
        {
            //bodXacNhanCongCongTatCa.Visibility = Visibility.Visible;
           
        }
        private void XacNhanCongCongTatCa_Unchecked(object sender, RoutedEventArgs e)
        {
           //bodXacNhanCongCongTatCa.Visibility = Visibility.Collapsed;
        }

        private void XacNhanCongCongChoNhanVien_Checked(object sender, RoutedEventArgs e)
        {
           //bodXacNhanCongCongnNv.Visibility = Visibility.Visible;
           
        }
        private void XacNhanCongCongChoNhanVien_Unchecked(object sender, RoutedEventArgs e)
        {
            //bodXacNhanCongCongnNv.Visibility = Visibility.Collapsed;
        }

        private void borHienThiNam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNam.Visibility == Visibility.Collapsed)
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borNam.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        private void lsvNam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNam.ScrollToVerticalOffset(scrollNam.VerticalOffset - e.Delta);
        }

        private void textSearchNam_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNam = new List<Nam>();
            foreach (var str in lstNam)
            {
                if (str.nam.Contains(textSearchNam.Text.ToString()))
                {
                    lstSearchNam.Add(str);

                }
            }
            lsvNam.ItemsSource = lstSearchNam;
            if (textSearchNam.Text == "")
            {
                lsvNam.ItemsSource = lstNam;
            }
        }

        private void lsvNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //textHienThiNam.Text = lsvNam.SelectedItem.ToString();
            borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
            borNam.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
            Main.Nam = lsvNam.SelectedItem.ToString();
        }

        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Visible)
            {
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
            }
            else if (borNam.Visibility == Visibility.Visible)
            {
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
            }
            
        }
        private void btnHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Collapsed)
            {
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borThang.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvThang_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollThang.ScrollToVerticalOffset(scrollThang.VerticalOffset - e.Delta);
        }

        private void lsvThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //textHienThiThang.Text = lsvThang.SelectedItem.ToString();
            borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
            borThang.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
            Main.Thang = lsvThang.SelectedItem.ToString();
        }

        private void textSearchThang_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchThang = new List<Thang>();
            foreach (var str in lstThang)
            {
                if (str.thang.Contains(textSearchThang.Text.ToString()))
                {
                    lstSearchThang.Add(str);

                }
            }
            lsvThang.ItemsSource = lstSearchThang;
            if (textSearchThang.Text == "")
            {
                lsvThang.ItemsSource = lstThang;
            }
        }
        private void borHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Collapsed)
            {
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borThang.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        private void borThang_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thang th = (sender as Border).DataContext as Thang;
            if (th != null)
            {
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                //textHienThiThang.Text = th.thang;
                Main.Thang = th.thang;
            }
        }

        private void borNam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Nam th = (sender as Border).DataContext as Nam;
            if (th != null)
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                //textHienThiNam.Text = th.nam;
                Main.Nam = th.nam;
            }
        }

        private void dgvCongCong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnHienThiNam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNam.Visibility == Visibility.Collapsed)
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borNam.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                if (lstChuaTL.Count != 0)
                {
                    lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                    for (int i = 0; i < 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgvCongCong.ItemsSource = lstChuaTLPT;
                }
                PageNumberCurrent = 1;
            }
            catch (Exception)
            {
            }
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                dgvCongCong.ItemsSource = lstChuaTLPT;
            }
            catch (Exception)
            {
            }
        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                    lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgvCongCong.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    if (textPage1.Text != PageNumberCurrent.ToString())
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
                            lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                            for (int i = 0; i < 10; i++)
                            {
                                lstChuaTLPT.Add(lstChuaTL[i]);
                            }
                            dgvCongCong.ItemsSource = lstChuaTLPT;
                            PageNumberCurrent = 1;
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
                            lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                            if (lstChuaTL.Count > 10)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                                {
                                    lstChuaTLPT.Add(lstChuaTL[i]);
                                }
                                dgvCongCong.ItemsSource = lstChuaTLPT;
                            }
                        }
                    }
                }
            }
            catch { }
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
                }
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                if (lstChuaTL.Count > 10)
                {
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgvCongCong.ItemsSource = lstChuaTLPT;;
                }
            }
            catch
            {

            }
        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (PageNumberCurrent != TongSoTrang)
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
                        lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        dgvCongCong.ItemsSource = lstChuaTLPT;
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
                            lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                            for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                            {
                                lstChuaTLPT.Add(lstChuaTL[i]);
                            }
                            dgvCongCong.ItemsSource = lstChuaTLPT;
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
                            lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                            if (lstChuaTL.Count > 10)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                                {
                                    lstChuaTLPT.Add(lstChuaTL[i]);
                                }
                                dgvCongCong.ItemsSource = lstChuaTLPT;
                            }
                        }
                    }
                }

            }
            catch { }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                dgvCongCong.ItemsSource = lstChuaTLPT;
            }
            catch (Exception)
            {
            }
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                lstChuaTLPT = new List<clsDSCongCong.ListDexuat>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                dgvCongCong.ItemsSource = lstChuaTLPT;
            }
            catch (Exception)
            { }
        }

    }
}
