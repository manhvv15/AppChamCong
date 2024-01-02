﻿using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
//using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec.QuanLyChung365TruocDangNhap.ChamCongNew.Entities.funcQuanLyCongTy;

using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec.ChamCong365.Entities.funcQuanLyCongTy;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucInstallCalendarWork.xaml
    /// </summary>
    ///

    public class Years
    {
        public string years { get; set; }
    }
    public partial class ucCaiDatLichLamViec : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainWindow Main;


        private List<DataCalendar> _dataCalendars;
        public List<DataCalendar> DataCalendars
        {
            get { return _dataCalendars; }
            set { _dataCalendars = value; }
        }

        BrushConverter br = new BrushConverter();
        public class Thang
        {
            public string thang { get; set; }
        }
        List<Thang> listThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        public class Nam
        {
            public string nam { get; set; }
        }
        List<Nam> listNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        private ICollectionView calendarView;

        private ObservableCollection<DataCalendar> calendarList;
        public ObservableCollection<string> ItemList { get; set; }
        public ObservableCollection<string> YearList { get; set; }

        public ucCaiDatLichLamViec(MainWindow main, int com_id)
        {
            ItemList = new ObservableCollection<string>();
            for (var i = 1; i <= 12; i++)
            {
                ItemList.Add($"Tháng {i}");
            }
            YearList = new ObservableCollection<string>();
            var c = DateTime.Now.Year;
            if (c != null)
            {
                YearList.Add($"Năm {c - 1}");
                YearList.Add($"Năm {c}");
                YearList.Add($"Năm {c + 1}");
            }
            InitializeComponent();
            this.DataContext = this;
            string month;
            int month1 = DateTime.Now.Month;
            if (month1 < 10)
            {
                month = month1.ToString();
                month = month.Split(' ')[1];
            }
            else
            {
                month = month1.ToString();
            }
            string year = DateTime.Now.ToString("yyyy");
            Main = main;
            txbSelectMonth.Text = "Tháng " + month.ToString();
            txbSelectYear.Text = "Năm" + year.ToString();
            LoadCalendarWorkStart(month, year);
            LoadCalendarWorkEnd(month, year);

            LoadCaMĐ();
            LoadDLNam();
            LoadDLThang();

        }
        private int _IsSmallSize;

        public int IsSmallSize
        {
            get { return _IsSmallSize; }
            set
            {
                _IsSmallSize = value;
                OnPropertyChanged("IsSmallSize");
            }
        }

        private List<Item_CaLamViec> _caComon;
        public List<Item_CaLamViec> caComon
        {
            get { return _caComon; }
            set { _caComon = value; OnPropertyChanged(); }
        }
        public async void LoadCaMĐ()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                string api = API.list_shifts_api;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                Root_CaLamViec dsCa = JsonConvert.DeserializeObject<Root_CaLamViec>(responseContent);

                if (dsCa.data.items != null)
                {
                    caComon = dsCa.data.items;

                }
            }
            catch (Exception)
            {
            }
        }

        #region Call API 

        private List<DataCalendar> _listGeneralCalendar;

        public List<DataCalendar> listGeneralCalendar
        {
            get { return _listGeneralCalendar; }
            set
            {
                _listGeneralCalendar = value;
                OnPropertyChanged();
            }
        }
        public async void LoadCalendarWorkStart(string month, string year)
        {
            try
            {
                if (month == "")
                {
                    month = month1;
                    year = year1;
                }
                else
                {

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_All_Calendar_Work);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(month), "month");
                    content.Add(new StringContent(year), "year");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();

                    RootCalendar result = JsonConvert.DeserializeObject<RootCalendar>(responseContent);

                    if (result.data.data != null)
                    {
                        listGeneralCalendar = result.data.data;
                        cboLichLamViec.ItemsSource = listGeneralCalendar;   
                        DateTime aDateTime;
                        foreach (var item in listGeneralCalendar)
                        {
                            DateTime.TryParse(item.apply_month, out aDateTime);
                            item.apply_month = aDateTime.ToString("MM/yyyy");
                        }
                        lsvDanhSachLichLamViec.ItemsSource = listGeneralCalendar.Where(x => x.is_personal == 0);
                        lsvDanhSachLichLamViecCaNhan.ItemsSource = listGeneralCalendar.Where(x => x.is_personal == 1);

                    }
                }

            }
            catch (Exception)
            {
            }
        }

        private List<DataCalendar> _listPersonalCalendar;

        public List<DataCalendar> listPersonalCalendar
        {
            get { return _listPersonalCalendar; }
            set
            {
                _listPersonalCalendar = value;
                OnPropertyChanged();
            }
        }
        public async void LoadCalendarWorkEnd(string month, string year)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_All_Calendar_Work);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(month1), "month");
                content.Add(new StringContent(year), "year");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                RootCalendar result = JsonConvert.DeserializeObject<RootCalendar>(responseContent);

                if (result.data.data != null)
                {
                    listPersonalCalendar = result.data.data;
                    DateTime aDateTime;
                    foreach (var item in listPersonalCalendar)
                    {
                        DateTime.TryParse(item.apply_month, out aDateTime);
                        item.apply_month = aDateTime.ToString("MM/yyyy");
                    }
                    lsvDanhSachLichLamViec.ItemsSource = listGeneralCalendar.Where(x => x.is_personal == 0);
                    lsvDanhSachLichLamViecCaNhan.ItemsSource = listGeneralCalendar.Where(x => x.is_personal == 1);
                }
            }
            catch (Exception)
            {
            }
        }

        private void Load(object sender, SelectionChangedEventArgs e)
        {
            string year = "", month = "";
            if (lsvNam.SelectedItem != null)
                year = txbSelectYear.ToString().Split(' ')[1];
            else
                year = DateTime.Now.ToString("yyyy");
            if (lsvThang.SelectedIndex != -1)
                month = (lsvThang.SelectedIndex + 1) + "";
            else month = DateTime.Now.ToString("MM");
            LoadCalendarWorkStart(month, year);
            LoadCalendarWorkEnd(month, year);
        }
        #endregion

        #region Popup 
        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Visible)
            {
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;

            }
            else if (borNam.Visibility == Visibility.Visible)
            {
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;

            }
        }
        #endregion

        #region Năm
        private void bodHienThiNam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNam.Visibility == Visibility.Collapsed)
            {

                borNam.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        private void LoadDLNam()
        {
            txbSelectYear.Text = "Năm " + DateTime.Now.Year.ToString();
            listNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            listNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            listNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lsvNam.ItemsSource = listNam;
        }

        private void textSearchNam_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNam = new List<Nam>();
            foreach (var str in listNam)
            {
                if (str.nam.Contains(textSearchNam.Text.ToString()))
                {
                    lstSearchNam.Add(str);

                }
            }
            lsvNam.ItemsSource = lstSearchNam;
            if (textSearchNam.Text == "")
            {
                lsvNam.ItemsSource = listNam;
            }
        }

        private void lsvListYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectYear.Text = lsvNam.SelectedItem.ToString();
            borNam.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
            Main.Nam = lsvNam.SelectedItem.ToString();
        }

        private void lsvNam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNam.ScrollToVerticalOffset(scrollNam.VerticalOffset - e.Delta);
        }

        private void borNam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Nam th = (sender as Border).DataContext as Nam;
            if (th != null)
            {
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                txbSelectYear.Text = th.nam;
                Main.Nam = th.nam;
                string year = "", month = "";
                if (th.nam != null)
                {
                    year = th.nam.ToString().Split(' ')[1];
                }
                else
                {
                    year = DateTime.Now.ToString("yyyy");
                }
                if (txbSelectMonth.Text != null)
                    month = txbSelectMonth.Text.ToString().Split(' ')[1];
                else
                    month = DateTime.Now.ToString("MM");
                LoadCalendarWorkStart(month, year);
                LoadCalendarWorkEnd(month, year);

            }
        }

        #endregion

        #region Tháng
        private void bodHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Collapsed)
            {

                borThang.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {

                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        private void textSearchThang_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchThang = new List<Thang>();
            foreach (var str in listThang)
            {
                if (str.thang.Contains(textSearchThang.Text.ToString()))
                {
                    lstSearchThang.Add(str);

                }
            }
            lsvThang.ItemsSource = lstSearchThang;
            if (textSearchThang.Text == "")
            {
                lsvThang.ItemsSource = listThang;
            }

        }
        private void borThang_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thang th = (sender as System.Windows.Controls.Border).DataContext as Thang;
            if (th != null)
            {
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                txbSelectMonth.Text = th.thang;
                Main.Thang = th.thang;
                string year = "", month = "";
                if (th.thang != null) { month = th.thang.ToString().Split(' ')[1]; }
                else { month = DateTime.Now.ToString("MM"); }
                if (txbSelectYear.Text != null) { year = txbSelectYear.Text.ToString().Split(' ')[1]; }
                else { year = DateTime.Now.ToString("yyyy"); }
                LoadCalendarWorkStart(month, year);
                LoadCalendarWorkEnd(month, year);
            }
        }

        private void LoadDLThang()
        {
            txbSelectMonth.Text = "Tháng " + DateTime.Now.Month.ToString();
            listThang.Add(new Thang { thang = "Tháng 1" });
            listThang.Add(new Thang { thang = "Tháng 2" });
            listThang.Add(new Thang { thang = "Tháng 3" });
            listThang.Add(new Thang { thang = "Tháng 4" });
            listThang.Add(new Thang { thang = "Tháng 5" });
            listThang.Add(new Thang { thang = "Tháng 6" });
            listThang.Add(new Thang { thang = "Tháng 7" });
            listThang.Add(new Thang { thang = "Tháng 8" });
            listThang.Add(new Thang { thang = "Tháng 9" });
            listThang.Add(new Thang { thang = "Tháng 10" });
            listThang.Add(new Thang { thang = "Tháng 11" });
            listThang.Add(new Thang { thang = "Tháng 12" });
            lsvThang.ItemsSource = listThang;
        }
        private void lsvThang_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollThang.ScrollToVerticalOffset(scrollThang.VerticalOffset - e.Delta);
        }
        #endregion

        #region MouseHover
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
        #endregion

        #region MouseClick 

        private void bodAddLich_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string year = "", month = "";
            if (lsvNam.SelectedItem != null)
                year = txbSelectYear.ToString().Split(' ')[1];
            else
                year = DateTime.Now.ToString("yyyy");
            if (lsvThang.SelectedIndex != -1)
                month = (lsvThang.SelectedIndex + 1) + "";
            else month = DateTime.Now.ToString("MM");
            LoadCalendarWorkStart(month, year);
            LoadCalendarWorkEnd(month, year);
            Main.grShowPopup.Children.Add(new ucThemMoiLichLamViec(Main, this));
        }
        string month1;
        string year1;

        private void bodButonCoppyCalendar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Border b = sender as Border;
            //DataCalendar data = (DataCalendar)b.DataContext;
            //month = txbSelectMonth.Text;
            // f
            lsvDanhSachLichLamViec.Items.Refresh();
            string thang = txbSelectMonth.Text;
            string nam = txbSelectYear.Text;
            month1 = thang.ToString().Split(' ')[1];
            year1 = nam.ToString().Split(' ')[1];
            //Nam n = (Nam)lsvNam.SelectedItem;
            //if (n != null)
            //{
            //    //year = DateTime.Now.ToString("yyyy");
            //}
            Main.grShowPopup.Children.Add(new ucSaoChepLichLamViec(Main, this));
            //Main.grShowPopup.Children.Add(new ucSaoChep(Main, 10, 2023));

        }
        private void dopListSaffSmall_Click(object sender, RoutedEventArgs e)
        {
            MenuItem p = sender as MenuItem;
            DataCalendar data = (DataCalendar)p.DataContext;

            Main.grShowPopup.Children.Add(new ucDanhSachNhanVien(Main, data.cy_id, data.apply_month));
        }

        private void stpEditCalendarWork_Click(object sender, RoutedEventArgs e)
        {
            MenuItem p = sender as MenuItem;
            DataCalendar data = (DataCalendar)p.DataContext;
            Main.grShowPopup.Children.Add(new ucChinhSuaLichLamViec(Main, data.cy_name, Main.IdAcount, data.cy_id, this, data.apply_month));

        }

        private void dopCoppyCalendarSaff_Click(object sender, RoutedEventArgs e)
        {

            MenuItem p = sender as MenuItem;
            DataCalendar data = (DataCalendar)p.DataContext;

            Main.grShowPopup.Children.Add(new ucTuyChonSaoChepLich(Main, data.cy_id.ToString(), this, txbSelectMonth.Text, txbSelectYear.Text));
            lsvDanhSachLichLamViec.Items.Refresh();
        }

        private void bodDeleteCalendarSaff_Click(object sender, RoutedEventArgs e)
        {
            //lsvDanhSachLichLamViec.Items.Refresh();
            MenuItem p = sender as MenuItem;
            DataCalendar data = (DataCalendar)p.DataContext;
            Main.grShowPopup.Children.Add(new ucXoaLichLamViec(Main, this, data.cy_id));
            lsvDanhSachLichLamViec.Items.Refresh();

        }

        private void dopAddSaff_Click(object sender, RoutedEventArgs e)
        {
            MenuItem p = sender as MenuItem;
            DataCalendar data = (DataCalendar)p.DataContext;
            Main.grShowPopup.Children.Add(new ucThemMoiNhanVien(Main, data.cy_id, this, data.apply_month));
        }

        #endregion

        private void WrapPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void lsvDanhSachLichLamViec_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    var scrollViewer = FindVisualChild<ScrollViewer>(lsvDanhSachLichLamViec);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
                        e.Handled = true;
                    }

                }
                else
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }
        private void lsvDanhSachLichLamViecCaNhan_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    var scrollViewer = FindVisualChild<ScrollViewer>(lsvDanhSachLichLamViecCaNhan);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
                        e.Handled = true;
                    }

                }
                else
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }
        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }

        private void cboLichLamViec_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboLichLamViec.SelectedIndex = -1;
                string textSearch = cboLichLamViec.Text;
                cboLichLamViec.Items.Refresh();
                cboLichLamViec.ItemsSource = listGeneralCalendar.Where(t => t.cy_name.RemoveUnicode().ToLower().Contains(textSearch.RemoveUnicode().ToLower())).Prepend(new DataCalendar() { cy_id = 0, cy_name = "Tất cả lịch làm việc" });
            }
        }

        private void cboLichLamViec_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboLichLamViec.SelectedIndex = -1;
            string textSearch = cboLichLamViec.Text + e.Text;
            cboLichLamViec.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboLichLamViec.Text = "";
                cboLichLamViec.Items.Refresh();
                cboLichLamViec.ItemsSource = listGeneralCalendar.Prepend(new DataCalendar() { cy_id= 0,cy_name="Tất cả lịch làm việc"});
                cboLichLamViec.SelectedIndex = -1;
            }
            else
            {
                cboLichLamViec.ItemsSource = "";
                cboLichLamViec.Items.Refresh();
                cboLichLamViec.ItemsSource = listGeneralCalendar.Where(t => t.cy_name.RemoveUnicode().ToLower().Contains(textSearch.RemoveUnicode().ToLower())).Prepend(new DataCalendar() { cy_id = 0, cy_name = "Tất cả lịch làm việc" });
            }

        }

        private void cboLichLamViec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                List<DataCalendar> searchCalendar = new List<DataCalendar>();
                var calendar = cboLichLamViec.SelectedItem as DataCalendar;
                searchCalendar.Add(calendar);
                if (calendar.cy_id != 0)
                {
                    if (calendar.is_personal == 1)
                    {
                        lsvDanhSachLichLamViecCaNhan.ItemsSource = searchCalendar;
                        lsvDanhSachLichLamViec.ItemsSource = null;

                    }
                    else
                    {
                        lsvDanhSachLichLamViec.ItemsSource = searchCalendar;
                        lsvDanhSachLichLamViecCaNhan.ItemsSource = null;
                    }
                }
                else
                {
                    lsvDanhSachLichLamViec.ItemsSource = listGeneralCalendar.Where(x => x.is_personal == 0);
                    lsvDanhSachLichLamViecCaNhan.ItemsSource = listGeneralCalendar.Where(x => x.is_personal == 1);
                }
            }
            catch (Exception ex) { }
            
       
        }
    }

}
