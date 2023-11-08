using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucUpdateWifi.xaml
    /// </summary>
    public partial class ucUpdateWifi : UserControl
    {
        public ucUpdateWifi()
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
