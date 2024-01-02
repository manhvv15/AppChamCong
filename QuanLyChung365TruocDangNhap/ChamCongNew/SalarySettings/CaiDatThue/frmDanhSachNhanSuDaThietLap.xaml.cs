using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.CaiDatThue
{
    /// <summary>
    /// Interaction logic for frmDanhSachNhanSuDaThietLap.xaml
    /// </summary>
    public partial class frmDanhSachNhanSuDaThietLap : Page
    {
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
        public class DaTLThue
        {
            public string Anh { get; set; }
            public string ID { get; set; }
            public string Ten { get; set; }
            public string PhongBan { get; set; }
            public string ChinhSachThue { get; set; }
            public string CachTinh { get; set; }
            public string ApDungTuNgay { get; set; }
            public string DenNgay { get; set; }
            public string TienThue { get; set; }

        }
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        public List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        public List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU> lstNhanVien = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU> lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
        private List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU> lstChuaTLFilter = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
        private List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU> lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
        private string IdNV = "0";
        private string IdPB = "0";
        private int TongSoTrang = 0;
        private int SoDu = 0;
        private int PageNumberCurrent = 1;
        public List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU> lstChuaTL = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
        private MainWindow Main;
        BrushConverter brus = new BrushConverter();
        string EndDate;
        public frmDanhSachNhanSuDaThietLap(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            
            Main = main;
            
            main.i = 0;
            
            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            EndDate = $"{DateTime.Now.Year}-{DateTime.Now.Month}";
            LoadDLDSNhanSuDaThietLap(EndDate);
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

        List<OOP.CaiDatLuong.Tax.clsNSDaTL.Detail> detail = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.Detail>();
        List<OOP.CaiDatLuong.Tax.clsNSDaTL.OrganizeDetail> lstOrganize = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.OrganizeDetail>();
        
        public void LoadDLDSNhanSuDaThietLap(string EndDate)
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    loading.Visibility = Visibility.Visible;
                    lstChuaTL.Clear();
                    lstChuaTLPT.Clear();
                    request.QueryString.Add("cls_id_com", Main.IdAcount.ToString());
                    request.QueryString.Add("end_date", EndDate);
                    request.QueryString.Add("token", Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        try
                        {
                            OOP.CaiDatLuong.Tax.clsNSDaTL.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsNSDaTL.Root>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (chuaTL.list_us != null)
                            {
                                lstNhanVien = chuaTL.list_us;
                                lsvNhanVien.ItemsSource = lstNhanVien;
                                TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById("SE Asia Standard Time");
                                foreach (var item in chuaTL.list_us)
                                {
                                    item.cls_day_format = $"{TimeZoneInfo.ConvertTimeFromUtc(item.cls_day, vietnamTimeZone).Month}-{TimeZoneInfo.ConvertTimeFromUtc(item.cls_day, vietnamTimeZone).Year}";
                                    item.cls_day_end_format = $"{TimeZoneInfo.ConvertTimeFromUtc(item.cls_day_end, vietnamTimeZone).Month}-{TimeZoneInfo.ConvertTimeFromUtc(item.cls_day_end, vietnamTimeZone).Year}";
                                    detail.Add(item.Detail);
                                    foreach (var it in detail)
                                    {
                                        if (item.cls_id_user == it.idQLC)
                                        {
                                            item.userName = it.userName;
                                            item.Avatar_Us = it.avatarUser_format.ToString();
                                        }
                                    }
                                    foreach (var it in item.TinhluongListClass)
                                    {
                                        item.CSThue = it.cl_name;
                                    }
                                    foreach (var it in item.TinhluongFormSalary)
                                    {
                                        item.CongThuc = it.fs_repica;
                                    }
                                    if (item.organizeDetail == null)
                                    {
                                        item.ToChuc = "Chưa cập nhật";
                                    }
                                    else
                                    {
                                        item.ToChuc = item.organizeDetail.organizeDetailName;
                                    }
                                    lstOrganize.Add(item.organizeDetail);
                                    lstChuaTL.Add(item);
                                }
                                if (lstChuaTL.Count > 10)
                                {
                                    TongSoTrang = chuaTL.list_us.Count / 10;
                                    SoDu = 10 - (chuaTL.list_us.Count % 10);
                                    if (chuaTL.list_us.Count % 10 > 0)
                                    {
                                        TongSoTrang++;
                                    }
                                    if (TongSoTrang < 3)
                                    {
                                        borPage3.Visibility = Visibility.Collapsed;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        lstChuaTLPT.Add(chuaTL.list_us[i]);
                                    }
                                    dgv.ItemsSource = lstChuaTLPT;

                                    docPhanTrang.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    dgv.ItemsSource = lstChuaTL;
                                    docPhanTrang.Visibility = Visibility.Collapsed;
                                }
                            }
                            loading.Visibility = Visibility.Collapsed;
                        }
                        catch { }
                    };
                    request.UploadValuesTaskAsync("http://210.245.108.202:3009/api/tinhluong/congty/show_list_user_tax",
                        request.QueryString);
                }
            }
            catch (Exception)
            {
            }
        }
        string months; string years;
        private void btnThongKe_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
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
            LoadDLDSNhanSuDaThietLap(EndDate);

        }

        List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU> listSaftSearch = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
        private void textTimKiemNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            listSaftSearch = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
            lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
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
                    if (str.userName.ToLower().RemoveUnicode().Contains(tb_TimKiemNhanVien.Text.ToLower().RemoveUnicode()) || str.cls_id_user.ToString().Contains(tb_TimKiemNhanVien.Text.ToString()))
                    {
                        listSaftSearch.Add(str);
                    }
                }
                lsvNhanVien.ItemsSource = listSaftSearch;
                dgv.ItemsSource = listSaftSearch;
                docPhanTrang.Visibility = Visibility.Collapsed;
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

                        dgv.ItemsSource = lstChuaTLPT;
                        docPhanTrang.Visibility = Visibility.Visible;

                    }
                    else
                    {

                        dgv.ItemsSource = lstChuaTL;
                        docPhanTrang.Visibility = Visibility.Collapsed;
                    }
                }
            }
        }

        private void lsvNhanVien_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvNhanVien.SelectedItem != null)
            {
                listSaftSearch = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                var chonca = lsvNhanVien.SelectedItem.ToString();
                if (!lstNhanVien.Any(item => item.cls_id_user.ToString() == chonca))
                {
                    listSaftSearch = lstNhanVien;
                    tb_TimKiemNhanVien.Text = ((OOP.CaiDatLuong.Tax.clsNSDaTL.ListU)lsvNhanVien.SelectedItem).userName;
                    borNhanVien.Visibility = Visibility.Collapsed;
                    popup.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                dgv.ItemsSource = lstChuaTL;
            }
        }
        private void LoadDLNhanVien()
        {
            //lstNhanVien = Main.lstNhanVienThuocCongTy;
            
        }

        private void LoadDLPhongBan()
        {
            //textHienThiPhongBan.Text = "Phòng ban (tất cả)";

            lstPhongBan = Main.lstPhongBanThuocCongTy;
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

        private void LoadDLNam()
        {
            textHienThiNam.Text = "Năm " + DateTime.Now.Year.ToString();
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 5).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 4).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 3).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 2).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 2).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 3).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 3).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 4).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 5).ToString() });
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
            //else if (borPhongBan.Visibility == Visibility.Visible)
            //{
            //    borPhongBan.Visibility = Visibility.Collapsed;
            //    popup.Visibility = Visibility.Collapsed;
            //    //borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
            //}
            else if (borNhanVien.Visibility == Visibility.Visible)
            {
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                //borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
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
        }


        private void borHienThiPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (borPhongBan.Visibility == Visibility.Collapsed)
            //{
            //    //borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 0, 0);
            //    borPhongBan.Visibility = Visibility.Visible;
            //    popup.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    //borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
            //    borPhongBan.Visibility = Visibility.Collapsed;
            //    popup.Visibility = Visibility.Collapsed;
            //}
        }

        private void lsvNhanVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //scrollNhanVien.ScrollToVerticalOffset(scrollNhanVien.VerticalOffset - e.Delta);
        }



        private void borHienThiNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNhanVien.Visibility == Visibility.Collapsed)
            {
                //borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borNhanVien.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                //borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void borTenNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.clsNhanVienThuocCongTy.ListUser nv = (sender as Border).DataContext as OOP.clsNhanVienThuocCongTy.ListUser;
            if (nv != null)
            {
                //textHienThiNhanVien.Text = nv.userName;
                //borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
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
                //borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                //borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                //textHienThiPhongBan.Text = pb.dep_name;
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
            //lstSearchNV = new List<NhanVien>();
            //foreach (var str in lstNhanVien)
            //{
            //    if (str.TenNhanVien.Contains(textSearchNhanVien.Text.ToString()))
            //    {
            //        lstSearchNV.Add(str);

            //    }
            //}
            //lsvNhanVien.ItemsSource = lstSearchNV;
            //if (textSearchNhanVien.Text == "")
            //{
            //    lsvNhanVien.ItemsSource = lstNhanVien;
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

        private void btnChinhSua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsNSDaTL.ListU listUs = (sender as Border).DataContext as OOP.CaiDatLuong.Tax.clsNSDaTL.ListU;
            if (listUs != null)
            {
                Main.grShowPopup.Children.Add(new PopUpTGApDung(Main, listUs, this));
            }      
        }

        private void btnXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsNSDaTL.ListU listUs = (sender as Border).DataContext as OOP.CaiDatLuong.Tax.clsNSDaTL.ListU;
            if (listUs != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpHoiTruocKhiXoaCaiDatThue(Main, listUs, this));
            }
        }
        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
            borPageCuoi.Visibility = Visibility.Visible;
            lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
            for (int i = 0; i < 10; i++)
            {
                lstChuaTLPT.Add(lstChuaTL[i]);
            }
            dgv.ItemsSource = lstChuaTLPT;
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgv.ItemsSource = lstChuaTLPT;
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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        dgv.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        dgv.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgv.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgv.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                for (int i = 0; i < 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                dgv.ItemsSource = lstChuaTLPT;
            }
        }
      
        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10 - 10 + (10 - SoDu); i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgv.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgv.ItemsSource = lstChuaTLPT;
                }
            }
            catch
            {}
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();

                for (int i = int.Parse(textPage3.Text) * 10 - 10; i < int.Parse(textPage3.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                dgv.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgv.ItemsSource = lstChuaTLPT;
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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        dgv.ItemsSource = lstChuaTLPT;
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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        dgv.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        dgv.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                dgv.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgv.ItemsSource = lstChuaTLPT;

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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                        for (int i = 10; i < 20; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        dgv.ItemsSource = lstChuaTLPT;

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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                        for (int i = 20; i < 30; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        dgv.ItemsSource = lstChuaTLPT;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    dgv.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                dgv.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                dgv.ItemsSource = lstChuaTLPT;
            }
        }
        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstChuaTLFilter = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
            if (IdPB == "0" && IdNV == "0")
            {
                if (lstChuaTL.Count > 10)
                {
                    lstChuaTLPT = new List<OOP.CaiDatLuong.Tax.clsNSDaTL.ListU>();
                    TongSoTrang = lstChuaTL.Count / 10;
                    SoDu = 10 - (lstChuaTL.Count % 10);
                    if (lstChuaTL.Count % 10 > 0)
                    {
                        TongSoTrang++;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgv.ItemsSource = lstChuaTLPT;
                    if (TongSoTrang < 3)
                    {
                        if (TongSoTrang == 2)
                        {
                            borPage3.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                            borPageCuoi.Visibility = Visibility.Collapsed;
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
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                    }
                    docPhanTrang.Visibility = Visibility.Visible;
                    //docPhanTrangS.Visibility = Visibility.Collapsed;

                }
                else
                {
                    dgv.ItemsSource = lstChuaTL;
                    docPhanTrang.Visibility = Visibility.Collapsed;
                    //docPhanTrangS.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                foreach (var item in lstChuaTL)
                {
                    if (item.cls_id_user.ToString() == IdNV && IdPB == "0")
                    {
                        lstChuaTLFilter.Add(item);
                    }
                    else if (IdNV == "0" && IdPB == item.dep_Id.ToString())
                    {
                        lstChuaTLFilter.Add(item);
                    }
                    else if (IdNV == item.cls_id_user.ToString() && IdPB == item.dep_Id.ToString())
                    {
                        lstChuaTLFilter.Add(item);
                    }
                }
                if (lstChuaTLFilter.Count > 10)
                {
                    docPhanTrang.Visibility = Visibility.Collapsed;
                    //docPhanTrangS.Visibility = Visibility.Visible;
                    TongSoTrang = lstChuaTLFilter.Count / 10;
                    SoDu = lstChuaTLFilter.Count % 10;
                    if (lstChuaTLFilter.Count % 10 > 0)
                    {
                        TongSoTrang++;
                    }

                    for (int i = 0; i < 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    dgv.ItemsSource = lstChuaTLFilterPT;
                }
                else
                {

                    docPhanTrang.Visibility = Visibility.Collapsed;
                    //docPhanTrangS.Visibility = Visibility.Collapsed;
                    dgv.ItemsSource = lstChuaTLFilter;
                }
                BrushConverter bc = new BrushConverter();
                //borPage1S.Background = (Brush)bc.ConvertFrom("#4c5bd4");
                //textPage1S.Foreground = (Brush)bc.ConvertFrom("#ffffff");
                //borPage2S.Background = (Brush)bc.ConvertFrom("#ffffff");
                //textPage2S.Foreground = (Brush)bc.ConvertFrom("#474747");
                //borPage3S.Background = (Brush)bc.ConvertFrom("#ffffff");
                //textPage3S.Foreground = (Brush)bc.ConvertFrom("#474747");
                //borPageDauS.Visibility = Visibility.Collapsed;
                //borLui1S.Visibility = Visibility.Collapsed;
                //borPageCuoiS.Visibility = Visibility.Visible;
                //borLen1S.Visibility = Visibility.Visible;
            }
        }

        private void bod_DeleteTenCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tb_TimKiemNhanVien.Text = "";
        }

        private void bod_DeleteTenCa_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bod_DeleteTenCa.Background = (Brush)brus.ConvertFrom("#FF5B4D");
        }

        private void bod_DeleteTenCa_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bod_DeleteTenCa.Background = (Brush)brus.ConvertFrom("#6666");
        }


        private void borHienThiNhanVien_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (tb_TimKiemNhanVien.Text != "")
            {
                bod_DeleteTenCa.Visibility = Visibility.Visible;
                btn_SelectListSafff.Visibility = Visibility.Collapsed;
            }
        }

        private void borHienThiNhanVien_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bod_DeleteTenCa.Visibility = Visibility.Collapsed;
            btn_SelectListSafff.Visibility = Visibility.Visible;
        }

        
        
        private void btn_SelectListSafff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

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
                    bod_TenNhanVien.Background = (Brush)brus.ConvertFrom("#4C5BD4");
                    tb_TenNhanVien.Foreground = (Brush)brus.ConvertFrom("#FFFFFF");
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
                    bod_TenNhanVien.Background = (Brush)brus.ConvertFrom("#FFFFFF");
                    tb_TenNhanVien.Foreground = (Brush)brus.ConvertFrom("#474747");
                }
            }
        }

       
    }
}
