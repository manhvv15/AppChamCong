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
using System.Windows.Shapes;

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for capNhatThanhCong.xaml
    /// </summary>
    public partial class capNhatThanhCong : Window
    {
        MainChamCong Main;
        public capNhatThanhCong(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            an.Visibility = Visibility.Collapsed;
        }
    }
}
