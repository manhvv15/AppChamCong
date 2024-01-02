using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.XuatCong;
//using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong.XuatCongEntities;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{

    public partial class ucXuatCongMoi : UserControl
    {
        BrushConverter bc = new BrushConverter();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        int com_id = 0;
        int ep_id = 0;
        int dep_id = 0;
        int pageNumber = 1;
        int pageSize = 10;
        int idQLC = 0;
        List<XuatCongEntities.DataXuatCong> ListData = new List<XuatCongEntities.DataXuatCong>();
        public ucXuatCongMoi(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;
            com_id = Main.IdAcount;
            searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
            dpStart.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dpEnd.SelectedDate = DateTime.Today;
            NameAccount.Text = Main.txbNameAccount.Text;
            GetListDataChamCong();
            //getData();

        }

        public async void GetListDataChamCong(int pageNumber = 1)
        {
            try
            {
                string token = Properties.Settings.Default.Token;

                string start_date = "";
                if (dpStart.SelectedDate != null)
                {
                    start_date = dpStart.SelectedDate?.ToString("yyyy-MM-dd");
                }
                string end_date = "";
                if (dpEnd.SelectedDate != null)
                {
                    end_date = dpEnd.SelectedDate?.ToString("yyyy-MM-dd");
                }
                int ep_id = 0;
                if (searchBarNhanVien.SelectedIndex != -1)
                {
                    dynamic seletedNV = searchBarNhanVien.SelectedItem;
                    ep_id = seletedNV?.idQLC;
                }
                var bodyObject = new
                {
                    token = token,
                    start_date = start_date,
                    end_date = end_date,
                    com_id = Main.IdAcount,
                    ep_id = ep_id
                };
                string json = JsonConvert.SerializeObject(bodyObject);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/data_xuat_cong");
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    XuatCongEntities.Root result = JsonConvert.DeserializeObject<XuatCongEntities.Root>(responseContent);
                    pagin.SelectedPage = 1;
                    pagin.TotalRecords = result.data.Count;
                    ListData = result.data;
                    dgvXuatCong.ItemsSource = result.data.Skip((pageNumber - 1) * 10).Take(10);

                }


            }
            catch { }
        }
        private async Task getData()
        {
            List<Task> k = new List<Task>()
            {
                getCom().ContinueWith(tt=>this.Dispatcher.Invoke(()=>{
                    if (tt.Result!=null)
                    {
                        listCom=tt.Result.data.items;
                        cboChonCongTy.ItemsSource = listCom;
                        if (listCom!=null&& listCom.Count>0)
                        {
                            cboChonCongTy.SelectedIndex=listCom.Count -1;
                            getDep().ContinueWith(hh=>this.Dispatcher.Invoke(()=>{
                                if (hh.Result!=null)
                                {
                                    listDep=hh.Result.data.items;
                                    listDep.Insert(0,new Item_PhongBan(){ dep_id = -1,dep_name="Phòng ban (tất cả)"});
                                    listDep=listDep.ToList();
                                    cboChonPhongBan.ItemsSource = listDep;
                                    cboChonPhongBan.SelectedIndex = 0;
                                }
                            }));
                        }
                    }
                })),
            };
            k.ForEach(t =>
            {
                t.ContinueWith(tt =>
                {
                    var ck = new List<bool>();
                    k.ForEach(h => ck.Add(h.IsCompleted));
                    if (!ck.Contains(false))
                    {
                        this.Dispatcher.Invoke(() =>
                        {
                            getEp(EpPage, 500).ContinueWith(ee => this.Dispatcher.Invoke(() =>
                            {
                                if (ee.Result != null)
                                {
                                    listEp = ee.Result.data.items;
                                    listEp.Insert(0, new Item_NhanVien() { ep_id = -1, ep_name = "Chọn nhân viên" });
                                    listEp = listEp.ToList();

                                }
                            }));
                            getBangCong().ContinueWith(zz => this.Dispatcher.Invoke(() =>
                            {
                                if (zz.Result.data != null)
                                {
                                    listCong = zz.Result.data.listUser;
                                    foreach (ListUser item in listCong)
                                    {
                                        if (item.avatarUser == "" || item.avatarUser == null)
                                        {
                                            item.avatarUser = "https://chamcong.timviec365.vn/images/ep_logo.png";
                                        }
                                        else
                                        {
                                            item.avatarUser = "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser;
                                        }
                                    }
                                    dgvXuatCong.ItemsSource = listCong;
                                    pagin.TotalRecords = zz.Result.data.listUser.Count;

                                }
                            }));
                        });
                    }
                });
            });

        }

        #region Even
        public MainWindow Main { get; set; }
        private int _IsSmallSize = 0;
        public int IsSmallSize
        {
            get { return _IsSmallSize; }
            set
            {
                _IsSmallSize = value;
                OnPropertyChanged("IsSmallSize");
            }
        }
        private int EpPage = 1;
        private string EpKey = "";

        private void pagin_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            dgvXuatCong.ItemsSource = ListData.Skip((pagin.SelectedPage-1)*10).Take(10);
        }
        private void LocNhanVienChamCong(object sender, MouseButtonEventArgs e)
        {
            pagin.SelectedPage = 0;
            GetListDataChamCong();
        }
        #endregion

        #region Bảng công

        private List<ListUser> _listCongUser;

        public List<ListUser> listCongUser
        {
            get { return _listCongUser; }
            set { _listCongUser = value; OnPropertyChanged(); }
        }

        private List<ListUser> _listCong;
        public List<ListUser> listCong
        {
            get { return _listCong; }
            set { _listCong = value; OnPropertyChanged(); }
        }
        private async Task<Root_TimeKeeping> getBangCong(int page = 1)
        {
            try
            {
                HttpClient http = new HttpClient();
                http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);


                var apilink = APIs.API.XuatCong_Api;
                var pram = new List<string>();
                var form = new Dictionary<string, string>();
                if (cboChonCongTy.SelectedItem != null) form.Add("com_id", cboChonCongTy.SelectedValue.ToString());
                if (cboChonPhongBan.SelectedItem != null && cboChonPhongBan.SelectedIndex >= 1) form.Add("dep_id", cboChonPhongBan.SelectedValue.ToString());


                if (dpStart.SelectedDate != null) form.Add("inputOld", dpStart.SelectedDate.Value.ToString("yyyy/MM/dd"));
                if (dpEnd.SelectedDate != null) form.Add("inputNew", dpEnd.SelectedDate.Value.ToString("yyyy/MM/dd"));

                if (page <= 0) page = 1;
                var offset = (page - 1) * 20;
                form.Add("pageNumber", page.ToString());
                form.Add("pageSize", "20");

                HttpResponseMessage respon = await http.PostAsync(apilink, new FormUrlEncodedContent(form));
                var check = respon.Content.ReadAsStringAsync().Result;
                Root_TimeKeeping api = JsonConvert.DeserializeObject<Root_TimeKeeping>(respon.Content.ReadAsStringAsync().Result);

                if (api.data != null) return api;
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region NhanVien
        public List<string> listCheck { get; set; } = new List<string> { "Nhân viên có chấm công", "Toàn bộ nhân viên" };

        private List<Item_NhanVien> _listEp1;
        public List<Item_NhanVien> listEp1
        {
            get { return _listEp1; }
            set { _listEp1 = value; OnPropertyChanged(); }
        }

        private List<Item_NhanVien> _listEp;
        public List<Item_NhanVien> listEp
        {
            get { return _listEp; }
            set { _listEp = value; OnPropertyChanged(); }
        }
        private async Task<Root_NhanVien> getEp(int page = 1, int length = 20)
        {
            try
            {
                if (cboChonCongTy.SelectedItem != null)
                {
                    HttpClient http = new HttpClient();
                    var apilink = APIs.API.List_ManagerSaff_Api;
                    var pram = new List<string>();
                    Dictionary<string, string> form = new Dictionary<string, string>();

                    http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                    if (cboChonCongTy.SelectedItem != null && cboChonCongTy.SelectedIndex >= 1) form.Add("com_id", cboChonCongTy.SelectedValue.ToString());
                    else form.Add("com_id", Main.IdAcount.ToString());

                    if (cboChonPhongBan.SelectedItem != null && cboChonPhongBan.SelectedIndex >= 1) form.Add("id_com", cboChonPhongBan.SelectedValue.ToString());
                    form.Add("pageNumber", page.ToString());
                    form.Add("pageSize", length.ToString());
                    HttpResponseMessage respon = await http.PostAsync(apilink, new FormUrlEncodedContent(form));

                    Root_NhanVien api = JsonConvert.DeserializeObject<Root_NhanVien>(respon.Content.ReadAsStringAsync().Result);
                    if (api.data != null) return api;
                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private void cboEP_ScrollToEnd()
        {
            EpPage++;
            if (listEp == null) listEp = new List<Item_NhanVien>();
            getEp(EpPage).ContinueWith(tt => this.Dispatcher.Invoke(() =>
            {
                if (tt.Result != null)
                {
                    listEp.AddRange(tt.Result.data.items);
                    listEp = listEp.ToList();

                }
            }));
        }
        private void cboEP_SearchIsNull(object sender, TextChangedEventArgs e)
        {
            if (sender is TextBox)
            {
                EpKey = (sender as TextBox).Text;
                EpPage = 1;
                getEp().ContinueWith(tt => this.Dispatcher.Invoke(() =>
                {
                    if (tt.Result != null)
                    {
                        listEp = tt.Result.data.items;
                        cboChonPhongBan.SelectedIndex = 0;

                    }
                }));
            }
        }
        #endregion

        #region PhongBan

        private List<Item_PhongBan> _listDep;
        public List<Item_PhongBan> listDep
        {
            get { return _listDep; }
            set { _listDep = value; OnPropertyChanged(); }
        }

        private void cboChonPhongBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            getEp().ContinueWith(ep => this.Dispatcher.Invoke(() =>
            {
                if (ep.Result != null)
                {
                    listEp = ep.Result.data.items;

                }
            }));

        }

        private async Task<Root_PhongBan> getDep(int page = 1)
        {
            try
            {
                if (cboChonCongTy.SelectedItem != null)
                {
                    string apilink = APIs.API.List_Room_Api;
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);


                    List<Item_PhongBan> list = new List<Item_PhongBan>();
                    Dictionary<string, string> form = new Dictionary<string, string>();
                    form.Add("com_id", cboChonCongTy.SelectedValue.ToString());
                    var respon = await httpClient.PostAsync(apilink, new FormUrlEncodedContent(form));
                    Root_PhongBan api = JsonConvert.DeserializeObject<Root_PhongBan>(respon.Content.ReadAsStringAsync().Result);
                    if (api.data != null)
                    {
                        return api;
                    }

                }
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region CongTy
        private List<Item_CtyCon> _listCom;
        public List<Item_CtyCon> listCom
        {
            get { return _listCom; }
            set { _listCom = value; OnPropertyChanged(); }
        }
        private async Task<Root_CongTyCon> getCom(int page = 1)
        {
            try
            {
                string apilink = "http://210.245.108.202:3000/api/qlc/company/child/list";
                HttpClient httpClient = new HttpClient();
                Dictionary<string, string> form = new Dictionary<string, string>();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                form.Add("com_id", Main.IdAcount.ToString());


                int i = 0;
                List<Item_CtyCon> list = new List<Item_CtyCon>();

                var respon = await httpClient.PostAsync(apilink, new FormUrlEncodedContent(form));
                Root_CongTyCon ts = JsonConvert.DeserializeObject<Root_CongTyCon>(respon.Content.ReadAsStringAsync().Result);
                if (ts != null)
                {
                    if (ts.data != null && ts.data.items != null)
                    {
                        ts.data.items.RemoveAll(item => item == null);
                        list = ts.data.items;
                        list.Add(new Item_CtyCon()
                        {
                            com_id = Main.IdAcount,
                            com_name = Main.txbNameAccount.Text,

                        });
                        //cboChonCongTy.ItemsSource = ts.data.items;
                    }
                }
                if (ts.data != null) return ts;
                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
        private void cboChonCongTy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cboChonCongTy.SelectedItem != null)
            {
                getDep().ContinueWith(tt => this.Dispatcher.Invoke(() =>
                {
                    if (tt.Result != null)
                    {
                        listDep = tt.Result.data.items;
                        listDep.Insert(0, new Item_PhongBan() { dep_id = -1, dep_name = "Phòng ban (tất cả)" });
                        listDep = listDep.ToList();
                        cboChonPhongBan.ItemsSource = listDep;
                        cboChonPhongBan.SelectedIndex = 0;


                        getEp().ContinueWith(zz => this.Dispatcher.Invoke(() =>
                        {
                            if (zz.Result != null)
                            {
                                listEp = zz.Result.data.items;

                            }
                        }));
                    }
                }));
            }
        }

        #endregion

        #region Xuất Excel
        private async Task ExportExcelDetail(List<XuatCongEntities.DataXuatCong> data, FileInfo file, string startDate = "", string endDate = "")
        {
            try
            {


                if (file.Exists)
                {
                    file.Delete();
                }
                using (var package = new ExcelPackage(file))
                {
                    var ws = package.Workbook.Worksheets.Add("Bảng công theo tháng");

                    ws.Cells["A3"].Offset(0, 0).Value = "Mã NV";
                    ws.Cells["A3"].Offset(0, 1).Value = "Tên nhân viên";
                    ws.Cells["A3"].Offset(0, 2).Value = "Phòng ban";
                    ws.Cells["A3"].Offset(0, 3).Value = "Ngày tháng";
                    ws.Cells["A3"].Offset(0, 4).Value = "Ca làm việc";
                    ws.Cells["A3"].Offset(0, 5).Value = "Thời gian làm việc (giờ)";
                    ws.Cells["A3"].Offset(0, 6).Value = "Đi muộn (Số phút)";
                    ws.Cells["A3"].Offset(0, 7).Value = "Về sớm (số phút)";
                    ws.Cells["A3"].Offset(0, 8).Value = "Công";
                    ws.Cells["A3"].Offset(0, 9).Value = "Tiền";
                    ws.Cells["A3"].Offset(0, 10).Value = "Tiền theo giờ";
                    ws.Cells["A3"].Offset(0, 11).Value = "Cộng công";
                    ws.Cells["A3"].Offset(0, 12).Value = "Cộng tiền";
                    ws.Cells["A3"].Offset(0, 13).Value = "Chi tiết chấm công";

                    int max = 1;

                    ws.Cells["A1"].Value = "CHI TIẾT CHẤM CÔNG CÔNG TY ";
                    ws.Cells[1, 1, 1, 9 + max].Merge = true;
                    ws.Row(1).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Row(1).Style.Font.Bold = true;
                    ws.Row(1).Style.Font.Size = 13;

                    ws.Cells["A2"].Value = string.Format("Từ ngày {0} đến ngày {1}", startDate, endDate);
                    ws.Cells[2, 1, 2, 9 + max].Merge = true;
                    ws.Row(2).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Row(2).Style.Font.Bold = true;
                    ws.Row(2).Style.Font.Size = 13;

                    for (int i = 0; i < max; i++)
                    {

                    }
                    ws.Row(3).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                    ws.Row(3).Style.Font.Bold = true;
                    ws.Row(3).Style.Font.Size = 13;

                    for (int i = 0; i < data.Count; i++)
                    {
                        ws.Row(i + 4).Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        ws.Row(i + 4).Style.Font.Size = 13;
                    }

                    for (int i = 0; i < data.Count; i++)
                    {
                        ws.Cells[4 + i, 1].Value = data[i]?.ep_id;
                        ws.Cells[4 + i, 2].Value = data[i]?.ep_name;

                        ws.Cells[4 + i, 3].Value = data[i]?.organizeDetailName;
                        ws.Cells[4 + i, 4].Value = data[i]?.ts_date_string;
                        ws.Cells[4 + i, 5].Value = data[i]?.shift_name;
                        ws.Cells[4 + i, 6].Value = data[i]?.hour_real;
                        ws.Cells[4 + i, 7].Value = data[i]?.late;
                        ws.Cells[4 + i, 8].Value = data[i]?.early;
                        ws.Cells[4 + i, 9].Value = data[i]?.num_to_calculate;
                        ws.Cells[4 + i, 10].Value = data[i]?.num_to_money;
                        ws.Cells[4 + i, 11].Value = data[i]?.money_per_hour;
                        ws.Cells[4 + i, 12].Value = data[i]?.cong_xn_them;
                        ws.Cells[4 + i, 13].Value = data[i]?.tien_xn_them;
                        //ws.Cells[4 + i, 6].Value = data[i]?.;
                        //ws.Cells[4 + i, 7].Value = data[i]?.early;
                        int k = 14;
                        foreach (var item in data[i].lst_time_string)
                        {
                            ws.Cells[4 + i, k++].Value = item;
                        }
     

                    }
                    ws.Cells["A3:" + ws.Cells.End.Address].AutoFitColumns();
                    package.SaveAsync();
                }
            }
            catch
            {

            }
        }

        private void ExportExcel(object sender, MouseButtonEventArgs e)
        {
            var list = new List<BangCong>();
            var start = dpStart.SelectedDate.Value.ToString("dd/MM/yyyy");
            var end = dpEnd.SelectedDate.Value.ToString("dd/MM/yyyy");
            bool flag = true;
            getXuatExcel();
        }
        private void getXuatExcel()
        {
            try
            {
                using (WebClient http = new WebClient())
                {
                    var apilink = APIs.API.XuatCong_Api;
                    var pram = new List<string>();

                    http.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                    if (cboChonCongTy.SelectedItem != null) http.QueryString.Add("com_id", cboChonCongTy.SelectedValue.ToString());
                    if (cboChonPhongBan.SelectedItem != null && cboChonPhongBan.SelectedIndex >= 1) http.QueryString.Add("dep_id", cboChonPhongBan.SelectedValue.ToString());


                    if (dpStart.SelectedDate != null) http.QueryString.Add("inputOld", dpStart.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    if (dpEnd.SelectedDate != null) http.QueryString.Add("inputNew", dpEnd.SelectedDate.Value.ToString("yyyy-MM-dd"));
                    http.QueryString.Add("pageNumber", "1");
                    http.QueryString.Add("pageSize", "1000000");
                    http.UploadValuesCompleted += (sender, e) =>
                    {
                        Root_TimeKeeping api = JsonConvert.DeserializeObject<Root_TimeKeeping>(UnicodeEncoding.UTF8.GetString(e.Result));
                        if (api.data.listUser != null)
                        {
                            var start = dpStart.SelectedDate.Value.ToString("dd/MM/yyyy");
                            var end = dpEnd.SelectedDate.Value.ToString("dd/MM/yyyy");
                            Microsoft.Win32.SaveFileDialog sv = new Microsoft.Win32.SaveFileDialog();
                            sv.Filter = "Microsoft Excel Worksheet | *.xlsx";
                            sv.FileName = "data";
                            if (sv.ShowDialog() == true)
                            {
                                var data = new List<ListUser>();
                                int max = 1;
                                List<ListUser> list1 = api.data.listUser;
                                list1.ForEach(i =>
                                {
                                    data.Add(new ListUser
                                    {
                                        idQLC = i.idQLC,
                                        userName = i.userName,
                                        avatarUser = i.avatarUser,
                                        lastActivedDate = i.lastActivedDate,
                                        lastActivedTime = i.lastActivedTime,
                                    });
                                });

                                ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                                var file = new FileInfo(sv.FileName);
                                //ExportExcelDetail(data, file, start, end).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                                //{

                                //    System.Diagnostics.Process.Start(file.FullName);

                                //}));

                            }
                        }
                    };
                    http.UploadValuesTaskAsync(new Uri(apilink), http.QueryString);
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion

        private void cboNhanVien_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ExportExcel5(object sender, MouseButtonEventArgs e)
        {
            var list = new List<BangCong>();
            var start = dpStart.SelectedDate.Value.ToString("dd/MM/yyyy");
            var end = dpEnd.SelectedDate.Value.ToString("dd/MM/yyyy");
            bool flag = true;
            getXuatExcel5();
        }
        public void getXuatExcel5()
        {
            try
            {

                var list = new List<BangCong>();
                var start = dpStart.SelectedDate.Value.ToString("dd-MM-yyyy");
                var end = dpEnd.SelectedDate.Value.ToString("dd-MM-yyyy");
                bool flag = true;


                Microsoft.Win32.SaveFileDialog sv = new Microsoft.Win32.SaveFileDialog();
                sv.Filter = "Microsoft Excel Worksheet | *.xlsx";
                sv.FileName = string.Format("Xuất công từ ngày {0} đến ngày {1}", start, end);
                if (sv.ShowDialog() == true)
                {



                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    var file = new FileInfo(sv.FileName);
                    ExportExcelDetail(ListData, file, start, end).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                    {

                        System.Diagnostics.Process.Start(file.FullName);

                    }));
                }
            }
            catch { }
        }

        private void dgvXuatCong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
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

        private void XuatCong_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void XoaCong_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {

                var selectedItem = (sender as StackPanel).DataContext as XuatCongEntities.DataXuatCong;
                if (selectedItem != null)
                {
                    Main.grShowPopup.Children.Add(new ucXoaCongNhanVien(this, selectedItem));
                }
            }
            catch
            {


            }

        }
    }
}
