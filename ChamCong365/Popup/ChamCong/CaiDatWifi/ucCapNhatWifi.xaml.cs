using ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi;
using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Collections.Generic;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using System.Web.UI.WebControls;

using ChamCong365.funcQuanLyCongTy;

namespace ChamCong365.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucUpdateWifi.xaml
    /// </summary>
    public partial class ucCapNhatWifi : UserControl
    {
        private ItemWifi itemWifi;
        private ucDanhSachWii ucDanhSachWii;
        MainWindow Main;

        public ucCapNhatWifi( MainWindow main, ItemWifi itemWifi, ucDanhSachWii ucDanhSachWii)
        {
            InitializeComponent();
            this.itemWifi = itemWifi;
            this.ucDanhSachWii = ucDanhSachWii;
            Main = main; 
            tb_TenWifi.Text = itemWifi.name_wifi;
            tb_DiaChiMac.Text = itemWifi.mac_address;
        }
        public async void EditWifi()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.edit_wifi_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(itemWifi.wifi_id.ToString()), "wifi_id");
                content.Add(new StringContent(tb_TenWifi.Text.ToString()), "name_wifi");
                content.Add(new StringContent(itemWifi.status.ToString()), "status");
                content.Add(new StringContent(itemWifi.is_default.ToString()), "is_default");
                content.Add(new StringContent(tb_DiaChiMac.Text), "mac_address");
                content.Add(new StringContent(itemWifi.ip_address), "ip_address");
                content.Add(new StringContent(itemWifi.com_id.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    
                    this.Visibility = Visibility.Collapsed;
                    ucDanhSachWii.LoadListWifi();

                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại, vui lòng thử lại.");
                }
               
            }
            catch
            {
                MessageBox.Show("Có lỗi xảy ra, vui lòng thử lại!");
            }
            //ucDanhSachWifi.LoadListWifi();
        }
        
        private bool ValidateEditForm()
        {
            if (tb_TenWifi.Text.Trim() == "")
            {
                txtValidateNameWifi.Text = "Tên wifi không được để trống!" as string;
                return false;
            }
            if (tb_DiaChiMac.Text.Trim() == "")
            {
                txtValidateNameIP.Text = "Địa chỉ Mac không được để trống!" as string;
                return false;
            }
           

            return true;
        }

        private  void bodCapNhatWifi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ValidateEditForm())
            {
                EditWifi();
            }
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitUpdateWifi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        
    }
}
