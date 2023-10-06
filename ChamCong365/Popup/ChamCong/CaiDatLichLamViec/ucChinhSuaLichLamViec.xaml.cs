using ChamCong365.Popup.DatePicker;
using ChamCong365.TimeKeeping;
using System.Windows;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Media;
using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.ComponentModel;
using System.Net.Http;
using ChamCong365.funcQuanLyCongTy;
using Newtonsoft.Json;
using ChamCong365.OOP;
using System.Collections.Generic;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec;
using ChamCong365.APIs;
using System.Runtime.CompilerServices;
using System.Net;
using System.Text;
using System.Linq;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec.ChamCong365.Entities.funcQuanLyCongTy;
using System.Security.Cryptography;
using System.Windows.Input;

namespace ChamCong365.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucCalendarWorkl.xaml
    /// </summary>
    public partial class ucChinhSuaLichLamViec : UserControl, INotifyPropertyChanged
    {
        MainWindow Main;
        public string name;
        public string date;
        public int IdCom;
        public int Cy_Id;
        public string month;
        public string year;
        public int month1;
        public int year1;
        public int start;
        ucCaiDatLichLamViec ucSetting;
        BrushConverter bcCalendar = new BrushConverter();

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
     
        public ucChinhSuaLichLamViec(MainWindow main, string cy_name, int com_id, int cy_id,ucCaiDatLichLamViec ucSetting, string date)
        {
            InitializeComponent();
            this.DataContext = this;
            this.name = cy_name;
            this.IdCom = com_id;
            this.Cy_Id = cy_id;
            this.date = date;
            this.ucSetting = ucSetting;
        
            Main = main;

            LoadShiftEdit();
            LoadShiftDetail();
          
        }

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
        public  void LoadShiftEdit()
        {
            ucSetting.LoadCaMĐ();
            listCa = ucSetting.caComon;
        }


        private List<DataCalendar> _listLLV;

        public List<DataCalendar> listLLV
        {
            get { return _listLLV; }
            set
            {
                _listLLV = value; OnPropertyChanged();

            }
        }
       
        List<CyDetail> cyDetails = new List<CyDetail>();
        DateTime aDateTime;
        int numDate;
        public async void LoadShiftDetail()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_All_Calendar_Work);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                month = ucSetting.txbSelectMonth.Text.Split(' ')[1];
                content.Add(new StringContent(month), "month");
                year = ucSetting.txbSelectYear.Text.Split(' ')[1];
                content.Add(new StringContent(year), "year");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                RootCalendar result = JsonConvert.DeserializeObject<RootCalendar>(responseContent);

                if (result.data.data != null)
                {
                    listLLV = result.data.data;
                    foreach (var item1 in listLLV)
                    {
                        if (item1.cy_id == Cy_Id)
                        {
                            cyDetails.AddRange(item1.cy_detail);
                        }
                    }
                    tbTitle.Text = date;
                    tbInput.Text = name;
                    month1 = int.Parse(month);
                    year1 = int.Parse(year);
                    start = (int)new DateTime(year1, month1, 1).DayOfWeek;
                    listLich = new List<lichlamviec>();
                    //Những ngày cuối của tháng trước trong tuần hiện tại
                    if (month1 - 1 > 0)
                    {
                        for (int i = 0; i < start; i++)
                        {
                            var x = DateTime.DaysInMonth(year1, month1 - 1);
                            listLich.Add(new lichlamviec() { id = listLich.Count, ngay = x - i, ca = 0, status = 0 });
                        }
                        listLich.Reverse();
                    }
                    //Ngày của tháng hiện tại
                    for (int i = 1; i <= DateTime.DaysInMonth(year1, month1); i++)
                    {
                        List<Item_CaLamViec> dsc = new List<Item_CaLamViec>();
                        string[] a;

                        if (1 <= DateTime.DaysInMonth(year1, month1) && DateTime.DaysInMonth(year1, month1) <= 31)
                        {
                            if (!string.IsNullOrEmpty(cyDetails[i - 1].shift_id))
                            {
                                a = cyDetails[i - 1].shift_id.Split(',');
                                foreach (var item in listCa)
                                {
                                    foreach (var x in a)
                                    {
                                        if (x == item.shift_id.ToString())
                                            dsc.Add(item);
                                    }
                                }
                            }
                        }
                        var d = new lichlamviec() { id = listLich.Count, ngay = i, ca = dsc.Count, status = 1, dsca = dsc };
                        listLich.Add(d);
                    }
                    //Những ngày đầu của tháng sau trong tuần
                    int n = 42 - listLich.Count;
                    for (int i = 1; i <= n; i++)
                    {
                        var d = new lichlamviec() { id = listLich.Count, ngay = i, ca = 0, status = 0 };
                        listLich.Add(d);
                    }
                    listLich = listLich.ToList();
                }
            }

            catch (Exception)
            {
            }
        }

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
     
        #region Lich Lam Viec

        private void UIElement_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Grid g = sender as Grid;
            if (g != null)
            {
                Border bordeIndex = g.Children[0] as Border;
                if (bordeIndex != null)
                {
                    TextBlock tb = bordeIndex.Child as TextBlock;
                    MessageBox.Show(tb.Text);
                }
            }
        }
        private void selectNgay(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Border b = sender as Border;
            lichlamviec data = (lichlamviec)b.DataContext;
            if (data.status == 1)
            {
                foreach (var item in listLich)
                {
                    if (item.status == 2)
                    {
                        item.status = 1;
                    }  
                    if (item.id == data.id)
                    {
                        item.status = 2;
                    }     
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
        private void abc(object sender, System.Windows.Input.MouseButtonEventArgs e)
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

        private async void LuuLich(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string cycle1 = "[";
                for (int i = 0; i < DateTime.DaysInMonth(year1, month1); i++)
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

                    if (i < DateTime.DaysInMonth(year1, month1) - 1)
                        cycle1 += "{\"date\":\"" + year1 + "-" + month1 + "-" + (i + 1) + "\"," + a + "},";
                    else
                        cycle1 += "{\"date\":\"" + year1 + "-" + month1 + "-" + (i + 1) + "\"," + a + "}]";
                }
            bool allow = true;
            validateName.Text = "";
            if (string.IsNullOrEmpty(tbInput.Text))
            {
                allow = false;
                validateName.Text = "Vui lòng nhập đầy đủ";
            }
            if (allow)
            {
                //LỖI
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Edit_Calendar_Api);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(tbInput.Text), "cy_name");
                    DateTime adate;
                    DateTime.TryParse(date, out adate);
                    string dateEdit = adate.ToString("yyyy/MM");
                    content.Add(new StringContent(dateEdit), "apply_month");
                    content.Add(new StringContent(cycle1), "cy_detail");
                    content.Add(new StringContent(Cy_Id.ToString()), "cy_id");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resConten = await response.Content.ReadAsStringAsync();
                        Root_SuaLich result = JsonConvert.DeserializeObject<Root_SuaLich>(resConten);
                        if (result != null)
                        {
                            ucSetting.LoadCalendarWorkStart(month, year);
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                catch (Exception)
                {} 
            }
        }
        private void Close_Click(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void UIElement_OnPreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
        #endregion
        private void Rectangle_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Hidden;

        }
    }

}
