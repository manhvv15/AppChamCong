using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatDiMuonVeSom;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.CoCau_ViTri_ToChuc.ucSoDoToChuc;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.DiMuonVeSom
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

        private string DuongDanEx = Environment.CurrentDirectory + "\\TempExcel\\FileLateEaly.xltx";
        public List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<Data_DMVS> lstDMVSFilter = new List<Data_DMVS>();
        private List<Data_DMVS> lstDMVS = new List<Data_DMVS>();
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
            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDLDiMuonVeSom();
            main.scrollMain.HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
        }

        private int TongSoTrang = 0;
        private int PageNumberCurrent = 1;
        private int SoDu = 0;
        private void LoadDLDiMuonVeSom()
        {
            try
            { 
                using (WebClient request = new WebClient())
                {
                    loading.Visibility = Visibility.Visible;
                    lstDMVS.Clear();
                    lstDMVSFilter.Clear();
                    int year = DateTime.Now.Year;
                    if(lsvNam.SelectedItem != null)
                        year = int.Parse(lsvNam.SelectedItem.ToString().Split(' ')[1]);
                    int month = DateTime.Now.Month;
                    if(lsvThang.SelectedItem != null)
                        month = int.Parse(lsvThang.SelectedItem.ToString().Split(' ')[1]);
                    request.QueryString.Add("start_date", year + "-" + month + "-01");
                    request.QueryString.Add("end_date", year + "-" + month + "-" + DateTime.DaysInMonth(year,month));
                    request.QueryString.Add("month", month.ToString());
                    request.QueryString.Add("year", year.ToString());
                    if(lsvNhanVien.SelectedItem != null)
                    {
                        request.QueryString.Add("ep_id", ((OOP.clsNhanVienThuocCongTy.ListUser)lsvNhanVien.SelectedItem).idQLC.ToString());
                    }
                    if(textHienThiPhongBan.SelectedItem != null && ((OOP.OrganizeData)textHienThiPhongBan.SelectedItem).id > 0)
                    {
                        request.QueryString.Add("listOrgDetailId", JsonConvert.SerializeObject(((OOP.OrganizeData)textHienThiPhongBan.SelectedItem).listOrganizeDetailId));
                    }
                    request.QueryString.Add("year", year.ToString());
                    request.QueryString.Add("com_id", Main.IdAcount.ToString());
                    request.QueryString.Add("token", Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        Root_DMVS dimuonvesom = JsonConvert.DeserializeObject<Root_DMVS>(UnicodeEncoding.UTF8.GetString(e.Result));
                        if (dimuonvesom != null && dimuonvesom.data != null)
                        {

                            foreach (var item in dimuonvesom.data)
                            {
                                item.date_format = $"{item.date.Day}-{item.date.Month}-{item.date.Year}";
                                item.shift_name = item.shift.shift_name;
                                if (item.user.organizeDetailName == null || item.user.organizeDetailName == "")
                                {
                                    item.organizeDetailName = "Chưa cập nhật";
                                }
                                else
                                {
                                    item.organizeDetailName = item.user.organizeDetailName;
                                }
                                //item.ep_idCom = item.pm_info.pm_id_com.ToString();
                                if (!string.IsNullOrEmpty(item.user.avatarUser))
                                {
                                    item.avatarUser = item.user.avatarUser;
                                }   
                                else
                                {
                                    item.avatarUser = "https://hungha365.com/avt_365.png";
                                }
                                if (item.addition.late > 0)
                                {
                                    if (item.addition.late >= 60)
                                    {
                                        Minute = (int)item.addition.late_second / 60;
                                        surplus = (int)item.addition.late_second % 60;
                                        item.addition.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
                                    }
                                }
                                else
                                {
                                    item.addition.late_second_string = "0";
                                }
                                if (item.addition.early > 0)
                                {
                                    if (item.addition.early_second >= 60)
                                    {
                                        Minute = (int)item.addition.early_second / 60;
                                        surplus = (int)item.addition.early_second % 60;
                                        item.addition.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
                                    }
                                }
                                if (item.pm_info == null && item.cong > 0)
                                {
                                    item.pm_monney_formatted = item.cong.ToString() + " Công";
                                    
                                }
                                else if (item.pm_info != null)
                                {
                                    item.pm_monney_formatted = item.pm_info.pm_monney_formatted + "VNĐ";
                                }
                                else
                                {
                                    item.pm_monney_formatted = "0";
                                }
                                lstDMVS.Add(item);
                            }
                            if (lstDMVS.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                            else DpPhanTRang.Visibility = Visibility.Visible;
                            TongSoTrang = lstDMVS.Count / 10;
                            SoDu = 10 - (lstDMVS.Count % 10);
                            if (lstDMVS.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            for (int i = 0; i < 10 && i < lstDMVS.Count; i++)
                            {
                                lstDMVSFilter.Add(lstDMVS[i]);
                            }
                            dgv.ItemsSource = lstDMVSFilter;
                            dgv.Items.Refresh();
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
                        
                    };
                    request.UploadValuesTaskAsync("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late", request.QueryString);
                }
                tb_MuonSom = Function.clsExPortExcel.NewTables("tb_MuonSom", new string[] { "colId", "colHoTen", "colPhongBan", "colThoiGian", "colCaLV", "colMuonSom", "colPhat" }, new int[] { 150, 250, 300, 250, 250, 200, 150 });
               
                GetDataToDataTable();
            }
            catch
            {
                loading.Visibility = Visibility.Collapsed;
            }
        }

        private void GetDataToDataTable()
        {
            DataRow row1 = tb_MuonSom.NewRow();
            row1["colId"] = "ID";
            row1["colHoTen"] = "Tên";
            row1["colPhongBan"] = "Tổ chức";
            row1["colThoiGian"] = "Thời gian";
            row1["colCaLV"] = "Ca làm việc";
            row1["colMuonSom"] = "Muộn/sớm";
            row1["colPhat"] = "Phạt";
            tb_MuonSom.Rows.Add(row1);
            foreach (var item in lstDMVS)
            {
                DataRow row = tb_MuonSom.NewRow();
                //row["colId"] = item.ep_id;
                //row["colHoTen"] = item.ep_name;
                //row["colPhongBan"] = item.Dep_name;
                //row["colThoiGian"] = item.display_ts_date;
                //row["colCaLV"] = item.Shift_name;
                //row["colMuonSom"] = item.late_second_string;
                //row["colPhat"] = item.monetary_fine;
                tb_MuonSom.Rows.Add(row);
            }
        }
        private void LoadDLNhanVien()
        {
            lstNhanVien = Main.lstNhanVienThuocCongTy;

            lsvNhanVien.ItemsSource = lstNhanVien;
            //lsvNhanVien.Items.Add("Tất cả nhân viên");
            //lsvNhanVien.Items.Add("(111788) Đỗ Văn Hoàng");
            //lsvNhanVien.Items.Add("(90229) Nguyễn Công Tiến");
        }

        private void LoadDLPhongBan()
        {
            lstPhongBan = Main.lstPhongBanThuocCongTy;
            textHienThiPhongBan.ItemsSource = Main.lstOrganizeData;
        }

        private void LoadDLThang()
        {
            //textHienThiThang.Text = "Tháng " + DateTime.Now.Month.ToString();
            List<string> listThang = new List<string>();
            listThang.Add("Tháng 1");
            listThang.Add("Tháng 2");
            listThang.Add("Tháng 3");
            listThang.Add("Tháng 4");
            listThang.Add("Tháng 5");
            listThang.Add("Tháng 6");
            listThang.Add("Tháng 7");
            listThang.Add("Tháng 8");
            listThang.Add("Tháng 9");
            listThang.Add("Tháng 10");
            listThang.Add("Tháng 11");
            listThang.Add("Tháng 12");
            lsvThang.ItemsSource = listThang;
            lsvThang.SelectedIndex = DateTime.Now.Month - 1;
            lsvThang.SelectedItem = listThang[DateTime.Now.Month - 1];
            lsvThang.PlaceHolder = listThang[DateTime.Now.Month - 1];
        }

        private void LoadDLNam()
        {
            //textHienThiNam.Text = "Năm " + DateTime.Now.Year.ToString();
            List<string> listNam = new List<string>();
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 2).ToString());
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
            listNam.Add("Năm " + DateTime.Now.Year);

            lsvNam.ItemsSource = listNam;
            lsvNam.SelectedIndex = 2;
            lsvNam.SelectedItem = listNam[2];
            lsvNam.PlaceHolder = listNam[2];
        }

        //private void btnHienThiNam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (borNam.Visibility == Visibility.Collapsed)
        //    {
        //        borHienThiNam.CornerRadius = new CornerRadius(5, 5, 0, 0);
        //        borNam.Visibility = Visibility.Visible;
        //        popup.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //        borNam.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void lsvNam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    scrollNam.ScrollToVerticalOffset(scrollNam.VerticalOffset - e.Delta);
        //}

        //private void textSearchNam_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    lstSearchNam = new List<Nam>();
        //    foreach (var str in lstNam)
        //    {
        //        if (str.nam.Contains(textSearchNam.Text.ToString()))
        //        {
        //            lstSearchNam.Add(str);

        //        }
        //    }
        //    lsvNam.ItemsSource = lstSearchNam;
        //    if (textSearchNam.Text == "")
        //    {
        //        lsvNam.ItemsSource = lstNam;
        //    }
        //}

        //private void lsvNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    textHienThiNam.Text = lsvNam.SelectedItem.ToString();
        //    borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //    borNam.Visibility = Visibility.Collapsed;
        //    popup.Visibility = Visibility.Collapsed;
        //    Main.Nam = lsvNam.SelectedItem.ToString();
        //}

        //private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (borThang.Visibility == Visibility.Visible)
        //    {
        //        borThang.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //        borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //    }
        //    else if (borNam.Visibility == Visibility.Visible)
        //    {
        //        borNam.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //        borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //    }
        //    else if (borPhongBan.Visibility == Visibility.Visible)
        //    {
        //        borPhongBan.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //        borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //    }
        //    else if (borNhanVien.Visibility == Visibility.Visible)
        //    {
        //        borNhanVien.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //        borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //    }
        //}
        //private void btnHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (borThang.Visibility == Visibility.Collapsed)
        //    {
        //        borHienThiThang.CornerRadius = new CornerRadius(5, 5, 0, 0);
        //        borThang.Visibility = Visibility.Visible;
        //        popup.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //        borThang.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void lsvThang_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    scrollThang.ScrollToVerticalOffset(scrollThang.VerticalOffset - e.Delta);
        //}

        //private void lsvThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    textHienThiThang.Text = lsvThang.SelectedItem.ToString();
        //    borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //    borThang.Visibility = Visibility.Collapsed;
        //    popup.Visibility = Visibility.Collapsed;
        //    Main.Thang = lsvThang.SelectedItem.ToString();
        //}

        //private void textSearchThang_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    lstSearchThang = new List<Thang>();
        //    foreach (var str in lstThang)
        //    {
        //        if (str.thang.Contains(textSearchThang.Text.ToString()))
        //        {
        //            lstSearchThang.Add(str);

        //        }
        //    }
        //    lsvThang.ItemsSource = lstSearchThang;
        //    if (textSearchThang.Text == "")
        //    {
        //        lsvThang.ItemsSource = lstThang;
        //    }
        //}

        //private void lsvPhongBan_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (sender is ListView && !e.Handled)
        //    {
        //        e.Handled = true;
        //        var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
        //        eventArg.RoutedEvent = UIElement.MouseWheelEvent;
        //        eventArg.Source = sender;
        //        var parent = ((Control)sender).Parent as UIElement;
        //        parent.RaiseEvent(eventArg);
        //    }
        //    //scrollPhongBan.ScrollToVerticalOffset(scrollPhongBan.VerticalOffset - e.Delta);
        //}


        //private void borHienThiPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (borPhongBan.Visibility == Visibility.Collapsed)
        //    {
        //        borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 0, 0);
        //        borPhongBan.Visibility = Visibility.Visible;
        //        popup.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //        borPhongBan.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void lsvNhanVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    scrollNhanVien.ScrollToVerticalOffset(scrollNhanVien.VerticalOffset - e.Delta);
        //}

        

        //private void borHienThiNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        //{
        //    if (borNhanVien.Visibility == Visibility.Collapsed)
        //    {
        //        borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 0, 0);
        //        borNhanVien.Visibility = Visibility.Visible;
        //        popup.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //        borNhanVien.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void borTenPB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        //    OOP.clsPhongBanThuocCongTy.Item pb = (sender as Border).DataContext as OOP.clsPhongBanThuocCongTy.Item;
        //    if (pb != null)
        //    {
        //        borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //        borPhongBan.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //        textHienThiPhongBan.Text = pb.dep_name;
        //        Main.PhongBan = pb.dep_name;
        //        IdPB = pb.dep_id.ToString();
        //        if (IdPB != "0")
        //        {
        //            foreach (var nv in Main.lstNhanVienThuocCongTy)
        //            {
        //                if (nv.department != null)
        //                {
        //                    foreach (var item in nv.department)
        //                    {
        //                        if (IdPB == item.dep_id)
        //                        {
        //                            lstSearchNV.Add(nv);
        //                        }
        //                    }
        //                }

        //            }
        //            lsvNhanVien.ItemsSource = lstSearchNV;
        //        }
        //        else
        //        {
        //            lsvNhanVien.ItemsSource = lstNhanVien;
        //        }
                
        //    }
        //}

        //private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        //    foreach (var str in lstNhanVien)
        //    {
        //        if (str.userName.Contains(textSearchNhanVien.Text.ToString()))
        //        {
        //            lstSearchNV.Add(str);

        //        }
        //    }
        //    lsvNhanVien.ItemsSource = lstSearchNV;
        //    if (textSearchNhanVien.Text == "")
        //    {
        //        lsvNhanVien.ItemsSource = lstNhanVien;
        //    }
        //}

        //private void borThang_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    Thang th = (sender as Border).DataContext as Thang;
        //    if (th != null)
        //    {
        //        borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //        borThang.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //        textHienThiThang.Text = th.thang;
        //        Main.Thang = th.thang;
        //    }
        //}

        //private void borNam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    Nam th = (sender as Border).DataContext as Nam;
        //    if (th != null)
        //    {
        //        borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
        //        borNam.Visibility = Visibility.Collapsed;
        //        popup.Visibility = Visibility.Collapsed;
        //        textHienThiNam.Text = th.nam;
        //        Main.Nam = th.nam;
        //    }
        //}

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void WrapPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void DockPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //string[] nam = lsvNam.SelectedItem.ToString().Split(new[] { "Năm " }, StringSplitOptions.None);
            //int HtNam = int.Parse(nam[nam.Length - 1]);
            //string[] thang = lsvThang.SelectedItem.ToString().Split(new[] { "Tháng " }, StringSplitOptions.None);
            //int HtThang = int.Parse(thang[thang.Length - 1]);
            //if (IdPB == "0" && IdNV == "0")
            //{
            //    lstDMVS = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
            //    lstDMVSFilter = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
            //    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late")))
            //    {
            //        RestRequest request = new RestRequest();
            //        request.Method = Method.Post;
            //        request.AlwaysMultipartFormData = true;
            //        if (HtThang < 10)
            //        {
            //            request.AddParameter("start_date", $"{HtNam}-0{HtThang}-01T00:00:00.000+00:00");
            //        }
            //        else
            //        {
            //            request.AddParameter("start_date", $"{HtNam}-{HtThang}-01T00:00:00.000+00:00");
            //        }
            //        if (HtThang == 12)
            //        {
            //            request.AddParameter("end_date", $"{HtNam + 1}-01-01T00:00:00.000+00:00");

            //        }
            //        else
            //        {
            //            if (HtThang < 10)
            //            {
            //                request.AddParameter("end_date", $"{HtNam}-0{HtThang + 1}-01T00:00:00.000+00:00");
            //            }
            //            else
            //            {
            //                request.AddParameter("end_date", $"{HtNam}-{HtThang + 1}-01T00:00:00.000+00:00");
            //            }

            //        }

            //        request.AddParameter("com_id", Main.IdAcount);
            //        request.AddParameter("token", Properties.Settings.Default.Token);
            //        RestResponse resAlbum = restclient.Execute(request);
            //        var b = resAlbum.Content;
            //        OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root dimuonvesom = JsonConvert.DeserializeObject<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root>(b);
            //        if (dimuonvesom.data.list_data_late_early != null)
            //        {

            //            foreach (var item in dimuonvesom.data.list_data_late_early)
            //            {
            //                foreach (var shift in dimuonvesom.data.listShiftDetail)
            //                {
            //                    if (shift.shift_id == item.shift_id)
            //                    {
            //                        item.Shift_name = shift.shift_name;
            //                    }
            //                }
            //                foreach (var dep in dimuonvesom.data.listUserDetail)
            //                {
            //                    if (item.ep_id == dep.idQLC)
            //                    {
            //                        item.Dep_name = dep.Deparment.dep_name;
            //                        item.ep_idCom = dep.inForPerson.employee.com_id;
            //                        item.ep_idChat = dep._id.ToString();
            //                    }
            //                }
            //                WebClient httpClient2 = new WebClient();
            //                httpClient2.QueryString.Clear();
            //                httpClient2.QueryString.Add("ep_id", item.ep_id.ToString());
            //                httpClient2.QueryString.Add("cp", item.ep_idCom.ToString());
            //                httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
            //                var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
            //                OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
            //                if (receiveInfoAPI.data != null)
            //                {
            //                    item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
            //                }
            //                //WebClient httpClient2 = new WebClient();
            //                //httpClient2.QueryString.Clear();
            //                //httpClient2.QueryString.Add("ID", item.ep_idChat.ToString());

            //                //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
            //                //OOP.CaiDatLuong.CaiDatLuongCB.APIUser receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.APIUser>(UnicodeEncoding.UTF8.GetString(response));
            //                //if (receiveInfoAPI.data != null)
            //                //{
            //                //    item.Avatar_Us = receiveInfoAPI.data.user_info.AvatarUser;
            //                //}
            //                foreach (var tp in dimuonvesom.data.tien_phat_muon)
            //                {
            //                    if (tp.sheet_id == item.sheet_id)
            //                    {
            //                        item.monetary_fine = tp.cong.ToString();
            //                    }
            //                }

            //                if (item.late > 0)
            //                {
            //                    if (item.late_second >= 60)
            //                    {
            //                        Minute = (int)item.late_second / 60;
            //                        surplus = (int)item.late_second % 60;
            //                        item.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
            //                    }
            //                }

            //                if (item.early > 0)
            //                {
            //                    if (item.early_second >= 60)
            //                    {
            //                        Minute = (int)item.early_second / 60;
            //                        surplus = (int)item.early_second % 60;
            //                        item.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
            //                    }
            //                }

            //                lstDMVS.Add(item);
            //            }
            //            dgv.ItemsSource = lstDMVS;
            //        }
            //    }

            //}
            //else if (IdPB != "0" && IdNV == "0")
            //{
            //    lstDMVS = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
            //    lstDMVSFilter = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
            //    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late")))
            //    {
            //        RestRequest request = new RestRequest();
            //        request.Method = Method.Post;
            //        request.AlwaysMultipartFormData = true;
            //        if (HtThang < 10)
            //        {
            //            request.AddParameter("start_date", $"{HtNam}-0{HtThang}-01T00:00:00.000+00:00");
            //        }
            //        else
            //        {
            //            request.AddParameter("start_date", $"{HtNam}-{HtThang}-01T00:00:00.000+00:00");
            //        }
            //        if (HtThang == 12)
            //        {
            //            request.AddParameter("end_date", $"{HtNam + 1}-01-01T00:00:00.000+00:00");

            //        }
            //        else
            //        {
            //            if (HtThang < 10)
            //            {
            //                request.AddParameter("end_date", $"{HtNam}-0{HtThang + 1}-01T00:00:00.000+00:00");
            //            }
            //            else
            //            {
            //                request.AddParameter("end_date", $"{HtNam}-{HtThang + 1}-01T00:00:00.000+00:00");
            //            }

            //        }

            //        request.AddParameter("com_id", Main.IdAcount);
            //        request.AddParameter("token", Properties.Settings.Default.Token);
            //        RestResponse resAlbum = restclient.Execute(request);
            //        var b = resAlbum.Content;
            //        OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root dimuonvesom = JsonConvert.DeserializeObject<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root>(b);
            //        if (dimuonvesom.data.list_data_late_early != null)
            //        {

            //            foreach (var item in dimuonvesom.data.list_data_late_early)
            //            {
            //                foreach (var shift in dimuonvesom.data.listShiftDetail)
            //                {
            //                    if (shift.shift_id == item.shift_id)
            //                    {
            //                        item.Shift_name = shift.shift_name;
            //                    }
            //                }
            //                foreach (var dep in dimuonvesom.data.listUserDetail)
            //                {
            //                    if (item.ep_id == dep.idQLC)
            //                    {
            //                        item.Dep_Id = dep.Deparment.dep_id;
            //                        item.Dep_name = dep.Deparment.dep_name;
            //                        item.ep_idCom = dep.inForPerson.employee.com_id;
            //                        item.ep_idChat = dep._id;
            //                    }
            //                }
            //                WebClient httpClient2 = new WebClient();
            //                httpClient2.QueryString.Clear();
            //                httpClient2.QueryString.Add("ep_id", item.ep_id.ToString());
            //                httpClient2.QueryString.Add("cp", item.ep_idCom.ToString());
            //                httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
            //                var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
            //                OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
            //                if (receiveInfoAPI.data != null)
            //                {
            //                    item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
            //                }


            //                //WebClient httpClient2 = new WebClient();
            //                //httpClient2.QueryString.Clear();
            //                //httpClient2.QueryString.Add("ID", item.ep_idChat.ToString());

            //                //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
            //                //OOP.CaiDatLuong.CaiDatLuongCB.APIUser receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.APIUser>(UnicodeEncoding.UTF8.GetString(response));
            //                //if (receiveInfoAPI.data != null)
            //                //{
            //                //    item.Avatar_Us = receiveInfoAPI.data.user_info.AvatarUser;
            //                //}
            //                foreach (var tp in dimuonvesom.data.tien_phat_muon)
            //                {
            //                    if (tp.sheet_id == item.sheet_id)
            //                    {
            //                        item.monetary_fine = tp.cong.ToString();
            //                    }
            //                }

            //                if (item.late > 0)
            //                {
            //                    if (item.late_second >= 60)
            //                    {
            //                        Minute = (int)item.late_second / 60;
            //                        surplus = (int)item.late_second % 60;
            //                        item.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
            //                    }
            //                }

            //                if (item.early > 0)
            //                {
            //                    if (item.early_second >= 60)
            //                    {
            //                        Minute = (int)item.early_second / 60;
            //                        surplus = (int)item.early_second % 60;
            //                        item.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
            //                    }
            //                }

            //                lstDMVS.Add(item);
            //            }
            //            foreach(var item in lstDMVS)
            //            {
            //                if (item.Dep_Id == int.Parse(IdPB))
            //                {
            //                    lstDMVSFilter.Add(item);
            //                }
            //            }
            //            dgv.ItemsSource = null;
            //            dgv.ItemsSource = lstDMVSFilter;
            //        }
            //    }

            //}
            //else if (IdPB == "0" && IdNV != "0")
            //{
            //    lstDMVS = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
            //    lstDMVSFilter = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
            //    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late")))
            //    {
            //        RestRequest request = new RestRequest();
            //        request.Method = Method.Post;
            //        request.AlwaysMultipartFormData = true;
            //        if (HtThang < 10)
            //        {
            //            request.AddParameter("start_date", $"{HtNam}-0{HtThang}-01T00:00:00.000+00:00");
            //        }
            //        else
            //        {
            //            request.AddParameter("start_date", $"{HtNam}-{HtThang}-01T00:00:00.000+00:00");
            //        }
            //        if (HtThang == 12)
            //        {
            //            request.AddParameter("end_date", $"{HtNam + 1}-01-01T00:00:00.000+00:00");

            //        }
            //        else
            //        {
            //            if (HtThang < 10)
            //            {
            //                request.AddParameter("end_date", $"{HtNam}-0{HtThang + 1}-01T00:00:00.000+00:00");
            //            }
            //            else
            //            {
            //                request.AddParameter("end_date", $"{HtNam}-{HtThang + 1}-01T00:00:00.000+00:00");
            //            }

            //        }

            //        request.AddParameter("com_id", Main.IdAcount);
            //        request.AddParameter("token", Properties.Settings.Default.Token);
            //        RestResponse resAlbum = restclient.Execute(request);
            //        var b = resAlbum.Content;
            //        OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root dimuonvesom = JsonConvert.DeserializeObject<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root>(b);
            //        if (dimuonvesom.data.list_data_late_early != null)
            //        {

            //            foreach (var item in dimuonvesom.data.list_data_late_early)
            //            {
            //                foreach (var shift in dimuonvesom.data.listShiftDetail)
            //                {
            //                    if (shift.shift_id == item.shift_id)
            //                    {
            //                        item.Shift_name = shift.shift_name;
            //                    }
            //                }
            //                foreach (var dep in dimuonvesom.data.listUserDetail)
            //                {
            //                    if (item.ep_id == dep.idQLC)
            //                    {
            //                        item.Dep_Id = dep.Deparment.dep_id;
            //                        item.Dep_name = dep.Deparment.dep_name;
            //                        item.ep_idCom = dep.inForPerson.employee.com_id;
            //                        item.ep_idChat = dep._id;
            //                    }
            //                }
            //                WebClient httpClient2 = new WebClient();
            //                httpClient2.QueryString.Clear();
            //                httpClient2.QueryString.Add("ep_id", item.ep_id.ToString());
            //                httpClient2.QueryString.Add("cp", item.ep_idCom.ToString());
            //                httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
            //                var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
            //                OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
            //                if (receiveInfoAPI.data != null)
            //                {
            //                    item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
            //                }


            //                //WebClient httpClient2 = new WebClient();
            //                //httpClient2.QueryString.Clear();
            //                //httpClient2.QueryString.Add("ID", item.ep_idChat.ToString());

            //                //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
            //                //OOP.CaiDatLuong.CaiDatLuongCB.APIUser receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.APIUser>(UnicodeEncoding.UTF8.GetString(response));
            //                //if (receiveInfoAPI.data != null)
            //                //{
            //                //    item.Avatar_Us = receiveInfoAPI.data.user_info.AvatarUser;
            //                //}
            //                foreach (var tp in dimuonvesom.data.tien_phat_muon)
            //                {
            //                    if (tp.sheet_id == item.sheet_id)
            //                    {
            //                        item.monetary_fine = tp.cong.ToString();
            //                    }
            //                }

            //                if (item.late > 0)
            //                {
            //                    if (item.late_second >= 60)
            //                    {
            //                        Minute = (int)item.late_second / 60;
            //                        surplus = (int)item.late_second % 60;
            //                        item.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
            //                    }
            //                }

            //                if (item.early > 0)
            //                {
            //                    if (item.early_second >= 60)
            //                    {
            //                        Minute = (int)item.early_second / 60;
            //                        surplus = (int)item.early_second % 60;
            //                        item.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
            //                    }
            //                }

            //                lstDMVS.Add(item);
            //            }
            //            foreach (var item in lstDMVS)
            //            {
            //                if (item.ep_id == IdNV)
            //                {
            //                    lstDMVSFilter.Add(item);
            //                }
            //            }
            //            dgv.ItemsSource = null;
            //            dgv.ItemsSource = lstDMVSFilter;
            //        }
            //    }

            //}
            //else if (IdPB != "0" && IdNV != "0")
            //{
            //    lstDMVS = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
            //    lstDMVSFilter = new List<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.ListDataLateEarly>();
            //    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_staff_late")))
            //    {
            //        RestRequest request = new RestRequest();
            //        request.Method = Method.Post;
            //        request.AlwaysMultipartFormData = true;
            //        if (HtThang < 10)
            //        {
            //            request.AddParameter("start_date", $"{HtNam}-0{HtThang}-01T00:00:00.000+00:00");
            //        }
            //        else
            //        {
            //            request.AddParameter("start_date", $"{HtNam}-{HtThang}-01T00:00:00.000+00:00");
            //        }
            //        if (HtThang == 12)
            //        {
            //            request.AddParameter("end_date", $"{HtNam + 1}-01-01T00:00:00.000+00:00");

            //        }
            //        else
            //        {
            //            if (HtThang < 10)
            //            {
            //                request.AddParameter("end_date", $"{HtNam}-0{HtThang + 1}-01T00:00:00.000+00:00");
            //            }
            //            else
            //            {
            //                request.AddParameter("end_date", $"{HtNam}-{HtThang + 1}-01T00:00:00.000+00:00");
            //            }

            //        }

            //        request.AddParameter("com_id", Main.IdAcount);
            //        request.AddParameter("token", Properties.Settings.Default.Token);
            //        RestResponse resAlbum = restclient.Execute(request);
            //        var b = resAlbum.Content;
            //        OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root dimuonvesom = JsonConvert.DeserializeObject<OOP.CaiDatDiMuonVeSom.clsDSDiMuonVeSom.Root>(b);
            //        if (dimuonvesom.data.list_data_late_early != null)
            //        {

            //            foreach (var item in dimuonvesom.data.list_data_late_early)
            //            {
            //                foreach (var shift in dimuonvesom.data.listShiftDetail)
            //                {
            //                    if (shift.shift_id == item.shift_id)
            //                    {
            //                        item.Shift_name = shift.shift_name;
            //                    }
            //                }
            //                foreach (var dep in dimuonvesom.data.listUserDetail)
            //                {
            //                    if (item.ep_id == dep.idQLC)
            //                    {
            //                        item.Dep_Id = dep.Deparment.dep_id;
            //                        item.Dep_name = dep.Deparment.dep_name;
            //                        item.ep_idCom = dep.inForPerson.employee.com_id;
            //                        item.ep_idChat = dep._id;
            //                    }
            //                }
            //                WebClient httpClient2 = new WebClient();
            //                httpClient2.QueryString.Clear();
            //                httpClient2.QueryString.Add("ep_id", item.ep_id.ToString());
            //                httpClient2.QueryString.Add("cp", item.ep_idCom.ToString());
            //                httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
            //                var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
            //                OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
            //                if (receiveInfoAPI.data != null)
            //                {
            //                    item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
            //                }


            //                //WebClient httpClient2 = new WebClient();
            //                //httpClient2.QueryString.Clear();
            //                //httpClient2.QueryString.Add("ID", item.ep_idChat.ToString());

            //                //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
            //                //OOP.CaiDatLuong.CaiDatLuongCB.APIUser receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.APIUser>(UnicodeEncoding.UTF8.GetString(response));
            //                //if (receiveInfoAPI.data != null)
            //                //{
            //                //    item.Avatar_Us = receiveInfoAPI.data.user_info.AvatarUser;
            //                //}
            //                foreach (var tp in dimuonvesom.data.tien_phat_muon)
            //                {
            //                    if (tp.sheet_id == item.sheet_id)
            //                    {
            //                        item.monetary_fine = tp.cong.ToString();
            //                    }
            //                }

            //                if (item.late > 0)
            //                {
            //                    if (item.late_second >= 60)
            //                    {
            //                        Minute = (int)item.late_second / 60;
            //                        surplus = (int)item.late_second % 60;
            //                        item.late_second_string = "Đi muộn " + Minute + " phút " + surplus + " giây";
            //                    }
            //                }

            //                if (item.early > 0)
            //                {
            //                    if (item.early_second >= 60)
            //                    {
            //                        Minute = (int)item.early_second / 60;
            //                        surplus = (int)item.early_second % 60;
            //                        item.early_second_string = "Về sớm " + Minute + " phút " + surplus + " giây";
            //                    }
            //                }

            //                lstDMVS.Add(item);
            //            }
            //            foreach (var item in lstDMVS)
            //            {
            //                if (item.ep_id == IdNV)
            //                {
            //                    lstDMVSFilter.Add(item);
            //                }
            //            }
            //            dgv.ItemsSource = null;
            //            dgv.ItemsSource = lstDMVSFilter;
            //        }
            //    }

            //}
            LoadDLDiMuonVeSom();
        }

        private void btnXuatExcel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string PathTemplate = "";
                var file = Properties.Resources.FileLuongCB;
                string path = $"{Environment.GetEnvironmentVariable("APPDATA")}/QuanLyChung365TruocDangNhap.ChamCongNew/";
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
                    string thang, nam;
                    if (!string.IsNullOrEmpty(lsvThang.Text))
                    {
                        thang = lsvThang.Text;
                    }
                    else thang = DateTime.Now.Month.ToString();
                    ws_HoaDon.Name = "Danh sách nhân viên đi muộn " + thang;
                    ws_HoaDon.Cells[1, 1] = Main.data.data.user_info.com_name;
                    ws_HoaDon.Cells[2, 1] = ws_HoaDon.Name;
                    int DongBatDau = 3;
                    foreach (DataRow row in tb_MuonSom.Rows)
                    {
                        for (int i = 0; i < tb_MuonSom.Columns.Count; i++)
                        {
                            ws_HoaDon.Cells[DongBatDau, i + 1] = row[i];
                        }
                        DongBatDau++;
                    }
                    System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
                    frm.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";
                    frm.FileName = ws_HoaDon.Name;
                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
            OOP.OrganizeData tc = (OOP.OrganizeData)textHienThiPhongBan.SelectedItem;
            if(textHienThiPhongBan.SelectedItem != null)
            {
                textHienThiPhongBan.PlaceHolderForground = "#474747";
            }
            else
            {
                textHienThiPhongBan.PlaceHolderForground = "#ACACAC";
            }
            lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            if(tc.id == 0)
            {
                lsvNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
            }
            else
            {
                foreach (var item in Main.lstNhanVienThuocCongTy)
                {
                    if (item.organizeDetail != null && item.organizeDetail.listOrganizeDetailId.Find(x => x.organizeDetailId == tc.id) != null)
                    {
                        lstNhanVien.Add(item);
                    }
                }
                lsvNhanVien.ItemsSource = lstNhanVien;
            }
        }
        private void Staff_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if(lsvNhanVien.SelectedItem != null)
            {
                lsvNhanVien.PlaceHolderForground = "#474747";
            }
            else
            {
                lsvNhanVien.PlaceHolderForground = "#ACACAC";
            }
        }

        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                lstDMVSFilter = new List<Data_DMVS>();
                for (int i = 0; i < 10; i++)
                {
                    lstDMVSFilter.Add(lstDMVS[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgv.ItemsSource = lstDMVSFilter;
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
                lstDMVSFilter = new List<Data_DMVS>();
                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDMVS.Count; i++)
                {
                    lstDMVSFilter.Add(lstDMVS[i]);
                }
                dgv.ItemsSource = lstDMVSFilter;
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
                    lstDMVSFilter = new List<Data_DMVS>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDMVS.Count; i++)
                    {
                        lstDMVSFilter.Add(lstDMVS[i]);
                    }
                    dgv.ItemsSource = lstDMVSFilter;
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
                            lstDMVSFilter = new List<Data_DMVS>();
                            for (int i = 0; i < 10; i++)
                            {
                                lstDMVSFilter.Add(lstDMVS[i]);
                            }
                            dgv.ItemsSource = lstDMVSFilter;
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
                            lstDMVSFilter = new List<Data_DMVS>();
                            if (lstDMVS.Count > 10)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDMVS.Count; i++)
                                {
                                    lstDMVSFilter.Add(lstDMVS[i]);
                                }
                                dgv.ItemsSource = lstDMVSFilter;
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
                    lstDMVSFilter = new List<Data_DMVS>();
                    if (lstDMVS.Count > 10)
                    {
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDMVS.Count; i++)
                        {
                            lstDMVSFilter.Add(lstDMVS[i]);
                        }
                        dgv.ItemsSource = lstDMVSFilter;
                    }
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
                        lstDMVSFilter = new List<Data_DMVS>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDMVS.Count; i++)
                        {
                            lstDMVSFilter.Add(lstDMVS[i]);
                        }
                        dgv.ItemsSource = lstDMVSFilter;
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
                            lstDMVSFilter = new List<Data_DMVS>();
                            for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                            {
                                lstDMVSFilter.Add(lstDMVS[i]);
                            }
                            dgv.ItemsSource = lstDMVSFilter;
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
                            lstDMVSFilter = new List<Data_DMVS>();
                            if (lstDMVS.Count > 10)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDMVS.Count; i++)
                                {
                                    lstDMVSFilter.Add(lstDMVS[i]);
                                }
                                dgv.ItemsSource = lstDMVSFilter;
                            }
                        }
                    }
                }

            }
            catch { }
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
            lstDMVSFilter = new List<Data_DMVS>();
            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstDMVS.Count; i++)
            {
                lstDMVSFilter.Add(lstDMVS[i]);
            }
            dgv.ItemsSource = lstDMVSFilter;
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
                lstDMVSFilter = new List<Data_DMVS>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstDMVSFilter.Add(lstDMVS[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgv.ItemsSource = lstDMVSFilter;
            }
            catch (Exception)
            {
            }

        }
    }
}
