using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.CoCau_ViTri_ToChuc;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Comons;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupSoDoViTri
{
    /// <summary>
    /// Interaction logic for ucThemMoiViTri.xaml
    /// </summary>
    public partial class ucThemMoiViTri : UserControl
    {
        frmMain Main;
        BrushConverter br = new BrushConverter();
        public ucThemMoiViTri(frmMain main)
        {
            InitializeComponent();
            Main = main;

        }
        public ucThemMoiViTri(frmMain main, List<ListPositionEntities.PositionData> listAllPosition)
        {
            InitializeComponent();
            Main = main;
            cboCacCapViTri.ItemsSource = listAllPosition;

        }
        #region Click Event
        private void bodExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCreate_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CreatePosition();
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

        #region CallApi
        private async void CreatePosition()
        {
            try
            {
                if (Validate())
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_create_position);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    int typeAdd = 1;
                    if (cboLeverCap.SelectedIndex == 0)
                        typeAdd = 1;
                    else if (cboLeverCap.SelectedIndex == 1)
                        typeAdd = 2;
                    var selectedLevel = cboCacCapViTri.SelectedItem as ListPositionEntities.PositionData;
                    var content = new StringContent("{\"level\":" + selectedLevel.level + ",\"typeAdd\":\"" + typeAdd + "\",\"positionName\":\"" + textNhapTenViTri.Text + "\"}", null, "application/json");
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
            }
            catch { }
        }
        #endregion
        private bool Validate()
        {
            tb_ValidateTenViTri.Visibility = Visibility.Collapsed;
            tb_ValidateCapBac.Visibility = Visibility.Collapsed;

            if (textNhapTenViTri.Text.Trim() == "")
            {
                tb_ValidateTenViTri.Text = "Tên vị trí không được để trống";
                tb_ValidateTenViTri.Visibility = Visibility.Visible;
                return false;
            }
            if (cboCacCapViTri.SelectedIndex == -1)
            {
                tb_ValidateCapBac.Text = "Vui lòng chọn cấp bậc";
                tb_ValidateCapBac.Visibility = Visibility.Visible;
                return false;
            }
            return true;
        }
    }
}
