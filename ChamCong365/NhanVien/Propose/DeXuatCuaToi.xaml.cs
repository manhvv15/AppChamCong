using ChamCong365.NhanVien.DetailOfDon;
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

namespace ChamCong365.NhanVien.Propose
{
    /// <summary>
    /// Interaction logic for DeXuatCuaToi.xaml
    /// </summary>
    public partial class DeXuatCuaToi : Window
    {
        MainChamCong Main;
       // ChamCong365.NhanVien.KindOfDon.LichLamViec.NguoiXetDuyet ListNg;
        public DeXuatCuaToi(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            //ListNg = listNg;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            LoaiDeXuat uc = new LoaiDeXuat(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            donXinNghiPhep uc = new donXinNghiPhep(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
