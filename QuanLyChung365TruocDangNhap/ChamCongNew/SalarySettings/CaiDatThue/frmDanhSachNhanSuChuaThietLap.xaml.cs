using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong;
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
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using System.Net;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.CaiDatThue
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
        BrushConverter br = new BrushConverter();
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
        public List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal> lsvNhanVien1 = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
        string EndDate;
        public frmDanhSachNhanSuChuaThietLap(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            EndDate = EndDate = $"{DateTime.Now.Year}-{DateTime.Now.Month}";
            //LoadDLNhanSuChuaThietLapThue(EndDate);
            LoadDLNhanSuChuaThietLapThue(EndDate);
        }
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


        private void LoadDLNam()
        {
            textHienThiNam.Text = "Năm " + DateTime.Now.Year.ToString();
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lsvNam.ItemsSource = lstNam;
        }

        public void LoadDataNull()
        {
            foreach (OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal item in lstChuaTL)
            {
                if (item.avatarUser == "" || item.avatarUser == null)
                {
                    item.avatarUser = "https://chamcong.timviec365.vn/images/ep_logo.png";
                }
                else
                {
                    item.avatarUser = "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser;
                }
            }
        }

        public void LoadDLNhanSuChuaThietLapThue(string EndDate)
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    loading.Visibility = Visibility.Visible;
                    lstChuaTL.Clear();
                    lstChuaTLPT.Clear();
                    request.QueryString.Add("cls_id_com", Main.IdAcount.ToString());
                    request.QueryString.Add("start_date", $"{DateTime.Now.Year}-0{DateTime.Now.Month}");
                    request.QueryString.Add("end_date", EndDate);
                    request.QueryString.Add("token", Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        try
                        {
                            OOP.CaiDatLuong.Tax.clsNSuChuaTL.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsNSuChuaTL.Root>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (chuaTL.listUserFinal != null)
                            {
                                lstChuaTL = chuaTL.listUserFinal;
                                lsvNhanVien1 = chuaTL.listUserFinal;
                                //lsvNhanVien.ItemsSource = lsvNhanVien1;
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
                                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                                    docPhanTrang.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    lsvDSNSChuaTL.ItemsSource = lstChuaTL;
                                    docPhanTrang.Visibility = Visibility.Collapsed;
                                }
                            }
                            loading.Visibility = Visibility.Collapsed;
                        }
                        catch { }
                    };
                    request.UploadValuesTaskAsync("https://api.timviec365.vn/api/tinhluong/congty/show_list_user_no_tax",
                        request.QueryString);
                }
            }
            catch (Exception)
            {
            }
          
        }
       
        private void LoadDLNhanVien()
        {
            lstNhanVien = Main.lstNhanVienThuocCongTy;
            lsvNhanVien.ItemsSource = lstNhanVien;
        }

        private void LoadDLPhongBan()
        {
            //lstPhongBan = Main.lstPhongBanThuocCongTy;
            //lsvPhongBan.ItemsSource = lstPhongBan;
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
                borNam.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
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
            else if (borNhanVien.Visibility == Visibility.Visible)
            {
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        private void btnHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Collapsed)
            {
                borThang.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
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
        }


        private void borHienThiPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (borPhongBan.Visibility == Visibility.Collapsed)
            //{
            //    borPhongBan.Visibility = Visibility.Visible;
            //    popup.Visibility = Visibility.Visible;
            //}
            //else
            //{ 
            //    borPhongBan.Visibility = Visibility.Collapsed;
            //    popup.Visibility = Visibility.Collapsed;
            //}
        }

        private void borHienThiNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNhanVien.Visibility == Visibility.Collapsed)
            {
                borNhanVien.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        

        private void borTenPB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            //OOP.clsPhongBanThuocCongTy.Item pb = (sender as Border).DataContext as OOP.clsPhongBanThuocCongTy.Item;
            //if (pb != null)
            //{
            //    borPhongBan.Visibility = Visibility.Collapsed;
            //    popup.Visibility = Visibility.Collapsed;
            //    Main.PhongBan = pb.dep_name;
            //    IdPB = pb.dep_id.ToString();
            //    if (IdPB != "0")
            //    {
            //        foreach (var nv in Main.lstNhanVienThuocCongTy)
            //        {
            //            if (nv.department != null)
            //            {
            //                foreach (var item in nv.department)
            //                {
            //                    if (IdPB == item.dep_id)
            //                    {
            //                        lstSearchNV.Add(nv);
            //                    }
            //                }
            //            }
            //        }
            //        lsvNhanVien.ItemsSource = lstSearchNV;
            //    }
            //    else
            //    {
            //        lsvNhanVien.ItemsSource = lstNhanVien;
            //    }
            //}
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
            OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal lstInforNv = (sender as Border).DataContext as OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal;
            if (lstInforNv != null)
            {
                Main.grShowPopup.Children.Add(new PopUpTGApDung(Main, lstInforNv));
            }
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
            lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (int.Parse(textPage1.Text) > 1)
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
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    if (textPage1.Text == "2")
                    {
                        borLui1.Visibility = Visibility.Collapsed;
                    }
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
                lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
            }

            else
            {
                textPage1.Text = (TongSoTrang - 1).ToString();
                textPage2.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
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
                lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
            }

            else
            {
                textPage1S.Text = (TongSoTrang - 1).ToString();
                textPage2S.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
            }
        }
        string months; string years;

        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lstChuaTLFilter.Clear();
                if (lsvNhanVien.SelectedItem != null && ((OOP.clsNhanVienThuocCongTy.ListUser)lsvNhanVien.SelectedItem).idQLC > 0)
                {
                    foreach (var item in lstChuaTL)
                    {
                        if (ID_NhanVien == item.idQLC.ToString())
                        {
                            lstChuaTLFilter.Add(item);
                        }
                    }
                    lsvDSNSChuaTL.ItemsSource = lstChuaTLFilter;
                }
                else if (lsvNhanVien.SelectedIndex == 0)
                {
                    if (textHienThiThang.Text != null)
                    {
                        months = textHienThiThang.Text.Split(' ')[1];
                    }
                    else
                    {
                        months = DateTime.Now.ToString("MM");
                    }
                    if (textHienThiNam.Text != null)
                    {
                        years = textHienThiNam.Text.Split(' ')[1];
                    }
                    else
                    {
                        years = DateTime.Now.ToString("yyyy");
                    }
                    EndDate = $"{years}-{months}";
                    LoadDLNhanSuChuaThietLapThue(EndDate);
                }
                else
                {
                    if (textHienThiThang.Text != null)
                    {
                        months = textHienThiThang.Text.Split(' ')[1];
                    }
                    else
                    {
                        months = DateTime.Now.ToString("MM");
                    }
                    if (textHienThiNam.Text != null)
                    {
                        years = textHienThiNam.Text.Split(' ')[1];
                    }
                    else
                    {
                        years = DateTime.Now.ToString("yyyy");
                    }
                    EndDate = $"{years}-{months}";
                    LoadDLNhanSuChuaThietLapThue(EndDate);
                }
            }
            catch (Exception)
            {
            }
           
        }

       
        private void bod_DeleteTenCa_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            tb_TimKiemNhanVien.Text = "";
            //popup.Visibility = Visibility.Collapsed;
            //borNhanVien.Visibility = Visibility.Collapsed;
        }
        private void bod_DeleteTenCa_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bod_DeleteTenCa.Background = (Brush)br.ConvertFrom("#FF5B4D");
        }

        private void bod_DeleteTenCa_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bod_DeleteTenCa.Background = (Brush)br.ConvertFrom("#6666");
        }

        private void btn_SelectListSafff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void borHienThiNhanVien_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (tb_TimKiemNhanVien.Text != "")
            {
                bod_DeleteTenCa.Visibility = Visibility.Visible;
                btn_SelectListSafff.Visibility = Visibility.Collapsed;
                popup.Visibility= Visibility.Collapsed;
            }
        }

        private void borHienThiNhanVien_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bod_DeleteTenCa.Visibility = Visibility.Collapsed;
            btn_SelectListSafff.Visibility = Visibility.Visible;
        }


        private void tb_TimKiemNhanVien_GotFocus(object sender, RoutedEventArgs e)
        {
            lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            if (borNhanVien.Visibility == Visibility.Collapsed)
            {
                borNhanVien.Visibility = Visibility.Visible;
                if (tb_TimKiemNhanVien.Focus())
                {
                    popup.Visibility = Visibility.Collapsed;
                }
                else
                {
                    popup.Visibility = Visibility.Visible;
                }
            }
            {
                foreach (var str in lstNhanVien)
                {
                    if (str.userName.ToLower().RemoveUnicode().Contains(tb_TimKiemNhanVien.Text.ToLower().RemoveUnicode()) || str.idQLC.ToString().Contains(tb_TimKiemNhanVien.Text.ToString()))
                    {
                        lstSearchNV.Add(str);

                    }
                }
                lsvNhanVien.ItemsSource = lstSearchNV;
                if (tb_TimKiemNhanVien.Text == "")
                {
                    lsvNhanVien.ItemsSource = lstNhanVien;
                }
            }
        }
        List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal> listSaftSearch = new List<OOP.CaiDatLuong.Tax.clsNSuChuaTL.ListUserFinal>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSelectSaft = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        string ID_NhanVien;
        private void lsvNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (lsvNhanVien.SelectedItem != null)
            //{
            //    lstSelectSaft = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            //    var chonca = lsvNhanVien.SelectedItem.ToString();
            //    if (!lsvNhanVien1.Any(item => item.idQLC.ToString() == chonca))
            //    {
            //        listSaftSearch = lsvNhanVien1;
            //        ID_NhanVien = chonca;
            //        tb_TimKiemNhanVien.Text = ((OOP.clsNhanVienThuocCongTy.ListUser)lsvNhanVien.SelectedItem).userName;
            //        borNhanVien.Visibility = Visibility.Collapsed;
            //        popup.Visibility = Visibility.Collapsed;
            //        //lsvDSNSChuaTL.ItemsSource = listSaftSearch;
            //    }
            //}
            try
            {
                if (lsvNhanVien.SelectedItem != null)
                {

                    lstSelectSaft = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
                    var chonca = ((OOP.clsNhanVienThuocCongTy.ListUser)lsvNhanVien.SelectedItem).idQLC.ToString();
                    if (chonca != null)
                    {
                        foreach (var item in Main.lstNhanVienThuocCongTy)
                        {
                            if (item.idQLC.ToString() == chonca)
                            {
                                Main.NhanVien = item.userName;
                                ID_NhanVien = item.idQLC.ToString();
                                tb_TimKiemNhanVien.Text = item.userName;
                            }
                        }
                        popup.Visibility = Visibility.Collapsed;
                        borNhanVien.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    lsvDSNSChuaTL.ItemsSource = lstChuaTL;
                }
            }
            catch (Exception)
            {
            }
            
        }
        private void textTimKiemNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_TimKiemNhanVien.Text == "")
            {
                if (lstChuaTL.Count > 10)
                {
                    TongSoTrang = lstChuaTL.Count / 10;
                    SoDu = 10 - (lstChuaTL.Count % 10);
                    if (lstChuaTL.Count % 10 > 0)
                    {
                        TongSoTrang++;
                    }
                    if (TongSoTrang < 3)
                    {
                        borPage3.Visibility = Visibility.Collapsed;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }

                    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;
                    docPhanTrang.Visibility = Visibility.Visible;

                }
                else
                {

                    lsvDSNSChuaTL.ItemsSource = lstChuaTL;
                    docPhanTrang.Visibility = Visibility.Collapsed;
                }
        }
    }

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

       
    }
}
