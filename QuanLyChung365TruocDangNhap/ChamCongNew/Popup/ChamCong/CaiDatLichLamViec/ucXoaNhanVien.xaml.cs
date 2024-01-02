using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
//using DocumentFormat.OpenXml.Spreadsheet;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec
{
    /// <summary>
    /// Interaction logic for ucDeleteSaff.xaml
    /// </summary>
    public partial class ucXoaNhanVien : UserControl
    {
        int cy_id = 0;
        int ep_id = 0;
        string Apply_moth = "";
        MainWindow Main;
        ucDanhSachNhanVien ucDanhSachNhanVien;
        CollectionViewSource Views;
        public ucXoaNhanVien(ucDanhSachNhanVien ucDanhSachNhanVien,MainWindow Main, int cy_id, int ep_id, string Apply_Month)
        {
            InitializeComponent();
            this.Main = Main;
            this.ucDanhSachNhanVien = ucDanhSachNhanVien;
            this.cy_id = cy_id;
            this.ep_id = ep_id;
            this.Apply_moth = Apply_Month;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private async void XacNhanXoa(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Delete_SaffInCalendar_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(cy_id.ToString()), "cy_id");
                content.Add(new StringContent(ep_id.ToString()), "ep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {


                    Console.WriteLine(await response.Content.ReadAsStringAsync());
                    this.Visibility = Visibility.Collapsed;
                    this.ucDanhSachNhanVien.Visibility = Visibility.Collapsed;  
                    Main.grShowPopup.Children.Add(new ucDanhSachNhanVien(Main, cy_id, Apply_moth));

                }

            }
            catch (System.Exception)
            {
            }
        }
    }
}
