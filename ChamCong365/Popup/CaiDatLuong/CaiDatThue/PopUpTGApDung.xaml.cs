﻿using ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem;
using RestSharp;
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

namespace ChamCong365.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpTGApDung.xaml
    /// </summary>
    public partial class PopUpTGApDung : UserControl
    {
        MainWindow Main;
        public static int static_month, static_year;
        int month, year;
        BrushConverter bc = new BrushConverter();
        private string Month = "";
        private string IdNV = "";
        private int IdThue = 0;
        PopUpAddStaffInTax PopUpThemNVCST;
        public PopUpTGApDung(MainWindow main,PopUpAddStaffInTax pop,string Idnv,int IdT)
        {
            InitializeComponent();
            Main = main;
            PopUpThemNVCST = pop;
            IdNV = Idnv;
            IdThue = IdT;
            ThangApDung();
            ThangKetThuc();
            textNamTruocBD.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiBD.Text = DateTime.Now.Year.ToString();
            textNamTruocKT.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiKT.Text = DateTime.Now.Year.ToString();
        }
        #region Lich Tháng

        public void ThangApDung()
        {
            //DateTime now = DateTime.Now;
            //month = now.Month;
            //year = now.Year;

            //static_month = month;
            //static_year = year;

            //CultureInfo viVietNam = new CultureInfo("vi-VN");
            //DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            //string monthName = vietnam.MonthNames[month - 1];
            ////txbLoadNumMonth.Text = monthName + " " + year;
            //txbThangApDung.Text = monthName;
            //DateTime startofthemonth = new DateTime(year, month, 1);
            //int days = DateTime.DaysInMonth(year, month);
            //int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            //for (month = 1; month <= 12; month++)
            //{
            //    ucHienThiThang ucm = new ucHienThiThang();
            //    ucm.txbLoadMonth.Text = "Thg " + month;
            //    HienThiThangApDung.Children.Add(ucm);
            //}
        }

        public void ThangKetThuc()
        {
            //DateTime now = DateTime.Now;
            //month = now.Month;
            //year = now.Year;

            //static_month = month;
            //static_year = year;

            //CultureInfo viVietNam = new CultureInfo("vi-VN");
            //DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            //string monthName = vietnam.MonthNames[month - 1];
            ////txbLoadNumMonth.Text = monthName + " " + year;
            //txbThangApDung.Text = monthName;
            //DateTime startofthemonth = new DateTime(year, month, 1);
            //int days = DateTime.DaysInMonth(year, month);
            //int dayofftheweek = Convert.ToInt32(startofthemonth.DayOfWeek.ToString("d")) + 1;

            //for (month = 1; month <= 12; month++)
            //{
            //    ucHienThiThang ucm = new ucHienThiThang();
            //    ucm.txbLoadMonth.Text = "Thg " + month;
            //    HienThiThangKetThuc.Children.Add(ucm);
            //}
        }
        private void borTGKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNamKT.Visibility == Visibility.Visible)
            {
                borNamKT.Visibility = Visibility.Collapsed;
                closePopup.Visibility = Visibility.Collapsed;
            }
            else
            {
                borNamKT.Visibility = Visibility.Visible;
                closePopup.Visibility = Visibility.Visible;
            }
        }

        private void borTGBatDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNamBatDauAD.Visibility == Visibility.Visible)
            {
                borNamBatDauAD.Visibility = Visibility.Collapsed;
                closePopup.Visibility = Visibility.Collapsed;
            }
            else
            {
                borNamBatDauAD.Visibility = Visibility.Visible;
                closePopup.Visibility = Visibility.Visible;
            }
        }

        private void closePopup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void borNamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocKT.Visibility = Visibility.Visible;
            DSThangNamSauKT.Visibility = Visibility.Collapsed;
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
        }

        private void borNamHienTaiKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocKT.Visibility = Visibility.Collapsed;
            DSThangNamSauKT.Visibility = Visibility.Visible;
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
        }


        private void NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocBatDau.Visibility = Visibility.Visible;
            DSThangNamSauBatDau.Visibility = Visibility.Collapsed;
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
        }

        private void NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocBatDau.Visibility = Visibility.Collapsed;
            DSThangNamSauBatDau.Visibility = Visibility.Visible;
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
        }

        private void Thang1NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void Thang1NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void btnChonThangNayNamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGBatDau.Text = Month + "/" + textNamHienTaiBD.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;

        }

        private void btnChonThangNayNamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGBatDau.Text = Month + "/" + textNamTruocBD.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void Thang1NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void Thang1NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void textThangNayNamHienTaiKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGKetThuc.Text = Month + "/" + textNamHienTaiKT.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void textHienThiThangNayNamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGKetThuc.Text = Month + "/" + textNamTruocKT.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
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

        private void bodSaveTime_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/them_nv_nhom_tax")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                request.AddParameter("cls_id_cl", IdThue);
                request.AddParameter("cls_id_com", Main.IdAcount);
                request.AddParameter("cls_id_user", IdNV);
                string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                string y = day[day.Length - 1];
                string[] day1 = day[0].Split(new[] { "Tháng" }, StringSplitOptions.None);
                string mo = day1[day1.Length - 1].Trim();
                if (int.Parse(mo) < 10)
                {
                    request.AddParameter("cls_day", y + "-0" + mo + "-" + "01T00:00:00.000+00:00");
                }
                else
                {
                    request.AddParameter("cls_day", y + "-" + mo + "-" + "01T00:00:00.000+00:00");
                }
                string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                string ykt = daykt[daykt.Length - 1];
                string[] day1kt = daykt[0].Split(new[] { "Tháng" }, StringSplitOptions.None);
                string mokt = day1kt[day1kt.Length - 1].Trim();
                if (int.Parse(mokt) < 10)
                {
                    request.AddParameter("cls_day_end", ykt + "-0" + mokt + "-" + "01T00:00:00.000+00:00");
                }
                else
                {
                    request.AddParameter("cls_day_end", ykt + "-" + mokt + "-" + "01T00:00:00.000+00:00");
                }
                //request.AddParameter("cls_salary", textNhapTienBH.Text);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                this.Visibility = Visibility.Collapsed;
                PopUpThemNVCST.Visibility = Visibility.Collapsed;
            }

        }

        private void ExitNextSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucThemNhanVienBaoHiem uca = new ucThemNhanVienBaoHiem(Main);
            this.Visibility = Visibility.Collapsed;

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            PopUpThemNVCST.bor.Fill = (Brush)brus.ConvertFrom("#000000");
            PopUpThemNVCST.bor.Opacity = 0.5;
            this.Visibility = Visibility.Collapsed;
        }
    }
}
