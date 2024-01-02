using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
//using DocumentFormat.OpenXml.Spreadsheet;
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
    public partial class ucDetailBoNhiem : UserControl
    {
        MainWindow MainWindow;
        MainChamCong MainChamCong;
        string com_id ="";
        List<Employee> AllEmployeeList = new List<Employee>();
        List<Position> ListPosition = new List<Position>();

        string filePatch = "";
        public ucDetailBoNhiem(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    MainWindow = (MainWindow)window;    
                }
                if (window.GetType() == typeof(MainChamCong))
                {
                    MainChamCong = (MainChamCong)window;
                }
            }
            if (MainWindow != null) com_id = MainWindow.IdAcount.ToString();
            if (MainChamCong != null) com_id = MainChamCong.ComdID.ToString();
            ShowData(detailDeXuat);

        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {
                await GetAllEmployee();
                await GetListOrganize();
                await GetListPosition();

                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                int id = (int)detailDeXuat.thong_tin_chung.bo_nhiem.thanhviendc_bn;
                txbThanhVienBoNhiem.Text = AllEmployeeList.Where(x => x.ep_id == id).FirstOrDefault()?.ep_name;
                int orgId = (int)detailDeXuat.thong_tin_chung.bo_nhiem.organizeDetailId;
                txbToChucHienTai.Text = lstOrganizeData.Where(x => x.id == orgId).FirstOrDefault()?.organizeDetailName; 
                int positionId = (int)detailDeXuat.thong_tin_chung.bo_nhiem.chucvu_hientai;
                txbChucVuHienTai.Text = lstPositionData.Where(x => x.id == positionId).FirstOrDefault()?.positionName;
                int newPositionId = (int)detailDeXuat.thong_tin_chung.bo_nhiem.chucvu_dx_bn;
                txbChuVuDeXuatBoNhiem.Text = lstPositionData.Where(x => x.id == newPositionId).FirstOrDefault()?.positionName;
                int newOrgId = (int)detailDeXuat.thong_tin_chung.bo_nhiem.new_organizeDetailId;
                txbCoCauToChucMoi.Text = lstOrganizeData.Where(x => x.id == newOrgId).FirstOrDefault()?.organizeDetailName;

                txbLyDo.Text = detailDeXuat.thong_tin_chung.bo_nhiem.ly_do;
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file; }


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

        private List<ListOrganizeEntities.OrganizeData> _lstOrganizeData;
        public List<ListOrganizeEntities.OrganizeData> lstOrganizeData
        {
            get { return _lstOrganizeData; }
            set { _lstOrganizeData = value; }
        }
        public async Task GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.listAll_organize);

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);

                    if (result.data.data != null)
                    {
                        lstOrganizeData = result.data.data;
                    }
                }
            }
            catch
            {

            }
        }

        private List<GioiHanIpVaPhanMem.Entities.ListPositionEntities.PositionData> _lstPositionData;
        public List<GioiHanIpVaPhanMem.Entities.ListPositionEntities.PositionData> lstPositionData
        {
            get { return _lstPositionData; }
            set { _lstPositionData = value; }
        }
        private List<List_NhanVien> _lstUserData;
        public List<List_NhanVien> lstUserData
        {
            get { return _lstUserData; }
            set { _lstUserData = value; }
        }
        private async Task GetListPosition()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.list_position);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    GioiHanIpVaPhanMem.Entities.ListPositionEntities.Root result = JsonConvert.DeserializeObject<GioiHanIpVaPhanMem.Entities.ListPositionEntities.Root>(responseContent);
                    if (result.data.data != null)
                    {
                        lstPositionData = result.data.data;
                    }
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}
