using ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi;
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

namespace ChamCong365.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucXoaIP.xaml
    /// </summary>
    public partial class ucXoaIP : UserControl
    {
        BrushConverter br = new BrushConverter();
        private ListIP listIP;
        ucDanhSachIP ucDanhSachIP;
        public ucXoaIP(ListIP listIP, ucDanhSachIP ucDanhSachIP)
        {
            InitializeComponent();
            this.listIP = listIP;
            this.ucDanhSachIP = ucDanhSachIP;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuy_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbHuy.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodHuy_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbHuy.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void Border_MouseLeftButtonUp_OffDelete(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private async void bodYesDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, APIs.API.delete_ip_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(listIP.id_acc.ToString()), "id_acc");
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
    }
}
