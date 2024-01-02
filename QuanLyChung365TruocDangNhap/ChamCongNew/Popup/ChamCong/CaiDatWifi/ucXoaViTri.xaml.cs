using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.CaiDatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ViTri;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong
{
    /// <summary>
    /// Interaction logic for ucDelete.xaml
    /// </summary>
    public partial class ucXoaViTri : UserControl
    {

        BrushConverter bc = new BrushConverter();
        private ItemWifi ItemWifi;
        ucDanhSachViTri ucDanhSachViTri;
        MainWindow main;
        int cor_id;
        public ucXoaViTri(int cor_id, ucDanhSachViTri ucDanhSachViTri, MainWindow main)
        {
            InitializeComponent();
            this.cor_id = cor_id;
            this.ucDanhSachViTri = ucDanhSachViTri;
            this.main = main;

        }

        private void Border_MouseLeftButtonUp_OffDelete(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuy_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbHuy.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void bodHuy_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbHuy.Foreground = (Brush)bc.ConvertFrom("#4C5BD4 ");
        }

        private async void bodYesDelete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/location/delete");

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new StringContent("{\r\n    \"cor_id\": " + cor_id + "\r\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                Console.WriteLine(await response.Content.ReadAsStringAsync());


                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucDanhSachViTri.GetListLocation();
                }

            }
            catch (System.Exception)
            {


            }

        }
    }
}
