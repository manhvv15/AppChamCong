using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoToChuc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    public partial class ucDetailLichLamViec : UserControl
    {

        public class Datum
        {
            public string date { get; set; }
            public string shift_id { get; set; }
        }

        public class MyArray
        {
            public int? type { get; set; }
            public ICollection<Datum> data { get; set; }
        }

        public class Root
        {
            public ICollection<MyArray> MyArray { get; set; }
        }
        string thang = "";
        string ngayBatDau = "";
        List<XemChiTietLichLichLamViecEntites.Data> ListDay = new List<XemChiTietLichLichLamViecEntites.Data>();
        List<Employee> AllEmployeeList = new List<Employee>();
        List<ListPositionEntities.Position> ListPosition = new List<ListPositionEntities.Position>();
        List<OrganizeDetail.OrganizeData> ListOrganizes = new List<OrganizeDetail.OrganizeData>();

        string filePatch = "";
        public ucDetailLichLamViec(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {


                txb1.Text = detailDeXuat.nguoi_tao;
                string lich_lam_viec = "";
                if (detailDeXuat.thong_tin_chung.lich_lam_viec.lich_lam_viec == 1)
                {
                    txb2.Text = "Thứ 2 - Thứ 6";

                }
                if (detailDeXuat.thong_tin_chung.lich_lam_viec.lich_lam_viec == 2)
                {
                    txb2.Text = "Thứ 2 - Thứ 7";

                }
                if (detailDeXuat.thong_tin_chung.lich_lam_viec.lich_lam_viec == 3) ;
                {
                    txb2.Text = "Thứ 2 - Chủ nhật";

                }
                thang = DateTimeOffset.FromUnixTimeSeconds((long)detailDeXuat.thong_tin_chung.lich_lam_viec.thang_ap_dung).ToLocalTime().ToString("MM/yyyy");
                txb3.Text = thang;
                ngayBatDau = DateTimeOffset.FromUnixTimeSeconds((long)detailDeXuat.thong_tin_chung.lich_lam_viec.ngay_bat_dau).ToLocalTime().DateTime.ToString("dd/MM/yyyy");
                txb4.Text = ngayBatDau;
                txb5.Text = detailDeXuat.thong_tin_chung.lich_lam_viec.ly_do;

                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file; }
                List<XemChiTietLichLichLamViecEntites.Root> result = JsonConvert.DeserializeObject<List<XemChiTietLichLichLamViecEntites.Root>>(detailDeXuat.thong_tin_chung.lich_lam_viec.ngay_lam_viec);

                foreach (var item in result)
                {
                    foreach (var dataItem in item.data)
                    {
                        DateTime outDateTime;
                        DateTime.TryParseExact(dataItem.date.ToString(), "yyyy-MM-dd", null,
                                DateTimeStyles.None, out outDateTime);
                        dataItem.day = outDateTime.Day;
                        dataItem.listShift_id = dataItem.shift_id.Split(',').ToList();
                    }
                }
                ListDay = result[0].data;

            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try { System.Diagnostics.Process.Start(filePatch); } catch { }
        }
        public async Task<bool> GetAllEmployee()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_all);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    EmployeeRoot result = JsonConvert.DeserializeObject<EmployeeRoot>(responseContent);
                    AllEmployeeList = result.data.items;
                    return true;
                }
            }
            catch { }
            return false;
        }
        public async Task<bool> GetListPosition()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/positions");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ListPositionEntities.Root result = JsonConvert.DeserializeObject<ListPositionEntities.Root>(responseContent);

                    ListPosition = result.positions;
                    return true;

                }


            }
            catch
            {
                MessageBox.Show("Có lỗi khi lấy danh sách chức vụ");
            }
            return false;
        }
        public async Task<bool> GetListOrganize()
        {

            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/organizeDetail/listAll");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("1664"), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    OrganizeDetail.Root result = JsonConvert.DeserializeObject<OrganizeDetail.Root>(responseContent);
                    ListOrganizes = result.data.data;
                }
            }
            catch { }
            return false;
        }

        private void BtnXemChiTietLichLamViec(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainChamCong))
                {
                    MainChamCong Main = (MainChamCong)window;
                    ucXemChiTietLichLamViec uc = new ucXemChiTietLichLamViec(thang, ngayBatDau, ListDay);
                    Main.grShowPopup.Children.Add(uc);
                }
                if (window.GetType() == typeof(MainWindow))
                {
                    MainWindow Main = (MainWindow)window;
                    ucXemChiTietLichLamViec uc = new ucXemChiTietLichLamViec(thang, ngayBatDau, ListDay);
                    Main.grShowPopup.Children.Add(uc);
                }

            }
        }
    }
}
