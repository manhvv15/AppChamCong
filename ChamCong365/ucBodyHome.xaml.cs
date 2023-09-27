using ChamCong365.Salarysettings;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ChamCong365.TimeKeeping;
using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;

namespace ChamCong365
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

            //txbLoadNameFuction.Text = txbF1.Text + ". " + txbChamCong.Text;
            //grLoadFunction.Children.Add(new ucListTimeKeeping(Main));
            //txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");


        }
        private void wapbuttonSecurityWifi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //ucSecurityWifi uc = new ucSecurityWifi(Main);
            //dopFormBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //dopFormBody.Children.Add(Content as UIElement);
            //Main.LableFunction.Visibility = Visibility.Visible;
            ////string[] str = txbFunctionChamCong.Text.Split(Convert.ToChar("."));
            ////string str1 = str[str.Length - 1];
            //uc.lsvLoadWifi.Visibility = Visibility.Visible;
            //uc.txbTextWifi.Foreground = new SolidColorBrush(Colors.Blue);
            //uc.txbTextWifi.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
            //Main.txbLoadChamCong.Text = txbChamCong.Text + " / " + txbFunction1.Text;
        }
        private void wapInstallCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ucInstallCalendarWork uc = new ucInstallCalendarWork(Main);
            //dopFormBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //dopFormBody.Children.Add(Content as UIElement);
            //Main.LableFunction.Visibility = Visibility.Visible;
            //Main.txbLoadChamCong.Text = txbChamCong.Text + " / " + txbFunction2.Text;
        }

        private void wapStandardInstallation_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ucStandardInstallation uc = new ucStandardInstallation();
            //dopFormBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //dopFormBody.Children.Add(Content as UIElement);
            //Main.LableFunction.Visibility = Visibility.Visible;
            //Main.txbLoadChamCong.Text = txbChamCong.Text + " / " + txbFunction3.Text;
        }

        private void wapUpdateFace_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ucUpdateFace uc = new ucUpdateFace();
            //dopFormBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //dopFormBody.Children.Add(Content as UIElement);
            //Main.LableFunction.Visibility = Visibility.Visible;
            //Main.txbLoadChamCong.Text = txbChamCong.Text + " / " + txbFunction4.Text;
        }

        private void wapConfirmationNewDevice_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ucConfirmationNewDevice uc = new ucConfirmationNewDevice();
            //dopFormBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //dopFormBody.Children.Add(Content as UIElement);
            //Main.LableFunction.Visibility = Visibility.Visible;
            //Main.txbLoadChamCong.Text = txbChamCong.Text + " / " + txbFunction5.Text;
        }

        private void wapOutWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ucOutWork uc = new ucOutWork();
            //dopFormBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //dopFormBody.Children.Add(Content as UIElement);
            //Main.LableFunction.Visibility = Visibility.Visible;
            //Main.txbLoadChamCong.Text = txbChamCong.Text + " / " + txbFunction6.Text;
        }

        private void txbChamCong_MouseEnter(object sender, MouseEventArgs e)
        {
            //txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbChamCong_MouseLeave(object sender, MouseEventArgs e)
        {
            //txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void txbFunction1_MouseEnter(object sender, MouseEventArgs e)
        {
            //txbFunction1.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction1_MouseLeave(object sender, MouseEventArgs e)
        {
            //txbFunction1.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void txbFunction2_MouseEnter(object sender, MouseEventArgs e)
        {
            //txbFunction2.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction2_MouseLeave(object sender, MouseEventArgs e)
        {
            //txbFunction2.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void txbFunction3_MouseEnter(object sender, MouseEventArgs e)
        {
            //txbFunction3.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction3_MouseLeave(object sender, MouseEventArgs e)
        {
            //txbFunction3.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void txbFunction4_MouseEnter(object sender, MouseEventArgs e)
        {
            //txbFunction4.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction4_MouseLeave(object sender, MouseEventArgs e)
        {
            //txbFunction4.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void txbFunction5_MouseEnter(object sender, MouseEventArgs e)
        {
            //txbFunction5.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction5_MouseLeave(object sender, MouseEventArgs e)
        {
            //txbFunction5.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }
        private void txbFunction6_MouseEnter(object sender, MouseEventArgs e)
        {
            //txbFunction6.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
        private void txbFunction6_MouseLeave(object sender, MouseEventArgs e)
        {
            //txbFunction6.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void bodSalarySettings_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 4;
            ucListSalarySettings uc = new ucListSalarySettings(Main);
            grLoadFunction.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunction.Children.Add(Content as UIElement);
            txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#474747");
            txbSalarySettings.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");

        }

       

        private void bodFunctionTimeKeeping_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 1;
            ucDanhSachChucNangChamCong uc = new ucDanhSachChucNangChamCong(Main);
            grLoadFunction.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunction .Children.Add(Content as UIElement);
            txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
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
            txbChamCong.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
            txbSalarySettings.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void btnQuanLyCongTy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 2;
            ucFunctionCompanyManager uc = new ucFunctionCompanyManager(Main, this);
            grLoadFunction.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            grLoadFunction.Children.Add(Content as UIElement);
            //txbQuanLyCongTy.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }
    } 
}
