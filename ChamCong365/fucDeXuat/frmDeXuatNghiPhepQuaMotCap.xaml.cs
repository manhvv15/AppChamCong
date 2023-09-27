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

namespace ChamCong365.fucDeXuat
{
    /// <summary>
    /// Interaction logic for frmDeXuatNghiPhepQuaMotCap.xaml
    /// </summary>
    public partial class frmDeXuatNghiPhepQuaMotCap : Page
    {

        private MainWindow Main;
        public frmDeXuatNghiPhepQuaMotCap(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDL();
        }
        private void LoadDL()
        {

        }
        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnChonNV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.DeXuat.LoaiHinhDuyetPhep.PopUpChonNhanVien(Main));
        }
    }
}
