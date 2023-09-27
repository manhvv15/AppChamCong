using System.Windows.Controls;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucCreateFileSaff.xaml
    /// </summary>
    public partial class ucThemFileNhanVien : UserControl
    {
        public ucThemFileNhanVien()
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
