using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucListTimeKeeping.xaml
    /// </summary>
    public partial class ucDanhSachChucNangChamCong : UserControl
    {
        public MainWindow Main;
        int Idcom;
        BrushConverter bcBody = new BrushConverter();

        public ucDanhSachChucNangChamCong(MainWindow main, int com_id)
        {
            InitializeComponent();
            Main = main;
            Idcom = com_id;
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            txbLoadNameFuction.Text = ucbodyhome.txbF1.Text + ". " + ucbodyhome.txbChamCong.Text;

        }
        #region List ShowFunction
        private void CaiDatWifi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucCaiDatBaoMatWifi uc = new ucCaiDatBaoMatWifi(Main, Main.IdAcount);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            uc.txbTextWifi.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction1.Text;
        }

        private void CaLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucBodyHome = new ucBodyHome(Main);
            ucShiftWorkManager uc = new ucShiftWorkManager(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucBodyHome.txbChamCong.Text + " / " + txbFunction2.Text;
        }

        private void CaiDatLichLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            ucCaiDatLichLamViec uc = new ucCaiDatLichLamViec(Main, Main.IdAcount);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction3.Text;
        }

        private void CaiDatCongChuan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucCaiDatCongChuan uc = new ucCaiDatCongChuan(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction4.Text;
        }

        private void CauHinhChamCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucCapNhatKhuonMatMoi ucc = new ucCapNhatKhuonMatMoi(Main);
            ucBodyHome ucBodyHome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = ucc.Content;
            ucc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucBodyHome.txbChamCong.Text + " / " + txbFunction5.Text;
        }

        private void CapNhatKhuonMat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucDanhSachBuocDiLam uc = new ucDanhSachBuocDiLam(Main);

            // ucCapNhatKhuonMat uc = new ucCapNhatKhuonMat(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction6.Text;
        }

        private void DuyetThietBiMoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LichSuDiemDanh uc = new LichSuDiemDanh(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction7.Text;
        }

        private void XuatCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucXuatCongMoi uc = new ucXuatCongMoi(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction7.Text;
        }
        private void ChamCongKhuonMat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 1;
            ChamCong_Main chamcong = new ChamCong_Main(Main);
            ucBodyHome ucBodyHome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = chamcong.Content;
            chamcong.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucBodyHome.txbChamCong.Text + " / " + txbFunction9.Text;
        }
        private void LichSuDiemDanh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LichSuDiemDanh uc = new LichSuDiemDanh(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction8.Text;
        }
        #endregion

        #region Mouse Enter 
        private void txbFunction1_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction1.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");

        }
        private void txbFunction2_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction2.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
        private void txbFunction3_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction3.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
        private void txbFunction4_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction4.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
        private void txbFunction5_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction5.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
        private void txbFunction6_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction6.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
        private void txbFunction7_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction7.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
        private void txbFunction9_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction9.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
        private void txbFunction8_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction8.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
        #endregion

        #region Mouse Lever
        private void txbFunction1_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction1.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
        private void txbFunction2_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction2.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
        private void txbFunction3_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction3.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
        private void txbFunction4_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction4.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
        private void txbFunction5_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction5.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
        private void txbFunction6_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction6.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
        private void txbFunction7_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction7.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
        private void txbFunction9_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction9.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
        private void txbFunction8_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction8.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }








        #endregion


    }
}
