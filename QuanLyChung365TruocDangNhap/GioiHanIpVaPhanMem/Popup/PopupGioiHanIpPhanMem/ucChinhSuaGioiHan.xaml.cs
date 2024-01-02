using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages;
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

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup
{
    /// <summary>
    /// Interaction logic for ucChinhSuaGioiHan.xaml
    /// </summary>
    public partial class ucChinhSuaGioiHan : UserControl
    {
        frmMain Main;
        List<ListAppEntities.AppData> ListAllApp = new List<ListAppEntities.AppData>();
        SettingIpAppEntities.User User = new SettingIpAppEntities.User();
        List<int> listUsers = new List<int>();
        List<string> listIps = new List<string>();
        List<int> listApps = new List<int>();

        public class BodyData
        {
            public List<int> listUsers { get; set; }
            public List<string> listIps { get; set; }
            public List<int> listApps { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
        }
        public class InputIp
        {
            public int STT { get; set; }
            public string IP { get; set; }
        }
        public ucChinhSuaGioiHan(frmMain main, SettingIpAppEntities.User User)
        {
            InitializeComponent();
            this.Main = main;
            this.User = User;
            BindingData();

        }

        private async void BindingData()
        {
            bool isload = await GetListApp();
            listUsers.Add(User.ep_id);
            DateTime outDate = new DateTime();
            DateTime.TryParse(User.start_date, out outDate);
            dpStartDate.SelectedDate = outDate;
            outDate = new DateTime();
            DateTime.TryParse(User.end_date, out outDate);
            dpEndDate.SelectedDate = outDate;
            if (User.listIPs.Count > 0)
                txtInputIp.Text = User.listIPs[0];
            int STT = 1;
            listIp = (from ip in User.listIPs.Skip(1)
                      select new InputIp
                      {
                          STT = STT++,
                          IP = ip,
                      }).ToList();
            lsvAddIp.ItemsSource = listIp;
            foreach (var item in User.listApps)
            {
                ListAllApp.Where(x => x.app_id == int.Parse(item)).FirstOrDefault().isSelect = true;
            }
            lsvListApp.ItemsSource = ListAllApp.Where(x => x.isSelect).ToList();
        }
        private async void Setting()
        {
            try
            {
                listIps = new List<string>();
                listApps = new List<int>();
                string start_date = User.start_date;
                if (dpStartDate.SelectedDate != null) start_date = dpStartDate.SelectedDate?.ToString("yyyy-MM-dd");
                string end_date = User.end_date;
                if (dpEndDate.SelectedDate != null) end_date = dpEndDate.SelectedDate?.ToString("yyyy-MM-dd");
                foreach (var item in ListAllApp.Where(x => x.isSelect))
                {
                    listApps.Add((int)item.app_id);
                }
                if (txtInputIp.Text != "") listIps.Add(txtInputIp.Text);
                foreach (var item in listIp)
                {
                    if (item.IP != "") listIps.Add(item.IP);
                }

                BodyData bodyData = new BodyData()
                {
                    listUsers = listUsers,
                    listApps = listApps,
                    listIps = listIps,
                    start_date = start_date,
                    end_date = end_date,

                };
                string bodyJson = JsonConvert.SerializeObject(bodyData, Formatting.Indented);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.APIs.IPnAPP_Setting);
                request.Headers.Add("Authorization", "Bearer " + Main.Tokens);
                var content = new StringContent(bodyJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {

                    PageDanhSachNhanVien qlnv = new PageDanhSachNhanVien(this.Main);
                    Main.stp_ShowPopup.Children.Clear();
                    object Content = qlnv.Content;
                    qlnv.Content = null;
                    Main.stp_ShowPopup.Children.Add(Content as UIElement);
                    this.Visibility = Visibility.Collapsed;
                }

            }
            catch { }
        }

        private async Task<bool> GetListApp()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.APIs.list_all_app);
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new StringContent("", null, "text/plain");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    ListAppEntities.Root result = JsonConvert.DeserializeObject<ListAppEntities.Root>(responseContent);
                    ListAllApp = result.data.data;
                    lsvApp.ItemsSource = ListAllApp;
                    return true;
                }
            }
            catch { }
            return false;
        }

        private void ckbxSelectAll_UnCheck(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListAllApp)
            {
                item.isSelect = false;
            }
            lsvListApp.ItemsSource = null;
        }

        private void ckbxSelectAll_Check(object sender, RoutedEventArgs e)
        {
            foreach (var item in ListAllApp)
            {
                item.isSelect = true;
            }
            //ListSelectedApp = ListAllApp;
            //lsvListApp.ItemsSource = ListSelectedApp;
            lsvListApp.ItemsSource = ListAllApp.Where(x => x.isSelect == true);
            lsvListApp.Items.Refresh();
        }

        private void UnSelectedApp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var UnSelectedApp = lsvListApp.SelectedItem as ListAppEntities.AppData;
            if (UnSelectedApp != null)
            {
                ListAllApp.Where(x => x.app_id == UnSelectedApp.app_id).ToList().First().isSelect = false;
            }
            lsvListApp.ItemsSource = ListAllApp.Where(x => x.isSelect == true);
            lsvListApp.Items.Refresh();
        }


        int countIp = 1;
        List<InputIp> listIp = new List<InputIp>();
        private void btnAddIp_MouseUp(object sender, MouseButtonEventArgs e)
        {

            listIp.Add(new InputIp());
            lsvAddIp.Items.Refresh();
            int STT = 1;
            listIp = (from item in listIp
                      select new InputIp
                      {
                          STT = STT++,
                          IP = item.IP,

                      }).ToList();
            lsvAddIp.ItemsSource = listIp;
            countIp++;
        }
        private void btnDeleteIp_MouseUp(object sender, MouseButtonEventArgs e)
        {
            InputIp selectedItem = (InputIp)((Image)sender).DataContext;
            listIp.Remove(selectedItem);
            lsvAddIp.Items.Refresh();
            int STT = 1;
            listIp = (from item in listIp
                      select new InputIp
                      {
                          STT = STT++,
                          IP = item.IP,

                      }).ToList();
            lsvAddIp.ItemsSource = listIp;
        }


        private void GetTextBoxTextFromListView()
        {

            for (int i = 0; i < lsvAddIp.Items.Count; i++)
            {
                // Find the TextBox within the ListView item's visual tree

                ListViewItem listViewItem = lsvAddIp.ItemContainerGenerator.ContainerFromIndex(i) as ListViewItem;
                {

                    TextBox textBox = FindVisualChild<TextBox>(listViewItem);
                    if (textBox != null)
                    {
                        string text = textBox.Text;
                        listIp[i].IP = text;
                    }
                }
            }
        }

        // Helper method to find a child control within a visual tree
        private T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T desiredChild)
                {
                    return desiredChild;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }

        private void Ok_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Setting();

        }

        private void txtInputIP_LostFocus(object sender, RoutedEventArgs e)
        {
            var textBox = ((TextBox)sender);
            var seletedItem = textBox.DataContext as InputIp;
            string ip = textBox.Text;
            listIp[seletedItem.STT - 1].IP = ip;
        }

        private void ComboBoxOpen_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (bodListAppCollapsed.Visibility == Visibility.Collapsed)
            {
                lsvApp.ItemsSource = ListAllApp;
                lsvApp.Items.Refresh();
                bodListAppCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListAppCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvApp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bodListAppCollapsed.Visibility = Visibility.Collapsed;
        }

        private void LsvAppItem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = ((Border)sender).DataContext as ListAppEntities.AppData;
            if (selectedItem != null)
            {
                if (selectedItem.isSelect == false)
                {
                    ListAllApp.Where(x => x.app_id == selectedItem.app_id).FirstOrDefault().isSelect = true;
                }
                else
                {
                    ListAllApp.Where(x => x.app_id == selectedItem.app_id).FirstOrDefault().isSelect = false;
                }
                lsvApp.ItemsSource = ListAllApp;
                lsvApp.Items.Refresh();
                lsvListApp.ItemsSource = ListAllApp.Where(x => x.isSelect == true);
                lsvListApp.Items.Refresh();
            }
        }

        private void ClosePopup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
