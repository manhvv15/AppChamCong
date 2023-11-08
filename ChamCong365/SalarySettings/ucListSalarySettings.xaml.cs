using ChamCong365.SalarySettings;
using ChamCong365.TimeKeeping;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChamCong365.Salarysettings
{
    /// <summary>
    /// Interaction logic for ucListSalarySettings.xaml
    /// </summary>
    public partial class ucListSalarySettings : UserControl
    {
        BrushConverter bcBody = new BrushConverter();
        MainWindow Main;
        public ucListSalarySettings( MainWindow main)
        {
            InitializeComponent();
            Main = main;
            ucBodyHome ucbh = new ucBodyHome(Main);
            txbNumber4.Text = ucbh.txbF4.Text + ". " + txbSalarySettings.Text;
            txbSalarySettings.Text = ucbh.txbSalarySettings.Text;
        }

        private void grLoadFunction01_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucBasicSalary uc = new ucBasicSalary(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFuncSalary01.Text;
            //string[] str = txbFunctionChamCong.Text.Split(Convert.ToChar("."));
            //string str1 = str[str.Length - 1];
            //uc.lsvLoadWifi.Visibility = Visibility.Visible;
            //uc.txbTextWifi.Foreground = new SolidColorBrush(Colors.Blue);
            //uc.txbTextWifi.Foreground = (Brush)bcBody.ConvertFrom("#4C5BD4");
            //Main.txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text + " / " + txbFunction1.Text;
        }

        private void grLoadFunction03_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucInsuranceSettings uc = new ucInsuranceSettings(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement );
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFuncSalary03.Text;

        }

        private void btnPhatDiMunVeSom_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
        }

        private void btnPhuCapKhac_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            frmDanhSachPhuCap frm = new frmDanhSachPhuCap(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
        }
    }
}
