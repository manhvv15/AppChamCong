using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup
{
    /// <summary>
    /// Interaction logic for ucChonNhanVienVaCapQuyen.xaml
    /// </summary>
    public partial class ucChonNhanVienVaCapQuyen : UserControl
    {
        frmMain Main;
        List<ListOrganizeEntities.OrganizeData> ListOrganizeData = new List<ListOrganizeEntities.OrganizeData>();
        Dictionary<string, string> ListOrganize = new Dictionary<string, string>();
        List<ListUserEntities.UserData> ListUsers = new List<ListUserEntities.UserData>();
        List<ListUserEntities.UserData> SelectedUsersList = new List<ListUserEntities.UserData>();
        List<int> listUsers = new List<int>();

        string searchJson = "";
        string com_id = "1664";

        public class SearchObject
        {
            public string ep_status { get; set; }
            public int pageNumber { get; set; }
            public int pageSize { get; set; }
            public List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailId { get; set; }
        }
        public ucChonNhanVienVaCapQuyen(frmMain main, Dictionary<string, string> listOrganize)
        {
            InitializeComponent();
            Main = main;
            com_id = Main.IdAcount;
            GetListOrganize();


        }

        private async void GetListUser(string json = "", int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.APIs.userManager_listUsers);
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListUserEntities.Root result = JsonConvert.DeserializeObject<ListUserEntities.Root>(responseContent);
                    ListUsers = result.data.data;
                    if (paginNV.SelectedPage == 0) paginNV.TotalRecords = (int)result.data.total;
                    dsNhanVien.ItemsSource = ListUsers;
                }
            }
            catch
            {

            }
        }
        private async void GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.APIs.listAll_organize);
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);
                    ListOrganizeData = result.data.data;
                    foreach (var item in ListOrganizeData)
                    {
                        ListOrganize.Add(item.id.ToString(), item.organizeDetailName);
                    }
                    cbxOrganize.ItemsSource = ListOrganize;
                }
            }
            catch
            {

            }
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void NavigateToSetUpPermission(object sender, MouseButtonEventArgs e)
        {
            if (AddPopupBody.Children.Count > 1) AddPopupBody.Children.RemoveAt(1);
            foreach (var item in SelectedUsersList)
            {
                listUsers.Add((int)item.ep_id);
            }
            ucCapQuyen uc = new ucCapQuyen(this.Main, this, listUsers);
            this.AddPopupBody.Children[0].Visibility = Visibility.Collapsed;
            object content = uc.Content;
            uc.Content = null;
            this.AddPopupBody.Children.Add(content as UIElement);
            uc = null;
            content = null;
        }

        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

        }
        private void cbxOrganize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxOrganize.SelectedIndex = -1;
                string textSearch = cbxOrganize.Text;
                cbxOrganize.Items.Refresh();
                cbxOrganize.ItemsSource = ListOrganize.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxOrganize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxOrganize.SelectedIndex = -1;
            string textSearch = cbxOrganize.Text + e.Text;
            cbxOrganize.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxOrganize.Text = "";
                cbxOrganize.Items.Refresh();
                cbxOrganize.ItemsSource = ListOrganize;
                cbxOrganize.SelectedIndex = -1;
            }
            else
            {
                cbxOrganize.ItemsSource = "";
                cbxOrganize.Items.Refresh();
                cbxOrganize.ItemsSource = ListOrganize.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            createSearchString(paginNV.SelectedPage);
            //getNV("", "", "", pagingShift.SelectedPage).ContinueWith(tt => this.Dispatcher.Invoke(() =>
            //{
            //    if (tt.Result != null)
            //    {
            //        List_Staff_All = tt.Result.data.items;
            //    }
            //}));
        }

        private void cbxOrganize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            paginNV.SelectedPage = 0;
            createSearchString();
        }
        private void createSearchString(int pageNumber = 1)
        {
            try
            {
                List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailId = new List<ListOrganizeEntities.ListOrganizeDetailId>();

                if (cbxOrganize.SelectedIndex != -1)
                {
                    listOrganizeDetailId = new List<ListOrganizeEntities.ListOrganizeDetailId>();
                    listOrganizeDetailId = ListOrganizeData.Where(x => x.id == int.Parse(cbxOrganize.SelectedValue.ToString())).FirstOrDefault()?.listOrganizeDetailId;

                }
                SearchObject searchObject = new SearchObject()
                {
                    ep_status = "Active",
                    pageSize = 10,
                    pageNumber = pageNumber,
                    listOrganizeDetailId = listOrganizeDetailId
                };
                // Convert the object to JSON
                searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

                GetListUser(searchJson);
            }
            catch { }
        }
        private void Employee_Checked(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ListUserEntities.UserData)dsNhanVien.SelectedItem;
            if (selectedItem != null)
            {
                SelectedUsersList.Add(selectedItem);
            }
        }

        private void Employee_UnChecked(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ListUserEntities.UserData)dsNhanVien.SelectedItem;
            if (selectedItem != null)
            {
                SelectedUsersList.Remove(selectedItem);
            }
        }

        private void Employee_CheckAll(object sender, RoutedEventArgs e)
        {


            //check all item in listview
            for (int i = 0; i < ListUsers.Count; i++)
            {
                ListUsers[i].isCheck = true;

            }
            SelectedUsersList = ListUsers;
            dsNhanVien.ItemsSource = null;
            dsNhanVien.ItemsSource = ListUsers;
        }

        private void Employee_UnCheckAll(object sender, RoutedEventArgs e)
        {

            //Uncheck all item in listview
            for (int i = 0; i < ListUsers.Count; i++)
            {
                ListUsers[i].isCheck = false;

            }
            SelectedUsersList = new List<ListUserEntities.UserData>();
            dsNhanVien.ItemsSource = null;
            dsNhanVien.ItemsSource = ListUsers;
        }

        private void dsNhanVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }
    }
}
