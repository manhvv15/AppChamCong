using QuanLyChung365TruocDangNhap.ChamCongNew.Login;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup
{
    /// <summary>
    /// Interaction logic for NotificationBlockLogin.xaml
    /// </summary>
    public partial class NotificationBlockLogin : UserControl
    {
        ucChooseLoginOptions uc { get; set; }
        public NotificationBlockLogin(ucChooseLoginOptions uc)
        {
            InitializeComponent();
            this.uc = uc;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            uc.popup.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            uc.popup.Visibility = Visibility.Collapsed;
        }
    }
}
