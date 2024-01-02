using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan;
using QuanLyChung365TruocDangNhap.ChamCong365.Entities.Company;
using QuanLyChung365TruocDangNhap.ChamCong365.Entities.Staff;
using QuanLyChung365TruocDangNhap.ChotDonTu;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages.PageLichLamViecCaLamViec;
using QuanLyChung365TruocDangNhap.Hr.Entities.LoginEntity;
using QuanLyChung365TruocDangNhap.Hr.View;
using QuanLyChung365TruocDangNhap.LuanChuyenCongTy;
using QuanLyChung365TruocDangNhap.PageDangKyVip;
using QuanLyChung365TruocDangNhap.PhanQuyenHeThong;
using QuanLyChung365TruocDangNhap.RecommendSetting;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.CoCau_ViTri_ToChuc;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Them_Xoa_NhanVien;
using RestSharp;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using static QuanLyChung365TruocDangNhap.ChamCong365.Entities.Company.API_Vip_Company;
using System.Net.Http;
using QuanLyChung365TruocDangNhap.TachDongChamCongNS;
using System.Collections.Generic;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.APIs;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien;
using QuanLyChung365TruocDangNhap.Popup.OOP.Login;

namespace QuanLyChung365TruocDangNhap
{
    /// <summary>
    /// Interaction logic for frmMain.xaml
    /// </summary>
    public partial class frmMain : Window
    {
        BrushConverter br = new BrushConverter();
        public string Tokens;
        public string IdAcount = "";
        public string IdCom = "";
        private string IdComRegister = "";
        public int Type;
        public frmMain()
        {
            InitializeComponent();

            btnMaximize.Visibility = Visibility.Collapsed;
            btnNomal.Visibility = Visibility.Visible;
            var workingArea = System.Windows.SystemParameters.WorkArea;
            this.WindowState = WindowState.Normal;
            Left = workingArea.TopLeft.X;
            Top = workingArea.TopLeft.Y;
            Width = workingArea.Width;
            Height = workingArea.Height;
            this.ResizeMode = ResizeMode.NoResize;
            spnFull.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            spnNS.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLBH.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLCV.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLNB.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            //LoadReCaptcha();
            LoadAppAll();
        }

        #region ListAll App
        public class AppAll
        {
            public int IdApp { get; set; }
            public int IdGround { get; set; }
            public string NameApp { get; set; }
            public string SourceLogoApp { get; set; }
        }
        List<AppAll> lstAppAll = new List<AppAll>();
        List<AppAll> lstAppAllSearch = new List<AppAll>();
        List<AppAll> lstAppFilter = new List<AppAll>();
        public void LoadAppAll()
        {

            lstAppAll.Clear();
            lstAppAll.Add(new AppAll() { IdApp = 1, IdGround = 1, NameApp = "Chấm công cũ", SourceLogoApp = "/Resource/image/Group 632586.png" });
            lstAppAll.Add(new AppAll() { IdApp = 2, IdGround = 1, NameApp = "Chấm công 365", SourceLogoApp = "/Resource/image/Group 632586.png" });
            lstAppAll.Add(new AppAll() { IdApp = 3, IdGround = 4, NameApp = "Chat365", SourceLogoApp = "/Resource/image/qlc_chat.png" });
            lstAppAll.Add(new AppAll() { IdApp = 4, IdGround = 1, NameApp = "Tính lương", SourceLogoApp = "/Resource/image/Group 632589.png" });
            lstAppAll.Add(new AppAll() { IdApp = 5, IdGround = 1, NameApp = "Quản trị nhân sự", SourceLogoApp = "/Resource/image/Group 632588.png" });
            lstAppAll.Add(new AppAll() { IdApp = 6, IdGround = 2, NameApp = "Phần mềm giao việc", SourceLogoApp = "/Resource/image/Group 632588.png" });
            lstAppAll.Add(new AppAll() { IdApp = 7, IdGround = 3, NameApp = "Văn thư lưu trữ", SourceLogoApp = "/Resource/image/Group 632590.png" });
            lstAppAll.Add(new AppAll() { IdApp = 8, IdGround = 4, NameApp = "Phần mềm CRM", SourceLogoApp = "/Resource/image/Frame 1000008774.png" });
            lstAppAll.Add(new AppAll() { IdApp = 9, IdGround = 3, NameApp = "Quản lý tài sản", SourceLogoApp = "/Resource/image/Group 632591.png" });
            lstAppAll.Add(new AppAll() { IdApp = 10, IdGround = 4, NameApp = "LiveChat", SourceLogoApp = "/Resource/image/qlc_dms.png" });
            lstAppAll.Add(new AppAll() { IdApp = 11, IdGround = 3, NameApp = "Chuyển văn bản thành giọng nói", SourceLogoApp = "/Resource/image/qlc_vb_gn.png" });
            lsvListApp.ItemsSource = lstAppAll;
        }
        private void btn_SearchAccount_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstAppAllSearch = new List<AppAll>();
            foreach (var str in lstAppAll)
            {
                if (str.NameApp != null)
                {
                    if (str.NameApp.ToLower().RemoveUnicode().Contains(tb_SearchNameAccount.Text.ToLower().RemoveUnicode()))
                    {
                        lstAppAllSearch.Add(str);
                    }
                }
            }
            lsvListApp.ItemsSource = lstAppAllSearch;
            if (tb_SearchNameAccount.Text == "")
            {
                lsvListApp.ItemsSource = lstAppAll;
            }
        }

