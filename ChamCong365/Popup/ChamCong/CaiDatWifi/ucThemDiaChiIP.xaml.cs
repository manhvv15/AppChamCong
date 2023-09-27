using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucCreateAddressIP.xaml
    /// </summary>
    public partial class ucThemDiaChiIP : UserControl
    {
        public ucThemDiaChiIP()
        {
            InitializeComponent();
        }

        private void ExitCreateWifi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }
    }
}
