
using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.CaiDatDeX.OOP;
using QuanLyChung365TruocDangNhap.CaiDatDeX.ThongBao;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
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
using static QuanLyChung365TruocDangNhap.CaiDatDeX.OOP.API_DetailUser;
using static QuanLyChung365TruocDangNhap.CaiDatDeX.OOP.API_DsNV;


namespace QuanLyChung365TruocDangNhap.CaiDatDeX
{
    /// <summary>
    /// Interaction logic for DeXuat.xaml
    /// </summary>
    public partial class DeXuat : UserControl
    {
        frmMain Main;
        List<API_DsDeXuat.DsDeXuat> listLoaiDx = new List<API_DsDeXuat.DsDeXuat>();
        List<API_DsDeXuat.DsDeXuat> listLoaiDx1 = new List<API_DsDeXuat.DsDeXuat>();
        List<API_DsDeXuat.DsDeXuat> listAddLoaiDx = new List<API_DsDeXuat.DsDeXuat>();
        List<API_DsDeXuat.DsDeXuat> listAddLoaiDx1 = new List<API_DsDeXuat.DsDeXuat>();
        List<API_DsNV.NV> listAllNv = new List<API_DsNV.NV>();
        List<API_DsNV.NV> listNv = new List<API_DsNV.NV>();
        List<API_DsNV.NV> listAddNv = new List<API_DsNV.NV>();
        public DeXuat(frmMain main)
        {
            Main = main;
            InitializeComponent();
            getDsDeXuat();
            getDsNv();
            getDsToChuc();
            getDsViTri();
        }
        BrushConverter bcBody = new BrushConverter();
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
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bornv_.Background = (Brush)bcBody.ConvertFrom("#4C5DB4");
            txtNv_.Foreground = (Brush)bcBody.ConvertFrom("#FFFFFF");
            borDx_.Background = (Brush)bcBody.ConvertFrom("#E4E7FF");
            txtDx_.Foreground = (Brush)bcBody.ConvertFrom("#4C5DB4");
            stackNhanVien.Visibility = Visibility.Visible;
            stackDeXuat.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            bornv_.Background = (Brush)bcBody.ConvertFrom("#E4E7FF");
            txtNv_.Foreground = (Brush)bcBody.ConvertFrom("#4C5DB4");
            borDx_.Background = (Brush)bcBody.ConvertFrom("#4C5DB4");
            txtDx_.Foreground = (Brush)bcBody.ConvertFrom("#FFFFFF");
            stackNhanVien.Visibility = Visibility.Collapsed;
            stackDeXuat.Visibility = Visibility.Visible;
        }
        private async void getDsDeXuat(int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/settingConfirm/listSettingPropose");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("", null, "text/plain");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    // Xử lý phản hồi ở đây
                    API_DsDeXuat.DeXuat api = JsonConvert.DeserializeObject<API_DsDeXuat.DeXuat>(responseContent);
                    if (api.data.data != null)
                    {
                        listLoaiDx = api.data.data;
                        lsvDx.ItemsSource = listLoaiDx;
                        listLoaiDx1 = api.data.data;
                        lsvDx1.ItemsSource = listLoaiDx1;
                        var list = api.data.data.Skip((pageNumber - 1) * 10).Take(10);
                        var fillNullDataList = (from item in list
                                                select new API_DsDeXuat.DsDeXuat
                                                {
                                                    id = item.id,
                                                    dexuat_name = item.dexuat_name,
                                                    confirm_level = item.confirm_level,
                                                    ht_duyet = item.ht_duyet,
                                                    confirm_type = item.confirm_type,
                                                    confirm_time = item.confirm_time
                                                }).ToList();
                        foreach (var item in fillNullDataList)
                        {
                            if (item.confirm_type == 1)
                            {
                                item.ht_duyet = "Duyệt lần lượt";
                            }
                            else if (item.confirm_type == 2)
                            {
                                item.ht_duyet = "Duyệt đồng thời";
                            }
                            else if (item.confirm_type == 3)
                            {
                                item.ht_duyet = "Duyệt lần lượt và đồng thời";
                            }

                        }
                        if (paginNV.SelectedPage == 0) paginNV.TotalRecords = (int)api.data.total;

                        dgvDeXuat.ItemsSource = fillNullDataList;

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        private async void getDsNv(int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/settingConfirm/listUser");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\r\n    \"ep_status\":\"Active\",\r\n    \r\n    \"pageSize\":10000,\r\n    \"com_id\":" + Main.IdAcount.ToString() + "\r\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {

                    // Xử lý phản hồi ở đây
                    API_DsNV.Ds_NV api = JsonConvert.DeserializeObject<API_DsNV.Ds_NV>(responseContent);
                    if (api.data.data != null)
                    {
                        listAllNv = api.data.data;
                        lsvName.ItemsSource = listAllNv;
                        listNv = api.data.data;
                        dgvNV.ItemsSource = listNv;
                        var list = api.data.data.Skip((pageNumber - 1) * 10).Take(10);
                        var fillNullDataList = (from item in list
                                                select new API_DsNV.NV
                                                {
                                                    ep_id = item.ep_id,
                                                    userName = item.userName,
                                                    avatarUser = (item.avatarUser == "" || item.avatarUser == null) ? "https://tinhluong.timviec365.vn/img/add.png" : "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser,
                                                    organizeDetailName = item.organizeDetailName == "" ? "Chưa cập nhật" : item.organizeDetailName,
                                                    positionName = item.positionName == "" ? "Chưa cập nhật" : item.positionName

                                                }).ToList();

                        if (paginNV1.SelectedPage == 0) paginNV1.TotalRecords = dgvNV.Items.Count;

                        dgvNV.ItemsSource = fillNullDataList;

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getDsDeXuat(paginNV.SelectedPage);

        }
        private void paginNV1_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getDsNv(paginNV1.SelectedPage);

        }
        string id;
        string type;
        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            edit_dexuat.Visibility = Visibility.Visible;
            main_dexuat.Visibility = Visibility.Visible;
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                TextBlock stt = FindChild<TextBlock>(row, "stt");
                if (stt != null)
                {
                    id = stt.Text;
                }
                TextBlock loaiDx = FindChild<TextBlock>(row, "loaiDx");
                if (loaiDx != null)
                {
                    txtDon.Text = loaiDx.Text;
                }
                TextBlock SoCapDx = FindChild<TextBlock>(row, "SoCapDx");
                if (SoCapDx != null)
                {
                    textSoCap.Text = SoCapDx.Text;
                }
                TextBlock HinhThucDx = FindChild<TextBlock>(row, "HinhThucDx");
                if (HinhThucDx != null)
                {
                    if (HinhThucDx.Text == "Duyệt lần lượt")
                    {
                        ComboBox.SelectedIndex = 0;
                    }
                    else if (HinhThucDx.Text == "Duyệt đồng thời")
                    {
                        ComboBox.SelectedIndex = 1;
                    }
                    else if (HinhThucDx.Text == "Duyệt lần lượt và đồng thời")
                    {
                        ComboBox.SelectedIndex = 2;
                    }

                }
                TextBlock TimeDx = FindChild<TextBlock>(row, "TimeDx");
                if (TimeDx != null)
                {
                    textTime.Text = TimeDx.Text;
                }
            }

        }
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
        #region
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindChild<T>(child, childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
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
        #endregion
        private void getInforEdit()
        {



        }
        private void Border_MouseLeftutton(object sender, MouseButtonEventArgs e)
        {
            edit_dexuat.Visibility = Visibility.Collapsed;
            //main_dexuat.Visibility = Visibility.Visible;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            edit_dexuat.Visibility = Visibility.Collapsed;
        }

        private void huy(object sender, MouseButtonEventArgs e)
        {
            edit_dexuat.Visibility = Visibility.Collapsed;
        }

        private async void Luu(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/settingPropose");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(id), "list_SettingPropose");
                content.Add(new StringContent(textSoCap.Text), "confirm_level");

                if (ComboBox.SelectedIndex == 0)
                {
                    type = "1";
                }
                else if (ComboBox.SelectedIndex == 1)
                {
                    type = "2";
                }
                else if (ComboBox.SelectedIndex == 2)
                {
                    type = "3";
                }
                content.Add(new StringContent(type), "confirm_type");
                content.Add(new StringContent(textTime.Text), "confirm_time");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    edit_dexuat.Visibility = Visibility.Collapsed;
                    ThanhCong uc = new ThanhCong(Main);
                    Main.pnlShowPopUp.Children.Add(uc);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.pnlShowPopUp.Children.Add(Content as UIElement);
                    getDsDeXuat();
                    dgvDeXuat.Items.Refresh();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void grNgD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Dxuat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double height = (sender as Grid).ActualHeight;
            double margi = 100 + height;
            borDexuat.Margin = new Thickness(0, margi, 0, 0);

            if (borDexuat.Visibility == Visibility.Collapsed)
            {
                borDexuat.Visibility = Visibility.Visible;
            }
            else
            {
                borDexuat.Visibility = Visibility.Collapsed;
            }
        }

        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            if (stackDeXuat.Visibility == Visibility.Visible)
            {
                caidatDx.Visibility = Visibility.Visible;
            }
            else if (stackNhanVien.Visibility == Visibility.Visible)
            {
                caidatNV.Visibility = Visibility.Visible;
            }

        }

