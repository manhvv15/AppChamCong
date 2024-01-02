using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using Newtonsoft.Json;
using System.Net.Http;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucCoppyCalendarWork.xaml
    /// </summary>
    public partial class ucSaoChepLichLamViec : UserControl, INotifyPropertyChanged
    {
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ucCaiDatLichLamViec ucSetting;
        string month;
        string year;
        public ucSaoChepLichLamViec(MainWindow main, ucCaiDatLichLamViec ucSetting)
        {
            InitializeComponent();
            Main = main;
            this.DataContext = this;
            this.ucSetting = ucSetting;
            dteSelectedMonth = new Calendar();
            dteSelectedMonth.Visibility = Visibility.Collapsed;
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            dteSelectedMonth.MouseLeftButtonDown += Select_thang;
            dteSelectedMonth.DisplayModeChanged += dteSelectedMonth_DisplayModeChanged;
            //this.month = (month + 1) + "";
            //this.year = (year + DateTime.Now.Year - 1) + "";
            cl = new List<Calendar>();
            cl.Add(dteSelectedMonth);
            cl = cl.ToList();
            ucSetting.LoadCalendarWorkStart(ucSetting.textSearchThang.Text.ToString(), ucSetting.textSearchNam.Text.ToString());
            lsvCalendarMonth.ItemsSource = ucSetting.listGeneralCalendar;
            txtLichCuaThang.Text = ucSetting.txbSelectMonth.Text.Split()[1] + "/" + ucSetting.txbSelectYear.Text.Split()[1];
            //getListCalendar();
        }
        private async void getListCalendar()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/cycle/list");
            request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
            request.Headers.Add("content-type", "application/json");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent("2023"), "year");
            content.Add(new StringContent("2"), "month");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            var responseContent = response.Content.ReadAsStringAsync();

        }

        private void Select_thang(object sender, MouseButtonEventArgs e)
        {
            dteSelectedMonth.Visibility = dteSelectedMonth.Visibility == Visibility.Visible
               ? Visibility.Collapsed
               : Visibility.Visible;
            flag = 1;
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
                _cl = value;
                OnPropertyChanged();
            }
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ChonNhanvien(object sender, MouseButtonEventArgs e)
        {

            //Border cb = sender as Border;
            //if (chonnv.Text == "Chọn tất cả")
            //{
            //    if (ucSetting.listGeneralCalendar != null)
            //    {
            //        foreach (var item in ucSetting.listGeneralCalendar)
            //        {
            //            item.status = 1;
            //            item.IsChecked = true;
            //        }
            //    }
            //    chonnv.Text = "Bỏ chọn";
            //}

            //else
            //{
            //    if (ucSetting.listGeneralCalendar != null)
            //    {
            //        foreach (var item in ucSetting.listGeneralCalendar)
            //        {
            //            item.status = 0;
            //            item.IsChecked = false;
            //        }
            //    }

            //    chonnv.Text = "Chọn tất cả";
            //}
            //lsvCalendarMonth.Items.Refresh();
        }
        //string month;
        //string year;
        string month1;
        //List<string> nv = new List<string>();
        private async void CoppyCalendar(object sender, MouseButtonEventArgs e)
        {
            //List<string> nv = new List<string>();
            //nv = null;

            validateMonth.Text = "";
            bool allow = true;
            foreach (var item in ucSetting.listGeneralCalendar)
            {
                if (item.IsChecked == true)
                    nv.Add(item.cy_id.ToString() + ",");
            }
            for (int i = 0; i < nv.Count; i++)
            {
                if (nv[i].EndsWith(",") && i == nv.Count - 1)
                {
                    nv[i] = nv[i].Substring(0, nv[i].Length - 1);
                }
                //listString = Convert.ToString(listString);
            }
            if (!string.IsNullOrEmpty(textThang.Text) && textThang.Text == "-- / ----")
            {
                allow = false;
                validateMonth.Text = "Vui lòng chọn tháng áp dụng";
            }

            if (nv.Count < 0)
            {
                allow = false;
            }
            if (allow)
            {

                //var client = new HttpClient();
                //var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Coppy_CalendarWork_Api);
                //request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                //var content = new MultipartFormDataContent();
                //content.Add(new StringContent(itemCalendar), "cy_id");
                //request.Content = content;
                //var response = await client.SendAsync(request);
                //if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                //{
                //    var resCoppy = await response.Content.ReadAsStringAsync();
                //    Root_CoppyCalendar result = JsonConvert.DeserializeObject<Root_CoppyCalendar>(resCoppy);
                //    this.Visibility = Visibility.Collapsed;
                //    month1 = dteSelectedMonth.DisplayDate.ToString("MM");
                //    month = month1.Substring(1, 1);
                //    year = dteSelectedMonth.DisplayDate.ToString("yyyy");
                //    foreach (var item in ucSetting.listGeneralCalendar)
                //    {
                //        if (item.cy_id == result.data.newCalendar.cy_id)
                //        {
                //            item.cy_id = result.data.newCalendar.cy_id;
                //            item.cy_name = $"Bản sao cua {result.data.newCalendar.cy_name}";
                //            item.apply_month = textThang.Text;
                //        }
                //    }

                //}
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/cycle/copyList");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                foreach (string value in nv)
                {
                    if (!nvAdd.Contains(value))
                    {
                        nvAdd.Add(value);
                    }
                }
                string listId = string.Join("", nvAdd);
                //MessageBox.Show(Convert.ToString(listId));
                content.Add(new StringContent(Convert.ToString(listId)), "listIds");
                string formattedMonth = "";
                string year = "";
                string month = "";
                string inputMonth = textThang.Text;
                // string inputMonth = "07/2023";
                string[] parts = inputMonth.Split('/'); // Tách chuỗi thành mảng gồm hai phần, "07" và "2023"
                if (parts.Length == 2)
                {
                    year = parts[1];
                    month = parts[0];
                    formattedMonth = $"{year}-{month}";
                }
                content.Add(new StringContent(formattedMonth), "month");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucSetting.LoadCalendarWorkStart(month, year);
                }
            }

        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lsvChonThang.Visibility = Visibility.Visible;
        }

        private void lsvChonThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lsvChonThang.Visibility = Visibility.Collapsed;
        }
        List<DataCalendar> listDataCalendarAll = new List<DataCalendar>();
        public List<string> nv = new List<string>();
        public List<string> nvAdd = new List<string>();
        private void ChonTatCa(object sender, RoutedEventArgs e)
        {
            try
            {
                foreach (var item in ucSetting.listGeneralCalendar)
                {
                    //listDataCalendarAll.Add(item);
                    item.IsChecked = true;
                    nv.Add(item.cy_id.ToString() + ",");
                }
                for (int i = 0; i < nv.Count; i++)
                {
                    if (nv[i].EndsWith(",") && i == nv.Count - 1)
                    {
                        nv[i] = nv[i].Substring(0, nv[i].Length - 1);
                    }
                    //listString = Convert.ToString(listString);
                }
                //MessageBox.Show(string.Join("", nv));
                lsvCalendarMonth.ItemsSource = ucSetting.listGeneralCalendar;
                lsvCalendarMonth.Items.Refresh();   
            }
            catch { }
            
        }

        private void HuyChonTatCa(object sender, RoutedEventArgs e)
        {
            foreach (var item in ucSetting.listGeneralCalendar)
            {
                //listDataCalendarAll.Add(item);
                item.IsChecked = false;
            }
            nvAdd = new List<string>();
            nv = new List<string>();
            lsvCalendarMonth.Items.Refresh();
        }

        private void Lich_Checked(object sender, RoutedEventArgs e)
        {
           ((sender as CheckBox).DataContext as DataCalendar).IsChecked = true; 
        }

        private void Lich_UnCheck(object sender, RoutedEventArgs e)
        {
            ((sender as CheckBox).DataContext as DataCalendar).IsChecked = false;
        }
    }
}
