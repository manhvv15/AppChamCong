using ChamCong365.OOP.funcQuanLyCongTy;
//using ChamCong365.Popup.funcCompanyManager;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages.PageLichLamViecCaLamViec;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupQuanLyNhanVien;
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
using static QRCoder.PayloadGenerator;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.LichLamViecPopups
{
    /// <summary>
    /// Interaction logic for ucXoaNhanVienGioiHanIP.xaml
    /// </summary>
    public partial class ucXoaLichLamViec : UserControl
    {
        PageDanhSachLichLamViec page;
        frmMain Main;
        int cy_id;

        public ucXoaLichLamViec(frmMain Main, PageDanhSachLichLamViec page, int ID)
        {
            InitializeComponent();
            this.Main = Main;
            this.page = page;
            this.DataContext = this;
            this.cy_id = ID;


        }
        public async void DeleteCalendar()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, "http://210.245.108.202:3000/api/qlc/cycle/del");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(cy_id.ToString()), "cy_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root resultMessage = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {

                    Main.pnlShowPopUp.Children.Add(new ucPopupSuccess(resultMessage.data.message));
                    this.Visibility = Visibility.Collapsed;
                    page.GetCycleList();
                }
                else
                {
                    Main.pnlShowPopUp.Children.Add(new ucPopupError(resultMessage.error.message));
                }

            }
            catch (Exception ex) { Main.pnlShowPopUp.Children.Add(new ucPopupError("Có lỗi xảy ra")); }



        }





        private void ClosePopup(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Delete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DeleteCalendar();

        }


    }
}
