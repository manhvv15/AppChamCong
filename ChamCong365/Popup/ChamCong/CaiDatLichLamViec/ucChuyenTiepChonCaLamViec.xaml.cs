using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.APIs;
using ChamCong365.funcQuanLyCongTy;
using ChamCong365.Popup.ChamCong;
using ChamCong365.Popup.ChamCong.CaiDatLichLamViec;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChamCong365.OOP;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec.ChamCong365.Entities.funcQuanLyCongTy;
using ChamCong365.TimeKeeping;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucSelectShift.xaml
    /// </summary>
    public partial class ucChuyenTiepChonCaLamViec : UserControl
    {
      
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MainWindow Main;
        string ten;
        string thang;
        int select;
        string date;
        ucCaiDatLichLamViec ucSetting;
        public ucChuyenTiepChonCaLamViec(MainWindow main, string ten, string thang, int select, string date, ucCaiDatLichLamViec ucSetting)
        {
            InitializeComponent();
            Main = main;
            this.DataContext = this;
            this.ten = ten;
            this.thang = thang;
            this.select = select;
            this.date = date;
            this.ucSetting = ucSetting;
            LoadShiftInChonCa();
           
        }
        #region Chọn ca làm việc
        private List<Item_CaLamViec> _listShift;

        public List<Item_CaLamViec> listShift
        {
            get { return _listShift; }
            set
            {
                _listShift = value;
                OnPropertyChanged();
            }
        }
        public async void LoadShiftInChonCa()
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
                Root_CaLamViec result = JsonConvert.DeserializeObject<Root_CaLamViec>(responseContent);
                if (result.data.items != null)
                {
                    listShift = result.data.items;
                    lsvCaLamViec.ItemsSource = listShift;
                }
            }
            catch (Exception) { }

        }

        private List<Item_CaLamViec> ca = new List<Item_CaLamViec>();

        private void ChonCaLamViec(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Item_CaLamViec data = (Item_CaLamViec)cb.DataContext;
            ca.Add(data);

        }

        private void HuyChon(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Item_CaLamViec data = (Item_CaLamViec)cb.DataContext;
            ca.Remove(data);
        }
        #endregion


        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Main.grShowPopup.Children.Add(new ucThemMoiLichLamViec(Main,null));
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

     
        private void bodNextCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucChuyenTiepTaoChuKyLamViec(Main, ten, thang, select, date, ca,ucSetting));
            this.Visibility = Visibility.Collapsed;
        }

        private void bodBackCalendar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiLichLamViec(Main,ucSetting));  
            this.Visibility = Visibility.Collapsed;
        }
    }
}
