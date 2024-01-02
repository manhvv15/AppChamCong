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
using static QRCoder.PayloadGenerator;

namespace QuanLyChung365TruocDangNhap.RecommendSetting
{
    /// <summary>
    /// Interaction logic for ucTimeRecommend.xaml
    /// </summary>
    public partial class ucTimeRecommend : UserControl
    {
        public ucTimeRecommend()
        {
            InitializeComponent();
        }

        private void Image_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {

        }
        public int i;
        private async void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/settingConfirm/updateAllSettingConfirmLevel");
            //    request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("20"), "confirm_level");
                content.Add(new StringContent("10000859"), "listUsers[]");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("loi update cap duyet");
            }
        }
    }
}
