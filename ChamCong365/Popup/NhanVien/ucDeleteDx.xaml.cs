using ChamCong365.NhanVien.DetailOfDon;
using ChamCong365.NhanVien.Propose;
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

namespace ChamCong365.Popup.NhanVien
{
    /// <summary>
    /// Interaction logic for ucDeleteDx.xaml
    /// </summary>
    public partial class ucDeleteDx : UserControl
    {
        ucChiTietDeXuat ucChiTietDeXuat;
        int dx_id = 0;
        public ucDeleteDx(ucChiTietDeXuat ucChiTietDeXuat,int dx_id)
        {
            InitializeComponent();
            this.ucChiTietDeXuat=ucChiTietDeXuat;

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
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("0"), "type");
                content.Add(new StringContent(dx_id.ToString()), "id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucChiTietDeXuat.GetChiTietDeXuat();
                }

            }
            catch { }
        }
    }
}
