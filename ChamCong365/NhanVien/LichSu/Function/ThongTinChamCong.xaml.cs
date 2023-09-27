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

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for ThongTinChamCong.xaml
    /// </summary>
    public partial class ThongTinChamCong : UserControl
    {
        MainChamCong Main;
        public ThongTinChamCong(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }
    }
}
