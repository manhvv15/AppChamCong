using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.LichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Microsoft.Win32;
using System.Windows;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien;
using System.Globalization;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for TaiSanCapPhat.xaml
    /// </summary>
    public partial class TaiSanCapPhat : UserControl
    {
        MainChamCong Main;
        private class VatChat
        {
            public int stt { get; set; }

        }
        // private ObservableCollection<VatChat> listInfor = new ObservableCollection<VatChat>();

        public TaiSanCapPhat(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
            ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");
            ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
            ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
            getNgDuyet();
            getTheoD();
            getDsTaiSan();
            txtName.Text = main.txbNameAccount.Text;
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
                var content = new StringContent("{\"dexuat_id\": 1}", null, "application/json");
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
                var content = new StringContent("{\"dexuat_id\": 1}", null, "application/json");
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
                            if (item.dexuat_id == 1)
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
                            if (item.dexuat_id == 1)
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
        private bool ContainsSpace(string text)
        {
            return text.Contains(" ");
        }
        private void AllowOnlyNumbers(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumber(e.Text) || ContainsSpace(e.Text))
            {
                e.Handled = true;
            }

        }

        private bool IsNumber(string text)
        {
            return int.TryParse(text, out _);
        }
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
        ObservableCollection<VatChat> list = new ObservableCollection<VatChat>();
        private void Border_MouseLeftButtonUp11(object sender, MouseButtonEventArgs e)
        {
            VatChat infor = new VatChat();
            list.Add(infor);
            // infor.stt = list.Count + 1;
            infor.stt = list.IndexOf(infor) + 1;

            dgv.ItemsSource = list;
        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            VatChat index = (VatChat)dgv.SelectedItem;
            if (index != null)
            {
                list.Remove(index);
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].stt = i + 1;
                };
                dgv.ClearValue(ItemsControl.ItemsSourceProperty);
                dgv.ItemsSource = list;
            }
        }


        string TenTep = "";
        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
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
                    validateLyDo.Text = "Vui lòng nhập lý do  ";
                }
                else if (!string.IsNullOrEmpty(textNhapLiDo.Text))
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
                if (txtTaiSan.Text == "Chọn tài sản")
                {
                    allow = false;
                    validateTs.Text = "Vui lòng chọn tài sản";
                }
                else
                {
                    validateTs.Visibility = Visibility.Collapsed;
                }
                if (NgayBatDau.Text == "")
                {
                    allow = false;
                    validateNgayBD.Text = "Vui lòng nhập ngày ";
                }
                else
                {
                    validateNgayBD.Visibility = Visibility.Collapsed;
                }
                if (string.IsNullOrEmpty(textNhapSoLuong.Text))
                {
                    allow = false;
                    validateSoLuong.Text = "Vui lòng nhập số lượng ";
                }
                else if (!string.IsNullOrEmpty(textNhapSoLuong.Text))
                {
                    validateSoLuong.Visibility = Visibility.Collapsed;
                }
                if (string.IsNullOrEmpty(textMoney.Text))
                {
                    allow = false;
                    validateMoney.Text = "Vui lòng nhập chi phí ";
                }
                else if (!string.IsNullOrEmpty(textMoney.Text))
                {
                    validateMoney.Visibility = Visibility.Collapsed;
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
                // validateLyDo.Visibility = Visibility.Collapsed;
                if (allow)
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/addDXVC");
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
                    string ngayBatDau = NgayBatDau.Text;
                    DateTime ngayBatDauDate = DateTime.Parse(ngayBatDau);
                    string ngayBatDauFormat = ngayBatDauDate.ToString("dd/MM/yyyy");
                    long epochTime = ConvertToEpochTime(ngayBatDauFormat);
                    content.Add(new StringContent(Convert.ToString(epochTime)), "ngay_sc");
                    content.Add(new StringContent(textNhapLiDo.Text), "ly_do");
                    content.Add(new StringContent(id_Ts), "tai_san");
                    content.Add(new StringContent(ComboBox.SelectedValue.ToString()), "kieu_duyet");
                    if (Int32.Parse(sl_conlai) < Int32.Parse(textNhapSoLuong.Text))
                    {
                        // Debug.WriteLine("Số lượng còn lại phải lớn hơn số lượng cấp phát");
                        Main.grShowPopup.Children.Add(new ucPopupError("Số lượng còn lại phải lớn hơn số lượng cấp phát"));
                    }
                    else
                    {
                        content.Add(new StringContent(textNhapSoLuong.Text), "so_luong");
                        content.Add(new StringContent(textMoney.Text), "so_tien");
                        if (tepDinhKem.Text != "Thêm tài liệu đính kèm") content.Add(new StreamContent(File.OpenRead(TenTep)), "fileKem", tepDinhKem.Text);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseLeaftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (BorTaiSan.Visibility == Visibility.Collapsed)
            {
                BorTaiSan.Visibility = Visibility.Visible;
            }
            else
            {
                BorTaiSan.Visibility = Visibility.Collapsed;
            }
        }
        private async void getDsTaiSan()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/listTSdaCPchoNV");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                //     request.Headers.Add("authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjp7Il9pZCI6MTAwNDM5OTMsImlkVGltVmllYzM2NSI6MTExMTE1MDE3NywiaWRRTEMiOjEwMDQwOTU2LCJpZFJhb05oYW5oMzY1IjoxMDAzOTAxNSwiZW1haWwiOiJmc2Z2ZmFzQGdtYWlsLmNvbSIsInBob25lVEsiOiIwOTY4NTIxNzQ1IiwiY3JlYXRlZEF0IjoxNjk5NDk2ODc0LCJ0eXBlIjoyLCJjb21faWQiOjEwMDAzMTM1LCJ1c2VyTmFtZSI6Iktp4buBdSBUaeG6v24gTeG6oW5oIn0sImlhdCI6MTcwMTA0NjgwMiwiZXhwIjoxNzAxMTMzMjAyfQ.lK1ougandBgT7rl30xYEgDM9SefT45rXSJ03EaVM3EU");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                //var content = new StringContent("{}", null, "application/json");
                //request.Content = content;
                //var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DsTaiSanSuaChua.Ds_TaiSan api = JsonConvert.DeserializeObject<API_DsTaiSanSuaChua.Ds_TaiSan>(responseContent);
                    if (api != null)
                    {
                        lsvTaiSan.ItemsSource = api.data;
                    }
                }

            }
            catch (Exception ex)
            {

            }

        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BorTaiSan.Visibility = Visibility.Collapsed;
        }
        string name_Ts;
        string id_Ts;
        string sl_conlai;
        private void lsvTs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            API_DsTaiSanSuaChua.TaiSan d = (API_DsTaiSanSuaChua.TaiSan)lsvTaiSan.SelectedItem;
            if (d != null)
            {
                txtTaiSan.Text = d.Name;
                name_Ts = d.capital_name;
                id_Ts = d.Ma_tai_san.ToString();
                sl_conlai = d.sl_dang_sd.ToString();
            }
            BorTaiSan.Visibility = Visibility.Collapsed;
        }
    }
}
