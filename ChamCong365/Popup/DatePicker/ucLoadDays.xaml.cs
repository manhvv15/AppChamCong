using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.DatePicker
{
    /// <summary>
    /// Interaction logic for ucLoadDays.xaml
    /// </summary>
    public partial class ucLoadDays : UserControl
    {
        public ucLoadDays()
        {
            InitializeComponent();
        }
        public void days(int numday)
        {
            txbLoadDays.Text = numday + "";
        }
        private void bodNumberDay_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bodNumberDay_MouseEnter(object sender, MouseEventArgs e)
        {
            bodNumberDay.BorderThickness = new Thickness(2);
        }

        private void bodNumberDay_MouseLeave(object sender, MouseEventArgs e)
        {
            bodNumberDay.BorderThickness = new Thickness(0);
        }
    }
}
