using ChamCong365.Popup.ChamCong;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.ListTabInsurance
{
    /// <summary>
    /// Interaction logic for ucSetupInsurance.xaml
    /// </summary>
    /// 

    public partial class ucThietLapBHNhanVien : UserControl
    {
        BrushConverter bru = new BrushConverter();
        public static int static_month, static_year;
        int month, year;
        List<string> insurance = new List<string>(){ "BHXH tính theo lương cơ bản", "BHXH tính theo lương nhập vào","BHXH mới"};
        public ucThietLapBHNhanVien()
        {
            InitializeComponent();
            lsvLoadInsurance.ItemsSource = insurance;
            ThangApDung();
            ThangKetThuc();
            txbNamHienTaiApDung.Text = year.ToString();
            txbNamTruocApDung.Text = (year - 1).ToString();
            txbThangApDung.Text = "Tháng " + static_month + "/" + static_year;
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
            txbThangApDung.Text = monthName;
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
            txbThangApDung.Text = monthName;
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
            bodNamHienTaiAoDung.Background = (Brush)bru.ConvertFrom("#4C5BD4");
            txbNamHienTaiApDung.Foreground = (Brush)bru.ConvertFrom("#FFFFFF");
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
            bodNamHienTaiKetThuc.Background = (Brush)bru.ConvertFrom("#4C5BD4");
            txbNamHienTaiKetThuc.Foreground = (Brush)bru.ConvertFrom("#FFFFFF");
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

        private void bodThangKetThuc1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void bodThangApDung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void ExitInsuranceSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void bodCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
