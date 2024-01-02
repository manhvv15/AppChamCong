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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucDuyetNoti.xaml
    /// </summary>
    public partial class ucDuyetNoti : UserControl
    {
        MainWindow Main;
        string listId;
        ucCapNhatKhuonMatMoi Uc;
        public ucDuyetNoti(MainWindow main, string listId, ucCapNhatKhuonMatMoi uc)
        {
            InitializeComponent();
            Main = main;
            this.listId = listId;
            Uc = uc;    
        }

        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/face/add");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(listId), "list_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if(response.IsSuccessStatusCode)
                {
                    ThongBao.Visibility = Visibility.Collapsed;
                    Uc.getListUpdateFace();
                    //Uc.dgvCapNhapKhuonMat.Items.Refresh();
                }


            }
            catch (Exception ex)
            {
                ThongBao.Visibility = Visibility.Collapsed;
            }
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ThongBao.Visibility = Visibility.Collapsed;
        }
    }
}
