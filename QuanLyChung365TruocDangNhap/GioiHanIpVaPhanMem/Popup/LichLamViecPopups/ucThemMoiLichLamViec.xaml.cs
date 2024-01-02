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
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCong365.Entities;
using System.Net.Http;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.LichLamViecPopups
{
    /// <summary>
    /// Interaction logic for ucCreateCalendarWork.xaml
    /// </summary>
    public partial class ucThemMoiLichLamViec : UserControl, INotifyPropertyChanged
    {
        frmMain Main;
        List<ListShiftEntities.Item> ListAllShift = new List<ListShiftEntities.Item>();
        BrushConverter bc = new BrushConverter();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static int static_month, static_year;
        int month, year;
        public ucThemMoiLichLamViec(frmMain main)
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
            GetListShift();

        }
        #region Popup Lich
        private void Select_thang(object sender, MouseButtonEventArgs e)
        {
            dteSelectedMonth.Visibility = dteSelectedMonth.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            lsvChonThang.ItemsSource = cl;
            flag = 1;
        }
        int flag = 0;
        private void dteSelectedMonth_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
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
            if (ListAllShift.Where(x => x.isSelect).FirstOrDefault() == null)
            {
                allow = false;
            }

            if (allow)
            {
                List<ListShiftEntities.Item> listShiftId = ListAllShift.Where(x => x.isSelect).ToList();
                //Main.grShowPopup.Children.Add(new ucChuyenTiepChonCaLamViec(Main, tb_TenLichLV.Text, textThang.Text, ComboBox.SelectedIndex, dpEnd.SelectedDate + "", ucSetting));
                Main.pnlShowPopUp.Children.Add(new ucTaoChuKy(Main, tb_TenLichLV.Text, textThang.Text, ComboBox.SelectedIndex, dpEnd.SelectedDate + "", listShiftId));
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
        private void ComboBoxOpen_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (bodListAppCollapsed.Visibility == Visibility.Collapsed)
            {
                lsvApp.ItemsSource = ListAllShift;
                lsvApp.Items.Refresh();
                bodListAppCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListAppCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvApp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bodListAppCollapsed.Visibility = Visibility.Collapsed;
        }

        private void LsvAppItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((Border)sender).DataContext as ListShiftEntities.Item;
            if (selectedItem != null)
            {
                if (selectedItem.isSelect == false)
                {
                    ListAllShift.Where(x => x.shift_id == selectedItem.shift_id).FirstOrDefault().isSelect = true;
                }
                else
                {
                    ListAllShift.Where(x => x.shift_id == selectedItem.shift_id).FirstOrDefault().isSelect = false;
                }
                lsvApp.ItemsSource = ListAllShift;
                lsvApp.Items.Refresh();
                lsvListApp.ItemsSource = ListAllShift.Where(x => x.isSelect == true);
                lsvListApp.Items.Refresh();
            }

        }
        private void UnSelectedApp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var UnSelectedApp = lsvListApp.SelectedItem as ListShiftEntities.Item;
            if (UnSelectedApp != null)
            {
                ListAllShift.Where(x => x.shift_id == UnSelectedApp.shift_id).ToList().First().isSelect = false;
            }
            lsvListApp.ItemsSource = ListAllShift.Where(x => x.isSelect == true);
            lsvListApp.Items.Refresh();
        }

        #region CallApi
        private async void GetListShift()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "http://210.245.108.202:3000/api/qlc/shift/list");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ListShiftEntities.Root result = JsonConvert.DeserializeObject<ListShiftEntities.Root>(responseContent);
                    ListAllShift = result.data.items;
                    lsvApp.ItemsSource = ListAllShift;

                }

            }
            catch { }
        }
        #endregion

    }
}
