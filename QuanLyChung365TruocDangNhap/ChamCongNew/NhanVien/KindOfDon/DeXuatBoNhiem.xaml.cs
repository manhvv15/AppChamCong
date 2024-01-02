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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.API_DS_NhanVien;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.CaLamViec;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.API_DS_ChucVu;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using System.IO;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien;
using Microsoft.Win32;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for DeXuatBoNhiem.xaml
    /// </summary>
    public partial class DeXuatBoNhiem : UserControl
    {
        MainChamCong Main;
        public class ChonChucVu
        {
            public string ChucVu { get; set; }
        }
        public class PhongBan1
        {
            public string PhongBan { get; set; }
        }
        public class ThanhVien
        {
            public string thanhVien { get; set; }
        }

        List<ChonChucVu> listChucVu = new List<ChonChucVu>();
        List<PhongBan1> listPhongBan = new List<PhongBan1>();
        List<ThanhVien> listThanhVien = new List<ThanhVien>();
        private void LoadChucVu()
        {

            listChucVu.Add(new ChonChucVu { ChucVu = "Sinh Viên Thực Tập " });
            listChucVu.Add(new ChonChucVu { ChucVu = "Sinh viên Part Time" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Nhân viên Chính Thức" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Trưởng Nhóm" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Nhóm Phó" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Tổ Trưởng" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Tổ Trưởng" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Trưởng Ban Dự Án" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Ban Dự Án" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Trưởng Phòng" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Trưởng Phòng" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Giám Đốc" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Giám Đốc" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Ban Dự Án" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Tổng Giám Đốc" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Tổng Giám Đốc" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Tổng Giám Đốc Tập Đoàn" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Tổng Giám Đốc Tập Đoàn" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Chủ Tịch Hội Đồng Quản Trị" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Chủ Tịch Hội Đồng Quản Trị" });
            lsvChonChucVu.ItemsSource = listChucVu;
        }
        private void LoadPhongBan()
        {

            listPhongBan.Add(new PhongBan1 { PhongBan = "Kỹ Thuật" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Biên Tập" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Nhân viên Chính Thức" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Kinh Doanh" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Nhóm Phó" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Đề Án" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Phòng Seo" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Phòng Đào Tạo" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Phòng Sáng Tạo" });

            lsvPhongBan.ItemsSource = listPhongBan;
        }

        public DeXuatBoNhiem(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            Dictionary<string, string> ItemCbxKieu_Phe_Duyet = new Dictionary<string, string>();
            ItemCbxKieu_Phe_Duyet.Add("0", "Duyệt đồng thời");
            ItemCbxKieu_Phe_Duyet.Add("1", "Duyệt lần lượt");
            ComboBox.ItemsSource = ItemCbxKieu_Phe_Duyet;
            LoadChucVu();
            LoadPhongBan();
            getDSNhanVien();
            getDSChucVu();
            getInfor();
            getNoiCongTac();
            getPosiTion();
            getNgDuyet();
            getTheoD();
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
                var content = new StringContent("{\"dexuat_id\": 7}", null, "application/json");
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
                var content = new StringContent("{\"dexuat_id\": 7}", null, "application/json");
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
                            if (item.dexuat_id == 7)
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
                            if (item.dexuat_id == 7)
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
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.ComdID.ToString()), "com_id");
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

        private void borTenChonLoai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChonChucVu d = (sender as Border).DataContext as ChonChucVu;
            if (d != null)
            {
                textChon.Text = d.ChucVu;

            }
        }

        private void Grid_MouseLeftButtonUp11(object sender, MouseButtonEventArgs e)
        {
            if (borChonChucVu.Visibility == Visibility.Collapsed)
            {

                borChonChucVu.Visibility = Visibility.Visible;

            }
            else
            {
                borChonChucVu.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvChonChucVu_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollChonChucVu.ScrollToVerticalOffset(scrollChonChucVu.VerticalOffset - e.Delta);
        }



        private void borPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PhongBan1 d = (sender as Border).DataContext as PhongBan1;
            if (d != null)
            {
                textPhongBan.Text = d.PhongBan; ;

            }
        }


        private void Grid_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            if (borThanhVien.Visibility == Visibility.Collapsed)
            {

                borThanhVien.Visibility = Visibility.Visible;

            }
            else
            {
                borThanhVien.Visibility = Visibility.Collapsed;

            }

        }



        private void lsvThanhVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCoCau.ScrollToVerticalOffset(scrollCoCau.VerticalOffset - e.Delta);

        }


        private async void getDSNhanVien()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/getUserWithOrganize");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    API_NVV.DS_NVV api = JsonConvert.DeserializeObject<API_NVV.DS_NVV>(responseContent);
                    if (api.data.user != null)
                    {
                        lsvThanhVien.ItemsSource = api.data.user;
                        // listUsersDuyets = api.data.listUsersDuyet;
                        //listUsersTheoDoi = lsvNguoiTheoDoi;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại! " + ex.Message);
            }


        }
        private async void getDSChucVu()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/positions");

                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {
                    OOP.NhanVien.DonDeXuat.API_DS_ChucVu.API_DS_Chuc api = JsonConvert.DeserializeObject<OOP.NhanVien.DonDeXuat.API_DS_ChucVu.API_DS_Chuc>(responseContent);
                    if (api.positions != null)
                    {
                        lsvChonChucVu.ItemsSource = api.positions;
                        // listUsersDuyets = api.data.listUsersDuyet;
                        //listUsersTheoDoi = lsvNguoiTheoDoi;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại! " + ex.Message);
            }


        }
        private void lsvPhongBann_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollPhongBann.ScrollToVerticalOffset(scrollPhongBann.VerticalOffset - e.Delta);
        }

        private void borTenChonLoai_MouseLeftButtonDown11(object sender, MouseButtonEventArgs e)
        {
            OOP.NhanVien.DonDeXuat.API_DS_ChucVu.Position d = (sender as Border).DataContext as OOP.NhanVien.DonDeXuat.API_DS_ChucVu.Position;
            if (d != null)
            {
                textChon.Text = d.positionName;
                borChonChucVu.Visibility = Visibility.Collapsed;
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhongBan1 d = (sender as Border).DataContext as PhongBan1;
            if (d != null)
            {
                textPhongBan.Text = d.PhongBan; ;
                borPhongBan.Visibility = Visibility.Collapsed;
            }
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            API_NVV.User d = (sender as Border).DataContext as API_NVV.User;
            if (d != null)
            {
                textThanhVien.Text = d.ep_name; ;
                //textPhongBan1.Text = d.dep_name;
                //OOP.NhanVien.DonDeXuat.API_DS_ChucVu.Position d1 = (sender as Border).DataContext as OOP.NhanVien.DonDeXuat.API_DS_ChucVu.Position;
                //txtChucVuHT.Text = d.position_id.ToString();
                borThanhVien.Visibility = Visibility.Collapsed;
            }
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private async void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
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
                if (textBoNhiem.Text == "Chọn người bổ nhiệm")
                {
                    allow = false;
                    validateTV.Text = "Vui lòng chọn người bổ nhiệm";
                }
                else
                {
                    validateTV.Visibility = Visibility.Collapsed;
                }
                if (textChucVu.Text == "Chọn chức vụ đề xuất bổ nhiệm")
                {
                    allow = false;
                    validateChucVu.Text = "Vui lòng chọn chức vụ";
                }
                else
                {
                    validateChucVu.Visibility = Visibility.Collapsed;
                }
                if (textCoCauMoi.Text == "Chọn cơ cấu tổ chức mới")
                {
                    allow = false;
                    validateCoCauMoi.Text = "Vui lòng chọn cơ cấu mới";
                }
                else
                {
                    validateCoCauMoi.Visibility = Visibility.Collapsed;
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
                    var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/De_Xuat_Xin_Bo_Nhiem");
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
                    // idTheoDoi = ((ListUsersTheoDoi)lsvNguoiTheoDoi.SelectedItem).idQLC;
                    content.Add(new StringContent(Convert.ToString(idTheoD)), "id_user_theo_doi");
                    content.Add(new StringContent(textNhapLiDo.Text), "ly_do");
                    content.Add(new StringContent(ep_id.ToString()), "thanhviendc_bn");
                    content.Add(new StringContent(organizeDetailId.ToString()), "organizeDetailId");
                    content.Add(new StringContent(txtChucVuHT.Text), "positionName");
                    content.Add(new StringContent(position_id.ToString()), "chucvu_hientai");
                    content.Add(new StringContent(CoCauMoi_id.ToString()), "new_organizeDetailId");
                    content.Add(new StringContent(dx_bonhiem_id.ToString()), "chucvu_dx_bn");

                    content.Add(new StringContent(ComboBox.SelectedValue.ToString()), "kieu_duyet");

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (borPhongBan.Visibility == Visibility.Collapsed)
            {
                borPhongBan.Visibility = Visibility.Visible;
            }
            else
            {
                borPhongBan.Visibility = Visibility.Collapsed;
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

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borChonChucVu.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            borPhongBan.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            borThanhVien.Visibility = Visibility.Collapsed;
        }

        private void Grid_MouseLeftButdtofdnUp_2(object sender, MouseButtonEventArgs e)
        {
            if (borBoNhiem.Visibility == Visibility.Collapsed)
            {
                borBoNhiem.Visibility = Visibility.Visible;
            }
            else
            {
                borBoNhiem.Visibility = Visibility.Collapsed;
            }
        }

        private void Rectangle_MouseLeftButtonUp2(object sender, MouseButtonEventArgs e)
        {
            borBoNhiem.Visibility = Visibility.Collapsed;
        }
        List<API_DuAn.User> listUser = new List<API_DuAn.User>();
        private async void getInfor()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/getUserWithOrganize");
                //request.Headers.Add("authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjp7Il9pZCI6MTAwMjA0MTksImlkVGltVmllYzM2NSI6MTExMTEyNzkyNSwiaWRRTEMiOjEwMDE4NzAxLCJpZFJhb05oYW5oMzY1IjoxMDAxNjc1OSwiZW1haWwiOm51bGwsInBob25lVEsiOiIwMzUzMjcxOTQyIiwiY3JlYXRlZEF0IjoxNjk3NjkwNzk1LCJ0eXBlIjoyLCJjb21faWQiOjEwMDEzNDQ2LCJ1c2VyTmFtZSI6IlbFqSBWxINuIE3huqFuaCJ9LCJpYXQiOjE2OTk1Nzk0MTMsImV4cCI6MTY5OTY2NTgxM30.bI6N5V9hwecU6g7jsAaigRqCPgBJ5mFBoUtds8gsmGQ");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                // 
                // var content = new MultipartFormDataContent();
                // content.Add(new StringContent(ep_id), "ep_id");
                //request.Content = content;

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DuAn.Du_An api = JsonConvert.DeserializeObject<API_DuAn.Du_An>(responseContent);
                    if (api != null)
                    {
                        listUser = api.data.user;
                        lsvBoNhiem.ItemsSource = api.data.user;
                        //organizeDetailName = 
                        // cocau.Text = listUser[0].organizeDetailName;
                        //chucvu.Text = listUser[0].positionName;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        private async void getInfor1()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/getUserWithOrganize");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ep_id), "ep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DuAn.Du_An api = JsonConvert.DeserializeObject<API_DuAn.Du_An>(responseContent);
                    if (api != null)
                    {
                        listUser = api.data.user;
                        //lsvBoNhiem.ItemsSource = api.data.user;
                        cocau.Text = listUser[0].organizeDetailName;
                        chucvuhientai.Text = listUser[0].positionName;
                        organizeDetailId = (int)listUser[0].organizeDetailId;
                        position_id = (int)listUser[0].position_id;

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        string ep_id = "";
        int position_id;
        int bonhiem_id;
        private void borTenChonCa1_MouseeLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            API_DuAn.User d = (sender as Border).DataContext as API_DuAn.User;
            if (d != null)
            {
                textBoNhiem.Text = d.ep_name;
                ep_id = d.ep_id.ToString();
                //bonhiem_id = d.
                borBoNhiem.Visibility = Visibility.Collapsed;
                getInfor1();
            }
        }

        private void Grid_MouvsseLeftButdtofdnUp_2(object sender, MouseButtonEventArgs e)
        {
            //2
            if (borChonCa1.Visibility == Visibility.Collapsed)
            {
                borChonCa1.Visibility = Visibility.Visible;
            }
            else
            {
                borChonCa1.Visibility = Visibility.Collapsed;
            }
        }

        private void Grid_MoussqeLeftButdtofdnUp_2(object sender, MouseButtonEventArgs e)
        {
            //cocau borCoCauMoi1
            if (borCoCauMoi1.Visibility == Visibility.Collapsed)
            {
                borCoCauMoi1.Visibility = Visibility.Visible;
            }
            else
            {
                borCoCauMoi1.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvCongTac_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //scroll scrollBoNhiem
            scrollBoNhiem.ScrollToVerticalOffset(scrollBoNhiem.VerticalOffset - e.Delta);
        }
        private async void getNoiCongTac()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/organizeDetail/listAll");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                //  content.Add(new StringContent("10003135"), "com_id");
                content.Add(new StringContent(Main.ComdID.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_CongTac.CongTac api = JsonConvert.DeserializeObject<API_CongTac.CongTac>(responseContent);
                    if (api != null)
                    {
                        lsvCoCauMoi.ItemsSource = api.data.data;

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Rectangle_MouseLeftBduttonUp2(object sender, MouseButtonEventArgs e)
        {
            borCoCauMoi1.Visibility = Visibility.Collapsed;
        }

        private void lsvCongTac_PrevieswMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //scrollChonCa1
            scrollChonCa1.ScrollToVerticalOffset(scrollChonCa1.VerticalOffset - e.Delta);
        }
        int organizeDetailId;
        int CoCauMoi_id;
        private void borTenChonCza1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            API_CongTac.NoiCongTac d = (sender as Border).DataContext as API_CongTac.NoiCongTac;
            if (d != null)
            {
                textCoCauMoi.Text = d.organizeDetailName;
                CoCauMoi_id = (int)d.id;
                //organizeDetailId = d.organizeDetailId;
                borChonCa1.Visibility = Visibility.Collapsed;
            }
        }
        private async void getPosiTion()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/positions");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.ComdID.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_Position.Data_Position api = JsonConvert.DeserializeObject<API_Position.Data_Position>(responseContent);
                    if (api != null)
                    {
                        lsvCongTfac.ItemsSource = api.positions;

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void borTenChonCza1_sMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            API_CongTac.NoiCongTac d = (sender as Border).DataContext as API_CongTac.NoiCongTac;
            if (d != null)
            {
                textCoCauMoi.Text = d.organizeDetailName;
                //organizeDetailId = d.organizeDetailId;
                CoCauMoi_id = (int)d.id;
                borCoCauMoi1.Visibility = Visibility.Collapsed;
            }
        }
        int dx_bonhiem_id;
        private void borTenChonCa1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            API_Position.Position d = (sender as Border).DataContext as API_Position.Position;
            if (d != null)
            {
                textChucVu.Text = d.positionName;
                dx_bonhiem_id = (int)d.id;
                borChonCa1.Visibility = Visibility.Collapsed;
            }
        }

        private void Rectangle_MouseLefatButtonUp2(object sender, MouseButtonEventArgs e)
        {
            borChonCa1.Visibility = Visibility.Collapsed;
        }

        private void lsvCongTfac_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            // e.Handled = true;
        }

        private void borTenChonCa1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //e.Handled = true;
        }

        private void lsvCongTac_PreviewMoudseWheel(object sender, MouseWheelEventArgs e)
        {

        }

        private void lsvCongTac_PrevieswMosuseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollToChucMoi.ScrollToVerticalOffset(scrollToChucMoi.VerticalOffset - e.Delta);
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
