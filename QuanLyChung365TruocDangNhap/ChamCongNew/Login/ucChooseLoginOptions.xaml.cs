using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Login
{
    /// <summary>
    /// Interaction logic for ucChooseLoginOptions.xaml
    /// </summary>
    public partial class ucChooseLoginOptions : Window
    {
        //MainWindow Main;
        public ucChooseLoginOptions()
        {
            InitializeComponent();
            var workingArea = System.Windows.SystemParameters.WorkArea;
            this.Width = workingArea.Right - 300;
            this.Height = workingArea.Bottom - 100;
            tb_TaiKhoanDangNhap.Focus();
            // Main = main;    
        }

        private void LoginEp(object sender, MouseButtonEventArgs e)
        {
            borDangNhap.Visibility = Visibility.Visible;
            borDangNhap.Width = this.Width / 2;
            borDangNhapCtyNvien.Visibility = Visibility.Collapsed;
            textTieuDe.Text = "Cùng doanh nghiệp chuyển đổi số, phát triển bản thân, gây dựng tập thể";
            textLoaiTK.Text = "TÀI KHOẢN NHÂN VIÊN";
            tb_TaiKhoanDangNhap.Text = Properties.Settings.Default.UserEp;
            tb_MatKhau.Password = Properties.Settings.Default.PassEp;
        }

        private void LoginCom(object sender, MouseButtonEventArgs e)
        {
            borDangNhap.Visibility = Visibility.Visible;
            borDangNhap.Width = this.Width / 2;
            borDangNhapCtyNvien.Visibility = Visibility.Collapsed;
            textTieuDe.Text = "Bứt phá thành công trong tương lai cùng hệ thống chuyển đổi số top 1";
            textLoaiTK.Text = "TÀI KHOẢN CÔNG TY";
            tb_TaiKhoanDangNhap.Text = Properties.Settings.Default.User;
            tb_MatKhau.Password = Properties.Settings.Default.Pass;
            //InitializeComponent();
            //LoginCompany ucbodyhome = new LoginCompany();
            //Main.dopBody.Children.Clear();
            //object Content = ucbodyhome.Content;
            //ucbodyhome.Content = null;
            //Main.dopBody.Children.Add(Content as UIElement);

        }

        private void tb_MatKhau_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tb_MatKhau.Password == "")
            {
                txtMK.Visibility = Visibility.Visible;
            }
            else
            {
                txtMK.Visibility = Visibility.Collapsed;
            }
        }

        private void btnQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borDangNhap.Visibility = Visibility.Collapsed;
            borDangNhapCtyNvien.Visibility = Visibility.Visible;
        }
        public string acc_token;
        private void btnDangNhap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            try
            {
                using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/qlc/employee/login")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;

                    request.AddParameter("account", tb_TaiKhoanDangNhap.Text);
                    request.AddParameter("password", tb_MatKhau.Password);
                    request.AddParameter("type", "1");
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.Login.LoginCom.Root receivedInfo = JsonConvert.DeserializeObject<OOP.Login.LoginCom.Root>(b);
                    if (receivedInfo.data != null)
                    {
                        if (receivedInfo.data.data.type == "1")
                        {
                            if (textLoaiTK.Text == "TÀI KHOẢN CÔNG TY")
                            {
                                if(receivedInfo.data.data.user_info.com_id == 22222616)
                                {
                                    popup.Children.Add(new NotificationBlockLogin(this));
                                    popup.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    LoadTokenTL();
                                    if (chkLuuTKMK.IsChecked == true)
                                    {
                                        Properties.Settings.Default.User = tb_TaiKhoanDangNhap.Text;
                                        Properties.Settings.Default.Pass = tb_MatKhau.Password;
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
                                    //this.Hide();
                                    //MainWindow main = new MainWindow(receivedInfo.data, this);
                                    //main.Show();
                                    textThongBao.Visibility = Visibility.Collapsed;
                                }
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
                                using (RestClient restclient1 = new RestClient(new Uri("http://210.245.108.202:3000/api/qlc/employee/login")))
                                {
                                    RestRequest request1 = new RestRequest();
                                    request1.Method = Method.Post;
                                    request1.AlwaysMultipartFormData = true;

                                    request1.AddParameter("account", tb_TaiKhoanDangNhap.Text);
                                    request1.AddParameter("password", tb_MatKhau.Password);
                                    request1.AddParameter("type", "2");
                                    RestResponse resAlbum1 = restclient.Execute(request1);
                                    var b1 = resAlbum1.Content;
                                    OOP.Login.LoginCom.Root receivedInf1 = JsonConvert.DeserializeObject<OOP.Login.LoginCom.Root>(b1);
                                    if (receivedInf1.data != null)
                                    {
                                        if (receivedInf1.data.data.user_info.com_id == 22222616)
                                        {
                                            popup.Children.Add(new NotificationBlockLogin(this));
                                            popup.Visibility = Visibility.Visible;
                                        }
                                        else
                                        {
                                            if (chkLuuTKMK.IsChecked == true)
                                            {
                                                Properties.Settings.Default.UserEp = tb_TaiKhoanDangNhap.Text;
                                                Properties.Settings.Default.PassEp = tb_MatKhau.Password;
                                                Properties.Settings.Default.Check = "1";
                                                Properties.Settings.Default.Save();
                                                acc_token = receivedInfo.data.data.access_token;
                                            }
                                            else
                                            {
                                                Properties.Settings.Default.UserEp = "";
                                                Properties.Settings.Default.PassEp = "";
                                                Properties.Settings.Default.Check = "0";
                                                Properties.Settings.Default.Save();
                                            }
                                            Properties.Settings.Default.Token = receivedInfo.data.data.access_token;

                                            //this.Hide();
                                            //MainChamCong main = new MainChamCong(receivedInfo.data, this);
                                            //main.Show();
                                            textThongBao.Visibility = Visibility.Collapsed;
                                        }

                                    }
                                }
                                
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
        public async void LoadTokenTL()
        {
            try
            {
                Dictionary<string, string> form = new Dictionary<string, string>();
                form.Add("email", tb_TaiKhoanDangNhap.Text);
                form.Add("pass", tb_MatKhau.Password);
                HttpClient httpClient = new HttpClient();
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                var respon = await httpClient.PostAsync("https://tinhluong.timviec365.vn/api_app/company/login_comp.php", new FormUrlEncodedContent(form));
                OOP.Login.InfoComTL.Root api = JsonConvert.DeserializeObject<OOP.Login.InfoComTL.Root>(respon.Content.ReadAsStringAsync().Result);
                if (api.data != null)
                {
                    Properties.Settings.Default.TokenTL = api.data.token;
                }
            }
            catch
            {

            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
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
                        request.AddParameter("password", tb_MatKhau.Password);

                        request.AddParameter("type", "1");
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;
                        OOP.Login.LoginCom.Root receivedInfo = JsonConvert.DeserializeObject<OOP.Login.LoginCom.Root>(b);
                        if (receivedInfo.data != null)
                        {
                            if (receivedInfo.data.data.type == "1")
                            {
                                if (textLoaiTK.Text == "TÀI KHOẢN CÔNG TY")
                                {
                                    LoadTokenTL();
                                    if (chkLuuTKMK.IsChecked == true)
                                    {
                                        Properties.Settings.Default.User = tb_TaiKhoanDangNhap.Text;
                                        Properties.Settings.Default.Pass = tb_MatKhau.Password;
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
                                    //this.Hide();
                                    //MainWindow main = new MainWindow(receivedInfo.data, this);
                                    //main.Show();
                                    textThongBao.Visibility = Visibility.Collapsed;
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
                                    Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                                    //this.Hide();
                                    //MainChamCong main = new MainChamCong(receivedInfo.data, this);
                                    //main.Show();
                                    textThongBao.Visibility = Visibility.Collapsed;
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
                //try
                //{
                //    if (textLoaiTK.Text == "TÀI KHOẢN NHÂN VIÊN")
                //    {
                //        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3000/api/qlc/employee/login")))
                //        {
                //            RestRequest request = new RestRequest();
                //            request.Method = Method.Post;
                //            request.AlwaysMultipartFormData = true;

                //            request.AddParameter("account", tb_TaiKhoanDangNhap.Text);
                //            request.AddParameter("password", tb_MatKhau.Password);
                //            request.AddParameter("type", "0");
                //            RestResponse resAlbum = restclient.Execute(request);
                //            var b = resAlbum.Content;
                //            OOP.Login.LoginEP.Root receivedInfo = JsonConvert.DeserializeObject<OOP.Login.LoginEP.Root>(b);
                //            if (receivedInfo.data != null)
                //            {
                //                Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                //                MainWindow main = new MainWindow(receivedInfo.data.data.user_info.com_id, receivedInfo.data.data.user_info.com_name, receivedInfo.data.data.access_token, this);
                //                main.Show();
                //                this.Hide();
                //                textThongBao.Visibility = Visibility.Collapsed;
                //            }
                //            else
                //            {
                //                textThongBao.Visibility = Visibility.Visible;
                //            }
                //        }
                //    }
                //    else if (textLoaiTK.Text == "TÀI KHOẢN CÔNG TY")
                //    {
                //        //try
                //        //{
                //        //    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3000/api/qlc/employee/login")))
                //        //    {
                //        //        RestRequest request = new RestRequest();
                //        //        request.Method = Method.Post;
                //        //        request.AlwaysMultipartFormData = true;

                //        //        request.AddParameter("account", tb_TaiKhoanDangNhap.Text);
                //        //        request.AddParameter("password", tb_MatKhau.Password);

                //        //        request.AddParameter("type", "1");
                //        //        RestResponse resAlbum = restclient.Execute(request);
                //        //        var b = resAlbum.Content;
                //        //        OOP.Login.LoginCom.Root receivedInfo = JsonConvert.DeserializeObject<OOP.Login.LoginCom.Root>(b);
                //        //        if (receivedInfo.data != null)
                //        //        {

                //        //            LoadTokenTL();
                //        //            Properties.Settings.Default.User = tb_TaiKhoanDangNhap.Text;
                //        //            Properties.Settings.Default.Pass = tb_MatKhau.Password;
                //        //            Properties.Settings.Default.Save();
                //        //            Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                //        //            this.Hide();
                //        //            MainWindow main = new MainWindow(receivedInfo.data, this);
                //        //            main.Show();
                //        //            textThongBao.Visibility = Visibility.Collapsed;
                //        //        }
                //        //        else
                //        //        {
                //        //            textThongBao.Visibility = Visibility.Visible;
                //        //        }
                //        //    }
                //        //}
                //        //catch
                //        //{

                //        //}
                //    }
                //}
                //catch
                //{
                //    textThongBao.Visibility = Visibility.Visible;
                //}
            }
        }

        private void btnDangKy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textLoaiTK.Text == "TÀI KHOẢN CÔNG TY")
            {
                Process.Start("https://hungha365.com/dang-ky-cong-ty.html");

            }
            else if (textLoaiTK.Text == "TÀI KHOẢN NHÂN VIÊN")
            {
                Process.Start("https://hungha365.com/dang-ky-nhan-vien.html");

            }
        }
    }
}
