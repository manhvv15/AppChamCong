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

namespace ChamCong365.NhanVien.ChamCongBangQRRR.Function
{
    /// <summary>
    /// Interaction logic for listChamCong.xaml
    /// </summary>
    public partial class listChamCong : Window
    {
        MainChamCong Main;
        public listChamCong(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChamCong365 uc = new ChamCong365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Chat365  uc = new Chat365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            PC365NhanVien uc = new PC365NhanVien(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
