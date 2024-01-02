using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
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
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Them_Xoa_NhanVien
{
    /// <summary>
    /// Interaction logic for ucQuanLyNhanVien.xaml
    /// </summary>
    public partial class ucQuanLyNhanVien : UserControl
    {
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        public string tokens;
        public string idAcount;

        public ucQuanLyNhanVien(MainWindow main)
        {
            this.tokens = Properties.Settings.Default.Token;
            this.idAcount = main.IdAcount.ToString();
            GetListOrganize();
            InitializeComponent();
            Main = main;
            bod_TatCaNhanVien.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            bod_TatCaNhanVien.Background = (Brush)br.ConvertFrom("#4C5DB4");
            tb_TatCaNhanVien.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            //grHienThiNhanVien.Children.Add(new ucTatCaNhanVien(main));
        }

        #region GetListOrganize
        private List<ListOrganizeEntities.OrganizeData> _lstOrganizeData;
        public List<ListOrganizeEntities.OrganizeData> lstOrganizeData
        {
            get { return _lstOrganizeData; }
            set { _lstOrganizeData = value; }
        }

        Dictionary<string, string> ListOrganize = new Dictionary<string, string>();
        Dictionary<string, string> ListPosition = new Dictionary<string, string>();
        public async void GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.listAll_organize);

                request.Headers.Add("authorization", "Bearer " + tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(idAcount), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);

                    if (result.data.data != null)
                    {
                        lstOrganizeData = result.data.data;

                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        #region GetPosition
        private List<ListPositionEntities.PositionData> _lstPositionData;
        public List<ListPositionEntities.PositionData> lstPositionData
        {
            get { return _lstPositionData; }
            set { _lstPositionData = value; }
        }
        private async void GetListPosition()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.list_position);
                request.Headers.Add("authorization", "Bearer " + tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(idAcount), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListPositionEntities.Root result = JsonConvert.DeserializeObject<ListPositionEntities.Root>(responseContent);
                    if (result.data.data != null)
                    {
                        lstPositionData = result.data.data;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion

        #region Even GiaoDien
        private void TatCaNhanVien(object sender, MouseButtonEventArgs e)
        {

            bod_TatCaNhanVien.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            bod_TatCaNhanVien.Background = (Brush)br.ConvertFrom("#4C5DB4");
            tb_TatCaNhanVien.Foreground = (Brush)br.ConvertFrom("#FFFFFF");

            bod_NhanVienChoDuyet.BorderBrush = (Brush)br.ConvertFrom("#6666");
            bod_NhanVienChoDuyet.Background = (Brush)br.ConvertFrom("#D9EAFF");
            tb_NhanVienChoDuyet.Foreground = (Brush)br.ConvertFrom("#4C5DB4");

            bod_NhanVienNghiViec.BorderBrush = (Brush)br.ConvertFrom("#6666");
            bod_NhanVienNghiViec.Background = (Brush)br.ConvertFrom("#D9EAFF");
            tb_NhanVienNghiViec.Foreground = (Brush)br.ConvertFrom("#4C5DB4");
            if (grHienThiNhanVien.Children != null)
            {
                grHienThiNhanVien.Children.Clear();
                // grHienThiNhanVien.Children.Add(new ucTatCaNhanVien(Main));
            }

        }
        private void NhanVienChoDuyet(object sender, MouseButtonEventArgs e)
        {

            bod_NhanVienChoDuyet.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            bod_NhanVienChoDuyet.Background = (Brush)br.ConvertFrom("#4C5DB4");
            tb_NhanVienChoDuyet.Foreground = (Brush)br.ConvertFrom("#FFFFFF");

            bod_TatCaNhanVien.BorderBrush = (Brush)br.ConvertFrom("#6666");
            bod_TatCaNhanVien.Background = (Brush)br.ConvertFrom("#D9EAFF");
            tb_TatCaNhanVien.Foreground = (Brush)br.ConvertFrom("#4C5DB4");

            bod_NhanVienNghiViec.BorderBrush = (Brush)br.ConvertFrom("#6666");
            bod_NhanVienNghiViec.Background = (Brush)br.ConvertFrom("#D9EAFF");
            tb_NhanVienNghiViec.Foreground = (Brush)br.ConvertFrom("#4C5DB4");

            if (grHienThiNhanVien.Children != null)
            {
                grHienThiNhanVien.Children.Clear();
                // grHienThiNhanVien.Children.Add(new ucDanhSachChoDuyet(Main));
            }

        }
        private void NhanVienNghiViec(object sender, MouseButtonEventArgs e)
        {
            bod_NhanVienNghiViec.BorderBrush = (Brush)br.ConvertFrom("#4C5DB4");
            bod_NhanVienNghiViec.Background = (Brush)br.ConvertFrom("#4C5DB4");
            tb_NhanVienNghiViec.Foreground = (Brush)br.ConvertFrom("#FFFFFF");

            bod_NhanVienChoDuyet.BorderBrush = (Brush)br.ConvertFrom("#6666");
            bod_NhanVienChoDuyet.Background = (Brush)br.ConvertFrom("#D9EAFF");
            tb_NhanVienChoDuyet.Foreground = (Brush)br.ConvertFrom("#4C5DB4");

            bod_TatCaNhanVien.BorderBrush = (Brush)br.ConvertFrom("#6666");
            bod_TatCaNhanVien.Background = (Brush)br.ConvertFrom("#D9EAFF");
            tb_TatCaNhanVien.Foreground = (Brush)br.ConvertFrom("#4C5DB4");
            if (grHienThiNhanVien.Children != null)
            {
                grHienThiNhanVien.Children.Clear();
                grHienThiNhanVien.Children.Add(new ucDanhSachNghiViec(Main, this));
            }
        }
        #endregion
    }
}
