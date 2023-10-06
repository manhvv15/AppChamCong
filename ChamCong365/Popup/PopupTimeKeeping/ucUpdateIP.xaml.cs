using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucUpdateIP.xaml
    /// </summary>
    public partial class ucUpdateIP : UserControl
    {
        public ucUpdateIP()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitUpdateIP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
