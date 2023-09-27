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

namespace ChamCong365.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpThietLapCongThuc.xaml
    /// </summary>
    public partial class PopUpThietLapCongThuc : UserControl
    {
        private MainWindow Main;
        private PopUpThemMoiChinhSachThue PopUp;
        public class CongThuc
        {
            public string TieuDeCT { get; set; }
            public string TieuDePhu { get; set; }
        }
        private List<CongThuc> lstCT = new List<CongThuc>();
        public PopUpThietLapCongThuc(MainWindow main, PopUpThemMoiChinhSachThue popup, string ttt, string ctt)
        {
            InitializeComponent();
            Main = main;
            PopUp = popup;
            textCTT.Text = ctt;
            textTenCongThuc.Text = ttt;
            LoadDLCongThuc();
        }

        private void LoadDLCongThuc()
        {
            lstCT.Add(new CongThuc { TieuDeCT = "luong_co_ban", TieuDePhu = "Mức lương cơ bản" });
            lstCT.Add(new CongThuc { TieuDeCT = "cong_chuan", TieuDePhu = "Số công chuẩn" });
            lstCT.Add(new CongThuc { TieuDeCT = "phu_thuoc", TieuDePhu = "Số người phụ thuộc" });
            lstCT.Add(new CongThuc { TieuDeCT = "dong_gop", TieuDePhu = "Các khoản đóng góp" });
            lstCT.Add(new CongThuc { TieuDeCT = "cong_thuc", TieuDePhu = "Số công thực tế" });
            lstCT.Add(new CongThuc { TieuDeCT = "cong_sau_phat", TieuDePhu = "Số công thực tế còn lại sau phạt" });
            lstCT.Add(new CongThuc { TieuDeCT = "luong_bao_hiem", TieuDePhu = "Mức lương đóng bảo hiểm thực tế" });
            lsvCongThuc.ItemsSource = lstCT;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            BrushConverter bc = new BrushConverter();
            PopUp.bor.Fill = (Brush)bc.ConvertFrom("#000000");
            PopUp.bor.Opacity = 0.5;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            BrushConverter bc = new BrushConverter();
            PopUp.bor.Fill = (Brush)bc.ConvertFrom("#000000");
            PopUp.bor.Opacity = 0.5;
        }

        private void btnThemCT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            BrushConverter bc = new BrushConverter();
            PopUp.bor.Fill = (Brush)bc.ConvertFrom("#000000");
            PopUp.bor.Opacity = 0.5;
            PopUp.TenCT = textTenCongThuc.Text;
            PopUp.CTTinh = textCTT.Text;
            this.Visibility = Visibility.Collapsed;

        }
        private string CTT = "";
        

        private void RadioHangSo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borHangSo.Background = (Brush)brus.ConvertFrom("#4C5BD4");
            borVienHS.BorderBrush = (Brush)brus.ConvertFrom("#4C5BD4");
            borCongThuc.Background = (Brush)brus.ConvertFrom("#C4C4C4");
            borVienCT.BorderBrush = (Brush)brus.ConvertFrom("#C4C4C4");
        }

        private void RadioCongThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borHangSo.Background = (Brush)brus.ConvertFrom("#C4C4C4");
            borVienHS.BorderBrush = (Brush)brus.ConvertFrom("#C4C4C4");
            borCongThuc.Background = (Brush)brus.ConvertFrom("#4C5BD4");
            borVienCT.BorderBrush = (Brush)brus.ConvertFrom("#4C5BD4");
        }

        private void lsvCongThuc_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void borCTT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CongThuc ct = (sender as StackPanel).DataContext as CongThuc;
            if (ct != null)
            {
                textCTT.Text = textCTT.Text + " " + ct.TieuDeCT;
            }
        }
    }
}
