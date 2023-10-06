using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.PopupSalarySettings
{
    /// <summary>
    /// Interaction logic for ucDeleteInsuranceSaffYestSettings.xaml
    /// </summary>
    public partial class ucXoaBaoHiemNhanSu : UserControl
    {
       BrushConverter br = new BrushConverter();
        public ucXoaBaoHiemNhanSu()
        {
            InitializeComponent();
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
          
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void txbTextCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility=Visibility.Collapsed;
        }

     
    }
}
