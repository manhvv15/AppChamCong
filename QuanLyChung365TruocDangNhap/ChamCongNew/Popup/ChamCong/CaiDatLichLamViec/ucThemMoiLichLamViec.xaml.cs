using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucCreateCalendarWork.xaml
    /// </summary>
    public partial class ucThemMoiLichLamViec : UserControl, INotifyPropertyChanged
    {
        MainWindow Main;
        ucCaiDatLichLamViec ucSetting;
        BrushConverter bc = new BrushConverter();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static int static_month, static_year;
        int month, year;
        public ucThemMoiLichLamViec(MainWindow main, ucCaiDatLichLamViec ucSetting)
        {
            InitializeComponent();
            Main = main;
            this.DataContext = Main;
            dteSelectedMonth = new Calendar();
            dteSelectedMonth.Visibility = Visibility.Collapsed;
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            dteSelectedMonth.MouseLeftButtonDown += Select_thang;
            dteSelectedMonth.DisplayModeChanged += dteSelectedMonth_DisplayModeChanged;
            cl = new List<Calendar>();
            cl.Add(dteSelectedMonth);
            cl = cl.ToList();
            this.ucSetting = ucSetting;
        }
        #region Popup Lich
        private void Select_thang(object sender, MouseButtonEventArgs e)
        {
            if (lsvChonThang.Visibility == Visibility.Collapsed)
            {
                lsvChonThang.Visibility = Visibility.Visible;
            }

            if (lsvChonThang.Visibility == Visibility.Visible)
            {
                dteSelectedMonth.Visibility = dteSelectedMonth.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                lsvChonThang.ItemsSource = cl;
                flag = 1;
            }

        }
        private void lsvChonThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lsvChonThang.Visibility = Visibility.Collapsed;
        }
        private void lsvChonThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lsvChonThang.Visibility = Visibility.Collapsed;
        }
        int flag = 0;
        private void dteSelectedMonth_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            lsvChonThang.Visibility = Visibility.Collapsed;
            var x = dteSelectedMonth.DisplayDate.ToString("MM/yyyy");
            if (flag == 0)
                x = "";
            else
                x = dteSelectedMonth.DisplayDate.ToString("MM/yyyy");
            if (textThang != null && !string.IsNullOrEmpty(x))
            {
                textThang.Text = x;
                DateTime a = DateTime.Parse(x);
                dpEnd.SelectedDate = a;
            }
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonth.DisplayDate != null && flag > 0)
            {
                dteSelectedMonth.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }
        Calendar dteSelectedMonth { get; set; }

        private List<Calendar> _cl;

        public List<Calendar> cl
        {
            get { return _cl; }
            set
            {
                _cl = value; OnPropertyChanged();
            }
        }
        #endregion

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
        private void bodContinue_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            validateName.Text = validateMonth.Text = "";

            if (string.IsNullOrEmpty(tb_TenLichLV.Text))
            {
                allow = false;
                validateName.Text = "Vui lòng nhập tên lịch làm việc";
            }

            if (textThang.Text == "-- / ----")
            {
                allow = false;
                validateMonth.Text = "Vui lòng chọn tháng áp dụng";
            }

            if (allow)
            {
                Main.grShowPopup.Children.Add(new ucChuyenTiepChonCaLamViec(Main, tb_TenLichLV.Text, textThang.Text, ComboBox.SelectedIndex, dpEnd.SelectedDate + "", ucSetting));
                this.Visibility = Visibility.Collapsed;
            }
        }



        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ExitCreateCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
