using ChamCong365.TimeKeeping;
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

namespace ChamCong365.Popup.ChamCong.CaiDatCongChuan
{
    /// <summary>
    /// Interaction logic for ucXacNhanCongChuan.xaml
    /// </summary>
    public partial class ucXacNhanCongChuan : UserControl
    {
        MainWindow Main;
        public ucXacNhanCongChuan(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }


        private void bodOkMessageBox_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
         
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
