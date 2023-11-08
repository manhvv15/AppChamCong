using ChamCong365.TimeKeeping;
using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChamCong365.Popup.DatePicker
{
    /// <summary>
    /// Interaction logic for ucCalendarWorkl.xaml
    /// </summary>
    public partial class ucCalendarWorkl : UserControl
    {
        MainWindow Main;
        int month, year;
        public static string static_month, static_year;
        BrushConverter bcCalendar = new BrushConverter();
        public ucCalendarWorkl(MainWindow main)
        {
            InitializeComponent();
            displayDays();
            Main = main;
        }


        public void displayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            ucInstallCalendarWork ucI = new ucInstallCalendarWork(Main);
             ucI.txbSelectMonth.Text = month.ToString();
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            txbViewTextMonth.Text = month + " / " + year;
            txbLoadTextCalendarWork.Text = "" + month;
            // lấy ngày đầu tiên của tháng
            DateTime startofthemonth = new DateTime(year, month, 1);
            //lấy số ngày trong tháng
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


        private void bodNextMonthBotom_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Clear container 
            loadFistDay.Children.Clear();
            //imcrrmen month to go to next month
            if (month<=1)
            {
                month++;
               year--;
            }
            month--;

            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            txbViewTextMonth.Text = month + " / " + year;
            txbLoadTextCalendarWork.Text = "" + month;
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

        private void bodNextMonthTop_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //Clear container 
            loadFistDay.Children.Clear();
            //imcrrmen month to go to next month
            if (month >= 12)
            {
                month--;
                year++;
            }
            month++;
           
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(Convert.ToInt32(month));
            txbViewTextMonth.Text = month + " / " + year;
            txbLoadTextCalendarWork.Text = ""+month;
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

        private void bodCheckedWorkMorrning_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (bodCheckedWorkMorrning.Background == Brushes.Blue)
            {
                bodCheckedWorkMorrning.Background = Brushes.White;
            }
            else
            {
                bodCheckedWorkMorrning.Background = Brushes.Blue;
            }
        }

        private void bodCheckedAffter_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (bodCheckedAffter.Background == Brushes.Blue)
            {
                bodCheckedAffter.Background = Brushes.White;
            }
            else
            {
                bodCheckedAffter.Background = Brushes.Blue;
            }
        }

        private void bodCheckedPartTimeNight_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (bodCheckedPartTimeNight.Background == Brushes.Blue)
            {
                bodCheckedPartTimeNight.Background = Brushes.White;
            }
            else
            {
                bodCheckedPartTimeNight.Background = Brushes.Blue;
            }
        }

        private void imgExitCoppyCalendarWork_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        private void Rectangle_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Visibility = System.Windows.Visibility.Hidden;
        }

        

    }
}
