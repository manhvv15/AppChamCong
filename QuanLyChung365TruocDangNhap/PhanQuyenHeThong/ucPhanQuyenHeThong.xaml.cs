using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.PhanQuyenHeThong.OOP;
using QuanLyChung365TruocDangNhap.PhanQuyenHeThong.ThongBao;
using QuanLyChung365TruocDangNhap.RecommendSetting.OOP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using static QuanLyChung365TruocDangNhap.PhanQuyenHeThong.OOP.API_DsToChucc;

namespace QuanLyChung365TruocDangNhap.PhanQuyenHeThong
{
    /// <summary>
    /// Interaction logic for ucPhanQuyenHeThong.xaml
    /// </summary>
    public partial class ucPhanQuyenHeThong : UserControl
    {
        frmMain Main;
        public ucPhanQuyenHeThong(frmMain main)
        {
            InitializeComponent();
            Main = main;
            getDsNv();
            getDsToChuc();
            getDsViTri();
            getDsNv1();
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucQuanTriVien uc = new ucQuanTriVien(Main);
            Main.stp_ShowPopup.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.stp_ShowPopup.Children.Add(Content as UIElement);
        }
        List<API_PhanQuyen.User> listAllUser = new List<API_PhanQuyen.User>();
        List<API_DsToChucc.ToChuc> listToChuc = new List<API_DsToChucc.ToChuc>();
        List<API_DsViTrii.ViTri> listViTri = new List<API_DsViTrii.ViTri>();
        private async void getDsNv(int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/listUser");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount), "com_id");
                content.Add(new StringContent("10000"), "pageSize");
                content.Add(new StringContent("Active"), "ep_status");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    API_PhanQuyen.PhanQuyen api = JsonConvert.DeserializeObject<API_PhanQuyen.PhanQuyen>(responseContent);
                    listAllUser = api.data.data;
                    lsvName.ItemsSource = listAllUser;
                    txtCountNv.Text = listAllUser.Count().ToString();
                    dgvNV.ItemsSource = listAllUser;
                    var list = api.data.data.Skip((pageNumber - 1) * 10).Take(10);
                    int stt = 1;
                    foreach (var item in listAllUser)
                    {
                        item.stt = stt++;
                    }
                    foreach (var item in listAllUser)
                    {
                        if (item.organizeDetailName == "")
                        {
                            item.organizeDetailName = "Chưa cập nhật";
                        }
                    }
                    foreach (var item in listAllUser)
                    {
                        if (item.positionName == "")
                        {
                            item.positionName = "Chưa cập nhật";
                        }
                    }
                    foreach (var item in listAllUser)
                    {
                        if (item.phone == "" || item.phone == null)
                        {
                            item.phone = "Chưa cập nhật";
                        }
                    }


                    if (pageNv.SelectedPage == 0) pageNv.TotalRecords = (int)listAllUser.Count();

                    dgvNV.ItemsSource = list;
                }


            }
            catch (Exception ex)
            {

            }
        }
        private async void getDsNv1()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/listUser");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount), "com_id");
                content.Add(new StringContent("10000"), "pageSize");
                content.Add(new StringContent("Active"), "ep_status");
                content.Add(new StringContent("1"), "isAdmin");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    API_PhanQuyen.PhanQuyen api = JsonConvert.DeserializeObject<API_PhanQuyen.PhanQuyen>(responseContent);
                    // listAllUser = api.data.data;
                    //  lsvName.ItemsSource = listAllUser;
                    txtCountQTV.Text = api.data.data.Count().ToString();
                }


            }
            catch (Exception ex)
            {

            }
        }
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

                    }
                }

            }
            catch (Exception ex)
            {

            }
        }
        private void pagiCaiDatNv_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getDsNv(pageNv.SelectedPage);

        }

        private void Rectangle_MouseLeftuttonUp_1(object sender, MouseButtonEventArgs e)
        {
            borTOChuc1.Visibility = Visibility.Collapsed;
        }

        private void lsvDx_SeledctionChanged(object sender, SelectionChangedEventArgs e)
        {
            textToChuc.Text = "";
            textToChuc.Focus();
            borTOChuc1.Visibility = Visibility.Collapsed;
            string searchToChuc = (lsvToChuc.SelectedItem as API_DsToChucc.ToChuc)?.organizeDetailName;
            //textChonToChuc1.Visibility = Visibility.Visible;
            textToChuc1.Text = searchToChuc;
            //textToChuc.Visibility = Visibility.Collapsed;
            List<API_PhanQuyen.User> listSearch = new List<API_PhanQuyen.User>();

            if (searchToChuc != null)
            {
                foreach (var str in listAllUser)
                {
                    if (str.organizeDetailName?.ToLower().RemoveUnicode() == (searchToChuc.ToLower().RemoveUnicode()))
                    {

                        listSearch.Add(str);

                    }
                }
            }
            int stt = 1;

            pageNv.SelectedPage = 0;
            pageNv.TotalRecords = listSearch.Count();
            // getDsNv();
            var list = listSearch.Skip(((int)(pageNv.SelectedPage) - 1) * 10).Take(10);
            //int stt = 1;
            foreach (var item in list)
            {
                item.stt = stt++;
            }
            dgvNV.ItemsSource = list;
            dgvNV.Items.Refresh();
        }

        private void Rectangle_MouseLeftuattonUp_1(object sender, MouseButtonEventArgs e)
        {
            borVitri1.Visibility = Visibility.Collapsed;
        }

        private void lsvDx_SeledctaionChanged(object sender, SelectionChangedEventArgs e)
        {
            textViTri.Text = "";
            textViTri.Focus();
            borVitri1.Visibility = Visibility.Collapsed;
            string searchToChuc = (lsvViTri.SelectedItem as API_DsViTrii.ViTri)?.positionName;
            //textChonToChuc1.Visibility = Visibility.Visible;
            textViTri1.Text = searchToChuc;
            string textToChuc11 = textToChuc1.Text;
            //textToChuc.Visibility = Visibility.Collapsed;
            List<API_PhanQuyen.User> listSearch = new List<API_PhanQuyen.User>();

            if (searchToChuc != null)
            {
                foreach (var str in listAllUser)
                {
                    if (str.positionName?.ToLower().RemoveUnicode() == (searchToChuc.ToLower().RemoveUnicode()) && str.organizeDetailName == textToChuc11)
                    {

                        listSearch.Add(str);

                    }
                }
            }
            int stt = 1;

            pageNv.SelectedPage = 0;
            pageNv.TotalRecords = listSearch.Count();
            // getDsNv();
            var list = listSearch.Skip(((int)(pageNv.SelectedPage) - 1) * 10).Take(10);
            //int stt = 1;
            foreach (var item in list)
            {
                item.stt = stt++;
            }
            dgvNV.ItemsSource = list;
            dgvNV.Items.Refresh();
        }
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

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
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

        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            double width = (sender as Border).ActualWidth;
            double margi = width + 20;
            borVitri1.Margin = new Thickness(margi, 129, 0, 0);
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
            string searchName = (lsvName.SelectedItem as API_PhanQuyen.User)?.userName;
            //textChonToChuc1.Visibility = Visibility.Visible;
            textName1.Text = searchName;
            string textToChuc11 = textToChuc1.Text;
            string textViTri11 = textViTri1.Text;
            List<API_PhanQuyen.User> listSearch = new List<API_PhanQuyen.User>();
            if (searchName != null)
            {
                foreach (var str in listAllUser)
                {
                    if (str.userName?.ToLower().RemoveUnicode() == (searchName.ToLower().RemoveUnicode()) && str.organizeDetailName == textToChuc11 && str.positionName == textViTri11)
                    {

                        listSearch.Add(str);

                    }
                }
            }
            pageNv.SelectedPage = 0;
            pageNv.TotalRecords = listSearch.Count();
            // getDsNv();
            var list = listSearch.Skip(((int)(pageNv.SelectedPage) - 1) * 10).Take(10);
            int stt = 1;
            foreach (var item in list)
            {
                item.stt = stt++;
            }
            dgvNV.ItemsSource = list;
            dgvNV.Items.Refresh();
        }

        private void textName_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<API_PhanQuyen.User> listSearchName = new List<API_PhanQuyen.User>();

            // textName1.Text = "";
            textName.Focus();
            borName1.Visibility = Visibility.Visible;
            string searchText = textName.Text.ToLower().RemoveUnicode();
            foreach (var str in listAllUser)
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
                listSearchName = listAllUser;
            }
            lsvName.ItemsSource = listSearchName;
            lsvName.Items.Refresh();

        }

        private void textViTri_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<API_DsViTrii.ViTri> listSearchName = new List<API_DsViTrii.ViTri>();

            // textName1.Text = "";
            textViTri.Focus();
            borVitri1.Visibility = Visibility.Visible;
            string searchText = textViTri.Text.ToLower().RemoveUnicode();
            foreach (var str in listViTri)
            {
                if (str.positionName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listSearchName.Any(item => item.positionName.Equals(str.positionName, StringComparison.OrdinalIgnoreCase)))
                    {
                        listSearchName.Add(str);
                    }
                    //listSearchName.Add(str);
                }
            }
            if (searchText == "")
            {
                listSearchName = listViTri;
            }
            lsvViTri.ItemsSource = listSearchName;
            lsvViTri.Items.Refresh();
        }

        private void textToChuc_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<API_DsToChucc.ToChuc> listSearchName = new List<API_DsToChucc.ToChuc>();

            // textName1.Text = "";
            textToChuc.Focus();
            borTOChuc1.Visibility = Visibility.Visible;
            string searchText = textToChuc.Text.ToLower().RemoveUnicode();
            foreach (var str in listToChuc)
            {
                if (str.organizeDetailName.ToLower().RemoveUnicode().Contains(searchText))
                {
                    if (!listSearchName.Any(item => item.organizeDetailName.Equals(str.organizeDetailName, StringComparison.OrdinalIgnoreCase)))
                    {
                        listSearchName.Add(str);
                    }
                    //listSearchName.Add(str);
                }
            }
            if (searchText == "")
            {
                listSearchName = listToChuc;
            }
            lsvToChuc.ItemsSource = listSearchName;
            lsvToChuc.Items.Refresh();
        }

        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            double width = (sender as Border).ActualWidth;
            double margi = width + 10;
            borName1.Margin = new Thickness(0, 129, margi, 0);
            if (borName1.Visibility == Visibility.Collapsed)
            {
                borName1.Visibility = Visibility.Visible;
            }
            else
            {
                borName1.Visibility = Visibility.Collapsed;

            }
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            phanQuyen.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            phanQuyen.Visibility = Visibility.Collapsed;
        }
        string ep_id;
        private void Border_MouseLeftButtonUp_5(object sender, MouseButtonEventArgs e)
        {
            phanQuyen.Visibility = Visibility.Visible;
            API_PhanQuyen.User d = (API_PhanQuyen.User)dgvNV.SelectedItem;
            if (d != null)
            {
                ep_id = d.ep_id.ToString();
            }
        }

        private void huy(object sender, MouseButtonEventArgs e)
        {
            phanQuyen.Visibility = Visibility.Collapsed;

        }

        private async void hoanThanh(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/updateAdmin");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                string isUpdate = "";
                if (ComboBox22.SelectedIndex == 0)
                {
                    isUpdate = "1";
                }
                else if (ComboBox22.SelectedIndex == 1)
                {
                    isUpdate = "0";
                }
                content.Add(new StringContent(isUpdate), "newIsAdmin");
                content.Add(new StringContent(ep_id.ToString()), "listUsers");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ThanhCong uc = new ThanhCong(Main);
                    // Main.stp_ShowPopup.Children.Clear();
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.pnlShowPopUp.Children.Add(Content as UIElement);
                    phanQuyen.Visibility = Visibility.Collapsed;
                    getDsNv();
                    getDsNv1();
                    dgvNV.Items.Refresh();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void dgvNV_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset - e.Delta);
        }
    }
}
