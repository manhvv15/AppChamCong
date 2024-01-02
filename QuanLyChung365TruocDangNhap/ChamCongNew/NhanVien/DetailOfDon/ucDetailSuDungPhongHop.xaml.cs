using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.DetailOfDon
{
    /// <summary>
    /// Interaction logic for ucDetailSuDungPhongHop.xaml
    /// </summary>
    public partial class ucDetailSuDungPhongHop : UserControl
    {
        string filePatch = "";
        public ucDetailSuDungPhongHop(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file; }
                var listRegisterRoom = new List<MeetingRoom.MeetingRoomWitStatus>();
                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                long startTime = (long)detailDeXuat.thong_tin_chung.su_dung_phong_hop.bd_hop;
                long endTime = (long)detailDeXuat.thong_tin_chung.su_dung_phong_hop.end_hop;
                txbStartTime.Text = DateTimeOffset.FromUnixTimeSeconds(startTime).ToLocalTime().ToString("dd/MM/yyyy hh:mm tt");
                txbEndTime.Text = DateTimeOffset.FromUnixTimeSeconds(endTime).ToLocalTime().ToString("dd/MM/yyyy hh:mm tt");
                txbPhongHop.Text = detailDeXuat.thong_tin_chung.su_dung_phong_hop.phong_hop;
                //string[] meetingRoomIds = detailDeXuat.thong_tin_chung.su_dung_phong_hop.phong_hop?.Split(',');
                //MeetingRoom.Root RoomData = await GetListRoom(startTime, endTime);
                //var listRoom = RoomData.meetingRoomWitStatus;
                //if (meetingRoomIds != null)
                //{
                //    foreach (string roomId in meetingRoomIds)
                //    {
                //        foreach (var item in listRoom)
                //        {
                //            if (item.id == int.Parse(roomId))
                //            {
                //                listRegisterRoom.Add(item);
                //            }
                //        }

                //    }
                //}
                //lsvRoom.ItemsSource = listRoom;
                txbLyDo.Text = detailDeXuat.thong_tin_chung.su_dung_phong_hop.ly_do;
            }
            catch { }
        }
        public async Task<MeetingRoom.Root> GetListRoom(long strartTime, long endTime)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/meetingRooms");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(strartTime.ToString()), "time_start");
                content.Add(new StringContent(endTime.ToString()), "time_end");
                request.Content = content;
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MeetingRoom.Root data = JsonConvert.DeserializeObject<MeetingRoom.Root>(responseContent);
                    return data;
                }

            }

            catch { }
            return null;
        }


        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try { System.Diagnostics.Process.Start(filePatch); } catch { }
        }
    }
}
