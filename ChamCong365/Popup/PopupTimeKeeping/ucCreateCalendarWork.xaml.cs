using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ChamCong365.Popup.DatePicker;
using ChamCong365.TimeKeeping;
using System.Globalization;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucCreateCalendarWork.xaml
    /// </summary>
    public partial class ucCreateCalendarWork : UserControl
    {
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        List<String> itemsLich = new List<String>() { "Thứ 2 - Thứ 6", "Thứ 2 - Thứ 7", "Thứ 2 - CN" };
        public static string static_month, static_year;
        int month, year;
        public ucCreateCalendarWork(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            lsvLich.ItemsSource = itemsLich;
            txbSelectedDay.Text = DateTime.Today.ToShortDateString();
            //tbInputName.Focusable = true;
            //tbInputName.Focus();
            txbSelectLich.Text = itemsLich[0];
            txbSelectLich.Foreground = (Brush)bc.ConvertFrom("#ACACAC");
            displayDays();
            txbYearBottom.Text = year.ToString();
            txbYearTop.Text = (year -1).ToString();
            ucLoadDaySmall ucl = new ucLoadDaySmall(Main);
            //txbSelectedDay.Text = month + "/" + ucl.txbDays.Text + "/" + year;
        }

        public void displayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            ucInstallCalendarWork ucI = new ucInstallCalendarWork(Main);
            ucI.txbSelectMonth.Text = month.ToString();
            //string MonthName = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture(month));
            CultureInfo viVietNam = new CultureInfo("vi-VN");
            DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            string monthName = vietnam.MonthNames[month - 1];
            //string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            txbLoadNumMonth.Text = monthName + " " + year;
            txbSelectedMonth.Text = monthName;
            //txbViewTextMonth.Text = month + " / " + year;
            //txbLoadTextCalendarWork.Text = "" + month;
            DateTime startofthemonth = new DateTime(year, month, 1);
            //var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            //var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
            int days = DateTime.DaysInMonth(year, month);
            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1; 
            for (int i = 1; i < dayofftheweek; i++)
            {
                ucLoadFirstDaySmall firstDaySmall = new ucLoadFirstDaySmall();
                loadFistDaySmall.Children.Add(firstDaySmall);
            }

            for (int i = 1; i <= days; i++)
            {
                ucLoadDaySmall loadDaySmall = new ucLoadDaySmall(Main);
                loadDaySmall.days(i);
                loadFistDaySmall.Children.Add(loadDaySmall);
            }

            for (month = 1; month <= 12; month++)
            {
                ucLoadMonth ucm = new ucLoadMonth();
                ucm.txbLoadMonth.Text = "Thg " + month;
                wapLoadMonth.Children.Add(ucm);

                Console.WriteLine("Thg" + month);
            }
        }

        private void Image_MouseLeftButtonUp_SelectLich(object sender, MouseButtonEventArgs e)
        {
            if (bodLich.Visibility == Visibility.Collapsed)
            {
                bodLich.Visibility = Visibility.Visible;
            }
            else
            {
                bodLich.Visibility -= Visibility.Collapsed;

            }
        }

        private void bodOpenCalendar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodCalendarMonth.Visibility == Visibility.Collapsed)
            {
                bodCalendarMonth.Visibility = Visibility.Visible;
            }
            else
            {
                bodCalendarMonth.Visibility = Visibility.Collapsed;
            }
        }

        private void bodOpenCalendarDay_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodCalendarDay.Visibility == Visibility.Collapsed)
            {
                bodCalendarDay.Visibility = Visibility.Visible;
            }
            else
            {
                bodCalendarDay.Visibility = Visibility.Collapsed;
            }
        }

        private void bodContinue_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Main.grShowPopup.Children.Add(new ucSelectShift(Main));
        }

        private void lsvLich_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectLich.Text = lsvLich.SelectedItem.ToString();
            bodLich.Visibility = Visibility.Collapsed;
            txbSelectLich.Foreground = (Brush)bc.ConvertFrom("#474747");
        }

        private void dapMonth_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {       
            //txbSelectedMonth.Text = dapMonth.SelectedDate.ToString();
            //bodCalendarMonth.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed; 
        }

        private void imgNextop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            loadFistDaySmall.Children.Clear();
            
            if (month >= 12)
            {
                month--;
                year++;
            }
            month++;


            //string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(Convert.ToInt32(month));
            CultureInfo viVietNam = new CultureInfo("vi-VN");
            DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            string monthName = vietnam.MonthNames[month - 1];
            txbLoadNumMonth.Text = monthName + " " + year;
            DateTime startofthemonth = new DateTime(year, month, 1);
           
            int days = DateTime.DaysInMonth(year, month);
           

            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
           
            for (int i = 1; i < dayofftheweek; i++)
            {
                ucLoadFirstDaySmall firstDaySmall = new ucLoadFirstDaySmall();
                 loadFistDaySmall.Children.Add(firstDaySmall);
            }

            //now left create uscontrol for day
            for (int i = 1; i <= days; i++)
            {
                ucLoadDaySmall loadDaySmall = new ucLoadDaySmall(Main);
                loadDaySmall.days(i);
                loadFistDaySmall.Children.Add(loadDaySmall);
            }
        }

        private void imgNexBottom_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //Clear container 
            loadFistDaySmall.Children.Clear();
            //imcrrmen month to go to next month
            if (month <= 1)
            {
                month++;
                year--;
            }
            month--;

            //string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            //txbViewTextMonth.Text = month + " / " + year;
            //txbLoadTextCalendarWork.Text = "" + month;
            CultureInfo viVietNam = new CultureInfo("vi-VN");
            DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            string monthName = vietnam.MonthNames[month - 1];
            txbLoadNumMonth.Text = monthName + " " + year;
            DateTime startofthemonth = new DateTime(year, month, 1);
           
            int days = DateTime.DaysInMonth(year, month);
          

            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
          
            for (int i = 1; i < dayofftheweek; i++)
            {
                ucLoadFirstDaySmall firstDaySmall = new ucLoadFirstDaySmall();
                loadFistDaySmall.Children.Add(firstDaySmall);
            }

            
            for (int i = 1; i <= days; i++)
            {
                ucLoadDaySmall loadDaySmall = new ucLoadDaySmall(Main);
                loadDaySmall.days(i);
                loadFistDaySmall.Children.Add(loadDaySmall);
            }
        }

        private void txbYearTop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            year--;
            txbYearTop.Text = (year -2).ToString();
            txbYearBottom.Text = (year - 1).ToString();
           
        }

        private void txbYearBottom_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            
            txbYearBottom.Text = year.ToString();
            txbYearTop.Text = (year - 1).ToString();
            year++;
            bodSelectMonth.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbYearBottom.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");

        }


        private void txbDeleteMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucLoadMonth ucm = new ucLoadMonth();
            ucm.bodLoadMonth.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            ucm.txbLoadMonth.Foreground = (Brush)bc.ConvertFrom("#474747");
        }

        private void txbNowMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            ucLoadMonth ucm = new ucLoadMonth();
            ucm.bodLoadMonth.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            ucm.txbLoadMonth.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void ExitCreateCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
