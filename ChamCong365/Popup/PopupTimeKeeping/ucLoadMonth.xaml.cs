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

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucLoadMonth.xaml
    /// </summary>
    public partial class ucLoadMonth : UserControl
    {
        BrushConverter br = new BrushConverter();
        public ucLoadMonth()
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
