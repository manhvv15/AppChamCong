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
    public partial class ucDetailBoNhiem : UserControl
    {
        List<Employee> AllEmployeeList = new List<Employee>();
        List<Position > ListPosition = new List<Position>();  
        
        string filePatch = "";
        public ucDetailBoNhiem(ChiTietDeXuat.DetailDeXuat detailDeXuat)
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

                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                int id = (int)detailDeXuat.thong_tin_chung.bo_nhiem.thanhviendc_bn;
                txbThanhVienBoNhiem.Text = AllEmployeeList.Where(x => x.ep_id == id).FirstOrDefault().ep_name;
                txbToChucHienTai.Text = "";
                int positionId = (int)detailDeXuat.thong_tin_chung.bo_nhiem.chucvu_hientai;
                txbChucVuHienTai.Text = ListPosition.Where(x => x.positionId == positionId).FirstOrDefault().positionName;
                int newPositionId = (int)detailDeXuat.thong_tin_chung.bo_nhiem.chucvu_dx_bn;
                txbChuVuDeXuatBoNhiem.Text = ListPosition.Where(x => x.positionId == newPositionId).FirstOrDefault().positionName;

                txbLyDo.Text = detailDeXuat.thong_tin_chung.bo_nhiem.ly_do;
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
    }
}
