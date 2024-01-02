using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.RecommendSetting.OOP;
using QuanLyChung365TruocDangNhap.RecommendSetting.Popup;
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

namespace QuanLyChung365TruocDangNhap.RecommendSetting
{
    /// <summary>
    /// Interaction logic for PopupThemMoi.xaml
    /// </summary>
    public partial class PopupThemMoi : UserControl
    {
        frmMain Main;
        List<API_NV.NV_Infor> listNV = new List<API_NV.NV_Infor>();
        List<API_Organization.Organization_Infor> ListOrganizeData = new List<API_Organization.Organization_Infor>();
        Dictionary<string, string> ListOrganize = new Dictionary<string, string>();
        //ucRecommended UcRecom;
        public PopupThemMoi(frmMain main)
        {
            InitializeComponent();
            Main = main;
            getListToChuc();
            getDsNhanVien();
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
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void iconClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

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
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getDsNhanVien( paginNV.SelectedPage);

        }
        private async void getDsNhanVien( int pageNumber = 1)
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
                        listNV = api.data.data;
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
                       // listNV = fillNullDataList;
                    }
                }
                //   TongSoTrang = (int)Math.Ceiling((double)TotalItem / pageSize);

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi lấy ra danh sách nhân viên " + ex.Message);
            }
        }
        List<API_NV.NV_Infor> listSearchNV = new List<API_NV.NV_Infor>();
        private void cbToChuc_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listSearchNV.Clear();
            dgvNV.ItemsSource = null;
            // API_NV.NV_Infor api = new API_NV.NV_Infor();
            string searchToChuc = cbToChuc.SelectedValue.ToString();
            if (searchToChuc != null)
            {
                foreach (API_NV.NV_Infor item in listNV)
                {

                    if (searchToChuc == item.organizeDetailName.ToString())
                    {
                        listSearchNV.Add(item);
                    }

                }
            }
            paginNV.SelectedPage = 0;
            getSearchNV();
           // dgvNV.Items.sSource = listSearchNV;
        }
        private void getSearchNV(int pageNumber = 1)
        {
            dgvNV.ItemsSource = listSearchNV;
            paginNV.TotalRecords = listSearchNV.Count ;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.pnlShowPopUp.Children.Add(new ucThemMoi(Main, id_NV));
        }
        int id_NV;
        private void CheckBox_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            id_NV = (int)((API_NV.NV_Infor)dgvNV.SelectedItem).ep_id;
            borTiepTuc.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1677FF"));
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            id_NV = (int)((API_NV.NV_Infor)dgvNV.SelectedItem).ep_id;
            borTiepTuc.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1677FF"));
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            borTiepTuc.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#a9a9a9"));
        }
    }
}
