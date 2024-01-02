using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.LichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for DeXuatHoaHongDoanhThu.xaml
    /// </summary>
    public partial class DeXuatHoaHongDoanhThu : UserControl, INotifyPropertyChanged
    {
        MainChamCong Main;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        int idMucDoanhThu { get; set; }
        public DeXuatHoaHongDoanhThu(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
            ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");
            ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
            ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
            this.DataContext = Main;
            dteSelectedMonth = new System.Windows.Controls.Calendar();
            dteSelectedMonth.Visibility = Visibility.Collapsed;
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            dteSelectedMonth.MouseLeftButtonDown += Select_thang;
            dteSelectedMonth.DisplayModeChanged += dteSelectedMonth_DisplayModeChanged;
            cl = new List<System.Windows.Controls.Calendar>();
            cl.Add(dteSelectedMonth);
            cl = cl.ToList();
            getTheoD();
            getNgDuyet();
            txtName.Text = main.txbNameAccount.Text;
            string currentDate = DateTime.Now.ToString("dd/MM/yyyy");
            NgayBatDau.Text = currentDate;
            DateTime parsedDate;
            if (DateTime.TryParseExact(currentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedDate))
            {
                NgayBatDau.SelectedDate = parsedDate;
            }
            getMucDoanhThu();
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
                var content = new StringContent("{\"dexuat_id\": 20}", null, "application/json");
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
                var content = new StringContent("{\"dexuat_id\": 20}", null, "application/json");
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
                            if (item.dexuat_id == 20)
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
                            if (item.dexuat_id == 20)
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
                    QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.XetDuyetTheoDoi api = JsonConvert.DeserializeObject<QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.XetDuyetTheoDoi>(responseContent);
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
                    QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.XetDuyetTheoDoi api = JsonConvert.DeserializeObject<QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.XetDuyetTheoDoi>(responseContent);
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
        #region
        private string RemoveSpacesAndLetters(string text)
        {
            // Sử dụng LINQ để lọc ra các ký tự là số
            string result = new string(text.Where(char.IsDigit).ToArray());

            return result;
        }


        private void AllowOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            // Lấy văn bản hiện tại trong vùng nhập liệu
            var textBox = (TextBox)sender;
            string currentText = textBox.Text;

            // Lưu lại vị trí của con trỏ (caret) trước khi xóa
            int caretIndex = textBox.CaretIndex;

            // Kiểm tra xem vị trí con trỏ có nằm trong biên của chuỗi không
            if (caretIndex >= 0 && caretIndex < currentText.Length)
            {
                // Xóa ký tự tại vị trí con trỏ
                currentText = currentText.Remove(caretIndex, 1);
            }

            // Kiểm tra xem ký tự mới nhập có phải là số hay không
            if (!IsNumber(e.Text) || e.Text == " ")
            {
                // Nếu không phải là số, không thêm vào vị trí con trỏ và kết thúc xử lý
                e.Handled = true;
            }
            //else
            //{
            //    // Nếu là số, thêm số mới vào vị trí con trỏ
            //    currentText = currentText.Insert(caretIndex, e.Text);
            //}

            // Lọc khoảng trắng và chữ cái khỏi văn bản
            string filteredText = RemoveSpacesAndLetters(currentText);

            // Gán văn bản đã lọc vào vùng nhập liệu
            textBox.Text = filteredText;

            // Đặt lại vị trí con trỏ sau khi đã thêm vào và lọc
            textBox.CaretIndex = caretIndex + (filteredText.Length - currentText.Length);
        }
        private bool IsNumber(string text)
        {
            return int.TryParse(text, out _);
        }
        #endregion
        private async void getMucDoanhThu()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/showMucDoanhThu");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("", null, "text/plain");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DoanhThu.DoanhThu api = JsonConvert.DeserializeObject<API_DoanhThu.DoanhThu>(responseContent);
                    if (api != null)
                    {
                        lsvDoanhThu.ItemsSource = api.data.danhthuList;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void Border_MouseLeftButtonUp11(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
        public static long ConvertToEpochTime(string dateString)
        {
            // Define the format of the input date string
            string format = "dd/MM/yyyy";

            // Parse the input date string using the specified format
            DateTime date = DateTime.ParseExact(dateString, format, CultureInfo.InvariantCulture);

            // Calculate the number of seconds since Unix epoch (January 1, 1970)
            TimeSpan timeSpan = date - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return (long)timeSpan.TotalSeconds;
        }
        string ngayBatDau;
        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                // validateName.Text = validateLyDo.Text = "";
                if (string.IsNullOrEmpty(textNhapTenDeXuat.Text))
                {
                    allow = false;
                    validateName.Text = "Vui lòng nhập tên đơn đề xuất ";
                }
                else if (!string.IsNullOrEmpty(textNhapTenDeXuat.Text))
                {
                    validateName.Visibility = Visibility.Collapsed;
                }

                if (string.IsNullOrEmpty(textNhapLiDo.Text))
                {
                    allow = false;
                    validateLyDo.Text = "Vui lòng nhập lý do xin đề xuất ";
                }
                else if (!string.IsNullOrEmpty(textNhapLiDo.Text))
                {
                    validateLyDo.Visibility = Visibility.Collapsed;
                }
                if (string.IsNullOrEmpty(textNhapSoTien.Text))
                {
                    allow = false;
                    validateMoney.Text = "Vui lòng nhập số tiền ";
                }
                else if (!string.IsNullOrEmpty(textNhapSoTien.Text))
                {
                    validateMoney.Visibility = Visibility.Collapsed;
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
                if (textThang.Text == "-- / ----")
                {
                    allow = false;
                    validateThang.Text = "Vui lòng chọn tháng";
                }
                else
                {
                    validateThang.Visibility = Visibility.Collapsed;
                }
                if (txtDT.Text == "Chọn mức doanh thu")
                {
                    allow = false;
                    validateDoanhThu.Text = "Vui lòng chọn mức doanh thu";
                }

                else
                {
                    validateDoanhThu.Visibility = Visibility.Collapsed;
                }
                if (NgayBatDau.Text.Contains("-"))
                {
                    ngayBatDau = NgayBatDau.Text;
                    ngayBatDau = ngayBatDau.Replace("-", "/");
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

                    validateThang1.Visibility = Visibility.Collapsed;
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/addDXHH");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(textNhapTenDeXuat.Text), "name_dx");
                    content.Add(new StringContent(ComboBox.SelectedValue.ToString()), "kieu_duyet");
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
                    content.Add(new StringContent(textNhapLiDo.Text), "ly_do");

                    //content.Add(new StringContent("2023-06-14T03:19:09.426Z"), "time_hh");
                    content.Add(new StringContent(textThang.Text), "chu_ky");
                    // content.Add(new StringContent("2023-12"), "chu_ky");
                    //   content.Add(new StringContent("2023-12-13"), "item_mdt_date");
                    //if (x.Contains("-"))
                    //{
                    //    // Thay thế kí tự '-' bằng '/'
                    //    x = x.Replace("-", "/");
                    //}
                    ngayBatDau = NgayBatDau.SelectedDate.Value.ToString("yyyy-MM-dd");
                    //     DateTime ngayBatDauDate = DateTime.Parse(ngayBatDau);
                    //    string ngayBatDauFormat = ngayBatDauDate.ToString("dd/MM/yyyy");
                    //long epochTime = ConvertToEpochTime(ngayBatDau) * 1000;
                    content.Add(new StringContent(ngayBatDau), "item_mdt_date");
                    content.Add(new StringContent(textNhapSoTien.Text), "dt_money");

                    // content.Add(new StringContent(""), "item_mdt_date");
                    // content.Add(new StreamContent(File.OpenRead("/C:/Users/FPTSHOP/Desktop/anh bai tap/áo 1.jfif")), "file_kem", "/C:/Users/FPTSHOP/Desktop/anh bai tap/áo 1.jfif");
                    content.Add(new StringContent(idMucDoanhThu.ToString()), "name_dt");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var responseContent = await response.Content.ReadAsStringAsync();
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
                // MessageBox.Show(ex.Message);
            }
        }
        private void dteSelectedMonth_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            lsvChonThang.Visibility = Visibility.Collapsed;
            var x = dteSelectedMonth.DisplayDate.ToString("yyyy-MM");
            if (flag == 0)
                x = "";
            else
                x = dteSelectedMonth.DisplayDate.ToString("yyyy-MM");
            if (textThang != null && !string.IsNullOrEmpty(x))
            {
                textThang.Text = x;
                DateTime a = DateTime.Parse(x);
                //dpEnd.SelectedDate = a;
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

            if (lsvChonThang.Visibility == Visibility.Visible)
            {
                //dteSelectedMonth.Visibility = dteSelectedMonth.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                if (dteSelectedMonth.Visibility == Visibility.Collapsed)
                {
                    dteSelectedMonth.Visibility = Visibility.Visible;
                }
                else
                {
                    dteSelectedMonth.Visibility = Visibility.Collapsed;
                }
                lsvChonThang.ItemsSource = cl;
                flag = 1;
            }

        }
        int flag = 0;
        System.Windows.Controls.Calendar dteSelectedMonth { get; set; }

        private List<System.Windows.Controls.Calendar> _cl;

        public List<System.Windows.Controls.Calendar> cl
        {
            get { return _cl; }
            set
            {
                _cl = value; OnPropertyChanged();
            }
        }
        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void lsvChonThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lsvChonThang.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            if (gridDoanhThu.Visibility == Visibility.Collapsed)
            {
                gridDoanhThu.Visibility = Visibility.Visible;
            }
            else
            {
                gridDoanhThu.Visibility = Visibility.Collapsed;
            }
        }

        private void Rectangle_MoudseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            gridDoanhThu.Visibility = Visibility.Collapsed;
        }

        private void borTenChonLoai_MouseLeftuttonDownaa(object sender, MouseButtonEventArgs e)
        {
            API_DoanhThu.DanhthuList d = (sender as Border).DataContext as API_DoanhThu.DanhthuList;
            if (d != null)
            {
                txtDT.Text = d.tl_name;
                idMucDoanhThu = int.Parse(d.tl_id.ToString());
                gridDoanhThu.Visibility = Visibility.Collapsed;
            }
        }

        private void textNhapSoTien_TextChanged(object sender, TextChangedEventArgs e)
        {
            string inputText = textNhapSoTien.Text;
            textNhapDoanhThu.Text = inputText;
        }
    }
}
