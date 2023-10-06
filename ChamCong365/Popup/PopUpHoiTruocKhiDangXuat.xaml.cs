using ChamCong365.Login;
using ChamCong365.NhanVien;
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
        private MainChamCong MainCC;
        private ucChooseLoginOptions frmLogin;
        public PopUpHoiTruocKhiDangXuat(MainWindow main,ucChooseLoginOptions ucLog)
        {
            InitializeComponent();
            frmLogin = ucLog;
            Main = main;
        }
        public PopUpHoiTruocKhiDangXuat(MainChamCong main, ucChooseLoginOptions ucLog)
        {
            InitializeComponent();
            frmLogin = ucLog;
            MainCC = main;
        }
        private void btnDongY_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            frmLogin.Show();
            if (Main != null)
            {
                Main.Hide();
                if (Properties.Settings.Default.Check == "0")
                {
                    frmLogin.tb_TaiKhoanDangNhap.Text = "";
                    frmLogin.tb_MatKhau.Password = "";
                    frmLogin.tb_TaiKhoanDangNhap.Focus();
                }

            }
            else if (MainCC != null)
            {
                MainCC.Hide();
            }

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
