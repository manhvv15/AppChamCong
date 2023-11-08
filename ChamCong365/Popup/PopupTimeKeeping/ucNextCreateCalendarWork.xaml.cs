using ChamCong365.Popup.DatePicker;
using System.Globalization;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucNextCreateCalendarWork.xaml
    /// </summary>
    public partial class ucNextCreateCalendarWork : UserControl
    {
        MainWindow Main;
        int month, year;
        public ucNextCreateCalendarWork(MainWindow main)
        {
            InitializeComponent();
            LoadDay();
            Main = main;
            
        }

        private void bodBackCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucSelectShift(Main));
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        public void LoadDay()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            txbViewTextMonth.Text = month + " / " + year;
            // Lets get the first day of the month
            DateTime startofthemonth = new DateTime(year, month, 1);
            //get the count of days of the month
            int days = DateTime.DaysInMonth(year, month);
            //Convert the statofthemonth to interger

            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            //first lets create a blank usercontrol
            for (int i = 1; i < dayofftheweek; i++)
            {
                ucFirstDay firstDay = new ucFirstDay();

                loadFistDay.Children.Add(firstDay);
            }

            //now left create uscontrol for day
            for (int i = 1; i <= days; i++)
            {
                ucLoadDays loadDays = new ucLoadDays();
                loadDays.days(i);
                loadFistDay.Children.Add(loadDays);
            }
        }
    }
}
