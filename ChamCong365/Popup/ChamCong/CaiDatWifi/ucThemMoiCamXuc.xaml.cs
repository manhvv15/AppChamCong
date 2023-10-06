using ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi;
using Newtonsoft.Json;
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
    /// Interaction logic for ucThemMoiCamSuc.xaml
    /// </summary>
    public partial class ucThemMoiCamXuc : UserControl
    {
        MainWindow Main;
        private List_CamXuc CamXuc;
        ucCamXuc ucCamXuc;
        public ucThemMoiCamXuc(MainWindow main, List_CamXuc camXuc, ucCamXuc ucCamXuc)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            this.CamXuc = camXuc;
            this.ucCamXuc = ucCamXuc;
        }

        public async void ThemMoiCamXuc()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.ThemMoi_CamSuc_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_ThongTinCamXuc.Text), "emotion_detail");
                content.Add(new StringContent(tb_StartScore.Text), "min_score");
                content.Add(new StringContent(tb_EndScore.Text), "max_score");
                content.Add(new StringContent(CamXuc.avg_pass_score.ToString()), "avg_pass_score");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resCx = await response.Content.ReadAsStringAsync();
                    Root_CamXuc loadListWifi = JsonConvert.DeserializeObject<Root_CamXuc>(resCx);
                    this.Visibility = Visibility.Collapsed;
                    ucCamXuc.LoadCamXuc();
                }
            }
            catch (Exception)
            {
            }
        }

        private bool ValidateCamXuc()
        {
            if (String.IsNullOrEmpty(tb_StartScore.Text))
            {
                txtValidateStartScore.Text = "Điểm cảm xúc không được để trống!" as string;
                return false;
            }

            else if (String.IsNullOrEmpty(tb_EndScore.Text))
            {
                txtvalidateEndScore.Text = "Điểm cảm xúc không được để trống!" as string;
                return false;
            }

            else if (String.IsNullOrEmpty(tb_ThongTinCamXuc.Text))
            {
                txtvalidateThongTinCamXuc.Text = "Thông tin cảm xúc không được để trống!" as string;
                return false;
            }

            return true;
        }

        private void ExitCreateCX(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ThemMoiCX(object sender, MouseButtonEventArgs e)
        {
            if (ValidateCamXuc())
            {
                ThemMoiCamXuc();
            }
        }

        private void Thoat(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
