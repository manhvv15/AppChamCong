using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for ucChinhSuaLuongCoBan.xaml
    /// </summary>
    public partial class ucChinhSuaLuongCoBan : UserControl
    {
        BrushConverter br = new BrushConverter();   
        public ucChinhSuaLuongCoBan()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodThoatChinhSuaLuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyChinhSuaLuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyChinhSuaLuong_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuyChinhSuaLuong.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbHuyChinhSuaLuong.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodHuyChinhSuaLuong_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuyChinhSuaLuong.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbHuyChinhSuaLuong.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodCapNhatLuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            
        }

        private void bodCapNhatLuong_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCapNhatLuong.BorderThickness = new Thickness(2);
        }

        private void bodCapNhatLuong_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCapNhatLuong.BorderThickness = new Thickness(1);
        }
    }
}
