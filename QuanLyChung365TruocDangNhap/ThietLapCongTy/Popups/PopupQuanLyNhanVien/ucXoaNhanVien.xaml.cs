using QuanLyChung365TruocDangNhap.ThietLapCongTy.Them_Xoa_NhanVien;
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

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupQuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for ucXoaNhanVien.xaml
    /// </summary>
    public partial class ucXoaNhanVien : UserControl
    {
        BrushConverter br = new BrushConverter();
        ucTatCaNhanVien ucTatCaNhanVien;
        int ep_id = 0;
        DateTime startDate;
        int typeQuitJob;
        public ucXoaNhanVien(ucTatCaNhanVien ucTatCaNhanVien, int ep_id, string ep_name, DateTime startDate, int typeQuitJob)
        {
            InitializeComponent();
            this.ucTatCaNhanVien = ucTatCaNhanVien;
            this.ep_id = ep_id;
            tb_TenNhanVien.Text = ep_name;
            this.startDate = startDate;
            this.typeQuitJob = typeQuitJob;
        }
        private async void DeleteUser()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/deleteCompany");

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\"ep_id\":" + ep_id + ",\"created_at\": \"" + startDate.ToString("yyyy-MM-dd") + "\",\"type_quit_job\": " + typeQuitJob + " }", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                ucTatCaNhanVien.LoadDanhSachNhanVien();
                this.Visibility = Visibility.Collapsed;

            }
            catch
            {

            }
        }
        #region Hover Event
        private void bodQuayLai_MouseEnter(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#FF7A00");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodQuayLai_MouseLeave(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#FFFFFF");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#FF7A00");
        }

        private void bodLuuThongTinNhanVien_MouseEnter(object sender, MouseEventArgs e)
        {
            bodDongYXoa.BorderThickness = new Thickness(1);
        }

        private void bodLuuThongTinNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            bodDongYXoa.BorderThickness = new Thickness(0);
        }
        #endregion

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodDongYXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DeleteUser();
        }

        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
