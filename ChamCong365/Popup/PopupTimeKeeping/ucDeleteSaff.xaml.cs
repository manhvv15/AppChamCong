using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucDeleteSaff.xaml
    /// </summary>
    public partial class ucDeleteSaff : UserControl
    {
        public ucDeleteSaff()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
