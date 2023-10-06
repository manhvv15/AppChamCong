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
using System.Windows.Shapes;

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for capNhatThanhCong.xaml
    /// </summary>
    public partial class capNhatThanhCong : Window
    {
        MainChamCong Main;
        ChupAnh Chup;
        string Image;
        public capNhatThanhCong(MainChamCong main, ChupAnh chup, string image)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            this.Chup = chup;
            this.Image = image;
        }
    

        private void HuyBo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popupXacNhanChapNhat.Visibility = Visibility.Collapsed;
        }

        private async void CapNhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/ai/updateFaceNew");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.ComdID.ToString()), "company_id ");
                content.Add(new StringContent(Main.Ep_Id.ToString()), "user_id");
                content.Add(new StringContent(Image), "image");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());


            }
            catch (Exception)
            {
                popupXacNhanChapNhat.Visibility = Visibility.Collapsed;
            }
        }
    }
}
