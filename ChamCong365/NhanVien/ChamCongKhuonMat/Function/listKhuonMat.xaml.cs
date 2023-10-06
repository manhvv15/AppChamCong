using ChamCong365.NhanVien.ChamCongBangQRRR.Function;
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

namespace ChamCong365.NhanVien.ChamCongKhuonMat.Function
{
    /// <summary>
    /// Interaction logic for listKhuonMat.xaml
    /// </summary>
    public partial class listKhuonMat : Window
    {
        MainChamCong Main;
        public listKhuonMat(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KhuonMat365 uc = new KhuonMat365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            KhuonMatChat365 uc = new KhuonMatChat365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            KhuonMatPC365 uc = new KhuonMatPC365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
