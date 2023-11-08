using MaterialDesignThemes.Wpf;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucLoadDays.xaml
    /// </summary>
    public partial class ucLoadDaySmall : UserControl
    {
        MainWindow Main;
        BrushConverter br = new BrushConverter();
        public static string static_day;
        public ucLoadDaySmall(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            
        }
        public void days(int numday)
        {
            txbDays.Text = numday+"";
        }


        private void bodLoadDaySmall_MouseEnter(object sender, MouseEventArgs e)
        {
            bodLoadDaySmall.BorderThickness = new Thickness(2);
            bodLoadDaySmall.BorderBrush = (Brush)br.ConvertFrom("#474747");
        }

        private void bodLoadDaySmall_MouseLeave(object sender, MouseEventArgs e)
        {
            bodLoadDaySmall.BorderThickness = new Thickness(0);
            bodLoadDaySmall.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodLoadDaySmall_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodLoadDaySmall.BorderThickness = new Thickness(2);
            bodLoadDaySmall.BorderBrush = (Brush)br.ConvertFrom("#474747");
            bodLoadDaySmall.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbDays.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
           
            
        }
    }
}
