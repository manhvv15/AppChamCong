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
    /// Interaction logic for frmCaiDatDiMuonVeSom.xaml
    /// </summary>
    public partial class frmCaiDatDiMuonVeSom : Page
    {
        public class Test
        {
            public string LoaiPhat { get; set; }
            public string CaLVApDung { get; set; }
            public string TuThang { get; set; }
            public string DenThang { get; set; }
            public string ThoiGianTinhPhat { get; set; }
            public string MucPhat { get; set; }

        }
        List<string> lstThang = new List<string>();
        List<string> lstNam = new List<string>();
        private MainWindow Main;
        public frmCaiDatDiMuonVeSom(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            textHienThiNam.Text = "Năm " + DateTime.Now.Year;
            lstThang.Add("Tháng 1");
            lstThang.Add("Tháng 2");
            lstThang.Add("Tháng 3");
            lstThang.Add("Tháng 4");
            lstThang.Add("Tháng 5");
            lstThang.Add("Tháng 6");
            lstThang.Add("Tháng 7");
            lstThang.Add("Tháng 8");
            lstThang.Add("Tháng 9");
            lstThang.Add("Tháng 10");
            lstThang.Add("Tháng 11");
            lstThang.Add("Tháng 12");
            lsvThang.ItemsSource = lstThang;
            lstNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
            lstNam.Add("Năm " + DateTime.Now.Year);
            lstNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
            lsvNam.ItemsSource = lstNam;
            List<Test> lst = new List<Test>();
            lst.Add(new Test { LoaiPhat = "1", CaLVApDung = "11", TuThang = "111", DenThang = "1111", ThoiGianTinhPhat = "11111", MucPhat = "111111" });
            lst.Add(new Test { LoaiPhat = "2", CaLVApDung = "11", TuThang = "111", DenThang = "1111", ThoiGianTinhPhat = "11111", MucPhat = "111111" });
            lst.Add(new Test { LoaiPhat = "3", CaLVApDung = "11", TuThang = "111", DenThang = "1111", ThoiGianTinhPhat = "11111", MucPhat = "111111" });
            lst.Add(new Test { LoaiPhat = "4", CaLVApDung = "11", TuThang = "111", DenThang = "1111", ThoiGianTinhPhat = "11111", MucPhat = "111111" });
            lst.Add(new Test { LoaiPhat = "5", CaLVApDung = "11", TuThang = "111", DenThang = "1111", ThoiGianTinhPhat = "11111", MucPhat = "111111" });
            lst.Add(new Test { LoaiPhat = "6", CaLVApDung = "11", TuThang = "111", DenThang = "1111", ThoiGianTinhPhat = "11111", MucPhat = "111111" });
            lst.Add(new Test { LoaiPhat = "7", CaLVApDung = "11", TuThang = "111", DenThang = "1111", ThoiGianTinhPhat = "11111", MucPhat = "111111" });
            dgv.ItemsSource = lst;
            main.i = 0;
        }

        private void borHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Collapsed)
            {
                borHienThiThang.CornerRadius = new CornerRadius(5,5,0,0);
                borThang.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiThang.CornerRadius = new CornerRadius(5,5,5,5);
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
        }
        private List<string> lst22 = new List<string>();
        private void textSearchThang_TextChanged(object sender, TextChangedEventArgs e)
        {
            lst22 = new List<string>();
            foreach(string str in lstThang)
            {
                if (str.Contains(textSearchThang.Text.ToString()))
                {
                    lst22.Add(str);
                    
                }
            }
            lsvThang.ItemsSource = lst22;
            if (textSearchThang.Text == "")
            {
                lsvThang.ItemsSource = lstThang;
            }
            
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

        private void textSearchNam_TextChanged(object sender, TextChangedEventArgs e)
        {
            lst22 = new List<string>();
            foreach (string str in lstNam)
            {
                if (str.Contains(textSearchNam.Text.ToString()))
                {
                    lst22.Add(str);

                }
            }
            lsvNam.ItemsSource = lst22;
            if (textSearchNam.Text == "")
            {
                lsvNam.ItemsSource = lstNam;
            }
        }

        private void lsvNam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void lsvNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textHienThiNam.Text = lsvNam.SelectedItem.ToString();
            borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
            borNam.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
        }

        private void btnThemMoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.PhatDiMuonVeSom.PopUpChinhSuaMucPhat("Thêm mới mức phạt đi muộn về sớm"));
        }

        private void btnChinhSua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.PhatDiMuonVeSom.PopUpChinhSuaMucPhat("Chỉnh sửa mức phạt đi muộn về sớm"));
        }

        private void btnXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.PhatDiMuonVeSom.PopUpXoaMucPhat());

        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
    }
}
