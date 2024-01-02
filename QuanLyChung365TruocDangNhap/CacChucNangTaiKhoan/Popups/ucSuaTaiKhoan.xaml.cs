using QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan.Entities;
using QuanLyChung365TruocDangNhap.LuanChuyenCongTy.Popups;
using System;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan.Popups
{
    /// <summary>
    /// Interaction logic for ucSuaTaiKhoan.xaml
    /// </summary>
    public partial class ucSuaTaiKhoan : UserControl
    {
        frmMain Main;
        Data_Company inforCom = new Data_Company();
        ThongTinTaiKhoan Infor;
        public ucSuaTaiKhoan(frmMain main, Data_Company inforcom, ThongTinTaiKhoan infor)
        {
            InitializeComponent();
            Main = main;
            inforCom = inforcom;
            Infor = infor;
            tb_Label1.Text = "Tên công ty";
            tb_Label2.Text = "Số điện thoại";
            tb_Label3.Text = "Email";
            tb_Label4.Text = "Địa chỉ";

            txt_Texbox1.Text = "Nhập tên công ty";
            txt_Texbox2.Text = "Nhập số điện thoại";
            txt_Texbox3.Text = "Nhập Email";
            txt_Texbox4.Text = "Nhập địa chỉ công ty";

            tb_TieuDe.Text = "Chỉnh sửa thông tin";

            tb_Texbox1.Text = inforcom.com_name;
            tb_Texbox2.Text = inforcom.com_phone;
            tb_Texbox3.Text = inforcom.com_email;
            tb_Texbox4.Text = inforcom.com_address;

            stp_TextBox1.Visibility = Visibility.Visible;
            stp_TextBox2.Visibility = Visibility.Visible;
            stp_TextBox3.Visibility = Visibility.Visible;
            stp_TextBox4.Visibility = Visibility.Visible;

            stp_PasstextBox1.Visibility = Visibility.Collapsed;
            stp_PasstextBox2.Visibility = Visibility.Collapsed;
            stp_PasstextBox3.Visibility = Visibility.Collapsed;

        }

        Data_InforSaft inforSaft = new Data_InforSaft();
        public ucSuaTaiKhoan(frmMain main, Data_Company inforcom, Data_InforSaft inforsaft, ThongTinTaiKhoan infor)
        {
            InitializeComponent();
            Main = main;
            inforCom = inforcom;
            inforSaft = inforsaft;
            Infor = infor;
            stp_TextBox1.Visibility = Visibility.Collapsed;
            stp_TextBox2.Visibility = Visibility.Collapsed;
            stp_TextBox3.Visibility = Visibility.Collapsed;
            stp_TextBox4.Visibility = Visibility.Collapsed;
            //stp_TextBox3.Margin = new Thickness(20, 0, 20, 20);

            tb_LabelPass1.Text = "Mật khẩu cũ";
            tb_LabelPass2.Text = "Mật khẩu mới";
            tb_LabelPass3.Text = "Nhập lại mật khẩu";
            
            tb_TieuDe.Text = "Đổi mật khẩu";
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        string UpdatePasswordCompany = "https://api.timviec365.vn/api/qlc/company/updateNewPassword";
        string UpdatePasswordEmloyeer = "https://api.timviec365.vn/api/qlc/employee/updatePassword";
        string ApiUpdate;
        private async void btn_HoanThanh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (stp_TextBox1.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(tb_Texbox1.Text))
                {
                    tb_ValidateText1.Visibility = Visibility.Visible;
                    tb_ValidateText1.Text = "Bạn vui lòng nhập thông tin";
                    allow = false;
                }
                else
                {
                    tb_ValidateText1.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_TextBox2.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(tb_Texbox2.Text))
                {
                    tb_ValidateText2.Visibility = Visibility.Visible;
                    tb_ValidateText2.Text = "Bạn vui lòng nhập thông tin";
                    allow = false;
                }
                else
                {
                    tb_ValidateText2.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_TextBox3.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(tb_Texbox3.Text))
                {
                    tb_ValidateText3.Visibility = Visibility.Visible;
                    tb_ValidateText3.Text = "Bạn vui lòng nhập thông tin";
                    allow = false;
                }
                else
                {
                    tb_ValidateText3.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_TextBox4.Visibility == Visibility.Visible)
            {
                if (stp_TextBox4.Visibility == Visibility.Visible)
                {
                    if (string.IsNullOrEmpty(tb_Texbox1.Text))
                    {
                        tb_ValidateText1.Visibility = Visibility.Visible;
                        tb_ValidateText1.Text = "Bạn vui lòng nhập thông tin";
                        allow = false;
                    }
                    else
                    {
                        tb_ValidateText1.Visibility = Visibility.Collapsed;
                    }
                }
            }
            if (stp_PasstextBox1.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(tb_MatKhauGo1.Password))
                {
                    tb_ValidateTextPass1.Visibility = Visibility.Visible;
                    tb_ValidateTextPass1.Text = "Bạn vui lòng nhập thông tin";
                    allow = false;
                }
                else
                {
                    tb_ValidateTextPass1.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_PasstextBox2.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(tb_MatKhauGo2.Password))
                {
                    tb_ValidateTextPass2.Visibility = Visibility.Visible;
                    tb_ValidateTextPass2.Text = "Bạn vui lòng nhập thông tin";
                    allow = false;
                }
                else
                {
                    tb_ValidateTextPass2.Visibility = Visibility.Collapsed;
                }
            }
            if (stp_PasstextBox3.Visibility == Visibility.Visible)
            {
                if (string.IsNullOrEmpty(tb_MatKhauGo1.Password))
                {
                    tb_ValidateTextPass3.Visibility = Visibility.Visible;
                    tb_ValidateTextPass3.Text = "Bạn vui lòng nhập thông tin";
                    allow = false;
                }
                else
                {
                    tb_ValidateTextPass3.Visibility = Visibility.Collapsed;
                }
            }
            if (allow)
            {
                if (tb_TieuDe.Text == "Chỉnh sửa thông tin")
                {
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/company/updateInfoCompany");
                        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(tb_Texbox1.Text), "userName");
                        content.Add(new StringContent(tb_Texbox2.Text), "emailContact");
                        content.Add(new StringContent(tb_Texbox3.Text), "phone");
                        content.Add(new StringContent(tb_Texbox4.Text), "address");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                        {
                            var resConten = await response.Content.ReadAsStringAsync();
                            Infor.LoadInforCompany();
                            Main.pnlShowPopUp.Children.Add(new ucThongBaoThanhCong(inforCom));
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                    catch (Exception)
                    {
                    }
                }
                else
                {
                    try
                    {
                        var client = new HttpClient();
                        if (Main.Type == 1)
                        {
                            ApiUpdate = UpdatePasswordCompany;
                        }
                        else
                        {
                            ApiUpdate = UpdatePasswordEmloyeer;
                        }
                        var request = new HttpRequestMessage(HttpMethod.Post, ApiUpdate);
                        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(tb_MatKhauGo1.Password), "old_password");
                        content.Add(new StringContent(tb_MatKhauGo2.Password), "password");
                        content.Add(new StringContent(tb_MatKhauGo3.Password), "re_password");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                        {
                            var resContent = await response.Content.ReadAsStringAsync();
                            if (Main.Type == 1)
                            {
                                Infor.LoadInforCompany();
                                Main.pnlShowPopUp.Children.Add(new ucThongBaoThanhCong(Main, inforCom, inforSaft));
                            }
                            else
                            {
                                Infor.LoadInforSaff();
                                Main.pnlShowPopUp.Children.Add(new ucThongBaoThanhCong(Main, inforCom, inforSaft));
                            }
                            this.Visibility = Visibility.Collapsed;
                        }
                      

                    }
                    catch (Exception)
                    {
                        Main.pnlShowPopUp.Children.Add(new ucThongBaoThanhCong());
                    }
                }
            }
        }

        static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Chuyển đổi mật khẩu thành mảng byte
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Chuyển đổi mảng byte thành chuỗi hex
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        static bool VerifyPassword(string inputPassword, string hashedPassword)
        {
            // So sánh chuỗi hash mới tạo ra với chuỗi hash đã cho
            return string.Equals(inputPassword, hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
        private void btn_Huy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Password1(object sender, TextCompositionEventArgs e)
        {
            if (tb_Label1.Text == "Mật khẩu cũ")
            {
                e.Handled = true;
                tb_Texbox1.Text += "•";
            }
        }
        private void Password2(object sender, TextCompositionEventArgs e)
        {
            if (tb_Label2.Text == "Mật khẩu mới" )
            {
                e.Handled = true;
                tb_Texbox2.Text += "•";
            }
        }
        private void Password3(object sender, TextCompositionEventArgs e)
        {
            if ( tb_Label3.Text == "Nhập lại mật khẩu")
            {
                e.Handled = true;
                tb_Texbox3.Text += "•";
            }
        }

        private void tb_MatKhauGo1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tb_MatKhauGo1.Password == "")
            {
                txtMK1.Visibility = Visibility.Visible;
            }
            else
            {
                txtMK1.Visibility = Visibility.Collapsed;
            }
        }

        private void tb_MatKhauGo2_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tb_MatKhauGo2.Password == "")
            {
                txtMK2.Visibility = Visibility.Visible;
            }
            else
            {
                txtMK2.Visibility = Visibility.Collapsed;
            }
        }
        private void tb_MatKhauGo3_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (tb_MatKhauGo3.Password == "")
            {
                txtMK3.Visibility = Visibility.Visible;
            }
            else
            {
                txtMK3.Visibility = Visibility.Collapsed;
            }
        }
    }
}
