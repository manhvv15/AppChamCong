using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ChamCong365.SalarySettings.CaiDatThue
{
    /// <summary>
    /// Interaction logic for frmDanhSachNhanSuChuaThietLap.xaml
    /// </summary>
    public partial class frmDanhSachNhanSuChuaThietLap : Page
    {
        MainWindow Main;
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
        //private string SearchNV = "";
        //private string SearchPB = "";
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        public List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal> lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
        private List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal> lstChuaTLFilter = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
        private List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal> lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
        private string IdNV = "0";
        private string IdPB = "0";
        private int TongSoTrang = 0;
        private int SoDu = 0;
        public  List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal> lstChuaTL = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
        public frmDanhSachNhanSuChuaThietLap(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDLNhanSuChuaThietLapThue();


        }

        

        private void LoadDLNam()
        {
            textHienThiNam.Text = "Năm " + DateTime.Now.Year.ToString();
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lsvNam.ItemsSource = lstNam;
        }
        

        private void LoadDLNhanSuChuaThietLapThue()
        {

            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_list_user_no_tax")))
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

                    request.AddParameter("cls_id_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.Tax.clsNSuChuaTL.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsNSuChuaTL.Root>(b);
                    if (chuaTL.listUserFinal != null)
                    {
                        foreach (var item in chuaTL.listUserFinal)
                        {

                            item.avatarUser = "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser;
                            lstChuaTL.Add(item);
                        }
                        if (lstChuaTL.Count > 10)
                        {
                            TongSoTrang = chuaTL.listUserFinal.Count / 10;
                            SoDu = 10 - (chuaTL.listUserFinal.Count % 10);
                            if (chuaTL.listUserFinal.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            if (TongSoTrang < 3)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                            }
                            for (int i = 0; i < 10; i++)
                            {
                                lstChuaTLPT.Add(chuaTL.listUserFinal[i]);
                            }
                            //lstLuongCB = luongCB.listResult;
                            lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;

                            docPhanTrang.Visibility = Visibility.Visible;

                        }
                        else
                        {
                            lsvDSNSChuaTL.ItemsSource = lstChuaTL;
                            docPhanTrang.Visibility = Visibility.Collapsed;
                        }
                        //lsvDSNSChuaTL.ItemsSource = lstChuaTL;
                    }
                }

            }
            catch
            {

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
                //SearchNV = nv.idQLC.ToString();
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
        private void lsvDSNSChuaTL_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnThietLap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //ChuaTLThue chuaTL = (sender as Border).DataContext as ChuaTLThue;
            //if (chuaTL != null)
            //{
            //    Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpThietLapThueNhanVien(Main, chuaTL.Anh, chuaTL.ID, chuaTL.Ten, chuaTL.PhongBan));
            //}
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
            borPageCuoi.Visibility = Visibility.Visible;
            lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
            for (int i = 0; i < 10; i++)
            {
                lstChuaTLPT.Add(lstChuaTL[i]);
            }
            //lstLuongCB = luongCB.listResult;
            lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage3.Text == TongSoTrang.ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                {
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    if (textPage1.Text == "1")
                    {
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                        lstChuaTL = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                    }


                }
            }

        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(textPage1.Text) > 1)
            {
                if (textPage1.Text == (TongSoTrang - 2).ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                {
                    textPage1.Text = (TongSoTrang - 3).ToString();
                    textPage2.Text = (TongSoTrang - 2).ToString();
                    textPage3.Text = (TongSoTrang - 1).ToString();
                    BrushConverter brus = new BrushConverter();

                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borLen1.Visibility = Visibility.Visible;
                    borPageCuoi.Visibility = Visibility.Visible;
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                }
            }
            else
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
                borLen1.Visibility = Visibility.Visible;
                borPageCuoi.Visibility = Visibility.Visible;
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = 0; i < 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
            }
        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TongSoTrang == 2)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
            }
            else if (TongSoTrang > 2)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                borPageCuoi.Visibility = Visibility.Visible;
                borLen1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
            }
        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textPage3.Text == TongSoTrang.ToString())
            {
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();

                for (int i = int.Parse(textPage3.Text) * 10 - 10; i < int.Parse(textPage3.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
            }
            else
            {
                if (TongSoTrang == 3)
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                }
                else if (TongSoTrang > 3)
                {
                    if (textPage3.Text == TongSoTrang.ToString())
                    {
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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                    }
                    else if (textPage3.Text == "3")
                    {
                        textPage1.Text = "2";
                        textPage2.Text = "3";
                        textPage3.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                    }

                }
            }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TongSoTrang == 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
            }
            else if (TongSoTrang > 3)
            {
                if (textPage3.Text == TongSoTrang.ToString())
                {
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;

                }
                else if (textPage3.Text == "3")
                {

                    if (borPageDau.Visibility == Visibility.Collapsed && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = 10; i < 20; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;

                    }
                    else if (borPageDau.Visibility == Visibility.Visible && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        textPage1.Text = "2";
                        textPage2.Text = "3";
                        textPage3.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = 20; i < 30; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                }

            }
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borPageDau.Visibility = Visibility.Visible;
            borLui1.Visibility = Visibility.Visible;
            borPageCuoi.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            if (TongSoTrang >= 3)
            {
                textPage1.Text = (TongSoTrang - 2).ToString();
                textPage2.Text = (TongSoTrang - 1).ToString();
                textPage3.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
            }

            else
            {
                textPage1.Text = (TongSoTrang - 1).ToString();
                textPage2.Text = TongSoTrang.ToString();
                //textPage3S.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                //borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                //textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
            }
        }
        private void borPageDauS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPageDauS.Visibility = Visibility.Collapsed;
            borLui1S.Visibility = Visibility.Collapsed;
            borPage1S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            textPage1S.Text = "1";
            textPage2S.Text = "2";
            textPage3S.Text = "3";
            borLen1S.Visibility = Visibility.Visible;
            borPageCuoiS.Visibility = Visibility.Visible;
            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
            for (int i = 0; i < 10; i++)
            {
                lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            }
            //lstLuongCB = luongCB.listResult;
            lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
        }

        private void borLui1S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (int.Parse(textPage1S.Text) >= 1)
            {
                if (textPage3S.Text == TongSoTrang.ToString() && borPageCuoiS.Visibility == Visibility.Collapsed)
                {
                    borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoiS.Visibility = Visibility.Visible;
                    borLen1S.Visibility = Visibility.Visible;
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                }
                else
                {
                    if (textPage1S.Text == "1")
                    {
                        borPageDauS.Visibility = Visibility.Collapsed;
                        borLui1S.Visibility = Visibility.Collapsed;
                        borPage1S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borLen1S.Visibility = Visibility.Visible;
                        borPageCuoiS.Visibility = Visibility.Visible;
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                    }
                    else
                    {
                        textPage1S.Text = (int.Parse(textPage1S.Text) - 1).ToString();
                        textPage2S.Text = (int.Parse(textPage2S.Text) - 1).ToString();
                        textPage3S.Text = (int.Parse(textPage3S.Text) - 1).ToString();
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                    }


                }
            }
        }

        private void borPage1S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(textPage1S.Text) > 1)
            {
                if (textPage1S.Text == (TongSoTrang - 2).ToString() && borPageCuoiS.Visibility == Visibility.Collapsed)
                {
                    textPage1S.Text = (TongSoTrang - 3).ToString();
                    textPage2S.Text = (TongSoTrang - 2).ToString();
                    textPage3S.Text = (TongSoTrang - 1).ToString();
                    BrushConverter brus = new BrushConverter();

                    borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borLen1S.Visibility = Visibility.Visible;
                    borPageCuoiS.Visibility = Visibility.Visible;
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                }
                else
                {
                    textPage1S.Text = (int.Parse(textPage1S.Text) - 1).ToString();
                    textPage2S.Text = (int.Parse(textPage2S.Text) - 1).ToString();
                    textPage3S.Text = (int.Parse(textPage3S.Text) - 1).ToString();
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                }
            }
            else
            {
                BrushConverter brus = new BrushConverter();
                borPageDauS.Visibility = Visibility.Collapsed;
                borLui1S.Visibility = Visibility.Collapsed;
                borPage1S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borLen1S.Visibility = Visibility.Visible;
                borPageCuoiS.Visibility = Visibility.Visible;
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = 0; i < 10; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
            }
        }

        private void borPage2S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TongSoTrang == 2)
            {
                borPageDauS.Visibility = Visibility.Visible;
                borLui1S.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
            }
            else if (TongSoTrang > 2)
            {
                borPageDauS.Visibility = Visibility.Visible;
                borLui1S.Visibility = Visibility.Visible;
                borPageCuoiS.Visibility = Visibility.Visible;
                borLen1S.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
            }
        }

        private void borPage3S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textPage3S.Text == TongSoTrang.ToString())
            {
                BrushConverter brus = new BrushConverter();
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoiS.Visibility = Visibility.Collapsed;
                borLen1S.Visibility = Visibility.Collapsed;
                borPageDauS.Visibility = Visibility.Visible;
                borLui1S.Visibility = Visibility.Visible;
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();

                for (int i = int.Parse(textPage3S.Text) * 10 - 10; i < int.Parse(textPage3S.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
            }
            else
            {
                if (TongSoTrang == 3)
                {
                    borPageDauS.Visibility = Visibility.Visible;
                    borLui1S.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                }
                else if (TongSoTrang > 3)
                {
                    if (textPage3S.Text == TongSoTrang.ToString())
                    {
                        borPageDauS.Visibility = Visibility.Visible;
                        borLui1S.Visibility = Visibility.Visible;
                        BrushConverter brus = new BrushConverter();
                        borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPageCuoiS.Visibility = Visibility.Collapsed;
                        borLen1S.Visibility = Visibility.Collapsed;
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                    }
                    else if (textPage3S.Text == "3")
                    {
                        textPage1S.Text = "2";
                        textPage2S.Text = "3";
                        textPage3S.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                    }
                    else
                    {
                        textPage1S.Text = (int.Parse(textPage1S.Text) + 1).ToString();
                        textPage2S.Text = (int.Parse(textPage2S.Text) + 1).ToString();
                        textPage3S.Text = (int.Parse(textPage3S.Text) + 1).ToString();
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                    }

                }
            }
        }

        private void borLen1S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void borPageCuoiS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borPageDauS.Visibility = Visibility.Visible;
            borLui1S.Visibility = Visibility.Visible;
            borPageCuoiS.Visibility = Visibility.Collapsed;
            borLen1S.Visibility = Visibility.Collapsed;
            if (TongSoTrang >= 3)
            {
                textPage1S.Text = (TongSoTrang - 2).ToString();
                textPage2S.Text = (TongSoTrang - 1).ToString();
                textPage3S.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
            }

            else
            {
                textPage1S.Text = (TongSoTrang - 1).ToString();
                textPage2S.Text = TongSoTrang.ToString();
                //textPage3S.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                //borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                //textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
            }
        }





        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstChuaTLFilter = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
            if (IdPB == "0" && IdNV == "0")
            {
                if (lstChuaTL.Count > 10)
                {
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                    TongSoTrang = lstChuaTL.Count / 10;
                    SoDu = 10 - (lstChuaTL.Count % 10);
                    if (lstChuaTL.Count % 10 > 0)
                    {
                        TongSoTrang++;
                    }
                    if (TongSoTrang < 3)
                    {
                        borPage3S.Visibility = Visibility.Collapsed;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                    //if (TongSoTrang < 3)
                    //{
                    //    if (TongSoTrang == 2)
                    //    {
                    //        borPage3.Visibility = Visibility.Collapsed;
                    //        borLen1.Visibility = Visibility.Collapsed;
                    //        borPageCuoi.Visibility = Visibility.Collapsed;
                    //    }
                    //    else if (TongSoTrang == 1)
                    //    {
                    //        borPage2.Visibility = Visibility.Collapsed;
                    //        borPage3.Visibility = Visibility.Collapsed;
                    //        borLen1.Visibility = Visibility.Collapsed;
                    //        borPageCuoi.Visibility = Visibility.Collapsed;
                    //    }
                    //}
                    //else
                    //{
                    //    borLui1.Visibility = Visibility.Collapsed;
                    //    borPageDau.Visibility = Visibility.Collapsed;
                    //    borLen1.Visibility = Visibility.Visible;
                    //    borPageCuoi.Visibility = Visibility.Visible;
                    //}
                    docPhanTrang.Visibility = Visibility.Visible;
                    docPhanTrangS.Visibility = Visibility.Collapsed;

                }
                else
                {
                    lsvDSNSChuaTL.ItemsSource = lstChuaTL;
                    docPhanTrang.Visibility = Visibility.Collapsed;
                    docPhanTrangS.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                foreach (var item in lstChuaTL)
                {
                    if (item.idQLC.ToString() == IdNV && IdPB == "0")
                    {
                        lstChuaTLFilter.Add(item);
                    }
                    else if (IdNV == "0" && IdPB == item.department.dep_id.ToString())
                    {
                        lstChuaTLFilter.Add(item);
                    }
                    else if (IdNV == item.idQLC.ToString() && IdPB == item.department.dep_id.ToString())
                    {
                        lstChuaTLFilter.Add(item);
                    }
                }
                if (lstChuaTLFilter.Count > 10)
                {
                    docPhanTrang.Visibility = Visibility.Collapsed;
                    docPhanTrangS.Visibility = Visibility.Visible;
                    TongSoTrang = lstChuaTLFilter.Count / 10;
                    SoDu = 10 - (lstChuaTLFilter.Count % 10);
                    if (lstChuaTLFilter.Count % 10 > 0)
                    {
                        TongSoTrang++;
                    }
                    if (TongSoTrang < 3)
                    {
                        borPage3S.Visibility = Visibility.Collapsed;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                }
                else
                {
                    docPhanTrang.Visibility = Visibility.Collapsed;
                    docPhanTrangS.Visibility = Visibility.Collapsed;
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLFilter;
                }

            }
        }
    }
}
