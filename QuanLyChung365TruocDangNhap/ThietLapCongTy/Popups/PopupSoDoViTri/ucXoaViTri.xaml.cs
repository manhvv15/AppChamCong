using QuanLyChung365TruocDangNhap.ThietLapCongTy.CoCau_ViTri_ToChuc;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Comons;
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

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupSoDoViTri
{
    /// <summary>
    /// Interaction logic for ucXoaViTri.xaml
    /// </summary>
    public partial class ucXoaViTri : UserControl
    {
        frmMain Main;
        ucSoDoViTRi ucSoDoViTRi;
        int id;
        BrushConverter br = new BrushConverter();
        public ucXoaViTri(frmMain main, ucSoDoViTRi ucSoDoViTRi, int id)
        {
            InitializeComponent();
            Main = main;
            this.ucSoDoViTRi = ucSoDoViTRi;
            this.id = id;
        }
        private async void DeletePosition()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_delete_position);

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\"id\":" + id + "}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                this.Visibility = Visibility.Collapsed;

                ucSoDoViTRi sodo = new ucSoDoViTRi(Main);
                Main.stp_ShowPopup.Children.Clear();
                object Content = sodo.Content;
                sodo.Content = null;
                Main.stp_ShowPopup.Children.Add(Content as UIElement);


            }
            catch { }
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
            DeletePosition();
        }

        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

    }
}
