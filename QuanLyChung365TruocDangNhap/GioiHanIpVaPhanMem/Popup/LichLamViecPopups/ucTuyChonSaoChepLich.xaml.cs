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
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using static QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.LichLamViecPopups.ucTaoChuKy;
using Syncfusion.Windows.Shared;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.LichLamViecPopups
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
        API_LichLamViec_List.Cycle cycle;
        frmMain Main;

        public ucTuyChonSaoChepLich(frmMain main, API_LichLamViec_List.Cycle cycle)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            this.cycle = cycle;

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

        public async void SaoChepLichLamViec()
        {
            try
            {
                var json = JsonConvert.SerializeObject(cycle.cy_detail);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/cycle/create");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("Bản sao của " + cycle.cy_name), "cy_name");
                content.Add(new StringContent(DateTime.Parse(textThang.Text).ToString("yyyy-MM-dd")), "apply_month");
                content.Add(new StringContent(json), "cy_detail");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                this.Visibility = Visibility.Collapsed;


                //var client = new HttpClient();
                //var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/cycle/create");
                //request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                //var content = new StringContent("{\"cy_name\":\"ssds\",\"apply_month\":\"2023-11-18\",\"cy_detail\":\"[{\\\"date\\\":\\\"2023-11-18\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-20\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456,2025475,2025467\\\"},{\\\"date\\\":\\\"2023-11-21\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-22\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-23\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-24\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-25\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-27\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-28\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-29\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"},{\\\"date\\\":\\\"2023-11-30\\\",\\\"shift_id\\\":\\\"2025481,2025454,2025456\\\"}]\"}", null, "application/json");
                //request.Content = content;
                //var response = await client.SendAsync(request);
                //var responseContent = await response.Content.ReadAsStringAsync();
                ////response.EnsureSuccessStatusCode();
                ////Console.WriteLine();


            }
            catch { }
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


    }
}
