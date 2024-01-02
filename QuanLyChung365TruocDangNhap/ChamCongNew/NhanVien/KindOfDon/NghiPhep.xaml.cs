using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.IO;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.NghiPhep;
using static QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose.listTess;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CongCong.clsDSCongCong;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.CaLamViec;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.ToiGuiDi.DeXuatToiGuiDi;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.LichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Tool;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.API_DsCRM;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for NghiPhep.xaml
    /// </summary>

    public partial class NghiPhep : UserControl
    {
        MainChamCong Main;
        QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon.LichLamViec.NguoiXetDuyet ListNg;
        public class ChonLoai
        {
            public string Loai { get; set; }
        }
        public class ChonLuong
        {
            public string Luong { get; set; }
        }

        public class NguoiXetDuyet
        {
            public string nguoiXetDuyet { get; set; }
        }
        public class NguoiTheoDoi
        {
            public string nguoiTheoDoi { get; set; }
        }
        public class InforNghiPhep
        {
            public int stt { get; set; }
            public int caNghi_id { get; set; }
            public string caNghi { get; set; }
            public string ngayBatDau { get; set; }
            public string ngayKetThuc { get; set; }


        }
        private class XetDuyet
        {
            public string nguoixetduyet { get; set; }
        }
        //List<InforNghiPhep> listInfor = new List<InforNghiPhep>();
        private ObservableCollection<InforNghiPhep> listInfor = new ObservableCollection<InforNghiPhep>();
        List<ChonLoai> listLoai = new List<ChonLoai>();
        List<ChonLuong> listLuong = new List<ChonLuong>();
        List<string> listCaNghi = new List<string>();
        List<NguoiXetDuyet> listNguoiXetDuyet = new List<NguoiXetDuyet>();
        List<NguoiTheoDoi> listNguoiTheoDoi = new List<NguoiTheoDoi>();
        public NghiPhep(MainChamCong main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
            ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");
            ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
            ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;

            LoadChonCaNghi();
            //getDataCaLamViec();
            getNgDuyet();
            getTheoD();
            getDsCRM();
            getSettingComfirm();
            txtName.Text = main.txbNameAccount.Text;

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

        private void LoadChonCaNghi()
        {

            listCaNghi.Add("Nghỉ cả ngày(tất cả các ca)");
            listCaNghi.Add("Ca sáng 7TR < LƯƠNG <= 10TR ");
            listCaNghi.Add("Ca chiều 7TR < LƯƠNG <= 10TR ");

            //lsvCaNghi.ItemsSource = listCaNghi;
        }
        private void lsvCaNghi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (NgayBatDau.Text == "" || NgayKetThuc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ngày", "Tìm việc 365 said:", MessageBoxButton.OK, MessageBoxImage.Error);

                BorCaNghi.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (dgv.Visibility == Visibility.Collapsed)
                {
                    dgv.Visibility = Visibility.Visible;
                }
                DateTime date1, date2;
                //date1 = DateTime.Parse(NgayBatDau.Text);
                date1 = NgayBatDau.SelectedDate.Value;
                date2 = NgayKetThuc.SelectedDate.Value;
                //date2 = DateTime.Parse(NgayKetThuc.Text);
                if (date1 <= date2)
                {
                    GetInfor1();
                }
                else
                {
                    MessageBox.Show("Ngày kết thúc nghỉ phải lớn hơn hoặc bằng ngày bắt đầu nghỉ", "Tìm việc 365 said:", MessageBoxButton.OK, MessageBoxImage.Error);
                    BorCaNghi.Visibility = Visibility.Collapsed;
                }


            }

        }


        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        public int count = 1;

        private void GetInfor()
        {
            dgv.ItemsSource = listInfor;

        }
        string ngayBatDau = "";
        DateTime ngayBatDauDate;
        string ngayBatDauFormat = "";
        string ngayKetThuc = "";
        string ngayKetThucFormat = "";

        DateTime ngayKetThucDate;
        private void GetInfor1()
        {
            try
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
                if (allow)
                {
                    InforNghiPhep infor = new InforNghiPhep()
                    {
                        caNghi = ((API_CaLvByID.List)lsvCaLamViec.SelectedItem).shift_name,
                        caNghi_id = (int)((API_CaLvByID.List)lsvCaLamViec.SelectedItem).shift_id,
                        ngayBatDau = NgayBatDau.Text,
                        ngayKetThuc = NgayKetThuc.Text
                    };
                    listInfor.Add(infor);
                    infor.stt = listInfor.IndexOf(infor) + 1;
                    dgv.ItemsSource = listInfor;
                    BorCaNghi.Visibility = Visibility.Collapsed;
                }
            }
            catch (Exception ex)
            {
                BorCaNghi.Visibility = Visibility.Collapsed;
            }

        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            InforNghiPhep index = (InforNghiPhep)dgv.SelectedItem;
            if (index != null)
            {
                listInfor.Remove(index);
                for (int i = 0; i < listInfor.Count; i++)
                {
                    listInfor[i].stt = i + 1;
                };
                dgv.ClearValue(ItemsControl.ItemsSourceProperty);
                dgv.ItemsSource = listInfor;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cbDotXuat.IsChecked = false;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            cbKeHoach.IsChecked = false;
        }
        private async void getDataCaLamViec()
        {

            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/empShiftInDay");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                //request.Headers.Add("Authorization", "Bearer " + Properties.Settings.);
                ngayKetThuc = NgayBatDau.Text;
                ngayBatDauDate = DateTime.Parse(ngayKetThuc);
                ngayBatDauFormat = ngayBatDauDate.ToString("yyyy-MM-dd");
                var content = new StringContent("{\"day\":\"" + ngayBatDauFormat + "\"}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                API_CaLvByID.CaLVByID dsCa = JsonConvert.DeserializeObject<API_CaLvByID.CaLVByID>(responseContent);


                if (dsCa != null)
                {
                    //caComon = dsCa.data.items;
                    listCa = dsCa.list;
                    API_CaLvByID.List newItem = new API_CaLvByID.List
                    {
                        shift_name = "Nghỉ cả ngày (tất cả các ca)",
                        shift_id = 0
                    };
                    listCa.Insert(0, newItem);
                    lsvCaLamViec.ItemsSource = listCa;
                    //g
                }
            }
            catch (Exception)
            {
            }

        }
        List<API_CaLvByID.List> listCa = new List<API_CaLvByID.List>();

        public class IDNgTheoDoi
        {
            int id { get; set; }


        }
        int idTheoDoi;
        List<InforNghiPhep> lsNghiPhep = new List<InforNghiPhep>();
        string CaNghi_id;
        //List<InforNghiPhep> lsNghiPhep = new List<InforNghiPhep>();
        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

            try
            {
                bool allow = true;
                // validateName.Text = validateLyDo.Text = "";
                if (string.IsNullOrEmpty(textNhapTenDeXuat.Text))
                {
                    allow = false;
                    validateName.Text = "Vui lòng nhập tên đơn nghỉ phép ";
                }
                else if (!string.IsNullOrEmpty(textNhapTenDeXuat.Text))
                {
                    validateName.Visibility = Visibility.Collapsed;
                }


                if (string.IsNullOrEmpty(textNhapLiDo.Text))
                {
                    allow = false;
                    validateLyDo.Text = "Vui lòng nhập lý do xin nghỉ ";
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

                if (cbDotXuat.IsChecked == false && cbKeHoach.IsChecked == false)
                {
                    allow = false;
                    validateType.Text = "Vui lòng chọn loại nghỉ";
                }
                else
                {
                    validateType.Visibility = Visibility.Collapsed;
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
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/De_Xuat_Xin_Nghi");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(textNhapTenDeXuat.Text), "name_dx");
                    if (cbKeHoach.IsChecked == true)
                    {
                        content.Add(new StringContent("1"), "loai_np");
                    }
                    if (cbDotXuat.IsChecked == true)
                    {
                        content.Add(new StringContent("2"), "loai_np");
                    }
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
                    StringBuilder jsonStringBuilder = new StringBuilder();
                    StringBuilder jsonStringBuilder1 = new StringBuilder();
                    bool firstItem = true;

                    jsonStringBuilder.Append("{\"nghi_phep\": [[");
                    foreach (var item in listInfor)
                    {
                        ngayBatDau = item.ngayBatDau;
                        ngayKetThuc = item.ngayKetThuc;
                        DateTime ngayBatDauDate = DateTime.Parse(ngayBatDau);
                        string ngayBatDauFormat = ngayBatDauDate.ToString("yyyy-MM-ddTHH:mm:ss.fff+00:00");
                        DateTime ngayKetThucDate = DateTime.Parse(ngayKetThuc);
                        string ngayKetThucFormat = ngayKetThucDate.ToString("yyyy-MM-ddTHH:mm:ss.fff+00:00");
                        if (item.caNghi == "Nghỉ cả ngày (tất cả các ca)")
                        {
                            CaNghi_id = "";
                        }
                        else
                        {
                            CaNghi_id = item.caNghi_id.ToString();
                        }


                        if (!firstItem)
                        {
                            jsonStringBuilder1.Append("],[");
                        }
                        else
                        {
                            firstItem = false;
                        }

                        jsonStringBuilder1.Append($"\"{ngayBatDauFormat}\", \"{ngayKetThucFormat}\", \"{Convert.ToString(CaNghi_id)}\"");
                        //jsonStringBuilder1.Append(",");
                    }
                    content.Add(new StringContent(ComboBox.SelectedValue.ToString()), "kieu_duyet");
                    //CaNghi_id = ((Item_CaLamViec)lsvCaLamViec.SelectedItem).shift_id;
                    jsonStringBuilder.Append(jsonStringBuilder1);
                    jsonStringBuilder.Append("]]}");
                    content.Add(new StringContent(jsonStringBuilder.ToString()), "noi_dung");
                    //     content.Add(new StringContent("{\"nghi_phep\":[[\"2023-12-05\",\"2023-12-05\",\"\"],[\"2023-12-05\",\"2023-12-05\",2025500],[\"2023-12-05\",\"2023-12-05\",2025489]]}"), "noi_dung");
                    content.Add(new StringContent(textNhapLiDo.Text), "ly_do");
                    if (txtCRM.Text != "Chọn người bàn giao CRM")
                    {
                        content.Add(new StringContent(id_crm), "id_user_bangiao_CRM");

                    }
                    if (tepDinhKem.Text != "Thêm tài liệu đính kèm") content.Add(new StreamContent(File.OpenRead(TenTep)), "fileKem", tepDinhKem.Text);
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    response.EnsureSuccessStatusCode();

                    if (response.IsSuccessStatusCode)
                    {
                        TaoDeXuatThanhCong uc = new TaoDeXuatThanhCong(Main);
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
        static readonly HttpClient client = new HttpClient();



        private void lsvCaLamViec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NgayBatDau.Text == "" || NgayKetThuc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ngày", "Tìm việc 365 said:", MessageBoxButton.OK, MessageBoxImage.Error);

                BorCaNghi.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (dgv.Visibility == Visibility.Collapsed)
                {
                    dgv.Visibility = Visibility.Visible;
                }
                DateTime date1, date2;
                date1 = DateTime.Parse(NgayBatDau.Text);
                date2 = DateTime.Parse(NgayKetThuc.Text);
                if (date1 <= date2)
                {
                    GetInfor1();
                    validateLoi.Visibility = Visibility.Collapsed;
                }
                else
                {
                    validateLoi.Visibility = Visibility.Visible;
                    validateLoi.Text = "Ngày kết thúc nghỉ phải lớn hơn hoặc bằng ngày bắt đầu nghỉ";
                    // MessageBox.Show("Ngày kết thúc nghỉ phải lớn hơn hoặc bằng ngày bắt đầu nghỉ", "Tìm việc 365 said:", MessageBoxButton.OK, MessageBoxImage.Error);
                    BorCaNghi.Visibility = Visibility.Collapsed;
                }

                ////Ptu p = new Ptu();
                ////p.canghi
                ////p.Count = IndexOf(p) + 1;
            }
        }



        public class FileLuong
        {
            public string FileName { get; set; }
        }

        private List<FileLuong> lstFileL = new List<FileLuong>();
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


        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Grid_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            if (NgayBatDau.Text == "")
            {

            }
            else
            {
                getDataCaLamViec();
                if (BorCaNghi.Visibility == Visibility.Collapsed)
                {

                    BorCaNghi.Visibility = Visibility.Visible;

                }
                else
                {
                    BorCaNghi.Visibility = Visibility.Collapsed;

                }
            }

        }

        private void borTenChonLoai_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            //borNgD.Visibility = Visibility.Collapsed;

        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BorCaNghi.Visibility = Visibility.Collapsed;
        }

        private void Border_MousesLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
        List<API_DsCRM.Item> listCRM = new List<API_DsCRM.Item>();
        private async void getDsCRM()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/listAll");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.ComdID.ToString()), "com_id");
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    // Xử lý phản hồi ở đây
                    API_DsCRM.DS_CRM api = JsonConvert.DeserializeObject<API_DsCRM.DS_CRM>(responseContent);
                    if (api.data != null)
                    {
                        listCRM = api.data.items;
                        lsvCRM.ItemsSource = listCRM;
                        lsvCRM1.ItemsSource = listCRM;
                        //   listUDuyets = api.data.listUsersDuyet;
                    }
                }


            }
            catch (Exception ex)
            {

            }
        }

        private void Rectangle_MouseLeftButtaonUp(object sender, MouseButtonEventArgs e)
        {
            BorCRM.Visibility = Visibility.Collapsed;
        }

        private void lsvCRM_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                API_DsCRM.Item d = (API_DsCRM.Item)lsvCRM.SelectedItems;
                if (d != null)
                {
                    txtCRM.Text = d.ep_name;
                }
                BorCRM.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {

            }

        }

        private void Grid_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            if (BorCRM1.Visibility == Visibility.Collapsed)
            {
                BorCRM1.Visibility = Visibility.Visible;
            }
            else
            {
                BorCRM1.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvCRM_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCRM.ScrollToVerticalOffset(scrollCRM.VerticalOffset - e.Delta);
        }

        private void Rectangle_MousseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BorCRM1.Visibility = Visibility.Collapsed;
        }

        private void lsvCRM1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    API_DsCRM.Item d = (API_DsCRM.Item)lsvCRM1.SelectedItems;
            //    if (d != null)
            //    {
            //        txtCRM.Text = d.ep_name;
            //    }
            //    BorCRM.Visibility = Visibility.Collapsed;
            //}
            //catch (Exception ex)
            //{

            //}
        }
        string id_crm;
        private void CRM1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            API_DsCRM.Item d = (sender as Border).DataContext as API_DsCRM.Item;
            if (d != null)
            {
                txtCRM.Text = d.ep_name;
                id_crm = d.ep_id.ToString();
            }
            BorCRM1.Visibility = Visibility.Collapsed;
        }

        private void lsvCRM1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCRM1.ScrollToVerticalOffset(scrollCRM1.VerticalOffset - e.Delta);
        }

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
