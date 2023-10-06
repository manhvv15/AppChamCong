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
    /// Interaction logic for ucXoaCamXuc.xaml
    /// </summary>
    public partial class ucXoaCamXuc : UserControl
    {
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        private List_CamXuc listCamXuc;
        ucCamXuc camXuc;
        public ucXoaCamXuc(MainWindow main,List_CamXuc listCamXuc, ucCamXuc camXuc)
        {
            InitializeComponent();
            Main = main;
            this.DataContext = this;
            this.listCamXuc = listCamXuc;
            this.camXuc = camXuc;
        }


        private void HuyXoa(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuy_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbHuy.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodHuy_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbHuy.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private async void XacNhanXoa(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Xoa_CamXuc_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(listCamXuc.emotion_id.ToString()), "emotion_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    camXuc.LoadCamXuc();
                }
            }
            catch (Exception)
            {
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
