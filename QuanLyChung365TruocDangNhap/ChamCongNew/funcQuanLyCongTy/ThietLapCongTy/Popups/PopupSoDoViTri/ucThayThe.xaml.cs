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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoViTri
{
    /// <summary>
    /// Interaction logic for ucThayThe.xaml
    /// </summary>
    public partial class ucThayThe : UserControl
    {
        MainWindow Main;
        int id;
        BrushConverter br = new BrushConverter();
        public ucThayThe(MainWindow main, int id, string name)
        {
            InitializeComponent();
            Main = main;
            this.id = id;
            textNhapTenCap.Text = name;
        }
        private async void UpdatePosition()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_update_position);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\"id\":\"" + id + "\",\"positionName\":\"" + textNhapTenCap.Text + "\"}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                this.Visibility = Visibility.Collapsed;
                ucSoDoViTRi sodo = new ucSoDoViTRi(Main);
                Main.dopBody.Children.Clear();
                object Content = sodo.Content;
                sodo.Content = null;
                Main.dopBody.Children.Add(Content as UIElement);


            }
            catch { }
        }
        private bool Validate()
        {
            tb_ValidateTenCap.Visibility = Visibility.Collapsed;


            if (textNhapTenCap.Text.Trim() == "")
            {
                tb_ValidateTenCap.Text = "Tên vị trí không được để trống";
                tb_ValidateTenCap.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }
        #region Click Event
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void bodLuu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (Validate())
            {

                UpdatePosition();
            }
        }
        #endregion
        #region Hover Event
        private void bodQuayLai_MouseEnter(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#4C5DB4");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodQuayLai_MouseLeave(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#FFFFFF");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#4C5DB4");
        }

        private void bodLuuThongTinNhanVien_MouseEnter(object sender, MouseEventArgs e)
        {
            bodLuuThongTinNhanVien.BorderThickness = new Thickness(1);
            bodLuuThongTinNhanVien.Background = (Brush)br.ConvertFrom("#339DFA");
        }

        private void bodLuuThongTinNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            bodLuuThongTinNhanVien.BorderThickness = new Thickness(0);
            bodLuuThongTinNhanVien.Background = (Brush)br.ConvertFrom("#4C5DB4");
        }
        #endregion


    }
}
