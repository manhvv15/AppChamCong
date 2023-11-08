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

namespace ChamCong365.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucListTimeKeeping.xaml
    /// </summary>
    public partial class ucListTimeKeeping : UserControl
    {
        MainWindow Main;
        BrushConverter bcBody = new BrushConverter();   
        
        public ucListTimeKeeping( MainWindow main)
        {
            InitializeComponent();
            Main = main;
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            txbLoadNameFuction.Text = ucbodyhome.txbF1.Text + ". " + ucbodyhome.txbChamCong.Text;
           
        }
        private void wapInstallCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucInstallCalendarWork uc = new ucInstallCalendarWork(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction2.Text;
        }

        private void txbFunction1_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction1.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction1_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction1.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

      

        private void txbFunction2_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction2.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction2_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction2.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void wapStandardInstallation_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucStandardInstallation uc = new ucStandardInstallation();
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction3.Text;
        }

        private void txbFunction3_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction3.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction3_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction3.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void txbFunction4_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction4.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction4_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction4.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void wapConfirmationNewDevice_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucConfirmationNewDevice uc = new ucConfirmationNewDevice();
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction5.Text;
        }

        private void txbFunction5_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction5.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction5_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction5.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void wapOutWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucOutWork uc = new ucOutWork();
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction6.Text;
        }

        private void txbFunction6_MouseEnter(object sender, MouseEventArgs e)
        {
            txbFunction6.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
        }

        private void txbFunction6_MouseLeave(object sender, MouseEventArgs e)
        {
            txbFunction6.Foreground = (Brush)bcBody.ConvertFrom("#474747");
        }

        private void wapbuttonSecurityWifi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucSecurityWifi uc = new ucSecurityWifi(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            //string[] str = txbFunctionChamCong.Text.Split(Convert.ToChar("."));
            //string str1 = str[str.Length - 1];
            //uc.lsvLoadWifi.Visibility = Visibility.Visible;
            //uc.txbTextWifi.Foreground = new SolidColorBrush(Colors.Blue);
            uc.txbTextWifi.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction1.Text;
        }

        private void wapUpdateFace_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucUpdateFace uc = new ucUpdateFace();
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction4.Text;
        }
    }
}
