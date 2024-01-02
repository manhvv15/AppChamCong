using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.CaiDatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong
{
    /// <summary>
    /// Interaction logic for ucDelete.xaml
    /// </summary>
    public partial class ucXoaWifi : UserControl
    {

        BrushConverter bc = new BrushConverter();
        private ItemWifi ItemWifi;
        ucDanhSachWifi ucDanhSachWii;
        MainWindow main;
        int id;
        public ucXoaWifi(ItemWifi itemWifi, ucDanhSachWifi ucDanhSachWii, MainWindow main)
        {
            InitializeComponent();
            this.ItemWifi = itemWifi;
            this.ucDanhSachWii = ucDanhSachWii;
            this.main = main;
            id = main.IdAcount;
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
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.delete_wifi_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ItemWifi.id.ToString()), "id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    // ucDanhSachWifi.LoadListWifi();

                    ucCaiDatBaoMatWifi uc = new ucCaiDatBaoMatWifi(main, id);
                    main.dopBody.Children.Clear();
                    object Content = uc.Content;
                    uc.Content = null;
                    main.dopBody.Children.Add(Content as UIElement);
                    ucDanhSachWii.LoadListWifi();
                }

            }
            catch (System.Exception)
            {


            }

        }
    }
}
