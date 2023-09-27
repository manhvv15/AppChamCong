using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ChamCong365.Login;
using ChamCong365.NhanVien;
using ChamCong365.NhanVien.ChamCongBangQRRR.Function;
using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using ChamCong365.Salarysettings;
using ChamCong365.SalarySettings;
using ChamCong365.SalarySettings.DiMuonVeSom;
using ChamCong365.TimeKeeping;
using Newtonsoft.Json;
using RestSharp;
using Brush = System.Windows.Media.Brush;

namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       List<Saff> saffs = new List<Saff>();
        BrushConverter bcMain = new BrushConverter();
        private frmDSDiMuonVeSom frm;
        public string Nam = "";
        public string Thang = "";
        public string PhongBan = "";
        public string NhanVien = "";
        public int Back;
        public int IdAcount;
        private string Token;
        private string Pass;
        private ucChooseLoginOptions frmLogin;
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVienThuocCongTy = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBanThuocCongTy = new List<OOP.clsPhongBanThuocCongTy.Item>();
        public MainWindow(int Id, string NameAcount, string tk, ucChooseLoginOptions ucLog)
        {
            InitializeComponent();
            frmLogin = ucLog;
            IdAcount = Id;
            Token = tk;
            GetAvatarEp();
            txbNameAccount.Text = NameAcount;
            ucBodyHome ucbodyhome = new ucBodyHome(this);
            dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            dopBody.Children.Add(Content as UIElement);
            txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text;

            ucDanhSachChucNangChamCong uc = new ucDanhSachChucNangChamCong(this);
            Back = 1;
            ucbodyhome.grLoadFunction.Children.Clear();
            object Content1 = uc.Content;
            uc.Content = null;
            ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
            ucbodyhome.txbChamCong.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
            

        }
        public MainWindow(OOP.Login.LoginCom.Data data, ucChooseLoginOptions ucLog)
        {
            InitializeComponent();
            Pass = data.data.user_info.com_pass;
            frmLogin = ucLog;
            IdAcount = data.data.user_info.com_id;
            Token = data.access_token;
            txbNameAccount.Text = data.data.user_info.com_name;

            //GetAvatarCom();
            ucBodyHome ucbodyhome = new ucBodyHome(this);
            dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            dopBody.Children.Add(Content as UIElement);
            txbLoadChamCong.Text = ucbodyhome.txbChamCong.Text;

            ucDanhSachChucNangChamCong uc = new ucDanhSachChucNangChamCong(this);
            Back = 1;
            ucbodyhome.grLoadFunction.Children.Clear();
            object Content1 = uc.Content;
            uc.Content = null;
            ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
            ucbodyhome.txbChamCong.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
            LoadDLDanhSachNVThuocCongTy();
            LoadDLPhongBanThuocCongTy();
            
        }

        private void LoadDLPhongBanThuocCongTy()
        {
            
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3000/api/qlc/department/list")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    //request.AddHeader("Authorization", Properties.Settings.Default.Token);
                    request.AddParameter("com_id", IdAcount);
                    RestResponse resAlbum = restclient.Execute(request);
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

        private void LoadDLDanhSachNVThuocCongTy()
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

                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.clsNhanVienThuocCongTy.Root NVthuocCty = JsonConvert.DeserializeObject<OOP.clsNhanVienThuocCongTy.Root>(b);
                    if (NVthuocCty != null)
                    {
                        lstNhanVienThuocCongTy = NVthuocCty.data.listUser;
                        OOP.clsNhanVienThuocCongTy.ListUser user = new OOP.clsNhanVienThuocCongTy.ListUser();
                        user.idQLC = 0;
                        user.userName = "Tất cả nhân viên";
                        lstNhanVienThuocCongTy.Insert(0, user);
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
            //MessageBox.Show(response.Content);
            OOP.Login.InfoCom.Root receivedInfo = JsonConvert.DeserializeObject<OOP.Login.InfoCom.Root>(response.Content);
            if (receivedInfo.data != null)
            {
                txbNameAccount.Text = receivedInfo.data.data.userName;
                //Uri DuongDan = new Uri(receivedInfo.data.data.avatarUser);
                //BitmapImage bm = new BitmapImage(DuongDan);
                //Avatar.ImageSource = bm;
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
                //https://chamcong.24hpay.vn/upload/employee/ep111788/ep111788/app1690735610_e111788.jpg
                //using (WebClient httpClient = new WebClient())
                //{
                //    httpClient.QueryString.Clear();
                //    httpClient.QueryString.Add("ma_xt", IdAcount.ToString());
                //    httpClient.Headers.Add("Authorization", Token);
                //    var data3 = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/employee/info");
                //    OOP.Login.InfoEp.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.Login.InfoEp.Root>(UnicodeEncoding.UTF8.GetString(data3));
                //    if (receiveInfoAPI != null)
                //    {
                //        Uri DuongDan = new Uri(receiveInfoAPI.data.data.avatarUser);
                //        BitmapImage bm = new BitmapImage(DuongDan);
                //        Avatar.ImageSource = bm;
                //    }
                //}

                //using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3000/api/qlc/employee/info")))
                //{
                //    RestRequest request = new RestRequest();
                //    request.Method = Method.Post;
                //    request.AlwaysMultipartFormData = true;
                //    request.AddHeader("Authorization", Token);
                //    request.AddParameter("ma_xt", IdAcount);
                //    RestResponse resAlbum = restclient.Execute(request);
                //    var b = resAlbum.Content;
                //    OOP.Login.InfoEp.Root receivedInfo = JsonConvert.DeserializeObject<OOP.Login.InfoEp.Root>(b);
                //    if (receivedInfo != null)
                //    {
                //        Uri DuongDan = new Uri(receivedInfo.data.data.avatarUser);
                //        BitmapImage bm = new BitmapImage(DuongDan);
                //        Avatar.ImageSource = bm;
                //    }
                //}
            }
            catch
            {

            }
        }

        private void bodBackto_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (Back == 1)
            {
                ucBodyHome ucbodyhome = new ucBodyHome(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);
                txbLoadChamCong.Text = "";
                i = 0;
                LableFunction.Visibility = Visibility.Collapsed;

                ucDanhSachChucNangChamCong uc = new ucDanhSachChucNangChamCong(this);
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
                ucbodyhome.txbSalarySettings.Foreground = (Brush)bcMain.ConvertFrom("#4C5BD4");
                scrollMain.HorizontalScrollBarVisibility = System.Windows.Controls.ScrollBarVisibility.Disabled;
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
            //ucCaiDatLuongCoBan ucb = new ucCaiDatLuongCoBan(this);
            if (k == 0)
            {
                if (i == 0)
                {
                    if (MainWindows.Width <= 1024)
                    {

                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;
                        //ucb.bodChonNhanVien.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;

                    }

                    else
                    {

                        HearderColesspa.Visibility = Visibility.Collapsed;
                        HearderVisibility.Visibility = Visibility.Visible;
                        MenuCollapsed.Visibility = Visibility.Collapsed;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                        //ucb.bodChonNhanVien.Visibility = Visibility.Visible;

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
                        //ucb.bodChonNhanVien.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;

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
                        //ucb.bodChonNhanVien.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                    }
                    else if (MainWindows.Width <= 1024)
                    {
                        HearderColesspa.Visibility = Visibility.Visible;
                        HearderVisibility.Visibility = Visibility.Collapsed;
                        //ucb.bodChonNhanVien.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;

                    }
                    //else if (MainWindows.Width <= 1064)
                    //{

                    //    j = 4;
                    //    frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                    //    dopBody.Children.Clear();
                    //    object Content = frm.Content;
                    //    frm.Content = null;
                    //    dopBody.Children.Add(Content as UIElement);
                    //    HearderColesspa.Visibility = Visibility.Visible;
                    //    HearderVisibility.Visibility = Visibility.Collapsed;
                    //    ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                    //    ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;


                    //}
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
                        //ucb.bodChonNhanVien.Visibility = Visibility.Visible;
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;


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
                        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                        //ucb.bodChonNhanVien.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                HearderColesspa.Visibility = Visibility.Collapsed;
                HearderVisibility.Visibility = Visibility.Visible;
                MenuCollapsed.Visibility = Visibility.Collapsed;
                //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
                //ucb.bodChonNhanVien.Visibility = Visibility.Visible;
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
            ////MainWindow Main = new MainWindow(IdAcount, txbNameAccount.Text, Token);
            //if (this.WindowState == WindowState.Normal)
            //{
            //    if (k == 0)
            //    {
            //        this.WindowState = WindowState.Maximized;
            //        k = 1;
            //        if (i == 2)
            //        {
            //            ucCaiDatLuongCoBan ucb = new ucCaiDatLuongCoBan(this);
            //            if (this.WindowState == WindowState.Maximized)
            //            {

            //                j = 2;
            //                frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
            //                dopBody.Children.Clear();
            //                object Content = frm.Content;
            //                frm.Content = null;
            //                dopBody.Children.Add(Content as UIElement);
            //                HearderColesspa.Visibility = Visibility.Visible;
            //                HearderVisibility.Visibility = Visibility.Collapsed;
            //                ucb.bodChonNhanVien.Visibility = Visibility.Visible;
            //                //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
            //            }


            //        }
            //        else if (i == 3)
            //        {
            //            ucCaiDatLuongCoBan ucb = new ucCaiDatLuongCoBan(this);
            //            if (this.WindowState == WindowState.Maximized)
            //            {
            //                j = 5;
            //                frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
            //                dopBody.Children.Clear();
            //                object Content = frm.Content;
            //                frm.Content = null;
            //                dopBody.Children.Add(Content as UIElement);
            //                HearderColesspa.Visibility = Visibility.Visible;
            //                HearderVisibility.Visibility = Visibility.Collapsed;
            //                ucb.bodChonNhanVien.Visibility = Visibility.Visible;
            //                //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
            //            }
            //        }
            //    }

                
            //}
            //else if (this.WindowState == WindowState.Maximized)
            //{
            //    if (k == 1)
            //    {
            //        this.WindowState = WindowState.Normal;
            //        k = 0;
            //        //if (i == 2 || i == 3)
            //        //{
            //        //    ucBasicSalary ucb = new ucBasicSalary(this);
            //        //    if (MainWindows.Width <= 1008)
            //        //    {
            //        //        j = 1;
            //        //        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
            //        //        dopBody.Children.Clear();
            //        //        object Content = frm.Content;
            //        //        frm.Content = null;
            //        //        dopBody.Children.Add(Content as UIElement);
            //        //        HearderColesspa.Visibility = Visibility.Visible;
            //        //        HearderVisibility.Visibility = Visibility.Collapsed;
            //        //        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
            //        //        ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;

            //        //    }
            //        //    else if (MainWindows.Width <= 1024)
            //        //    {
            //        //        j = 2;
            //        //        HearderColesspa.Visibility = Visibility.Visible;
            //        //        HearderVisibility.Visibility = Visibility.Collapsed;
            //        //        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
            //        //        ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;


            //        //    }
            //        //    else
            //        //    {
            //        //        j = 2;
            //        //        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
            //        //        dopBody.Children.Clear();
            //        //        object Content = frm.Content;
            //        //        frm.Content = null;
            //        //        dopBody.Children.Add(Content as UIElement);

            //        //    }
            //        //}

            //        //i = 2;

            //    }
            //}
            
        }

        

        private void MainWindows_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            grShowPopup.Children.Add(new Popup.PopUpHoiTruocKhiDangXuat(this, frmLogin));
        }

        private void borThongTin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThongTinChiTiet.Visibility == Visibility.Collapsed)
            {
                borThongTinChiTiet.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borThongTinChiTiet.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
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
            //this.WindowState = WindowState.Maximized;
            btnMaximize.Visibility = Visibility.Collapsed;
            btnNomal.Visibility = Visibility.Visible;
            var workingArea = System.Windows.SystemParameters.WorkArea;
            this.WindowState = WindowState.Normal;
            Left = workingArea.TopLeft.X;
            Top = workingArea.TopLeft.Y;
            Width = workingArea.Width;
            Height = workingArea.Height;
            this.ResizeMode = ResizeMode.NoResize;
            //k = 1;
            //if (i == 2)
            //{
            //    ucCaiDatLuongCoBan ucb = new ucCaiDatLuongCoBan(this);
            //    if (this.WindowState == WindowState.Maximized)
            //    {

            //        j = 2;
            //        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
            //        dopBody.Children.Clear();
            //        object Content = frm.Content;
            //        frm.Content = null;
            //        dopBody.Children.Add(Content as UIElement);
            //        HearderColesspa.Visibility = Visibility.Visible;
            //        HearderVisibility.Visibility = Visibility.Collapsed;
            //        //ucb.bodChonNhanVien.Visibility = Visibility.Visible;
            //        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
            //    }


            //}
            //else if (i == 3)
            //{
            //    ucCaiDatLuongCoBan ucb = new ucCaiDatLuongCoBan(this);
            //    if (this.WindowState == WindowState.Maximized)
            //    {
            //        j = 5;
            //        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
            //        dopBody.Children.Clear();
            //        object Content = frm.Content;
            //        frm.Content = null;
            //        dopBody.Children.Add(Content as UIElement);
            //        HearderColesspa.Visibility = Visibility.Visible;
            //        HearderVisibility.Visibility = Visibility.Collapsed;
            //        //ucb.bodChonNhanVien.Visibility = Visibility.Visible;
            //        //ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;
            //    }
            //}
        }

        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            grShowPopup.Children.Add(new Popup.PopUpHoiTruocKhiDangXuat(this, frmLogin));
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
                //if (i == 2 || i == 3)
                //{
                //    ucBasicSalary ucb = new ucBasicSalary(this);
                //    if (MainWindows.Width <= 1008)
                //    {
                //        j = 1;
                //        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                //        dopBody.Children.Clear();
                //        object Content = frm.Content;
                //        frm.Content = null;
                //        dopBody.Children.Add(Content as UIElement);
                //        HearderColesspa.Visibility = Visibility.Visible;
                //        HearderVisibility.Visibility = Visibility.Collapsed;
                //        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                //        ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;

                //    }
                //    else if (MainWindows.Width <= 1024)
                //    {
                //        j = 2;
                //        HearderColesspa.Visibility = Visibility.Visible;
                //        HearderVisibility.Visibility = Visibility.Collapsed;
                //        ucb.bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                //        ucb.bodSelectRoomVisible.Visibility = Visibility.Collapsed;


                //    }
                //    else
                //    {
                //        j = 2;
                //        frmCaiDatThietLapPhatDiMuonVeSom frm = new frmCaiDatThietLapPhatDiMuonVeSom(this);
                //        dopBody.Children.Clear();
                //        object Content = frm.Content;
                //        frm.Content = null;
                //        dopBody.Children.Add(Content as UIElement);

                //    }
                //}

                //i = 2;

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
    }
}
