using ChamCong365.Popup.PopupTimeKeeping;
using ChamCong365.TimeKeeping;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ChamCong365.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucCreateGiamBienChe.xaml
    /// </summary>
    public partial class ucCreateGiamBienChe : UserControl
    {
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        public static string static_month, static_year;
        int month, year;
        public ucCreateGiamBienChe(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            displayDays();
            ucLoadDaySmall ucl = new ucLoadDaySmall(Main);
            List<string> dsTenCongTy = new List<string>() { "Hà hung 2", "PT shop", "PT Shop 2", "PT Shop 3" };
            List<string> dsTenPhongBan = new List<string>() { "Kỹ thuật", "Biên tập", "Kinh doanh", "Đề án", "Phòng seo", "Phòng đào tạo", "Phòng sáng tạo" };
            List<string> dsTenDepartment = new List<string>() { "Kỹ Thuật", "Biên Tập", "Kinh doanh", "Đề Án", "Phòng Seo", "Phòng Đào Tạo", "Phòng sáng Tạo" };
           
            lsvOldCompany.ItemsSource = dsTenCongTy;  

          
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = (((Rectangle)sender).Parent) as Grid;
            var bodSelectCollasped = grid.Parent as Border;
            bodSelectCollasped.Visibility = Visibility.Collapsed;
        }

        #region SelectOldCompany    
        private void bodSelectOldCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListOldCompanyCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListOldCompanyCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListOldCompanyCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvOldCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectOldCompany.Text = lsvOldCompany.SelectedItem.ToString();
            txbSelectOldCompany.Foreground = (Brush)bc.ConvertFromString("#474747");
            txtSelectOldCompany.Text = lsvOldCompany.SelectedItem.ToString();

            bodListOldCompanyCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectCurrentDepartment    
        private void bodSelectCurrentDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCurrentDepartmentCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListCurrentDepartmentCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCurrentDepartmentCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCurrentDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectCurrentDepartment.Text = lsvCurrentDepartment.SelectedItem.ToString();
            txbSelectCurrentDepartment.Foreground = (Brush)bc.ConvertFromString("#474747");
            txtSelectCurrentDepartment.Text = lsvCurrentDepartment.SelectedItem.ToString();

            bodListCurrentDepartmentCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectStaffName    
        private void bodSelectStaffName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListStaffNameCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListStaffNameCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListStaffNameCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvStaffName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectStaffName.Text = lsvStaffName.SelectedItem.ToString();
            txbSelectStaffName.Foreground = (Brush)bc.ConvertFromString("#474747");
            txtSelectStaffName.Text = lsvStaffName.SelectedItem.ToString();

            bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectCurrentPosition    
        private void bodSelectCurrentPosition_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCurrentPositionCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListCurrentPositionCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCurrentPositionCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCurrentPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectCurrentPosition.Text = lsvCurrentPosition.SelectedItem.ToString();
            txbSelectCurrentPosition.Foreground = (Brush)bc.ConvertFromString("#474747");
            txtSelectCurrentPosition.Text = lsvCurrentPosition.SelectedItem.ToString();

            bodListCurrentPositionCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectCalendar
        public void displayDays()
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            ucCaiDatLichLamViec ucI = new ucCaiDatLichLamViec(Main, Main.IdAcount);
            ucI.txbSelectMonth.Text = month.ToString();
            //string MonthName = DateTime.Now.ToString("MMMM", CultureInfo.CreateSpecificCulture(month));
            CultureInfo viVietNam = new CultureInfo("vi-VN");
            DateTimeFormatInfo vietnam = viVietNam.DateTimeFormat;
            string monthName = vietnam.MonthNames[month - 1];
            //string monthName = DateTimeFormatInfo.CurrentInfo.GetMonthName(month);
            txbLoadNumMonth.Text = monthName + " " + year;
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

                Console.WriteLine("Thg" + month);
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

        private void imgNextop_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            loadFistDaySmall.Children.Clear();

            if (month >= 12)
            {
                month = 11;
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




        private void txbNowMonth_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DateTime now = DateTime.Now;
            month = now.Month;
            year = now.Year;
            ucLoadMonth ucm = new ucLoadMonth();
            BrushConverter bc = new BrushConverter();
            ucm.bodLoadMonth.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            ucm.txbLoadMonth.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        #endregion
        #region SelectShift    
        private void bodSelectShift_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListShiftCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListShiftCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListShiftCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvShift_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectShift.Text = lsvShift.SelectedItem.ToString();
            txbSelectShift.Foreground = (Brush)bc.ConvertFromString("#474747");
            txtSelectShift.Text = lsvShift.SelectedItem.ToString();

            bodListShiftCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectType    
        private void bodSelectType_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListTypeCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListTypeCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListTypeCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectType.Text = lsvType.SelectedItem.ToString();
            txbSelectType.Foreground = (Brush)bc.ConvertFromString("#474747");
            txtSelectType.Text = lsvType.SelectedItem.ToString();

            bodListTypeCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectRule    
        private void bodSelectRule_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListRuleCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListRuleCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListRuleCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvRule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectRule.Text = lsvRule.SelectedItem.ToString();
            txbSelectRule.Foreground = (Brush)bc.ConvertFromString("#474747");
            txtSelectRule.Text = lsvRule.SelectedItem.ToString();

            bodListRuleCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion

    }
}
