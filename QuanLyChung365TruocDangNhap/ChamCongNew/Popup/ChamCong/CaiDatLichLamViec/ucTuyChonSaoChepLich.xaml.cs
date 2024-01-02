using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucTuyChonSaoChepLich.xaml
    /// </summary>
    public partial class ucTuyChonSaoChepLich : UserControl
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        string month;
        string year;
        string month1;
        string id;
        MainWindow Main;
        ucCaiDatLichLamViec ucSetting;
        public ucTuyChonSaoChepLich(MainWindow main, string cy_id, ucCaiDatLichLamViec ucSetting, string month, string year)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            this.id = cy_id;
            this.ucSetting = ucSetting;
            dteSelectedMonth = new Calendar();
            dteSelectedMonth.Visibility = Visibility.Collapsed;
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            dteSelectedMonth.MouseLeftButtonDown += Select_thang;
            dteSelectedMonth.DisplayModeChanged += dteSelectedMonth_DisplayModeChanged;
            cl = new List<Calendar>();
            cl.Add(dteSelectedMonth);
            cl = cl.ToList();
        }
        private NewCalendar newCalendar1;
        int listId;
        public async void SaoChepLichLamViec()
        {
            try
            {
                bool allow = true;

                if (!string.IsNullOrEmpty(textThang.Text) && textThang.Text == "-- / ----")
                {
                    allow = false;
                    validateMonth.Text = "Vui lòng Chọn tháng áp dụng";
                }
                if (allow)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/cycle/copyList");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                   // listId = 1;
                    content.Add(new StringContent(Convert.ToString(id)), "listIds");
                    string inputMonth = textThang.Text;
                    string formattedMonth = "";
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
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        this.Visibility = Visibility.Collapsed;
                        ucSetting.LoadCalendarWorkStart(month, year);
                    }
                    //content.Add(new StringContent(id), "cy_id");
                    //request.Content = content;
                    //var response = await client.SendAsync(request);
                    //if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    //{
                    //    var resCoppy = await response.Content.ReadAsStringAsync();
                    //    Root_CoppyCalendar result = JsonConvert.DeserializeObject<Root_CoppyCalendar>(resCoppy);
                    //    newCalendar1 = result.data.newCalendar;
                    //    this.Visibility = Visibility.Collapsed;
                    //    month1 = dteSelectedMonth.DisplayDate.ToString("MM");
                    //    month = month1.Substring(1, 1);
                    //    year = dteSelectedMonth.DisplayDate.ToString("yyyy");
                    //    foreach (var item in ucSetting.listGeneralCalendar)
                    //    {
                    //        if (item.cy_id == newCalendar1.cy_id)
                    //        {
                    //            item.cy_id = newCalendar1.cy_id;
                    //            item.cy_name = $"Bản sao cua {newCalendar1.cy_name}";
                    //            item.apply_month = textThang.Text;
                    //        }
                    //    }
                       ucSetting.LoadCalendarWorkStart(month, year);
                    //}
                } 
            }
            catch (Exception)
            {
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

        private void btnSaveCalendar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SaoChepLichLamViec();
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

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lsvChonThang.Visibility == Visibility.Collapsed)
            {
                lsvChonThang.Visibility = Visibility.Visible;
            }
            else
            {
                lsvChonThang.Visibility = Visibility.Collapsed;
            }
        }
    }
}
