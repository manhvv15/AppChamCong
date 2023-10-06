using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
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
    /// Interaction logic for frmDSDiMuonVeSom.xaml
    /// </summary>
    public partial class frmDSDiMuonVeSom : Page
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

        private string DuongDanEx = Environment.CurrentDirectory + "\\TempExcel\\FileLateEaly.xltx";
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        public List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly> lstDMVSFilter = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
        private List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly> lstDMVS = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public DataTable tb_MuonSom = new DataTable();
        private string IdNV;
        private string IdPB;
        private int Minute = 0;
        private int surplus = 0;
        //private string Nam = "";
        //private string Thang = "";
        public frmDSDiMuonVeSom(MainWindow main, frmCaiDatThietLapPhatDiMuonVeSom frm)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            //if (main.WindowState == WindowState.Maximized)
            //{
            //    borNam.HorizontalAlignment = HorizontalAlignment.Left;
            //    borNam.VerticalAlignment = VerticalAlignment.Top;
            //    borNam.Margin = new Thickness(814, 64, 0, 0);
            //}
            //else if (main.WindowState == WindowState.Normal)
            //{
            //    borNam.HorizontalAlignment = HorizontalAlignment.Left;
            //    borNam.VerticalAlignment = VerticalAlignment.Top;
            //    borNam.Margin = new Thickness(10, 108, 0, 0);
            //}
            //main.i = 2;
            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDLDiMuonVeSom();

            main.scrollMain.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        private void LoadDLDiMuonVeSom()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    if (DateTime.Now.Month < 10)
                    {
                        request.AddParameter("start_date", $"{DateTime.Now.Year}-0{DateTime.Now.Month}-01T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("start_date", $"{DateTime.Now.Year}-{DateTime.Now.Month}-01T00:00:00.000+00:00");
                    }
                    if (DateTime.Now.Month == 12)
                    {
                        request.AddParameter("end_date", $"{DateTime.Now.Year + 1}-01-01T00:00:00.000+00:00");

                    }
                    else
                    {
                        if (DateTime.Now.Month < 10)
                        {
                            request.AddParameter("end_date", $"{DateTime.Now.Year}-0{DateTime.Now.Month + 1}-01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("end_date", $"{DateTime.Now.Year}-{DateTime.Now.Month + 1}-01T00:00:00.000+00:00");
                        }

                    }
                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root dimuonvesom = JsonConvert.DeserializeObject<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root>(b);
                    if (dimuonvesom.data.list_data_late_early != null)
                    {

                        foreach (var item in dimuonvesom.data.list_data_late_early)
                        {
                            foreach (var shift in dimuonvesom.data.listShiftDetail)
                            {
                                if (shift.shift_id == item.shift_id)
                                {
                                    item.Shift_name = shift.shift_name;
                                }
                            }
                            foreach (var dep in dimuonvesom.data.listUserDetail)
                            {
                                if (item.ep_id == dep.idQLC)
                                {
                                    item.Dep_name = dep.Deparment.dep_name;
                                    item.ep_idCom = dep.inForPerson.employee.com_id;
                                    item.ep_idChat = dep._id;
                                }
                            }
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ep_id", item.ep_id.ToString());
                            httpClient2.QueryString.Add("cp", item.ep_idCom.ToString());
                            httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
                            var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
                            }


                            //WebClient httpClient2 = new WebClient();
                            //httpClient2.QueryString.Clear();
                            //httpClient2.QueryString.Add("ID", item.ep_idChat.ToString());

                            //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            //OOP.CaiDatLuong.CaiDatLuongCB.APIUser receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            //if (receiveInfoAPI.data != null)
                            //{
                            //    item.Avatar_Us = receiveInfoAPI.data.user_info.AvatarUser;
                            //}
                            foreach (var tp in dimuonvesom.data.tien_phat_muon)
                            {
                                if (tp.sheet_id == item.sheet_id)
                                {
                                    item.monetary_fine = tp.cong.ToString();
                                }
                            }

                            if (item.late > 0)
                            {
                                if (item.late_second >= 60)
                                {
                                    Minute = item.late_second / 60;
                                    surplus = item.late_second % 60;
                                    item.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            if (item.early > 0)
                            {
                                if (item.early_second >= 60)
                                {
                                    Minute = item.early_second / 60;
                                    surplus = item.early_second % 60;
                                    item.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            lstDMVS.Add(item);
                        }
                        dgv.ItemsSource = lstDMVS;
                    }
                }
                tb_MuonSom = Function.clsExPortExcel.NewTables("tb_MuonSom", new string[] { "colId", "colHoTen", "colPhongBan", "colThoiGian", "colCaLV", "colMuonSom", "colPhat" }, new int[] { 150, 250, 300, 250, 250, 200, 150 });

                GetDataToDataTable();
            }
            catch
            {

            }
        }

        private void GetDataToDataTable()
        {
            foreach (var item in lstDMVS)
            {
                DataRow row = tb_MuonSom.NewRow();
                row["colId"] = item.ep_id;
                row["colHoTen"] = item.ep_name;
                row["colPhongBan"] = item.Dep_name;
                row["colThoiGian"] = item.ts_date;
                row["colCaLV"] = item.Shift_name;
                row["colMuonSom"] = item.late_second_string;
                row["colPhat"] = item.monetary_fine;
                tb_MuonSom.Rows.Add(row);
            }
        }
        private void LoadDLNhanVien()
        {
            textHienThiNhanVien.Text = "Tất cả nhân viên";
            lstNhanVien = Main.lstNhanVienThuocCongTy;

            lsvNhanVien.ItemsSource = lstNhanVien;
            //lsvNhanVien.Items.Add("Tất cả nhân viên");
            //lsvNhanVien.Items.Add("(111788) Đỗ Văn Hoàng");
            //lsvNhanVien.Items.Add("(90229) Nguyễn Công Tiến");
        }

        private void LoadDLPhongBan()
        {
            textHienThiPhongBan.Text = "Phòng ban (tất cả)";

            lstPhongBan = Main.lstPhongBanThuocCongTy;
            lsvPhongBan.ItemsSource = lstPhongBan;
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
            lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            OOP.clsPhongBanThuocCongTy.Item pb = (sender as Border).DataContext as OOP.clsPhongBanThuocCongTy.Item;
            if (pb != null)
            {
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
                    lsvNhanVien.ItemsSource = lstNhanVien;
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

        private void WrapPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void DockPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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
                lstDMVS = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
                lstDMVSFilter = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late")))
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
                        if (HtThang < 10)
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
                    OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root dimuonvesom = JsonConvert.DeserializeObject<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root>(b);
                    if (dimuonvesom.data.list_data_late_early != null)
                    {

                        foreach (var item in dimuonvesom.data.list_data_late_early)
                        {
                            foreach (var shift in dimuonvesom.data.listShiftDetail)
                            {
                                if (shift.shift_id == item.shift_id)
                                {
                                    item.Shift_name = shift.shift_name;
                                }
                            }
                            foreach (var dep in dimuonvesom.data.listUserDetail)
                            {
                                if (item.ep_id == dep.idQLC)
                                {
                                    item.Dep_name = dep.Deparment.dep_name;
                                    item.ep_idCom = dep.inForPerson.employee.com_id;
                                    item.ep_idChat = dep._id.ToString();
                                }
                            }
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ep_id", item.ep_id.ToString());
                            httpClient2.QueryString.Add("cp", item.ep_idCom.ToString());
                            httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
                            var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
                            }
                            //WebClient httpClient2 = new WebClient();
                            //httpClient2.QueryString.Clear();
                            //httpClient2.QueryString.Add("ID", item.ep_idChat.ToString());

                            //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            //OOP.CaiDatLuong.CaiDatLuongCB.APIUser receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            //if (receiveInfoAPI.data != null)
                            //{
                            //    item.Avatar_Us = receiveInfoAPI.data.user_info.AvatarUser;
                            //}
                            foreach (var tp in dimuonvesom.data.tien_phat_muon)
                            {
                                if (tp.sheet_id == item.sheet_id)
                                {
                                    item.monetary_fine = tp.cong.ToString();
                                }
                            }

                            if (item.late > 0)
                            {
                                if (item.late_second >= 60)
                                {
                                    Minute = item.late_second / 60;
                                    surplus = item.late_second % 60;
                                    item.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            if (item.early > 0)
                            {
                                if (item.early_second >= 60)
                                {
                                    Minute = item.early_second / 60;
                                    surplus = item.early_second % 60;
                                    item.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            lstDMVS.Add(item);
                        }
                        dgv.ItemsSource = lstDMVS;
                    }
                }

            }
            else if (IdPB != "0" && IdNV == "0")
            {
                lstDMVS = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
                lstDMVSFilter = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late")))
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
                        if (HtThang < 10)
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
                    OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root dimuonvesom = JsonConvert.DeserializeObject<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root>(b);
                    if (dimuonvesom.data.list_data_late_early != null)
                    {

                        foreach (var item in dimuonvesom.data.list_data_late_early)
                        {
                            foreach (var shift in dimuonvesom.data.listShiftDetail)
                            {
                                if (shift.shift_id == item.shift_id)
                                {
                                    item.Shift_name = shift.shift_name;
                                }
                            }
                            foreach (var dep in dimuonvesom.data.listUserDetail)
                            {
                                if (item.ep_id == dep.idQLC)
                                {
                                    item.Dep_Id = dep.Deparment.dep_id;
                                    item.Dep_name = dep.Deparment.dep_name;
                                    item.ep_idCom = dep.inForPerson.employee.com_id;
                                    item.ep_idChat = dep._id;
                                }
                            }
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ep_id", item.ep_id.ToString());
                            httpClient2.QueryString.Add("cp", item.ep_idCom.ToString());
                            httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
                            var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
                            }


                            //WebClient httpClient2 = new WebClient();
                            //httpClient2.QueryString.Clear();
                            //httpClient2.QueryString.Add("ID", item.ep_idChat.ToString());

                            //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            //OOP.CaiDatLuong.CaiDatLuongCB.APIUser receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            //if (receiveInfoAPI.data != null)
                            //{
                            //    item.Avatar_Us = receiveInfoAPI.data.user_info.AvatarUser;
                            //}
                            foreach (var tp in dimuonvesom.data.tien_phat_muon)
                            {
                                if (tp.sheet_id == item.sheet_id)
                                {
                                    item.monetary_fine = tp.cong.ToString();
                                }
                            }

                            if (item.late > 0)
                            {
                                if (item.late_second >= 60)
                                {
                                    Minute = item.late_second / 60;
                                    surplus = item.late_second % 60;
                                    item.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            if (item.early > 0)
                            {
                                if (item.early_second >= 60)
                                {
                                    Minute = item.early_second / 60;
                                    surplus = item.early_second % 60;
                                    item.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            lstDMVS.Add(item);
                        }
                        foreach(var item in lstDMVS)
                        {
                            if (item.Dep_Id == int.Parse(IdPB))
                            {
                                lstDMVSFilter.Add(item);
                            }
                        }
                        dgv.ItemsSource = null;
                        dgv.ItemsSource = lstDMVSFilter;
                    }
                }

            }
            else if (IdPB == "0" && IdNV != "0")
            {
                lstDMVS = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
                lstDMVSFilter = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late")))
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
                        if (HtThang < 10)
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
                    OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root dimuonvesom = JsonConvert.DeserializeObject<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root>(b);
                    if (dimuonvesom.data.list_data_late_early != null)
                    {

                        foreach (var item in dimuonvesom.data.list_data_late_early)
                        {
                            foreach (var shift in dimuonvesom.data.listShiftDetail)
                            {
                                if (shift.shift_id == item.shift_id)
                                {
                                    item.Shift_name = shift.shift_name;
                                }
                            }
                            foreach (var dep in dimuonvesom.data.listUserDetail)
                            {
                                if (item.ep_id == dep.idQLC)
                                {
                                    item.Dep_Id = dep.Deparment.dep_id;
                                    item.Dep_name = dep.Deparment.dep_name;
                                    item.ep_idCom = dep.inForPerson.employee.com_id;
                                    item.ep_idChat = dep._id;
                                }
                            }
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ep_id", item.ep_id.ToString());
                            httpClient2.QueryString.Add("cp", item.ep_idCom.ToString());
                            httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
                            var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
                            }


                            //WebClient httpClient2 = new WebClient();
                            //httpClient2.QueryString.Clear();
                            //httpClient2.QueryString.Add("ID", item.ep_idChat.ToString());

                            //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            //OOP.CaiDatLuong.CaiDatLuongCB.APIUser receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            //if (receiveInfoAPI.data != null)
                            //{
                            //    item.Avatar_Us = receiveInfoAPI.data.user_info.AvatarUser;
                            //}
                            foreach (var tp in dimuonvesom.data.tien_phat_muon)
                            {
                                if (tp.sheet_id == item.sheet_id)
                                {
                                    item.monetary_fine = tp.cong.ToString();
                                }
                            }

                            if (item.late > 0)
                            {
                                if (item.late_second >= 60)
                                {
                                    Minute = item.late_second / 60;
                                    surplus = item.late_second % 60;
                                    item.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            if (item.early > 0)
                            {
                                if (item.early_second >= 60)
                                {
                                    Minute = item.early_second / 60;
                                    surplus = item.early_second % 60;
                                    item.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            lstDMVS.Add(item);
                        }
                        foreach (var item in lstDMVS)
                        {
                            if (item.ep_id == IdNV)
                            {
                                lstDMVSFilter.Add(item);
                            }
                        }
                        dgv.ItemsSource = null;
                        dgv.ItemsSource = lstDMVSFilter;
                    }
                }

            }
            else if (IdPB != "0" && IdNV != "0")
            {
                lstDMVS = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
                lstDMVSFilter = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late")))
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
                        if (HtThang < 10)
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
                    OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root dimuonvesom = JsonConvert.DeserializeObject<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root>(b);
                    if (dimuonvesom.data.list_data_late_early != null)
                    {

                        foreach (var item in dimuonvesom.data.list_data_late_early)
                        {
                            foreach (var shift in dimuonvesom.data.listShiftDetail)
                            {
                                if (shift.shift_id == item.shift_id)
                                {
                                    item.Shift_name = shift.shift_name;
                                }
                            }
                            foreach (var dep in dimuonvesom.data.listUserDetail)
                            {
                                if (item.ep_id == dep.idQLC)
                                {
                                    item.Dep_Id = dep.Deparment.dep_id;
                                    item.Dep_name = dep.Deparment.dep_name;
                                    item.ep_idCom = dep.inForPerson.employee.com_id;
                                    item.ep_idChat = dep._id;
                                }
                            }
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ep_id", item.ep_id.ToString());
                            httpClient2.QueryString.Add("cp", item.ep_idCom.ToString());
                            httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
                            var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
                            }


                            //WebClient httpClient2 = new WebClient();
                            //httpClient2.QueryString.Clear();
                            //httpClient2.QueryString.Add("ID", item.ep_idChat.ToString());

                            //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            //OOP.CaiDatLuong.CaiDatLuongCB.APIUser receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            //if (receiveInfoAPI.data != null)
                            //{
                            //    item.Avatar_Us = receiveInfoAPI.data.user_info.AvatarUser;
                            //}
                            foreach (var tp in dimuonvesom.data.tien_phat_muon)
                            {
                                if (tp.sheet_id == item.sheet_id)
                                {
                                    item.monetary_fine = tp.cong.ToString();
                                }
                            }

                            if (item.late > 0)
                            {
                                if (item.late_second >= 60)
                                {
                                    Minute = item.late_second / 60;
                                    surplus = item.late_second % 60;
                                    item.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            if (item.early > 0)
                            {
                                if (item.early_second >= 60)
                                {
                                    Minute = item.early_second / 60;
                                    surplus = item.early_second % 60;
                                    item.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
                                }
                            }

                            lstDMVS.Add(item);
                        }
                        foreach (var item in lstDMVS)
                        {
                            if (item.ep_id == IdNV)
                            {
                                lstDMVSFilter.Add(item);
                            }
                        }
                        dgv.ItemsSource = null;
                        dgv.ItemsSource = lstDMVSFilter;
                    }
                }

            }
        }

        private void btnXuatExcel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (File.Exists(DuongDanEx))
            {
                Microsoft.Office.Interop.Excel.Application Ex = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = Ex.Workbooks.Open(DuongDanEx);
                Microsoft.Office.Interop.Excel.Worksheet ws_HoaDon = wb.Worksheets["DiMuonVeSom"];
                int DongBatDau = 2;
                foreach (DataRow row in tb_MuonSom.Rows)
                {
                    for (int i = 0; i < tb_MuonSom.Columns.Count; i++)
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
