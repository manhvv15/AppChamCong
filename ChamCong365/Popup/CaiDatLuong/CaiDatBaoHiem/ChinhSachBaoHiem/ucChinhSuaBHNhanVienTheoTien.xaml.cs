using ChamCong365.Popup.ChamCong;
using ChamCong365.TimeKeeping;
using System.Globalization;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ChamCong365.Popup.ChamCong.CaiDatLichLamViec;

namespace ChamCong365.Popup.CaiDatLuong

{
    /// <summary>
    /// Interaction logic for ucEditInsuranceSaff.xaml
    /// </summary>
    public partial class ucChinhSuaBHNhanVienTheoTien : UserControl
    {
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        int month, year;
        public static int static_month, static_year;

        public ucChinhSuaBHNhanVienTheoTien(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            ThangApDung();
            ThangKetThuc();
            txbNamHienTaiApDung.Text = year.ToString();
            txbNamTruocApDung.Text = (year - 1).ToString();
            txbSelectedMonth.Text = "Tháng " + static_month + "/" + static_year;
            txbNamHienTaiKetThuc.Text = year.ToString();
            txbNamTruocKetThuc.Text = (year - 1).ToString();
        }

        #region Lich Tháng

        public void ThangApDung()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            static_month = month;
            static_year = year;

            CultureInfo viVietNam = new CultureInfo("vi-VN");
            DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            string monthName = vietnam.MonthNames[month - 1];
            //txbLoadNumMonth.Text = monthName + " " + year;
            txbSelectedMonth.Text = monthName;
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (month = 1; month <= 12; month++)
            {
                ucHienThiThang ucm = new ucHienThiThang();
                ucm.txbLoadMonth.Text = "Thg " + month;
                HienThiThangApDung.Children.Add(ucm);
            }
        }

        public void ThangKetThuc()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;

            static_month = month;
            static_year = year;

            CultureInfo viVietNam = new CultureInfo("vi-VN");
            DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            string monthName = vietnam.MonthNames[month - 1];
            //txbLoadNumMonth.Text = monthName + " " + year;
            txbSelectedMonth.Text = monthName;
            DateTime startofthemonth = new DateTime(year, month, 1);
            int days = DateTime.DaysInMonth(year, month);
            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            for (month = 1; month <= 12; month++)
            {
                ucHienThiThang ucm = new ucHienThiThang();
                ucm.txbLoadMonth.Text = "Thg " + month;
                HienThiThangKetThuc.Children.Add(ucm);
            }
        }
        private void txbNamTruocApDung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            year--;
            txbNamTruocApDung.Text = (year - 2).ToString();
            txbNamHienTaiApDung.Text = (year - 1).ToString();
        }

        private void txbNamHienTaiApDung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txbNamHienTaiApDung.Text = year.ToString();
            txbNamTruocApDung.Text = (year - 1).ToString();
            year++;
            bodNamHienTaiAoDung.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbNamHienTaiApDung.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void txbNamTruocKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            year--;
            txbNamTruocKetThuc.Text = (year - 2).ToString();
            txbNamHienTaiKetThuc.Text = (year - 1).ToString();
        }

        private void txbNamHienTaiKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            txbNamHienTaiKetThuc.Text = year.ToString();
            txbNamTruocKetThuc.Text = (year - 1).ToString();
            year++;
            bodNamHienTaiKetThuc.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbNamHienTaiKetThuc.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }
        private void txbXoaThangApDung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void txbChonThangApDung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void txbXoaThangKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void txbChonThangKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion
        private void ExitNextSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void txbDeleteMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bodChonLichKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodLichThangKetThuc.Visibility == Visibility.Collapsed)
            {
                bodLichThangKetThuc.Visibility = Visibility.Visible;
                bodLichThangApDung.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodLichThangKetThuc.Visibility = Visibility.Collapsed;
            }
        }

        private void bodChonLichApDung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodLichThangApDung.Visibility == Visibility.Collapsed)
            {
                bodLichThangApDung.Visibility = Visibility.Visible;
                bodLichThangKetThuc.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodLichThangApDung.Visibility = Visibility.Collapsed;
            }
        }


        private void txbNowMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
