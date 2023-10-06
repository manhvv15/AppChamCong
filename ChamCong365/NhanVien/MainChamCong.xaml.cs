
using ChamCong365.Login;
using ChamCong365.NhanVien.ChamCongBangQRRR.Function;
using ChamCong365.NhanVien.ChamCongBangTaiKhoanCongTy.Function;
using ChamCong365.NhanVien.ChamCongKhuonMat.Function;
using ChamCong365.NhanVien.LichSu.Function;
using ChamCong365.NhanVien.Propose;
using ChamCong365.OOP.NhanVien.ChamCongNhanVien;
using ChamCong365.TimeKeeping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChamCong365.NhanVien
{
    /// <summary>
    /// Interaction logic for MainChamCong.xaml
    /// </summary>
    public partial class MainChamCong : Window
    {
        MainChamCong Main;
       
        BrushConverter bcMain = new BrushConverter();
        public ucChooseLoginOptions frmLogin;
        public OOP.Login.LoginCom.Data Dt;
        public int Test = 0;
        public int Back = 0;
        public int IdEp = 0;
        public int Ep_Id = 0;
        public int ComdID = 0;

        public MainChamCong(OOP.Login.LoginCom.Data data, ucChooseLoginOptions login)
        {
            InitializeComponent();
            try
            {
                LoadInfoSaff();
                StartDynamicText();
                LoadImgEp(data);
                Dt = data;
                frmLogin = login;
                IdEp = data.data._id;
                Ep_Id = data.data.user_info.com_id;
                txbNameAccount.Text = data.data.user_info.com_name;
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listChamCong uc = new listChamCong(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            
            }
            catch
            {

            }

        }
        private Thread textUpdateThread;
        private void StartDynamicText()
        {
            try
            {
                textUpdateThread = new Thread(() =>
                {
                    while (true)
                    {
                        // Thay đổi nội dung của TextBlock
                        Dispatcher.Invoke(() =>
                        {
                            dynamicTextBlock.Text = "" + DateTime.Now.ToString("HH:mm:ss");
                        });

                        // Đợi 1 giây trước khi cập nhật lại
                        Thread.Sleep(1000);
                    }
                });

                textUpdateThread.Start();
            }
            catch (Exception)
            {
                throw;
            }

        }
        public async void LoadInfoSaff()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/employee/info");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();

                Root result = JsonConvert.DeserializeObject<Root>(resContent);
                if (result != null)
                {
                    ComdID = result.data.data.com_id;
                }
            }
            catch (Exception)
            {
            }
        }
        private void LoadImgEp(OOP.Login.LoginCom.Data dt)
        {
            var img = new Uri("https://chamcong.24hpay.vn/upload/employee/" + dt.data.user_info.com_logo);
            BitmapImage bm = new BitmapImage(img);
            ImgAvatar.ImageSource = bm;
        }

        private int i = 0;
        private void Window_SizeChangedChamCong(object sender, SizeChangedEventArgs e)
        {
            //ChamCongBangQR uc = new ChamCongBangQR(this);
            //if (i == 0)
            //{
            //    if (MainWindowChamCong.Width <= 1024)
            //    {

            //        HearderColesspa.Visibility = Visibility.Visible;
            //        HearderVisibility.Visibility = Visibility.Collapsed;

            //    }
            //    else
            //    {

            //        HearderColesspa.Visibility = Visibility.Collapsed;
            //        HearderVisibility.Visibility = Visibility.Visible;
            //        MenuCollapsed.Visibility = Visibility.Collapsed;

            //    }
            //}
           
        }

        private void MainWindows_StateChangedChamcong(object sender, EventArgs e)
        {

        }

        private void Grid_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
            //dopBody.Children.Clear();
            //object Content = ucbodyhome.Content;
            //ucbodyhome.Content = null;
            //dopBody.Children.Add(Content as UIElement);
            if (Back == 1)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listChamCong uc = new listChamCong(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            }else if(Back == 2)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listKhuonMat uc = new listKhuonMat(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            }else if(Back == 3)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listCompany uc = new listCompany(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            }else if(Back == 4)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listPropose uc = new listPropose(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
            }else if(Back == 5)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);
            }else if (Back == 6)
            {
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listHistory uc = new listHistory(this,null);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object content = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(content as UIElement);
            }
        }

        private void MainWindowChamCong_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void clearPopUp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borThongTinChiTiet.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
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
            
        }
        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textUpdateThread != null && textUpdateThread.IsAlive)
            {
                textUpdateThread.Abort(); // Tắt luồng
            }
            Application.Current.Shutdown();
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
            

        }

        private void pnlTieuDe1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }
        private void btnMinimize_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void LogOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            grShowPopup.Children.Add(new Popup.PopUpHoiTruocKhiDangXuat(this, frmLogin));

        }

        private void bodBackto_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btnBackTo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                
                ChamCongBangQR ucbodyhome = new ChamCongBangQR(this);
                dopBody.Children.Clear();
                object Content = ucbodyhome.Content;
                ucbodyhome.Content = null;
                dopBody.Children.Add(Content as UIElement);

                listChamCong uc = new listChamCong(this);
                ucbodyhome.grLoadFunctionQR.Children.Clear();
                object Content1 = uc.Content;
                uc.Content = null;
                ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
                LoadImgEp(Dt);
            }
            catch
            {

            }
        }
    }
}
