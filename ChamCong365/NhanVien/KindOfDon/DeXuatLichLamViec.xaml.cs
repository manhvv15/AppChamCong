using AForge.Imaging.Filters;
using ChamCong365.APIs;
using ChamCong365.OOP;
using ChamCong365.OOP.NhanVien.DonDeXuat;
using ChamCong365.TimeKeeping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ChamCong365.NhanVien.KindOfDon.ĐonDoiCa;
using static ChamCong365.OOP.NhanVien.DonDeXuat.CaLamViec;

namespace ChamCong365.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for DeXuatLichLamViec.xaml
    /// </summary>
    public partial class DeXuatLichLamViec : UserControl, INotifyPropertyChanged
    {
        public class ChonCaLamViec
        {
            public string CaLamViec { get; set; }
        }
        List<ChonCaLamViec> listCaLamViec = new List<ChonCaLamViec>();
        private void LoadCaLamViec()
        {

            listCaLamViec.Add(new ChonCaLamViec { CaLamViec = "Làm việc theo ca" });
            listCaLamViec.Add(new ChonCaLamViec { CaLamViec = "Làm việc theo giờ" });

            lsvChonCaLamViec.ItemsSource = listCaLamViec;
        }
        public class ChonLichLamViec
        {
            public string LichLamViec { get; set; }
        }
        List<ChonLichLamViec> listLichLamViec = new List<ChonLichLamViec>();

        public class ChonCaLamViec1
        {
            public string CaLamViec1 { get; set; }
        }
        List<ChonCaLamViec1> listCaLamViec1 = new List<ChonCaLamViec1>();

        MainChamCong Main;
        public DeXuatLichLamViec(MainChamCong main)
        {
            this.DataContext = this;
            InitializeComponent();
            dteSelectedMonth = new Calendar();
            dteSelectedMonth.Visibility = Visibility.Collapsed;
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            dteSelectedMonth.MouseLeftButtonDown += Select_thang;
            dteSelectedMonth.DisplayModeChanged += dteSelectedMonth_DisplayModeChanged;
            cl = new List<Calendar>();
            cl.Add(dteSelectedMonth);
            cl = cl.ToList();
            Main = main;
            getDataCaLamViec();
           
           
            LoadCaLamViec();
            // ChuKiLichLamViec();


        }
        private async void getDataCaLamViec()
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
                    //caComon = dsCa.data.items;
                    lsvCaLamViec.ItemsSource = dsCa.data.items;
                    lsvCa.ItemsSource = dsCa.data.items;
                    listCa = dsCa.data.items;
                    //lsvChonCa1.ItemsSource = dsCa.data.items;
                }
            }
            catch (Exception)
            {
            }

        }
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
                DatePicker.SelectedDate = a;
            }
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonth.DisplayDate != null && flag > 0)
            {
                dteSelectedMonth.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }

        private void Select_thang(object sender, MouseButtonEventArgs e)
        {
            dteSelectedMonth.Visibility = dteSelectedMonth.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            lsvChonThang.ItemsSource = cl;
            flag = 1;
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borCaLamViec.Visibility == Visibility.Collapsed)
            {

                borCaLamViec.Visibility = Visibility.Visible;

            }
            else
            {
                borCaLamViec.Visibility = Visibility.Collapsed;

            }

        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChonCaLamViec d = (sender as Border).DataContext as ChonCaLamViec;
            if (d != null)
            {
                textCaLamViec.Text = d.CaLamViec;

            }
        }
        private void scroll_PreviewMouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);
        }




        private void Grid_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            if (borChonCaLamViec.Visibility == Visibility.Collapsed)
            {

                borChonCaLamViec.Visibility = Visibility.Visible;

            }
            else
            {
                borChonCaLamViec.Visibility = Visibility.Collapsed;

            }
        }



        private void lsvChonCaLamViec1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCaLamViec1.ScrollToVerticalOffset(scrollCaLamViec1.VerticalOffset - e.Delta);
        }

        private void Grid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);

            Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);
        }


        int flag = 0;


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
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        private List<Item_CaLamViec> dsca = new List<Item_CaLamViec>();
        List<Item_CaLamViec> dsca1;
        private void ChonNhanvien(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Item_CaLamViec data = (Item_CaLamViec)cb.DataContext;
            dsca.Add(data);
        }

        private void HuyChon(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Item_CaLamViec data = (Item_CaLamViec)cb.DataContext;
            dsca.Remove(data);
        }
        private void ChuKiLichLamViec(string ten, string thang, int select, string date, List<Item_CaLamViec> ca)
        {
            thang = textThang.Text;
            select = ComboBox.SelectedIndex;
            date = DatePicker.SelectedDate + "";
            ca = dsca;


        }
        public List<Item_CaLamViec> listCa;
        private void selectNgay(object sender, MouseButtonEventArgs e)
        {

            
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
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

            chonCa.Visibility = Visibility.Visible;
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
                            item.ischecked = true;
                    }
                }
            }
        }
        public class DataLichLamViec
        {
            public int select { get; set; }
            public string date { get; set; }
            public List<Item_CaLamViec> ca { get; set; }

        }
        List<DataLichLamViec> listDataLich = new List<DataLichLamViec>() ;
        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            TextBlockThang.Text = textThang.Text;
            DataLichLamViec DtLichLamViec = new DataLichLamViec()
            {
                select = ComboBox.SelectedIndex,
                date = DatePicker.SelectedDate + "",
                ca = dsca
            };
            //listDataLich.Add(DtLichLamViec);
            //listLichLam.ItemsSource = listDataLich;
            getData1();
            
        }
        string ten;
        string thang;
        int select;
        string date;
        int month;
        int year;
        int start;
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
         private void getData()
        {
            

           
        }   
        private void getData1()
        {
            string input = textThang.Text;
            string[] parts = input.Split('/');
            string month1 = parts[0].Trim();
            string year1 = parts[1].Trim();
            //  MessageBox.Show(month1 + year1);
            //month = Conver.Int;
            //month = int.Parse((DateTime.Parse(month1)).ToString("MM"));
            //year = int.Parse((DateTime.Parse(year1)).ToString("yyyy"));

            month = int.Parse(month1);
            year = int.Parse(year1);

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

            date = DatePicker.SelectedDate + "";
            select = ComboBox.SelectedIndex;
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
            listLichLam.ItemsSource = listLich;
        }

        private void abc(object sender, MouseButtonEventArgs e)
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

        private void lsvCa_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCaLV.ScrollToVerticalOffset(scrollCaLV.VerticalOffset - e.Delta);
        }
    }

}
