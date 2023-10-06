using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.OOP.NhanVien.DeXuatCuaToi;
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

namespace ChamCong365.NhanVien.DetailOfDon
{
    /// <summary>
    /// Interaction logic for ucDetailSuDungPhongHop.xaml
    /// </summary>
    public partial class ucDetailThamGiaDuAn : UserControl
    {
        List<Employee> AllEmployeeList = new List<Employee>();
        List<Position > ListPosition = new List<Position>();  
        List<OrganizeDetail.OrganizeData> ListOrganizes =  new List<OrganizeDetail.OrganizeData>();
        
        string filePatch = "";
        public ucDetailThamGiaDuAn(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {
                await GetAllEmployee();
                await GetListPosition();
                await GetListOrganize();

                txb1.Text = detailDeXuat.nguoi_tao;
                int cv_id = int.Parse(detailDeXuat.thong_tin_chung.tham_gia_du_an.cv_nguoi_da);
                txb2.Text = ListPosition.Where(x => x.positionId == cv_id).FirstOrDefault().positionName;
                int pb_id = int.Parse(detailDeXuat.thong_tin_chung.tham_gia_du_an.pb_nguoi_da);
                txb3.Text = ListOrganizes.Where(x => x.id == pb_id).FirstOrDefault()?.organizeDetailName;
                txb4.Text = detailDeXuat.thong_tin_chung.tham_gia_du_an.dx_da;
                txb5.Text = detailDeXuat.thong_tin_chung.tham_gia_du_an.ly_do;
                filePatch = detailDeXuat.file_kem[0].file;


            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(filePatch);
        }
        public async Task<bool> GetAllEmployee()
        {
            try {
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
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_position_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    PositionRoot result = JsonConvert.DeserializeObject<PositionRoot>(responseContent);
                    if (result.data.data.Count > 0)
                    {
                        ListPosition = result.data.data;
                        return true;
                    }
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

            try {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/organizeDetail/listAll");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent("1664"), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    var responseContent =  await response.Content.ReadAsStringAsync();
                    OrganizeDetail.Root result = JsonConvert.DeserializeObject<OrganizeDetail.Root>(responseContent);
                    ListOrganizes = result.data.data;
                }
            }
            catch { }
            return false;
        }
    }
}
