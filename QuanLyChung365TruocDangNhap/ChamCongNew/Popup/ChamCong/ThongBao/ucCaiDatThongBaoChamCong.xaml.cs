using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
//using DocumentFormat.OpenXml.Drawing.Charts;
using Newtonsoft.Json;
//using NPOI.OpenXmlFormats.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ThongBao
{
    /// <summary>
    /// Interaction logic for ucCaiDatThongBaoChamCong.xaml
    /// </summary>
    public partial class ucCaiDatThongBaoChamCong : UserControl
    {
        MainWindow Main;
        public ucCaiDatThongBaoChamCong(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            GetNotifyTimeKeeping();
        }
        public async void GetNotifyTimeKeeping()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/NotifyTimekeeping/getData");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    NotifyEntities.Root result = JsonConvert.DeserializeObject<NotifyEntities.Root>(responseContent);
                    gridDisplay.DataContext = result.data.message;
                }
            }
            catch
            {

            }
        }
        public async void Update()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/NotifyTimekeeping/update");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var jsonBody = new
                {
                    status = 1,
                    minute = int.Parse(txtMinute.Text),
                    content = txtContent.Text
                };
                string json = JsonConvert.SerializeObject(jsonBody);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    GetNotifyTimeKeeping();
                    borMinute.Visibility = Visibility.Collapsed;
                    borContent.Visibility = Visibility.Collapsed;
                    dockLuuHuy.Visibility = Visibility.Collapsed;
                    txbContent.Visibility = Visibility.Visible;
                }
            }
            catch
            {

            }
        }

        public async void UpdateStatus0n()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/NotifyTimekeeping/update");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var jsonBody = new
                {
                    status = 1
                };
                string json = JsonConvert.SerializeObject(jsonBody);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    GetNotifyTimeKeeping();
                }
            }
            catch
            {

            }
        }

        public async void UpdateStatusOff()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/NotifyTimekeeping/update");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var jsonBody = new
                {
                    status = -1
                };
                string json = JsonConvert.SerializeObject(jsonBody);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    GetNotifyTimeKeeping();
                }
            }
            catch
            {

            }
        }

        private void BackOnOff(object sender, MouseButtonEventArgs e)
        {

        }

        private void Sua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borMinute.Visibility = Visibility.Visible;
            borContent.Visibility = Visibility.Visible;
            dockLuuHuy.Visibility = Visibility.Visible;
            txbContent.Visibility = Visibility.Collapsed;
        }

        private void Huy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borMinute.Visibility = Visibility.Collapsed;
            borContent.Visibility = Visibility.Collapsed;
            dockLuuHuy.Visibility = Visibility.Collapsed;
            txbContent.Visibility = Visibility.Visible;
        }

        private void Luu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Update();
        }

        private void BackOn(object sender, MouseButtonEventArgs e)
        {
            UpdateStatus0n();
        }

        private void BackOff(object sender, MouseButtonEventArgs e)
        {
            UpdateStatusOff();
        }
    }
}
