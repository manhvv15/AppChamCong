using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ViTri.API_Location;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatQR;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.QR;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.CaiDatQR
{
    /// <summary>
    /// Interaction logic for ucCreateQR.xaml
    /// </summary>
    public partial class ucChinhSuaQR : UserControl
    {
        MainWindow Main;
        private ucListQR ucListQR;
        int id;
        public ucChinhSuaQR(MainWindow main, int id, string qrName, ucListQR ucListQR)
        {
            InitializeComponent();
            this.ucListQR = ucListQR;
            Main = main;
            this.id = id;
            tb_TenQR.Text = qrName;

        }
        public async void UpdateQR()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/qrCode/update");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_TenQR.Text), "QRCodeName");
                content.Add(new StringContent(id.ToString()), "id");

                request.Content = content;
                var response = await client.SendAsync(request);
                var responsQR = await response.Content.ReadAsStringAsync();

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucListQR.GetListQR();
                }
            }
            catch
            {

            }
        }

        private bool ValidateAddForm()
        {
            txtValidateNameQR.Visibility = Visibility.Collapsed;

            if (String.IsNullOrEmpty(tb_TenQR.Text))
            {
                txtValidateNameQR.Text = "Tên QR không được để trống!" as string;
                txtValidateNameQR.Visibility = Visibility.Visible;
                return false;
            }

            return true;
        }

        private void CreateQR_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitCreateQR_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }



        private void bodThemMoiQR_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (ValidateAddForm())
            {
                UpdateQR();
            }

        }


    }
}
