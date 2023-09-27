using ChamCong365.Popup.ChamCong;

using ChamCong365.Popup.ChamCong.CaiDatLichLamViec;
using ChamCong365.Popup.DatePicker;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucInstallCalendarWork.xaml
    /// </summary>
    ///
    public class Saff
    {
        public string NameSaff { get; set; }
        public int IdSaff { get; set; }
        public string ImageSaff { get; set; }
        public string MonthYear { get; set; }
        public int NubSaff { get; set; }
        public string EmailSaff { get; set; }
        public string NumPhoneSaff { get; set; }
    }
    public class Years
    {
        public string years { get; set; }
    }
    public partial class ucCaiDatLichLamViec : UserControl
    {
        private MainWindow Main;
        int month, year;
        BrushConverter br = new BrushConverter();
        List<string> listSearchMonth = new List<string>();
       
        List<Saff> itemsSaff = new List<Saff>();
        List<String> itemsLich = new List<String>() { "Thứ 2 - Thứ 6", "Thứ 2 - Thứ 7", "Thứ 2 - CN" };
        List<Years> listYear = new List<Years>();
        List<Years> listSearchYear = new List<Years>();
        /*{ "Năm 2023", "Năm 2024", "Năm 2025", "Năm 2026", "Năm 2027" }*/
        List<string> listMonth = new List<string>() { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6", "Tháng 7", "Tháng 8", "Tháng 9", "Tháng 10", "Tháng 11", "Tháng 12" };
       
        public ucCaiDatLichLamViec(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            #region FakeSaff
            itemsSaff.Add(new Saff() { IdSaff = 50001, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto001@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50002, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto002@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50003, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto003@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50004, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto004@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50004, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto005@gmail.com", NumPhoneSaff = "0393039393" });
            itemsSaff.Add(new Saff() { IdSaff = 50005, NameSaff = "Tô Ngọc Ký", ImageSaff = "./Resource/image/KyTo.jpg", NubSaff = 1, MonthYear = "Tháng 6-2023", EmailSaff = "ngockyto006@gmail.com", NumPhoneSaff = "0393039393" });
            #endregion
            DateTime now =  DateTime.Now;
            month = now.Month;
            year = now.Year;
            //txbSelectYear.Text = "Năm " + year;
            txbSelectMonth.Text = "Tháng " + month.ToString();
            //lsvListYear.ItemsSource = listYear;
            LoadDLNam();
            lsvListMonth.ItemsSource = listMonth;
            string[] splitmonts = txbSelectMonth.Text.Split(Convert.ToChar(" "));
            string splitmonts1 = splitmonts[splitmonts.Length - 1];
            txbCalendarNumMonth.Text = splitmonts1;
            string[] splityear = txbSelectYear.Text.Split(Convert.ToChar(" "));
            string splityear1 = splityear[splityear.Length - 1];
            txbMonthAndYear.Text = txbSelectMonth.Text + "-" + splityear1;
        }

        private void LoadDLNam()
        {
            txbSelectYear.Text = "Năm " + year;
            for (year = 2022; year <= 2025; year++)
            {
                lsvListYear.Items.Add(year);

            }
            //listYear.Add(new Years { years = "Năm " + (double.Parse( year -1).ToString()});
            //listYear.Add(new Years { years = "Năm " + year });
            //listYear.Add(new Years { years = "Năm " + (year - 1)});
            //lsvListYear.ItemsSource = listYear;
        }

        private void lsvListMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectMonth.Text = lsvListMonth.SelectedItem.ToString();
            bodMonth.Visibility = Visibility.Collapsed;
            string[] str = lsvListMonth.SelectedItem.ToString().Split(Convert.ToChar(" "));
            string str1 = str[str.Length - 1];
            txbCalendarNumMonth.Text = str1;
            string[] splityear = txbSelectYear.Text.Split(Convert.ToChar(" "));
            string splityear1 = splityear[splityear.Length - 1];
            txbMonthAndYear.Text = txbSelectMonth.Text + "-" + splityear1;

        }

        private void bodMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodMonth.Visibility == Visibility.Collapsed)
            {
                bodMonth.Visibility = Visibility.Visible;
                txtSearchMonth.Focus();
                bodYear.Visibility = Visibility.Collapsed;
                stpListMethond.Visibility = Visibility.Collapsed;
                Popup.Visibility = Visibility.Visible;
            }
            else
            {
                bodMonth.Visibility = Visibility.Collapsed;
            }
        }

        private void txtSearchMonth_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var itemMoth in listMonth)
            {
                if (itemMoth.ToLower().Contains(txtSearchMonth.Text.ToString()))
                {
                    listSearchMonth.Add(itemMoth);
                }
                
            }
            lsvListMonth.ItemsSource = listSearchMonth;

        }

        private void txtSearchYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            //listSearchYear = new List<Years>();
            //foreach (var str in listYear)
            //{
            //    if (str.years.Contains(txbSelectYear.Text.ToString()))
            //    {
            //        listSearchYear.Add(str);

            //    }
            //}
            //lsvListYear.ItemsSource = listSearchYear;
            //if (txbSelectYear.Text == "")
            //{
            //    lsvListYear.ItemsSource = listYear;
            //}

            foreach (var itemYear in listYear)
            {
                if (itemYear.years.Contains(txtSearchYear.Text.ToString()))
                {
                    listSearchYear.Add(itemYear);
                }

            }
            lsvListYear.ItemsSource = listSearchYear;
        }

        private void lsvListYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectYear.Text = lsvListYear.SelectedItem.ToString();
            bodYear.Visibility = Visibility.Collapsed;
        }

        private void bodYear_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodYear.Visibility == Visibility.Collapsed)
            {
                bodYear.Visibility = Visibility.Visible;
                bodMonth.Visibility = Visibility.Collapsed;
                stpListMethond.Visibility = Visibility.Collapsed;
                Popup.Visibility = Visibility.Visible;
            }
            else
            {
                bodYear.Visibility -= Visibility.Collapsed;
            }
        }

        private void bodAddLich_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiLichLamViec(Main));
            bodYear.Visibility = Visibility.Collapsed; 
        }

        private void bodButonCoppyCalendar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucSaoChepLichLamViec(Main));
           
        }

        private void bodListMethond_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (stpListMethond.Visibility == Visibility.Collapsed)
            {
                stpListMethond.Visibility = Visibility.Visible;
                bodYear.Visibility = Visibility.Collapsed;
                bodMonth.Visibility = Visibility.Collapsed;

            }
            else
            {
                stpListMethond.Visibility = Visibility.Collapsed;
            }
        }

        private void dopAddSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiNhanVien(Main));
            stpListMethond.Visibility = Visibility.Collapsed;
        }

        private void dopListSaffSmall_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDanhSachNhanVien(Main));
            stpListMethond.Visibility = Visibility.Collapsed;

        }

        private void stpEditCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
    {
        Main.grShowPopup.Children.Add(new ucChinhSuaLichLamViec(Main));
            stpListMethond.Visibility = Visibility.Collapsed;
        }

        private void dopCoppyCalendarSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucSaoChepLichLamViec(Main));
            stpListMethond.Visibility = Visibility.Collapsed;
        }

        private void bodDeleteCalendarSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucXoaLichLamViec(Main));
            stpListMethond.Visibility = Visibility.Collapsed;
        }

        private void bodAddCalendar_MouseEnter(object sender, MouseEventArgs e)
        {
            bodAddCalendar.BorderThickness = new Thickness(1);
        }

        private void bodAddCalendar_MouseLeave(object sender, MouseEventArgs e)
        {
            bodAddCalendar.BorderThickness = new Thickness(0);
        }

        private void bodButonCoppyCalendar_MouseEnter(object sender, MouseEventArgs e)
        {
            bodButonCoppyCalendar.BorderThickness = new Thickness(1);
        }

        private void bodButonCoppyCalendar_MouseLeave(object sender, MouseEventArgs e)
        {
            bodButonCoppyCalendar.BorderThickness = new Thickness(0);
        }

        #region Hover
        private void dopAddSaff_MouseEnter(object sender, MouseEventArgs e)
        {
            txbAddSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            iconAccount1.Stroke = (Brush)br.ConvertFrom("#4C5BD4");
            iconAccount2.Stroke = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void dopAddSaff_MouseLeave(object sender, MouseEventArgs e)
        {
            txbAddSaff.Foreground = (Brush)br.ConvertFrom("#474747");
            iconAccount1.Stroke = (Brush)br.ConvertFrom("#474747");
            iconAccount2.Stroke = (Brush)br.ConvertFrom("#474747");
        }

        private void dopListSaffSmall_MouseEnter(object sender, MouseEventArgs e)
        {
            txbListSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            iconDanhSach1.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            iconDanhSach2.Stroke = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void dopListSaffSmall_MouseLeave(object sender, MouseEventArgs e)
        {
            txbListSaff.Foreground = (Brush)br.ConvertFrom("#474747");
            iconDanhSach1.BorderBrush = (Brush)br.ConvertFrom("#474747");
            iconDanhSach2.Stroke = (Brush)br.ConvertFrom("#474747");
        }
        private void stpEditCalendarWork_MouseEnter(object sender, MouseEventArgs e)
        {
            txbEditSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            iconChinhSua.Stroke = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void stpEditCalendarWork_MouseLeave(object sender, MouseEventArgs e)
        {
            txbEditSaff.Foreground = (Brush)br.ConvertFrom("#474747");
            iconChinhSua.Stroke = (Brush)br.ConvertFrom("#474747");
        }

        private void dopCoppyCalendarSaff_MouseEnter(object sender, MouseEventArgs e)
        {
            txbCoppySaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            iconSaoChep1.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            iconSaoChep2.Stroke = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void dopCoppyCalendarSaff_MouseLeave(object sender, MouseEventArgs e)
        {
            txbCoppySaff.Foreground = (Brush)br.ConvertFrom("#474747");
            iconSaoChep1.BorderBrush = (Brush)br.ConvertFrom("#474747");
            iconSaoChep2.Stroke = (Brush)br.ConvertFrom("#474747");
        }
        private void bodDeleteCalendarSaff_MouseEnter(object sender, MouseEventArgs e)
        {
            iconXoa.Fill = (Brush)br.ConvertFrom("#FF5B4D");
            txbDeleteCalendarSaff.Foreground = (Brush)br.ConvertFrom("#FF5B4D");
        }
        private void Popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodYear.Visibility = Visibility.Collapsed;
            bodMonth.Visibility = Visibility.Collapsed;
            stpListMethond.Visibility = Visibility.Collapsed;
            Popup.Visibility = Visibility.Collapsed;
        }

        private void bodDeleteCalendarSaff_MouseLeave(object sender, MouseEventArgs e)
        {
            iconXoa.Fill = (Brush)br.ConvertFrom("#474747");
            txbDeleteCalendarSaff.Foreground = (Brush)br.ConvertFrom("#474747");
        }
        #endregion#









      

    }
}

