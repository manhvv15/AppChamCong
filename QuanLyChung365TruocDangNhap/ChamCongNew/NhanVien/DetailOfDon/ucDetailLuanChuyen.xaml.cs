using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using Newtonsoft.Json;
//using NPOI.SS.Formula.Functions;
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
    public partial class ucDetailLuanChuyen : UserControl
    {
        MainWindow MainWindow;
        MainChamCong MainChamCong;
        string com_id = "";
        List<Employee> AllEmployeeList = new List<Employee>();
        List<Position> ListPosition = new List<Position>();

        string filePatch = "";
        public ucDetailLuanChuyen(ChiTietDeXuat.DetailDeXuat detailDeXuat)
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
                await GetListOrganize();
                await GetListPosition();


                txbHoVaTen.Text = detailDeXuat.nguoi_tao;

                string positionId = detailDeXuat.thong_tin_chung?.luan_chuyen_cong_tac?.cv_nguoi_lc;
                txbChucVu.Text  = lstPositionData.Where(x => x.id.ToString() == positionId).FirstOrDefault()?.positionName;
                string organizeId = detailDeXuat.thong_tin_chung?.luan_chuyen_cong_tac?.pb_nguoi_lc;
                txbPhongBan.Text = lstOrganizeData.Where(x => x.id.ToString() == organizeId).FirstOrDefault()?.organizeDetailName;
                txbNoiDangCong.Text = detailDeXuat.thong_tin_chung?.luan_chuyen_cong_tac?.noi_cong_tac;
                string newOrganizeId = detailDeXuat.thong_tin_chung?.luan_chuyen_cong_tac?.noi_chuyen_den;
                txbNoiCanChuyenDen.Text = lstOrganizeData.Where(x => x.id.ToString() == newOrganizeId).FirstOrDefault()?.organizeDetailName;
                txbLyDo.Text = detailDeXuat.thong_tin_chung?.luan_chuyen_cong_tac?.ly_do;
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file; }
            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try
            {
                try { System.Diagnostics.Process.Start(filePatch); } catch { }

            }
            catch
            {

            }
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
