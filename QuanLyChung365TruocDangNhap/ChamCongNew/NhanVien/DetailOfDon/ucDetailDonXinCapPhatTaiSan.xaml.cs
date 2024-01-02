using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien;
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
    /// 
    class TaiSan
    {
        public string[][] ds_ts { get; set; }
    }
    class ThongTinTaiSanCapPhat
    {
        public int STT { get; set; }
        public string name { get; set; }
        public string soLuong { get; set; }
    }
    public partial class ucDetailDonXinCapPhatTaiSan : UserControl
    {

        string filePatch = "";
        List<ListTaiSanEntities.TaiSan> ListTaiSan = new List<ListTaiSanEntities.TaiSan>();
        public ucDetailDonXinCapPhatTaiSan(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {

            try
            {
                await LoadListTaiSan();
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file; }
                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                //txbDsTaiSan.Text = detailDeXuat.thong_tin_chung.cap_phat_tai_san.danh_sach_tai_san;
                //txbSoLuongTaiSan.Text += detailDeXuat.thong_tin_chung.cap_phat_tai_san.so_luong_tai_san;
                txbLyDo.Text = detailDeXuat.thong_tin_chung.cap_phat_tai_san.ly_do;

                string json = detailDeXuat.thong_tin_chung.cap_phat_tai_san.cap_phat_taisan;
                var deserializedObject = JsonConvert.DeserializeObject<TaiSan>(json);
                List<ThongTinTaiSanCapPhat> list = new List<ThongTinTaiSanCapPhat>();
                // Accessing the deserialized data
                int STT = 1;
                foreach (var data in deserializedObject.ds_ts)
                {
                    list.Add(new ThongTinTaiSanCapPhat() { STT = STT++, name = ListTaiSan.Where(x => x.ts_id.ToString() == data[0]).FirstOrDefault().ts_ten, soLuong = data[1] });

                }

                lsvTaiSan.ItemsSource = list;
            }
            catch { }
        }
        public async Task LoadListTaiSan()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/listTaiSan");
                request.Headers.Add("authorization", "Bearer "+Properties.Settings.Default.Token);
          
                var content = new StringContent("{}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ListTaiSanEntities.Root api = JsonConvert.DeserializeObject<ListTaiSanEntities.Root>(responseContent);
                    if (api != null)
                    {
                        ListTaiSan = api.data;
                    }
                }
            }
            catch { 
            
            }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try { System.Diagnostics.Process.Start(filePatch); } catch { }
        }

    }
}
