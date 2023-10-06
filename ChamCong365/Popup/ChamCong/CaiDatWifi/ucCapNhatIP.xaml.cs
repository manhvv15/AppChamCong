using ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi;
using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucUpdateIP.xaml
    /// </summary>
    public partial class ucCapNhatIP : UserControl
    {
        MainWindow Main;
        private ListIP ListIP;
        ucDanhSachIP ucDanhSachIP;
        DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        public ucCapNhatIP(MainWindow main, ListIP listIP, ucDanhSachIP ucDanhSachIP)
        {
            InitializeComponent();
            Main = main;
            this.ListIP = listIP;
            this.ucDanhSachIP = ucDanhSachIP;
            tb_TenIP.Text = listIP.from_site;
            tb_DiaChiIPMang.Text = listIP.ip_access;
          
        }

        public static DateTime UnixTimeStampToDateTime(int dt)
        {
            // Unix timestamp is seconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(dt).ToLocalTime();
            return dateTime;
        }
        public async void UpdateIP()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.edit_ip_api);
                request.Headers.Add("Authorization", "Bearer "+ Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_TenIP.Text), "from_site");
                content.Add(new StringContent(tb_DiaChiIPMang.Text), "ip_access");
                content.Add(new StringContent(ListIP.id_acc.ToString()), "id_acc");
                content.Add(new StringContent(ListIP.created_time), "created_time");
                content.Add(new StringContent(ListIP.update_time), "update_time");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucDanhSachIP.LoadListIP();
                }
            }
            catch (System.Exception)
            {          
            }
        }

        private bool ValidateEditForm()
        {
            if (tb_TenIP.Text.Trim() == "")
            {
                txtValidateNameIP.Text = "Tên IP không được để trống!" as string;
                return false;
            }
            if (tb_DiaChiIPMang.Text.Trim() == "")
            {
                txtValidateDiaChiIP.Text = "Địa chỉ IP mạng không được để trống!" as string;
                return false;
            }

            return true;
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitUpdateIP_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCapNhatIP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ValidateEditForm())
            {
                UpdateIP();
            }
        }

     
    }
}
