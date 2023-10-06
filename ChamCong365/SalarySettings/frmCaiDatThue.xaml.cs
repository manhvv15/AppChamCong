using ChamCong365.SalarySettings.CaiDatThue;
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

namespace ChamCong365.SalarySettings
{
    /// <summary>
    /// Interaction logic for frmCaiDatThue.xaml
    /// </summary>
    public partial class frmCaiDatThue : Page
    {
        private MainWindow Main;
        public frmCaiDatThue(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            BrushConverter bc = new BrushConverter();
            if (main.i == 0)
            {
                textChinhSachThue.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
                lineChinhSachThue.Visibility = Visibility.Visible;
                textDSNhanSuChuaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
                lineDSNhanSuChuaThietLap.Visibility = Visibility.Collapsed;
                textDSNhanSuDaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
                lineDSNhanSuDaThietLap.Visibility = Visibility.Collapsed;
                //textDanhSachNghiSaiQD.Foreground = (Brush)bc.ConvertFrom("#666666");
                //lineDanhSachNghiSaiQD.Visibility = Visibility.Collapsed;
                frmChinhSachThue frm = new frmChinhSachThue(Main);
                pnlHienThi.Children.Clear();
                object content = frm.Content;
                frm.Content = null;
                pnlHienThi.Children.Add(content as UIElement);
            }
        }

        private void tabChinhSachThue_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            textChinhSachThue.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
            lineChinhSachThue.Visibility = Visibility.Visible;
            textDSNhanSuChuaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuChuaThietLap.Visibility = Visibility.Collapsed;
            textDSNhanSuDaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuDaThietLap.Visibility = Visibility.Collapsed;
            frmChinhSachThue frm = new frmChinhSachThue(Main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);
        }

        private void tabDSNhanSuChuaThietLap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            textChinhSachThue.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineChinhSachThue.Visibility = Visibility.Collapsed;
            textDSNhanSuChuaThietLap.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
            lineDSNhanSuChuaThietLap.Visibility = Visibility.Visible;
            textDSNhanSuDaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuDaThietLap.Visibility = Visibility.Collapsed;
            frmDanhSachNhanSuChuaThietLap frm = new frmDanhSachNhanSuChuaThietLap(Main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);
        }

        private void tabDSNhanSuDaThietLap_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            textChinhSachThue.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineChinhSachThue.Visibility = Visibility.Collapsed;
            textDSNhanSuChuaThietLap.Foreground = (Brush)bc.ConvertFrom("#666666");
            lineDSNhanSuChuaThietLap.Visibility = Visibility.Collapsed;
            textDSNhanSuDaThietLap.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
            lineDSNhanSuDaThietLap.Visibility = Visibility.Visible;
            frmDanhSachNhanSuDaThietLap frm = new frmDanhSachNhanSuDaThietLap(Main);
            pnlHienThi.Children.Clear();
            object content = frm.Content;
            frm.Content = null;
            pnlHienThi.Children.Add(content as UIElement);
        }
    }
}
