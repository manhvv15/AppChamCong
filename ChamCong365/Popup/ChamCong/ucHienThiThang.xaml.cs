using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.ChamCong
{
    /// <summary>
    /// Interaction logic for ucLoadMonth.xaml
    /// </summary>
    public partial class ucHienThiThang : UserControl
    {
        BrushConverter br = new BrushConverter();
        public ucHienThiThang()
        {
            InitializeComponent();
        }
        public void month(int numday)
        {
            txbLoadMonth.Text = numday + "";
        }

        private void bodLoadMonth_MouseEnter(object sender, MouseEventArgs e)
        {
            bodLoadMonth.BorderThickness = new Thickness(2);
            bodLoadMonth.BorderBrush = (Brush)br.ConvertFrom("#474747");
        }

        private void bodLoadMonth_MouseLeave(object sender, MouseEventArgs e)
        {
            bodLoadMonth.BorderThickness = new Thickness(0);
            bodLoadMonth.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodLoadMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodLoadMonth.BorderThickness = new Thickness(2);
            bodLoadMonth.BorderBrush = (Brush)br.ConvertFrom("#474747");
            bodLoadMonth.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbLoadMonth.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }
    }
}
