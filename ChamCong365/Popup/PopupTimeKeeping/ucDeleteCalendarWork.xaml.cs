using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucDeleteCalendarWork.xaml
    /// </summary>
    public partial class ucDeleteCalendarWork : UserControl
    {
        BrushConverter bc = new BrushConverter();   
        public ucDeleteCalendarWork()
        {
            InitializeComponent();
        }

        private void bodCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
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
