using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for ucThemHopDongNhanVien.xaml
    /// </summary>
    public partial class ucThemHopDongNhanVien : UserControl
    {
        BrushConverter br = new BrushConverter();
        public ucThemHopDongNhanVien()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodThoatThemHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyThemHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyThemHopDong_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuyThemHopDong.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbHuyThemHopDong.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodHuyThemHopDong_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuyThemHopDong.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbHuyThemHopDong.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodThemMoiHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodThemMoiHopDong_MouseEnter(object sender, MouseEventArgs e)
        {
            bodThemMoiHopDong.BorderThickness = new Thickness(2);
        }

        private void bodThemMoiHopDong_MouseLeave(object sender, MouseEventArgs e)
        {
            bodThemMoiHopDong.BorderThickness = new Thickness(0);
        }
    }
}
