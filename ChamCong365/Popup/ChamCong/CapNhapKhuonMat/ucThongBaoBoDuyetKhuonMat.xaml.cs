using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.ChamCong.CapNhapKhuonMat
{
    /// <summary>
    /// Interaction logic for ucThongBaoBoDuyetKhuonMat.xaml
    /// </summary>
    public partial class ucThongBaoBoDuyetKhuonMat : UserControl
    {
        MainWindow Main;
        public ucThongBaoBoDuyetKhuonMat(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void BoDuyet(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
