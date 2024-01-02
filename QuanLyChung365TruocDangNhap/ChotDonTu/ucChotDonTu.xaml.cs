using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCong365.Entities.Company;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using QuanLyChung365TruocDangNhap.ChotDonTu.OOP;
using QuanLyChung365TruocDangNhap.ChotDonTu.Popup;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
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

namespace QuanLyChung365TruocDangNhap.ChotDonTu
{
    /// <summary>
    /// Interaction logic for ucChotDonTu.xaml
    /// </summary>
    public partial class ucChotDonTu : UserControl, INotifyPropertyChanged
    {
        frmMain Main;
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
        public class lichlamviec : INotifyPropertyChanged
        {
            public event PropertyChangedEventHandler PropertyChanged;

            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

            public int id;
            public int ngay { get; set; }
            public int ngayChot { get; set; }
            public string ngayString { get; set; } = "";


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

        }
        public ucChotDonTu(frmMain main)
        {
            this.DataContext = this;
            InitializeComponent();
            Main = main;
            dteSelectedMonth = new Calendar();
            dteSelectedMonth.Visibility = Visibility.Collapsed;
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            dteSelectedMonth.MouseLeftButtonDown += Select_thang;
            dteSelectedMonth.DisplayModeChanged += dteSelectedMonth_DisplayModeChanged;
            cl = new List<Calendar>();
            cl.Add(dteSelectedMonth);
            cl = cl.ToList();
            textThang.Text = DateTime.Now.ToString("MM/yyyy");

            string input = textThang.Text;
            string[] parts = input.Split('/');
            string month1 = parts[0].Trim();
            string year1 = parts[1].Trim();
            month = int.Parse(month1);
            year = int.Parse(year1);
            getList();

        }
        public List<API_ChotDon.ChotDon> listChot = new List<API_ChotDon.ChotDon>();
        public async void getList()
        {
            try
            {
                string input = textThang.Text;
                string[] parts = input.Split('/');
                string month1 = parts[0].Trim();
                string year1 = parts[1].Trim();

                month = int.Parse(month1);
                year = int.Parse(year1);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/chotDonTu/list");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(year.ToString()), "nam_ap_dung");
                content.Add(new StringContent(month.ToString()), "thang_ap_dung");
                // var content = new StringContent("{\"nam_ap_dung\":\""+ year.ToString() + "\",\"thang_ap_dung\":\""+ month.ToString() + "\"}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_ChotDon.Ds_ChotDon api = JsonConvert.DeserializeObject<API_ChotDon.Ds_ChotDon>(responseContent);
                    if (api != null)
                    {
                        listChot = api.data.data;
                        getData1();
                    }

                }
            }
            catch (Exception e) { }
        }
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

                //DatePicker.SelectedDate = a;
                //DatePicker.Text = a.ToString();
            }
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonth.DisplayDate != null && flag > 0)
            {
                dteSelectedMonth.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }
        int flag = 0;
        private void Select_thang(object sender, MouseButtonEventArgs e)
        {
            if (lsvChonThang.Visibility == Visibility.Collapsed)
            {
                lsvChonThang.Visibility = Visibility.Visible;
            }
            else
            {
                lsvChonThang.Visibility = Visibility.Collapsed;
            }
            if (lsvChonThang.Visibility == Visibility.Visible)
            {
                dteSelectedMonth.Visibility = dteSelectedMonth.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                lsvChonThang.ItemsSource = cl;
                flag = 1;
            }

        }

        private void lsvChonThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public int month;
        public int year;
        int start;
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
        private void getData1()
        {
            string input = textThang.Text;
            string[] parts = input.Split('/');
            string month1 = parts[0].Trim();
            string year1 = parts[1].Trim();

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
                        new lichlamviec() { id = listLich.Count, ngay = x - i, status = 0 });
                }

                listLich.Reverse();
            }

            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                var d = new lichlamviec()
                { id = listLich.Count, ngay = i, status = 1 };
                listLich.Add(d);
            }

            int n = 42 - listLich.Count;
            for (int i = 1; i <= n; i++)
            {
                var d = new lichlamviec() { id = listLich.Count, ngay = i, status = 0 };
                listLich.Add(d);
            }
            DateTime date_chot = DateTime.Now;
            DateTime date_auto_chot = DateTime.Now;
            if (listChot.Count() > 0)
            {
                foreach (var item in listChot)
                {
                    date_chot = (DateTime)item.date_chot;
                    date_auto_chot = (DateTime)item.date_auto_chot;
                }
            }
            int dayChot = date_chot.Day;
            int dayAutoChot = date_auto_chot.Day;
            if (listChot.Count() > 0)
            {
                foreach (var item in listLich)
                {
                    if (item.ngay == dayChot && item.status == 1)
                    {
                        item.status = 3;
                        break;
                    }

                }
                foreach (var item in listLich)
                {

                    if (item.ngay == dayAutoChot && item.status == 1)
                    {
                        item.status = 2;
                        break;
                    }
                }
                for (int i = dayChot + 1; i < dayAutoChot; i++)
                {
                    foreach (var item in listLich)
                    {

                        if (item.ngay == i && item.status == 1)
                        {
                            item.status = 4;
                            break;
                        }
                    }
                }
            }
            listLich = listLich.ToList();
            listLichLam.ItemsSource = listLich;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void timkiem(object sender, MouseButtonEventArgs e)
        {
            getList();
        }
        public int date_chot;
        public string dateChot;
        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            lichlamviec l = (sender as Border).DataContext as lichlamviec;
            if (l != null)
            {
                if (l.status == 1)
                {
                    date_chot = l.ngay;
                    dateChot = year + "-" + month + "-" + date_chot;
                    ucChonDon uc = new ucChonDon(Main, this);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.pnlShowPopUp.Children.Add(Content as UIElement);
                    statusLich = l.status;
                    ngayUpdate = l.ngay;
                }
            }


        }
        public int ngayUpdate;
        public int idLich;
        public int statusLich  = 0;
        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            foreach (var item in listLich)
            {

                if (item.status == 3)
                {
                    var border = sender as Border;
                    Update.PlacementTarget = border;
                    Update.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                    Update.VerticalOffset = -(border.ActualHeight + Update.Child.DesiredSize.Height) / 2;
                    Update.IsOpen = true;
                    ngayUpdate = item.ngay;
                    statusLich = item.status;
                }

            }
            foreach (var item in listChot)
            {
                idLich = (int)item.id;
            }

        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            Update.IsOpen = false;
        }

        private void Update_MouseEnter(object sender, MouseEventArgs e)
        {
            Update.IsOpen = true;
        }

        private void DockPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Update.IsOpen = false;
            ucChonDon uc = new ucChonDon(Main, this);
            object Content = uc.Content;
            uc.Content = null;
            Main.pnlShowPopUp.Children.Add(Content as UIElement);
        }
        public int id_xoa;
        private void DockPanel_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            foreach (var item in listChot)
            {
                id_xoa = (int)item.id;
            }
            Update.IsOpen = false;
            Xoa uc = new Xoa(Main, this, id_xoa);
            object Content = uc.Content;
            uc.Content = null;
            Main.pnlShowPopUp.Children.Add(Content as UIElement);
        }

        private void listLichLam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset - e.Delta);
        }
    }
}
