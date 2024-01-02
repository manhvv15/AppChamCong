using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
//using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec.QuanLyChung365TruocDangNhap.ChamCongNew.Entities.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CauHinhChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ChamCongKhuonMat;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec.ChamCong365.Entities.funcQuanLyCongTy;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucCauHinhChamCong.xaml
    /// </summary>
    public partial class ucCauHinhChamCong : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public double CornerRadius
        {
            get { return (double)GetValue(CornerRadiusProperty); }
            set { SetValue(CornerRadiusProperty, value); }
        }


        // Using a DependencyProperty as the backing store for CornerRadius.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CornerRadiusProperty =
            DependencyProperty.Register("CornerRadius", typeof(double), typeof(System.Windows.Controls.DatePicker));
        public class itemz
        {
            public int Day { get; set; }
            public bool LastMonth { get; set; } = false;
            public bool Today { get; set; } = false;
            public bool status { get; set; } = false;
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
        int month;
        int year;
        MainWindow Main;
        public ucCauHinhChamCong(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            _Years = new List<int>();
            var y = DateTime.Now.Year;
            for (int i = 1; i < 40; i++)
            {
                _Years.Add(y - i);
            }
            _Years.Reverse();
            for (int i = 0; i < 20; i++)
            {
                _Years.Add(y + i);
            }
            OnPropertyChanged("Years");
            _Months = new List<string>();
            for (int i = 1; i <= 12; i++)
            {
                _Months.Add("Tháng " + i.ToString());
            }
            OnPropertyChanged("Months");
            Main = main;
            cboMonth.ItemsSource = Months;
            cboMonth.Text = "Tháng " + DateTime.Now.Month.ToString();
            cboYear.ItemsSource = Years;
            cboYear.Text = DateTime.Now.Year.ToString();
            Part_TextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");

            year = Years[cboYear.SelectedIndex];
            month = cboMonth.SelectedIndex + 1;
            LoadDay(year, month);
            LoadCaMĐ();

        }

        private List<Item_CaLamViec> _caComon1;
        public List<Item_CaLamViec> caComon1
        {
            get { return _caComon1; }
            set { _caComon1 = value; OnPropertyChanged(); }
        }
        List<Item_CaLamViec> listAllCaLv = new List<Item_CaLamViec>();
        public async void LoadCaMĐ()
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
                    caComon1 = dsCa.data.items;
                    listAllCaLv = dsCa.data.items;
                    lsvCheckCaLamViec.ItemsSource = caComon1;
                    lsvDanhSachCaLamViec.ItemsSource = caComon1;

                }
            }
            catch (Exception)
            {
            }
        }
       // ListView listView = new ListView();


        #region CauHinhChamCong
        public List<Border> borCauHinh { get; set; }
        public List<TabItem> tabitemIps { get; set; }

        private DetailCompany _ComDetail;
        public DetailCompany ComDetail
        {
            get { return _ComDetail; }
            set { _ComDetail = value; OnPropertyChanged(); }
        }

        private async Task<API_Com_ChiTiet> getComDetail()
        {
            try
            {
                string apilink = APIs.API.ChiTiet_CongTy_Api;
                HttpClient httpClient = new HttpClient();
                Dictionary<string, string> form = new Dictionary<string, string>();

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                form.Add("com_id", Main.IdAcount.ToString());


                int i = 0;
                List<ChildCompany> list = new List<ChildCompany>();
                var respon = await httpClient.PostAsync(apilink, new FormUrlEncodedContent(form));
                var check = respon.Content.ReadAsStringAsync().Result;
                API_Com_ChiTiet api = JsonConvert.DeserializeObject<API_Com_ChiTiet>(respon.Content.ReadAsStringAsync().Result);
                if (api.data != null) return api;
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private async Task CauHinhChamCong(int k, int type = 1)
        {
            try
            {
                if (Main != null)
                    if (!string.IsNullOrEmpty(ComDetail.type_timekeeping))
                    {
                        List<string> x = new List<string>();
                        if (ComDetail.type_timekeeping.Contains(",")) x = ComDetail.type_timekeeping.Split(',').ToList();
                        else x.Add(ComDetail.type_timekeeping);
                        if (type == 1)
                        {
                            if (!x.Contains(k.ToString())) x.Add(k.ToString());
                        }
                        else if (type == 2)
                        {
                            if (x.Contains(k.ToString())) x.RemoveAll(m => m == k.ToString());
                        }
                        Dictionary<string, string> form = new Dictionary<string, string>();
                        if (x.Count > 0) form.Add("type_timekeeping", string.Join(",", x));
                        else form.Add("type_timekeeping", "\"\"");
                        HttpClient httpClient = new HttpClient();

                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);

                        var respon = await httpClient.PostAsync(APIs.API.CapNhat_CauHinh_Api, new FormUrlEncodedContent(form));
                        API_Respon add = JsonConvert.DeserializeObject<API_Respon>(respon.Content.ReadAsStringAsync().Result);
                        if (add.data != null && add.data.result)
                        {

                            MessageBox.Show("Cập nhật cấu hình chấm công thành công");
                            ComDetail.type_timekeeping = string.Join(",", x);

                        }
                        else
                        {

                            MessageBox.Show("Cập nhật cấu hình chấm công thất bại, vui lòng thử lại sau");

                        }
                    }

            }
            catch (Exception)
            {
            }
        }

        private async Task CauHinhChamCong1(int k, int type = 1)
        {
            try
            {
                if (Main != null)
                    if (!string.IsNullOrEmpty(ComDetail.id_way_timekeeping))
                    {
                        List<string> x = new List<string>();
                        if (ComDetail.id_way_timekeeping.Contains(",")) x = ComDetail.id_way_timekeeping.Split(',').ToList();
                        else x.Add(ComDetail.id_way_timekeeping);
                        if (type == 1)
                        {
                            if (!x.Contains(k.ToString())) x.Add(k.ToString());
                        }
                        else if (type == 2)
                        {
                            if (x.Contains(k.ToString())) x.RemoveAll(m => m == k.ToString());
                        }
                        Dictionary<string, string> form = new Dictionary<string, string>();
                        if (x.Count > 0) form.Add("lst_way_id", string.Join(",", x));
                        else form.Add("lst_way_id", "\"\"");
                        HttpClient httpClient = new HttpClient();

                        httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);

                        var respon = await httpClient.PostAsync(APIs.API.CauHinh_ChamCong_Api, new FormUrlEncodedContent(form));
                        API_Respon add = JsonConvert.DeserializeObject<API_Respon>(respon.Content.ReadAsStringAsync().Result);
                        if (add.data != null && add.data.result)
                        {

                            MessageBox.Show("Cập nhật cấu hình chấm công thành công");
                            ComDetail.id_way_timekeeping = string.Join(",", x);

                        }
                        else
                        {
                            MessageBox.Show("Cập nhật cấu hình chấm công thất bại, vui lòng thử lại sau");

                        }
                    }

            }
            catch (Exception)
            {
            }
        }
        #endregion 

        #region DateTime
        private double _DayItemWidth;

        public double DayItemWidth
        {
            get { return _DayItemWidth; }
        }

        private List<itemz> _Days;

        public List<itemz> Days
        {
            get { return _Days; }
        }
        private List<int> _Years;

        public List<int> Years
        {
            get { return _Years; }
        }
        private List<string> _Months;

        public List<string> Months
        {
            get { return _Months; }
        }


        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register("SelectedDate", typeof(DateTime?), typeof(System.Windows.Controls.DatePicker), new PropertyMetadata(null));

        private void cboYear_electionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void cboDay_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion

        #region Checked Chấm công
        private void ckAppTimViec_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ckChat_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ckChatQR(object sender, MouseButtonEventArgs e)
        {

        }

        private void ckWifi_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ckWifi_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ckViTri_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ckViTri_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ckNV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ckIPNV_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ckIPNV_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ckCTY_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ckIPCTY_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void ckIPCTY_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void ckQR_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void DownLoadPC365(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://chamcong.timviec365.com/");
        }



        #endregion

        #region Lịch
        private void grdTextBox(object sender, MouseButtonEventArgs e)
        {
            Part_TextBox.Focus();
        }
        private void Part_TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                textBoxFormat();
            }
        }
        private void lv_Loaded(object sender, RoutedEventArgs e)
        {
            _DayItemWidth = lvLoadDay.ActualWidth / 7 - .5;
            OnPropertyChanged("DayItemWidth");

            cboMonth.SelectedIndex = DateTime.Now.Month - 1;
            cboYear.SelectedIndex = Years.IndexOf(DateTime.Now.Year);
        }
        public DateTime? SelectedDate
        {
            get { return (DateTime?)GetValue(SelectedDateProperty); }
            set
            {
                SetValue(SelectedDateProperty, value);
                var z = (DateTime?)GetValue(SelectedDateProperty);
                if (z != null)
                {
                    Part_TextBox.Text = z.Value.ToString("dd/MM/yyyy");
                }
            }
        }
        private void LoadDay(int year, int month)
        {
            try
            {
                var start = (int)new DateTime(year, month, 1).DayOfWeek;
                _Days = new List<itemz>();
                if (month - 1 > 0)
                {
                    for (int i = 0; i < start; i++)
                    {
                        var x = DateTime.DaysInMonth(year, month - 1);
                        _Days.Add(new itemz() { Day = x - i, LastMonth = true, status = false });
                    }
                    _Days.Reverse();
                }
                else
                {
                    if (year - 1 > 0)
                    {
                        for (int i = 0; i < start; i++)
                        {
                            var x = DateTime.DaysInMonth(year - 1, month);
                            _Days.Add(new itemz() { Day = x - i, LastMonth = true, status = false });
                        }
                        _Days.Reverse();
                    }
                }
                for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
                {
                    var d = new itemz() { Day = i };

                    if (year == DateTime.Now.Year)
                    {
                        if (month == DateTime.Now.Month)
                        {
                            if (i == DateTime.Now.Day) d.Today = true;
                        }
                    }
                    _Days.Add(d);
                }

                int n = 42 - Days.Count;
                for (int i = 1; i <= n; i++)
                {
                    _Days.Add(new itemz() { Day = i, LastMonth = true, status = false });
                }

                lvLoadDay.ItemsSource = Days;
                lv.ItemsSource = Days;
                OnPropertyChanged("Days");
            }
            catch (Exception)
            {
            }
        }
        private void textBoxFormat()
        {
            var error = true;
            var txt = Part_TextBox.Text.Trim();
            if (txt.Length >= 7 && txt.Length <= 10)
            {
                DateTime dateValue;
                if (DateTime.TryParseExact(txt, "dd/MM/yyyy", new CultureInfo("en-US"), DateTimeStyles.None, out dateValue))
                {
                    SelectedDate = dateValue;
                    Part_TextBox.Text = SelectedDate.Value.ToString("dd/MM/yyyy");
                    error = false;
                }
                else if (txt.Length >= 7 && txt.Length <= 8)
                {
                    int day = 0; int month = 0; int year = 0;
                    if (txt.Length == 8)
                    {
                        var ck = int.TryParse(txt.Substring(0, 2), out day) && (int.TryParse(txt.Substring(2, 2), out month)) && int.TryParse(txt.Substring(4, 4), out year);
                        if (ck)
                        {
                            if (month > 0 && month <= 12)
                            {
                                int days = DateTime.DaysInMonth(year, month);
                                if (days >= day)
                                {
                                    var d = string.Format("{0}/{1}/{2}", day, month, year);
                                    SelectedDate = DateTime.Parse(d);
                                    Part_TextBox.Text = SelectedDate.Value.ToString("dd/MM/yyyy");
                                    error = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        var ck = int.TryParse(txt.Substring(0, 2), out day) && (int.TryParse(txt.Substring(2, 1), out month)) && int.TryParse(txt.Substring(3, 4), out year);
                        if (ck)
                        {
                            if (month > 0 && month <= 12)
                            {
                                int days = DateTime.DaysInMonth(year, month);
                                if (days >= day)
                                {
                                    var d = string.Format("{0}/{1}/{2}", day, month, year);
                                    SelectedDate = DateTime.Parse(d);
                                    Part_TextBox.Text = SelectedDate.Value.ToString("dd/MM/yyyy");
                                    error = false;
                                }

                            }
                        }
                    }

                }
                else if (txt.Length >= 9 && txt.Length <= 10)
                {
                    if (txt.Length == 10)
                    {
                        var checkint = 0;
                        var t = int.TryParse(txt[2].ToString(), out checkint) || int.TryParse(txt[5].ToString(), out checkint);
                        if (!t)
                        {
                            int day = 0; int month = 0; int year = 0;
                            var ck = int.TryParse(txt.Substring(0, 2), out day) && (int.TryParse(txt.Substring(3, 2), out month)) && int.TryParse(txt.Substring(5, 4), out year);
                            if (ck)
                            {
                                if (month > 0 && month <= 12)
                                {
                                    int days = DateTime.DaysInMonth(year, month);
                                    if (days >= day)
                                    {
                                        var d = string.Format("{0}/{1}/{2}", day, month, year);
                                        SelectedDate = DateTime.Parse(d);
                                        Part_TextBox.Text = SelectedDate.Value.ToString("dd/MM/yyyy");
                                        error = false;
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        var checkint = 0;
                        var t = int.TryParse(txt[2].ToString(), out checkint) || int.TryParse(txt[4].ToString(), out checkint);
                        if (!t)
                        {
                            int day = 0; int month = 0; int year = 0;
                            var ck = int.TryParse(txt.Substring(0, 2), out day) && (int.TryParse(txt.Substring(3, 1), out month)) && int.TryParse(txt.Substring(5, 4), out year);
                            if (ck)
                            {
                                if (month > 0 && month <= 12)
                                {
                                    int days = DateTime.DaysInMonth(year, month);
                                    if (days >= day)
                                    {
                                        var d = string.Format("{0}/{1}/{2}", day, month, year);
                                        SelectedDate = DateTime.Parse(d);
                                        Part_TextBox.Text = SelectedDate.Value.ToString("dd/MM/yyyy");
                                        error = false;
                                    }
                                }

                            }
                        }
                    }
                }
            }
            if (error)
            {
                Part_TextBox.Text = "";
                SelectedDate = null;
            }
            else
            {
                Part_TextBox.SelectionStart = Part_TextBox.Text.Length;

            }
        }
        private void cboMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var year = 0;
            var month = 0;

            if (cboYear.SelectedIndex >= 0) year = Years[cboYear.SelectedIndex];
            if (cboMonth.SelectedIndex >= 0) month = cboMonth.SelectedIndex + 1;

            if (year > 0 && month > 0) LoadDay(year, month);
        }
        private void Previous(object sender, MouseButtonEventArgs e)
        {

            if (cboMonth.SelectedIndex - 1 >= 0) cboMonth.SelectedIndex--;
            else if (cboYear.SelectedIndex - 1 >= 0)
            {
                cboMonth.SelectedIndex = Months.Count - 1;
                cboYear.SelectedIndex--;
            }
        }
        private void NextMonth(object sender, MouseButtonEventArgs e)
        {
            if (cboMonth.SelectedIndex + 1 < Months.Count) cboMonth.SelectedIndex++;
            else if (cboYear.SelectedIndex + 1 < Years.Count)
            {
                cboMonth.SelectedIndex = 0;
                cboYear.SelectedIndex++;
            }
        }
        private void lv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lv.SelectedIndex >= 0)
            {
                var item = lv.SelectedItem as itemz;
                if (!item.LastMonth)
                {
                    SelectedDate = new DateTime(Years[cboYear.SelectedIndex], cboMonth.SelectedIndex + 1, item.Day);
                    Part_TextBox.Text = SelectedDate.Value.ToString("dd/MM/yyyy");
                }
                else
                {
                    if (cboMonth.SelectedIndex - 1 >= 0)
                    {
                        SelectedDate = new DateTime(Years[cboYear.SelectedIndex], cboMonth.SelectedIndex, item.Day);
                        Part_TextBox.Text = SelectedDate.Value.ToString("dd/MM/yyyy");
                    }
                    else
                    {
                        if (cboYear.SelectedIndex - 1 < Years.Count)
                        {
                            if (cboYear.SelectedIndex - 1 >= 0)
                            {
                                SelectedDate = new DateTime(Years[cboYear.SelectedIndex - 1], Months.Count, item.Day);
                                Part_TextBox.Text = SelectedDate.Value.ToString("dd/MM/yyyy");
                            }
                        }
                    }
                }

                pop.IsOpen = false;
            }

        }
        private void pop_Opened(object sender, EventArgs e)
        {
            lv.SelectedIndex = -1;
        }
        private void Part_TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            textBoxFormat();
        }
        private void XoaNgay(object sender, MouseButtonEventArgs e)
        {
            Part_TextBox.Text = "";
        }

        private void NgayHomNay(object sender, MouseButtonEventArgs e)
        {
            Part_TextBox.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        #endregion

        private void ClickCheckChonCa(object sender, MouseButtonEventArgs e)
        {

        }

        private void TenCaLamViec_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        public bool shouldProcessEvent = true;
        private void bodChonCaLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (shouldProcessEvent)
            {
                if (borDsCa.Visibility == Visibility.Collapsed)
                {
                    borDsCa.Visibility = Visibility.Visible;
                    popup.Visibility = Visibility.Visible;

                }
                else
                {
                    borDsCa.Visibility = Visibility.Collapsed;
                    popup.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void lsvCaLamViec_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollListCa.ScrollToVerticalOffset(scrollListCa.VerticalOffset - e.Delta);
        }

        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borDsCa.Visibility == Visibility.Visible)
            {
                borDsCa.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;

            }
        }

        List<Item_CaLamViec> listCaLv = new List<Item_CaLamViec>();

        private void lsvCheckCaLamViec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvCheckCaLamViec.SelectedItem != null)
            {
                var chonca = ((Item_CaLamViec)(lsvCheckCaLamViec.SelectedItem)).shift_name.ToString();
                if (!listCaLv.Any(item => item.shift_name == chonca))
                {
                    Item_CaLamViec infor = new Item_CaLamViec()
                    {
                        shift_name = ((Item_CaLamViec)lsvCheckCaLamViec.SelectedItem).shift_name,
                        shift_id = ((Item_CaLamViec)lsvCheckCaLamViec.SelectedItem).shift_id,
                    };

                    listCaLv.Add(infor);
                    listCaLv = listCaLv.ToList();
                    lsvCaDuocChon.ItemsSource = listCaLv;
                    lsvCaDuocChon.Visibility = Visibility.Visible;
                    borDsCa.Visibility = Visibility.Collapsed;
                    popup.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void xoaCaDaChon_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Item_CaLamViec index = (Item_CaLamViec)lsvCaDuocChon.SelectedItem;
            if (index != null)
            {
                listCaLv.Remove(index);
                lsvCaDuocChon.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvCaDuocChon.ItemsSource = listCaLv;
                shouldProcessEvent = false;
                status = 1;
            }
            shouldProcessEvent = true;
            if (listCaLv.Count == 0)
            {
                lsvCaDuocChon.Visibility = Visibility.Collapsed;

            }
        }

        private void SelectedCaLamViecTrongDs(object sender, SelectionChangedEventArgs e)
        {
            if (lsvDanhSachCaLamViec.SelectedItem != null)
            {
                var chonca = ((Item_CaLamViec)lsvDanhSachCaLamViec.SelectedItem).shift_name.ToString();
                //  string selectedUserName = ((ListUsersDuyet)lsvNguoiXetDuyet.SelectedItem).userName;
                if (!listCaLv.Any(item => item.shift_name == chonca))
                {
                    Item_CaLamViec infor = new Item_CaLamViec()
                    {
                        shift_name = ((Item_CaLamViec)lsvDanhSachCaLamViec.SelectedItem).shift_name,
                        shift_id = ((Item_CaLamViec)lsvDanhSachCaLamViec.SelectedItem).shift_id,
                    };

                    listCaLv.Add(infor);
                    listCaLv = listCaLv.ToList();
                    status = 2;
                    lsvCaDuocChon.ItemsSource = listCaLv;
                    lsvCaDuocChon.Visibility = Visibility.Visible;
                    borDsCa.Visibility = Visibility.Collapsed;
                    popup.Visibility = Visibility.Collapsed;

                }
            }
        }

        private void ChonTatCa(object sender, RoutedEventArgs e)
        {
            listCaLv.Clear();
            foreach (var item in listAllCaLv)
            {
                listCaLv.Add(item);
            }

            lsvCaDuocChon.ItemsSource = listCaLv;
            lsvCaDuocChon.Visibility = Visibility.Visible;
           
        }

        private void HuyChonTatCa(object sender, RoutedEventArgs e)
        {
            listCaLv.Clear();
            lsvCaDuocChon.ItemsSource = listCaLv;
            lsvCaDuocChon.Visibility = Visibility.Collapsed;
        }
        private void SelectAllCheckBoxes(bool isChecked)
        {
            // Lặp qua tất cả các phần tử con của StackPanel
            foreach (UIElement element in stackPanel.Children)
            {
                if (element is CheckBox checkBox)
                {
                    // Đặt trạng thái Checked của CheckBox tương ứng
                    checkBox.IsChecked = isChecked;
                }
            }
        }

        private void ChonTatCaCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SelectAllCheckBoxes(true);
        }

        private void ChonTatCaCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SelectAllCheckBoxes(false);
        }
    }
}
