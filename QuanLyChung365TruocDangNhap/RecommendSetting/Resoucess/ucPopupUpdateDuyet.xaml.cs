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

namespace QuanLyChung365TruocDangNhap.RecommendSetting.Resoucess
{
    /// <summary>
    /// Interaction logic for ucPopupUpdateDuyet.xaml
    /// </summary>
    public partial class ucPopupUpdateDuyet : UserControl
    {
        frmMain Main;
        int id;
        public ucPopupUpdateDuyet(frmMain main, int id)
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

        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/settingConfirm/updateAllSettingConfirmLevel");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(textNhap.Text), "confirm_level");
                content.Add(new StringContent(id.ToString()), "listUsers[]");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                if (response.IsSuccessStatusCode)
                {
                    ucRecommended uc = new ucRecommended(Main);
                    Main.stp_ShowPopup.Children.Clear();
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.stp_ShowPopup.Children.Add(Content as UIElement);
                    this.Visibility = Visibility.Collapsed;
                }             

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("loi update cap duyet"+ex.Message);
            }
        }
    }
}
