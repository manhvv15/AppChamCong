using ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucCreateWifi.xaml
    /// </summary>
    public partial class ucThemMoiWifi : UserControl
    {
        MainWindow Main;
        public ucThemMoiWifi(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void CreateWifi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitCreateWifi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

     

        private void bodThemMoiWifi_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ucDanhSachWii ucw = new ucDanhSachWii(Main);
           
        }

       
    }
}
