using ChamCong365.Popup.DatePicker;
using System.Globalization;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucNextCreateCalendarWork.xaml
    /// </summary>
    public partial class ucChuyenTiepThemMoiLich : UserControl
    {
        MainWindow Main;
        int month, year;
        public ucChuyenTiepThemMoiLich(MainWindow main)
        {
            InitializeComponent();
            LoadDay();
            Main = main;
            
        }

        private void bodBackCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucChuyenTiepChonCaLamViec(Main));
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
            ucThemMoiLichLamViec uct = new ucThemMoiLichLamViec(Main);
            string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            txbViewTextMonth.Text = uct.txbSelectedMonth.Text;
            // Lets get the first day of the month
            DateTime startofthemonth = new DateTime(year, month, 1);
            //get the count of days of the month
            int days = DateTime.DaysInMonth(year, month);
            //Convert the statofthemonth to interger

            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            for (int i = 1; i < dayofftheweek; i++)
            {
                ucFirstDay firstDay = new ucFirstDay();
                //lsvLoadLich.SelectedItems.Add(firstDay);
                //firstDay.daysfirst(i);
                loadFistDay.Children.Add(firstDay);
            }

            //now left create uscontrol for day
            for (int i = 1; i <= days; i++)
            {
                ucLoadDays loadDays = new ucLoadDays();
                loadDays.days(i);
                //lsvLoadLich.SelectedItems.Add(loadDays);
                loadFistDay.Children.Add(loadDays);
            }

            
        }
    }
}
