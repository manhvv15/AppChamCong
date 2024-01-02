using AForge.Imaging.Filters;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.LichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using Newtonsoft.Json;
using System;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Input;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.ĐonDoiCa;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.CaLamViec;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for DeXuatLichLamViec.xaml
    /// </summary>
    public partial class DeXuatLichLamViec : UserControl, INotifyPropertyChanged
    {
        public class ListBodyJson
        {
            public string date { get; set; }
            public string shift_id { get; set; }
        }
        public class ChonCaLamViec
        {
            public string CaLamViec { get; set; }
        }
        List<ChonCaLamViec> listCaLamViec = new List<ChonCaLamViec>();

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
            Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
            ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");
            ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
            ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
            getDataCaLamViec();
            //listLichLam.Width = gridCol.Width;
            txtName.Text = Main.txbNameAccount.Text;
            //LoadCaLamViec();
            // ChuKiLichLamViec();
            getNgDuyet();
            getTheoD();
            getSettingComfirm();
        }
        int soNguoiDuyet = 1;
        private async Task getSettingPropose()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/settingPropose");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\"dexuat_id\": 18}", null, "application/json");
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    API_SettingPropose.Root api = JsonConvert.DeserializeObject<API_SettingPropose.Root>(responseContent);
                    if (api != null)
                    {
                        var item = api.settingPropose;
                        if (item.confirm_level.Value > 0)
                        {
                            soNguoiDuyet = item.confirm_level.Value;
                        }
                        type_id = item.confirm_type.Value;
                        if (item.confirm_type == 2)
                        {
                            Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();

                            ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
                            ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
                        }
                        else if (item.confirm_type == 1)
                        {
                            Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
                            ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");

                            ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
                        }
                        else if (item.confirm_type == 3)
                        {
                            Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
                            ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");
                            ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
                            ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
                        }



                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        List<API_SettingConfirm.ListPrivateType> listConfirm = new List<API_SettingConfirm.ListPrivateType>();
        private async void getSettingComfirm()
        {
            try
            {
                await getSettingPropose();
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/settingConfirm");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\"dexuat_id\": 18}", null, "application/json");
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    API_SettingConfirm.Data_Confirm api = JsonConvert.DeserializeObject<API_SettingConfirm.Data_Confirm>(responseContent);
                    if (api != null)
                    {
                        listConfirm = api.settingConfirm.listPrivateType;
                        foreach (var item in listConfirm)
                        {
                            if (item.dexuat_id == 18)
                            {

                                type_id = item.confirm_type;
                                if (item.confirm_type == 2)
                                {
                                    Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();

                                    ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
                                    ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
                                }
                                else if (item.confirm_type == 1)
                                {
                                    Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
                                    ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");

                                    ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
                                }
                                else if (item.confirm_type == 3)
                                {
                                    Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
                                    ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");
                                    ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
                                    ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
                                }
                                break;
                            }
                        }
                        foreach (var item in api.settingConfirm.listPrivateLevel)
                        {
                            if (item.dexuat_id == 18)
                            {
                                if (item.confirm_level > 0)
                                {

                                    soNguoiDuyet = item.confirm_level;
                                }

                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        int type_id;
        #region
        List<ListUsersDuyet> listUDuyets = new List<ListUsersDuyet>();
        List<ListUsersDuyet> listAddUDuyets = new List<ListUsersDuyet>();
        List<ListUsersTheoDoi> listUerTheoD = new List<ListUsersTheoDoi>();
        List<ListUsersTheoDoi> listAddUTheoD = new List<ListUsersTheoDoi>();
        public async void getNgDuyet()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/showadd");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {

                    // Xử lý phản hồi ở đây
                    XetDuyetTheoDoi api = JsonConvert.DeserializeObject<XetDuyetTheoDoi>(responseContent);
                    if (api.data.listUsersDuyet != null)
                    {
                        lsvNguoiXtDuyet.ItemsSource = api.data.listUsersDuyet;
                        listUDuyets = api.data.listUsersDuyet;
                        //listUsersTheoDoi = lsvNguoiTheoDoi;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi lấy danh sách ng duyệt " + ex.Message);
            }
        }
        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (borChonLoai.Visibility == Visibility.Collapsed)
            //{
            //    borChonLoai.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    borChonLoai.Visibility = Visibility.Collapsed;
            //}
        }

        private void grNgD1(object sender, MouseButtonEventArgs e)
        {
            if (borNgD.Visibility == Visibility.Collapsed)
            {
                borNgD.Visibility = Visibility.Visible;
            }
            else
            {
                borNgD.Visibility = Visibility.Collapsed;
            }
        }
        bool shouldProcessEvent = true;
        private void xoaAnh_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListUsersDuyet index = (ListUsersDuyet)listXetDuyt.SelectedItem;
            if (index != null)
            {
                listAddUDuyets.Remove(index);
                listXetDuyt.ClearValue(ItemsControl.ItemsSourceProperty);
                listXetDuyt.ItemsSource = listAddUDuyets;
                shouldProcessEvent = false;
            }
            shouldProcessEvent = true;
            if (listAddUDuyets.Count == 0)
            {
                borNgD.Visibility = Visibility.Visible;
                listXetDuyt.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }
        private void xoaAnh_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }
        Brush brush = new SolidColorBrush();
        private void xoaAnh_MouseLeave(object sender, MouseEventArgs e)
        {
            borNgD.Visibility = Visibility.Collapsed;
            textChonNgD.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listAddUDuyets.Count == 0)
            {
                borNgD.Visibility = Visibility.Collapsed;
                textChonNgD.Focus();
            }
            if (listAddUDuyets.Count == 0)
            {
                textDuye.Text = "Chọn người xét duyệt";
            }
        }
        private void lsvNguoiXtDctionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvNguoiXtDuyet.SelectedItem != null)
            {
                borNgD.Visibility = Visibility.Collapsed;
                string selectedUserName = ((ListUsersDuyet)lsvNguoiXtDuyet.SelectedItem).userName;
                if (!listAddUDuyets.Any(item => item.userName == selectedUserName))
                {
                    ListUsersDuyet infor = new ListUsersDuyet()
                    {
                        userName = ((ListUsersDuyet)lsvNguoiXtDuyet.SelectedItem).userName,
                        idQLC = ((ListUsersDuyet)lsvNguoiXtDuyet.SelectedItem).idQLC

                    };

                    listAddUDuyets.Add(infor);
                    listAddUDuyets = listAddUDuyets.ToList();
                    if (listAddUDuyets.Count > 0)
                    {
                        textDuye.Text = "";
                        //  grNgD.Height = 45;
                    }

                    listXetDuyt.ItemsSource = listAddUDuyets;
                    listXetDuyt.Visibility = Visibility.Visible;
                }

            }
            lsvNguoiXtDuyet.Items.Refresh();
            if (lsvNguoiXtDuyet.Items.Count > 0)
            {
                textChonNgD.Text = "";
                textChonNgD.IsReadOnly = false;
                textChonNgD.Focus();
            }
            borNgD.Visibility = Visibility.Collapsed;
        }

        private void textChonNgD_TextChanged(object sender, TextChangedEventArgs e)
        {
            borNgD.Visibility = Visibility.Visible;
            List<ListUsersDuyet> listUsersDuyetTimKiem = new List<ListUsersDuyet>();
            string searchText = textChonNgD.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listUDuyets)
            {
                if (str.userName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    listUsersDuyetTimKiem.Add(str);

                }
            }
            lsvNguoiXtDuyet.ItemsSource = listUsersDuyetTimKiem;

            if (textChonNgD.Text == "")
            {
                lsvNguoiXtDuyet.ItemsSource = listUDuyets;
            }
        }
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree

        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }
        private void ListView_Loaded(object sender, RoutedEventArgs e)
        {
            WrapPanel listWithPanel = FindChild<WrapPanel>(listXetDuyt, "listwith");
            if (listWithPanel != null)
            {
                //double newWidth = 300; // Thay đổi giá trị này theo nhu cầu của bạn
                listWithPanel.Width = listWithPanel.Width - gridText.Width;
            }
        }
        public static T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            if (parent == null)
                return null;

            T foundChild = null;
            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T && (child as FrameworkElement).Name == childName)
                {
                    foundChild = (T)child;
                    break;
                }
                else
                {
                    foundChild = FindChild<T>(child, childName);
                    if (foundChild != null)
                        break;
                }
            }
            return foundChild;
        }
        private async void getTheoD()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/showadd");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {

                    // Xử lý phản hồi ở đây
                    XetDuyetTheoDoi api = JsonConvert.DeserializeObject<XetDuyetTheoDoi>(responseContent);
                    if (api.data.listUsersTheoDoi != null)
                    {
                        lsvNguoiTheoDo.ItemsSource = api.data.listUsersTheoDoi;
                        listUerTheoD = api.data.listUsersTheoDoi;
                        //listUsersTheoDoi = lsvNguoiTheoDoi;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi lấy danh sách ng theo doi " + ex.Message);
            }

        }
        int idTheoD;
        private void borTenChonLoai_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListUsersTheoDoi d = (sender as Border).DataContext as ListUsersTheoDoi;
            if (d != null)
            {
                txtTheoD.Text = d.userName;
                idTheoD = d.idQLC;
                txtSearchTheoD.Visibility = Visibility.Visible;
                txtSearchTheoD.Text = "";
                txtSearchTheoD.Focus();
                borNgThD.Visibility = Visibility.Collapsed;
            }
        }
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNgThD.Visibility == Visibility.Collapsed)
            {
                borNgThD.Visibility = Visibility.Visible;
                txtSearchTheoD.Visibility = Visibility.Visible;
                txtTheoD.Text = "";
                //txtSearchTheoD.Text = "";
                //
                txtSearchTheoD.Focus();
            }
            else
            {
                borNgThD.Visibility = Visibility.Collapsed;
            }
        }
        private void txtSearchTheoD_TextChanged(object sender, TextChangedEventArgs e)
        {
            borNgThD.Visibility = Visibility.Visible;
            List<ListUsersTheoDoi> listUseTheoDoiTimKiem = new List<ListUsersTheoDoi>();
            string searchText = txtSearchTheoD.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listUerTheoD)
            {
                if (str.userName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    listUseTheoDoiTimKiem.Add(str);

                }
            }
            lsvNguoiTheoDo.ItemsSource = listUseTheoDoiTimKiem;

            if (txtSearchTheoD.Text == "")
            {
                lsvNguoiTheoDo.ItemsSource = listUerTheoD;
            }
        }
        #endregion
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
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
        List<Item_CaLamViec> listCa12 = new List<Item_CaLamViec>();
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
                    listCa12 = dsCa.data.items;
                    listShift = dsCa.data.items;
                    lsvCaLamViec.ItemsSource = dsCa.data.items; ;
                    lsvCa.ItemsSource = dsCa.data.items;
                    listCa = dsCa.data.items;
                    listCaMoiNgay = dsCa.data.items;
                    lsvCa.ItemsSource = listCaMoiNgay;
                    //lsvChonCa1.ItemsSource = dsCa.data.items;
                }
            }
            catch (Exception)
            {
            }

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

                DatePicker.SelectedDate = a;
                DatePicker.Text = a.ToString();
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
        private List<Item_CaLamViec> caMoiNgay = new List<Item_CaLamViec>();
        List<Item_CaLamViec> dsca1;

        private void ChuKiLichLamViec(string ten, string thang, int select, string date, List<Item_CaLamViec> ca)
        {
            thang = textThang.Text;
            select = ComboBoxSelectDayWork.SelectedIndex;
            date = DatePicker.SelectedDate + "";
            ca = dsca;


        }
        public List<Item_CaLamViec> listCa;
        public List<Item_CaLamViec> listCaMoiNgay;
        private void selectNgay(object sender, MouseButtonEventArgs e)
        {


        }
        public lichlamviec SelectedLichLamViec = new lichlamviec();
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

            ListView row = FindAncestor<ListView>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bodXoaNhanVien = FindChild<Border>(row, "bodXoaNhanVien");

                if (bodXoaNhanVien != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodXoaNhanVien.Visibility = Visibility.Collapsed;
                }
            }

            Border b = sender as Border;
            lichlamviec data = (lichlamviec)b.DataContext;
            SelectedLichLamViec = data;
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
                //listCaMoiNgay=listCa;
                foreach (var item in listCaMoiNgay)

                {
                    item.ischecked = false;
                    foreach (var i in SelectedLichLamViec.dsca)
                    {
                        if (item.shift_id == i.shift_id)
                            item.ischecked = true;
                    }
                }
            }
            //listCaMoiNgay = listCa;
            lsvCa.ItemsSource = listCaMoiNgay;
            lsvCa.Items.Refresh();

        }
        public class DataLichLamViec
        {
            public int select { get; set; }
            public string date { get; set; }
            public List<Item_CaLamViec> ca { get; set; }

        }
        List<DataLichLamViec> listDataLich = new List<DataLichLamViec>();
        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (textThang.Text == "-- / ----")
            {
                allow = false;
                validateMonth.Text = "Vui lòng chọn tháng ";
            }
            else
            {
                validateMonth.Visibility = Visibility.Collapsed;
            }
            if (dsca.Count == 0)
            {
                allow = false;
                validateCa.Text = "Vui lòng chọn ca làm việc ";
            }
            else
            {
                validateCa.Visibility = Visibility.Collapsed;
            }
            if (allow)
            {

                TextBlockThang.Text = textThang.Text;
                DataLichLamViec DtLichLamViec = new DataLichLamViec()
                {
                    select = ComboBoxSelectDayWork.SelectedIndex,
                    date = DatePicker.SelectedDate + "",
                    ca = dsca
                };
                //listDataLich.Add(DtLichLamViec);
                //listLichLam.ItemsSource = listDataLich;
                getData1();
            }

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
            public string ngayString { get; set; } = "";
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
            public List<Item_CaLamViec> caThayDoi { get; set; }
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
            select = ComboBoxSelectDayWork.SelectedIndex;
            for (int i = 1; i <= DateTime.DaysInMonth(year, month); i++)
            {
                string ngayString = "";
                List<Item_CaLamViec> dsc = new List<Item_CaLamViec>();
                int x = (int)new DateTime(year, month, listLich[i + start - 1].ngay).DayOfWeek;
                if (DateTime.Parse(date).Day <= listLich[i + start - 1].ngay)
                {

                    ngayString = year + "-" + month + "-" + listLich[i + start - 1].ngay;
                    if (select == 0)
                    {
                        if (x >= 1 && x < 6)
                        {
                            foreach (var item in dsca)
                            {
                                dsc.Add(item);
                            }

                        }
                    }

                    if (select == 1)
                    {
                        if (x >= 1 && x < 7)
                        {
                            foreach (var item in dsca)
                            {
                                dsc.Add(item);
                            }
                        }
                    }

                    if (select == 2)
                    {
                        foreach (var item in dsca)
                        {
                            dsc.Add(item);
                        }
                    }
                }

                var d = new lichlamviec()
                { id = i + start - 1, ngay = i, ca = dsc.Count, status = 1, dsca = dsc, ngayString = ngayString };
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

                    SelectedLichLamViec.ca--;
                    SelectedLichLamViec.dsca.Remove(data);


                }
                else
                {
                    Item_CaLamViec data = (Item_CaLamViec)cb.DataContext;

                    SelectedLichLamViec.ca++;
                    SelectedLichLamViec.dsca.Add(data);


                }
            }
        }

        private void lsvCa_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCaLV.ScrollToVerticalOffset(scrollCaLV.VerticalOffset - e.Delta);
        }

        private void DockPanel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private async void Border_MousteftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                // validateName.Text = validateLyDo.Text = "";
                if (string.IsNullOrEmpty(textNhapTenDeXuat.Text))
                {
                    allow = false;
                    validateName.Text = "Vui lòng nhập tên đề xuất";
                }
                else if (!string.IsNullOrEmpty(textNhapTenDeXuat.Text))
                {
                    validateName.Visibility = Visibility.Collapsed;
                }

                if (string.IsNullOrEmpty(textNhapLyDo.Text))
                {
                    allow = false;
                    validateLyDo.Text = "Vui lòng nhập lý do  ";
                }
                else if (!string.IsNullOrEmpty(textNhapLyDo.Text))
                {
                    validateLyDo.Visibility = Visibility.Collapsed;
                }
                if (listAddUDuyets.Count == 0)
                {
                    allow = false;
                    validateNgD.Text = "Vui lòng chọn người xét duyệt ";
                }
                else
                {
                    validateNgD.Visibility = Visibility.Collapsed;
                }
                if (txtTheoD.Text == "Chọn người theo dõi")
                {
                    allow = false;
                    validateNgTheoD.Text = "Vui lòng chọn người theo dõi";
                }
                else
                {
                    validateNgTheoD.Visibility = Visibility.Collapsed;
                }
                if (ComboBox.SelectedIndex == -1)
                {
                    allow = false;
                    validateKieuDuyet.Text = "Vui lòng chọn kiểu duyệt ";
                }
                else
                {
                    validateKieuDuyet.Visibility = Visibility.Collapsed;
                }
                if (listAddUDuyets.Count < soNguoiDuyet)
                {
                    allow = false;
                    CustomMessageBox.Show("Bạn chưa chọn đủ số người duyệt. Số người duyệt cần: " + soNguoiDuyet);
                }
                if (allow)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/De_Xuat_Lich_Lam_Viec");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(textNhapTenDeXuat.Text), "name_dx");
                    int id;
                    List<StringContent> NgXeDuyet = new List<StringContent>();
                    List<string> listString = new List<string>();
                    foreach (var item in listAddUDuyets)
                    {
                        id = item.idQLC;
                        string idString = Convert.ToString(id);

                        listString.Add(idString + ",");

                    }
                    for (int i = 0; i < listString.Count; i++)
                    {
                        if (listString[i].EndsWith(",") && i == listString.Count - 1)
                        {
                            listString[i] = listString[i].Substring(0, listString[i].Length - 1);
                        }
                        //listString = Convert.ToString(listString);
                    }
                    string idXetDuyet = string.Join("", listString);
                    //MessageBox.Show(Convert.ToString(idXetDuyet));
                    content.Add(new StringContent(Convert.ToString(idXetDuyet)), "id_user_duyet");
                    //idTheoDoi = ((ListUsersTheoDoi)lsvNguoiTheoDoi.SelectedItem).idQLC;
                    content.Add(new StringContent(Convert.ToString(idTheoD)), "id_user_theo_doi");
                    content.Add(new StringContent(textNhapLyDo.Text), "ly_do");
                    content.Add(new StringContent(ComboBox.SelectedValue.ToString()), "kieu_duyet");
                    if (ComboBoxSelectDayWork.SelectedIndex == 0)
                    {
                        llv = 1;
                    }
                    else if (ComboBoxSelectDayWork.SelectedIndex == 1)
                    {

                        llv = 2;
                    }
                    else if (ComboBoxSelectDayWork.SelectedIndex == 2)
                    {
                        llv = 3;
                    }
                    //MessageBox.Show(ComboBox.SelectedItem.ToString());
                    content.Add(new StringContent(llv.ToString()), "lich_lam_viec");
                    //DateTime ngayBatDau = (DateTime)DatePicker.SelectedDate;
                    string ngayBatDau = DatePicker.Text;
                    //DateTime ngayBatDauDate1 = 
                    DateTime ngayBatDauDate = (DateTime)DatePicker.SelectedDate;
                    string[] tenThang = { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12" };

                    // Lấy tên tháng từ mảng
                    string thang = tenThang[ngayBatDauDate.Month];
                    //string thang1 = tenThang[ngayBatDau.Month];
                    string ngayBatDauFormat = ngayBatDauDate.ToString("dd/MM/yyyy");
                    long epochTime = ConvertToEpochTime(ngayBatDauFormat);
                    content.Add(new StringContent(Convert.ToString(epochTime)), "thang_ap_dung");
                    content.Add(new StringContent(Convert.ToString(epochTime)), "ngay_bat_dau");
                    //content.Add(new StringContent("1693562128"), "thang_ap_dung");
                    // content.Add(new StringContent("1693562128"), "ngay_bat_dau");
                    content.Add(new StringContent("2"), "ca_lam_viec");
                    List<ListBodyJson> listBodyJsons = new List<ListBodyJson>();
                    foreach (var item in listLich)
                    {
                        if (item.ngayString != null && item.ngayString != "")
                        {
                            string listShiftString = "";
                            listShiftString = string.Join(",", item.dsca.Select(x => x.shift_id));
                            listBodyJsons.Add(new ListBodyJson() { date = item.ngayString, shift_id = listShiftString });
                        }
                    }

                    var listLichObject = new
                    {
                        type = 1,
                        data = listBodyJsons
                    };
                    string json = JsonConvert.SerializeObject(listLichObject);

                    content.Add(new StringContent("[" + json + "]"), "ngay_lam_viec");
                    if (tepDinhKem.Text != "Thêm tài liệu đính kèm") content.Add(new StreamContent(File.OpenRead(TenTep)), "fileKem", tepDinhKem.Text);

                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var responsContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        TaoDeXuatThanhCong uc = new TaoDeXuatThanhCong(Main);
                        //Main.dopBody.Children.Clear();
                        object Content = uc.Content;
                        uc.Content = null;
                        Main.grShowPopup.Children.Add(Content as UIElement);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void lsvChonThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lsvChonThang.Visibility = Visibility.Collapsed;
        }
        int llv = 0;
        private void borTenChonLoai_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void lsvCaLamViec_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (e.OriginalSource is Border )
            //{
            //    // Nếu là CheckBox, không chặn sự kiện
            //    return;
            //}

            // Ngăn chặn sự kiện tiếp theo (nếu có)
            //  e.Handled = true;

        }
        private void CheckBox_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            // Không chặn sự kiện cho CheckBox
            //  e.Handled = false;
        }
        private void ChonNhanvien(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Item_CaLamViec data = (Item_CaLamViec)cb.DataContext;
            dsca.Add(data);
            caMoiNgay.Add(data);

        }

        private void HuyChon(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Item_CaLamViec data = (Item_CaLamViec)cb.DataContext;
            dsca.Remove(data);
            caMoiNgay.Remove(data);
            //lsvCa.ItemsSource = dsca;
            //lsvCa.Items.Refresh();
        }
        string TenTep = "";
        private void Border_MouseLaeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tất cả các tệp|*.*"; // Lọc tất cả các tệp

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                try
                {
                    // Đọc nội dung của tệp bằng File.ReadAllText
                    string fileContent = File.ReadAllText(filePath);
                    //  tepDinhKem.Text = filePath;
                    TenTep = filePath;
                    tepDinhKem.Text = System.IO.Path.GetFileName(filePath); ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi đọc tệp: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }
    }

}
