using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
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
    public partial class ucDetailSuaChuaCoSoVatChat : UserControl
    {
        string filePatch = "";
        public ucDetailSuaChuaCoSoVatChat(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {
                await LoadListTaiSan();

                txb1.Text = detailDeXuat.nguoi_tao;
                string taiSanId = detailDeXuat.thong_tin_chung.sua_chua_co_so_vat_chat.tai_san.ToString();
                txb2.Text = ListTaiSan.FirstOrDefault(x=>x.ts_id.ToString() == taiSanId).ts_ten;
                txb3.Text = detailDeXuat.thong_tin_chung.sua_chua_co_so_vat_chat?.so_luong.ToString();
                txb4.Text = DateTimeOffset.FromUnixTimeSeconds((long)detailDeXuat.thong_tin_chung.sua_chua_co_so_vat_chat.ngay_sc).ToLocalTime().ToString("dd/MM/yyyy");
                txb5.Text = detailDeXuat.thong_tin_chung.sua_chua_co_so_vat_chat.so_tien?.ToString("#,###.##") + " đ";
                txb6.Text = detailDeXuat.thong_tin_chung.sua_chua_co_so_vat_chat.ly_do;
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file;}

            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try{System.Diagnostics.Process.Start(filePatch);}catch{}
        }
        List<ListTaiSanEntities.TaiSan> ListTaiSan = new List<ListTaiSanEntities.TaiSan>();
        public async Task LoadListTaiSan()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/listTaiSan");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);

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
            catch
            {

            }
        }
    }
}
