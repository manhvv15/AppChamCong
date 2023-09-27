using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucDelete.xaml
    /// </summary>
    public partial class ucDelete : UserControl
    {
       
        BrushConverter bc = new BrushConverter();
       
        public ucDelete()
        {
            InitializeComponent();
           
        }

        private void Border_MouseLeftButtonUp_OffDelete(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuy_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbHuy.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void bodHuy_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbHuy.Foreground = (Brush)bc.ConvertFrom("#4C5BD4 ");
        }

        private void bodYesDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
           
           
        }
    }
}
