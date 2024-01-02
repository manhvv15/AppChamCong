using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
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
    /// Interaction logic for ucThemCap.xaml
    /// </summary>
    public partial class ucThemCap : UserControl
    {
        frmMain Main;
        int level;
        BrushConverter br = new BrushConverter();
        public ucThemCap(frmMain main, int level)
        {
            InitializeComponent();
            Main = main;
            this.level = level;
        }
        private async void CreatePosition()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_create_position);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                int typeAdd = 1;
                if (cboLeverCap.SelectedIndex == 0)
                    typeAdd = 1;
                else if (cboLeverCap.SelectedIndex == 1)
                    typeAdd = 2;

                var content = new StringContent("{\"level\":" + level + ",\"typeAdd\":\"" + typeAdd + "\",\"positionName\":\"" + textNhapTenCap.Text + "\"}", null, "application/json");
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
        #region Clicl Event
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
                CreatePosition();
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
