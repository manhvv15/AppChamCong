using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.LichLamViec
{
    /// <summary>
    /// Interaction logic for TaoDeXuatThanhCong.xaml
    /// </summary>
    public partial class TaoDeXuatThanhCong : UserControl
    {
        MainChamCong Main;
        public TaoDeXuatThanhCong(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void ok_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borSay.Visibility = Visibility.Collapsed;
            DeXuatCuaToi uc = new DeXuatCuaToi(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            
        }
    }
}
