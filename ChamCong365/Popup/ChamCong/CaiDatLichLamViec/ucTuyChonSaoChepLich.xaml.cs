using ChamCong365.OOP.ChamCong.CaiDatLichLamViec;
using ChamCong365.TimeKeeping;
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

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
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
                    var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Coppy_CalendarWork_Api);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(id), "cy_id");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resCoppy = await response.Content.ReadAsStringAsync();
                        Root_CoppyCalendar result = JsonConvert.DeserializeObject<Root_CoppyCalendar>(resCoppy);
                        newCalendar1 = result.data.newCalendar;
                        this.Visibility = Visibility.Collapsed;
                        month1 = dteSelectedMonth.DisplayDate.ToString("MM");
                        month = month1.Substring(1, 1);
                        year = dteSelectedMonth.DisplayDate.ToString("yyyy");
                        foreach (var item in ucSetting.listGeneralCalendar)
                        {
                            if (item.cy_id == newCalendar1.cy_id)
                            {
                                item.cy_id = newCalendar1.cy_id;
                                item.cy_name = $"Bản sao cua {newCalendar1.cy_name}";
                                item.apply_month = textThang.Text;
                            }
                        }
                        ucSetting.LoadCalendarWorkStart(month, year);
                    }
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
