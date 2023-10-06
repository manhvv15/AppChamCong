using ChamCong365.Popup.CaiDatLuong.ThuongPhat;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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

namespace ChamCong365.SalarySettings
{
    /// <summary>
    /// Interaction logic for frmThuongPhat.xaml
    /// </summary>
    public partial class frmThuongPhat : Page
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
        public class ThuongPhat
        {
            public string STT { get; set; }
            public string Anh { get; set; }
            public string Ten { get; set; }
            public string ID { get; set; }
            public string TongTienThuong { get; set; }
            public string TienPhat { get; set; }
            public List<ChiTietThuong> lst = new List<ChiTietThuong>();
            public List<ChiTietPhat> lstPhat = new List<ChiTietPhat>();
        }
        public class ChiTietThuong
        {
            public double TienThuong { get; set; }
            public string NgayApDung { get; set; }
            public string LyDo { get; set; }
        }
        public class ChiTietPhat
        {
            public double TienPhat { get; set; }
            public string NgayApDung { get; set; }
            public string LyDo { get; set; }
        }
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<string> lst22 = new List<string>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private double TongTT;
        private double TongTP;
        private string IdNV = "0";
        private string IdPB = "0";
        private int TongSoTrang = 0;
        private int SoDu = 0;
        private List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTL = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
        private List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
        private List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTLFilter = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
        private List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
        private MainWindow Main;
        private string DuongDanEx = Environment.CurrentDirectory + "\\TempExcel\\FileThuongPhat.xltx";
        public static DataTable tb_ThuongPhat = new DataTable();

        public frmThuongPhat(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            tb_ThuongPhat = Function.clsExPortExcel.NewTables("tb_ThuongPhat", new string[] { "colIDNhanVien", "colTenNhanVien", "colTienThuong", "colTienPhat"}, new int[] { 100, 250, 150, 100 });

            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDLThuongPhat();
            main.scrollMain.ScrollToVerticalOffset(0);
        }

        public void LoadDLThuongPhat()
        {
            try
            {
                lstChuaTL = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/take_thuong_phat")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("month", DateTime.Now.Month.ToString());
                    request.AddParameter("year", DateTime.Now.Year.ToString());
                    request.AddParameter("id_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.Root>(b);
                    if (chuaTL.data.data_final != null)
                    {
                        foreach (var item in chuaTL.data.data_final)
                        {
                            item.inforUser.avatarUser_full = "https://chamcong.24hpay.vn/upload/employee/" + item.inforUser.avatarUser;
                            lstChuaTL.Add(item);
                        }
                        if (lstChuaTL.Count > 10)
                        {
                            TongSoTrang = chuaTL.data.data_final.Count / 10;
                            SoDu = 10 - (chuaTL.data.data_final.Count % 10);
                            if (chuaTL.data.data_final.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            if (TongSoTrang < 3)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                            }
                            for (int i = 0; i < 10; i++)
                            {
                                lstChuaTLPT.Add(chuaTL.data.data_final[i]);
                            }
                            //lstLuongCB = luongCB.listResult;
                            lsvThuongPhat.ItemsSource = lstChuaTLPT;

                            docPhanTrang.Visibility = Visibility.Visible;

                        }
                        else
                        {
                            lsvThuongPhat.ItemsSource = lstChuaTL;
                            docPhanTrang.Visibility = Visibility.Collapsed;
                        }
                        LoadDataInDataTable(lstChuaTL);
                    }
                }
            }
            catch
            {

            }
        }

        private void LoadDataInDataTable(List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> lstChuaTL)
        {
            foreach (var item in lstChuaTL)
            {
                DataRow dr = tb_ThuongPhat.NewRow();

                dr["colIDNhanVien"] = item.inforUser.idQLC;
                dr["colTenNhanVien"] = item.inforUser.userName;
                dr["colTienThuong"] = item.tt_thuong.tong_thuong;
                dr["colTienPhat"] = item.tt_phat.tong_phat;
                tb_ThuongPhat.Rows.Add(dr);
            }
        }

