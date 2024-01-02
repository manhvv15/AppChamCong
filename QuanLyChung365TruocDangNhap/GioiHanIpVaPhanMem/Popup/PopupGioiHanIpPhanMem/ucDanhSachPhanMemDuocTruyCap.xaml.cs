using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
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
using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using System.Runtime.CompilerServices;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup
{
    /// <summary>
    /// Interaction logic for ucChinhSuaGioiHan.xaml
    /// </summary>
    public partial class ucDanhSachPhanMemDuocTruyCap : UserControl
    {
        List<ListAppEntities.AppData> ListApps = new List<ListAppEntities.AppData>();
        List<int> listApps = new List<int>();
        frmMain Main;
        class BodyData
        {
            public List<int> listApps { get; set; }
        }
        public ucDanhSachPhanMemDuocTruyCap(frmMain Main, List<int> listAppIds)
        {
            InitializeComponent();
            this.Main = Main;
            this.listApps = listAppIds;
            GetListApp();

        }

        private void ClosePopup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public async void GetListApp()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/settingIPApp/listApp");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);

                var content = new MultipartFormDataContent();
                foreach (var item in listApps)
                {
                    content.Add(new StringContent(item.ToString()), "listApps[]");
                }

                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    ListAppEntities.Root result = JsonConvert.DeserializeObject<ListAppEntities.Root>(responseContent);
                    ListApps = result.data.data;

                    lsvListTypeApp.ItemsSource = ListApps.GroupBy(x => x.app_type);
                    lsvListNameApp.ItemsSource = ListApps.Where(x => x.app_type == 1);
                }

            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(" error: " + e.Message);
            }

        }

        private void lsvListTypeApp_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                dynamic selectedItem = lsvListTypeApp.SelectedItem;
                if (selectedItem != null)
                {
                    lsvListNameApp.ItemsSource = selectedItem;
                }
            }
            catch { }

        }
        private void paginApp_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            LoadListNameApp(paginApp.SelectedPage);
        }
        private void LoadListNameApp(int pageNumber = 1)
        {

        }
    }
}
