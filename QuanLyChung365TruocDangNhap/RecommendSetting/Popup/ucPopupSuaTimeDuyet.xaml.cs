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

namespace QuanLyChung365TruocDangNhap.RecommendSetting.Popup
{
    /// <summary>
    /// Interaction logic for ucPopupSuaTimeDuyet.xaml
    /// </summary>
    public partial class ucPopupSuaTimeDuyet : UserControl
    {
        frmMain Main;
        int id;
        public ucPopupSuaTimeDuyet(frmMain main, int id)
        {
            InitializeComponent();
            Main = main;
            this.id = id;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Path_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private async void UpdateTimeDuyet()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/setting/updateTimeSetting");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                // id_Update = ((TimeDx)dgvTimeKeHoach.SelectedItem)._id;
                content.Add(new StringContent(id.ToString()), "id_dx");

                content.Add(new StringContent(textNhap.Text), "time");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                // getDsTimeKH(pageKH.SelectedPage);
            }

            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi update thời gian duyệt " + ex.Message);
            }
        }
        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            UpdateTimeDuyet();
            this.Visibility = Visibility.Collapsed;
        }
    }
}