        private void LoadDLNhanVien()
        {
            //textHienThiNhanVien.Text = "Tất cả nhân viên";
            //lstNhanVien.Add(new NhanVien { TenNhanVien = "Tất cả nhân viên" });
            //lstNhanVien.Add(new NhanVien { TenNhanVien = "(111788) Đỗ Văn Hoàng" });
            //lstNhanVien.Add(new NhanVien { TenNhanVien = "(90229) Nguyễn Công Tiến" });
            //lstNhanVien.Add(new NhanVien { TenNhanVien = "(81259) Nguyễn Việt Hoàng" });
            //lstNhanVien.Add(new NhanVien { TenNhanVien = "(144257) Thân Đức Toàn" });

            //lsvNhanVien.ItemsSource = lstNhanVien;
            ////lsvNhanVien.Items.Add("Tất cả nhân viên");
            ////lsvNhanVien.Items.Add("(111788) Đỗ Văn Hoàng");
            ////lsvNhanVien.Items.Add("(90229) Nguyễn Công Tiến");
            textHienThiNhanVien.Text = "Tất cả nhân viên";
            lstNhanVien = Main.lstNhanVienThuocCongTy;

            lsvNhanVien.ItemsSource = lstNhanVien;
        }

        private void LoadDLPhongBan()
        {
            //textHienThiPhongBan.Text = "Phòng ban (tất cả)";
            //lstPhongBan.Add(new PhongBan { TenPB = "Phòng ban (tất cả)" });
            //lstPhongBan.Add(new PhongBan { TenPB = "Kỹ thuật" });
            //lstPhongBan.Add(new PhongBan { TenPB = "Biên tập" });
            //lstPhongBan.Add(new PhongBan { TenPB = "Kinh doanh" });
            //lstPhongBan.Add(new PhongBan { TenPB = "Đề án" });
            //lstPhongBan.Add(new PhongBan { TenPB = "Phòng SEO)" });
            //lstPhongBan.Add(new PhongBan { TenPB = "Phòng đào tạo" });
            //lstPhongBan.Add(new PhongBan { TenPB = "Phòng sáng tạo" });
            //lsvPhongBan.ItemsSource = lstPhongBan;
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
            else
            {
                popup.Visibility = Visibility.Collapsed;
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
                    OOP.clsNhanVienThuocCongTy.ListUser Staff = new OOP.clsNhanVienThuocCongTy.ListUser();
                    Staff.idQLC = 0;
                    Staff.userName = "Tất cả nhân viên";
                    lstSearchNV.Insert(0, Staff);
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
            lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser> ();
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
        private void borThuong_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //yy.Margin = new Thickness(Mouse.GetPosition(popupTP).X, (Mouse.GetPosition(popupTP).Y), 0, 0);
            //yy.HorizontalAlignment = HorizontalAlignment.Left;
            //yy.VerticalAlignment = VerticalAlignment.Top;
            //yy.Margin = new Thickness(6, 6, 0, 0);

            OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp = (sender as Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal;
            if (tp != null)
            {
                Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset);
                Border b = sender as Border;
                //Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.ThuongPhat.PopUpChiTietThuongNV(this, tp, Main));
                //popupTP.Children.Add(new Popup.CaiDatLuong.ThuongPhat.PopUpChiTietThuongNV(this, tp, Main));
                dgvThuongNV.ItemsSource = tp.tt_thuong.ds_thuong;
                borChiTietThuongNV.Margin = new Thickness(Mouse.GetPosition(popupTP).X - 250, (Mouse.GetPosition(popupTP).Y + 20), 0, 0);
                borChiTietThuongNV.VerticalAlignment = VerticalAlignment.Top;
                borChiTietThuongNV.HorizontalAlignment = HorizontalAlignment.Left;
                popupTP.Visibility = Visibility.Visible;
                borChiTietThuongNV.Visibility = Visibility.Visible;


            }
        }

        private void popupTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popupTP.Children.Clear();
            popupTP.Visibility = Visibility.Hidden;
            borChiTietThuongNV.Visibility = Visibility.Collapsed;
            borChiTietPhatNV.Visibility = Visibility.Collapsed;
        }

