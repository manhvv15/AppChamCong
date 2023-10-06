using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ChamCong365.Popups.ChamCong.CaiDatWifi;

namespace ChamCong365.Popups.ChamCong
{
    /// <summary>
    /// Interaction logic for ucDelete.xaml
    /// </summary>
    public partial class ucXoaWifi : UserControl
    {
       
        BrushConverter bc = new BrushConverter();
        private ItemWifi ItemWifi;
        ucDanhSachWii ucDanhSachWii;
        public ucXoaWifi(ItemWifi itemWifi, ucDanhSachWii ucDanhSachWii)
        {
            InitializeComponent();
            this.ItemWifi = itemWifi;
            this.ucDanhSachWii = ucDanhSachWii;
        }

        private void Border_MouseLeftButtonUp_OffDelete(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuy_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbHuy.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void bodHuy_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbHuy.Foreground = (Brush)bc.ConvertFrom("#4C5BD4 ");
        }

        private async void bodYesDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.delete_wifi_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ItemWifi.wifi_id.ToString()), "wifi_id");
                request.Content = content;
                var response = await client.SendAsync(request);
               
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucDanhSachWii.LoadListWifi();

                }

            }
            catch (System.Exception)
            {

               
            }
           
        }
    }
}
