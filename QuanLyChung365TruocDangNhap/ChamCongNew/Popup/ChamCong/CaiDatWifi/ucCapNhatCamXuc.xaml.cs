using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using System;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucCapNhatCamXuc.xaml
    /// </summary>
    public partial class ucCapNhatCamXuc : UserControl
    {
        MainWindow Main;
        private List_CamXuc ListCamXuc;
        BrushConverter bc = new BrushConverter();
        ucCamXuc CamXuc;
        public ucCapNhatCamXuc(MainWindow main,List_CamXuc listCamXuc,ucCamXuc camXuc)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            this.ListCamXuc = listCamXuc;
            this.CamXuc = camXuc;
            tb_StartUpdateScore.Text = ListCamXuc.min_score.ToString();
            tb_EndUpdateScore.Text = ListCamXuc.max_score.ToString();
            tb_UpdateThongTinCamXuc.Text = ListCamXuc.emotion_detail.ToString();
        }
        public async void CapNhatCamXuc()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.CapNhat_CamXuc_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_UpdateThongTinCamXuc.Text), "emotion_detail");
                content.Add(new StringContent(tb_StartUpdateScore.Text), "min_score");
                content.Add(new StringContent(tb_EndUpdateScore.Text), "max_score");
                content.Add(new StringContent(ListCamXuc.emotion_id.ToString()), "emotion_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    this.Visibility = Visibility.Collapsed;
                    CamXuc.LoadCamXuc();
                }
            }
            catch (Exception)
            {
            }
        }
        private void CapNhatCamSuc(object sender, MouseButtonEventArgs e)
        {
            if (ValidateCamXuc())
            {
                CapNhatCamXuc();
            }
        }

        private bool ValidateCamXuc()
        {
            if (String.IsNullOrEmpty(tb_StartUpdateScore.Text))
            {
                txtValidateStartUpdateScore.Text = "Điểm cảm xúc không được để trống!" as string;
                return false;
            }

            else if (String.IsNullOrEmpty(tb_EndUpdateScore.Text))
            {
                txtValidateEndUpdateScore.Text = "Điểm cảm xúc không được để trống!" as string;
                return false;
            }

            else if (String.IsNullOrEmpty(tb_UpdateThongTinCamXuc.Text))
            {
                txtValidateUpdateThongTinCamXuc.Text = "Thông tin cảm xúc không được để trống!" as string;
                return false;
            }

            return true;
        }

        private void ExitUpdateCX(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Thoat(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnHuy_MouseEnter(object sender, MouseEventArgs e)
        {
            btnHuy.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txtHuy.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void btnHuy_MouseLeave(object sender, MouseEventArgs e)
        {
            btnHuy.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txtHuy.Foreground = (Brush)bc.ConvertFrom("#4C5BD4");
        }
    }
}
