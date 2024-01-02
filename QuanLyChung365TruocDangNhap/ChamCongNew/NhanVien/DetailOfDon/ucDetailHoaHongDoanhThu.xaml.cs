using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
    public partial class ucDetailHoaHongDoanhThu : UserControl
    {
        string filePatch = "";
        public ucDetailHoaHongDoanhThu(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {

                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file; }
                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                txb2.Text = detailDeXuat.thong_tin_chung.hoa_hong.chu_ky;
                txb3.Text = detailDeXuat.thong_tin_chung.hoa_hong.item_mdt_date;
                txb4.Text = detailDeXuat.thong_tin_chung.hoa_hong.dt_money.ToString() + "đ";
                txb5.Text = (await GetMucDoanhThu()).Where(x => x.tl_id == int.Parse(detailDeXuat.thong_tin_chung.hoa_hong.name_dt)).FirstOrDefault()?.tl_name;

                txbLyDo.Text = detailDeXuat.thong_tin_chung.hoa_hong.ly_do;


            }
            catch { }
        }

        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try { System.Diagnostics.Process.Start(filePatch); } catch { }
        }
        private async Task<List<MucDoanhThuEntities.Danhthu>> GetMucDoanhThu()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/showMucDoanhThu");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent(string.Empty);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    MucDoanhThuEntities.Root result = JsonConvert.DeserializeObject<MucDoanhThuEntities.Root>(responseContent);
                    List<MucDoanhThuEntities.Danhthu> list = result.data.danhthuList;
                    return list;
                }


            }
            catch { }
            return null;
        }
    }
}
