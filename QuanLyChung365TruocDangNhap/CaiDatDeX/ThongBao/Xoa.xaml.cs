
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

namespace QuanLyChung365TruocDangNhap.CaiDatDeX.ThongBao
{
    /// <summary>
    /// Interaction logic for Xoa.xaml
    /// </summary>
    public partial class Xoa : UserControl
    {
        frmMain Main;
        int id;
        public Xoa(frmMain main, int id)
        {
            InitializeComponent();
            Main = main;
            this.id = id;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            thongbaoxoa.Visibility = Visibility.Collapsed;
        }

        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingTimesheet/del");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(id.ToString()), "setting_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if(response.IsSuccessStatusCode)
                {

                    thongbaoxoa.Visibility = Visibility.Collapsed;
                    //ucDetailIP uc = new ucDetailIP(Main);
                    //Main.dopBody.Children.Clear();
                    //object Content = uc.Content;
                    //uc.Content = null;
                    //Main.dopBody.Children.Add(Content as UIElement);
                }
                

            }
            catch (Exception ex)
            {

            }
        }
    }
}
