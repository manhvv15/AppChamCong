using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Tool
{
    /// <summary>
    /// Interaction logic for NgDuyetAndNgTheoD.xaml
    /// </summary>
    public partial class NgDuyetAndNgTheoD : UserControl
    {

        public NgDuyetAndNgTheoD()
        {
            InitializeComponent();

           // getNgDuyet();
          //  getTheoD();
        }
        //Nguoi xet duyet va nguoi theo doi
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
        private void borTenChonLoai_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListUsersTheoDoi d = (sender as Border).DataContext as ListUsersTheoDoi;
            if (d != null)
            {
                txtTheoD.Text = d.userName;
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
    }
}
