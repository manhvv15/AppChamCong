using QuanLyChung365TruocDangNhap.ChamCongNew.Salarysettings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;

namespace QuanLyChung365TruocDangNhap.ChamCongNew
{
    /// <summary>
    /// Interaction logic for ucBodyHome.xaml
    /// </summary>
    public partial class ucBodyHome : UserControl
    {
        MainWindow Main;
        BrushConverter bcBody = new BrushConverter();

        public ucBodyHome(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }
        
        private void bodSalarySettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 4;
            ucListSalarySettings uc = new ucListSalarySettings(Main);
            grLoadFunction.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunction.Children.Add(Content as UIElement);
            txbQuanLyCongTy.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbDeXuat.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbSalarySettings.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");

        }

        private void bodFunctionTimeKeeping_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 1;
            ucDanhSachChucNangChamCong uc = new ucDanhSachChucNangChamCong(Main, Main.IdAcount);
            grLoadFunction.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunction .Children.Add(Content as UIElement);
            txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");

            txbQuanLyCongTy.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbDeXuat.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbSalarySettings.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void borDeXuat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 3;
            Body uc = new Body(Main);
            grLoadFunction.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunction.Children.Add(Content as UIElement);
            txbDeXuat.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");

            txbSalarySettings.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbQuanLyCongTy.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void btnQuanLyCongTy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 2;
            ucFunctionCompanyManager uc = new ucFunctionCompanyManager(Main, this);
            grLoadFunction.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunction.Children.Add(Content as UIElement);
            txbQuanLyCongTy.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");

            txbSalarySettings.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbDeXuat.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
    } 
}
