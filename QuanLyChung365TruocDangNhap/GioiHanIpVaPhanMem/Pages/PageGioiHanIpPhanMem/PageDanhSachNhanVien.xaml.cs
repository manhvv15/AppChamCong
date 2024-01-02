using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages
{
    /// <summary>
    /// Interaction logic for PageDanhSachNhanVien.xaml
    /// </summary>
    public partial class PageDanhSachNhanVien : Page
    {
        frmMain Main;
        List<ListOrganizeEntities.OrganizeData> ListOrganizeData = new List<ListOrganizeEntities.OrganizeData>();
        Dictionary<string, string> ListOrganize = new Dictionary<string, string>();
        Dictionary<string, string> ListPosition = new Dictionary<string, string>();
        string com_id = "";
        string searchJson = "";



        public class SearchObject
        {
            public int pageSize { get; set; }
            public string userName { get; set; }
            public int position_id { get; set; }
            public List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailId { get; set; }
        }
        public PageDanhSachNhanVien(frmMain Main)
        {

            InitializeComponent();
            this.Main = Main;
            com_id = Main.IdAcount;
            GetListUserIPApp();
            GetListOrganize();
            GetListPosition();

        }

        private void BtnSetUp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.pnlShowPopUp.Children.Add(new ucChonNhanVienVaCapQuyen(this.Main, ListOrganize));
        }

        private async void GetListUserIPApp(string searchJson = "", int pageNumber = 1)
        {

            var handler = new HttpClientHandler();

            using (var httpClient = new HttpClient(handler))
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), APIs.APIs.IPnAPP_listUser))
                {
                    try
                    {



                        request.Headers.TryAddWithoutValidation("authorization", "Bearer " + Main.Tokens);

                        var content = new StringContent("");
                        //content = new StringContent("{\"pageSize\":1000,\"userName\":\"" + userName + "\",\"listOrganizeDetailId\": [{\"level\": " + OrganizeLevel + ",\"organizeDetailId\":" + OrganizeId + "}],\"position_id\": " + positionId + "}", null, "application/json");
                        content = new StringContent(searchJson, null, "application/json");
                        request.Content = content;
                        var response = await httpClient.SendAsync(request);
                        if (response.IsSuccessStatusCode)
                        {
                            var responseContent = await response.Content.ReadAsStringAsync();
                            SettingIpAppEntities.Root result = JsonConvert.DeserializeObject<SettingIpAppEntities.Root>(responseContent);
                            var list = result.data.data.Skip((pageNumber - 1) * 10).Take(10);
                            if (paginNV.SelectedPage == 0)
                            {
                                paginNV.TotalRecords = result.data.data.Count;
                                paginNV.SelectedPage = 1;
                            }
                            var STT = (pageNumber - 1) * 10 + 1;
                            var fillNullDataList = (from item in list
                                                    select new SettingIpAppEntities.User
                                                    {
                                                        STT = STT++,
                                                        phone = item.phone,
                                                        avatarUser = item.avatarUser,
                                                        ep_id = item.ep_id,
                                                        userName = item.userName,
                                                        organizeDetailName = (item.organizeDetailName == "") ? "Chưa cập nhật" : item.organizeDetailName,
                                                        positionName = (item.positionName == "") ? "Chưa cập nhật" : item.positionName,
                                                        listIPs = item.listIPs,
                                                        listApps = item.listApps,
                                                        start_date = (item.start_date == "0") ? "Không giới hạn" : DateTimeOffset.FromUnixTimeSeconds((long)double.Parse(item.start_date)).ToLocalTime().ToString("dd/MM/yyyy"),
                                                        end_date = (item.end_date == "0") ? "Không giới hạn" : DateTimeOffset.FromUnixTimeSeconds((long)double.Parse(item.end_date)).ToLocalTime().ToString("dd/MM/yyyy"),
                                                        app_num = item.app_num,
                                                    }).ToList();


                            dsGioiHanIpVaUngDung.ItemsSource = fillNullDataList;
                        }
                    }
                    catch
                    {

                    }

                }
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
        private async void GetListPosition()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.APIs.list_position);
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListPositionEntities.Root result = JsonConvert.DeserializeObject<ListPositionEntities.Root>(responseContent);
                    var list = result.data.data;
                    foreach (var item in list)
                    {
                        ListPosition.Add(item.id.ToString(), item.positionName);
                    }
                    cbxPosition.ItemsSource = ListPosition;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }
        }
        private void Edit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((Image)sender).DataContext as SettingIpAppEntities.User;
            Main.pnlShowPopUp.Children.Add(new ucChinhSuaGioiHan(Main, selectedItem));

        }

        private void Delelte_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((Image)sender).DataContext as SettingIpAppEntities.User;
            Main.pnlShowPopUp.Children.Add(new ucXoaNhanVienGioiHanIP(this.Main, selectedItem));
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

        private void cbxPosition_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxPosition.SelectedIndex = -1;
                string textSearch = cbxPosition.Text;
                cbxPosition.Items.Refresh();
                cbxPosition.ItemsSource = ListPosition.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxPosition_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxPosition.SelectedIndex = -1;
            string textSearch = cbxPosition.Text + e.Text;
            cbxPosition.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxPosition.Text = "";
                cbxPosition.Items.Refresh();
                cbxPosition.ItemsSource = ListPosition;
                cbxPosition.SelectedIndex = -1;
            }
            else
            {
                cbxPosition.ItemsSource = "";
                cbxPosition.Items.Refresh();
                cbxPosition.ItemsSource = ListPosition.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (paginNV.SelectedPage > 0) GetListUserIPApp(searchJson, paginNV.SelectedPage);
            //getNV("", "", "", pagingShift.SelectedPage).ContinueWith(tt => this.Dispatcher.Invoke(() =>
            //{
            //    if (tt.Result != null)
            //    {
            //        List_Staff_All = tt.Result.data.items;
            //    }
            //}));
        }

        private void btnSearch_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string userName = txtSearchName.Text;
                int positionId = 0;
                if (cbxPosition.SelectedIndex != -1) positionId = int.Parse(cbxPosition.SelectedValue.ToString());
                List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailId = null;
                if (cbxOrganize.SelectedIndex != -1)
                {
                    listOrganizeDetailId = new List<ListOrganizeEntities.ListOrganizeDetailId>();
                    listOrganizeDetailId = ListOrganizeData.Where(x => x.id == int.Parse(cbxOrganize.SelectedValue.ToString())).FirstOrDefault()?.listOrganizeDetailId;

                }


                SearchObject searchObject = new SearchObject
                {
                    pageSize = 10000,
                    userName = userName,
                    position_id = positionId,
                    listOrganizeDetailId = listOrganizeDetailId
                };
                // Convert the object to JSON
                searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

                paginNV.SelectedPage = 0;
                GetListUserIPApp(searchJson);


            }
            catch { }
        }

        private void AppDetail_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((TextBlock)sender).DataContext as SettingIpAppEntities.User;
            if (selectedItem != null)
            {
                List<int> listAppIds = selectedItem.listApps.Select(x => int.Parse(x)).ToList();
                Main.pnlShowPopUp.Children.Add(new ucDanhSachPhanMemDuocTruyCap(Main, listAppIds));
            }

        }

        private void dsGioiHanIpVaUngDung_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    var scrollViewer = FindVisualChild<ScrollViewer>(dsGioiHanIpVaUngDung);
                    if (scrollViewer != null)
                    {
                        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
                        e.Handled = true;
                    }

                }
                else
                {
                    Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }

        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }
    }
}
