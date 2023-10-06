using ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace ChamCong365.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucCreateAddressIP.xaml
    /// </summary>
    public partial class ucThemDiaChiIP : UserControl
    {
        MainWindow Main;
        private ListIP listIP;
        ucDanhSachIP ucDanhSachIP;
        public ucThemDiaChiIP(MainWindow main, ListIP listIP, ucDanhSachIP ucDanhSachIP)
        {
            InitializeComponent();
            Main = main;
            this.listIP = listIP;
            this.ucDanhSachIP = ucDanhSachIP;
           
        }
        
        public async void AddIP()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.create_ip_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_TenIP.Text), "from_site");
                content.Add(new StringContent(tb_DiaChiIP.Text), "ip_access");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var responsIP = await response.Content.ReadAsStringAsync();
                    RootIp loadListWifi = JsonConvert.DeserializeObject<RootIp>(responsIP);
                    this.Visibility = Visibility.Collapsed;
                    ucDanhSachIP.LoadListIP();
                }
                else
                {

                }
            }
            catch (System.Exception)
            {

               
            }
        }

        private bool ValidateAddForm()
        {
            if (String.IsNullOrEmpty(tb_TenIP.Text))
            {
                txtValidateNameIP.Text = "Tên wifi không được để trống!" as string;
                return false;
            }

            else if (String.IsNullOrEmpty(tb_DiaChiIP.Text))
            {
                txtValidateAddressIP.Text = "Địa chỉ Mac không được để trống!" as string;
                return false;
            }


            return true;
        }

        private void btnAddWifi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ValidateAddForm())
            {
                AddIP();
            }
        }
        private void ExitCreateWifi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown_1(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

       
    }
}
