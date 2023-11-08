using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.TimeKeeping;
using System.Globalization;
using System.Windows.Media;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucCoppyCalendarWork.xaml
    /// </summary>
    public partial class ucCoppyCalendarWork : UserControl
    {
        List<String> itemsLich = new List<String>() { "Thứ 2 - Thứ 6", "Thứ 2 - Thứ 7", "Thứ 2 - CN" };
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        int month, year;
        public ucCoppyCalendarWork()
        {
            InitializeComponent();
            lsvCalendarMonth.ItemsSource = itemsLich;
            displayDays();
            txbYearBottom.Text = year.ToString();
            txbYearTop.Text = (year - 1).ToString();
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
            //txbLoadNumMonth.Text = monthName + " " + year;
            //txbSelectedMonth.Text = monthName;
            //txbViewTextMonth.Text = month + " / " + year;
            //txbLoadTextCalendarWork.Text = "" + month;
            DateTime startofthemonth = new DateTime(year, month, 1);
            //var DaysInMonth = DateTime.DaysInMonth(now.Year, now.Month);
            //var lastDay = new DateTime(now.Year, now.Month, DaysInMonth);
            int days = DateTime.DaysInMonth(year, month);
            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (month = 1; month <= 12; month++)
            {
                ucLoadMonth ucm = new ucLoadMonth();
                ucm.txbLoadMonth.Text = "Thg " + month;
                wapLoadMonth.Children.Add(ucm);

                Console.WriteLine("Thg" + month);
            }
        }

        private void dapMonthCoppy_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //txbSelectedMonthCoppy.Text = dapMonthCoppy.SelectedDate.ToString();
            //bodCalendarMonthCoppy.Visibility = Visibility.Collapsed;
        }

        private void dopSelectedMonthCoppy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodCalendarMonthCoppy.Visibility == Visibility.Collapsed)
            {
                bodCalendarMonthCoppy.Visibility = Visibility.Visible;
            }
            else
            {
                bodCalendarMonthCoppy.Visibility = Visibility.Collapsed;
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed; 
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCheckBoxCoppySaffAll_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void txbYearTop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            year--;
            txbYearTop.Text = (year - 2).ToString();
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
            //ucLoadMonth ucm = new ucLoadMonth();
            //ucm.bodLoadMonth.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            //ucm.txbLoadMonth.Foreground = (Brush)bc.ConvertFrom("#474747");
        }

        private void txbNowMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //DateTime now = DateTime.Now;
            //month = now.Month;
            //year = now.Year;
            //ucLoadMonth ucm = new ucLoadMonth();
            //ucm.bodLoadMonth.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            //ucm.txbLoadMonth.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }
    }
}
