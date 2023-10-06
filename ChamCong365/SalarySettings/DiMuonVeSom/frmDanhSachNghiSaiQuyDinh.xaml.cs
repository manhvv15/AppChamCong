using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace ChamCong365.SalarySettings.DiMuonVeSom
{
    /// <summary>
    /// Interaction logic for frmDanhSachNghiSaiQuyDinh.xaml
    /// </summary>
    public partial class frmDanhSachNghiSaiQuyDinh : Page
    {
        private MainWindow Main;
        public class NhanVien
        {
            public string TenNhanVien { get; set; }
        }
        public class PhongBan
        {
            public string TenPB { get; set; }
        }
        public class Thang
        {
            public string thang { get; set; }
        }
        public class Nam
        {
            public string nam { get; set; }
        }
        public class DSDiMuonVeSom
        {
            public string ID { get; set; }
            public string HoVaTen { get; set; }
            public string Anh { get; set; }
            public string PhongBan { get; set; }
            public string ThoiGian { get; set; }
            public string Ca { get; set; }
            public string Muon { get; set; }
            public string Som { get; set; }
            public string Phat { get; set; }
        }
        List<DSDiMuonVeSom> lstDiMuonVeSom = new List<DSDiMuonVeSom>();
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<string> lst22 = new List<string>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public List<OOP.CaiDatLuong.clsShift.Item> lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();

        private List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum> lstSaiQD = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
        private List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum> lstSaiQDFilter = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
        public DataTable tb_SaiQD = new DataTable();
        private string DuongDanEx = Environment.CurrentDirectory + "\\TempExcel\\PhatNghiSaiQD.xltx";
        private string IdNV = "0";
        private string IdPB = "0";
        public frmDanhSachNghiSaiQuyDinh(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            
            main.i = 3;
            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDLCaLV();
            LoadDLNghiSaiQD();

        }
        private void GetDataToDataTable(List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum> dt)
        {
            foreach (var item in dt)
            {
                DataRow row = tb_SaiQD.NewRow();
                row["colId"] = item.ep_id;
                row["colHoTen"] = item.ep_name;
                row["colPhongBan"] = item.ep_dep_name;
                row["colThoiGian"] = item.time_ad;
                row["colCaLV"] = item.shift_name;
                row["colPhat"] = item.money;
                tb_SaiQD.Rows.Add(row);
            }
        }
        private void LoadDLCaLV()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    //string url = Properties.Resources.URL + "listGroupCustomer";
                    string url = "http://210.245.108.202:3000/api/qlc/shift/list";
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var kq = httpClient.GetAsync(url);
                    OOP.CaiDatLuong.clsShift.Root CaLV = JsonConvert.DeserializeObject<OOP.CaiDatLuong.clsShift.Root>(kq.Result.Content.ReadAsStringAsync().Result);
                    if (CaLV.data != null)
                    {
                        foreach (var calv in CaLV.data.items)
                        {

                            lstShift.Add(calv);
                            //cboCaLVApDung.Items.Add("(" + calv.shift_id + ")" + "-" + calv.shift_name);
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private void LoadDLNghiSaiQD()
        {
            
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_listuser_nghi_khong_phep")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    if (int.Parse(DateTime.Now.Month.ToString()) < 10)
                    {
                        request.AddParameter("start_date", DateTime.Now.Year + "-0" + DateTime.Now.Month + "-01T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("start_date", DateTime.Now.Year + "-" + DateTime.Now.Month + "-01T00:00:00.000+00:00");

                    }
                    if (DateTime.Now.Month == 12)
                    {
                        request.AddParameter("end_date", (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString() + "-01-01T00:00:00.000+00:00");

                    }
                    else
                    {
                        if (int.Parse(DateTime.Now.Month.ToString() + 1) < 10)
                        {
                            request.AddParameter("end_date", DateTime.Now.Year + "-0" + (int.Parse(DateTime.Now.Month.ToString()) + 1).ToString() + "-01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("end_date", DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Month.ToString()) + 1).ToString() + "-01T00:00:00.000+00:00");

                        }
                    }
                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("month", DateTime.Now.Month.ToString());
                    request.AddParameter("year", DateTime.Now.Year.ToString());
                    request.AddParameter("skip", 0);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root NSQD = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root>(b);
                    if (NSQD.data != null)
                    {
                        foreach (var item in NSQD.data)
                        {
                            foreach (var it in item.data_ko_cc_final)
                            {
                                item.time_ad = it.time.ToString();
                                item.shift_id = it.shift_id.ToString();
                                foreach(var s in lstShift)
                                {
                                    if (item.shift_id == s.shift_id.ToString())
                                    {
                                        item.shift_name = s.shift_name;
                                    }
                                }
                                foreach(var p in it.data_phat_nghi_ko_phep)
                                {
                                    if (item.shift_id == p.pc_shift.ToString())
                                    {
                                        item.money = p.pc_money.ToString();
                                        if (item.money == "1")
                                        {
                                            item.money = "0";
                                        }
                                    }
                                }
                                if (item.ep_id == it.detail.idQLC)
                                {
                                    item.ep_dep_id = it.detail.inForPerson.employee.dep_id.ToString();
                                    foreach(var pb in Main.lstPhongBanThuocCongTy)
                                    {
                                        if (pb.dep_id.ToString() == item.ep_dep_id)
                                        {
                                            item.ep_dep_name = pb.dep_name;
                                        }
                                    }
                                    item.ep_name = it.detail.userName;
                                    item.ep_avatar = "https://chamcong.24hpay.vn/upload/employee/" + it.detail.avatarUser;
                                }
                            }
                            lstSaiQD.Add(item);
                        }
                        dgv.ItemsSource = lstSaiQD;
                    }
                }
                tb_SaiQD = Function.clsExPortExcel.NewTables("tb_SaiQD", new string[] { "colId", "colHoTen", "colPhongBan", "colThoiGian", "colCaLV", "colPhat" }, new int[] { 150, 250, 300, 250, 250, 150 });
                GetDataToDataTable(lstSaiQD);

            }
            catch
            {

            }
        }

        private void LoadDLNhanVien()
        {
            textHienThiNhanVien.Text = "Tất cả nhân viên";
            
            lsvNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
            
        }

        private void LoadDLPhongBan()
        {
            textHienThiPhongBan.Text = "Phòng ban (tất cả)";
            lsvPhongBan.ItemsSource = Main.lstPhongBanThuocCongTy;
        }

        private void LoadDLThang()
        {
            textHienThiThang.Text = "Tháng " + DateTime.Now.Month.ToString();
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
            lsvThang.ItemsSource = lstThang;
        }

        private void LoadDLNam()
        {
            textHienThiNam.Text = "Năm " + DateTime.Now.Year.ToString();
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lsvNam.ItemsSource = lstNam;
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
            textHienThiNam.Text = lsvNam.SelectedItem.ToString();
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
            else if (borPhongBan.Visibility == Visibility.Visible)
            {
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
            }
            else if (borNhanVien.Visibility == Visibility.Visible)
            {
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
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
            textHienThiThang.Text = lsvThang.SelectedItem.ToString();
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

        private void lsvPhongBan_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ListView && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
            //scrollPhongBan.ScrollToVerticalOffset(scrollPhongBan.VerticalOffset - e.Delta);
        }


        private void borHienThiPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borPhongBan.Visibility == Visibility.Collapsed)
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borPhongBan.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvNhanVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNhanVien.ScrollToVerticalOffset(scrollNhanVien.VerticalOffset - e.Delta);
        }



        private void borHienThiNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNhanVien.Visibility == Visibility.Collapsed)
            {
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borNhanVien.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void borTenNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.clsNhanVienThuocCongTy.ListUser nv = (sender as Border).DataContext as OOP.clsNhanVienThuocCongTy.ListUser;
            if (nv != null)
            {
                textHienThiNhanVien.Text = nv.userName;
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.NhanVien = nv.userName;
                IdNV = nv.idQLC.ToString();
            }
        }

        private void borTenPB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OOP.clsPhongBanThuocCongTy.Item pb = (sender as Border).DataContext as OOP.clsPhongBanThuocCongTy.Item;
            if (pb != null)
            {
                lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                textHienThiPhongBan.Text = pb.dep_name;
                Main.PhongBan = pb.dep_name;
                IdPB = pb.dep_id.ToString();
                if (IdPB != "0")
                {
                    foreach (var nv in Main.lstNhanVienThuocCongTy)
                    {
                        if (nv.department != null)
                        {
                            foreach (var item in nv.department)
                            {
                                if (IdPB == item.dep_id)
                                {
                                    lstSearchNV.Add(nv);
                                }
                            }
                        }

                    }
                    lsvNhanVien.ItemsSource = lstSearchNV;
                }
                else
                {
                    lsvNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
                }
            }
        }

        private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            foreach (var str in lstNhanVien)
            {
                if (str.userName.Contains(textSearchNhanVien.Text.ToString()))
                {
                    lstSearchNV.Add(str);

                }
            }
            lsvNhanVien.ItemsSource = lstSearchNV;
            if (textSearchNhanVien.Text == "")
            {
                lsvNhanVien.ItemsSource = lstNhanVien;
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
                textHienThiThang.Text = th.thang;
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
                textHienThiNam.Text = th.nam;
                Main.Nam = th.nam;
            }
        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string[] nam = textHienThiNam.Text.Split(new[] { "Năm " }, StringSplitOptions.None);
            int HtNam = int.Parse(nam[nam.Length - 1]);
            string[] thang = textHienThiThang.Text.Split(new[] { "Tháng " }, StringSplitOptions.None);
            int HtThang = int.Parse(thang[thang.Length - 1]);
            if (IdPB == "0" && IdNV == "0")
            {
                lstSaiQD = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
                lstSaiQDFilter = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_listuser_nghi_khong_phep")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    if (HtThang < 10)
                    {
                        request.AddParameter("start_date", $"{HtNam}-0{HtThang}-01T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("start_date", $"{HtNam}-{HtThang}-01T00:00:00.000+00:00");
                    }
                    if (HtThang == 12)
                    {
                        request.AddParameter("end_date", $"{HtNam + 1}-01-01T00:00:00.000+00:00");

                    }
                    else
                    {
                        if (HtThang + 1 < 10)
                        {
                            request.AddParameter("end_date", $"{HtNam}-0{HtThang + 1}-01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("end_date", $"{HtNam}-{HtThang + 1}-01T00:00:00.000+00:00");
                        }

                    }

                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root NSQD = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root>(b);
                    if (NSQD.data != null)
                    {
                        foreach (var item in NSQD.data)
                        {
                            foreach (var it in item.data_ko_cc_final)
                            {
                                item.time_ad = it.time.ToString();
                                item.shift_id = it.shift_id.ToString();
                                foreach (var s in lstShift)
                                {
                                    if (item.shift_id == s.shift_id.ToString())
                                    {
                                        item.shift_name = s.shift_name;
                                    }
                                }
                                foreach (var p in it.data_phat_nghi_ko_phep)
                                {
                                    if (item.shift_id == p.pc_shift.ToString())
                                    {
                                        item.money = p.pc_money.ToString();
                                        if (item.money == "1")
                                        {
                                            item.money = "0";
                                        }
                                    }
                                }
                                if (item.ep_id == it.detail.idQLC)
                                {
                                    item.ep_dep_id = it.detail.inForPerson.employee.dep_id.ToString();
                                    foreach (var pb in Main.lstPhongBanThuocCongTy)
                                    {
                                        if (pb.dep_id.ToString() == item.ep_dep_id)
                                        {
                                            item.ep_dep_name = pb.dep_name;
                                        }
                                    }
                                    item.ep_name = it.detail.userName;
                                    item.ep_avatar = "https://chamcong.24hpay.vn/upload/employee/" + it.detail.avatarUser;
                                }
                            }
                            lstSaiQD.Add(item);
                        }
                        dgv.ItemsSource = lstSaiQD;
                    }
                    GetDataToDataTable(lstSaiQD);
                }

            }
            else if (IdPB != "0" && IdNV == "0")
            {
                lstSaiQD = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
                lstSaiQDFilter = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_listuser_nghi_khong_phep")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    if (HtThang < 10)
                    {
                        request.AddParameter("start_date", $"{HtNam}-0{HtThang}-01T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("start_date", $"{HtNam}-{HtThang}-01T00:00:00.000+00:00");
                    }
                    if (HtThang == 12)
                    {
                        request.AddParameter("end_date", $"{HtNam + 1}-01-01T00:00:00.000+00:00");

                    }
                    else
                    {
                        if (HtThang + 1 < 10)
                        {
                            request.AddParameter("end_date", $"{HtNam}-0{HtThang + 1}-01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("end_date", $"{HtNam}-{HtThang + 1}-01T00:00:00.000+00:00");
                        }

                    }

                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root NSQD = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root>(b);
                    if (NSQD.data != null)
                    {
                        foreach (var item in NSQD.data)
                        {
                            foreach (var it in item.data_ko_cc_final)
                            {
                                item.time_ad = it.time.ToString();
                                item.shift_id = it.shift_id.ToString();
                                foreach (var s in lstShift)
                                {
                                    if (item.shift_id == s.shift_id.ToString())
                                    {
                                        item.shift_name = s.shift_name;
                                    }
                                }
                                foreach (var p in it.data_phat_nghi_ko_phep)
                                {
                                    if (item.shift_id == p.pc_shift.ToString())
                                    {
                                        item.money = p.pc_money.ToString();
                                        if (item.money == "1")
                                        {
                                            item.money = "0";
                                        }
                                    }
                                }
                                if (item.ep_id == it.detail.idQLC)
                                {
                                    item.ep_dep_id = it.detail.inForPerson.employee.dep_id.ToString();
                                    foreach (var pb in Main.lstPhongBanThuocCongTy)
                                    {
                                        if (pb.dep_id.ToString() == item.ep_dep_id)
                                        {
                                            item.ep_dep_name = pb.dep_name;
                                        }
                                    }
                                    item.ep_name = it.detail.userName;
                                    item.ep_avatar = "https://chamcong.24hpay.vn/upload/employee/" + it.detail.avatarUser;
                                }
                            }
                            lstSaiQD.Add(item);
                        }
                        foreach (var item in lstSaiQD)
                        {
                            if (item.ep_dep_id == IdPB)
                            {
                                lstSaiQDFilter.Add(item);
                            }
                        }
                        dgv.ItemsSource = null;
                        
                        dgv.ItemsSource = lstSaiQDFilter;
                    }
                    
                }

            }
            else if (IdPB == "0" && IdNV != "0")
            {
                lstSaiQD = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
                lstSaiQDFilter = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_listuser_nghi_khong_phep")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    if (HtThang < 10)
                    {
                        request.AddParameter("start_date", $"{HtNam}-0{HtThang}-01T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("start_date", $"{HtNam}-{HtThang}-01T00:00:00.000+00:00");
                    }
                    if (HtThang == 12)
                    {
                        request.AddParameter("end_date", $"{HtNam + 1}-01-01T00:00:00.000+00:00");

                    }
                    else
                    {
                        if (HtThang + 1 < 10)
                        {
                            request.AddParameter("end_date", $"{HtNam}-0{HtThang + 1}-01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("end_date", $"{HtNam}-{HtThang + 1}-01T00:00:00.000+00:00");
                        }

                    }

                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root NSQD = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root>(b);
                    if (NSQD.data != null)
                    {
                        foreach (var item in NSQD.data)
                        {
                            foreach (var it in item.data_ko_cc_final)
                            {
                                item.time_ad = it.time.ToString();
                                item.shift_id = it.shift_id.ToString();
                                foreach (var s in lstShift)
                                {
                                    if (item.shift_id == s.shift_id.ToString())
                                    {
                                        item.shift_name = s.shift_name;
                                    }
                                }
                                foreach (var p in it.data_phat_nghi_ko_phep)
                                {
                                    if (item.shift_id == p.pc_shift.ToString())
                                    {
                                        item.money = p.pc_money.ToString();
                                        if (item.money == "1")
                                        {
                                            item.money = "0";
                                        }
                                    }
                                }
                                if (item.ep_id == it.detail.idQLC)
                                {
                                    item.ep_dep_id = it.detail.inForPerson.employee.dep_id.ToString();
                                    foreach (var pb in Main.lstPhongBanThuocCongTy)
                                    {
                                        if (pb.dep_id.ToString() == item.ep_dep_id)
                                        {
                                            item.ep_dep_name = pb.dep_name;
                                        }
                                    }
                                    item.ep_name = it.detail.userName;
                                    item.ep_avatar = "https://chamcong.24hpay.vn/upload/employee/" + it.detail.avatarUser;
                                }
                            }
                            lstSaiQD.Add(item);
                        }
                        foreach (var item in lstSaiQD)
                        {
                            if (item.ep_id.ToString() == IdNV)
                            {
                                lstSaiQDFilter.Add(item);
                            }
                        }
                        dgv.ItemsSource = null;

                        dgv.ItemsSource = lstSaiQDFilter;
                    }
                }

            }
            else if (IdPB != "0" && IdNV != "0")
            {
                lstSaiQD = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
                lstSaiQDFilter = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Datum>();
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_listuser_nghi_khong_phep")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    if (HtThang < 10)
                    {
                        request.AddParameter("start_date", $"{HtNam}-0{HtThang}-01T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("start_date", $"{HtNam}-{HtThang}-01T00:00:00.000+00:00");
                    }
                    if (HtThang == 12)
                    {
                        request.AddParameter("end_date", $"{HtNam + 1}-01-01T00:00:00.000+00:00");

                    }
                    else
                    {
                        if (HtThang + 1 < 10)
                        {
                            request.AddParameter("end_date", $"{HtNam}-0{HtThang + 1}-01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("end_date", $"{HtNam}-{HtThang + 1}-01T00:00:00.000+00:00");
                        }

                    }

                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root NSQD = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsNghiSaiQD.Root>(b);
                    
                    if (NSQD.data != null)
                    {
                        foreach (var item in NSQD.data)
                        {
                            foreach (var it in item.data_ko_cc_final)
                            {
                                item.time_ad = it.time.ToString();
                                item.shift_id = it.shift_id.ToString();
                                foreach (var s in lstShift)
                                {
                                    if (item.shift_id == s.shift_id.ToString())
                                    {
                                        item.shift_name = s.shift_name;
                                    }
                                }
                                foreach (var p in it.data_phat_nghi_ko_phep)
                                {
                                    if (item.shift_id == p.pc_shift.ToString())
                                    {
                                        item.money = p.pc_money.ToString();
                                        if (item.money == "1")
                                        {
                                            item.money = "0";
                                        }
                                    }
                                }
                                if (item.ep_id == it.detail.idQLC)
                                {
                                    item.ep_dep_id = it.detail.inForPerson.employee.dep_id.ToString();
                                    foreach (var pb in Main.lstPhongBanThuocCongTy)
                                    {
                                        if (pb.dep_id.ToString() == item.ep_dep_id)
                                        {
                                            item.ep_dep_name = pb.dep_name;
                                        }
                                    }
                                    item.ep_name = it.detail.userName;
                                    item.ep_avatar = "https://chamcong.24hpay.vn/upload/employee/" + it.detail.avatarUser;
                                }
                            }
                            lstSaiQD.Add(item);
                        }
                        foreach (var item in lstSaiQD)
                        {
                            if (item.ep_id.ToString() == IdNV)
                            {
                                lstSaiQDFilter.Add(item);
                            }
                        }
                        dgv.ItemsSource = null;

                        dgv.ItemsSource = lstSaiQDFilter;
                    }
                }

            }
        }

        private void btnXuatFileTK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (File.Exists(DuongDanEx))
            {
                Microsoft.Office.Interop.Excel.Application Ex = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = Ex.Workbooks.Open(DuongDanEx);
                Microsoft.Office.Interop.Excel.Worksheet ws_HoaDon = wb.Worksheets["PhatNghiSaiQD"];
                int DongBatDau = 2;
                foreach (DataRow row in tb_SaiQD.Rows)
                {
                    for (int i = 0; i < tb_SaiQD.Columns.Count; i++)
                    {
                        ws_HoaDon.Cells[DongBatDau, i + 1] = row[i];
                    }
                    DongBatDau++;
                }
                System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
                frm.Filter = "|*.xlsx";
                if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    wb.SaveAs(frm.FileName);
                    wb.Close();
                    Ex.Quit();
                }
            }
        }
    }
}
