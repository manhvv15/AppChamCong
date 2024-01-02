using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.CoCau_ViTri_ToChuc;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoToChuc
{
    /// <summary>
    /// Interaction logic for ucXoaViTri.xaml
    /// </summary>
    public partial class ucXoaToChuc : UserControl
    {
        BrushConverter br = new BrushConverter();
        public int org_id = 0;
        public string Token = "";
        MainWindow Main;
        public ucXoaToChuc(MainWindow Main, string name, int org_id)
        {
            InitializeComponent();
            this.Main = Main;
            this.org_id = org_id;
            this.Token = Properties.Settings.Default.Token;
            tb_TenTaiKhoan.Text = name;

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
            DeleteOrganize();
        }

        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        #region CallAPI
        public async void DeleteOrganize()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_delete_org);

                request.Headers.Add("authorization", "Bearer " + Token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(org_id.ToString()), "id");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucSoDoToChuc sodo = new ucSoDoToChuc(Main);
                    Main.dopBody.Children.Clear();
                    object Content = sodo.Content;
                    sodo.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                }

            }
            catch { }
        }
        #endregion
    }
}
