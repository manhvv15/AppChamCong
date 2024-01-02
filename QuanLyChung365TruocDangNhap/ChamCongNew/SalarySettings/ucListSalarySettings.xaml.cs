using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Salarysettings
{
    /// <summary>
    /// Interaction logic for ucListSalarySettings.xaml
    /// </summary>
    public partial class ucListSalarySettings : UserControl
    {
        BrushConverter bcBody = new BrushConverter();
        MainWindow Main;
   
        public ucListSalarySettings(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            //frmmain.BackToBack = "";
            ucBodyHome ucbh = new ucBodyHome(Main);
            txbNumber4.Text = ucbh.txbF4.Text + ". " + txbSalarySettings.Text;
            txbSalarySettings.Text = ucbh.txbSalarySettings.Text;
        }

        private void grLoadFunction01_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Main.Back = 4;
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucCaiDatLuongCoBan uc = new ucCaiDatLuongCoBan(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFunction01.Text;
        }

        private void btnPhatDiMunVeSom_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFunction02.Text;
        }
        private void btnCaiDatBaoHiem_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucCaiDatBaoHiem uc = new ucCaiDatBaoHiem(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFunction03.Text;

        }
        private void btnCaiDatPhucLoi_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucCaiDatPhucLoi uc = new ucCaiDatPhucLoi(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFunction04.Text;

        }
        private void btnPhuCapKhac_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            frmDanhSachPhuCap frm = new frmDanhSachPhuCap(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFunction05.Text;
        }
       
       
        private void btnCaiDatThue_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            frmCaiDatThue frm = new frmCaiDatThue(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFunction06.Text;
        }
        private void btnCongCong_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucCongCong uc = new ucCongCong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFunction07.Text;

        }
        private void btnThuongPhat_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            frmThuongPhat frm = new frmThuongPhat(Main);
            Main.dopBody.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            Main.dopBody.Children.Add(content as UIElement);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFunction08.Text;
        }

        private void btnXuatLuong_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //ucBodyHome ucbodyhome = new ucBodyHome(Main);
            //ucCaiDatLuongCoBan uc = new ucCaiDatLuongCoBan(Main);
            //Main.dopBody.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //Main.dopBody.Children.Add(Content as UIElement);
            //Main.LableFunction.Visibility = Visibility.Visible;
            //Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbXuatLuong.Text;

        }

        private void btnHoaHong_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Main.Back = 4;
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucHoaHong uc = new ucHoaHong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + txbFunction09.Text;
        }
    }
}
