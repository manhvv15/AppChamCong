using Emgu.CV.CvEnum;
using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCong365.Entities;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.APIs;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.Popup.TruocDangNhap;
using QuanLyChung365TruocDangNhap.RecommendSetting.OOP;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
using QuanLyChung365TruocDangNhap.RecommendSetting.Popup;
using QuanLyChung365TruocDangNhap.RecommendSetting.Resoucess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
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
using static QRCoder.PayloadGenerator;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;
using static QuanLyChung365TruocDangNhap.RecommendSetting.OOP.API_Time_KH;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace QuanLyChung365TruocDangNhap.RecommendSetting
{
    /// <summary>
    /// Interaction logic for ucRecommended.xaml
    /// </summary>
    public partial class ucRecommended : UserControl
    {

        frmMain Main;
        List<API_Organization.Organization_Infor> ListOrganizeData = new List<API_Organization.Organization_Infor>();
        List<API_ChucVu.ChucVu_Infor> ListChucVuData = new List<API_ChucVu.ChucVu_Infor>();
        List<API_Time_KH.TimeDx> ListDeXuatData = new List<API_Time_KH.TimeDx>();
        Dictionary<string, string> ListOrganize = new Dictionary<string, string>();
        Dictionary<string, string> ListChucVu = new Dictionary<string, string>();
        Dictionary<string, string> ListPosition = new Dictionary<string, string>();
        Dictionary<string, string> ListLoaiDeXuat = new Dictionary<string, string>();
        string com_id = "1664";
        string searchJson = "";
        public ucRecommended(frmMain frmmain)
        {
            this.DataContext = this;
            InitializeComponent();
            Main = frmmain;
            getDsNhanVien();
            getDsTimeKH();
            getListToChuc();
            getListChucVu();
            getListTimeDotXuat();
            getLoaiDeX();
        }
        private async void getListChucVu()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/positions/listAll");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("1664"), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_ChucVu.ChucVu api = JsonConvert.DeserializeObject<API_ChucVu.ChucVu>(responseContent);
                    ListChucVuData = api.data.data;
                    foreach (var item in ListChucVuData)
                    {
                        ListChucVu.Add(item.id.ToString(), item.positionName);
                    }
                    //  cbToChuc.ItemsSource = ListOrganize;
                    cbViTri.ItemsSource = ListChucVu;
                }

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("ds chuc vu" + ex.Message);
            }
        }
        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            stackTime.Visibility = Visibility.Collapsed;
            hinhthucduyet.Visibility = Visibility.Collapsed;
            //stackKeHoach.Visibility = Visibility.Collapsed;
            stackSoCapVaHinhThuc.Visibility = Visibility.Visible;
        }


        public void ChangeBorderColor(Border border)
        {
            border.BorderThickness = new Thickness(0, 0, 0, 5);
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        ///<summary>
        /// set default color menu
        /// </summary>
        public void SetDefaultMenuColor()
        {
            foreach (var child in Menu.Children)
            {
                if (child is Border)
                {
                    var border = (Border)child;
                    border.BorderThickness = new Thickness(0, 0, 0, 1);
                    border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
                    ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));

                }
            }
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            stackTime.Visibility = Visibility.Collapsed;
            hinhthucduyet.Visibility = Visibility.Visible;
            stackSoCapVaHinhThuc.Visibility = Visibility.Visible;
        }

        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            stackTime.Visibility = Visibility.Visible;
            // stackKeHoach.Visibility = Visibility.Visible;
            stackSoCapVaHinhThuc.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            borKeHoach.BorderThickness = new Thickness(0, 0, 0, 5);
            borKeHoach.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            txtKeHoach.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            borDotXuat.BorderThickness = new Thickness(0, 0, 0, 0);
            //borKeHoach.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            txtDotXuat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666"));
            stackKeHoach.Visibility = Visibility.Visible;
            stackDotXuat.Visibility = Visibility.Collapsed;
        }

        private void TextBlock_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borDotXuat.BorderThickness = new Thickness(0, 0, 0, 5);
            borDotXuat.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            txtDotXuat.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));

            borKeHoach.BorderThickness = new Thickness(0, 0, 0, 0);
            //borKeHoach.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            txtKeHoach.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#666666"));

            stackDotXuat.Visibility = Visibility.Visible;
            stackKeHoach.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            Main.pnlShowPopUp.Children.Add(new PopupThemMoi(Main));

        }

        private void UserControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //CustomMessageBox.Show("Hello");
        }
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getDsNhanVien("", paginNV.SelectedPage);

        }
        private async void getDsNhanVien(string json = "", int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/settingConfirm/listUser");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                //  request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_NV.API_Ds_NhanVien api = JsonConvert.DeserializeObject<API_NV.API_Ds_NhanVien>(responseContent);
                    if (api.data.data != null)
                    {
                        var list = api.data.data.Skip((pageNumber - 1) * 10).Take(10);
                        var fillNullDataList = (from item in list
                                                select new API_NV.NV_Infor
                                                {

                                                    ep_id = item.ep_id,
                                                    userName = item.userName,
                                                    organizeDetailName = (item.organizeDetailName == "") ? "Chưa cập nhật" : item.organizeDetailName,
                                                    positionName = (item.positionName == "") ? "Chưa cập nhật" : item.positionName,
                                                    confirm_level = (item.confirm_level == "0") ? "Chưa cập nhật cấp duyệt" : ("Cấp " + item.confirm_level + " -Cần " + item.confirm_level + " người duyệt"),


                                                }).ToList();

                        if (paginNV.SelectedPage == 0) paginNV.TotalRecords = (int)api.data.total;

                        dgvNV.ItemsSource = fillNullDataList;

                    }
                }
                //   TongSoTrang = (int)Math.Ceiling((double)TotalItem / pageSize);

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi lấy ra danh sách nhân viên " + ex.Message);
            }
        }
        ObservableCollection<Data_TimeDx> listDs = new ObservableCollection<Data_TimeDx>();
        private async void getDsTimeKH(int pagenumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/setting/fetchTimeSetting");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_Time_KH.Data_TimeDx api = JsonConvert.DeserializeObject<API_Time_KH.Data_TimeDx>(responseContent);

                    var list = api.time_dx.Skip((pagenumber - 1) * 10).Take(10);
                    var fullList = (from item in list
                                    select new TimeDx
                                    {
                                        _id = item._id,
                                        name_cate_dx = item.name_cate_dx,
                                        id_dx =item.id_dx,
                                        time = (item.time == "0") ? "Chưa cập nhật" : item.time


                                    }).ToList();

                    if (pageKH.SelectedPage == 0) pageKH.TotalRecords = (int)api.time_dx.Count();
                    dgvTimeKeHoach.ItemsSource = fullList;
                    //ListDeXuatData = api.time_dx;
                    //foreach (var item in ListDeXuatData)
                    //{
                    //    ListLoaiDeXuat.Add(item._id.ToString(), item.name_cate_dx);
                    //}
                    //cbLoaiDeXuat.ItemsSource = ListLoaiDeXuat;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi lấy danh sách thời gian có kế hoạch " + ex.Message);
            }

        }
        private async void getLoaiDeX()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/setting/fetchTimeSetting");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_Time_KH.Data_TimeDx api = JsonConvert.DeserializeObject<API_Time_KH.Data_TimeDx>(responseContent);
                    ListDeXuatData = api.time_dx;
                    foreach (var item in ListDeXuatData)
                    {
                        ListLoaiDeXuat.Add(item._id.ToString(), item.name_cate_dx);
                    }
                    cbLoaiDeXuat.ItemsSource = ListLoaiDeXuat;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi lấy loai de xuat " + ex.Message);
            }
        }
        private void pageKH_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getDsTimeKH(pageKH.SelectedPage);

        }

        private void Border_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
        {
            ////MyDataGrid_Loaded(sender, e);
            //DataGrid row = FindAncestor<DataGrid>((DependencyObject)e.OriginalSource);
            //if (row != null)
            //{
            //    Border updateTime = FindChild<Border>(row, "updateTime");
            //    updateTime.Visibility = Visibility.Visible;
            //    TextBlock text = FindChild<TextBlock>(row, "text");
            //    text.Visibility = Visibility.Collapsed;
            //}

            //UpdateTimeDuyet();

            //Update uc = new Update(Main);
            //Main.pnlShowPopUp.Children.Add(uc);
            //object Content = uc.Content;
            //uc.Content = null;
            //Main.pnlShowPopUp.Children.Add(Content as UIElement);

            //getDsTimeKH(pageKH.SelectedPage);
            popupUpdateTimeD.Visibility = Visibility.Visible;
            id_time_duyet = (int)((API_Time_KH.TimeDx)dgvTimeKeHoach.SelectedItem).id_dx;
            //Main.pnlShowPopUp.Children.Add(new ucPopupSuaTimeDuyet(Main, id_time_duyet));
        }
        int id_time_duyet;
        private async void Border_MouseLeftButtonUp_6(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/setting/updateTimeSetting");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                id_Update = ((TimeDx)dgvTimeKeHoach.SelectedItem).id_dx;
                content.Add(new StringContent(id_Update.ToString()), "id_dx");

                content.Add(new StringContent("0"), "time");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                getDsTimeKH(pageKH.SelectedPage);
            }

            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi update thời gian duyệt " + ex.Message);
            }
            Refresh uc = new Refresh(Main);
            Main.pnlShowPopUp.Children.Add(uc);
            object Content = uc.Content;
            uc.Content = null;
            Main.pnlShowPopUp.Children.Add(Content as UIElement);
        }
        int id_Update;
        private async void UpdateTimeDuyet()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/setting/updateTimeSetting");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
               id_Update = ((TimeDx)dgvTimeKeHoach.SelectedItem).id_dx;
                content.Add(new StringContent(id_Update.ToString()), "id_dx");
               
                content.Add(new StringContent(textNhap.Text), "time");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                getDsTimeKH(pageKH.SelectedPage);
            }

            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi update thời gian duyệt " + ex.Message);
            }
        }
        private TextBox FindTextBoxInVisualTree(DependencyObject parent, string name)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child is FrameworkElement frameworkElement && frameworkElement.Name == name)
                {
                    return (TextBox)frameworkElement;
                }

                TextBox childOfChild = FindTextBoxInVisualTree(child, name);

                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }

        private void MyDataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid != null)
            {
                dataGrid.PreviewMouseLeftButtonDown += DataGrid_PreviewMouseLeftButtonDown;
            }
        }
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid != null)
            {
                DependencyObject dep = (DependencyObject)e.OriginalSource;

                while ((dep != null) && !(dep is DataGridCell) && !(dep is DataGridColumnHeader))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }

                if (dep == null)
                {
                    return;
                }

                if (dep is DataGridCell)
                {
                    DataGridCell cell = dep as DataGridCell;
                    if (cell.Column.Header.ToString() == "Thời gian")
                    {
                        // Tìm Border trong DataTemplate
                        Border updateTimeBorder = FindVisualChild<Border>(cell);

                        if (updateTimeBorder != null)
                        {
                            updateTimeBorder.Visibility = Visibility.Visible;
                        }
                    }
                }
            }
        }
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);

                if (child != null && child is T)
                {
                    return (T)child;
                }

                T childOfChild = FindVisualChild<T>(child);

                if (childOfChild != null)
                {
                    return childOfChild;
                }
            }

            return null;
        }

        private void cbToChuc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

            cbToChuc.SelectedIndex = -1;
            string textSearch = cbToChuc.Text + e.Text;
            cbToChuc.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbToChuc.Text = "";
                cbToChuc.Items.Refresh();
                cbToChuc.ItemsSource = ListOrganize;
                cbToChuc.SelectedIndex = -1;
            }
            else
            {
                cbToChuc.ItemsSource = "";
                cbToChuc.Items.Refresh();
                cbToChuc.ItemsSource = ListOrganize.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbToChuc_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Back)
            {
                cbToChuc.SelectedIndex = -1;
                string textSearch = cbToChuc.Text;
                cbToChuc.Items.Refresh();
                cbToChuc.ItemsSource = ListOrganize.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));

            }
        }

        private void cbToChuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string textSearch = cbToChuc.Text;
            cbToChuc.Items.Refresh();
            cbToChuc.ItemsSource = ListOrganize.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
        }
        private async void getListToChuc()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/organizeDetail/listAll");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    API_Organization.Organization result = JsonConvert.DeserializeObject<API_Organization.Organization>(responseContent);
                    ListOrganizeData = result.data.data;
                    foreach (var item in ListOrganizeData)
                    {
                        ListOrganize.Add(item.id.ToString(), item.organizeDetailName);
                    }
                    cbToChuc.ItemsSource = ListOrganize;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi lấy ra danh sách organization " + ex.Message);
            }
        }
        public class SearchObject
        {
            public string ep_status { get; set; }
            public int? confirm_type { get; set; }
            public int? confirm_level { get; set; }
            public List<API_Organization.ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public int? position_id { get; set; }
            public string userName { get; set; }
            public int? pageNumber { get; set; }
            public int? pageSize { get; set; }
        }
        private void createSearchString(int pageNumber = 1)
        {
            try
            {
                List<API_Organization.ListOrganizeDetailId> listOrganizeDetailId = new List<API_Organization.ListOrganizeDetailId>();

                if (cbToChuc.SelectedIndex != -1)
                {
                    listOrganizeDetailId = new List<API_Organization.ListOrganizeDetailId>();
                    listOrganizeDetailId = ListOrganizeData.Where(x => x.id == int.Parse(cbToChuc.SelectedValue.ToString())).FirstOrDefault()?.listOrganizeDetailId;

                }
                SearchObject searchObject = new SearchObject()
                {
                    ep_status = "Active",
                    confirm_type = 2,
                    confirm_level = 1,
                    listOrganizeDetailId = listOrganizeDetailId,
                    position_id = 1,
                    userName = "n",
                    pageNumber = 1,
                    pageSize = 10
                };

                // Convert the object to JSON
                searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

                getDsNhanVien(searchJson);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi biến search json" + ex.Message);
            }
        }

        private void cbToChuc_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {

            cbViTri.SelectedIndex = -1;
            string textSearch = cbViTri.Text + e.Text;
            cbViTri.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbViTri.Text = "";
                cbViTri.Items.Refresh();
                cbViTri.ItemsSource = ListChucVu;
                cbViTri.SelectedIndex = -1;
            }
            else
            {
                cbViTri.ItemsSource = "";
                cbViTri.Items.Refresh();
                cbViTri.ItemsSource = ListChucVu.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbViTri_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbViTri.SelectedIndex = -1;
            string textSearch = cbViTri.Text + e.Text;
            cbViTri.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbViTri.Text = "";
                cbViTri.Items.Refresh();
                cbViTri.ItemsSource = ListChucVu;
                cbViTri.SelectedIndex = -1;
            }
            else
            {
                cbViTri.ItemsSource = "";
                cbViTri.Items.Refresh();
                cbViTri.ItemsSource = ListChucVu.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbToChuc_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            paginNV.SelectedPage = 0;
            createSearchString();
        }
        private async void getListTimeDotXuat()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "http://210.245.108.202:3000/api/qlc/shift/list");
                request.Headers.Add("Authorization", "Bearer " + Main.Tokens);
                // var content = new MultipartFormDataContent();
                // content.Add(new StringContent(Main.IdAcount), "companyID");
                // request.Content = content;                          
                var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_CaLamViec.CaLamViec api = JsonConvert.DeserializeObject<API_CaLamViec.CaLamViec>(responseContent);
                    listCaLamViec = api.data.items;
                    lsvTimeDotXuat.ItemsSource = listCaLamViec;
                }

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Loi ca lam viec" + ex.Message);
            }
        }
        string id_dx1;
        private async void Border_MouseLeftButtonUp_7(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/setting/updateTimeSetting");
                request.Headers.Add("Authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                // id_dx = ((Item)lsvTimeDotXuat.SelectedItem).shift_id;
                content.Add(new StringContent(id_dx1), "id_dx");
                content.Add(new StringContent(text), "time");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Update uc = new Update(Main);
                    Main.pnlShowPopUp.Children.Add(uc);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.pnlShowPopUp.Children.Add(Content as UIElement);
                }

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("cap nhat time" + ex.Message);
            }
        }
        string text;
        List<API_CaLamViec.Item> listCaLamViec = new List<API_CaLamViec.Item>();
        private void textDuyetTime_TextChanged(object sender, TextChangedEventArgs e)
        {
            ListView row = FindAncestor<ListView>((DependencyObject)e.OriginalSource);
            if (row != null)
            {
                TextBlock id_dx = FindChild<TextBlock>(row, "id_dx");
                id_dx1 = id_dx.Text;
            }

            TextBox textBox = (TextBox)sender; // Lấy ra TextBox đã được thay đổi giá trị
            text = textBox.Text;

        }

        //DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

        //    if (row != null)
        //    {
        //        // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
        //        Border bodXoaNhanVien = FindChild<Border>(row, "bodXoaNhanVien");

        //        if (bodXoaNhanVien != null)
        //        {
        //            // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
        //            // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
        //            bodXoaNhanVien.Visibility = Visibility.Collapsed;
        //        }
        //}


        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
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
        List<API_Time_KH.TimeDx> listSearchLoaiDX = new List<API_Time_KH.TimeDx>();
        private void cbLoaiDeXuat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listSearchLoaiDX.Clear();
           // string searchDx = cbLoaiDeXuat.Text;
            string searchDx = cbLoaiDeXuat.SelectedValue.ToString();
            dgvTimeKeHoach.ItemsSource = null;
            foreach (API_Time_KH.TimeDx item in ListDeXuatData)
            {
                //if (item.name_cate_dx.Equals(searchDx, StringComparison.OrdinalIgnoreCase))
                //{
                //    listSearchLoaiDX.Add(item);
                //}
                if(item.id_dx.ToString() == searchDx){
                    listSearchLoaiDX.Add(item);
                }
             
            }
           
            dgvTimeKeHoach.ItemsSource = listSearchLoaiDX;
        }
        public int id_update_duyet;
        int socap;
        private void Border_MouseLeftButtonUp_8(object sender, MouseButtonEventArgs e)
        {
            popupChinhCapD.Visibility = Visibility.Visible;
            socap = (int)((API_NV.NV_Infor)dgvNV.SelectedItem).ep_id;
            //g
            //id_update_duyet = (int)((API_NV.NV_Infor)dgvNV.SelectedItem).ep_id;
            //Main.pnlShowPopUp.Children.Add(new ucPopupUpdateDuyet(Main, id_update_duyet));
          
        }

        private void Border_MouseLeftButtonUp_9(object sender, MouseButtonEventArgs e)
        {
            khoiphuc.Visibility = Visibility.Visible;
            socap = (int)((API_NV.NV_Infor)dgvNV.SelectedItem).ep_id;
        }

        private void Path_MouseLeeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popupUpdateTimeD.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseeLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popupUpdateTimeD.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeqftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popupUpdateTimeD.Visibility = Visibility.Collapsed;
        }

        private void Border_Mouse2LeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            UpdateTimeDuyet();
            popupUpdateTimeD.Visibility = Visibility.Collapsed;
            dgvTimeKeHoach.Items.Refresh();
            Update uc = new Update(Main);
            Main.pnlShowPopUp.Children.Add(uc);
            object Content = uc.Content;
            uc.Content = null;
            Main.pnlShowPopUp.Children.Add(Content as UIElement);
        }

        private void Rectangle_MoweuseLeftBeuttonUp(object sender, MouseButtonEventArgs e)
        {
            popupChinhCapD.Visibility = Visibility.Collapsed;
        }

        private void Path_MouseLqaeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popupChinhCapD.Visibility = Visibility.Collapsed;
        }

        private void Border_MousedfLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popupChinhCapD.Visibility = Visibility.Collapsed;
        }

        private async void Border_MouseLedfsftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/settingConfirm/updateAllSettingConfirmLevel");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(textNhapSoCap.Text), "confirm_level");
                content.Add(new StringContent(socap.ToString()), "listUsers[]");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                getDsNhanVien();
                popupChinhCapD.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("loi update cap duyet" + ex.Message);
            }
            Update uc = new Update(Main);
            Main.pnlShowPopUp.Children.Add(uc);
            object Content = uc.Content;
            uc.Content = null;
            Main.pnlShowPopUp.Children.Add(Content as UIElement);
        }

        private void Rectangle_MousfgheLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            khoiphuc.Visibility = Visibility.Collapsed;
        }

        private void Border_ModsuseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            khoiphuc.Visibility = Visibility.Collapsed;
        }

        private async void Border_MousesdfLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/settingConfirm/updateAllSettingConfirmLevel");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("0"), "confirm_level");
                content.Add(new StringContent(socap.ToString()), "listUsers[]");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                getDsNhanVien();
                khoiphuc.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("loi update cap duyet" + ex.Message);
            }
            Refresh uc = new Refresh(Main);
            Main.pnlShowPopUp.Children.Add(uc);
            object Content = uc.Content;
            uc.Content = null;
            Main.pnlShowPopUp.Children.Add(Content as UIElement);
        }
    }
}
