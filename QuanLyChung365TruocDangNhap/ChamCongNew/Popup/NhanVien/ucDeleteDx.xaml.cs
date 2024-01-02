using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.DetailOfDon;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json.Nodes;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.NhanVien
{
    /// <summary>
    /// Interaction logic for ucDeleteDx.xaml
    /// </summary>
    public partial class ucDeleteDx : UserControl
    {
        ucChiTietDeXuatCongTy ucChiTietDeXuatCongTy;
        ucChiTietDeXuat ucChiTietDeXuat;
        int dx_id = 0;
        bool isCom;
        public ucDeleteDx(ucChiTietDeXuat ucChiTietDeXuat, int dx_id)
        {
            InitializeComponent();
            this.ucChiTietDeXuat = ucChiTietDeXuat;

            this.dx_id = dx_id;

        }
        public ucDeleteDx(ucChiTietDeXuatCongTy ucChiTietDeXuatCongTy, int dx_id)
        {
            InitializeComponent();
            this.ucChiTietDeXuatCongTy = ucChiTietDeXuatCongTy;
            isCom = true;
            this.dx_id = dx_id;
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

        }

        private void OK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            XoaDX();

        }
        public async void XoaDX()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/deletedx/delete_dx");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var body = new
                {
                    type = 0,
                    id = dx_id
                };
                string json = JsonConvert.SerializeObject(body);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    if (isCom)
                    {

                        ucChiTietDeXuatCongTy.GetChiTietDeXuat();
                        return;

                    }
                    ucChiTietDeXuat.GetChiTietDeXuat();
                }

            }
            catch { }
        }
    }
}
