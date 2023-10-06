using ChamCong365.Popup.DatePicker;
using System.Globalization;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.OOP;
using System.Collections.Generic;
using ChamCong365.funcQuanLyCongTy;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChamCong365.APIs;
using Newtonsoft.Json;
using System.Net.Http;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec.ChamCong365.Entities.funcQuanLyCongTy;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec;
using static ChamCong365.Popup.ChamCong.CaiDatLichLamViec.ucChinhSuaLichLamViec;
using System.Linq;
using System.Net;
using System.Text;
using ChamCong365.TimeKeeping;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucNextCreateCalendarWork.xaml
    /// </summary>
    public partial class ucChuyenTiepTaoChuKyLamViec : UserControl
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
        int month;
        int year;
        int start;
        ucCaiDatLichLamViec ucSetting;
        public ucChuyenTiepTaoChuKyLamViec(MainWindow main, string ten, string thang, int select, string date,
            List<Item_CaLamViec> ca, ucCaiDatLichLamViec ucSetting)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            this.ten = ten;
            txbViewTextMonth.Text = "Lịch Tháng " + thang;
            this.thang = thang;
            this.select = select;
            this.date = date;
            this.dsca = ca;
            this.ucSetting = ucSetting;
            LoadShiftInChuKy();
            LoadShiftDetail(); 
        }
        #region# ListCa
        private List<Item_CaLamViec> _listCa;

        public List<Item_CaLamViec> listCa
        {
            get { return _listCa; }
            set
            {
                _listCa = value;
                OnPropertyChanged();
            }
        }

        public async void LoadShiftInChuKy()
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
                    listCa = result.data.items;
                    lsvChonCaChuKyLamViec.ItemsSource = listCa;
                }
            }
            catch (Exception) { }
        }


        List<Item_CaLamViec> dsca;

        public class lichlamviec : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public int id;
            public int ngay { get; set; }

            public int _ca;
            public int ca
            {
                get { return _ca; }
                set
                {
                    _ca = value;
                    OnPropertyChanged();
                }
            }

            public int _status;
            public int status
            {
                get { return _status; }
                set
                {
                    _status = value;
                    OnPropertyChanged();
                }
            }

            public List<Item_CaLamViec> dsca { get; set; }
        }

        private List<lichlamviec> _listLich;

        public List<lichlamviec> listLich
        {
            get { return _listLich; }
            set
            {
                _listLich = value;
                OnPropertyChanged();
            }
        }
        private void LoadShiftDetail()
        {
            month = int.Parse(DateTime.Parse(thang).ToString("MM"));
            year = int.Parse(DateTime.Parse(thang).ToString("yyyy"));
            start = (int)new DateTime(year, month, 1).DayOfWeek;
            listLich = new List<lichlamviec>();
            if (month - 1 > 0)
            {
                for (int i = 0; i < start; i++)
                {
                    var x = DateTime.DaysInMonth(year, month - 1);
                    listLich.Add(
                        new lichlamviec() { id = listLich.Count, ngay = x - i, ca = 0, status = 0 });
                }

                listLich.Reverse();
            }

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                List<Item_CaLamViec> dsc = new List<Item_CaLamViec>();
                var d = new lichlamviec()
                { id = listLich.Count, ngay = i, ca = dsc.Count, status = 1, dsca = dsc };
                listLich.Add(d);
            }

            int n = 42 - listLich.Count;
            for (int i = 1; i <= n; i++)
            {
                var d = new lichlamviec() { id = listLich.Count, ngay = i, ca = 0, status = 0 };
                listLich.Add(d);
            }

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                List<Item_CaLamViec> dsc = new List<Item_CaLamViec>();
                int x = (int)new DateTime(year, month, listLich[i + start - 1].ngay).DayOfWeek;
                if (DateTime.Parse(date).Day <= listLich[i + start - 1].ngay)
                {
                    if (select == 0)
                    {
                        if (x >= 1 && x < 6)
                        {
                            dsc = dsca;
                        }
                    }

                    if (select == 1)
                    {
                        if (x >= 1 && x < 7)
                        {
                            dsc = dsca;
                        }
                    }

                    if (select == 2)
                    {
                        dsc = dsca;
                    }
                }

                var d = new lichlamviec()
                { id = i + start - 1, ngay = i, ca = dsc.Count, status = 1, dsca = dsc };
                listLich[i + start - 1] = d;
            }

            listLich = listLich.ToList();
        }

        private void selectNgay(object sender, MouseButtonEventArgs e)
        {
            Border b = sender as Border;
            lichlamviec data = (lichlamviec)b.DataContext;
            if (data.status == 1)
            {
                foreach (var item in listLich)
                {
                    if (item.status == 2)
                        item.status = 1;
                    if (item.id == data.id)
                        item.status = 2;
                }
            }

            ChonCa.Visibility = Visibility.Visible;
            txtNgay.Text = data.ngay + "";
            txtThang.Text = DateTime.Now.ToString("MM");
            txtNam.Text = DateTime.Now.ToString("yyyy");
            if (data.dsca != null)
            {
                foreach (var item in listCa)
                {
                    item.ischecked = false;
                    foreach (var i in data.dsca)
                    {
                        if (item.shift_id == i.shift_id)
                        {
                            item.ischecked = true;
                        }    
                    }
                }
            }
        }

        private void ChonCaTrongLich(object sender, MouseButtonEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            if (cb != null)
            {
                if (cb.IsChecked == true)
                {
                    Item_CaLamViec data = (Item_CaLamViec)cb.DataContext;
                    foreach (var item in listLich)
                    {
                        if (item.status == 2)
                        {
                            item.ca--;
                            item.dsca.Remove(data);
                        }
                    }
                }
                else
                {
                    Item_CaLamViec data = (Item_CaLamViec)cb.DataContext;
                    foreach (var item in listLich)
                    {
                        if (item.status == 2)
                        {
                            item.ca++;
                            item.dsca.Add(data);
                        }
                    }
                }
            }
        }

        private async void LưuLich(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string cycle1 = "[";
                for (int i = 0; i < DateTime.DaysInMonth(year, month); i++)
                {
                    string a = "\"shift_id\":\"";
                    if (listLich[start + i].dsca.Count > 0)
                    {
                        for (int j = 0; j < listLich[start + i].dsca.Count; j++)
                        {
                            if (j < listLich[start + i].dsca.Count - 1)
                                a += listLich[start + i].dsca[j].shift_id + ",";
                            else
                                a += listLich[start + i].dsca[j].shift_id + "\"" + "";
                        }
                    }
                    else
                    {
                        a += "\"";
                    }

                    if (i < DateTime.DaysInMonth(year, month) - 1)
                        cycle1 += "{\"date\":\"" + year + "-" + month + "-" + (i + 1) + "\"," + a + "},";
                    else
                        cycle1 += "{\"date\":\"" + year + "-" + month + "-" + (i + 1) + "\"," + a + "}]";
                }

                bool allow = true;
                if (allow)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Add_Calendar_api);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(ten), "cy_name");
                    string monApply = DateTime.Parse(date).ToString("yyyy-MM") + "-01";
                    content.Add(new StringContent(monApply), "apply_month");
                    content.Add(new StringContent(cycle1), "cy_detail");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var resContent = await response.Content.ReadAsStringAsync();
                        Root_LuuLich resultCalendar = JsonConvert.DeserializeObject<Root_LuuLich>(resContent);
                       this.Visibility = Visibility.Collapsed;
                        string month = ucSetting.txbSelectMonth.Text.ToString().Split(' ')[1];
                        string year = ucSetting.txbSelectYear.Text.ToString().Split(' ')[1];
                        ucSetting.LoadCalendarWorkEnd(month, year);
                    }
                }
            }
            catch (Exception)
            {}   
        }
        
        #endregion
        private void bodBackCalendarWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucChuyenTiepChonCaLamViec(Main, ten, thang, select, date,null));
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void UIElement_OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
    }
}
