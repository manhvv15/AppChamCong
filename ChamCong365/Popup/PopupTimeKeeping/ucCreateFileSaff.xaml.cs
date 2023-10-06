using System.Windows.Controls;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucCreateFileSaff.xaml
    /// </summary>
    public partial class ucCreateFileSaff : UserControl
    {
        public ucCreateFileSaff()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void bodExitCreateFile_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Visibility=System.Windows.Visibility.Collapsed;
        }
    }
}
