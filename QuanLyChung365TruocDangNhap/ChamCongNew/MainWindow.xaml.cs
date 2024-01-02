using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Security.Policy;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using QuanLyChung365TruocDangNhap.ChamCongNew.Login;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;
using QuanLyChung365TruocDangNhap.ChamCongNew.Pages.ManageRecruitmentPages.ListOfCandidatesPages;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.Salarysettings;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.DiMuonVeSom;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
//using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json;
using RestSharp;
using Brush = System.Windows.Media.Brush;
using AForge.Video.DirectShow;
using QuanLyChung365TruocDangNhap.Popup.OOP.Login;

namespace QuanLyChung365TruocDangNhap.ChamCongNew
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BrushConverter bcMain = new BrushConverter();
        private frmDSDiMuonVeSom frm;
        public string Nam = "";
        public string Thang = "";
        public string PhongBan = "";
        public string ToChuc = "";
        public string NhanVien = "";
        public int Back;
        public int IdAcount;
        private string Token;
        private string Pass;
        public VideoCaptureDevice videoSource;
        private ucChooseLoginOptions frmLogin;
        public int Type { get; set; }
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVienThuocCongTy = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public List<List_NhanVien> lstNhanVienThuocCongTy1 = new List<List_NhanVien>();
        public List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBanThuocCongTy = new List<OOP.clsPhongBanThuocCongTy.Item>();
        public OOP.Login.LoginCom.Data data { get; set; }
        public MainWindow(int Id, string NameAcount, string tk, ucChooseLoginOptions ucLog)
        {
            InitializeComponent();
            
            frmLogin = ucLog;
            IdAcount = Id;
            Token = tk;
            GetAvatarEp();
            txbNameAccount.Text = NameAcount;
            txbIDAccount.Text = Id.ToString();
            ucBodyHome ucbodyhome = new ucBodyHome(this);
            dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            dopBody.Children.Add(Content as UIElement);
            txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text;

            ucFunctionCompanyManager uc = new ucFunctionCompanyManager(this, ucbodyhome);
            Back = 1;
            ucbodyhome.grLoadFunction.Children.Clear();
            object Content1 = uc.Content;
            uc.Content = null;
            ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
            ucbodyhome.txbChamCong.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");


        }
        //MainLogin 
        //ucChooseLoginOptions ucLog
        frmMain frmMain;
        QuanLyChung365TruocDangNhap.Popup.OOP.Login.clsLogin.Data data1;
        public MainWindow(clsLogin.Data data, frmMain frmmain)
        {
            InitializeComponent();
            this.data1 = data;
            //this.data = data;
            Pass = data.data.user_info.com_pass;
            frmMain = frmmain;
            //frmLogin = ucLog;
            IdAcount = data.data.user_info.com_id;
            Token = data.access_token;
            txbNameAccount.Text = data.data.user_info.com_name;
            txbIDAccount.Text = data.data.user_info.com_id.ToString();
            GetAvatarCom();
            Setup();
            ucBodyHome ucbodyhome = new ucBodyHome(this);
            dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            dopBody.Children.Add(Content as UIElement);
            //txbLoadChamCong.Text = ucbodyhome.txbQuanLyCongTy.Text;
            frmmain.BackToBack = "ChamCong";
            ucFunctionCompanyManager uc = new ucFunctionCompanyManager(this, ucbodyhome);
            ucbodyhome.grLoadFunction.Children.Clear();
            object Content1 = uc.Content;
            uc.Content = null;
            ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
            ucbodyhome.txbQuanLyCongTy.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
            LoadDLDanhSachNVThuocCongTy();
            LoadDanhSachTatCaNhanVien();
            LoadDLPhongBanThuocCongTy();
            GetListOrganize();
            StartDynamicText();
        }
        private Thread textUpdateThread;
        private bool isUpdatingText = false;

        private void StartDynamicText()
        {
            try
            {
                isUpdatingText = true;

                textUpdateThread = new Thread(() =>
                {
                    while (isUpdatingText)
                    {
                        Dispatcher.Invoke(() =>
                        {
                            dynamicTextBlock.Text = "" + DateTime.Now.ToString("HH:mm:ss");
                        });

                        Thread.Sleep(1000);
                    }
                });

                textUpdateThread.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
        private void StopDynamicText()
        {
            isUpdatingText = false;

            if (textUpdateThread != null && textUpdateThread.IsAlive)
            {
                textUpdateThread.Join();
            }
        }
        #region GetListOrganize
        private List<OrganizeData> _lstOrganizeData;
        public List<OrganizeData> lstOrganizeData
        {
            get { return _lstOrganizeData; }
            set { _lstOrganizeData = value; }
        }


        public async void GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.listAll_organize);

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    Root result = JsonConvert.DeserializeObject<Root>(responseContent);

                    if (result.data.data != null)
                    {
                        lstOrganizeData = result.data.data;
                        OOP.OrganizeData dep = new OOP.OrganizeData();
                        dep.id = 0;
                        dep.organizeDetailName = "Tổ chức (tất cả)";
                        lstOrganizeData.Insert(0, dep);
                    }
                }
            }
            catch
            {

            }
        }
        #endregion
        private async void LoadDLPhongBanThuocCongTy()
        {

            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3000/api/qlc/department/list")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("com_id", IdAcount);
                    RestResponse resAlbum = await restclient.ExecuteAsync(request);
                    var b = resAlbum.Content;
                    OOP.clsPhongBanThuocCongTy.Root PBthuocCty = JsonConvert.DeserializeObject<OOP.clsPhongBanThuocCongTy.Root>(b);
                    if (PBthuocCty.data != null)
                    {
                        lstPhongBanThuocCongTy = PBthuocCty.data.items;
                        OOP.clsPhongBanThuocCongTy.Item dep = new OOP.clsPhongBanThuocCongTy.Item();
                        dep.dep_id = 0;
                        dep.dep_name = "Phòng ban (tất cả)";
                        lstPhongBanThuocCongTy.Insert(0, dep);
                    }
                }
            }
            catch
            {

            }
        }

        public async void LoadDanhSachTatCaNhanVien()
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
                    lstNhanVienThuocCongTy1 = resultSaff.data.data;
                    List_NhanVien us = new List_NhanVien();
                    us.ep_id = 0;
                    us.userName = "Tất cả nhân viên";
                    lstNhanVienThuocCongTy1.Insert(0, us);
                }
            }
            catch (Exception)
            {
            }
        }
        public async void LoadDLDanhSachNVThuocCongTy()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/list_em")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddHeader("Authorization", Properties.Settings.Default.Token);
                    request.AddParameter("id_com", IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = await restclient.ExecuteAsync(request);
                    var b = resAlbum.Content;
                    OOP.clsNhanVienThuocCongTy.Root NVthuocCty = JsonConvert.DeserializeObject<OOP.clsNhanVienThuocCongTy.Root>(b);
                    if (NVthuocCty != null)
                    {
                        lstNhanVienThuocCongTy = NVthuocCty.data.listUser;
                        clsNhanVienThuocCongTy.ListUser us = new clsNhanVienThuocCongTy.ListUser();
                        us.idQLC = 0;
                        us.userName = "Tất cả nhân viên";
                        lstNhanVienThuocCongTy.Insert(0, us);

                    }
                }
            }
            catch
            {
            }
        }

        private async void GetAvatarCom()
        {
            var options = new RestClientOptions("http://210.245.108.202:3000")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/api/qlc/company/info", Method.Post);
            request.AddHeader("Authorization", "Bearer " + Token);
            request.AlwaysMultipartFormData = true;
            request.AddParameter("ma_xt", IdAcount);
            RestResponse response = await client.ExecuteAsync(request);
            OOP.Login.InfoCom.Root receivedInfo = JsonConvert.DeserializeObject<OOP.Login.InfoCom.Root>(response.Content);
            if (receivedInfo.data != null)
            {
                string[] Img = receivedInfo.data.data.avatarUser.Split(new[] { "https://cdn.timviec365.vn/upload/employee/ep" + IdAcount.ToString() }, StringSplitOptions.None);
                Uri DuongDan = new Uri("https://chamcong.24hpay.vn/upload/employee/" + Img[Img.Length - 1]);
                if (DuongDan != null)
                {
                    BitmapImage bm = new BitmapImage(DuongDan);
                    Avatar.ImageSource = bm;
                }
            }
        }

        private async void GetAvatarEp()
        {
            try
            {
                var options = new RestClientOptions("http://210.245.108.202:3000")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/api/qlc/employee/info", Method.Post);
                request.AddHeader("Authorization", "Bearer " + Token);
                request.AlwaysMultipartFormData = true;
                request.AddParameter("ma_xt", IdAcount);
                RestResponse response = await client.ExecuteAsync(request);
                //MessageBox.Show(response.Content);
                OOP.Login.InfoEp.Root receivedInfo = JsonConvert.DeserializeObject<OOP.Login.InfoEp.Root>(response.Content);
                if (receivedInfo.data != null)
                {
                    string[] Img = receivedInfo.data.data.avatarUser.Split(new[] { "https://cdn.timviec365.vn/upload/employee/ep" + IdAcount.ToString() }, StringSplitOptions.None);
                    Uri DuongDan = new Uri("https://chamcong.24hpay.vn/upload/employee/" + Img[Img.Length - 1]);
                    BitmapImage bm = new BitmapImage(DuongDan);
                    Avatar.ImageSource = bm;
                }


            }
            catch
            {

            }
        }

        private void StopCamera()
        {
            if (videoSource != null && videoSource.IsRunning)
            {
                videoSource.SignalToStop();
                videoSource = null;
               
            }
        }
        private void bodBackto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Back == 1)
            {
                StopCamera();
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);
                txbLoadChamCong.Text = "";
                i = 0;
                LableFunction.Visibility = Visibility.Collapsed;

                ucDanhSachChucNangChamCong uc = new ucDanhSachChucNangChamCong(this, IdAcount);
                ucbodyhome.grLoadFunction.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
                ucbodyhome.txbChamCong.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
                scrollMain.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Disabled;
               
            }
            else if (Back == 4)
            {
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);
                txbLoadChamCong.Text = "";
                i = 0;
                LableFunction.Visibility = Visibility.Collapsed;

                ucListSalarySettings uc = new ucListSalarySettings(this);
                ucbodyhome.grLoadFunction.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
                ucbodyhome.txbSalarySettings.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
                scrollMain.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Disabled;
                
            }
            else if (Back == 3)
            {
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);
                txbLoadChamCong.Text = "";
                i = 0;
                LableFunction.Visibility = Visibility.Collapsed;

                Body uc = new Body(this);
                ucbodyhome.grLoadFunction.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
                ucbodyhome.txbDeXuat.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
                scrollMain.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Disabled;
                
            }
            else if (Back == 10)
            {
                Back = 4;
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                ucCaiDatLuongCoBan uc = new ucCaiDatLuongCoBan(this);
                dopBody.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                dopBody.Children.Add(Content as UIElement);
                LableFunction.Visibility = Visibility.Visible;

            }
            else if (Back == 21)
            {
                Back = 4;
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                ucHoaHong uc = new ucHoaHong(this);
                dopBody.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                dopBody.Children.Add(Content as UIElement);
                LableFunction.Visibility = Visibility.Visible;
                ucListSalarySettings ucl = new ucListSalarySettings(this);
                txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text;
            }
            else if (Back == 22)
            {
                Back = 4;
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                ucHoaHong uc = new ucHoaHong(this);
                dopBody.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                dopBody.Children.Add(Content as UIElement);
                LableFunction.Visibility = Visibility.Visible;
                ucListSalarySettings ucl = new ucListSalarySettings(this);
                txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text;
            }
            else if (Back == 23)
            {
                Back = 4;
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                ucHoaHong uc = new ucHoaHong(this);
                dopBody.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                dopBody.Children.Add(Content as UIElement);
                LableFunction.Visibility = Visibility.Visible;
                ucListSalarySettings ucl = new ucListSalarySettings(this);
                txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text;

            }
            else if (Back == 24)
            {
                Back = 4;
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                ucHoaHong uc = new ucHoaHong(this);
                dopBody.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                dopBody.Children.Add(Content as UIElement);
                LableFunction.Visibility = Visibility.Visible;
                ucListSalarySettings ucl = new ucListSalarySettings(this);
                txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text;
            }
            else if (Back == 25)
            {
                Back = 4;
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                ucHoaHong uc = new ucHoaHong(this);
                dopBody.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                dopBody.Children.Add(Content as UIElement);
                LableFunction.Visibility = Visibility.Visible;
                ucListSalarySettings ucl = new ucListSalarySettings(this);
                txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text;
            }
            else if (Back == 2)
            {
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);
                txbLoadChamCong.Text = "";
                i = 0;
                LableFunction.Visibility = Visibility.Collapsed;

                ucFunctionCompanyManager uc = new ucFunctionCompanyManager(this, ucbodyhome);
                ucbodyhome.grLoadFunction.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
                scrollMain.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Disabled;
            }
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {

            if (k == 0)
            {
                if (i == 0)
                {
                    if (MainWindows.Width <= 1024)
                    {

                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;


                    }

                    else
                    {

                        HearderColesspa.Visibility = Visibility.Collapsed;
                        HearderVisibility.Visibility = Visibility.Visible;
                        MenuCollapsed.Visibility = Visibility.Collapsed;


                    }

                }
                else if (i == 2 || i == 3)
                {
                    if (MainWindows.Width <= 865)
                    {
                        j = 3;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;


                    }
                    else if (MainWindows.Width <= 872)
                    {
                        j = 1;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;

                    }
                    else if (MainWindows.Width <= 1024)
                    {
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;

                    }

                    else if (MainWindows.Width <= 1138)
                    {

                        j = 1;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;



                    }
                    else
                    {
                        j = 2;
                        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                        dopBody.Children.Clear();
                        object Content = frm.Content;
                        frm.Content = null;
                        dopBody.Children.Add(Content as UIElement);

                        HearderColesspa.Visibility = Visibility.Collapsed;
                        HearderVisibility.Visibility = Visibility.Visible;
                        MenuCollapsed.Visibility = Visibility.Collapsed;

                    }
                }
            }
            else
            {
                HearderColesspa.Visibility = Visibility.Collapsed;
                HearderVisibility.Visibility = Visibility.Visible;
                MenuCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void SlineBar_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (MenuCollapsed.Visibility == Visibility.Collapsed)
            {
                MenuCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                MenuCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void bodBackto_MouseEnter(object sender, MouseEventArgs e)
        {
            txbBackToBack.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
        }

        private void bodBackto_MouseLeave(object sender, MouseEventArgs e)
        {
            txbBackToBack.Foreground = (Brush)bcMain.ConvertFrom("#474747");
        }
        public int k = 0;
        public int i = 0;
        public int j = 0;
        public int m = 0;
        private void MainWindows_StateChanged(object sender, EventArgs e)
        {

        }



        private void MainWindows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            grShowPopup.Children.Add(new Popup.PopUpHoiTruocKhiDangXuat(this, frmLogin));
        }

        private void borThongTin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (borThongTinChiTiet.Visibility == Visibility.Collapsed)
            //{
            //    borThongTinChiTiet.Visibility = Visibility.Visible;
            //    popup.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    borThongTinChiTiet.Visibility = Visibility.Collapsed;
            //    popup.Visibility = Visibility.Collapsed;
            //}
        }

        private void clearPopUp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borThongTinChiTiet.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
        }

        private void LogOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            grShowPopup.Children.Add(new Popup.PopUpHoiTruocKhiDangXuat(this, frmLogin));
        }

        private void btnMaximize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            btnMaximize.Visibility = Visibility.Collapsed;
            btnNomal.Visibility = Visibility.Visible;
            var workingArea = System.Windows.SystemParameters.WorkArea;
            this.WindowState = WindowState.Normal;
            Left = workingArea.TopLeft.X;
            Top = workingArea.TopLeft.Y;
            Width = workingArea.Width;
            Height = workingArea.Height;
            this.ResizeMode = ResizeMode.NoResize;

        }

        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textUpdateThread != null && textUpdateThread.IsAlive)
            {
                textUpdateThread.Abort(); // Tắt luồng
            }
            StopCamera();
            Application.Current.Shutdown();
            StopDynamicText();

            //grShowPopup.Children.Add(new Popup.PopUpHoiTruocKhiDangXuat(this, frmLogin));
        }

        private void btnNomal_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            btnMaximize.Visibility = Visibility.Visible;
            btnNomal.Visibility = Visibility.Collapsed;
            var workingArea = System.Windows.SystemParameters.WorkArea;
            this.WindowState = WindowState.Normal;
            Width = workingArea.Right - 180;
            Height = workingArea.Bottom - 100;
            Left = (workingArea.Right / 2) - (this.ActualWidth / 2);
            Top = (workingArea.Bottom / 2) - (this.ActualHeight / 2);
            this.ResizeMode = ResizeMode.CanResize;
            if (k == 1)
            {

                k = 0;


            }

        }

        private void pnlTieuDe1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnThongTinTK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://chamcong.timviec365.vn/thong-bao.html?s=f30f0b61e761b8926941f232ea7cccb9." + IdAcount + "." + Pass + "&link=https://quanlychung.timviec365.vn/quan-ly-thong-tin-tai-khoan-cong-ty.html");

        }

        private void btnMinimize_MouseEnter(object sender, MouseEventArgs e)
        {
            btnMinimize.Background = (Brush)bcMain.ConvertFrom("#61F1DB");
        }

        private void btnMinimize_MouseLeave(object sender, MouseEventArgs e)
        {
            btnMinimize.Background = (Brush)bcMain.ConvertFrom("#FFFFFF");
        }

        private void btnMaximize_MouseEnter(object sender, MouseEventArgs e)
        {
            btnMaximize.Background = (Brush)bcMain.ConvertFrom("#61F1DB");
        }

        private void btnMaximize_MouseLeave(object sender, MouseEventArgs e)
        {
            btnMaximize.Background = (Brush)bcMain.ConvertFrom("#FFFFFF");
        }


        private void btnClose_MouseEnter(object sender, MouseEventArgs e)
        {
            btnClose.Background = (Brush)bcMain.ConvertFrom("#FF5B4D");
        }

        private void btnClose_MouseLeave(object sender, MouseEventArgs e)
        {
            btnClose.Background = (Brush)bcMain.ConvertFrom("#FFFFFF");
        }

        private void btnNomal_MouseEnter(object sender, MouseEventArgs e)
        {
            btnNomal.Background = (Brush)bcMain.ConvertFrom("#61F1DB");
        }

        private void btnNomal_MouseLeave(object sender, MouseEventArgs e)
        {
            btnNomal.Background = (Brush)bcMain.ConvertFrom("#FFFFFF");
        }
        public Key keyDown { get; set; }
        private void ucSalary_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                keyDown = e.Key;
            }
        }

        private void ucSalary_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == keyDown)
            {
                keyDown = Key.Cancel;
            }
        }
        public void ShowPopup(object obj)
        {
            try
            {

                if (obj.ToString() == "")
                {
                    HidePopup();
                }
                else if (obj.ToString() == "1") // ẩn không hiệu ứng
                {
                    if (svPopup.Children.Count > 1)
                    {
                        int count = svPopup.Children.Count;
                        for (int i = count - 1; i > 0; i--)
                        {
                            svPopup.Children.RemoveAt(i);
                        }
                        gridPopup.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    svPopup.Children.Add((UserControl)obj);
                    gridPopup.Visibility = Visibility.Visible;

                    Storyboard storyboard = new Storyboard();
                    TimeSpan duration = TimeSpan.FromSeconds(0.3);
                    DoubleAnimation animation = new DoubleAnimation();
                    animation.From = 0.0;
                    animation.To = 1.0;
                    animation.Duration = new Duration(duration);
                    Storyboard.SetTargetName(animation, "scPopup");
                    Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));
                    storyboard.Children.Add(animation);
                    storyboard.Begin(this);

                    var sb = new Storyboard();
                    var ta = new ThicknessAnimation();
                    ta.BeginTime = new TimeSpan(0);
                    ta.SetValue(Storyboard.TargetNameProperty, "scPopup");
                    Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

                    ta.From = new Thickness(0, -300, 0, 300);
                    ta.To = new Thickness(0, 20, 0, 0);
                    ta.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                    sb.Children.Add(ta);
                    sb.Begin(this);


                }
            }
            catch { }
        }

        public void HidePopup()
        {
            if (gridPopup.Visibility == Visibility.Visible)
            {
                EventHandler onComplete = null;
                onComplete = (s, e) =>
                {
                    if (svPopup.Children.Count > 1)
                    {
                        int count = svPopup.Children.Count;
                        for (int i = count - 1; i > 0; i--)
                        {
                            svPopup.Children.RemoveAt(i);
                        }
                    }
                    //svPopup.Children.Clear();
                    //svPopup.Children.RemoveAt(1);
                    gridPopup.Visibility = Visibility.Collapsed;
                };
                Storyboard storyboard = new Storyboard();
                TimeSpan duration = TimeSpan.FromSeconds(0.3);
                DoubleAnimation animation = new DoubleAnimation();
                animation.From = 1.0;
                animation.To = 0.0;
                animation.Duration = new Duration(duration);
                Storyboard.SetTargetName(animation, "scPopup");
                Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));
                storyboard.Children.Add(animation);
                storyboard.Begin(this);

                var sb = new Storyboard();
                var ta = new ThicknessAnimation();
                ta.BeginTime = new TimeSpan(0);
                ta.SetValue(Storyboard.TargetNameProperty, "scPopup");
                Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));

                ta.From = new Thickness(0, 20, 0, 0);
                ta.To = new Thickness(0, -300, 0, 300);
                ta.Duration = new Duration(TimeSpan.FromSeconds(0.25));
                sb.Children.Add(ta);
                sb.Completed += onComplete;
                sb.Begin(this);
                //if (svPopup.Children.Count > 1)
                //    svPopup.Children.RemoveAt(1);
                //gridPopup.Visibility = Visibility.Collapsed;
            }


        }
        private void Setup()
        {

            //MainContent.NavigationService.Navigate(new HomePage(Token, Id, UserName));


            ListOfCandidatesPage.onShowPopup += ShowPopup;

            CandidateWarehousePage.onShowPopup += ShowPopup;

            CandidateDetailPage.onShowPopup += ShowPopup;


            CandidateDetailProcessInterviewPage.onShowPopup += ShowPopup;
            CandidateDetailGetJobPage.onShowPopup += ShowPopup;
            CandidateDetailFailJobPage.onShowPopup += ShowPopup;
            CandidateDetailCancelJobPage.onShowPopup += ShowPopup;
            CandidateDetailContractJobPage.onShowPopup += ShowPopup;
            ucXemChiTietLichLamViec.onShowPopup += ShowPopup;
            //PositionChart.onShowPopup += ShowPopup;

            //ChooseTransportProcess.hidePopup1 += ShowPopup;
        }

        public void UnSetup()
        {

            //MainContent.NavigationService.Navigate(new HomePage(Token, Id, UserName));


            ListOfCandidatesPage.onShowPopup -= ShowPopup;

            CandidateWarehousePage.onShowPopup -= ShowPopup;

            CandidateDetailPage.onShowPopup -= ShowPopup;


            CandidateDetailProcessInterviewPage.onShowPopup -= ShowPopup;
            CandidateDetailGetJobPage.onShowPopup -= ShowPopup;
            CandidateDetailFailJobPage.onShowPopup -= ShowPopup;
            CandidateDetailCancelJobPage.onShowPopup -= ShowPopup;
            CandidateDetailContractJobPage.onShowPopup -= ShowPopup;
            ucXemChiTietLichLamViec.onShowPopup -= ShowPopup;
            //PositionChart.onShowPopup += ShowPopup;

            //ChooseTransportProcess.hidePopup1 += ShowPopup;
        }

        private void gridPopup_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            HidePopup();
        }

        private void gridPopup_PreviewMouseUp(object sender, MouseButtonEventArgs e)
        {
            e.Handled = false;
        }

        private void BackToQLC_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           frmMain.Show();
           this.Close();
        }
    }
}
