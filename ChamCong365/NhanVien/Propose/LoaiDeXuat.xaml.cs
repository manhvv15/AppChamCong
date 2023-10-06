using Newtonsoft.Json;
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
using System.Windows.Shapes;
using static ChamCong365.OOP.NhanVien.ToiGuiDi.DeXuatToiGuiDi;

namespace ChamCong365.NhanVien.Propose
{
    /// <summary>
    /// Interaction logic for LoaiDeXuat.xaml
    /// </summary>
    public partial class LoaiDeXuat : Window
    {
        List<Data> listDataToiGuiDi = new List<Data>();
        MainChamCong Main;
        public LoaiDeXuat(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            getData();
            

        }
        ChamCong365.OOP.NhanVien.DonDeXuat.GetDonGuiDi.DataAPI_DonGuiDi InforGuiDi = new OOP.NhanVien.DonDeXuat.GetDonGuiDi.DataAPI_DonGuiDi();

        private async void getData()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/DeXuat/user_send_deXuat_All");
                request.Headers.Add("id_user",Convert.ToString(InforGuiDi.id_user));
                request.Headers.Add("com_id", Convert.ToString(InforGuiDi.com_id));
                request.Headers.Add("type", "2");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("10000238"), "id_user");
                content.Add(new StringContent("3312"), "com_id");
                content.Add(new StringContent("1"), "type");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    // Xử lý phản hồi ở đây
                    ChamCong365.OOP.NhanVien.ToiGuiDi.DeXuatToiGuiDi.API_ToiGuiDi api = JsonConvert.DeserializeObject<ChamCong365.OOP.NhanVien.ToiGuiDi.DeXuatToiGuiDi.API_ToiGuiDi>(responseContent);
                    if (api.data.data != null)
                    {
                        dgv.ItemsSource = api.data.data;
                        //listDataToiGuiDi = api.data.data;
                    }
                }
                else
                {
                    // Xử lý khi có lỗi trong phản hồi (ví dụ: response.StatusCode)
                    // Ví dụ: throw new Exception("Có lỗi trong yêu cầu HTTP.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại! " + ex.Message);
            }

        }
    }
}
