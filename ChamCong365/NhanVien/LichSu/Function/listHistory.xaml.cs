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
        MainWindow Main1;
        public listHistory(MainChamCong main, MainWindow main1)
        {
            InitializeComponent();
            Main = main;
            Main1 = main1;
        }

        private void DockPanel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChamCongNv uc = new ChamCongNv(Main,Main1);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.txbLoadChamCong.Text = tb_NameFunction6.Text + " / " + tb_NameCC.Text;
        }

        private void DockPanel_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            LuongNhanVien uc = new LuongNhanVien(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.txbLoadChamCong.Text = tb_NameFunction6.Text + " / " + tb_NameLuong.Text;
        }
    }
}
