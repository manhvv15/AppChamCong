using System.Collections.Generic;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.TimeKeeping;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec;
using Newtonsoft.Json;
using System.Net.Http;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
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
            cl = new List<Calendar>();
            cl.Add(dteSelectedMonth);
            cl = cl.ToList();
            ucSetting.LoadCalendarWorkStart(ucSetting.textSearchThang.Text.ToString(), ucSetting.textSearchNam.Text.ToString());
            lsvCalendarMonth.ItemsSource = ucSetting.listGeneralCalendar;
            txtLichCuaThang.Text = ucSetting.txbSelectMonth.Text.Split()[1] + "/" + ucSetting.txbSelectYear.Text.Split()[1];

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
           
                Border cb = sender as Border;
                if (chonnv.Text == "Chọn tất cả")
                {
                    if (ucSetting.listGeneralCalendar != null)
                    {
                        foreach (var item in ucSetting.listGeneralCalendar)
                        {
                            item.status = 1;
                            item.IsChecked = true;
                        }
                    }
                    chonnv.Text = "Bỏ chọn";
                }

                else
                {
                    if (ucSetting.listGeneralCalendar != null)
                    {
                        foreach (var item in ucSetting.listGeneralCalendar)
                        {
                            item.status = 0;
                            item.IsChecked = false;
                        }
                    }

                    chonnv.Text = "Chọn tất cả";
                }
        }
        string month;
        string year;
        string month1;
        private async void CoppyCalendar(object sender, MouseButtonEventArgs e)
        {
            List<string> nv = new List<string>();
            validateMonth.Text = "";
            bool allow = true;
            foreach (var item in ucSetting.listGeneralCalendar)
            {
                if (item.IsChecked == true)
                    nv.Add(item.cy_id.ToString());
            }

            if (!string.IsNullOrEmpty(textThang.Text) && textThang.Text == "-- / ----")
            {
                allow = false;
                validateMonth.Text = "Vui lòng chọn tháng áp dụng";
            }

            if (nv.Count <= 0)
            {
                allow = false;
            }
            if (allow)
            {
                foreach (var itemCalendar in nv)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Coppy_CalendarWork_Api);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(itemCalendar), "cy_id");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resCoppy = await response.Content.ReadAsStringAsync();
                        Root_CoppyCalendar result = JsonConvert.DeserializeObject<Root_CoppyCalendar>(resCoppy);
                        this.Visibility = Visibility.Collapsed;
                        month1 = dteSelectedMonth.DisplayDate.ToString("MM");
                        month = month1.Substring(1, 1);
                        year = dteSelectedMonth.DisplayDate.ToString("yyyy");
                        foreach (var item in ucSetting.listGeneralCalendar)
                        {
                            if (item.cy_id == result.data.newCalendar.cy_id)
                            {
                                item.cy_id = result.data.newCalendar.cy_id;
                                item.cy_name = $"Bản sao cua {result.data.newCalendar.cy_name}";
                                item.apply_month = textThang.Text;
                            }
                        }
                       
                    }
                }
                ucSetting.LoadCalendarWorkStart(month, year);
            }
        }
    }
}
