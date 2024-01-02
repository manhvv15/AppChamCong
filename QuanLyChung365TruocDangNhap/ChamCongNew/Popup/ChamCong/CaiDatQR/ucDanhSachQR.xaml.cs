using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ViTri;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupQuanLyNhanVien;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;
using static QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.ThietLapCongTy.Comons.ucComboboxMuiltiSelect;
//using DocumentFormat.OpenXml.Drawing;
//using DocumentFormat.OpenXml.Office2010.Excel;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi.API_List_Detail;
//using DocumentFormat.OpenXml.Spreadsheet;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi.API_FilterComp;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatQR
{
    /// <summary>
    /// Interaction logic for ucDanhSachQR.xaml
    /// </summary>
    public partial class ucDanhSachQR : UserControl
    {
        public static List<ItemCbx> listDevices = new List<ItemCbx>()
        {
            new ItemCbx(){Key = "1", Value = "CameraAI365"},
            new ItemCbx(){Key = "2", Value = "AppChat365"},
            new ItemCbx(){Key = "3", Value = "QRChat365"}
        };

        public class ShiftBody
        {
            public int id { get; set; }
            public int type_shift { get; set; }
        }

        MainWindow Main;
        List<ListUsersDuyet> listUDuyets = new List<ListUsersDuyet>();
        List<ListUsersDuyet> listAddUDuyets = new List<ListUsersDuyet>();
        List<ListOrganizeEntities.OrganizeData> ListOrganize = new List<ListOrganizeEntities.OrganizeData>();
        List<ListPositionEntities.PositionData> ListPosition = new List<ListPositionEntities.PositionData>();
        List<ItemWifi> ListWifi = new List<ItemWifi>();
        List<API_Location.Location> ListLocation = new List<API_Location.Location>();
        List<ListQREntities.QRInfo> ListQR = new List<ListQREntities.QRInfo>();
        List<ListUser> ListStaff = new List<ListUser>();
        List<Shift> ListShiftWork = new List<Shift>();
        List<ItemCbx> ListDevices = new List<ItemCbx>();
        List<ItemCbx> ListOrganizeMuiltiCbx = new List<ItemCbx>();
        List<ItemCbx> ListPositionMuiltiCbx = new List<ItemCbx>();
        List<ItemCbx> ListStaffNameMuiltiCbx = new List<ItemCbx>();
        List<ItemCbx> ListShiftWorkMuiltiCbx = new List<ItemCbx>();
        List<ItemCbx> ListWifiMuiltiCbx = new List<ItemCbx>();
        List<ItemCbx> ListLocationMuiltiCbx = new List<ItemCbx>();
        BrushConverter bcWifi = new BrushConverter(); 


        public ucDanhSachQR(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            GetListOrganize();
            GetListPosition();
            GetListLocation();
            GetListShift();
            //LoadListStaff();
            getFilterComp();
            GetListWifi();
            GetListQR();

            GetListQRSetting();

        }


        #region searchInCombobox

        private void cbxQRCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxQRCode.SelectedIndex = -1;
                string textSearch = cbxQRCode.Text;
                cbxQRCode.Items.Refresh();
                cbxQRCode.ItemsSource = ListQR.Where(t => t.QRCodeName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cbxQRCode_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxQRCode.SelectedIndex = -1;
            string textSearch = cbxQRCode.Text + e.Text;
            cbxQRCode.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxQRCode.Text = "";
                cbxQRCode.Items.Refresh();
                cbxQRCode.ItemsSource = ListQR;
                cbxQRCode.SelectedIndex = -1;
            }
            else
            {
                cbxQRCode.ItemsSource = "";
                cbxQRCode.Items.Refresh();
                cbxQRCode.ItemsSource = ListQR.Where(t => t.QRCodeName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cbxOrganize_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxOrganize.SelectedIndex = -1;
                string textSearch = cbxOrganize.Text;
                cbxOrganize.Items.Refresh();
                cbxOrganize.ItemsSource = ListOrganize.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }
        private void cbxOrganize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxOrganize.SelectedIndex = -1;
            string textSearch = cbxOrganize.Text + e.Text;
            cbxOrganize.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxOrganize.Text = "";
                cbxOrganize.Items.Refresh();
                cbxOrganize.ItemsSource = ListOrganize;
                cbxOrganize.SelectedIndex = -1;
            }
            else
            {
                cbxOrganize.ItemsSource = "";
                cbxOrganize.Items.Refresh();
                cbxOrganize.ItemsSource = ListOrganize.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }
        //private void cbxOrganize_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Back)
        //    {
        //        cbxOrganize.SelectedIndex = -1;
        //        string textSearch = cbxOrganize.Text;
        //        cbxOrganize.Items.Refresh();
        //        cbxOrganize.ItemsSource = ListOrganize.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}

        //private void cbxOrganize_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    cbxOrganize.SelectedIndex = -1;
        //    string textSearch = cbxOrganize.Text + e.Text;
        //    cbxOrganize.IsDropDownOpen = true;
        //    if (textSearch == "")
        //    {
        //        cbxOrganize.Text = "";
        //        cbxOrganize.Items.Refresh();
        //        cbxOrganize.ItemsSource = ListOrganize;
        //        cbxOrganize.SelectedIndex = -1;
        //    }
        //    else
        //    {
        //        cbxOrganize.ItemsSource = "";
        //        cbxOrganize.Items.Refresh();
        //        cbxOrganize.ItemsSource = ListOrganize.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}
        //private void cbxPosition_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Back)
        //    {
        //        cbxPosition.SelectedIndex = -1;
        //        string textSearch = cbxPosition.Text;
        //        cbxPosition.Items.Refresh();
        //        cbxPosition.ItemsSource = ListPosition.Where(t => t.positionName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}

        //private void cbxPosition_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    cbxPosition.SelectedIndex = -1;
        //    string textSearch = cbxPosition.Text + e.Text;
        //    cbxPosition.IsDropDownOpen = true;
        //    if (textSearch == "")
        //    {
        //        cbxPosition.Text = "";
        //        cbxPosition.Items.Refresh();
        //        cbxPosition.ItemsSource = ListPosition;
        //        cbxPosition.SelectedIndex = -1;
        //    }
        //    else
        //    {
        //        cbxPosition.ItemsSource = "";
        //        cbxPosition.Items.Refresh();
        //        cbxPosition.ItemsSource = ListPosition.Where(t => t.positionName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}
        //private void cbxStaffName_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Back)
        //    {
        //        cbxStaffName.SelectedIndex = -1;
        //        string textSearch = cbxStaffName.Text;
        //        cbxStaffName.Items.Refresh();
        //        cbxStaffName.ItemsSource = ListStaff.Where(t => t.userName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}

        //private void cbxStaffName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    cbxStaffName.SelectedIndex = -1;
        //    string textSearch = cbxStaffName.Text + e.Text;
        //    cbxStaffName.IsDropDownOpen = true;
        //    if (textSearch == "")
        //    {
        //        cbxStaffName.Text = "";
        //        cbxStaffName.Items.Refresh();
        //        cbxStaffName.ItemsSource = ListStaff;
        //        cbxStaffName.SelectedIndex = -1;
        //    }
        //    else
        //    {
        //        cbxStaffName.ItemsSource = "";
        //        cbxStaffName.Items.Refresh();
        //        cbxStaffName.ItemsSource = ListStaff.Where(t => t.userName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}
        //private void cbxShiftWork_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Back)
        //    {
        //        cbxShiftWork.SelectedIndex = -1;
        //        string textSearch = cbxShiftWork.Text;
        //        cbxShiftWork.Items.Refresh();
        //        cbxShiftWork.ItemsSource = ListShiftWork.Where(t => t.shift_name.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}

        //private void cbxShiftWork_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    cbxShiftWork.SelectedIndex = -1;
        //    string textSearch = cbxShiftWork.Text + e.Text;
        //    cbxShiftWork.IsDropDownOpen = true;
        //    if (textSearch == "")
        //    {
        //        cbxShiftWork.Text = "";
        //        cbxShiftWork.Items.Refresh();
        //        cbxShiftWork.ItemsSource = ListShiftWork;
        //        cbxShiftWork.SelectedIndex = -1;
        //    }
        //    else
        //    {
        //        cbxShiftWork.ItemsSource = "";
        //        cbxShiftWork.Items.Refresh();
        //        cbxShiftWork.ItemsSource = ListShiftWork.Where(t => t.shift_name.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}
        //private void cbxWifi_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Back)
        //    {
        //        cbxWifi.SelectedIndex = -1;
        //        string textSearch = cbxWifi.Text;
        //        cbxWifi.Items.Refresh();
        //        cbxWifi.ItemsSource = ListWifi.Where(t => t.name_wifi.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}

        //private void cbxWifi_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    cbxWifi.SelectedIndex = -1;
        //    string textSearch = cbxWifi.Text + e.Text;
        //    cbxWifi.IsDropDownOpen = true;
        //    if (textSearch == "")
        //    {
        //        cbxWifi.Text = "";
        //        cbxWifi.Items.Refresh();
        //        cbxWifi.ItemsSource = ListWifi;
        //        cbxWifi.SelectedIndex = -1;
        //    }
        //    else
        //    {
        //        cbxWifi.ItemsSource = "";
        //        cbxWifi.Items.Refresh();
        //        cbxWifi.ItemsSource = ListWifi.Where(t => t.name_wifi.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}
        //private void cbxDevices_KeyUp(object sender, KeyEventArgs e)
        //{
        //    if (e.Key == Key.Back)
        //    {
        //        cbxDevices.SelectedIndex = -1;
        //        string textSearch = cbxDevices.Text;
        //        cbxDevices.Items.Refresh();
        //        cbxDevices.ItemsSource = ListDevices.Where(t => t.Value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}

        //private void cbxDevices_PreviewTextInput(object sender, TextCompositionEventArgs e)
        //{
        //    cbxDevices.SelectedIndex = -1;
        //    string textSearch = cbxDevices.Text + e.Text;
        //    cbxDevices.IsDropDownOpen = true;
        //    if (textSearch == "")
        //    {
        //        cbxDevices.Text = "";
        //        cbxDevices.Items.Refresh();
        //        cbxDevices.ItemsSource = ListDevices;
        //        cbxDevices.SelectedIndex = -1;
        //    }
        //    else
        //    {
        //        cbxDevices.ItemsSource = "";
        //        cbxDevices.Items.Refresh();
        //        cbxDevices.ItemsSource = ListDevices.Where(t => t.Value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
        //    }
        //}
        #endregion

        #region CallAPI

        private async void getFilterComp()
        {
            try
            {

                var list_org = MuiltiSelectOrganize.SelectedList.Select(x => int.Parse(x.Key));
                var list_pos = MuiltiSelectPosition.SelectedList.Select(x => int.Parse(x.Key));

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/timekeeping/filterComp");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var jsonBody = new
                {
                    posId = list_pos
                };
                string json = JsonConvert.SerializeObject(jsonBody);

                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_FilterComp.Filter_Comp api = JsonConvert.DeserializeObject<API_FilterComp.Filter_Comp>(responseContent);
                    ListStaff = api.data.listUsers;

                    var listIdOrgSeleted = MuiltiSelectOrganize.SelectedList.Select(x => int.Parse(x.Key)).ToList();
                    if (listIdOrgSeleted.Count > 0)
                    {
                        List<ListUser> listUsers = new List<ListUser>();
                        foreach (var item in listIdOrgSeleted)

                        {
                            listUsers.AddRange(api.data.listUsers.Where(user => user.dep.Select(dep => dep.listOrganizeDetailId.Select(x => x.organizeDetailId.Value)).Where(x => x.Contains(item)).FirstOrDefault() != null).ToList());                            listUsers.AddRange(api.data.listUsers.Where(user => user.dep.Select(dep => dep.listOrganizeDetailId.Select(x => x.organizeDetailId.Value)).Where(x => x.Contains(item)).FirstOrDefault() != null).ToList());
                        }
                        api.data.listUsers = listUsers.GroupBy(p => p.idQLC).Select(group => group.First()).ToList();
                    }

                    //api.data.listUsers = api.data.listUsers.Where(user => user.dep.Where(dep => MuiltiSelectOrganize.SelectList.Select(x=>x.Key).Contains(dep.id.ToString())).FirstOrDefault()!=null).ToList();
                    List<ItemCbx> list = new List<ItemCbx>(); list.Add(new ItemCbx() { Key = "", Value = "Tất cả nhân viên" });
                    foreach (var item in api.data.listUsers) { list.Add(new ItemCbx() { Key = item.idQLC.ToString(), Value = item.userName }); }
                    MuiltiSelectStaffName.ItemsSource = list;
                    MuiltiSelectStaffName.SelectList = new List<ItemCbx>();
                    ListStaffNameMuiltiCbx = list;
                }


            }
            catch (Exception ex)
            {
                // MessageBox.Show("loi load list " + ex.Message);
            }
        }
        public async void GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.listAll_organize);

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);

                    if (result.data.data != null)
                    {
                        ListOrganize = result.data.data;
                        List<ItemCbx> list = new List<ItemCbx>(); list.Add(new ItemCbx() { Key = "", Value = "Tất cả phòng ban" });
                        foreach (var item in result.data.data) { list.Add(new ItemCbx() { Key = item.id.ToString(), Value = item.organizeDetailName }); }
                        MuiltiSelectOrganize.ItemsSource = list;
                        ListOrganizeMuiltiCbx = list;
                    }
                }
            }
            catch
            {

            }
        }


        private async void GetListPosition()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.list_position);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListPositionEntities.Root result = JsonConvert.DeserializeObject<ListPositionEntities.Root>(responseContent);
                    if (result.data.data != null)
                    {
                        ListPosition = result.data.data;
                        List<ItemCbx> list = new List<ItemCbx>(); list.Add(new ItemCbx() { Key = "", Value = "Tất cả chức vụ" });
                        foreach (var item in result.data.data) { list.Add(new ItemCbx() { Key = item.id.ToString(), Value = item.positionName }); }
                        MuiltiSelectPosition.ItemsSource = list;
                        ListPositionMuiltiCbx = list;

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public async void GetListWifi()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.list_wifi_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                RootWifi loadListWifi = JsonConvert.DeserializeObject<RootWifi>(responseContent);

                if (loadListWifi.data.data != null)
                {
                    ListWifi = loadListWifi.data.data;
                    List<ItemCbx> list = new List<ItemCbx>(); list.Add(new ItemCbx() { Key = "", Value = "Tất cả wifi" });
                    list.Add(new ItemCbx() { Key = "0", Value = "Tất cả wifi đã lưu" });
                    foreach (var item in loadListWifi.data.data) { list.Add(new ItemCbx() { Key = item.id.ToString(), Value = item.name_wifi }); }
                    MuiltiSelectWifi.ItemsSource = list;
                    ListWifiMuiltiCbx = list;
                }
            }
            catch (Exception)
            {
            }
        }
        public async void GetListLocation()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/location/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_Location.Root result = JsonConvert.DeserializeObject<API_Location.Root>(responseContent);
                    ListLocation = result.data.list;
                    List<ItemCbx> list = new List<ItemCbx>(); list.Add(new ItemCbx() { Key = "", Value = "Tất cả vị trí" });
                    list.Add(new ItemCbx() { Key = "0", Value = "Tất cả vị trí đã lưu" });
                    foreach (var item in result.data.list) { list.Add(new ItemCbx() { Key = item.cor_id.ToString(), Value = item.cor_location_name }); }
                    MuiltiSelectLocation.ItemsSource = list;
                    ListLocationMuiltiCbx = list;
                }
            }
            catch
            {
            }


        }
        public async void GetListQR()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/qrCode/listAll");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ListQREntities.Root result = JsonConvert.DeserializeObject<ListQREntities.Root>(responseContent);
                    ListQR = result.data.data;
                    cbxQRCode.ItemsSource = ListQR;
                }
            }
            catch
            {
            }
        }
        public async void GetListShift()
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

                ListShiftWork = result.data.items;
                List<ItemCbx> list = new List<ItemCbx>(); list.Add(new ItemCbx() { Key = "", Value = "Tất cả ca làm việc" });
                foreach (var item in result.data.items) { list.Add(new ItemCbx() { Key = item.shift_id.ToString(), Value = item.shift_name }); }
                MuiltiSelectShiftWork.ItemsSource = list;
                ListShiftWorkMuiltiCbx = list;
            }
            catch { }
        }
        //public async void LoadListStaff()
        //{
        //    try
        //    {

        //        var searchObject = new
        //        {
        //            ep_status = "Active",
        //            pageSize = 10000


        //        };
        //        string searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_DanhSachNhanVien);
        //        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

        //        var content = new StringContent(searchJson, null, "application/json");
        //        request.Content = content;
        //        var response = await client.SendAsync(request);
        //        var resSaff = await response.Content.ReadAsStringAsync();

        //        if (response.IsSuccessStatusCode)
        //        {
        //            Root_NhanVien resultSaff = JsonConvert.DeserializeObject<Root_NhanVien>(resSaff);

        //            ListStaff = resultSaff.data.data;
        //            List<ItemCbx> list = new List<ItemCbx>(); list.Add(new ItemCbx() { Key = "", Value = "Tất cả nhân viên" });
        //            foreach (var item in resultSaff.data.data) { list.Add(new ItemCbx() { Key = item.ep_id.ToString(), Value = item.userName }); }
        //            MuiltiSelectStaffName.ItemsSource = list;
        //            ListStaffNameMuiltiCbx = list;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        public async void GetListQRSetting()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/qrCode/listSettingTrackingQR");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("", null, "text/plain");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_QR.Root result = JsonConvert.DeserializeObject<API_QR.Root>(responseContent);
                    int stt = 1;
                    foreach (var item in result.data.data)
                    {
                        item.stt = stt++;
                        item.start_time = item.start_time.Value.ToLocalTime();
                        item.end_time = item.end_time.Value.ToLocalTime();
                    }
                    dgv.ItemsSource = result.data.data;

                }

            }
            catch
            {
            }
        }
        public async void DeleteQR(int id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/qrCode/deleteSettingTrackingQR");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\r\n   \"id\":" + id + "\r\n}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root result = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {

                    Main.grShowPopup.Children.Add(new ucPopupSuccess(result.data.message));
                }
                else
                {
                    Main.grShowPopup.Children.Add(new ucPopupError(result.error.message));
                }

            }
            catch (Exception ex) { Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra")); }

        }
        public async void CreateQRSetting()
        {
            try
            {
                if (Validate())
                {

                    List<ShiftBody> listShiftBody = new List<ShiftBody>();
                    foreach (var item in MuiltiSelectShiftWork.SelectedList)
                    {
                        int id = int.Parse(item.Key);
                        int type_shift = ListShiftWork.FirstOrDefault(x => x.shift_id == id).shift_type;
                        listShiftBody.Add(new ShiftBody() { id = id, type_shift = type_shift });
                    }
                    int? type_loc = null;
                    List<int> list_loc = new List<int>();
                    bool isSelectAllSaveLocation = (MuiltiSelectLocation.SelectedList.FirstOrDefault(x => x.Key == "0") != null);
                    if (!isSelectAllSaveLocation)
                    {
                        list_loc = MuiltiSelectLocation.SelectedList.Select(x => int.Parse(x.Key)).ToList();
                        if (list_loc.Count == 0)
                        {
                            type_loc = 3;
                        }
                    }
                    else
                    {
                        type_loc = 2;
                        list_loc = new List<int>();
                    }


                    int? type_ip = null;
                    List<int> list_ip = new List<int>();
                    bool isSelectAllSaveWifi = (MuiltiSelectWifi.SelectedList.FirstOrDefault(x => x.Key == "0") != null);
                    if (!isSelectAllSaveWifi)
                    {
                        list_ip = MuiltiSelectWifi.SelectedList.Select(x => int.Parse(x.Key)).ToList();
                        if (list_ip.Count == 0)
                        {
                            type_ip = 3;
                        }
                    }
                    else
                    {
                        type_ip = 2;
                        list_ip = new List<int>();
                    }




                    var bodyObject = new
                    {

                        name = txtSettingName.Text,
                        QRCode_id = cbxQRCode.SelectedValue,
                        list_org = MuiltiSelectOrganize.SelectedList.Select(x => int.Parse(x.Key)),
                        list_pos = MuiltiSelectPosition.SelectedList.Select(x => int.Parse(x.Key)),
                        listUsers = MuiltiSelectStaffName.SelectedList.Select(x => int.Parse(x.Key)),
                        start_time = dpStartTime.SelectedDate,
                        end_time = dpEndTime.SelectedDate,
                        list_shifts = listShiftBody,
                        list_loc = list_loc,
                        list_ip = list_ip,
                        type_loc = type_loc,
                        type_ip = type_ip

                    };
                    string json = JsonConvert.SerializeObject(bodyObject, Formatting.Indented);

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/qrCode/SettingTrackingQR");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new StringContent(json, null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    MessageEntities.Root result = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                    if (response.IsSuccessStatusCode)
                    {

                        Main.grShowPopup.Children.Add(new ucPopupSuccess(result.data.message));
                        ucCaiDatBaoMatWifi uc = new ucCaiDatBaoMatWifi(Main, Main.IdAcount);
                        uc.txbChamCongQR.Foreground = (Brush)bcWifi.ConvertFrom("#4C5BD4");
                        uc.bodChamCongQR.BorderThickness = new Thickness(0, 0, 0, 5);
                        uc.bodChamCongQR.BorderBrush = (Brush)bcWifi.ConvertFrom("#4C5BD4");

                        uc.txbTextWifi.Foreground = (Brush)bcWifi.ConvertFrom("#474747");
                        uc.bodTextWifi.BorderThickness = new Thickness(0, 0, 0, 0);
                        uc.bodTextWifi.BorderBrush = (Brush)bcWifi.ConvertFrom("#FFFFFF");
                        ucDanhSachQR ucc1 = new ucDanhSachQR(Main);
                        uc.grLoadListWifiIp.Children.Clear();
                        object Content = ucc1.Content;
                        ucc1.Content = null;
                        uc.grLoadListWifiIp.Children.Add(Content as UIElement);

                        Main.dopBody.Children.Clear();
                        object Content1 = uc.Content;
                        uc.Content = null;
                        Main.dopBody.Children.Add(Content1 as UIElement);
                    }
                    else
                    {
                        Main.grShowPopup.Children.Add(new ucPopupError(result.error.message));
                    }
                }

            }
            catch (Exception ex) { Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra")); }

        }
        #endregion

        private void Edit_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedSetting = (sender as Grid).DataContext as API_QR.Item;
                Main.grShowPopup.Children.Add(new ucChinhSuaCaiDatQR(Main, selectedSetting, this, ListQR, ListOrganizeMuiltiCbx, ListPositionMuiltiCbx, ListStaffNameMuiltiCbx, ListShiftWorkMuiltiCbx, ListLocationMuiltiCbx, ListWifiMuiltiCbx));
            }
            catch
            {

            }

        }

        private void Delete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var grid = sender as System.Windows.Shapes.Path;
                int id = (int)grid.DataContext;
                Main.grShowPopup.Children.Add(new ucXoaCaiDatQR(this, id));

            }
            catch { }
        }

        private void ViewDetailOrg(object sender, MouseButtonEventArgs e)
        {

        }

        private void ViewDetailPosition(object sender, MouseButtonEventArgs e)
        {

        }

        private void ViewDetailEmployee(object sender, MouseButtonEventArgs e)
        {

        }

        private void ViewDetailShift(object sender, MouseButtonEventArgs e)
        {

        }

        private void ViewDetailWifi(object sender, MouseButtonEventArgs e)
        {

        }

        private void ViewDetailIp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ViewDetailDevice(object sender, MouseButtonEventArgs e)
        {

        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                var textblock = sender as TextBlock;
                var ListTypeObject = textblock.DataContext as List<object>;
                if (ListTypeObject != null)
                {
                    lsvShowDetai.ItemsSource = ListTypeObject;
                }
                var ListTypeString = textblock.DataContext as List<string>;
                if (ListTypeString != null)
                {
                    lsvShowDetai.ItemsSource = ListTypeString;
                }

                popUpViewDetail.PlacementTarget = textblock;
                popUpViewDetail.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                popUpViewDetail.VerticalOffset = -(textblock.ActualHeight + popUpViewDetail.Child.DesiredSize.Height) / 2;
                popUpViewDetail.IsOpen = true;
            }
            catch { }

        }

        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            popUpViewDetail.IsOpen = false;
        }



        private void xoaNg_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void textChonDx_TextChanged(object sender, TextChangedEventArgs e)
        {

        }



        private void xoaNg_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void Rectangle_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void lsvDx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void ApDung_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CreateQRSetting();
        }

        private void lsvShowDetai_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);

        }
        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            try
            {

                if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
                {
                    scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta);
                }
                else
                {
                    Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
                }
            }
            catch { }
        }

        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }

        private void OpenDetailLocation_MouseEnter(object sender, MouseEventArgs e)
        {
            try
            {
                var textblock = sender as TextBlock;
                var ListLocation = textblock.DataContext as List<API_QR.ListLoc>;
                if (ListLocation != null)
                {
                    lsvShowDetaiListLocation.ItemsSource = ListLocation;
                }


                popUpViewDetailListLocation.PlacementTarget = textblock;
                popUpViewDetailListLocation.Placement = System.Windows.Controls.Primitives.PlacementMode.Center;
                popUpViewDetailListLocation.VerticalOffset = -(textblock.ActualHeight + popUpViewDetail.Child.DesiredSize.Height) / 2;
                popUpViewDetailListLocation.IsOpen = true;
            }
            catch { }
        }

        private void OpenDetailLocation_MouseLeave(object sender, MouseEventArgs e)
        {
            popUpViewDetailListLocation.IsOpen = false;
        }

        private bool Validate()
        {
            if (txtSettingName.Text == "")
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Tên cài đặt không được để trống"));
                return false;
            }
            else if (cbxQRCode.SelectedIndex == -1)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn mã QR"));
                return false;
            }
            else if (MuiltiSelectStaffName.SelectList.Count == 0)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn nhân viên áp dụng cài đặt chấm công QR"));
                return false;
            }
            else if (dpStartTime.SelectedDate == null)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn ngày bắt đầu áp dụng cài đặt chấm công QR"));
                return false;
            }
            else if (dpEndTime.SelectedDate == null)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn ngày kết thúc áp dụng cài đặt chấm công QR"));
                return false;
            }
            else if (dpStartTime.SelectedDate > dpEndTime.SelectedDate)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Ngày bắt đầu áp dụng cài đặt chấm công QR phải nhỏ hơn ngày kết thúc"));
                return false;
            }
            else if (MuiltiSelectShiftWork.SelectList.Count == 0)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn ca làm việc áp dụng cài đặt chấm công QR"));
                return false;
            }
            else if (MuiltiSelectLocation.SelectList.Count == 0)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn vị trí áp dụng cài đặt chấm công QR"));
                return false;
            }
            else if (MuiltiSelectWifi.SelectList.Count == 0)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn wifi áp dụng cài đặt chấm công QR"));
                return false;
            }
            return true;
        }
        public void MuiltiSelectOrganize_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getFilterComp();
        }
        public void MuiltiSelectPosition_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getFilterComp();
        }


    }
}
