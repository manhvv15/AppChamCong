using Microsoft.Win32;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.Login;
using QuanLyChung365TruocDangNhap.LuanChuyenCongTy.Popups;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan
{
    /// <summary>
    /// Interaction logic for ucDanhGia.xaml
    /// </summary>
    public partial class ucDanhGia_BaoLoi : UserControl
    {
        frmMain Main;
        public ucDanhGia_BaoLoi(frmMain main)
        {
            InitializeComponent();
            btn_AnhDanhGia.Visibility = Visibility.Visible;
            CountDanhGia.Visibility = Visibility.Visible;
            btn_DanhGia.Visibility = Visibility.Visible;
            Main = main;
        }

        int ucDanhGia;
        public ucDanhGia_BaoLoi(frmMain main, int danhgia)
        {
            InitializeComponent();
            Main = main;
            ucDanhGia = danhgia;
            btn_AnhDanhGia.Visibility = Visibility.Collapsed;
            CountDanhGia.Visibility = Visibility.Collapsed;
            btn_DanhGia.Visibility = Visibility.Collapsed;

            btn_AnhBaoLoi.Visibility = Visibility.Visible;
            btn_AnhBaoLoi.Width = 200;
            btn_AnhBaoLoi.Height = 250;
            btn_AnhBaoLoi.Margin = new Thickness(0, 0, 0, 20);
            tb_TextMo.Text = "Hãy cho chúng tôi biết vấn đề bạn gặp phải";
            gr_BaoLoi.Visibility = Visibility.Visible;
            
        }

        string Content;
        public async void DanhGia()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/Feedback");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_DanhGia_BaoLoi.Text), "feed_back");
                content.Add(new StringContent(CountDanhGia.rating.ToString()), "rating");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var rescontent = await response.Content.ReadAsStringAsync();
                    Main.pnlShowPopUp.Children.Add(new ucThongBaoThanhCong(tb_DanhGia_BaoLoi.Text));
                }
            }
            catch (Exception)
            {}
        }

        private void btn_DanhGia_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (string.IsNullOrEmpty(tb_DanhGia_BaoLoi.Text) || CountDanhGia.rating <= 0)
            {
                tb_ValidateDanhGia_BaoLoi.Visibility = Visibility.Visible;
                tb_ValidateDanhGia_BaoLoi.Text = "Bạn vui lòng nhập đánh giá và chọn số sao cho phần mềm";
                allow = false;
            }
            else
            {
                tb_ValidateDanhGia_BaoLoi.Visibility = Visibility.Collapsed;
            }
            if (allow)
            {
                DanhGia();
            }
        }
        byte[] imageBytes;
        private void btn_ChonAnhLoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tệp ảnh (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Tất cả các tệp (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri(selectedImagePath)));
                imageBytes  = File.ReadAllBytes(selectedImagePath);
                tb_CountImage.Visibility = Visibility.Visible;

            }
           
        }

        private async void btn_BaoLoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (string.IsNullOrEmpty(tb_DanhGia_BaoLoi.Text))
            {
                tb_ValidateDanhGia_BaoLoi.Visibility = Visibility.Visible;
                tb_ValidateDanhGia_BaoLoi.Text = "Bạn vui lòng cho chúng tôi biết lỗi mà bạn gặp phải và chọn ảnh nếu có ";
                allow = false;
            }
            else
            {
                tb_ValidateDanhGia_BaoLoi.Visibility = Visibility.Collapsed;
            }
            if (allow)
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/ReportError");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(tb_DanhGia_BaoLoi.Text), "detail_error");
                    content.Add(new StringContent(imageBytes.ToString()), "gallery_image_error");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var rescontent = await response.Content.ReadAsStringAsync();
                        Main.pnlShowPopUp.Children.Add(new ucThongBaoThanhCong(imageBytes));
                    }
                }
                catch (Exception)
                {
                    decimal error = 2;
                    Main.pnlShowPopUp.Children.Add(new ucThongBaoThanhCong(error));
                }
            }
        }
    }
}
