using ChamCong365.Login;
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

namespace ChamCong365.Popup
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiDangXuat.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiDangXuat : UserControl
    {
        private MainWindow Main;
        private ucChooseLoginOptions frmLogin;
        public PopUpHoiTruocKhiDangXuat(MainWindow main,ucChooseLoginOptions ucLog)
        {
            InitializeComponent();
            frmLogin = ucLog;
            Main = main;
        }

        private void btnDongY_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            frmLogin.Show();
            Main.Hide();
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnHuy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
