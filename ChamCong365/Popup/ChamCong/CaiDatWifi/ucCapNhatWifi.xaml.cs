using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucUpdateWifi.xaml
    /// </summary>
    public partial class ucCapNhatWifi : UserControl
    {
        public ucCapNhatWifi()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitUpdateWifi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
