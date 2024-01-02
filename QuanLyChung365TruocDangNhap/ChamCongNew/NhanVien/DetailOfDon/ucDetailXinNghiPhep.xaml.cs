using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;

using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;

//using DocumentFormat.OpenXml.Spreadsheet;

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.API_DsCRM;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.DetailOfDon
{
    /// <summary>
    /// Interaction logic for ucDetailSuDungPhongHop.xaml
    /// </summary>
    public partial class ucDetailXinNghiPhep : UserControl
    {
        private List<List_NhanVien> _lstUserData;
        public List<List_NhanVien> lstUserData
        {
            get { return _lstUserData; }
            set { _lstUserData = value; }
        }
        string filePatch = "";
        public ucDetailXinNghiPhep(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {
                var listShift = await GetListShift();
                await LoadDanhSachTatCaNhanVien();
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file; }
                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                string idUserCRM = detailDeXuat.thong_tin_chung.nghi_phep.ng_ban_giao_CRM.ToString();
                txbBanGiaoCRM.Text = lstUserData.Where(x => x.ep_id.ToString() == idUserCRM).FirstOrDefault()?.userName;
                var listLichNghi = detailDeXuat.thong_tin_chung.nghi_phep.nd;
                var STT = 1;
                var query = from lich in listLichNghi
                            select new
                            {
                                STT = STT++,
                                lich.bd_nghi,
                                lich.kt_nghi,
                                ca_nghi = (listShift.Where(x => x.shift_id == lich.ca_nghi).FirstOrDefault() == null) ? "Nghỉ cả ngày ( Tất cả các ca )" : listShift.Where(x => x.shift_id == lich.ca_nghi).FirstOrDefault().shift_name,
                            };
                lsvLichNghi.ItemsSource = query.ToList();
                txbLyDo.Text = detailDeXuat.thong_tin_chung.nghi_phep.ly_do;
                //if(detailDeXuat!= null && detailDeXuat.thong_tin_chung.nghi_phep.ng_ban_giao_CRM != null)
                //{
                //    var client = new HttpClient();
                //    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/listAll");
                //    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                //    var response = await client.SendAsync(request);
                //    response.EnsureSuccessStatusCode();
                //    var responseContent = await response.Content.ReadAsStringAsync();
                //    if (response.IsSuccessStatusCode)
                //    {
                //        // Xử lý phản hồi ở đây
                //        API_DsCRM.DS_CRM api = JsonConvert.DeserializeObject<API_DsCRM.DS_CRM>(responseContent);
                //        if (api.data != null && api.data.items != null)
                //        {
                //            txtNgNhanBanGiao.Text = api.data.items.Find(x => x.ep_id == int.Parse(detailDeXuat.thong_tin_chung.nghi_phep.ng_ban_giao_CRM)).ep_name;
                //        }
                //    }
                //}
                //else
                //{
                //    NhanBanGiao.Visibility = Visibility.Collapsed;
                //}


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
        public async Task LoadDanhSachTatCaNhanVien()
        {
            try
            {

                var searchObject = new
                {
                    ep_status = "Active",
                    pageSize = 10000


                };
                string searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_DanhSachNhanVien);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new StringContent(searchJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resSaff = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Root_NhanVien resultSaff = JsonConvert.DeserializeObject<Root_NhanVien>(resSaff);

                    lstUserData = resultSaff.data.data;

                }
            }
            catch (Exception)
            {
            }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try { System.Diagnostics.Process.Start(filePatch); } catch { }
        }
    }
}