        private void btnThemTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp = (sender as Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal;
            if (tp != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.ThuongPhat.PopUpThemMoiThuongPhat(Main, tp));

            }
        }

        private void borPhat_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp = (sender as Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal;
            if (tp != null)
            {
                Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset);
                dgvPhatNV.ItemsSource = tp.tt_phat.ds_phat;
                borChiTietPhatNV.VerticalAlignment = VerticalAlignment.Top;
                borChiTietPhatNV.HorizontalAlignment = HorizontalAlignment.Left;
                borChiTietPhatNV.Margin = new Thickness(Mouse.GetPosition(popupTP).X - 350, Mouse.GetPosition(popupTP).Y + 20, 0, 0);
                popupTP.Visibility = Visibility.Visible;
                borChiTietPhatNV.Visibility = Visibility.Visible;
            }
        }

        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstChuaTL = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            lstChuaTLFilter = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            string[] year1 = textHienThiNam.Text.Split(new[] { "Năm" }, StringSplitOptions.None);
            string y = year1[year1.Length - 1].Trim();
            string[] month1 = textHienThiThang.Text.Split(new[] { "Tháng" }, StringSplitOptions.None);
            string mo = month1[month1.Length - 1].Trim();

            if (int.Parse(mo) < 10)
            {
                mo = "0" + mo;
            }
            using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/take_thuong_phat")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                request.AddParameter("month", mo);
                request.AddParameter("year", y);
                request.AddParameter("id_com", Main.IdAcount);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.Root>(b);
                if (chuaTL.data.data_final != null)
                {
                    foreach (var item in chuaTL.data.data_final)
                    {
                        item.inforUser.avatarUser_full = "https://chamcong.24hpay.vn/upload/employee/" + item.inforUser.avatarUser;
                        lstChuaTL.Add(item);
                    }
                    //if (lstChuaTL.Count > 10)
                    //{
                    //    TongSoTrang = chuaTL.listUserFinal.Count / 10;
                    //    SoDu = 10 - (chuaTL.listUserFinal.Count % 10);
                    //    if (chuaTL.listUserFinal.Count % 10 > 0)
                    //    {
                    //        TongSoTrang++;
                    //    }
                    //    if (TongSoTrang < 3)
                    //    {
                    //        borPage3.Visibility = Visibility.Collapsed;
                    //    }
                    //    for (int i = 0; i < 10; i++)
                    //    {
                    //        lstChuaTLPT.Add(chuaTL.listUserFinal[i]);
                    //    }
                    //    //lstLuongCB = luongCB.listResult;
                    //    lsvDSNSChuaTL.ItemsSource = lstChuaTLPT;

                    //    docPhanTrang.Visibility = Visibility.Visible;

                    //}
                    //else
                    //{
                    //    lsvDSNSChuaTL.ItemsSource = lstChuaTL;
                    //    docPhanTrang.Visibility = Visibility.Collapsed;
                    //}
                    
                }
            }
            if (textHienThiPhongBan.Text=="Phòng ban (tất cả)"&&textHienThiNhanVien.Text=="Tất cả nhân viên")
            {
                lstChuaTLFilter = lstChuaTL;
                //lsvDSNhanVien.ItemsSource = lstChuaTLFilter;

            }
            else
            {
                foreach (var item in lstChuaTL)
                {
                    if (item.inforUser.idQLC.ToString() == IdNV && IdPB == "0")
                    {
                        lstChuaTLFilter.Add(item);
                    }
                    else if (IdNV == "0" && IdPB == item.inforUser.inForPerson.employee.dep_id)
                    {
                        lstChuaTLFilter.Add(item);
                    }
                    else if (IdNV == item.inforUser.idQLC.ToString() && IdPB == item.inforUser.inForPerson.employee.dep_id)
                    {
                        lstChuaTLFilter.Add(item);
                    }
                }
                
                //if (lstChuaTLFilter.Count > 10)
                //{
                //    docPhanTrang.Visibility = Visibility.Collapsed;
                //    docPhanTrangS.Visibility = Visibility.Visible;
                //    TongSoTrang = lstChuaTLFilter.Count / 10;
                //    SoDu = 10 - (lstChuaTLFilter.Count % 10);
                //    if (lstChuaTLFilter.Count % 10 > 0)
                //    {
                //        TongSoTrang++;
                //    }
                //    if (TongSoTrang < 3)
                //    {
                //        borPage3S.Visibility = Visibility.Collapsed;
                //    }
                //    for (int i = 0; i < 10; i++)
                //    {
                //        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                //    }
                //    lsvDSNSChuaTL.ItemsSource = lstChuaTLFilterPT;
                //}
                //else
                //{
                //    docPhanTrang.Visibility = Visibility.Collapsed;
                //    docPhanTrangS.Visibility = Visibility.Collapsed;
                //    lsvDSNSChuaTL.ItemsSource = lstChuaTLFilter;
                //}
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
                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            }
            else
            {
                docPhanTrang.Visibility = Visibility.Collapsed;
                docPhanTrangS.Visibility = Visibility.Collapsed;
                lsvThuongPhat.ItemsSource = lstChuaTLFilter;
            }
            LoadDataInDataTable(lstChuaTLFilter);
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
            lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            for (int i = 0; i < 10; i++)
            {
                lstChuaTLPT.Add(lstChuaTL[i]);
            }
            //lstLuongCB = luongCB.listResult;
            lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                        lstChuaTL = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = 0; i < 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();

                for (int i = int.Parse(textPage3.Text) * 10 - 10; i < int.Parse(textPage3.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;

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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = 10; i < 20; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;

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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = 20; i < 30; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLPT;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLPT;
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
            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
            for (int i = 0; i < 10; i++)
            {
                lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            }
            //lstLuongCB = luongCB.listResult;
            lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
                    }
                    else
                    {
                        textPage1S.Text = (int.Parse(textPage1S.Text) - 1).ToString();
                        textPage2S.Text = (int.Parse(textPage2S.Text) - 1).ToString();
                        textPage3S.Text = (int.Parse(textPage3S.Text) - 1).ToString();
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
                }
                else
                {
                    textPage1S.Text = (int.Parse(textPage1S.Text) - 1).ToString();
                    textPage2S.Text = (int.Parse(textPage2S.Text) - 1).ToString();
                    textPage3S.Text = (int.Parse(textPage3S.Text) - 1).ToString();
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = 0; i < 10; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();

                for (int i = int.Parse(textPage3S.Text) * 10 - 10; i < int.Parse(textPage3S.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
                    }
                    else
                    {
                        textPage1S.Text = (int.Parse(textPage1S.Text) + 1).ToString();
                        textPage2S.Text = (int.Parse(textPage2S.Text) + 1).ToString();
                        textPage3S.Text = (int.Parse(textPage3S.Text) + 1).ToString();
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
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
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvThuongPhat.ItemsSource = lstChuaTLFilterPT;
            }
        }

        private void btnExPortExcel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (File.Exists(DuongDanEx))
            {
                Microsoft.Office.Interop.Excel.Application Ex = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = Ex.Workbooks.Open(DuongDanEx);
                Microsoft.Office.Interop.Excel.Worksheet ws_HoaDon = wb.Worksheets["ThuongPhat"];
                int DongBatDau = 2;
                foreach (DataRow row in tb_ThuongPhat.Rows)
                {
                    for (int i = 0; i < tb_ThuongPhat.Columns.Count; i++)
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

        private void btnThemMoiTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new PopUpAddTP(Main, this));
        }
    }
}
