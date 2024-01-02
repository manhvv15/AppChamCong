using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Them_Xoa_NhanVien;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupQuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for ucLuanChuyenPhongBan.xaml
    /// </summary>
    public partial class ucLuanChuyenPhongBan : UserControl
    {
        BrushConverter br = new BrushConverter();
        ucTatCaNhanVien ucTatCaNhanVien;
        List<ListOrganizeEntities.OrganizeData> lstOrganizeData;
        int ep_id;
        public ucLuanChuyenPhongBan(ucTatCaNhanVien ucTatCaNhanVien, List<ListOrganizeEntities.OrganizeData> lstOrganizeData, int org_id, int ep_id)
        {
            InitializeComponent();
            this.ep_id = ep_id;
            this.ucTatCaNhanVien = ucTatCaNhanVien;
            this.lstOrganizeData = lstOrganizeData;
            cboTenToChuc.ItemsSource = lstOrganizeData;
            cboTenToChuc.SelectedItem = lstOrganizeData.Where(x => x.id == org_id).FirstOrDefault();
        }
        private async void LuanChuyenPhongBan()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/changeOrganizeDetail");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var selectedNv = cboTenToChuc.SelectedItem as ListOrganizeEntities.OrganizeData;

                var bodyObject = new
                {
                    ep_id = ep_id,
                    organizeDetailId = selectedNv.id,
                    listOrganizeDetailId = selectedNv.listOrganizeDetailId

                };
                string bodyJson = JsonConvert.SerializeObject(bodyObject, Formatting.Indented);
                var content = new StringContent(bodyJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                this.Visibility = Visibility.Collapsed;
                ucTatCaNhanVien.LoadDanhSachNhanVien();

            }
            catch { }
        }
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
            bodLuuThongTinLuanChuyen.BorderThickness = new Thickness(1);
        }

        private void bodLuuThongTinNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            bodLuuThongTinLuanChuyen.BorderThickness = new Thickness(0);
        }
        #endregion

        #region Click Event
        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodLuuThongTinLuanChuyen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LuanChuyenPhongBan();
        }

        private void bodExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        #endregion
        private void cboTenToChuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboTenToChuc.SelectedIndex = -1;
                string textSearch = cboTenToChuc.Text;
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cboTenToChuc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboTenToChuc.SelectedIndex = -1;
            string textSearch = cboTenToChuc.Text + e.Text;
            cboTenToChuc.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboTenToChuc.Text = "";
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData;
                cboTenToChuc.SelectedIndex = -1;
            }
            else
            {
                cboTenToChuc.ItemsSource = "";
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }

        }
    }
}
