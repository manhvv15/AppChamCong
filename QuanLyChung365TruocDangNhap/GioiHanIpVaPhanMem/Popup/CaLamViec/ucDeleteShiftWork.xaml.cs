
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.APIs;
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

namespace QuanLyChung365TruocDangNhap.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucDeleteShiftWork.xaml
    /// </summary>
    public partial class ucDeleteShiftWork : UserControl
    {
        ucShiftWorkManager ucShiftWorkManager;
        BrushConverter br = new BrushConverter();
        string shift_id;
        public ucDeleteShiftWork(ucShiftWorkManager ucShiftWorkManager, string shift_id)
        {
            InitializeComponent();
            this.ucShiftWorkManager = ucShiftWorkManager;
            this.shift_id = shift_id;
        }
        private async void DeleteShift()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, APIs.delete_shift_api);
            request.Headers.Add("Authorization", "Bearer "+Properties.Settings.Default.Token);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(shift_id), "shift_id");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                ucShiftWorkManager.GetListShift();
            };

        }

        #region Hover Event
        private void bodQuayLai_MouseEnter(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#4C5BD4");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodQuayLai_MouseLeave(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#FFFFFF");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
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
            this.Visibility = Visibility.Collapsed;
            DeleteShift();
        }

        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
