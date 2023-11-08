using ChamCong365.SalarySettings;
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

namespace ChamCong365.Popup.DatePicker
{
    /// <summary>
    /// Interaction logic for ucCalendar.xaml
    /// </summary>
    public partial class ucCalendar : UserControl
    {
        MainWindow Main;
        public ucCalendar( MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void dapDays_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            ucBasicSalary ucb = new ucBasicSalary(Main);
            ucb.txbSelectedDays.Text = dapDays.SelectedDate.ToString();
            bodCalendarDay.Visibility = Visibility.Collapsed;
        }
    }
}
