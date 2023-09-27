using ChamCong365.Popup.ChamCong;
using ChamCong365.TimeKeeping;
using System;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

using System.Windows.Input;
using System.Windows.Media;



namespace ChamCong365.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for ucChinhSuaHopDong.xaml
    /// </summary>
    public partial class ucChinhSuaHopDong : UserControl
    {
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        int day, month, year;
        public ucChinhSuaHopDong(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            NgayBatDau();
            NgayHetHan();
        }

        public void NgayBatDau()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            day = now.Day;
           

            CultureInfo viVietNam = new CultureInfo("vi-VN");
            DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            string monthName = vietnam.MonthNames[month - 1];

            txbLoadNumMonth.Text = monthName + " " + year;
            txbNgayHieuLuc.Text = day + "/" + month + "/" + year ;

            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            for (int i = 1; i < dayofftheweek; i++)
            {
                ucHienThiNgayDauTien firstDaySmall = new ucHienThiNgayDauTien();
                NgayHieuLuc.Children.Add(firstDaySmall);
            }

            for (int i = 1; i <= days; i++)
            {
                ucHienThiDuLieuNgay loadDaySmall = new ucHienThiDuLieuNgay(Main);
                loadDaySmall.days(i);
                NgayHieuLuc.Children.Add(loadDaySmall);
            }

            for (month = 1; month <= 12; month++)
            {
                ucHienThiThang ucm = new ucHienThiThang();
                ucm.txbLoadMonth.Text = "Thg " + month;
                //wapLoadMonth.Children.Add(ucm);

                Console.WriteLine("Thg" + month);
            }
        }


        public void NgayHetHan()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            day = now.Day;


            CultureInfo viVietNam = new CultureInfo("vi-VN");
            DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            string monthName = vietnam.MonthNames[month - 1];

            txbLoadNumMonth.Text = monthName + " " + year;
            txbNamHetHan.Text = day + "/" + month + "/" + year;

            DateTime startofthemonth = new DateTime(year, month, 1);

            int days = DateTime.DaysInMonth(year, month);
            int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;
            for (int i = 1; i < dayofftheweek; i++)
            {
                ucHienThiNgayDauTien firstDaySmall = new ucHienThiNgayDauTien();
                NgayHetHieuLuc.Children.Add(firstDaySmall);
            }

            for (int i = 1; i <= days; i++)
            {
                ucHienThiDuLieuNgay loadDaySmall = new ucHienThiDuLieuNgay(Main);
                loadDaySmall.days(i);
                NgayHetHieuLuc.Children.Add(loadDaySmall);
            }

            for (month = 1; month <= 12; month++)
            {
                ucHienThiThang ucm = new ucHienThiThang();
                ucm.txbLoadMonth.Text = "Thg " + month;
                //wapLoadMonth.Children.Add(ucm);

                Console.WriteLine("Thg" + month);
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodThoatCapNhatHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyCapNhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyCapNhat_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuyCapNhat.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbCapNhatHopDong.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodHuyCapNhat_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuyCapNhat.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbCapNhatHopDong.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodCapNhatHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility=Visibility.Collapsed;
        }

        private void bodCapNhatHopDong_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCapNhatHopDong.BorderThickness = new Thickness(2);
        }

        private void bodNgayHetHieuLuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodNgayHetHan.Visibility == Visibility.Collapsed)
            {
                bodNgayHetHan.Visibility = Visibility.Visible;
                bodNgayHieuLuc.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodNgayHetHan.Visibility = Visibility.Collapsed;
            }
        }

        private void bodCapNhatHopDong_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCapNhatHopDong.BorderThickness = new Thickness(2);
        }

        private void bodLichNgayHieuLuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodNgayHieuLuc.Visibility == Visibility.Collapsed)
            {
                bodNgayHieuLuc.Visibility = Visibility.Visible;
                bodNgayHetHan.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodNgayHieuLuc.Visibility = Visibility.Collapsed;
            }
        }
    }
}
