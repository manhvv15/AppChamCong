using ChamCong365.NhanVien;
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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChamCong365.Login
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
            
            // Main = main;    
        }

        private void LoginEp(object sender, MouseButtonEventArgs e)
        {
            borDangNhap.Visibility = Visibility.Visible;
            borDangNhap.Width = this.Width / 2;
            borDangNhapCtyNvien.Visibility = Visibility.Collapsed;
            textTieuDe.Text = "Cùng doanh nghiệp chuyển đổi số, phát triển bản thân, gây dựng tập thể";
            textLoaiTK.Text = "TÀI KHOẢN NHÂN VIÊN";
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

        private void btnDangNhap_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
                            LoadTokenTL();
                            Properties.Settings.Default.User = tb_TaiKhoanDangNhap.Text;
                            Properties.Settings.Default.Pass = tb_MatKhau.Password;
                            Properties.Settings.Default.Save();
                            Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                            this.Hide();
                            MainWindow main = new MainWindow(receivedInfo.data, this);
                            main.Show();
                            textThongBao.Visibility = Visibility.Collapsed;
                        }
                        else if (receivedInfo.data.data.type == "2")
                        {
                            Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                            this.Hide();
                            MainChamCong main = new MainChamCong(receivedInfo.data, this);
                            main.Show();
                            textThongBao.Visibility = Visibility.Collapsed;
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
                                LoadTokenTL();
                                Properties.Settings.Default.User = tb_TaiKhoanDangNhap.Text;
                                Properties.Settings.Default.Pass = tb_MatKhau.Password;
                                Properties.Settings.Default.Save();
                                Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                                this.Hide();
                                MainWindow main = new MainWindow(receivedInfo.data, this);
                                main.Show();
                                textThongBao.Visibility = Visibility.Collapsed;
                            }
                            else if (receivedInfo.data.data.type == "2")
                            {
                                Properties.Settings.Default.Token = receivedInfo.data.data.access_token;
                                this.Hide();
                                MainChamCong main = new MainChamCong(receivedInfo.data, this);
                                main.Show();
                                textThongBao.Visibility = Visibility.Collapsed;
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
    }
}
