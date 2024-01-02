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
    public partial class ucDetailXinTangCa : UserControl
    {
        string filePatch = "";
        public ucDetailXinTangCa(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {
                var listShift = await GetListShift();

                txb1.Text = detailDeXuat.nguoi_tao;
                txb2.Text = DateTimeOffset.FromUnixTimeMilliseconds((long)detailDeXuat.thong_tin_chung.tang_ca.time_tc).ToLocalTime().ToString("dd/MM/yyyy");
                txb3.Text = listShift.Where(x => x.shift_id == (int)detailDeXuat.thong_tin_chung.tang_ca.shift_id).FirstOrDefault()?.shift_name;
                txb4.Text = detailDeXuat.thong_tin_chung.tang_ca.ly_do;
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file;}

            }
            catch { }
        }
        public async Task<List<Shift>> GetListShift()
        {
            try
            {

                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                string api = API.list_shift_api;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();
                ShiftRoot result = JsonConvert.DeserializeObject<ShiftRoot>(responseContent);

                List<Shift> listShift = result.data.items;
                return listShift;
            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
            return null;
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try{System.Diagnostics.Process.Start(filePatch);}catch{}
        }
    }
}
