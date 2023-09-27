using ChamCong365.TimeKeeping;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucDeleteCalendarWork.xaml
    /// </summary>
    public partial class ucXoaLichLamViec : UserControl
    {
        BrushConverter bc = new BrushConverter();
        MainWindow Main;
        public ucXoaLichLamViec(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void bodCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            ucCaiDatLichLamViec ucC = new ucCaiDatLichLamViec(Main);
            ucC.stpListMethond.Visibility = Visibility.Visible;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            ucCaiDatLichLamViec ucC = new ucCaiDatLichLamViec(Main);
            ucC.stpListMethond.Visibility = Visibility.Visible;
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbCancel.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbCancel.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
        }
    }
}
