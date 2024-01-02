using QuanLyChung365TruocDangNhap.Hr.Entities.AdministrationEntity.EmployeeManager;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
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

namespace QuanLyChung365TruocDangNhap.ChotDonTu.Popup
{
    /// <summary>
    /// Interaction logic for Xoa.xaml
    /// </summary>
    public partial class Xoa : UserControl
    {
        frmMain Main;
        ucChotDonTu UcChonDon;
        int id;
        public Xoa(frmMain main, ucChotDonTu ucChonDon, int id)
        {
            InitializeComponent();
            Main = main;
            UcChonDon = ucChonDon;
            this.id = id;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Xoa11.Visibility = Visibility.Collapsed;
        }

        private void huy(object sender, MouseButtonEventArgs e)
        {
            Xoa11.Visibility = Visibility.Collapsed;
        }

        private async void hoanThanh(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/chotDonTu/delete");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(id.ToString()), "id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    XoaThanhCong uc = new XoaThanhCong(Main);
                    // Main.stp_ShowPopup.Children.Clear();
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.pnlShowPopUp.Children.Add(Content as UIElement);
                    Xoa11.Visibility = Visibility.Collapsed;
                    UcChonDon.getList();
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
