using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucInputRecipe.xaml
    /// </summary>
    /// 
    //public class CongThuc
    //{
    //    public string TenCongThuc { get; set; }
    //    public string TieuDe { get; set; }  
    //    public string NoiDung { get; set; }
    //}
    
    
    public partial class ucNhapMoiChinhSachBH : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class CongThuc
        {
            public string TieuDeCT { get; set; }
            public string TieuDePhu { get; set; }
        }
        List<CongThuc> lstCT = new List<CongThuc>();
        private int _type = 1;

        public int type
        {
            get { return _type; }
            set { _type = value; OnPropertyChanged(); }
        }

        MainWindow Main;
        ucThemMoiChinhSachBH Popup;
        public ucNhapMoiChinhSachBH(MainWindow main,ucThemMoiChinhSachBH popup)
        {
            InitializeComponent();
            LoadDLCongThuc();
            Main = main;
            Popup = popup;
        }
        public ucNhapMoiChinhSachBH(MainWindow main, ucThemMoiChinhSachBH popup, OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList bh)
        {
            InitializeComponent();
            LoadDLCongThuc();
            foreach (var item in bh.TinhluongFormSalary)
            {
                textTenCongThuc.Text = item.fs_name;
                textCTT.Text = item.fs_repica;
                type = item.fs_data;
            }
            Main = main;
            Popup = popup;
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            BrushConverter bc = new BrushConverter();
            Popup.bor.Fill = (Brush)bc.ConvertFrom("#000000");
            Popup.bor.Opacity = 0.5;
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
        private void ExitNextSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnThemCT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Popup.TenCT = textTenCongThuc.Text;
            Popup.ChiTietCT = textCTT.Text;
            Popup.TypeCT = type.ToString();
            this.Visibility = Visibility.Collapsed;
            BrushConverter bc = new BrushConverter();
            Popup.bor.Fill = (Brush)bc.ConvertFrom("#000000");
            Popup.bor.Opacity = 0.5;
        }

        private void lsvCongThuc_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            BrushConverter bc = new BrushConverter();
            Popup.bor.Fill = (Brush)bc.ConvertFrom("#000000");
            Popup.bor.Opacity = 0.5;
        }

        private void borCTT_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            CongThuc ct = (sender as StackPanel).DataContext as CongThuc;
            if (ct != null)
            {
                textCTT.Text = textCTT.Text + " " + ct.TieuDeCT;
            }
        }

        private void RadioHangSo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            type = 1;
        }

        private void RadioCongThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            type = 2;
        }
    }
}
