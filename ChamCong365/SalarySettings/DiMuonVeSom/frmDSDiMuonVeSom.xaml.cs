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
        List<PhongBan> lstPhongBan = new List<PhongBan>();
        List<NhanVien> lstNhanVien = new List<NhanVien>();
        List<string> lst22 = new List<string>();
        List<NhanVien> lstSearchNV = new List<NhanVien>();
        //private string Nam = "";
        //private string Thang = "";
        public frmDSDiMuonVeSom(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            if (main.WindowState == WindowState.Maximized)
            {
                borNam.HorizontalAlignment = HorizontalAlignment.Left;
                borNam.VerticalAlignment = VerticalAlignment.Top;
                borNam.Margin = new Thickness(814, 64, 0, 0);
            }
            else if(main.WindowState == WindowState.Normal)
            {
                borNam.HorizontalAlignment = HorizontalAlignment.Left;
                borNam.VerticalAlignment = VerticalAlignment.Top;
                borNam.Margin = new Thickness(10, 108, 0, 0);
            }
            main.i = 2;
            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDLDiMuonVeSom();
            //main.j = 1;
        }

        private void LoadDLDiMuonVeSom()
        {
            lstDiMuonVeSom.Add(new DSDiMuonVeSom { ID = "11", HoVaTen = "Đỗ Văn Hoàng", Anh = "F:\\anonymous-muc-dic-hoat-dong.jpg", PhongBan = "Kỹ Thuật", ThoiGian = "10/7/2023", Ca = "Ca chiều", Muon = "Muộn 0 phút 5 giây",Som= "Về sớm 0 phút 40 giây", Phat = "50000 VND" });
            lstDiMuonVeSom.Add(new DSDiMuonVeSom { ID = "22", HoVaTen = "Nguyễn Công Tiến", Anh = "F:\\anonymous-muc-dic-hoat-dong.jpg", PhongBan = "Kỹ Thuật", ThoiGian = "10/7/2023", Ca = "Ca chiều", Muon = "Muộn 0 phút 5 giây", Som = "Về sớm 0 phút 40 giây", Phat = "50000 VND" });
            lstDiMuonVeSom.Add(new DSDiMuonVeSom { ID = "33", HoVaTen = "Nguyễn Trọng Hùng", Anh = "F:\\anonymous-muc-dic-hoat-dong.jpg", PhongBan = "Kỹ Thuật", ThoiGian = "10/7/2023", Ca = "Ca chiều", Muon = "Muộn 0 phút 5 giây", Som = "Về sớm 0 phút 40 giây", Phat = "50000 VND" });
            lstDiMuonVeSom.Add(new DSDiMuonVeSom { ID = "44", HoVaTen = "Nguyễn Thị Tố Uyên", Anh = "F:\\anonymous-muc-dic-hoat-dong.jpg", PhongBan = "Kỹ Thuật", ThoiGian = "10/7/2023", Ca = "Ca chiều", Muon = "Muộn 0 phút 5 giây", Som = "Về sớm 0 phút 40 giây", Phat = "50000 VND" });
            lstDiMuonVeSom.Add(new DSDiMuonVeSom { ID = "55", HoVaTen = "Thân Đức Toàn", Anh = "F:\\anonymous-muc-dic-hoat-dong.jpg", PhongBan = "Kỹ Thuật", ThoiGian = "10/7/2023", Ca = "Ca chiều", Muon = "Muộn 0 phút 5 giây", Som = "Về sớm 0 phút 40 giây", Phat = "50000 VND" });
            lstDiMuonVeSom.Add(new DSDiMuonVeSom { ID = "66", HoVaTen = "Lê Thị Thu Hằng", Anh = "F:\\anonymous-muc-dic-hoat-dong.jpg", PhongBan = "Kỹ Thuật", ThoiGian = "10/7/2023", Ca = "Ca chiều", Muon = "Muộn 0 phút 5 giây", Som = "Về sớm 0 phút 40 giây", Phat = "50000 VND" });
            dgv.ItemsSource = lstDiMuonVeSom;
        }

        private void LoadDLNhanVien()
        {
            textHienThiNhanVien.Text = "Tất cả nhân viên";
            lstNhanVien.Add(new NhanVien { TenNhanVien = "Tất cả nhân viên" });
            lstNhanVien.Add(new NhanVien { TenNhanVien = "(111788) Đỗ Văn Hoàng" });
            lstNhanVien.Add(new NhanVien { TenNhanVien = "(90229) Nguyễn Công Tiến" });
            lstNhanVien.Add(new NhanVien { TenNhanVien = "(81259) Nguyễn Việt Hoàng" });
            lstNhanVien.Add(new NhanVien { TenNhanVien = "(144257) Thân Đức Toàn"});

            lsvNhanVien.ItemsSource = lstNhanVien;
            //lsvNhanVien.Items.Add("Tất cả nhân viên");
            //lsvNhanVien.Items.Add("(111788) Đỗ Văn Hoàng");
            //lsvNhanVien.Items.Add("(90229) Nguyễn Công Tiến");
        }

        private void LoadDLPhongBan()
        {
            textHienThiPhongBan.Text = "Phòng ban (tất cả)";
            lstPhongBan.Add(new PhongBan { TenPB = "Phòng ban (tất cả)" });
            lstPhongBan.Add(new PhongBan { TenPB = "Kỹ thuật" });
            lstPhongBan.Add(new PhongBan { TenPB = "Biên tập" });
            lstPhongBan.Add(new PhongBan { TenPB = "Kinh doanh" });
            lstPhongBan.Add(new PhongBan { TenPB = "Đề án" });
            lstPhongBan.Add(new PhongBan { TenPB = "Phòng SEO)" });
            lstPhongBan.Add(new PhongBan { TenPB = "Phòng đào tạo" });
            lstPhongBan.Add(new PhongBan { TenPB = "Phòng sáng tạo" });
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
            NhanVien nv = (sender as Border).DataContext as NhanVien;
            if (nv != null)
            {
                textHienThiNhanVien.Text = nv.TenNhanVien;
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.NhanVien = nv.TenNhanVien;
            }
        }

        private void borTenPB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhongBan pb = (sender as Border).DataContext as PhongBan;
            if (pb != null)
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                textHienThiPhongBan.Text = pb.TenPB;
                Main.PhongBan = pb.TenPB;
            }
        }

        private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNV = new List<NhanVien>();
            foreach (var str in lstNhanVien)
            {
                if (str.TenNhanVien.Contains(textSearchNhanVien.Text.ToString()))
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
    }
}
