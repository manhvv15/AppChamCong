using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;

namespace ChamCong365.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucCreateWifi.xaml
    /// </summary>
    public partial class ucThemMoiWifi : UserControl
    {
        MainWindow Main;
        private ItemWifi itemWifi;
        private ucDanhSachWii ucDanhSachWii;
        public ucThemMoiWifi(MainWindow main, ItemWifi itemWifi, ucDanhSachWii ucDanhSachWii)
        {
            InitializeComponent();
            this.itemWifi = itemWifi;
            this.ucDanhSachWii = ucDanhSachWii;
            Main = main;
        }
        public async void AddWifi()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.add_wifi_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_TenWifi.Text), "name_wifi");
                content.Add(new StringContent(tb_DiaChiMac.Text), "mac_address");
                content.Add(new StringContent(""), "ip_address");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var responsWifi = await response.Content.ReadAsStringAsync();
                    RootWifi loadListWifi = JsonConvert.DeserializeObject<RootWifi>(responsWifi);
                    this.Visibility = Visibility.Collapsed;
                    ucDanhSachWii.LoadListWifi();
                }
            }
            catch (System.Exception)
            {

            }
        }

        private bool ValidateAddForm()
        {
            if (String.IsNullOrEmpty(tb_TenWifi.Text))
            {
                txtValidateNameWifi.Text = "Tên wifi không được để trống!" as string;
                return false;
            }

            else if (String.IsNullOrEmpty(tb_DiaChiMac.Text))
            {
                txtvalidateAddressMac.Text = "Địa chỉ Mac không được để trống!" as string;
                return false;
            }


            return true;
        }

        private void CreateWifi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitCreateWifi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

     

        private  void bodThemMoiWifi_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (ValidateAddForm())
            {
                AddWifi();
            }

        }

       
    }
}
