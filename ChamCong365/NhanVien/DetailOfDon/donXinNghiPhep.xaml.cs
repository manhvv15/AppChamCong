using ChamCong365.NhanVien.Propose;
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

namespace ChamCong365.NhanVien.DetailOfDon
{
    /// <summary>
    /// Interaction logic for donXinNghiPhep.xaml
    /// </summary>
    public partial class donXinNghiPhep : UserControl
    {
        MainChamCong Main;
        public donXinNghiPhep(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DeXuatCuaToi uc = new DeXuatCuaToi(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
    }
}
