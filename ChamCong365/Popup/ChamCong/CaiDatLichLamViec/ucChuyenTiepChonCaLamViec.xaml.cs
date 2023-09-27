using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.Popup.ChamCong;
using ChamCong365.Popup.ChamCong.CaiDatLichLamViec;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucSelectShift.xaml
    /// </summary>
    public partial class ucChuyenTiepChonCaLamViec : UserControl
    {
        MainWindow Main;
        public ucChuyenTiepChonCaLamViec(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Main.grShowPopup.Children.Add(new ucThemMoiLichLamViec(Main));
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodNextCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucChuyenTiepThemMoiLich(Main));
            this.Visibility = Visibility.Collapsed;
        }

        private void bodBackCalendar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiLichLamViec(Main));  
            this.Visibility = Visibility.Collapsed;
        }
    }
}