        private void tb_SearchNameAccount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (tb_SearchNameAccount.Text == "")
            {
                lsvListApp.ItemsSource = lstAppAll;
            }
        }

        public string BackToBack;
        private void btnChuyenUngDung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AppAll lstapp = (sender as Border).DataContext as AppAll;
            if (lstapp != null)
            {
                if (lstapp.IdApp == 1)
                {
                    if (b == "")
                    {
                        pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

                    }
                    else
                    {
                        API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                        if (api != null)
                        {
                            ChamCong365.MainWindow home = new ChamCong365.MainWindow(api);
                            var workingArea = System.Windows.SystemParameters.WorkArea;
                            home.Width = workingArea.Right - 100;
                            home.Height = workingArea.Bottom - 80;
                            home.Show();
                            this.Close();
                        }
                    }
                }
                else if (lstapp.IdApp == 2)
                {
                    if (b == "")
                    {
                        pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

                    }
                    else
                    {
                        Popup.OOP.Login.clsLogin.Data result = JsonConvert.DeserializeObject<Popup.OOP.Login.clsLogin.Data>(b);
                        if (result != null)
                        {
                           
                            if (Type == 1)
                            {
                                BackToBack = "ChamCong";
                                this.Hide();
                                MainWindow main = new MainWindow(result.data, this);
                                main.Show();
                            }
                            else if (Type == 2 )
                            {
                                this.Hide();
                                MainChamCong main = new MainChamCong(result.data, this);
                                main.Show();
                            }
                            this.Hide();
                        }
                    }
                }
                else if (lstapp.IdApp == 4)
                {
                    Process.Start("https://hungha365.com/tinh-luong/tinh-luong/cong-ty/trang-chu");
                }
                else if (lstapp.IdApp == 5)
                {
                    if (b == "")
                    {
                        pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

                    }
                    else
                    {
                        LoginEmployeeEntity result = JsonConvert.DeserializeObject<LoginEmployeeEntity>(b);
                        if (result != null)
                        {

                            HomeView homeView = new HomeView(result,this);
                            homeView.Show();
                            this.Hide();
                        }
                    }
                }
                else if (lstapp.IdApp == 6)
                {
                    Process.Start("https://hungha365.com/giao-viec/quan-ly-chung-cong-ty");
                }
                else if (lstapp.IdApp == 7)
                {
                    Process.Start("https://hungha365.com/van-thu-luu-tru");
                }
                else if (lstapp.IdApp == 8)
                {
                    Process.Start("https://hungha365.com/crm/home");
                }
                else if (lstapp.IdApp == 9)
                {
                    Process.Start("https://hungha365.com/quan-ly-tai-san/trang-chu");
                }
                else if (lstapp.IdApp == 10)
                {
                    Process.Start("https://hungha365.com/live-chat-365");
                }
                else if (lstapp.IdApp == 11)
                {
                    Process.Start("https://chuyenvanbanthanhgiongnoi.timviec365.vn/");
                }
            }
        }

        private void spnNS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstAppFilter = new List<AppAll>();
            foreach (var item in lstAppAll)
            {
                if (item.IdGround == 1)
                {
                    lstAppFilter.Add(item);
                }
            }
            lsvListApp.ItemsSource = lstAppFilter;
            spnFull.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnNS.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            spnQLBH.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLCV.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLNB.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
        }
        private void spnFull_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoadAppAll();
            spnFull.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            spnNS.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLBH.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLCV.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLNB.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void spnQLCV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstAppFilter = new List<AppAll>();
            foreach (var item in lstAppAll)
            {
                if (item.IdGround == 2)
                {
                    lstAppFilter.Add(item);
                }
            }
            lsvListApp.ItemsSource = lstAppFilter;
            spnFull.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnNS.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLBH.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLCV.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            spnQLNB.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void spnQLNB_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstAppFilter = new List<AppAll>();
            foreach (var item in lstAppAll)
            {
                if (item.IdGround == 3)
                {
                    lstAppFilter.Add(item);
                }
            }
            lsvListApp.ItemsSource = lstAppFilter;
            spnFull.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnNS.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLBH.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLCV.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLNB.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void spnQLBH_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstAppFilter = new List<AppAll>();
            foreach (var item in lstAppAll)
            {
                if (item.IdGround == 4)
                {
                    lstAppFilter.Add(item);
                }
            }
            lsvListApp.ItemsSource = lstAppFilter;
            spnFull.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnNS.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLBH.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            spnQLCV.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
            spnQLNB.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
        }
        #endregion

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
            Application.Current.Shutdown();

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


        }

        private void pnlTieuDe1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnDisplayFullSizebar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pnlFullSizebar.Visibility = Visibility.Visible;
            pnlCollapseSizebar.Visibility = Visibility.Collapsed;
            btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
            btnCollapseSizebar.Visibility = Visibility.Visible;
        }

        private void btnCollapseSizebar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pnlFullSizebar.Visibility = Visibility.Collapsed;
            pnlCollapseSizebar.Visibility = Visibility.Visible;
            btnDisplayFullSizebar.Visibility = Visibility.Visible;
            btnCollapseSizebar.Visibility = Visibility.Collapsed;
        }



        private void clearPopUp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borThongTinChiTiet.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
        }

        private void btnShowSPN_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borThongTinChiTiet.Visibility = Visibility.Visible;
            popup.Visibility = Visibility.Visible;
        }

        private void btnDangKy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpDangKy(this));
        }

        private void txtSDT_LostFocus(object sender, RoutedEventArgs e)
        {


        }


        private void txtSDT_TextChanged(object sender, TextChangedEventArgs e)
        {


        }

        private void txtTenCongTy_LostFocus(object sender, RoutedEventArgs e)
        {

        }
        private string _Password = "";
        public string Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _ShowPass = false;
        public bool ShowPass
        {
            get { return _ShowPass; }
            set { _ShowPass = value; OnPropertyChanged(); }
        }

        private API_Vip_Company.VipInfo _VipInfo = new VipInfo();
        public API_Vip_Company.VipInfo VipInfo
        {
            get { return _VipInfo; }
            set
            {
                _VipInfo = value; OnPropertyChanged();
                borVipArea.DataContext = value;
            }
        }
        private void InputPassword(object sender, RoutedEventArgs e)
        {
            Password = ((PasswordBox)sender).Password;
        }

        private void ShowPassword(object sender, MouseButtonEventArgs e)
        {
            ShowPass = true;
        }

        private void btnHome_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            pnlMain.Visibility = Visibility.Visible;
            btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
            btnCollapseSizebar.Visibility = Visibility.Visible;
            pnlFullSizebar.Visibility = Visibility.Visible;
            pnlCollapseSizebar.Visibility = Visibility.Collapsed;
            pnlDangKy.Visibility = Visibility.Collapsed;
        }

        private async void btnContinue_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (string.IsNullOrEmpty(txtIDCompany.Text))
            {
                textTB.Visibility = Visibility.Visible;
            }
            else
            {
                if (await GetVipCom(txtIDCompany.Text))
                {
                    pnlDangKy.Visibility = Visibility.Visible;
                    pnlMain.Visibility = Visibility.Collapsed;
                    PopCT.Visibility = Visibility.Collapsed;
                    PopNV.Visibility = Visibility.Visible;
                    PopIdCT.Visibility = Visibility.Collapsed;
                    btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
                    btnCollapseSizebar.Visibility = Visibility.Collapsed;
                    textTB.Visibility = Visibility.Collapsed;
                    IdComRegister = txtIDCompany.Text;
                    GetListOrganize(IdComRegister);
                    GetListPosition(IdComRegister);
                }

                else
                {
                    textTB.Text = "Công ty không tồn tại";
                    textTB.Visibility = Visibility.Visible;
                }

            }

        }

        private void btnDangNhap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textThongBao.Visibility = Visibility.Collapsed;
            pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopupDangNhap(this));

        }
        public void ChangeBorderColor(Border border)
        {
            border.BorderThickness = new Thickness(0, 0, 0, 5);
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));


        }
        public void SetDefaultMenuColor()
        {

        }

        private void TaiKhoan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            SetDefaultMenuColor();
        }

        private void SelectedTypeLogin(object sender, MouseButtonEventArgs e)
        {

        }

        private void txtEmail_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtEmail_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPass_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void txtPass_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void txtPass_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void ckSave_Unchecked(object sender, RoutedEventArgs e)
        {
            QuanLyChung365TruocDangNhap.Properties.Settings.Default.EpEmail = "";
            QuanLyChung365TruocDangNhap.Properties.Settings.Default.EpPass = "";
            QuanLyChung365TruocDangNhap.Properties.Settings.Default.Save();
        }

        private void ForgotPass(object sender, MouseButtonEventArgs e)
        {

        }
        private string b = "";
        public string Pass_Us;
        public string Pass_com;
        //clsLogin.Data Data { get; set; }
        private void btnDangNhapGo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3000/api/qlc/employee/login")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;

                    request.AddParameter("account", tb_TaiKhoanDangNhap.Text);
                    request.AddParameter("password", tb_MatKhauGo.Password);
                    request.AddParameter("type", "1");
                    RestResponse resAlbum = restclient.Execute(request);
                    b = resAlbum.Content;
                    Popup.OOP.Login.clsLogin.Root receivedInfo = JsonConvert.DeserializeObject<Popup.OOP.Login.clsLogin.Root>(b);
                    if (receivedInfo.data != null)
                    {
                        if (receivedInfo.data.data.type == "1")
                        {

                            if (textLoaiTK.Text == "TÀI KHOẢN CÔNG TY")
                            {
                                Type = 1;
                                Pass_com = receivedInfo.data.data.user_info.com_pass;
                                textThongBao.Visibility = Visibility.Collapsed;
                                pnlMain.Visibility = Visibility.Visible;
                                btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
                                btnCollapseSizebar.Visibility = Visibility.Visible;
                                pnlFullSizebar.Visibility = Visibility.Visible;
                                pnlCollapseSizebar.Visibility = Visibility.Collapsed;
                                pnlDangKy.Visibility = Visibility.Collapsed;
                                docListBtn.Visibility = Visibility.Collapsed;
                                btnShowSPN.Visibility = Visibility.Visible;
                                IdAcount = receivedInfo.data.data.user_info.com_id.ToString();
                                IdCom = IdAcount;
                                textUserName.Text = receivedInfo.data.data.user_info.com_name;
                                Tokens = receivedInfo.data.data.access_token.ToString();
                                if (chkLuuTKMK.IsChecked == true)
                                {
                                    Properties.Settings.Default.User = tb_TaiKhoanDangNhap.Text;
                                    Properties.Settings.Default.Pass = tb_MatKhauGo.Password;
                                    Properties.Settings.Default.Check = "1";
                                    Properties.Settings.Default.Save();
                                }
                                else
                                {
                                    Properties.Settings.Default.User = "";
                                    Properties.Settings.Default.Pass = "";
                                    Properties.Settings.Default.Check = "0";
                                    Properties.Settings.Default.Save();
                                }
                                Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                               
                            }
                            else
                            {
                                textThongBao.Visibility = Visibility.Visible;
                            }
                        }
                        else if (receivedInfo.data.data.type == "2")
                        {
                            if (textLoaiTK.Text == "TÀI KHOẢN NHÂN VIÊN")
                            {
                                request = new RestRequest();
                                request.Method = Method.Post;
                                request.AlwaysMultipartFormData = true;
                                
                                request.AddParameter("account", tb_TaiKhoanDangNhap.Text);
                                request.AddParameter("password", tb_MatKhauGo.Password);
                                request.AddParameter("type", "2");
                                resAlbum = restclient.Execute(request);
                                b = resAlbum.Content;
                                Popup.OOP.Login.clsLogin.Root receivedInfo1 = JsonConvert.DeserializeObject<Popup.OOP.Login.clsLogin.Root>(b);

                                Type = 2;
                                Pass_Us = receivedInfo.data.data.user_info.com_pass;
                                textThongBao.Visibility = Visibility.Collapsed;
                                pnlMain.Visibility = Visibility.Visible;
                                btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
                                btnCollapseSizebar.Visibility = Visibility.Visible;
                                pnlFullSizebar.Visibility = Visibility.Visible;
                                pnlCollapseSizebar.Visibility = Visibility.Collapsed;
                                pnlDangKy.Visibility = Visibility.Collapsed;
                                docListBtn.Visibility = Visibility.Collapsed;
                                btnShowSPN.Visibility = Visibility.Visible;

                                btn_CaiDatDeXuat.Visibility = Visibility.Collapsed;
                                btn_GioiHanIp.Visibility = Visibility.Collapsed;
                                btn_LuanChuyenCongTy.Visibility = Visibility.Collapsed;
                                btnPhanQuyen.Visibility = Visibility.Collapsed;
                                btn_LichLamViec.Visibility = Visibility.Collapsed;
                                btn_CaIDatViTriWifi.Visibility = Visibility.Collapsed;
                                btn_CheDoChamCong.Visibility = Visibility.Collapsed;
                                btn_TachDongChamCong.Visibility = Visibility.Collapsed;
                                btnChotDon.Visibility = Visibility.Collapsed;
                                btn_HeSoLamTron.Visibility = Visibility.Collapsed;
                                btn_NgayChotTuDong.Visibility = Visibility.Collapsed;
                                bod_CheDoPheDuyetChamCong.Visibility = Visibility.Collapsed;
                                btn_IconPheDuyetChamCong.Visibility = Visibility.Collapsed;
                                btn_IconViTriVaWifi.Visibility = Visibility.Collapsed;
                                btn_IconLichCaLamViec.Visibility = Visibility.Collapsed;
                                btn_IconPhanQuyenHeThong.Visibility = Visibility.Collapsed;
                                btn_IconLuanChuyenCongTy.Visibility = Visibility.Collapsed;
                                btn_IconGIoiHanIPPhanMem.Visibility = Visibility.Collapsed;
                                btn_IconCaiDatDeXuat.Visibility = Visibility.Collapsed;
                                btn_IconTachDongChamCong.Visibility = Visibility.Collapsed;
                                btn_IconChotDonTu.Visibility = Visibility.Collapsed;
                                btn_IconNgayChotTuDong.Visibility = Visibility.Collapsed;
                                btn_IconHeSoLamTron.Visibility = Visibility.Collapsed;


                                IdAcount = receivedInfo.data.data.user_info.com_id.ToString();
                                IdCom = receivedInfo1.data.data.user_info.com_id.ToString();
                                textUserName.Text = receivedInfo.data.data.user_info.com_name;
                                Tokens = receivedInfo.data.data.access_token.ToString();
                                if (receivedInfo.data.data.user_info.com_logo != null)
                                {
                                    var img = new Uri("https://chamcong.24hpay.vn/upload/employee/" + receivedInfo.data.data.user_info.com_logo);
                                    BitmapImage bm = new BitmapImage(img);
                                    ImgAc.ImageSource = bm;
                                }

                                if (chkLuuTKMK.IsChecked == true)
                                {
                                    Properties.Settings.Default.UserEp = tb_TaiKhoanDangNhap.Text;
                                    Properties.Settings.Default.PassEp = tb_MatKhauGo.Password;
                                    Properties.Settings.Default.Check = "1";
                                    Properties.Settings.Default.Save();
                                }
                                else
                                {
                                    Properties.Settings.Default.UserEp = "";
                                    Properties.Settings.Default.PassEp = "";
                                    Properties.Settings.Default.Check = "0";
                                    Properties.Settings.Default.Save();
                                }
                                Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                            }
                            else
                            {
                                textThongBao.Visibility = Visibility.Visible;
                            }

                        }

                    }
                    else
                    {
                        textThongBao.Visibility = Visibility.Visible;
                    }
                }
                GetVipCom(IdAcount);
            }
            catch
            {
                textThongBao.Visibility = Visibility.Visible;
            }
        }

        private void tb_MatKhauGo_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tb_MatKhauGo.Password == "")
            {
                txtMK.Visibility = Visibility.Visible;
            }
            else
            {
                txtMK.Visibility = Visibility.Collapsed;
            }
        }

        private void LogOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pnlShowPopUp.Children.Add(new Popup.QuanLyChungSauDangNhap.PopUpDangXuat(this));
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (pnlDangNhapCTy.Visibility == Visibility.Visible)
            {
                if (e.Key == Key.Enter)
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3000/api/qlc/employee/login")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;

                            request.AddParameter("account", tb_TaiKhoanDangNhap.Text);
                            request.AddParameter("password", tb_MatKhauGo.Password);
                            request.AddParameter("type", "1");
                            RestResponse resAlbum = restclient.Execute(request);
                            b = resAlbum.Content;
                            Popup.OOP.Login.clsLogin.Root receivedInfo = JsonConvert.DeserializeObject<Popup.OOP.Login.clsLogin.Root>(b);
                            if (receivedInfo.data != null)
                            {
                                if (receivedInfo.data.data.type == "1")
                                {
                                    if (textLoaiTK.Text == "TÀI KHOẢN CÔNG TY")
                                    {
                                        Type = 1;
                                        Pass_com = receivedInfo.data.data.user_info.com_pass;
                                        textThongBao.Visibility = Visibility.Collapsed;
                                        pnlMain.Visibility = Visibility.Visible;
                                        btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
                                        btnCollapseSizebar.Visibility = Visibility.Visible;
                                        pnlFullSizebar.Visibility = Visibility.Visible;
                                        pnlCollapseSizebar.Visibility = Visibility.Collapsed;
                                        pnlDangKy.Visibility = Visibility.Collapsed;
                                        docListBtn.Visibility = Visibility.Collapsed;
                                        btnShowSPN.Visibility = Visibility.Visible;
                                        IdAcount = receivedInfo.data.data.user_info.com_id.ToString();
                                        textUserName.Text = receivedInfo.data.data.user_info.com_name;

                                        if (chkLuuTKMK.IsChecked == true)
                                        {
                                            Properties.Settings.Default.User = tb_TaiKhoanDangNhap.Text;
                                            Properties.Settings.Default.Pass = tb_MatKhauGo.Password;
                                            Properties.Settings.Default.Check = "1";
                                            Properties.Settings.Default.Save();
                                        }
                                        else
                                        {
                                            Properties.Settings.Default.User = "";
                                            Properties.Settings.Default.Pass = "";
                                            Properties.Settings.Default.Check = "0";
                                            Properties.Settings.Default.Save();
                                        }
                                        Properties.Settings.Default.Token = receivedInfo.data.data.access_token;


                                    }
                                    else
                                    {
                                        textThongBao.Visibility = Visibility.Visible;
                                    }
                                }
                                else if (receivedInfo.data.data.type == "2")
                                {
                                    if (textLoaiTK.Text == "TÀI KHOẢN NHÂN VIÊN")
                                    {
                                        Type = 2;
                                        Pass_Us = receivedInfo.data.data.user_info.com_pass;
                                        textThongBao.Visibility = Visibility.Collapsed;
                                        pnlMain.Visibility = Visibility.Visible;
                                        btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
                                        btnCollapseSizebar.Visibility = Visibility.Visible;
                                        pnlFullSizebar.Visibility = Visibility.Visible;
                                        pnlCollapseSizebar.Visibility = Visibility.Collapsed;
                                        pnlDangKy.Visibility = Visibility.Collapsed;
                                        docListBtn.Visibility = Visibility.Collapsed;
                                        btnShowSPN.Visibility = Visibility.Visible;

                                        btn_CaiDatDeXuat.Visibility = Visibility.Collapsed;
                                        btn_GioiHanIp.Visibility = Visibility.Collapsed;
                                        btn_LuanChuyenCongTy.Visibility = Visibility.Collapsed;
                                        btnPhanQuyen.Visibility = Visibility.Collapsed;
                                        btn_LichLamViec.Visibility = Visibility.Collapsed;
                                        btn_CaIDatViTriWifi.Visibility = Visibility.Collapsed;
                                        btn_CheDoChamCong.Visibility = Visibility.Collapsed;
                                        btn_TachDongChamCong.Visibility = Visibility.Collapsed;
                                        btnChotDon.Visibility = Visibility.Collapsed;
                                        btn_HeSoLamTron.Visibility = Visibility.Collapsed;
                                        btn_NgayChotTuDong.Visibility = Visibility.Collapsed;

                                        IdAcount = receivedInfo.data.data.user_info.com_id.ToString();
                                        IdCom = receivedInfo.data.data.com_info.com_id.ToString();
                                        textUserName.Text = receivedInfo.data.data.user_info.com_name;
                                        Tokens = receivedInfo.data.data.access_token.ToString();
                                        if (receivedInfo.data.data.user_info.com_logo != null)
                                        {
                                            var img = new Uri("https://chamcong.24hpay.vn/upload/employee/" + receivedInfo.data.data.user_info.com_logo);
                                            BitmapImage bm = new BitmapImage(img);
                                            ImgAc.ImageSource = bm;
                                        }

                                        if (chkLuuTKMK.IsChecked == true)
                                        {
                                            Properties.Settings.Default.UserEp = tb_TaiKhoanDangNhap.Text;
                                            Properties.Settings.Default.PassEp = tb_MatKhauGo.Password;
                                            Properties.Settings.Default.Check = "1";
                                            Properties.Settings.Default.Save();
                                        }
                                        else
                                        {
                                            Properties.Settings.Default.UserEp = "";
                                            Properties.Settings.Default.PassEp = "";
                                            Properties.Settings.Default.Check = "0";
                                            Properties.Settings.Default.Save();
                                        }
                                        Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                                    }
                                    else
                                    {
                                        textThongBao.Visibility = Visibility.Visible;
                                    }

                                }

                            }
                            else
                            {
                                textThongBao.Visibility = Visibility.Visible;
                            }
                        }
                    }
                    catch
                    {
                        textThongBao.Visibility = Visibility.Visible;
                    }
                }
            }
        }

        private void btnDKyEp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textTKDangNhap.Text == "")
            {
                textThongBaoNhapSDT.Visibility = Visibility.Visible;
            }
            else if (textHoTen.Text == "")
            {
                textThongBaoHVT.Visibility = Visibility.Visible;
            }
            else if (textMatKhau.Password == "")
            {
                textThongBaoNhapMK.Visibility = Visibility.Visible;
            }
            else if (textNhapLaiMK.Password == "")
            {
                textThongBaoNhapLaiMK.Visibility = Visibility.Visible;
            }

            else if (textDiaChi.Text == "")
            {
                textThongBaoNhapDiaChi.Visibility = Visibility.Visible;
            }
            else if (cboGioiTinh.SelectedIndex == -1)
            {
                cboGioiTinh.Visibility = Visibility.Visible;
            }
            else if (dtpNgaySinh.Text == "")
            {
                textThongBaoChonNgaySinh.Visibility = Visibility.Visible;
            }
            else if (textMatKhau.Password != textNhapLaiMK.Password)
            {
                textThongBaoNhapLaiMK.Text = "Mật khẩu không khớp";
                textThongBaoNhapLaiMK.Visibility = Visibility.Visible;
            }

            else
            {
                try
                {
                    Register();

                }
                catch
                {

                }
            }
        }

        private void btnDkyCom_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textThongBaoNhapSDTCom.Visibility = Visibility.Collapsed;
            textThongBaoNhapTenCTCom.Visibility = Visibility.Collapsed;
            textThongBaoNhapEmail.Visibility = Visibility.Collapsed;
            textThongBaoNhapLaiMKCTCom.Visibility = Visibility.Collapsed;
            textThongBaoNhapDiaChiCom.Visibility = Visibility.Collapsed;
            textThongBaoNhapLaiMKCTCom.Visibility = Visibility.Collapsed;
            textThongBaoNhapMKCTCom.Visibility = Visibility.Collapsed;

            if (txtSDT.Text == "")
            {
                textThongBaoNhapSDTCom.Visibility = Visibility.Visible;
            }
            else if (textTenCT.Text == "")
            {
                textThongBaoNhapTenCTCom.Visibility = Visibility.Visible;
            }
            else if (textEmail.Text == "")
            {
                textThongBaoNhapEmail.Visibility = Visibility.Visible;
            }
            else if (passboxEmailPassWordNhanVien.Password == "")
            {
                textThongBaoNhapMKCTCom.Visibility = Visibility.Visible;
            }
            else if (passboxEmailPassWordNL.Password == "")
            {
                textThongBaoNhapLaiMKCTCom.Visibility = Visibility.Visible;
            }
            else if (textDC.Text == "")
            {
                textThongBaoNhapDiaChiCom.Visibility = Visibility.Visible;
            }

            else if (passboxEmailPassWordNhanVien.Password != passboxEmailPassWordNL.Password)
            {
                textThongBaoNhapLaiMKCTCom.Text = "Mật khẩu không khớp";
                textThongBaoNhapLaiMKCTCom.Visibility = Visibility.Visible;
            }

            else
            {
                try
                {
                    using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/qlc/company/register")))
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("address", textDC.Text);
                        request.AddParameter("emailContact", textEmail.Text);
                        request.AddParameter("password", passboxEmailPassWordNhanVien.Password);
                        request.AddParameter("phoneTK", txtSDT.Text);
                        request.AddParameter("res_password", passboxEmailPassWordNL.Password);
                        request.AddParameter("userName", textTenCT.Text);
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;

                        if (resAlbum.IsSuccessStatusCode)
                        {
                            pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoDangKyTKThanhCong(txtSDT.Text, txtEmailPassWordNhanVien.Text, this));

                        }
                        else
                        {
                            dynamic message = b;
                            CustomMessageBox.Show(message?.error?.message);
                        }
                    }

                }
                catch
                {
                    CustomMessageBox.Show("Có lỗi khi tạo tài khoản");
                }
            }
        }

        private void btnAppQTNS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                LoginEmployeeEntity result = JsonConvert.DeserializeObject<LoginEmployeeEntity>(b);
                if (result != null)
                {

                    HomeView homeView = new HomeView(result, this);
                    homeView.Show();
                    this.Hide();
                }
            }
        }
        private async Task Login()
        {
            // var httpClient = new HttpClient();

            // var httpRequestMessage = new HttpRequestMessage();
            // httpRequestMessage.Method = HttpMethod.Post;
            // string api = "http://210.245.108.202:3000/api/qlc/employee/login";
            // httpRequestMessage.RequestUri = new Uri(api);

            // var parameters = new List<KeyValuePair<string, string>>();
            // parameters.Add(new KeyValuePair<string, string>("account", txtEmail.Text));

            // string passLogin = "";

            // parameters.Add(new KeyValuePair<string, string>("password", passLogin));
            // parameters.Add(new KeyValuePair<string, string>("type", "2"));

            // Thiết lập Content
            //var content = new FormUrlEncodedContent(parameters);
            // httpRequestMessage.Content = content;

            // Thực hiện Post
            //var response = await httpClient.SendAsync(httpRequestMessage);

            // var responseContent = await response.Content.ReadAsStringAsync();

            // LoginEmployeeEntity result = JsonConvert.DeserializeObject<LoginEmployeeEntity>(responseContent);


            // if (result.data != null && result.data.result)
            // {

            //     HomeView homeView = new HomeView(result);
            //     homeView.Show();
            //     this.Close();
            // }

        }

        private void btnChamCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                if (api != null)
                {
                    ChamCong365.MainWindow home = new ChamCong365.MainWindow(api);
                    //home.WindowState = Login.WindowState;
                    var workingArea = System.Windows.SystemParameters.WorkArea;
                    home.Width = workingArea.Right - 100;
                    home.Height = workingArea.Bottom - 80;
                    home.Show();
                    this.Close();

                }
            }


        }

        private void btn_LuanChuyenCongTy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                List_NhanVien api = JsonConvert.DeserializeObject<List_NhanVien>(b);
                if (api != null)
                {
                    ucLuanChuyenCongTy qlnv = new ucLuanChuyenCongTy(this);
                    stp_ShowPopup.Children.Clear();
                    object Content = qlnv.Content;
                    qlnv.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);


                    UnActiveSideBar();

                    btn_IconLuanChuyenCongTy.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_IconLuanChuyenCongTy.CornerRadius = new CornerRadius(10);
                    btn_LuanChuyenCongTy.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_LuanChuyenCongTy.CornerRadius = new CornerRadius(10);

                    textNameUD.Text = tb_LuanChuyenCongTy.Text;
                }
            }
        }

        private void btn_ThongTinTaiKhoan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                List_NhanVien api = JsonConvert.DeserializeObject<List_NhanVien>(b);
                if (api != null)
                {
                    ThongTinTaiKhoan qlnv = new ThongTinTaiKhoan(this);
                    stp_ShowPopup.Children.Clear();
                    object Content = qlnv.Content;
                    qlnv.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);

                    UnActiveSideBar();
                    btn_ThongTinTaiKhoan.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_ThongTinTaiKhoan.CornerRadius = new CornerRadius(10);
                    btn_IconThongTinTaiKhoan.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_IconThongTinTaiKhoan.CornerRadius = new CornerRadius(10);

                    textNameUD.Text = tb_ThongTinTaiKhoan.Text;
                }
            }
        }

        private void btnThongTinTK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            List_NhanVien api = JsonConvert.DeserializeObject<List_NhanVien>(b);
            if (api != null)
            {
                ThongTinTaiKhoan qlnv = new ThongTinTaiKhoan(this);
                stp_ShowPopup.Children.Clear();
                object Content = qlnv.Content;
                qlnv.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);

                UnActiveSideBar();
                textNameUD.Text = tb_ThongTinTaiKhoan.Text;
            }
        }
        private void bod_GioiHanIpNPhanMem_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                if (api != null)
                {

                    PageDanhSachNhanVien qlnv = new PageDanhSachNhanVien(this);
                    stp_ShowPopup.Children.Clear();
                    object Content = qlnv.Content;
                    qlnv.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);

                    UnActiveSideBar();
                    btn_GioiHanIp.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_GioiHanIp.CornerRadius = new CornerRadius(10);
                    btn_IconGIoiHanIPPhanMem.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_IconGIoiHanIPPhanMem.CornerRadius = new CornerRadius(10);
                    textNameUD.Text = tb_GioiHanIPvaPhanMem.Text;
                }
            }
        }

        private void bod_CoCau_ViTri_ToChuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {

                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                if (api != null)
                {
                    ucCoCauToChuc_ViTri sodo = new ucCoCauToChuc_ViTri(this);
                    stp_ShowPopup.Children.Clear();
                    object Content = sodo.Content;
                    sodo.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);

                    UnActiveSideBar();
                    bod_CoCau_ViTri_ToChuc.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    bod_CoCau_ViTri_ToChuc.CornerRadius = new CornerRadius(10);
                    btn_IconThietLapCongTy.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_IconThietLapCongTy.CornerRadius = new CornerRadius(10);
                    textNameUD.Text = tb_ThietLapCongTy.Text;

                }
            }
        }

        private void bod_QuanLyNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                if (api != null)
                {
                    if (Type == 1)
                    {
                        ucQuanLyNhanVien qlnv = new ucQuanLyNhanVien(this);
                        stp_ShowPopup.Children.Clear();
                        object Content = qlnv.Content;
                        qlnv.Content = null;
                        stp_ShowPopup.Children.Add(Content as UIElement);

                        UnActiveSideBar();


                        bod_QuanLyNhanVien.Background = (Brush)br.ConvertFrom("#4AA7FF");
                        bod_QuanLyNhanVien.CornerRadius = new CornerRadius(10);
                        btn_IconQuanLyNhanVien.Background = (Brush)br.ConvertFrom("#4AA7FF");
                        btn_IconQuanLyNhanVien.CornerRadius = new CornerRadius(10);
                    }
                    else if (Type == 2)
                    {
                        ucNVXemTatCaNhanVien qlnv = new ucNVXemTatCaNhanVien(this);
                        stp_ShowPopup.Children.Clear();
                        object Content = qlnv.Content;
                        qlnv.Content = null;
                        stp_ShowPopup.Children.Add(Content as UIElement);

                        UnActiveSideBar();


                        bod_QuanLyNhanVien.Background = (Brush)br.ConvertFrom("#4AA7FF");
                        bod_QuanLyNhanVien.CornerRadius = new CornerRadius(10);
                        btn_IconQuanLyNhanVien.Background = (Brush)br.ConvertFrom("#4AA7FF");
                        btn_IconQuanLyNhanVien.CornerRadius = new CornerRadius(10);
                    }
                    textNameUD.Text = tb_QuanLyNhanVien.Text;
                }
            }
        }

        private void btn_IconCaiDatDeXuat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                ucRecommended uc = new ucRecommended(this);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
                btn_IconThietLapCongTy.Background = (Brush)br.ConvertFrom("#4c5bd4");
                icon_Fill_ThietLapCongTy.Fill = (Brush)br.ConvertFrom("#FFFFFF");
                textNameUD.Text = tb_Caidatdexuat.Text;
            }
        }

        private void btn_LuanChuyenCongTyIcon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                List_NhanVien api = JsonConvert.DeserializeObject<List_NhanVien>(b);
                if (api != null)
                {
                    ucLuanChuyenCongTy qlnv = new ucLuanChuyenCongTy(this);
                    stp_ShowPopup.Children.Clear();
                    object Content = qlnv.Content;
                    qlnv.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);
                    btn_IconThietLapCongTy.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_ThietLapCongTy.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconQuanLyNhanVien.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_QuanLyNhanVien1.Fill = (Brush)br.ConvertFrom("#FFFFFF");
                    icon_Fill_QuanLyNhanVien2.Fill = (Brush)br.ConvertFrom("#FFFFFF");
                    icon_Fill_QuanLyNhanVien3.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconCaiDatDeXuat.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_CaiDatDeXuat.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconGIoiHanIPPhanMem.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_GioiHanIpPhanMem.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconLuanChuyenCongTy.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    icon_Fill_LuanChuyenCongTy.Fill = (Brush)br.ConvertFrom("#4c5bd4");

                    btn_IconPhanQuyenHeThong.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_PhanQuenHeThong.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconLichCaLamViec.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_LichLamViec.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconViTriVaWifi.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_ViTriVaWifi.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconPheDuyetChamCong.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_PheDuyetChamCong.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconTachDongChamCong.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_TachDongChamCong.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconChotDonTu.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_ChotDonTu.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconNgayChotTuDong.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_NgayChotTuDong.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconHeSoLamTron.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_HeSoLamTron.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconThongTinTaiKhoan.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_ThongTinTaiKhoan.Fill = (Brush)br.ConvertFrom("#FFFFFF");

                    btn_IconBaoLoi.Background = (Brush)br.ConvertFrom("#4c5bd4");
                    icon_Fill_BaoLoi.Fill = (Brush)br.ConvertFrom("#FFFFFF");
                    textNameUD.Text = tb_LuanChuyenCongTy.Text;
                }
            }
        }
        private void btn_CaiDatDeXuat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                CaiDatDeX.DeXuat uc = new CaiDatDeX.DeXuat(this);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);

                UnActiveSideBar();

                btn_IconCaiDatDeXuat.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btn_IconCaiDatDeXuat.CornerRadius = new CornerRadius(10);
                btn_CaiDatDeXuat.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btn_CaiDatDeXuat.CornerRadius = new CornerRadius(10);
                textNameUD.Text = tb_Caidatdexuat.Text;


            }
        }

        private void btb_ChamCongCoBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                ChamCongNew.OOP.Login.LoginCom result = JsonConvert.DeserializeObject<ChamCongNew.OOP.Login.LoginCom>(b);
                if (result != null)
                {

                    ChamCongNew.Login.ucChooseLoginOptions uclogin = new ChamCongNew.Login.ucChooseLoginOptions();
                    uclogin.Show();
                    this.Close();
                }
            }
        }

        private void bod_LichCaLamViec_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                if (api != null)
                {

                    PageDanhSachCaLamViec qlnv = new PageDanhSachCaLamViec(this);
                    stp_ShowPopup.Children.Clear();
                    object Content = qlnv.Content;
                    qlnv.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);

                    UnActiveSideBar();
                    btn_LichLamViec.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_LichLamViec.CornerRadius = new CornerRadius(10);
                    btn_IconLichCaLamViec.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_IconLichCaLamViec.CornerRadius = new CornerRadius(10);
                    textNameUD.Text = tb_LichVaCaLamViec.Text;
                }
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (frmMains.Width <= 1184)
            {
                pnlFullSizebar.Visibility = Visibility.Collapsed;
                pnlCollapseSizebar.Visibility = Visibility.Visible;
                btnDisplayFullSizebar.Visibility = Visibility.Visible;
                btnCollapseSizebar.Visibility = Visibility.Collapsed;
            }
            else
            {
                pnlFullSizebar.Visibility = Visibility.Visible;
                pnlCollapseSizebar.Visibility = Visibility.Collapsed;
                btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
                btnCollapseSizebar.Visibility = Visibility.Visible;
            }

        }

        private void btnDangNhapDK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textThongBao.Visibility = Visibility.Collapsed;
            pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopupDangNhap(this));
        }

        private void btnDangKyDK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpDangKy(this));
        }

        private void btnQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pnlMain.Visibility = Visibility.Visible;
            btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
            btnCollapseSizebar.Visibility = Visibility.Visible;
            pnlFullSizebar.Visibility = Visibility.Visible;
            pnlCollapseSizebar.Visibility = Visibility.Collapsed;
            pnlDangKy.Visibility = Visibility.Collapsed;
        }

        private void btnBackDKNV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pnlMain.Visibility = Visibility.Visible;
            btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
            btnCollapseSizebar.Visibility = Visibility.Visible;
            pnlFullSizebar.Visibility = Visibility.Visible;
            pnlCollapseSizebar.Visibility = Visibility.Collapsed;
            pnlDangKy.Visibility = Visibility.Collapsed;
        }

        private void btnQuayLaiNhapIdCT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pnlMain.Visibility = Visibility.Collapsed;
            btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
            btnCollapseSizebar.Visibility = Visibility.Collapsed;
            pnlFullSizebar.Visibility = Visibility.Collapsed;
            pnlCollapseSizebar.Visibility = Visibility.Collapsed;
            pnlDangKy.Visibility = Visibility.Visible;
            PopIdCT.Visibility = Visibility.Visible;
            PopNV.Visibility = Visibility.Collapsed;
        }

        private void btnBackLogCom_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            pnlMain.Visibility = Visibility.Visible;
            btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
            btnCollapseSizebar.Visibility = Visibility.Visible;
            pnlFullSizebar.Visibility = Visibility.Visible;
            pnlCollapseSizebar.Visibility = Visibility.Collapsed;
            pnlDangKy.Visibility = Visibility.Collapsed;
        }

        private void btnDangKyGo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textLoaiTK.Text == "TÀI KHOẢN CÔNG TY")
            {
                pnlDangKy.Visibility = Visibility.Visible;
                pnlMain.Visibility = Visibility.Collapsed;
                PopCT.Visibility = Visibility.Visible;
                PopNV.Visibility = Visibility.Collapsed;
                PopIdCT.Visibility = Visibility.Collapsed;
                btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
                btnCollapseSizebar.Visibility = Visibility.Collapsed;
                pnlDangNhapCTy.Visibility = Visibility.Collapsed;
            }
            else if (textLoaiTK.Text == "TÀI KHOẢN NHÂN VIÊN")
            {
                pnlDangKy.Visibility = Visibility.Visible;
                pnlMain.Visibility = Visibility.Collapsed;
                PopCT.Visibility = Visibility.Collapsed;
                PopNV.Visibility = Visibility.Collapsed;
                PopIdCT.Visibility = Visibility.Visible;
                btnDisplayFullSizebar.Visibility = Visibility.Collapsed;
                btnCollapseSizebar.Visibility = Visibility.Collapsed;
                pnlDangNhapCTy.Visibility = Visibility.Collapsed;
            }
        }

        private void btn_ThietLap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (stp_ListSoDo.Visibility == Visibility.Collapsed)
            {

                stp_ListSoDo.Visibility = Visibility.Visible;
                iconControlBottom.Visibility = Visibility.Collapsed;
                iconControlTop.Visibility = Visibility.Visible;


            }
            else
            {
                stp_ListSoDo.Visibility = Visibility.Collapsed;
                iconControlBottom.Visibility = Visibility.Visible;
                iconControlTop.Visibility = Visibility.Collapsed;
            }
        }
        private void SoDoToChuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {

                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                if (api != null)
                {
                    ucSoDoToChuc sodo = new ucSoDoToChuc(this);

                    stp_ShowPopup.Children.Clear();
                    object Content = sodo.Content;
                    sodo.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);
                    textNameUD.Text = tb_ThietLapCongTy.Text;

                    UnActiveSideBar();
                    bod_CoCau_ViTri_ToChuc.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    bod_CoCau_ViTri_ToChuc.CornerRadius = new CornerRadius(10);
                    btn_IconThietLapCongTy.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_IconThietLapCongTy.CornerRadius = new CornerRadius(10);
                }
            }
        }

        private void SoDoViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {

                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                if (api != null)
                {
                    ucSoDoViTRi sodo = new ucSoDoViTRi(this);
                    stp_ShowPopup.Children.Clear();
                    object Content = sodo.Content;
                    sodo.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);
                    textNameUD.Text = tb_ThietLapCongTy.Text;

                    UnActiveSideBar();
                    bod_CoCau_ViTri_ToChuc.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    bod_CoCau_ViTri_ToChuc.CornerRadius = new CornerRadius(10);
                    btn_IconThietLapCongTy.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_IconThietLapCongTy.CornerRadius = new CornerRadius(10);
                }
            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://hungha365.com/tinh-luong/tinh-luong/cong-ty/trang-chu");
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://hungha365.com/giao-viec/quan-ly-chung-cong-ty");
        }

        private void Border_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://hungha365.com/van-thu-luu-tru");
        }

        private void Border_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://hungha365.com/crm/home");
        }

        private void Border_MouseLeftButtonDown_4(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://hungha365.com/quan-ly-tai-san/trang-chu");
        }

        private void Border_MouseLeftButtonDown_5(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://hungha365.com/live-chat-365");
        }

        private void Border_MouseLeftButtonDown_6(object sender, MouseButtonEventArgs e)
        {
            Process.Start("https://chuyenvanbanthanhgiongnoi.timviec365.vn/");
        }

        private void Border_MouseLeftButtonDown_7(object sender, MouseButtonEventArgs e)
        {
            btXemThem.Visibility = btXemThem.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            StMoRong.Visibility = StMoRong.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            stXemThemDanhMuc.Visibility = stXemThemDanhMuc.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
        }

        private void TextBlock_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            btXemThem.Visibility = Visibility.Collapsed;
            StMoRong.Visibility = Visibility.Visible;
            ScrollMain.ScrollToVerticalOffset(2400);
        }

        private void TrangChuQuanLyChung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            stp_ShowPopup.Children.Clear();
            stpListAppArea.Visibility = Visibility.Visible;
        }


        private void btnPhanQuyen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                ucPhanQuyenHeThong uc = new ucPhanQuyenHeThong(this);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);

                UnActiveSideBar();

                btn_IconPhanQuyenHeThong.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btn_IconPhanQuyenHeThong.CornerRadius = new CornerRadius(10);
                btnPhanQuyen.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btnPhanQuyen.CornerRadius = new CornerRadius(10);
                textNameUD.Text = tb_PhanQuyenHeThong.Text;
            }
        }

        private void btnDanhGia_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                ucDanhGia_BaoLoi uc = new ucDanhGia_BaoLoi(this);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
                textNameUD.Text = "Đánh giá";

            }
        }

        int DanhGia;
        private void btnBaoLoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                DanhGia = 1;
                ucDanhGia_BaoLoi uc = new ucDanhGia_BaoLoi(this, DanhGia);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
                textNameUD.Text = tb_BaoLoi.Text;
            }
        }

        private void btnChotDon_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                ucChotDonTu uc = new ucChotDonTu(this);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);

                UnActiveSideBar();
                btnChotDon.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btnChotDon.CornerRadius = new CornerRadius(10);
                btn_IconChotDonTu.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btn_IconChotDonTu.CornerRadius = new CornerRadius(10);
                textNameUD.Text = tb_ChotDonTu.Text;

            }
        }

        private void bod_CheDoPheDuyetChamCong_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {

                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                if (api != null)
                {
                    ucLuaChonDanhSachNhanVien duyetChamCong = new ucLuaChonDanhSachNhanVien(this);
                    stp_ShowPopup.Children.Clear();
                    object Content = duyetChamCong.Content;
                    duyetChamCong.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);
                    textNameUD.Text = tb_CheDoPheDuyetChamCong.Text;

                    UnActiveSideBar();
                    bod_CheDoPheDuyetChamCong.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    bod_CheDoPheDuyetChamCong.CornerRadius = new CornerRadius(10);
                    btn_IconPheDuyetChamCong.Background = (Brush)br.ConvertFrom("#4AA7FF");
                    btn_IconPheDuyetChamCong.CornerRadius = new CornerRadius(10);
                }
            }
        }
        private void btn_BaoLoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                DanhGia = 1;
                ucDanhGia_BaoLoi uc = new ucDanhGia_BaoLoi(this, DanhGia);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);

                UnActiveSideBar();
                btn_BaoLoi.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btn_BaoLoi.CornerRadius = new CornerRadius(10);
                btn_IconBaoLoi.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btn_IconBaoLoi.CornerRadius = new CornerRadius(10);
                textNameUD.Text = tb_BaoLoi.Text;
            }

        }
        public async Task<bool> GetVipCom(string com_id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/company/checkVipCom");
                var content = new StringContent("{\"com_id\":" + com_id.ToString() + "}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_Vip_Company.Root result = JsonConvert.DeserializeObject<API_Vip_Company.Root>(responseContent);
                    VipInfo = result.data.data;
                    return true;
                }
            }
            catch
            {

            }

            return false;
        }

        private void NavigateToVipRegister(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {

                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {
                API_Login_Staff api = JsonConvert.DeserializeObject<API_Login_Staff>(b);
                if (api != null)
                {
                    DangKyVipPage duyetChamCong = new DangKyVipPage();
                    stp_ShowPopup.Children.Clear();
                    object Content = duyetChamCong.Content;
                    duyetChamCong.Content = null;
                    stp_ShowPopup.Children.Add(Content as UIElement);
                    textNameUD.Text = tb_CheDoPheDuyetChamCong.Text;
                }
            }
        }

        private void NavigateToWebPageNews(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start("https://hungha365.com/tin-tuc");
        }

        private void UnActiveSideBar()
        {
            try
            {

                stpListAppArea.Visibility = Visibility.Collapsed;
                for (int i = 0; i <= pnlFullSizebar.Children.Count; i++)
                {
                    if (i == 0) continue;
                    (pnlFullSizebar.Children[i] as Border).Background = (Brush)br.ConvertFrom("#4c5bd4");

                    (pnlCollapseSizebar.Children[i] as Border).Background = (Brush)br.ConvertFrom("#4c5bd4");

                }
            }
            catch { }
        }

        private void btn_TachDongChamCong_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (b == "")
            {
                pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoCanDangNhap(this));

            }
            else
            {

                ucTachDongChamCongNS uc = new ucTachDongChamCongNS();
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);

                UnActiveSideBar();
                btn_TachDongChamCong.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btn_TachDongChamCong.CornerRadius = new CornerRadius(10);
                btn_IconTachDongChamCong.Background = (Brush)br.ConvertFrom("#4AA7FF");
                btn_IconTachDongChamCong.CornerRadius = new CornerRadius(10);
                textNameUD.Text = tb_TachDongChamCongNS.Text;
            }
        }

        #region CallAPI_ToChuc_ViTri
        private async void GetListOrganize(string com_id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.listAll_organize);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);
                    cboToChuc.ItemsSource = result.data.data;
                }
            }
            catch
            {

            }
        }
        private async void GetListPosition(string com_id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.list_position);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListPositionEntities.Root result = JsonConvert.DeserializeObject<ListPositionEntities.Root>(responseContent);
                    cboChucVu.ItemsSource = result.data.data;
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show(ex.Message);
            }
        }
        private async void Register()
        {
            try
            {
                if (textTKDangNhap.Text == "")
                {
                    textThongBaoNhapSDT.Visibility = Visibility.Visible;
                }
                else if (textHoTen.Text == "")
                {
                    textThongBaoHVT.Visibility = Visibility.Visible;
                }
                else if (textMatKhau.Password == "")
                {
                    textThongBaoNhapMK.Visibility = Visibility.Visible;
                }
                else if (textNhapLaiMK.Password == "")
                {
                    textThongBaoNhapLaiMK.Visibility = Visibility.Visible;
                }

                else if (textDiaChi.Text == "")
                {
                    textThongBaoNhapDiaChi.Visibility = Visibility.Visible;
                }
                else if (cboGioiTinh.SelectedIndex == -1)
                {
                    cboGioiTinh.Visibility = Visibility.Visible;
                }
                else if (dtpNgaySinh.Text == "")
                {
                    textThongBaoChonNgaySinh.Visibility = Visibility.Visible;
                }
                else if (textMatKhau.Password != textNhapLaiMK.Password)
                {
                    textThongBaoNhapLaiMK.Text = "Mật khẩu không khớp";
                    textThongBaoNhapLaiMK.Visibility = Visibility.Visible;
                }
                else
                {
                    int organizeDetailId = (cboToChuc.SelectedItem as ListOrganizeEntities.OrganizeData).id.Value;

                    var resgisterBodyObject = new
                    {
                        phoneTK = textTKDangNhap.Text,
                        userName = textHoTen.Text,
                        password = textMatKhau.Password,
                        res_password = textNhapLaiMK.Password,
                        address = textDiaChi.Text,
                        birthday = dtpNgaySinh.SelectedDate?.ToString("yyyy-MM-dd"),
                        gender = cboGioiTinh.SelectedIndex,
                        education = cboHocVan.SelectedIndex,
                        married = cboTinhTrangHonNhan.SelectedIndex,
                        experience = cboKinhNghiemLv.SelectedIndex,
                        com_id = IdComRegister,
                        position_id = cboChucVu.SelectedValue,
                        listOrganizeDetailId = cboToChuc.SelectedValue,
                        organizeDetailId = organizeDetailId

                    };

                    string json = JsonConvert.SerializeObject(resgisterBodyObject);
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/employee/register");
                    var content = new StringContent(json, null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    pnlShowPopUp.Children.Add(new Popup.TruocDangNhap.PopUpThongBaoDangKyTKThanhCong(textTKDangNhap.Text, textMatKhau.Password, this));

                }

            }
            catch
            {
                CustomMessageBox.Show("Có lỗi khi tạo tài khoản");
            }


        }

        //private void LoadReCaptcha()
        //{
        //    // Replace "YourSiteKey" with your reCAPTCHA site key
        //    string siteKey = "6Le4I1goAAAAAHzVTr_USrF1Up1Z6Qf3dzt1EfAy";
        //    string html = $"<html><head></head><body> aaaaa <script src='https://www.google.com/recaptcha/api2/render?k={siteKey}'></script><div class='g-recaptcha' data-sitekey='{siteKey}'></div></body></html>";

        //    webBrowser.NavigateToString(html);
        //}

        #endregion

    }
}
