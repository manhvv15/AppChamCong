using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.Popup.PopupTimeKeeping;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucSelectShift.xaml
    /// </summary>
    public partial class ucSelectShift : UserControl
    {
        MainWindow Main;
        public ucSelectShift(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Main.grShowPopup.Children.Add(new ucCreateCalendarWork(Main));
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodNextCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucNextCreateCalendarWork(Main));
            this.Visibility = Visibility.Collapsed;
        }

        private void bodBackCalendar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCreateCalendarWork(Main));  
            this.Visibility = Visibility.Collapsed;
        }
    }
}
