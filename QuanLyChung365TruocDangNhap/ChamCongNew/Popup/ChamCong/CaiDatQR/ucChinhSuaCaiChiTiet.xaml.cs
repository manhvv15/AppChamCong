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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatQR
{
    /// <summary>
    /// Interaction logic for ucDanhSachQR.xaml
    /// </summary>
    public partial class ucChinhSuaCaiChiTiet : UserControl
    {
        public static List<ItemCbx> listDevices = new List<ItemCbx>()
        {
        new ItemCbx(){Key = "", Value = "Tất cả thiết bị"},
        new ItemCbx(){Key = "1", Value = "CameraAI365"},
        new ItemCbx(){Key = "2", Value = "AppChat365"},
        };

        public class ShiftBody
        {
            public int id { get; set; }
            public int type_shift { get; set; }
        }

        MainWindow Main;
        ListAll QRSetting;
        ucDanhSachChiTiet ucDanhSachChiTiet;
        List<ListUsersDuyet> listUDuyets = new List<ListUsersDuyet>();
        List<ListUsersDuyet> listAddUDuyets = new List<ListUsersDuyet>();
        List<ListQREntities.QRInfo> ListQR = new List<ListQREntities.QRInfo>();
        List<ItemCbx> ListOrganize = new List<ItemCbx>();
        List<ItemCbx> ListPosition = new List<ItemCbx>();
        List<ItemCbx> ListWifi = new List<ItemCbx>();
        List<ItemCbx> ListLocation = new List<ItemCbx>();
        List<ItemCbx> ListStaff = new List<ItemCbx>();
        List<ItemCbx> ListShifts = new List<ItemCbx>();
        List<ItemCbx> ListDevices = new List<ItemCbx>();
        List<Shift> ListShiftWork = new List<Shift>();




        public ucChinhSuaCaiChiTiet(MainWindow main, ListAll QRSetting, ucDanhSachChiTiet ucDanhSachChiTiet, List<ListQREntities.QRInfo> ListQR, List<ItemCbx> ListOrganizeMuiltiCbx, List<ItemCbx> ListPositionMuiltiCbx, List<ItemCbx> ListStaffNameMuiltiCbx, List<ItemCbx> ListShiftWorkMuiltiCbx, List<ItemCbx> ListLocationMuiltiCbx, List<ItemCbx> ListWifiMuiltiCbx)
        {
            InitializeComponent();
            Main = main;
            this.QRSetting = QRSetting;
            this.ucDanhSachChiTiet = ucDanhSachChiTiet;
            this.ListQR = ListQR;
            this.ListOrganize = ListOrganizeMuiltiCbx;
            this.ListPosition = ListPositionMuiltiCbx;
            this.ListStaff = ListStaffNameMuiltiCbx;
            this.ListShifts = ListShiftWorkMuiltiCbx;
            this.ListWifi = ListWifiMuiltiCbx;
            this.ListLocation = ListLocationMuiltiCbx;
            cbxQRCode.ItemsSource = ListQR;
            MuiltiSelectOrganize.ItemsSource = ListOrganizeMuiltiCbx;
            MuiltiSelectPosition.ItemsSource = ListPositionMuiltiCbx;

            MuiltiSelectShiftWork.ItemsSource = ListShiftWorkMuiltiCbx;
            MuiltiSelectLocation.ItemsSource = ListLocationMuiltiCbx;
            MuiltiSelectWifi.ItemsSource = ListWifiMuiltiCbx;
            MuiltiSelectDevice.ItemsSource = listDevices;
        }

        public async void BindingData()
        {
            try
            {


                //await GetListOrganize();
                //await GetListPosition();
                //await GetListLocation();
                await GetListShift();
                //await LoadListStaff();
                //await GetListWifi();
                //await GetListQR();



                txtSettingName.Text = QRSetting.detail.setting_name;
                //cbxQRCode.SelectedItem = ListQR.Where(x => x.id == QRSetting.QRCode_id).FirstOrDefault();
                //cbxOrganize.SelectedItem = ListOrganize.Where(x => x.id == QRSetting.detail.list_org[0]).FirstOrDefault();
                dpStartTime.SelectedDate = QRSetting.detail.start_time.Value.ToLocalTime();
                dpStartTime.Text = QRSetting.detail.start_time.Value.ToLocalTime().ToString();
                dpEndTime.SelectedDate = QRSetting.detail.end_time.Value.ToLocalTime();
                dpEndTime.Text = QRSetting.detail.end_time.Value.ToLocalTime().ToString();
                //var listOrganize = ListOrganize.Where(x => QRSetting.list_org.Contains(x.id)).ToList();
                //var listPosition = ListPosition.Where(x => QRSetting.list_pos.Contains(x.id));
                //// var listShift = ListShiftWork.Where(x => QRSetting.list_shifts.Contains(x.shift_id));
                //var listUser = ListStaff.Where(x => QRSetting.listUsers.Contains(x.ep_id));
                //var listIp = ListWifi.Where(x => QRSetting.list_ip.Contains(x.id));
                //var listDevice = listDevices.Where(x => QRSetting.list_device.Contains(int.Parse(x.Key)));
                //var listOrganizeInMuiltiBox = new List<ItemCbx>();
                //foreach (var item in listOrganize)
                //{

                //    listOrganizeInMuiltiBox.Add(new ItemCbx() { Key = item.id.ToString(), Value = item.organizeDetailName });
                //}
                //MuiltiSelectOrganize.SelectList = listOrganizeInMuiltiBox;

                //var listPositionInMuiltiBox = new List<ItemCbx>();
                //foreach (var item in listPosition)
                //{

                //    listPositionInMuiltiBox.Add(new ItemCbx() { Key = item.id.ToString(), Value = item.positionName });
                //}

                List<ItemCbx> listOrganizeMuilti = new List<ItemCbx>();
                foreach (int item in QRSetting.detail.list_org)
                {
                    var itemCbx = ListOrganize.Where(x => x.Key == item.ToString()).FirstOrDefault();
                    if (itemCbx != null) listOrganizeMuilti.Add(itemCbx);
                }
                if (listOrganizeMuilti.Count == 0)
                {
                    listOrganizeMuilti.Add(ListOrganize[0]);

                }


                MuiltiSelectOrganize.SelectList = listOrganizeMuilti;

                List<ItemCbx> listPositionMuilti = new List<ItemCbx>();
                foreach (int item in QRSetting.detail.list_pos)
                {
                    var itemCbx = ListPosition.Where(x => x.Key == item.ToString()).FirstOrDefault();
                    if (itemCbx != null) listPositionMuilti.Add(itemCbx);
                }
                if (listPositionMuilti.Count == 0)
                {
                    listPositionMuilti.Add(ListPosition[0]);

                }


                MuiltiSelectPosition.SelectList = listPositionMuilti;



                //Mui.SelectList = ListDevices.Where(x => QRSetting.list_device.Contains(int.Parse(x.Key))).ToList();

                //var listIpInMuiltiBox = new List<ItemCbx>();
                //foreach (var item in listIp)
                //{

                //    listIpInMuiltiBox.Add(new ItemCbx() { Key = item.id.ToString(), Value = item.ip_access });
                //}
                List<ItemCbx> listWifiMuilti = new List<ItemCbx>();
                foreach (var item in QRSetting.detail.list_wifi)
                {
                    var itemCbx = ListWifi.Where(x => x.Key == item.ToString()).FirstOrDefault();
                    if (itemCbx != null) listWifiMuilti.Add(itemCbx);
                }
                if (QRSetting.detail.type_wifi == 2)
                {
                    listWifiMuilti.Add(ListWifi[1]);

                }
                else if (QRSetting.detail.type_wifi == 3)
                {
                    listWifiMuilti.Add(ListWifi[0]);
                }


                MuiltiSelectWifi.SelectList = listWifiMuilti;

                //var listUserInMuiltiBox = new List<ItemCbx>();
                //foreach (var item in listUser)
                //{

                //    listUserInMuiltiBox.Add(new ItemCbx() { Key = item.ep_id.ToString(), Value = item.userName });
                //}
                List<ItemCbx> listStaffNameMuilti = new List<ItemCbx>();
                getFirstFilterComp();
                foreach (var item in QRSetting.list_emps)
                {
                    var itemCbx = ListStaff.Where(x => x.Key == item.idQLC.ToString()).FirstOrDefault();
                    if (itemCbx != null) listStaffNameMuilti.Add(itemCbx);
                }
                if (listStaffNameMuilti.Count == 0)
                {
                    listStaffNameMuilti.Add(ListStaff[0]);

                }


                MuiltiSelectStaffName.SelectList = listStaffNameMuilti;


                List<ItemCbx> listShiftMuilti = new List<ItemCbx>();
                foreach (var item in QRSetting.list_shifts)
                {
                    var itemCbx = ListShifts.Where(x => x.Key == item.shift_id.ToString()).FirstOrDefault();
                    if (itemCbx != null) listShiftMuilti.Add(itemCbx);
                }
                if (listShiftMuilti.Count == 0)
                {
                    listShiftMuilti.Add(ListShifts[0]);

                }


                MuiltiSelectShiftWork.SelectList = listShiftMuilti;


                List<ItemCbx> listLocationMuilti = new List<ItemCbx>();
                foreach (var item in QRSetting.list_loc)
                {
                    var itemCbx = ListLocation.Where(x => x.Key == item.cor_id.ToString()).FirstOrDefault();
                    if (itemCbx != null) listLocationMuilti.Add(itemCbx);
                }
                if (QRSetting.detail.type_loc == 2)
                {
                    listLocationMuilti.Add(ListLocation[1]);
                }
                else if (QRSetting.detail.type_loc == 3)
                {
                    listLocationMuilti.Add(ListLocation[0]);

                }
                MuiltiSelectLocation.SelectList = listLocationMuilti;


                List<ItemCbx> listDeviceMuilti = new List<ItemCbx>();
                foreach (var item in QRSetting.detail.list_device)
                {
                    var itemCbx = listDevices.Where(x => x.Value == ((item.ToString() == "web") ? "CameraAI365" : "AppChat365")).FirstOrDefault();
                    if (itemCbx != null) listDeviceMuilti.Add(itemCbx);
                }
                if (listDeviceMuilti.Count == 0)
                {
                    listDeviceMuilti.Add(listDevices[0]);

                }


                MuiltiSelectDevice.SelectList = listDeviceMuilti;
            }
            catch { }

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

        #endregion

        #region CallAPI
        //public async Task GetListOrganize()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.listAll_organize);

        //        request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
        //        var content = new MultipartFormDataContent();
        //        content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
        //        request.Content = content;
        //        var response = await client.SendAsync(request);
        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseContent = await response.Content.ReadAsStringAsync();
        //            ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);

        //            if (result.data.data != null)
        //            {
        //                ListOrganize = result.data.data;
        //                cbxOrganize.ItemsSource = ListOrganize;
        //            }
        //        }
        //    }
        //    catch
        //    {

        //    }
        //}


        //private async Task GetListPosition()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.list_position);
        //        request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
        //        var content = new MultipartFormDataContent();
        //        content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
        //        request.Content = content;
        //        var response = await client.SendAsync(request);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            var responseContent = await response.Content.ReadAsStringAsync();
        //            ListPositionEntities.Root result = JsonConvert.DeserializeObject<ListPositionEntities.Root>(responseContent);
        //            if (result.data.data != null)
        //            {
        //                ListPosition = result.data.data;
        //                List<ItemCbx> list = new List<ItemCbx>();
        //                foreach (var item in result.data.data) { list.Add(new ItemCbx() { Key = item.id.ToString(), Value = item.positionName }); }
        //                MuiltiSelectPosition.ItemsSource = list;

        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public async Task GetListWifi()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.list_wifi_api);
        //        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        var responseContent = await response.Content.ReadAsStringAsync();

        //        RootWifi loadListWifi = JsonConvert.DeserializeObject<RootWifi>(responseContent);

        //        if (loadListWifi.data.data != null)
        //        {
        //            ListWifi = loadListWifi.data.data;
        //            List<ItemCbx> list = new List<ItemCbx>();
        //            foreach (var item in loadListWifi.data.data) { list.Add(new ItemCbx() { Key = item.id.ToString(), Value = item.ip_access }); }
        //            MuiltiSelectWifi.ItemsSource = list;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        //public async Task GetListLocation()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/location/list");
        //        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
        //        var response = await client.SendAsync(request);

        //        var responseContent = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            API_Location.Root result = JsonConvert.DeserializeObject<API_Location.Root>(responseContent);
        //            ListLocation = result.data.list;
        //            List<ItemCbx> list = new List<ItemCbx>();
        //            foreach (var item in result.data.list) { list.Add(new ItemCbx() { Key = item.cor_id.ToString(), Value = item.cor_location_name }); }

        //            MuiltiSelectLocation.ItemsSource = list;
        //        }
        //    }
        //    catch
        //    {
        //    }


        //}
        //public async Task GetListQR()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/qrCode/listAll");
        //        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
        //        var response = await client.SendAsync(request);

        //        var responseContent = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            ListQREntities.Root result = JsonConvert.DeserializeObject<ListQREntities.Root>(responseContent);
        //            ListQR = result.data.data;
        //            cbxQRCode.ItemsSource = ListQR;
        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        public async Task GetListShift()
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
            }
            catch { }
        }
        //public async Task LoadListStaff()
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
        //            List<ItemCbx> list = new List<ItemCbx>();
        //            foreach (var item in resultSaff.data.data) { list.Add(new ItemCbx() { Key = item.ep_id.ToString(), Value = item.userName }); }
        //            MuiltiSelectStaffName.ItemsSource = list;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}
        public async Task DeleteQR(int id)
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
        public async Task UpdateQRSetting()
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
                        setting_id = QRSetting.detail.setting_id,
                        setting_name = txtSettingName.Text,
                        //QRCode_id = cbxQRCode.SelectedValue,
                        list_org = MuiltiSelectOrganize.SelectedList.Select(x => int.Parse(x.Key)),
                        list_pos = MuiltiSelectPosition.SelectedList.Select(x => int.Parse(x.Key)),
                        list_emps = MuiltiSelectStaffName.SelectedList.Select(x => int.Parse(x.Key)),
                        start_time = dpStartTime.SelectedDate,
                        end_time = dpEndTime.SelectedDate,
                        list_shifts = listShiftBody,
                        list_loc = list_loc,
                        list_wifi = list_ip,
                        list_device = MuiltiSelectDevice.SelectedList.Select(x => (x.Value == "CameraAI365") ? "web" : "app"),
                        type_loc = type_loc,
                        type_wifi = type_ip,
                        app = 1
                    };
                    string json = JsonConvert.SerializeObject(bodyObject, Formatting.Indented);

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingTimesheet/edit");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new StringContent(json, null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    MessageEntities.Root result = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                    if (response.IsSuccessStatusCode)
                    {

                        this.Visibility = Visibility.Collapsed;
                        this.ucDanhSachChiTiet.GetListDetail();
                        Main.grShowPopup.Children.Add(new ucPopupSuccess(result.data.message));

                    }
                    else
                    {
                        Main.grShowPopup.Children.Add(new ucPopupError(result.error.message));
                    }
                }

            }
            catch (Exception ex) { Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra")); }

        }

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

                    var listIdOrgSeleted = MuiltiSelectOrganize.SelectedList.Select(x => int.Parse(x.Key)).ToList();
                    if (listIdOrgSeleted.Count > 0)
                    {
                        List<ListUser> listUsers = new List<ListUser>();
                        foreach (var item in listIdOrgSeleted)

                        {
                            listUsers.AddRange(api.data.listUsers.Where(user => user.dep.Select(dep => dep.id.Value).Contains(item)).ToList());
                        }
                        api.data.listUsers = listUsers.GroupBy(p => p.idQLC).Select(group => group.First()).ToList();
                    }

                    List<ItemCbx> list = new List<ItemCbx>(); list.Add(new ItemCbx() { Key = "", Value = "Tất cả nhân viên" });
                    foreach (var item in api.data.listUsers) { list.Add(new ItemCbx() { Key = item.idQLC.ToString(), Value = item.userName }); }
                    MuiltiSelectStaffName.ItemsSource = list;
                    MuiltiSelectStaffName.SelectList = new List<ItemCbx>();
                    ListStaff = list;
                }




            }
            catch (Exception ex)
            {
                // MessageBox.Show("loi load list " + ex.Message);
            }
        }
        private async void getFirstFilterComp()
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

                    List<ItemCbx> list = new List<ItemCbx>(); list.Add(new ItemCbx() { Key = "", Value = "Tất cả nhân viên" });
                    foreach (var item in api.data.listUsers) { list.Add(new ItemCbx() { Key = item.idQLC.ToString(), Value = item.userName }); }
                    MuiltiSelectStaffName.ItemsSource = list;
                    ListStaff = list;
                }




            }
            catch (Exception ex)
            {
                // MessageBox.Show("loi load list " + ex.Message);
            }
        }
        #endregion

        private void Edit_MouseUp(object sender, MouseButtonEventArgs e)
        {

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
            UpdateQRSetting();
        }

        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private bool Validate()
        {
            if (txtSettingName.Text == "")
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Tên cài đặt không được để trống"));
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
            else if (MuiltiSelectDevice.SelectList.Count == 0)
            {
                Main.grShowPopup.Children.Add(new ucNotificationPopup("Vui lòng chọn thiết bị"));
                return false;
            }
            return true;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            BindingData();
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
