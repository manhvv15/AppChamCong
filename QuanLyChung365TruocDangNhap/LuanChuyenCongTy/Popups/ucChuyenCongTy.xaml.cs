using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.LuanChuyenCongTy.Entities;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities;
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

namespace QuanLyChung365TruocDangNhap.LuanChuyenCongTy.Popups
{
    /// <summary>
    /// Interaction logic for ucChuyenCongTy.xaml
    /// </summary>
    public partial class ucChuyenCongTy : UserControl
    {
        frmMain Main;
        BrushConverter br = new BrushConverter();
        List_NhanVien lstLuanChuyen = new List_NhanVien();
        ucLuanChuyenCongTy ucLuanChuyen;
        public ucChuyenCongTy(frmMain main, List_NhanVien lstluanchuyen, ucLuanChuyenCongTy ucluanlhuyen)
        {
            InitializeComponent();
            Main = main;
            lstLuanChuyen = lstluanchuyen;
            this.ucLuanChuyen = ucluanlhuyen;
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

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

        private async void bbtn_LuanChuyenCongTy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (string.IsNullOrEmpty(tb_IDCongTy.Text))
            {
                tb_ValidateIDCongTy.Visibility = Visibility.Visible;
                tb_ValidateIDCongTy.Text = "Bạn vui lòng nhập id công ty!";
                allow = false;
            }
            else
            {
                tb_ValidateIDCongTy.Visibility = Visibility.Collapsed;
            }
            if (string.IsNullOrEmpty(tb_TenCongTy.Text))
            {
                tb_ValidateTenCongTy.Visibility = Visibility.Visible;
                tb_ValidateTenCongTy.Text = "Bạn vui lòng search id công ty ở ô phía trên!";
                allow = false;
            }
            else
            {
                tb_ValidateTenCongTy.Visibility = Visibility.Collapsed;
            }
            if (allow)
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/changeCompany");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_IDCongTy.Text), "com_id");
                content.Add(new StringContent(lstLuanChuyen.ep_id.ToString()), "ep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resContent = await response.Content.ReadAsStringAsync();
                    ucLuanChuyen.LoadDanhSachNhanVien();
                    Main.pnlShowPopUp.Children.Add(new ucThongBaoThanhCong(Main, lstLuanChuyen));
                    this.Visibility = Visibility.Collapsed;
                }
                

                
            }
        }

        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility=Visibility.Collapsed;
        }

        private async void pat_SearchTenCongTy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/infoCompany");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_IDCongTy.Text), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resContent = await response.Content.ReadAsStringAsync();
                    Root_InforCompany infor = JsonConvert.DeserializeObject<Root_InforCompany>(resContent);
                    if (infor.data != null)
                    {
                        if (tb_IDCongTy.Text == infor.data.data.idQLC.ToString())
                        {
                            tb_TenCongTy.Text = infor.data.data.userName;
                            tb_TenCongTy.Foreground = (Brush)br.ConvertFrom("#474747");
                        }
                        else
                        {
                            tb_TenCongTy.Text = infor.data.data.userName;
                            tb_TenCongTy.Foreground = (Brush)br.ConvertFrom("#474747");
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        
    }
}
