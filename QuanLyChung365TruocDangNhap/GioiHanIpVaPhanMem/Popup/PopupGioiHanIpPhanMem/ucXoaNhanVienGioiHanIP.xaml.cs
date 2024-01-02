using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages;
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

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup
{
    /// <summary>
    /// Interaction logic for ucXoaNhanVienGioiHanIP.xaml
    /// </summary>
    public partial class ucXoaNhanVienGioiHanIP : UserControl
    {
        frmMain Main;
        SettingIpAppEntities.User User;

        List<int> listUsers = new List<int>();
        List<string> listIps = new List<string>();
        List<int> listApps = new List<int>();

        public class BodyData
        {
            public List<int> listUsers { get; set; }
            public List<string> listIps { get; set; }
            public List<int> listApps { get; set; }
            public string start_date { get; set; }
            public string end_date { get; set; }
        }
        public ucXoaNhanVienGioiHanIP(frmMain Main, SettingIpAppEntities.User User)
        {
            InitializeComponent();
            this.Main = Main;
            this.User = User;

        }
        private async void Setting()
        {
            try
            {
                listUsers.Add(User.ep_id);
                listApps = User.listApps.Select(x => int.Parse(x)).ToList();
                listIps = null;
                string start_date = User.start_date;
                string end_date = User.end_date;
                BodyData bodyData = new BodyData()
                {
                    listUsers = listUsers,
                    listApps = listApps,
                    listIps = listIps,
                    start_date = start_date,
                    end_date = end_date,

                };
                string bodyJson = JsonConvert.SerializeObject(bodyData, Formatting.Indented);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.APIs.IPnAPP_Setting);
                request.Headers.Add("Authorization", "Bearer " + Main.Tokens);
                var content = new StringContent(bodyJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {

                    PageDanhSachNhanVien qlnv = new PageDanhSachNhanVien(this.Main);
                    Main.stp_ShowPopup.Children.Clear();
                    object Content = qlnv.Content;
                    qlnv.Content = null;
                    Main.stp_ShowPopup.Children.Add(Content as UIElement);
                    this.Visibility = Visibility.Collapsed;
                }

            }
            catch { }
        }

        private void ClosePopup(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Delete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Setting();
            this.Main.pnlShowPopUp.Children.Add(new ucXoaThanhCong());

        }
    }
}