        private void Rectangle_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            borDexuat.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            caidatDx.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void lsvDx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvDx.SelectedItem != null)
            {
                borDexuat.Visibility = Visibility.Collapsed;
                string selectedUserName = ((API_DsDeXuat.DsDeXuat)lsvDx.SelectedItem).dexuat_name;
                if (!listAddLoaiDx.Any(item => item.dexuat_name == selectedUserName))
                {
                    API_DsDeXuat.DsDeXuat infor = new API_DsDeXuat.DsDeXuat()
                    {
                        dexuat_name = ((API_DsDeXuat.DsDeXuat)lsvDx.SelectedItem).dexuat_name,
                        id = ((API_DsDeXuat.DsDeXuat)lsvDx.SelectedItem).id,
                        ht_duyet = ((API_DsDeXuat.DsDeXuat)lsvDx.SelectedItem).ht_duyet,
                        confirm_time = ((API_DsDeXuat.DsDeXuat)lsvDx.SelectedItem).confirm_time
                    };

                    listAddLoaiDx.Add(infor);
                    listAddLoaiDx = listAddLoaiDx.ToList();
                    if (listAddLoaiDx.Count > 0)
                    {
                        textDx.Text = "";
                        //  grNgD.Height = 45;
                    }

                    lsvDexuat.ItemsSource = listAddLoaiDx;
                    lsvDexuat.Visibility = Visibility.Visible;
                }

            }
            lsvDexuat.Items.Refresh();
            if (lsvDexuat.Items.Count > 0)
            {
                textChonDx.Text = "";
                textChonDx.IsReadOnly = false;
                textChonDx.Focus();
            }
            borDexuat.Visibility = Visibility.Collapsed;
        }

        private void xoaNg_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((System.Windows.Controls.Border)sender).Background = redBrush;
        }

        private void xoaNg_MouseLeave(object sender, MouseEventArgs e)
        {
            borDexuat.Visibility = Visibility.Collapsed;
            textChonDx.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((System.Windows.Controls.Border)sender).Background = grayBrush;
            if (listAddLoaiDx.Count == 0)
            {
                borDexuat.Visibility = Visibility.Collapsed;
                textChonDx.Focus();
            }
            if (listAddLoaiDx.Count == 0)
            {
                textDx.Text = "Chọn";
            }
        }
        bool shouldProcessEvent = true;
        private void xoaAnh_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_DsDeXuat.DsDeXuat index = (API_DsDeXuat.DsDeXuat)lsvDexuat.SelectedItem;
            if (index != null)
            {
                listAddLoaiDx.Remove(index);
                lsvDexuat.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvDexuat.ItemsSource = listAddLoaiDx;
                shouldProcessEvent = false;
            }
            shouldProcessEvent = true;
            if (listAddLoaiDx.Count == 0)
            {
                Dxuat.Visibility = Visibility.Visible;
                lsvDexuat.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        List<API_DsDeXuat.DsDeXuat> listNull = new List<API_DsDeXuat.DsDeXuat>();
        private void textChonDx_TextChanged(object sender, TextChangedEventArgs e)
        {
            borDexuat.Visibility = Visibility.Visible;
            List<API_DsDeXuat.DsDeXuat> listDeXTimKiem = new List<API_DsDeXuat.DsDeXuat>();
            string searchText = textChonDx.Text.ToString().ToLower().RemoveUnicode();
            foreach (var str in listLoaiDx)
            {
                if (str.dexuat_name.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listDeXTimKiem.Any(item => item.dexuat_name.Equals(str.dexuat_name, StringComparison.OrdinalIgnoreCase)))
                    {
                        listDeXTimKiem.Add(str);
                    }

                }
            }
            lsvDx.ItemsSource = null;
            lsvDx.ItemsSource = listDeXTimKiem;
            //listDeXTimKiem.Clear();
            if (textChonDx.Text == "")
            {
                lsvDx.ItemsSource = listLoaiDx;
            }
        }


        private void All(object sender, RoutedEventArgs e)
        {
            // lsvDexuat.Visibility = Visibility.Visible;
            listAddLoaiDx.Clear();
            foreach (var str in listLoaiDx)
            {
                listAddLoaiDx.Add(str);
            }
            lsvDexuat.ItemsSource = listAddLoaiDx;
            lsvDexuat.Items.Refresh();
            lsvDexuat.Visibility = Visibility.Visible;
            gridDx.Visibility = Visibility.Collapsed;

            textChonDx.IsReadOnly = false;
            textChonDx.Focus();

        }

        private void huyy(object sender, RoutedEventArgs e)
        {
            listAddLoaiDx.Clear();
            lsvDexuat.ItemsSource = listAddLoaiDx;
            gridDx.Visibility = Visibility.Visible;
            textDx.Text = "Chọn";
            lsvDexuat.ItemsSource = listNull;
            // lsvDexuat.Visibility = Visibility.Visible;
        }

        private void Border_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
        {
            caidatDx.Visibility = Visibility.Collapsed;
        }

        private async void Border_MouseLeftButtonUp_6(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/settingPropose");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);


                string type_Cd = "";
                if (ComboBox1.SelectedIndex == 0)
                {
                    type_Cd = "1";
                }
                else if (ComboBox1.SelectedIndex == 1)
                {
                    type_Cd = "2";
                }
                else if (ComboBox1.SelectedIndex == 2)
                {
                    type_Cd = "3";
                }
                List<string> d = new List<string>();

                foreach (var item in lsvDexuat.Items)
                {
                    if (item is API_DsDeXuat.DsDeXuat dx)
                    {

                        d.Add(((int)dx.id).ToString() + ",");
                    }
                }
                for (int i = 0; i < d.Count; i++)
                {
                    if (d[i].EndsWith(",") && i == d.Count - 1)
                    {
                        d[i] = d[i].Substring(0, d[i].Length - 1);
                    }
                    //listString = Convert.ToString(listString);
                }
                string list_Id = string.Join("", d);
                string list_Id1 = "[" + list_Id + "]";

                //int a = lsvDexuat.Items.Count;
                var content = new StringContent("{\"list_SettingPropose\":" + list_Id1 + ",\"confirm_level\":" + textSoCap1.Text + ",\"confirm_type\":" + type_Cd + ",\"confirm_time\":" + textTime1.Text + "}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    caidatDx.Visibility = Visibility.Collapsed;
                    ThanhCong uc = new ThanhCong(Main);
                    Main.pnlShowPopUp.Children.Add(uc);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.pnlShowPopUp.Children.Add(Content as UIElement);
                    getDsDeXuat();
                    dgvDeXuat.Items.Refresh();
                }

            }
            catch (Exception ex)
            {

            }

        }

        private void textNhapTenDeXuat_TextChanged(object sender, TextChangedEventArgs e)
        {
            getDsSearch(paginNV.SelectedPage);
        }
        private void getDsSearch(int pagenumber = 1)
        {
            List<API_DsDeXuat.DsDeXuat> listSearch = new List<API_DsDeXuat.DsDeXuat>();
            string searchText = textNhapTenDeXuat.Text.ToLower().RemoveUnicode();
            foreach (var str in listLoaiDx)
            {
                if (str.dexuat_name.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listSearch.Any(item => item.dexuat_name.Equals(str.dexuat_name, StringComparison.OrdinalIgnoreCase)))
                    {
                        listSearch.Add(str);
                    }

                }
            }
            foreach (var item in listSearch)
            {
                if (item.confirm_type == 1)
                {
                    item.ht_duyet = "Duyệt lần lượt";
                }
                else if (item.confirm_type == 2)
                {
                    item.ht_duyet = "Duyệt đồng thời";
                }
                else if (item.confirm_type == 3)
                {
                    item.ht_duyet = "Duyệt lần lượt và đồng thời";
                }
            }
            if (paginNV.SelectedPage == 1) paginNV.TotalRecords = listSearch.Count();
            dgvDeXuat.ItemsSource = listSearch;
            dgvDeXuat.Items.Refresh();
        }

        private void Rectangle_MouseLeftuttonUp_1(object sender, MouseButtonEventArgs e)
        {
            borTOChuc1.Visibility = Visibility.Collapsed;
        }
        List<API_DsToChucc.ToChuc> listToChuc = new List<API_DsToChucc.ToChuc>();
        List<API_DsToChucc.ToChuc> listToChucNv = new List<API_DsToChucc.ToChuc>();
        private async void getDsToChuc()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/organizeDetail/listAll");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DsToChucc.DsToChuc api = JsonConvert.DeserializeObject<API_DsToChucc.DsToChuc>(responseContent);
                    if (api != null)
                    {
                        listToChuc = api.data.data;
                        lsvToChuc.ItemsSource = listToChuc;
                        listToChucNv = api.data.data;
                        lsvToChucNv.ItemsSource = listToChucNv;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void getDsNvToChuc(int pagenumber = 1)
        {

        }
        private void lsvDx_SeledctionChanged(object sender, SelectionChangedEventArgs e)
        {
            textChonToChuc.Text = "";
            borTOChuc1.Visibility = Visibility.Collapsed;
            string searchToChuc = (lsvToChuc.SelectedItem as API_DsToChucc.ToChuc)?.organizeDetailName;
            //textChonToChuc1.Visibility = Visibility.Visible;
            textChonToChuc1.Text = searchToChuc;
            List<API_DsNV.NV> listSearch = new List<API_DsNV.NV>();

            if (searchToChuc != null)
            {
                foreach (var str in listNv)
                {
                    if (str.organizeDetailName?.ToLower().RemoveUnicode() == (searchToChuc.ToLower().RemoveUnicode()))
                    {

                        listSearch.Add(str);

                    }
                }
            }
            foreach (var item in listNv)
            {
                if (item.organizeDetailName == "")
                {
                    item.organizeDetailName = "Chưa cập nhật";
                }
                else
                {
                    item.organizeDetailName = item.organizeDetailName;
                }
            }
            foreach (var item in listNv)
            {
                //if (item.avatarUser.ToString() == "" || item.avatarUser == null)
                //{
                //    item.avatarUser = "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser;
                //}
                //else
                //{

                //    item.avatarUser = "https://tinhluong.timviec365.vn/img/add.png";
                //}
            }
            foreach (var item in listNv)
            {
                if (item.positionName == "")
                {
                    item.positionName = "Chưa cập nhật";
                }
                else
                {
                    item.positionName = item.positionName;
                }
            }
            if (paginNV1.SelectedPage == 1) paginNV1.TotalRecords = listSearch.Count();
            dgvNV.ItemsSource = listSearch;
            dgvNV.Items.Refresh();

        }
        List<API_DsViTrii.ViTri> listViTri = new List<API_DsViTrii.ViTri>();
        private async void getDsViTri()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/positions/listAll");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DsViTrii.DsViTri api = JsonConvert.DeserializeObject<API_DsViTrii.DsViTri>(responseContent);
                    if (api != null)
                    {
                        listViTri = api.data.data;
                        lsvViTri.ItemsSource = listViTri;
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Rectangle_MouseLeftuattonUp_1(object sender, MouseButtonEventArgs e)
        {
            borVitri1.Visibility = Visibility.Collapsed;
        }

        private void lsvDx_SeledctaionChanged(object sender, SelectionChangedEventArgs e)
        {
            textChonViTri.Text = "";
            borVitri1.Visibility = Visibility.Collapsed;
            string searchToChuc = (lsvViTri.SelectedItem as API_DsViTrii.ViTri)?.positionName;
            //textChonToChuc1.Visibility = Visibility.Visible;
            string textToChuc = textChonToChuc1.Text;
            textChonViTri1.Text = searchToChuc;
            List<API_DsNV.NV> listSearch = new List<API_DsNV.NV>();
            if (searchToChuc != null)
            {
                foreach (var str in listNv)
                {
                    if (str.positionName?.ToLower().RemoveUnicode() == (searchToChuc.ToLower().RemoveUnicode()) && str.organizeDetailName == textToChuc)
                    {

                        listSearch.Add(str);

                    }
                }
            }
            foreach (var item in listNv)
            {
                if (item.organizeDetailName == "")
                {
                    item.organizeDetailName = "Chưa cập nhật";
                }
                else
                {
                    item.organizeDetailName = item.organizeDetailName;
                }
            }
            foreach (var item in listNv)
            {
                //if (item.avatarUser.ToString() == "" || item.avatarUser == null)
                //{
                //    item.avatarUser = "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser;
                //}
                //else
                //{

                //    item.avatarUser = "https://tinhluong.timviec365.vn/img/add.png";
                //}
            }
            foreach (var item in listNv)
            {
                if (item.positionName == "")
                {
                    item.positionName = "Chưa cập nhật";
                }
                else
                {
                    item.positionName = item.positionName;
                }
            }
            if (paginNV1.SelectedPage == 1) paginNV1.TotalRecords = listSearch.Count();
            dgvNV.ItemsSource = listSearch;
            dgvNV.Items.Refresh();
        }

        private void TToChuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borTOChuc1.Visibility == Visibility.Collapsed)
            {
                borTOChuc1.Visibility = Visibility.Visible;
            }
            else
            {
                borTOChuc1.Visibility = Visibility.Collapsed;
            }
        }

        private void ViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            double width = (sender as System.Windows.Controls.Border).ActualWidth;
            //double margi = 100 + height;
            borDexuat.Margin = new Thickness(width, 0, 0, 0);
            if (borVitri1.Visibility == Visibility.Collapsed)
            {
                borVitri1.Visibility = Visibility.Visible;
            }
            else
            {
                borVitri1.Visibility = Visibility.Collapsed;
            }
        }

        private void Rectangle_MouseLeftduattonUp_1(object sender, MouseButtonEventArgs e)
        {
            borName1.Visibility = Visibility.Collapsed;
        }

        private void lsvDx_SelaedctaionChanged(object sender, SelectionChangedEventArgs e)
        {
            textName.Text = "";
            borName1.Visibility = Visibility.Collapsed;
            string searchName = (lsvName.SelectedItem as API_DsNV.NV)?.userName;
            string textToChuc = textChonToChuc1.Text;
            string textViTri = textChonViTri1.Text;
            //textChonToChuc1.Visibility = Visibility.Visible;
            textName1.Text = searchName;
            List<API_DsNV.NV> listSearch = new List<API_DsNV.NV>();
            if (searchName != null)
            {
                foreach (var str in listNv)
                {
                    if (str.userName?.ToLower().RemoveUnicode() == (searchName.ToLower().RemoveUnicode()) && str.organizeDetailName == textToChuc && str.positionName == textViTri)
                    {

                        listSearch.Add(str);

                    }
                }
            }
            foreach (var item in listNv)
            {
                if (item.organizeDetailName == "")
                {
                    item.organizeDetailName = "Chưa cập nhật";
                }
                else
                {
                    item.organizeDetailName = item.organizeDetailName;
                }
            }
            foreach (var item in listNv)
            {
                //if (item.avatarUser.ToString() == "" || item.avatarUser == null)
                //{
                //    item.avatarUser = "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser;
                //}
                //else
                //{

                //    item.avatarUser = "https://tinhluong.timviec365.vn/img/add.png";
                //}
            }
            foreach (var item in listNv)
            {
                if (item.positionName == "")
                {
                    item.positionName = "Chưa cập nhật";
                }
                else
                {
                    item.positionName = item.positionName;
                }
            }
            if (paginNV1.SelectedPage == 1) paginNV1.TotalRecords = listSearch.Count();
            dgvNV.ItemsSource = listSearch;
            dgvNV.Items.Refresh();
        }

        private void ViTri_MaouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //double width = (sender as System.Windows.Controls.Border).ActualWidth;
            ////double margi = 100 + height;
            //double mar_top = 195;
            //borName1.Margin = new Thickness(0, mar_top, width, 0);
            if (borName1.Visibility == Visibility.Collapsed)
            {
                borName1.Visibility = Visibility.Visible;
            }
            else
            {
                borName1.Visibility = Visibility.Collapsed;
            }
        }

        private void textChonToChuc_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<API_DsToChucc.ToChuc> listSearchTochuc = new List<API_DsToChucc.ToChuc>();
            textChonToChuc1.Text = "";
            textChonToChuc.Focus();
            borTOChuc1.Visibility = Visibility.Visible;
            string searchText = textChonToChuc.Text.ToLower().RemoveUnicode();
            foreach (var str in listToChuc)
            {
                if (str.organizeDetailName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listSearchTochuc.Any(item => item.organizeDetailName.Equals(str.organizeDetailName, StringComparison.OrdinalIgnoreCase)))
                    {
                        listSearchTochuc.Add(str);
                    }
                    //listSearchTochuc.Add(str);
                }
            }
            if (searchText == "")
            {
                listSearchTochuc = listToChuc;
            }
            lsvToChuc.ItemsSource = listSearchTochuc;
            lsvToChuc.Items.Refresh();
        }
        //search vi tri
        private void textChonViTri_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<API_DsViTrii.ViTri> listSearchTochuc = new List<API_DsViTrii.ViTri>();
            textChonViTri1.Text = "";
            textChonViTri.Focus();
            borVitri1.Visibility = Visibility.Visible;
            string searchText = textChonViTri.Text.ToLower().RemoveUnicode();
            foreach (var str in listViTri)
            {
                if (str.positionName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listSearchTochuc.Any(item => item.positionName.Equals(str.positionName, StringComparison.OrdinalIgnoreCase)))
                    {
                        listSearchTochuc.Add(str);
                    }
                    //listSearchTochuc.Add(str);
                }
            }
            if (searchText == "")
            {
                listSearchTochuc = listViTri;
            }
            lsvViTri.ItemsSource = listSearchTochuc;
            lsvViTri.Items.Refresh();
        }

        private void textName_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<API_DsNV.NV> listSearchName = new List<API_DsNV.NV>();
            textName1.Text = "";
            textName.Focus();
            borName1.Visibility = Visibility.Visible;
            string searchText = textName.Text.ToLower().RemoveUnicode();
            foreach (var str in listNv)
            {
                if (str.userName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listSearchName.Any(item => item.userName.Equals(str.userName, StringComparison.OrdinalIgnoreCase)))
                    {
                        listSearchName.Add(str);
                    }
                    //listSearchName.Add(str);
                }
            }
            if (searchText == "")
            {
                listSearchName = listNv;
            }
            lsvName.ItemsSource = listSearchName;
            lsvName.Items.Refresh();
        }
        //xem chi tiêt
        private void borChiTiet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            chitietdexuat.Visibility = Visibility.Visible;
            API_DsNV.NV d = ((API_DsNV.NV)dgvNV.SelectedItem) as API_DsNV.NV;
            if (d != null)
            {
                ep_idUser = (int)d.ep_id;
            }
            getDsDetailUser();
        }

        private void Rectangle_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            chitietdexuat.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_7(object sender, MouseButtonEventArgs e)
        {
            chitietdexuat.Visibility = Visibility.Collapsed;
        }
        int ep_idUser;
        List<API_DetailUser.Detail> listDetailUser = new List<API_DetailUser.Detail>();
        private async void getDsDetailUser(int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/detailUser");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                //request.Headers.Add("content-type", "application/json");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ep_idUser.ToString()), "ep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DetailUser.DetailUser api = JsonConvert.DeserializeObject<API_DetailUser.DetailUser>(responseContent);
                    if (api != null)
                    {
                        listDetailUser = api.data.data;
                        dgvDetailDeXuat.ItemsSource = listDetailUser;
                        var list = api.data.data.Skip((pageNumber - 1) * 10).Take(10);
                        //var fillNullDataList = (from item in list
                        //                        select new API_DetailUser.Detail
                        //                        {
                        //                            dexuat_name = item.dexuat_name,

                        //                        }).ToList();
                        foreach (var item in list)
                        {
                            if (item.confirm_type == 1)
                            {
                                item.ht_duyet = "Duyệt lần lượt";
                            }
                            else if (item.confirm_type == 2)
                            {
                                item.ht_duyet = "Duyệt đồng thời";
                            }
                            else if (item.confirm_type == 3)
                            {
                                item.ht_duyet = "Duyệt lần lượt và đồng thời";
                            }
                        }
                        if (paginDetailUser.SelectedPage == 0) paginDetailUser.TotalRecords = dgvDetailDeXuat.Items.Count;

                        dgvDetailDeXuat.ItemsSource = list;


                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void DetailUser_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getDsDetailUser(paginDetailUser.SelectedPage);

        }
        string id_donDetail;
        string id_UserDetail;
        private void Border_MouseLeftButtonUp_8(object sender, MouseButtonEventArgs e)
        {
            chitietdexuat.Visibility = Visibility.Collapsed;
            edit_detailUser.Visibility = Visibility.Visible;
            API_DetailUser.Detail d = ((API_DetailUser.Detail)dgvDetailDeXuat.SelectedItem) as API_DetailUser.Detail;
            if (d != null)
            {
                id_donDetail = d.id.ToString();
                //id_UserDetail = d.dexuat_id.ToString();
                txtDonDetail.Text = d.dexuat_name.ToString();
                textSoCap11.Text = d.confirm_level.ToString();
                if (d.ht_duyet.ToString() == "Duyệt lần lượt")
                {
                    ComboBox22.SelectedIndex = 0;
                }
                else if (d.ht_duyet.ToString() == "Duyệt đồng thời")
                {
                    ComboBox22.SelectedIndex = 1;
                }
                else if (d.ht_duyet.ToString() == "Duyệt lần lượt và đồng thời")
                {
                    ComboBox22.SelectedIndex = 2;
                }
                textTime11.Text = d.confirm_time.ToString();
            }
        }

        private void Border_MouseLeButtonUp_7(object sender, MouseButtonEventArgs e)
        {
            chitietdexuat.Visibility = Visibility.Visible;
            edit_detailUser.Visibility = Visibility.Collapsed;

        }

        private void Border_MouseLeftButtonUp_9(object sender, MouseButtonEventArgs e)
        {
            chitietdexuat.Visibility = Visibility.Visible;
            edit_detailUser.Visibility = Visibility.Collapsed;
        }

        private async void UpdateSoCap()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/updatePrivateLevel");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                string listUser = "[" + ep_idUser.ToString() + "]";
                string listConfirm = "[" + id_donDetail.ToString() + "]";
                var content = new StringContent("{\"confirm_level\":" + textSoCap11.Text + ",\"listConfirm\":" + listConfirm + ",\"listUsers\":" + listUser + "}", null, "application/json");
                //var content = new StringContent("{\"confirm_level\":56,\"listConfirm\":[1],\"listUsers\":[10001135]}", null, "application/json");
                // var content = new StringContent("{\"confirm_level\":13,\"listConfirm\":[1],\"listUsers\":[10001135]}", null, "text/plain");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
        }
        private async void UpdateHinhThuc()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/updatePrivateType");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                string listUser = "[" + ep_idUser.ToString() + "]";
                string listConfirm = "[" + id_donDetail.ToString() + "]";
                string type_Cd = "";
                if (ComboBox22.SelectedIndex == 0)
                {
                    type_Cd = "1";
                }
                else if (ComboBox22.SelectedIndex == 1)
                {
                    type_Cd = "2";
                }
                else if (ComboBox22.SelectedIndex == 2)
                {
                    type_Cd = "3";
                }
                var content = new StringContent("{\"confirm_type\":" + type_Cd + ",\"listConfirm\":" + listConfirm + ",\"listUsers\":" + listUser + "}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
        }
        private async void UpdateTime()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/updatePrivateTime");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                string listUser = "[" + ep_idUser.ToString() + "]";
                string listConfirm = "[" + id_donDetail.ToString() + "]";
                var content = new StringContent("{\"confirm_time\":" + textTime11.Text + ",\"listConfirm\":" + listConfirm + ",\"listUsers\":" + listUser + "}", null, "application/json");
                //var content = new StringContent("{\"confirm_level\":56,\"listConfirm\":[1],\"listUsers\":[10001135]}", null, "application/json");
                // var content = new StringContent("{\"confirm_level\":13,\"listConfirm\":[1],\"listUsers\":[10001135]}", null, "text/plain");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
        }
        private void Border_MouseLeftButtonUp_10(object sender, MouseButtonEventArgs e)
        {
            try
            {
                UpdateSoCap();
                UpdateHinhThuc();
                UpdateTime();
                chitietdexuat.Visibility = Visibility.Visible;
                edit_detailUser.Visibility = Visibility.Collapsed;

                ThanhCong uc = new ThanhCong(Main);
                Main.pnlShowPopUp.Children.Add(uc);
                object Content = uc.Content;
                uc.Content = null;
                Main.pnlShowPopUp.Children.Add(Content as UIElement);
                getDsDetailUser();
                dgvDetailDeXuat.Items.Refresh();
            }

            catch (Exception ex)
            {

            }
        }

        private void Rectangle_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            caidatNV.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_11(object sender, MouseButtonEventArgs e)
        {
            caidatNV.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouserssLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BorNv.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_12(object sender, MouseButtonEventArgs e)
        {
            if (BorNv.Visibility == Visibility.Collapsed)
            {
                BorNv.Visibility = Visibility.Visible;
            }
            else
            {
                BorNv.Visibility = Visibility.Collapsed;
            }
        }

        private void chonNV_TextChanged(object sender, TextChangedEventArgs e)
        {
            BorNv.Visibility = Visibility.Visible;
            List<API_DsToChucc.ToChuc> listToChucNvSearch = new List<API_DsToChucc.ToChuc>();

            string searchText = chonNV.Text.ToLower().RemoveUnicode();
            foreach (var str in listToChucNv)
            {
                if (str.organizeDetailName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listToChucNvSearch.Any(item => item.organizeDetailName.Equals(str.organizeDetailName, StringComparison.OrdinalIgnoreCase)))
                    {
                        listToChucNvSearch.Add(str);
                    }

                }
            }
            if (chonNV.Text == "")
            {
                listToChucNvSearch = listToChucNv;
            }
            lsvToChucNv.ItemsSource = listToChucNvSearch;
            lsvToChucNv.Items.Refresh();
        }

        private void chonNV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (BorNv.Visibility == Visibility.Collapsed)
            {
                BorNv.Visibility = Visibility.Visible;
            }
            else
            {
                BorNv.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvToChucNv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNv.ScrollToVerticalOffset(scrollNv.VerticalOffset - e.Delta);
        }
        List<API_DsToChucc.ListOrganizeDetailId> listOrDetailId = new List<API_DsToChucc.ListOrganizeDetailId>();
        private void lsvToChucNv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            API_DsToChucc.ToChuc d = (API_DsToChucc.ToChuc)lsvToChucNv.SelectedItem as API_DsToChucc.ToChuc;
            if (d != null)
            {

                chonNV1.Text = d.organizeDetailName;
                listOrDetailId = d.listOrganizeDetailId;
                foreach (var item in listOrDetailId)
                {
                    level = item.level.ToString();
                    organizeDetailId = item.organizeDetailId.ToString();
                }
            }

            //  if (pagiCaiDatNv.SelectedPage == 1) pagiCaiDatNv.TotalRecords = lsvToChucNv.Count();
            pagiCaiDatNv.SelectedPage = 0;
            getDsNvCaiDat();
            //chonNV.Clear();
            chonNV.Focus();
            BorNv.Visibility = Visibility.Collapsed;
        }
        string level;
        string organizeDetailId;
        List<API_DsNV.NV> listNvCaiDat = new List<API_DsNV.NV>();
        private async void getDsNvCaiDat(int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/listUser");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\"ep_status\":\"Active\",\"listOrganizeDetailId\":[{\"level\":" + level + ",\"organizeDetailId\":" + organizeDetailId + "}]}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DsNV.Ds_NV api = JsonConvert.DeserializeObject<API_DsNV.Ds_NV>(responseContent);
                    if (api != null)
                    {
                        listNvCaiDat = api.data.data;
                        //dgvCaiDatNv.ItemsSource = api.data.data;
                        //var list = api.data.data.Skip((pageNumber - 1) * 10).Take(10);
                        dgvCaiDatNv.ItemsSource = api.data.data;
                        var list = api.data.data.Skip((pageNumber - 1) * 10).Take(10);
                        foreach (var item in list)
                        {
                            if (item.positionName == "")
                            {
                                item.positionName = "Chưa cập nhật";
                            }
                            else
                            {
                                item.positionName = item.positionName;
                            }
                        }
                        foreach (var item in list)
                        {
                            if (item.organizeDetailName == "")
                            {
                                item.organizeDetailName = "Chưa cập nhật";
                            }
                            else
                            {
                                item.organizeDetailName = item.organizeDetailName;
                            }
                        }
                        if (pagiCaiDatNv.SelectedPage == 0)
                            pagiCaiDatNv.TotalRecords = dgvCaiDatNv.Items.Count;

                        dgvCaiDatNv.ItemsSource = list;
                        //dgvCaiDatNv.Items.Refresh();
                    }
                }
            }
            catch (Exception ex)
            {

            }

        }
        private void pagiCaiDatNv_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getDsNvCaiDat(pagiCaiDatNv.SelectedPage);

        }

        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            //CheckBox c = (sender as CheckBox).DataContext as API_DsNV.NV ;
            //   (((CheckBox)sender).DataContext as API_DsNV.NV).isChecked = true;
            listString.Clear();
            foreach (var item in listNvCaiDat)
            {
                item.isChecked = true;
                listString.Add(item.ep_id.ToString());
            }
            daChon.Text = listNvCaiDat.Count().ToString();
            dgvCaiDatNv.Items.Refresh();
            borTiepTuc.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            listString.Clear();
            foreach (var item in listNvCaiDat)
            {
                item.isChecked = false;
            }
            daChon.Text = "0";
            dgvCaiDatNv.Items.Refresh();
            borTiepTuc.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a9a9a9"));
        }
        int id_caidatNv = 0;
        string idAll_CaiDat = "";
        List<string> listString = new List<string>();
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            (((CheckBox)sender).DataContext as API_DsNV.NV).isChecked = true;
            id_caidatNv = (int)(((CheckBox)sender).DataContext as API_DsNV.NV).ep_id;

            listString.Add(id_caidatNv + ",");

            int count1 = 0;
            foreach (var item in listNvCaiDat)
            {
                if (item.isChecked == true)
                {
                    count1++;
                }
            }
            daChon.Text = count1.ToString();
            borTiepTuc.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            (((CheckBox)sender).DataContext as API_DsNV.NV).isChecked = false;
            int count1 = 0;
            foreach (var item in listNvCaiDat)
            {
                if (item.isChecked == true)
                {
                    count1++;
                    borTiepTuc.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
                }
            }
            daChon.Text = count1.ToString();
            int count = 0;
            foreach (var item in listNvCaiDat)
            {
                if (item.isChecked == false)
                {
                    count++;
                }
            }
            if (count == listNvCaiDat.Count())
            {
                borTiepTuc.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a9a9a9"));
            }
        }

        private void borTiepTuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borTiepTuc.Background is SolidColorBrush solidColorBrush &&
    solidColorBrush.Color.Equals((Color)ColorConverter.ConvertFromString("#4C5BD4")))
            {
                caidatnv2.Visibility = Visibility.Visible;
                caidatNV.Visibility = Visibility.Collapsed;
            }
        }

        private void Rectangle_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            caidatnv2.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftaqButtonUp_11(object sender, MouseButtonEventArgs e)
        {
            caidatnv2.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_13(object sender, MouseButtonEventArgs e)
        {
            caidatNV.Visibility = Visibility.Visible;
            caidatnv2.Visibility = Visibility.Collapsed;
        }
        //hoàn thành
        private async void UpdateSoCap1()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/updatePrivateLevel");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                // string listUser = "[" + ep_idUser.ToString() + "]";
                //string listConfirm = "[" + id_donDetail.ToString() + "]";
                var content = new StringContent("{\"confirm_level\":" + textSoCap31.Text + ",\"listConfirm\":" + id_confirm + ",\"listUsers\":" + idAll_CaiDat + "}", null, "application/json");
                //var content = new StringContent("{\"confirm_level\":56,\"listConfirm\":[1],\"listUsers\":[10001135]}", null, "application/json");
                // var content = new StringContent("{\"confirm_level\":13,\"listConfirm\":[1],\"listUsers\":[10001135]}", null, "text/plain");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
        }
        private async void UpdateHinhThuc1()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/updatePrivateType");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                //string listUser = "[" + ep_idUser.ToString() + "]";
                //string listConfirm = "[" + id_donDetail.ToString() + "]";
                string type_Cd = "";
                if (ComboBox32.SelectedIndex == 0)
                {
                    type_Cd = "1";
                }
                else if (ComboBox32.SelectedIndex == 1)
                {
                    type_Cd = "2";
                }
                else if (ComboBox32.SelectedIndex == 2)
                {
                    type_Cd = "3";
                }
                var content = new StringContent("{\"confirm_type\":" + type_Cd + ",\"listConfirm\":" + id_confirm + ",\"listUsers\":" + idAll_CaiDat + "}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
        }
        private async void UpdateTime1()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingConfirm/updatePrivateTime");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                //string listUser = "[" + ep_idUser.ToString() + "]";
                //string listConfirm = "[" + id_donDetail.ToString() + "]";
                var content = new StringContent("{\"confirm_time\":" + textTime31.Text + ",\"listConfirm\":" + id_confirm + ",\"listUsers\":" + idAll_CaiDat + "}", null, "application/json");
                //var content = new StringContent("{\"confirm_level\":56,\"listConfirm\":[1],\"listUsers\":[10001135]}", null, "application/json");
                // var content = new StringContent("{\"confirm_level\":13,\"listConfirm\":[1],\"listUsers\":[10001135]}", null, "text/plain");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
            }
            catch (Exception ex)
            {

            }
        }
        //string idAll_CaiDat = ""
        string id_confirm = "";
        private void Border_MouseLeftButtonUp_14(object sender, MouseButtonEventArgs e)
        {
            try
            {
                for (int i = 0; i < listString.Count; i++)
                {
                    if (listString[i].EndsWith(",") && i == listString.Count - 1)
                    {
                        listString[i] = listString[i].Substring(0, listString[i].Length - 1);
                    }
                }
                idAll_CaiDat = "[" + string.Join("", listString) + "]";
                //CustomMessageBox.Show(idAll_CaiDat);
                List<string> d = new List<string>();
                foreach (var item in lsvDexuat1.Items)
                {
                    if (item is API_DsDeXuat.DsDeXuat dx)
                    {

                        d.Add(((int)dx.id).ToString() + ",");
                    }
                }
                for (int i = 0; i < d.Count; i++)
                {
                    if (d[i].EndsWith(",") && i == d.Count - 1)
                    {
                        d[i] = d[i].Substring(0, d[i].Length - 1);
                    }
                }

                id_confirm = "[" + string.Join("", d) + "]";
                if (ktd11.Visibility == Visibility.Visible)
                {
                    UpdateSoCap1();
                }
                if (ktd21.Visibility == Visibility.Visible)
                {
                    UpdateHinhThuc1();
                }
                if (ktd31.Visibility == Visibility.Visible)
                {
                    UpdateTime1();
                }
                caidatnv2.Visibility = Visibility.Collapsed;
                ThanhCong uc = new ThanhCong(Main);
                Main.pnlShowPopUp.Children.Add(uc);
                object Content = uc.Content;
                uc.Content = null;
                Main.pnlShowPopUp.Children.Add(Content as UIElement);
                getDsDetailUser();
            }
            catch (Exception ex)
            {

            }

        }

        private void ktd1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ktd1.Visibility = Visibility.Collapsed;
            ktd11.Visibility = Visibility.Visible;
            socapAn.Visibility = Visibility.Visible;
        }

        private void ktd11_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ktd1.Visibility = Visibility.Visible;
            ktd11.Visibility = Visibility.Collapsed;
            socapAn.Visibility = Visibility.Collapsed;
        }

        private void ktd2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ktd2.Visibility = Visibility.Collapsed;
            ktd21.Visibility = Visibility.Visible;
            HinhThucAn.Visibility = Visibility.Visible;
        }

        private void ktd22_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ktd2.Visibility = Visibility.Visible;
            ktd21.Visibility = Visibility.Collapsed;
            HinhThucAn.Visibility = Visibility.Collapsed;
        }

        private void ktd3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ktd3.Visibility = Visibility.Collapsed;
            ktd31.Visibility = Visibility.Visible;
            ThoiGianAn.Visibility = Visibility.Visible;
        }

        private void ktd32_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ktd3.Visibility = Visibility.Visible;
            ktd31.Visibility = Visibility.Collapsed;
            ThoiGianAn.Visibility = Visibility.Collapsed;
        }
        bool shouldProcessEvent1 = true;
        private void xoaAnh_PreviewMouseLeftButtonUp1(object sender, MouseButtonEventArgs e)
        {
            API_DsDeXuat.DsDeXuat index = (API_DsDeXuat.DsDeXuat)lsvDexuat1.SelectedItem;
            if (index != null)
            {
                listAddLoaiDx1.Remove(index);
                lsvDexuat1.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvDexuat1.ItemsSource = listAddLoaiDx1;
                shouldProcessEvent1 = false;
            }
            shouldProcessEvent1 = true;
            if (listAddLoaiDx1.Count == 0)
            {
                Dxuat1.Visibility = Visibility.Visible;
                lsvDexuat1.Visibility = Visibility.Collapsed;
                //textChonNgD.Text = "";

            }
        }

        private void xoaNg_MouseLeave1(object sender, MouseEventArgs e)
        {
            borDexuat1.Visibility = Visibility.Collapsed;
            textChonDx1.Focus();
            //xoaAnh.bac
            SolidColorBrush grayBrush = new SolidColorBrush(System.Windows.Media.Color.FromRgb(220, 220, 220)); // Màu #dcdcdc
            ((System.Windows.Controls.Border)sender).Background = grayBrush;
            if (listAddLoaiDx1.Count == 0)
            {
                borDexuat1.Visibility = Visibility.Collapsed;
                textChonDx1.Focus();
            }
            if (listAddLoaiDx1.Count == 0)
            {
                textDx1.Text = "Chọn";
            }
        }

        private void xoaNg_MouseEnter1(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((System.Windows.Controls.Border)sender).Background = redBrush;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            listAddLoaiDx1.Clear();
            foreach (var str in listLoaiDx1)
            {
                listAddLoaiDx1.Add(str);
            }
            lsvDexuat1.ItemsSource = listAddLoaiDx1;
            lsvDexuat1.Items.Refresh();
            lsvDexuat1.Visibility = Visibility.Visible;
            gridDx1.Visibility = Visibility.Collapsed;

            textChonDx1.IsReadOnly = false;
            textChonDx1.Focus();
        }

        private void CheckBox_Unchecked_1(object sender, RoutedEventArgs e)
        {
            listAddLoaiDx1.Clear();
            lsvDexuat1.ItemsSource = listAddLoaiDx1;
            gridDx1.Visibility = Visibility.Visible;
            textDx1.Text = "Chọn";
            lsvDexuat1.ItemsSource = listNull;

        }

        private void textChonDx_TextChanged1(object sender, TextChangedEventArgs e)
        {

        }

        private void Dxuat_MouseLeftButtonUp1(object sender, MouseButtonEventArgs e)
        {
            double height = (sender as Grid).ActualHeight;
            double margi = 100 + height;
            borDexuat1.Margin = new Thickness(20, margi, 20, 0);
            if (borDexuat1.Visibility == Visibility.Collapsed)
            {
                borDexuat1.Visibility = Visibility.Visible;
            }
            else
            {
                borDexuat1.Visibility = Visibility.Collapsed;
            }
        }

        private void Rectangle_MouseLeftButtonUp_21(object sender, MouseButtonEventArgs e)
        {
            borDexuat1.Visibility = Visibility.Collapsed;
        }

        private void lsvDx_SelectionChanged1(object sender, SelectionChangedEventArgs e)
        {
            if (lsvDx1.SelectedItem != null)
            {
                borDexuat1.Visibility = Visibility.Collapsed;
                string selectedUserName = ((API_DsDeXuat.DsDeXuat)lsvDx1.SelectedItem).dexuat_name;
                if (!listAddLoaiDx1.Any(item => item.dexuat_name == selectedUserName))
                {
                    API_DsDeXuat.DsDeXuat infor = new API_DsDeXuat.DsDeXuat()
                    {
                        dexuat_name = ((API_DsDeXuat.DsDeXuat)lsvDx1.SelectedItem).dexuat_name,
                        id = ((API_DsDeXuat.DsDeXuat)lsvDx1.SelectedItem).id,
                        ht_duyet = ((API_DsDeXuat.DsDeXuat)lsvDx1.SelectedItem).ht_duyet,
                        confirm_time = ((API_DsDeXuat.DsDeXuat)lsvDx1.SelectedItem).confirm_time
                    };

                    listAddLoaiDx1.Add(infor);
                    listAddLoaiDx1 = listAddLoaiDx1.ToList();
                    if (listAddLoaiDx1.Count > 0)
                    {
                        textDx1.Text = "";
                        //  grNgD.Height = 45;
                    }

                    lsvDexuat1.ItemsSource = listAddLoaiDx1;
                    lsvDexuat1.Visibility = Visibility.Visible;
                }

            }
            lsvDexuat1.Items.Refresh();
            if (lsvDexuat1.Items.Count > 0)
            {
                textChonDx1.Text = "";
                textChonDx1.IsReadOnly = false;
                textChonDx1.Focus();
            }
            borDexuat1.Visibility = Visibility.Collapsed;
        }

        private void dgvCaiDatNv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset - e.Delta);
        }

        private void dgvNV_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset - e.Delta);
        }

        private void dgvDetailDeXuat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset - e.Delta);
        }

        private void dgvDeXuat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset - e.Delta);
        }

        private void Rectangle_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
        {
            caidatDx.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp_6(object sender, MouseButtonEventArgs e)
        {
            edit_detailUser.Visibility = Visibility.Collapsed;
        }
    }
}


