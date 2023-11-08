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

namespace ChamCong365.Popup.ListTabInsurance
{
    /// <summary>
    /// Interaction logic for ucSetupInsurance.xaml
    /// </summary>
    /// 

    public partial class ucSetupInsurance : UserControl
    {
        BrushConverter bru = new BrushConverter();
        List<string> insurance = new List<string>(){ "BHXH tính theo lương cơ bản", "BHXH tính theo lương nhập vào","BHXH mới"};
        public ucSetupInsurance()
        {
            InitializeComponent();
            lsvLoadInsurance.ItemsSource = insurance;
        }

        private void bodSelectInsurance_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodlistInsurance.Visibility == Visibility.Collapsed)
            {
                bodlistInsurance.Visibility = Visibility.Visible;
            }
            else
            {
                bodlistInsurance.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvLoadInsurance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbTextInsurance.Text = lsvLoadInsurance.SelectedItem.ToString();
            bodlistInsurance.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodSave_MouseEnter(object sender, MouseEventArgs e)
        {
            bodSave.BorderThickness = new Thickness(1);
        }

        private void bodSave_MouseLeave(object sender, MouseEventArgs e)
        {
            bodSave.BorderThickness = new Thickness(0);
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)bru.ConvertFrom("#4C5BD4");
            txbCancel.Foreground = (Brush)bru.ConvertFrom("#FFFFFF");
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)bru.ConvertFrom("#FFFFFF");
            txbCancel.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");
        }
    }
}
