using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.PhanQuyenHeThong.OOP;
using QuanLyChung365TruocDangNhap.PhanQuyenHeThong.ThongBao;
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
using static QuanLyChung365TruocDangNhap.PhanQuyenHeThong.OOP.API_PhanQuyen;

namespace QuanLyChung365TruocDangNhap.PhanQuyenHeThong.Popup
{
    /// <summary>
    /// Interaction logic for ucThemMoi.xaml
    /// </summary>
    public partial class ucThemMoi : UserControl
    {
        frmMain Main;
        ucQuanTriVien ucQTV;
        public ucThemMoi(frmMain main, ucQuanTriVien ucqTV)
        {
            InitializeComponent();
            Main = main;
            getDsNv();
            
            ucQTV = ucqTV;
        }
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
                    //txtCountNv.Text = listAllUser.Count().ToString();
                    dgvNV.ItemsSource = listAllUser;
                    var list = api.data.data.Skip((pageNumber - 1) * 10).Take(10);
                    int stt = 1;
                    foreach (var item in list)
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

                    //  dgvNV.ItemsSource = fillNullDataList;
                    dgvNV.ItemsSource = list;
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
        List<API_PhanQuyen.User> listAllUser = new List<API_PhanQuyen.User>();

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

        private void Border_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            if (borName1.Visibility == Visibility.Collapsed)
            {
                borName1.Visibility = Visibility.Visible;
            }
            else
            {
                borName1.Visibility = Visibility.Collapsed;
            }
        }

        private void dgvNV_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNv.ScrollToVerticalOffset(scrollNv.VerticalOffset - e.Delta);
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucThemmoi.Visibility = Visibility.Collapsed;
        }
        List<string> listString = new List<string>();
        private void chkSelectAll_Checked(object sender, RoutedEventArgs e)
        {
            listString.Clear();
            foreach(var item in listAllUser)
            {
                item.isChecked = true;
                listString.Add(item.ep_id.ToString()+",");
            }
            dgvNV.Items.Refresh();

        }

        private void chkSelectAll_Unchecked(object sender, RoutedEventArgs e)
        {
            listString.Clear();
            foreach (var item in listAllUser)
            {
                item.isChecked = false;
            }
            dgvNV.Items.Refresh();
        }

        private void huy(object sender, MouseButtonEventArgs e)
        {
            ucThemmoi.Visibility = Visibility.Collapsed;
        }
        string idAll_CaiDat ;
        private async void Luu(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/updateAdmin");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                //var content = new MultipartFormDataContent();
                //  string isUpdate = "";
                for (int i = 0; i < listString.Count; i++)
                {
                    if (listString[i].EndsWith(",") && i == listString.Count - 1)
                    {
                        listString[i] = listString[i].Substring(0, listString[i].Length - 1);
                    }
                }
                idAll_CaiDat = "[" + string.Join("", listString) + "]";
                var content = new StringContent("{\"listUsers\":"+ idAll_CaiDat + ",\"newIsAdmin\":1}", null, "application/json");
                //content.Add(new StringContent("1"), "newIsAdmin");
                //content.Add(new StringContent(idAll_CaiDat), "listUsers");
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
                    ucThemmoi.Visibility = Visibility.Collapsed;
                    getDsNv();
                    ucQTV.pageNv.SelectedPage = 0;
                    ucQTV.getDsNv();
                    dgvNV.Items.Refresh();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucThemmoi.Visibility = Visibility.Collapsed;
        }
        int id_caidatNv = 0;
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //CheckBox c = (sender as CheckBox).DataContext as API_PhanQuyen.User;
            (((CheckBox)sender).DataContext as API_PhanQuyen.User).isChecked = true;
            id_caidatNv = (int)(((CheckBox)sender).DataContext as API_PhanQuyen.User).ep_id;

            listString.Add(id_caidatNv + ",");
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            (((CheckBox)sender).DataContext as API_PhanQuyen.User).isChecked = false;
            id_caidatNv = (int)(((CheckBox)sender).DataContext as API_PhanQuyen.User).ep_id;
            listString.Remove(id_caidatNv + ",");
        }

        private void Rectangle_MouseLeftduatdtonUp_1(object sender, MouseButtonEventArgs e)
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
            List<API_PhanQuyen.User> listSearch = new List<API_PhanQuyen.User>();
            if (searchName != null)
            {
                foreach (var str in listAllUser)
                {
                    if (str.userName?.ToLower().RemoveUnicode() == (searchName.ToLower().RemoveUnicode()))
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
    }
}
