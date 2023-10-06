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

namespace ChamCong365.Popups.ChamCong.ViTri
{
    /// <summary>
    /// Interaction logic for ucViTri.xaml
    /// </summary>
    public partial class ucViTri : UserControl
    {
        BrushConverter br = new BrushConverter();
        public ucViTri()
        {
            InitializeComponent();
            bodBuoc1.Background = (Brush)br.ConvertFrom("#42D778");
            txtBuoc1.Foreground = (Brush)br.ConvertFrom("#42D778");
            bodSPhone01.Visibility = Visibility.Visible;
        }

        private void grBuoc1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodBuoc1.Background = (Brush)br.ConvertFrom("#42D778");
            txtBuoc1.Foreground = (Brush)br.ConvertFrom("#42D778");
            bodSPhone01.Visibility = Visibility.Visible;

            bodSPhone02.Visibility = Visibility.Collapsed;
            bodSPhone03.Visibility = Visibility.Collapsed;
            bodSPhone04.Visibility = Visibility.Collapsed;
            bodSPhone05.Visibility = Visibility.Collapsed;

            bodBuoc2.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc2.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc3.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc3.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc4.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc4.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc5.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc5.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void grBuoc2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodBuoc2.Background = (Brush)br.ConvertFrom("#42D778");
            txtBuoc2.Foreground = (Brush)br.ConvertFrom("#42D778");
            bodSPhone02.Visibility = Visibility.Visible;

            bodSPhone01.Visibility = Visibility.Collapsed;
            bodSPhone03.Visibility = Visibility.Collapsed;
            bodSPhone04.Visibility = Visibility.Collapsed;
            bodSPhone05.Visibility = Visibility.Collapsed;

            bodBuoc1.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc1.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc3.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc3.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc4.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc4.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc5.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc5.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void grBuoc3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodBuoc3.Background = (Brush)br.ConvertFrom("#42D778");
            txtBuoc3.Foreground = (Brush)br.ConvertFrom("#42D778");
            bodSPhone03.Visibility = Visibility.Visible;

            bodSPhone02.Visibility = Visibility.Collapsed;
            bodSPhone01.Visibility = Visibility.Collapsed;
            bodSPhone04.Visibility = Visibility.Collapsed;
            bodSPhone05.Visibility = Visibility.Collapsed;

            bodBuoc2.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc2.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc1.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc1.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc4.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc4.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc5.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc5.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void grBuoc4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodBuoc4.Background = (Brush)br.ConvertFrom("#42D778");
            txtBuoc4.Foreground = (Brush)br.ConvertFrom("#42D778");
            bodSPhone04.Visibility = Visibility.Visible;

            bodSPhone02.Visibility = Visibility.Collapsed;
            bodSPhone03.Visibility = Visibility.Collapsed;
            bodSPhone01.Visibility = Visibility.Collapsed;
            bodSPhone05.Visibility = Visibility.Collapsed;

            bodBuoc2.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc2.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc3.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc3.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc1.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc1.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc5.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc5.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void grBuoc5_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodBuoc5.Background = (Brush)br.ConvertFrom("#42D778");
            txtBuoc5.Foreground = (Brush)br.ConvertFrom("#42D778");
            bodSPhone05.Visibility = Visibility.Visible;

            bodSPhone02.Visibility = Visibility.Collapsed;
            bodSPhone03.Visibility = Visibility.Collapsed;
            bodSPhone04.Visibility = Visibility.Collapsed;
            bodSPhone01.Visibility = Visibility.Collapsed;

            bodBuoc2.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc2.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc3.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc3.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc4.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc4.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            bodBuoc1.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txtBuoc1.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }
    }
}
