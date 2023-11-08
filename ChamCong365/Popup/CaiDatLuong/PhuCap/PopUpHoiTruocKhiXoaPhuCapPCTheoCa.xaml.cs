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

namespace ChamCong365.Popup.CaiDatLuong.PhuCap
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaPhuCapPCTheoCa.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaPhuCapPCTheoCa : UserControl
    {
        public PopUpHoiTruocKhiXoaPhuCapPCTheoCa(MainWindow main, string NoiDung)
        {
            InitializeComponent();
            textNoiDung.Text = NoiDung;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
