using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.LichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.Win32;
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
using System.Globalization;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
//using DocumentFormat.OpenXml.Office2016.Drawing.Command;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for DeXuatDangKiPhongHop.xaml
    /// </summary>
    public partial class DeXuatDangKiPhongHop : UserControl
    {
        MainChamCong Main;

        public DeXuatDangKiPhongHop(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
            ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");
            ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
            ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
            getNgDuyet();
            getTheoD();
            txtName.Text = main.txbNameAccount.Text;
            getPhopHop();
            // getDsPH();
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
                var content = new StringContent("{\"dexuat_id\": 12}", null, "application/json");
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
                var content = new StringContent("{\"dexuat_id\": 12}", null, "application/json");
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
                            if (item.dexuat_id == 12)
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
                            if (item.dexuat_id == 12)
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

        public class PhongHop
        {
            public string Ten { get; set; }
        }
        List<PhongHop> listPhongHop = new List<PhongHop> { };
        public void getPhopHop()
        {
            listPhongHop.Add(new PhongHop { Ten = "Số 1" });
            listPhongHop.Add(new PhongHop { Ten = "Số 2" });
            listPhongHop.Add(new PhongHop { Ten = "Số 3" });
            lsvPhongBan.ItemsSource = listPhongHop;
        }
        private void Border_MouseLeftButtonUp11(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
        string TenTep = "";
        public void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
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

        public static long ConvertToEpochTime(string dateString, string timeString)
        {
            string dateTimeString = dateString + " " + timeString;

            // Define the format of the input date-time string
            string format = "M/d/yyyy h:mm tt";

            // Parse the input date-time string using the specified format
            DateTime dateTime = DateTime.ParseExact(dateTimeString, format, null);

            // Calculate the number of seconds since Unix epoch (January 1, 1970)
            TimeSpan timeSpan = dateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)timeSpan.TotalSeconds;
        }
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

                if (string.IsNullOrEmpty(textNhapLyDo.Text))
                {
                    allow = false;
                    validateLyDo.Text = "Vui lòng nhập lý do xin đề xuất ";
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
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/addDxPh");
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
                    }
                    string idXetDuyet = string.Join("", listString);
                    content.Add(new StringContent(Convert.ToString(idXetDuyet)), "id_user_duyet");
                    content.Add(new StringContent(Convert.ToString(idTheoD)), "id_user_theo_doi");
                    content.Add(new StringContent(textNhapLyDo.Text), "ly_do");

                    content.Add(new StringContent(ngayBatDauFormat), "ngay_bd_hop");
                    content.Add(new StringContent(ComboBox.SelectedValue.ToString()), "kieu_duyet");
                    //        content.Add(new StringContent("1702351020"), "bd_hop");
                    //       content.Add(new StringContent("1702354620"), "end_hop");
                    DateTime time1 = DateTime.Parse(timepicker1.Text);
                    string inputDate1 = NgayBatDau.Text;
                    string inputDate2 = NgayKetThuc.Text;
                    string inputTime1 = timepicker1.Text;
                    string inputTime2 = timepicker2.Text;
                    string vietnamTimeZoneId = "SE Asia Standard Time";
                    long epochTime21 = ConvertToEpochTimeInTimeZone(inputDate1, inputTime1, vietnamTimeZoneId);
                    long epochTime22 = ConvertToEpochTimeInTimeZone(inputDate2, inputTime2, vietnamTimeZoneId);

                    long epochTime11 = ConvertToEpochTime(inputDate1, inputTime1);
                    long epochTime12 = ConvertToEpochTime(inputDate2, inputTime2);
                    content.Add(new StringContent(Convert.ToString(epochTime21)), "bd_hop");

                    DateTime time2 = DateTime.Parse(timepicker2.Text);

                    content.Add(new StringContent("06:34"), "gio_bd_hop");
                    content.Add(new StringContent("20:37"), "gio_end_hop");
                    content.Add(new StringContent(ngayKetThucDateFormat), "ngay_end_hop");
                    content.Add(new StringContent(Convert.ToString(epochTime22)), "end_hop");
                    if (tepDinhKem.Text != "Thêm tài liệu đính kèm") content.Add(new StreamContent(File.OpenRead(TenTep)), "fileKem", tepDinhKem.Text);
                    List<string> listId_Ph = new List<string>();
                    foreach (var item in listPHAdd)
                    {
                        listId_Ph.Add(item.id.ToString() + ",");
                    }
                    for (int i = 0; i < listId_Ph.Count; i++)
                    {
                        if (listId_Ph[i].EndsWith(",") && i == listId_Ph.Count - 1)
                        {
                            listId_Ph[i] = listId_Ph[i].Substring(0, listId_Ph[i].Length - 1);
                        }
                    }
                    string id_PH = string.Join("", listId_Ph);
                    content.Add(new StringContent(Convert.ToString(id_PH)), "phong_hop");
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
                MessageBox.Show(ex.Message);
            }
        }
        public static long ConvertToEpochTimeInTimeZone(string dateString, string timeString, string timeZoneId)
        {
            string dateTimeString = dateString + " " + timeString;

            // Define the format of the input date-time string
            string format = "M/d/yyyy h:mm tt";

            // Parse the input date-time string using the specified format
            DateTime localDateTime = DateTime.ParseExact(dateTimeString, format, null);

            // Get the time zone information for Vietnam
            TimeZoneInfo vietnamTimeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);

            // Convert local time to Vietnam time
            DateTime vietnamDateTime = TimeZoneInfo.ConvertTimeToUtc(localDateTime, vietnamTimeZone);

            // Calculate the number of seconds since Unix epoch (January 1, 1970)
            TimeSpan timeSpan = vietnamDateTime - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)timeSpan.TotalSeconds;
        }
        static string GetFormattedTime(DateTime dateTime)
        {
            return dateTime.ToString("hh:mm");
        }
        static string Convert12HourTo24Hour(string time12HourFormat)
        {
            DateTime parsedTime;
            if (DateTime.TryParseExact(time12HourFormat, "hh:mm tt", null, System.Globalization.DateTimeStyles.None, out parsedTime))
            {
                return parsedTime.ToString("HH:mm");
            }
            else
            {
                return "Invalid time format";
            }
        }
        private void CloseTimePicer_MouseUp(object sender, MouseButtonEventArgs e)
        {
            gridPopupTimePicker.Visibility = Visibility.Collapsed;
            timepicker1.Visibility = Visibility.Collapsed;
            timepicker2.Visibility = Visibility.Collapsed;
        }

        private void OpenTimePicker1(object sender, MouseButtonEventArgs e)
        {
            gridPopupTimePicker.Visibility = Visibility.Visible;
            timepicker1.Visibility = Visibility.Visible;
        }

        private void OpenTimePicker2(object sender, MouseButtonEventArgs e)
        {
            gridPopupTimePicker.Visibility = Visibility.Visible;
            timepicker2.Visibility = Visibility.Visible;
        }

        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void borTenChonLoai_MouseLeftButtonDown11(object sender, MouseButtonEventArgs e)
        {
            PhongHop d = (sender as Border).DataContext as PhongHop;
            if (d != null)
            {
                txtphonghop.Text = d.Ten;
                borPhongHop.Visibility = Visibility.Collapsed;
            }
        }

        private void Rectangle_MouseLeftButtfonUp(object sender, MouseButtonEventArgs e)
        {
            borPhongHop.Visibility = Visibility.Collapsed;

        }

        private void borphong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borPhongHop.Visibility == Visibility.Visible)
            {
                borPhongHop.Visibility = Visibility.Collapsed;
            }
            else
            {
                borPhongHop.Visibility = Visibility.Visible;
            }
        }
        long epochTime;
        long epochTime1;
        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;

            if (NgayBatDau.Text == "")
            {
                allow = false;
                validateNgayBD.Text = "Vui lòng nhập ngày bắt đầu ";
            }
            else
            {
                validateNgayBD.Visibility = Visibility.Collapsed;
            }
            if (NgayKetThuc.Text == "")
            {
                allow = false;
                validateNgayKT.Text = "Vui lòng nhập ngày kết thúc ";
            }
            else
            {
                validateNgayKT.Visibility = Visibility.Collapsed;
            }
            if (timepicker1.Text == "--:-- --")
            {
                allow = false;
                validateTime1.Text = "Vui lòng nhập thời gian bắt đầu ";
            }
            else
            {
                validateTime1.Visibility = Visibility.Collapsed;
            }
            if (timepicker2.Text == "--:-- --")
            {
                allow = false;
                validateTime2.Text = "Vui lòng nhập thời gian kết thúc ";
            }
            else
            {
                validateTime2.Visibility = Visibility.Collapsed;
            }
            if (allow)
            {
                DateTime time1 = DateTime.Parse(timepicker1.Text);
                DateTime time2 = DateTime.Parse(timepicker2.Text);
                string ngayBatDau = NgayBatDau.Text;
                DateTime ngayBatDauDate = DateTime.Parse(ngayBatDau);
                ngayBatDauFormat = ngayBatDauDate.ToString("yyyy-MM-dd");
                //   epochTime = ConvertToEpochTime(ngayBatDauFormat);
                string ngayKetThuc = NgayKetThuc.Text;
                DateTime ngayKetThucDate = DateTime.Parse(ngayKetThuc);
                //   string ngayKetThucDateFormat = ngayKetThucDate.ToString("dd/MM/yyyy");
                ngayKetThucDateFormat = ngayKetThucDate.ToString("yyyy-MM-dd");
                //     epochTime1 = ConvertToEpochTime(ngayKetThucDateFormat);
                if (epochTime > epochTime1)
                {
                    validateNghayKT.Visibility = Visibility.Visible;
                    validateNghayKT.Text = "Vui lòng chọn thời gian bắt đầu nhỏ hơn thời gian kết thúc";
                    validateNg.Visibility = Visibility.Collapsed;
                }
                else if (epochTime == epochTime1)
                {
                    if (time1 > time2)
                    {
                        validateNghayKT.Visibility = Visibility.Visible;
                        validateNghayKT.Text = "Vui lòng chọn thời gian bắt đầu nhỏ hơn thời gian kết thúc";
                        validateNg.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        validateNghayKT.Visibility = Visibility.Collapsed;
                        getDsPH();
                    }
                }
                else
                {
                    validateNghayKT.Visibility = Visibility.Collapsed;
                    validateNg.Visibility = Visibility.Collapsed;
                    getDsPH();
                }

                if (BorPH.Visibility == Visibility.Collapsed)
                {
                    BorPH.Visibility = Visibility.Visible;
                }
                else
                {
                    BorPH.Visibility = Visibility.Collapsed;
                }
            }

        }
        string ngayBatDauFormat;
        string ngayKetThucDateFormat;
        private void Rectangle_MoauseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BorPH.Visibility = Visibility.Collapsed;
        }
        private async void getDsPH()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/meetingRooms");

                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(epochTime.ToString()), "time_start");
                content.Add(new StringContent(epochTime1.ToString()), "time_end");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DsPhongHop.Ds_PhongHop api = JsonConvert.DeserializeObject<API_DsPhongHop.Ds_PhongHop>(responseContent);
                    if (api != null)
                    {
                        listPH = api.meetingRoomWitStatus;
                        lsvPH.ItemsSource = listPH;
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }

        private void lsvTs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvPH.SelectedItem != null)
                {
                    //string selectedPo = ((ListPo)lsvChucVu.SelectedItem).positionName;

                    string selected = ((API_DsPhongHop.MeetingRoomWitStatus)lsvPH.SelectedItem).name;
                    //    int selected_id = (int)((API_DsPhongHop.MeetingRoomWitStatus)lsvPH.SelectedItem).idQLC;
                    if (!listPHAdd.Any(item => item.name == selected))
                    {
                        API_DsPhongHop.MeetingRoomWitStatus listPo = new API_DsPhongHop.MeetingRoomWitStatus()
                        {
                            name = ((API_DsPhongHop.MeetingRoomWitStatus)lsvPH.SelectedItem).name,
                            id = ((API_DsPhongHop.MeetingRoomWitStatus)lsvPH.SelectedItem).id
                        };
                        listPHAdd.Add(listPo);
                        listPHAdd = listPHAdd.ToList();
                    }
                    lsvPHAdd.Visibility = Visibility.Visible;
                    textNhapPH.Focus();
                    textNhapPH1.Text = "";
                    textNhapPH.Text = "";
                    lsvPHAdd.ItemsSource = listPHAdd;
                    lsvPHAdd.Items.Refresh();
                    BorPH.Visibility = Visibility.Collapsed;


                }
            }
            catch (Exception ex)
            {

            }

        }

        private void PH_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }
        List<API_DsPhongHop.MeetingRoomWitStatus> listPH = new List<API_DsPhongHop.MeetingRoomWitStatus>();
        List<API_DsPhongHop.MeetingRoomWitStatus> listPHAdd = new List<API_DsPhongHop.MeetingRoomWitStatus>();
        private void PH_MouseLeave(object sender, MouseEventArgs e)
        {
            BorPH.Visibility = Visibility.Collapsed;
            textNhapPH.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((Border)sender).Background = grayBrush;
            if (listPHAdd.Count == 0)
            {
                lsvPHAdd.Visibility = Visibility.Collapsed;
                textNhapPH.Focus();
                textNhapPH1.Text = "Chọn phòng họp";
            }
        }

        private void PH_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_DsPhongHop.MeetingRoomWitStatus index = (API_DsPhongHop.MeetingRoomWitStatus)lsvPHAdd.SelectedItem;
            if (index != null)
            {
                listPHAdd.Remove(index);
                lsvPHAdd.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvPHAdd.ItemsSource = listPHAdd;
                shouldProcessEvent = false;
            }
            shouldProcessEvent = true;
            if (listPHAdd.Count == 0)
            {
                gridPH.Visibility = Visibility.Visible;
                lsvPHAdd.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void textNhapPH_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
