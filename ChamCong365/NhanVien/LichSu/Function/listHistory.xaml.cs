using ChamCong365.NhanVien.ChamCongKhuonMat.Function;
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
    /// Interaction logic for listHistory.xaml
    /// </summary>
    public partial class listHistory : Window
    {
        MainChamCong Main;
        public listHistory(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void DockPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            chamCong uc = new chamCong(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void DockPanel_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            LuongCoBan uc = new LuongCoBan(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
