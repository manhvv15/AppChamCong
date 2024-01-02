using Microsoft.Win32;
using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan.Entities;
using QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan.Popups;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages;
using System;
using System.IO;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan
{
    /// <summary>
    /// Interaction logic for ThongTinTaiKhoan.xaml
    /// </summary>
    public partial class ThongTinTaiKhoan : UserControl
    {
        frmMain frmMain;
        public ThongTinTaiKhoan(frmMain frmmain)
        {
            InitializeComponent();
            this.frmMain = frmmain;
            if (frmMain.Type == 1)
            {
                LoadInforCompany();
                stp_LabelHeader.Visibility = Visibility.Collapsed;
                tb_Label6.Visibility = Visibility.Collapsed;
                tb_Label7.Visibility = Visibility.Collapsed;
                tb_Label8.Visibility = Visibility.Collapsed;
                tb_TexInfor6.Visibility = Visibility.Collapsed;
                tb_TexInfor7.Visibility = Visibility.Collapsed;
                tb_TexInfor8.Visibility = Visibility.Collapsed;
            }
            else
            {
                LoadInforSaff();
                tb_Label1.Text = "Kinh nghiệm làm việc:";
                tb_Label2.Text = "Ngày bắt đầu làm việc:";
                tb_Label3.Text = "Tài khoản đăng nhập:";
                tb_Label4.Text = "Số điện thoại:";
                tb_Label5.Text = "Ngày sinh:";
                tb_Label9.Text = "Gới tính";
                tb_Label6.Text = "Trình độ học vấn:";
                tb_Label7.Text = "Địa chỉ:";
                tb_Label8.Text = "Tình trạng hôn nhân:";
            }
           
        }
        Data_Company inforCom = new Data_Company();
        public async void LoadInforCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/company/info");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resConten = await response.Content.ReadAsStringAsync();
                    Root_Company infor = JsonConvert.DeserializeObject<Root_Company>(resConten);
                    if (infor != null)
                    {
                        inforCom = infor.data.data;
                        tb_IdSaff.Text = inforCom.com_id.ToString();
                        tb_TexInfor1.Text = inforCom.com_email;
                        if (inforCom.com_address == null)
                        {
                            tb_TexInfor3.Text = "Chưa cập nhật";
                        }
                        else
                        {
                            tb_TexInfor3.Text = inforCom.com_address;
                        }
                        tb_TexInfor2.Text = inforCom.com_phone;
                        tb_TexInfor5.Text = inforCom.userNum.ToString();
                        tb_TexInfor4.Text = inforCom.departmentsNum.ToString();
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        Data_InforSaft inforSaft = new Data_InforSaft();
        public async void LoadInforSaff()
        {
            try
            {
                 var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/employee/info");
            request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
            var response = await client.SendAsync(request);
            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
            {
                var resConten = await response.Content.ReadAsStringAsync();
                Root_InforSaft infor = JsonConvert.DeserializeObject<Root_InforSaft>(resConten);
                if (infor.data.data != null)
                {
                    inforSaft = infor.data.data;
                    tb_LabelHeader1.Text = inforSaft.userName;
                    tb_LabelHeader2.Text = inforSaft.companyName.userName;
                    tb_LabelHeader3.Text = inforSaft.positionName;

                    switch (inforSaft.experience)
                    {
                        case 0:
                            tb_TexInfor1.Text  = "Chưa cập nhật";
                            break;
                        case 1:
                            tb_TexInfor1.Text = "CHưa có kinh nghiệm";
                            break;
                        case 2:
                            tb_TexInfor1.Text = "Dưới 1 năm kinh nghiệm";
                            break;
                        case 3:
                            tb_TexInfor1.Text = "1 năm";
                            break;
                        case 4:
                            tb_TexInfor1.Text = "2 năm";
                            break;
                        case 5:
                            tb_TexInfor1.Text = "3 năm";
                            break;
                        case 6:
                            tb_TexInfor1.Text = "4 năm";
                            break;
                        case 7:
                            tb_TexInfor1.Text = "5 năm";
                            break;
                        case 8:
                            tb_TexInfor1.Text = "Trên 5 năm";
                            break;
                        default:
                        tb_TexInfor1.Text = "Chưa cập nhật";
                        break;
                    }
                    tb_TexInfor2.Text = inforSaft.display_start_working_time;
                    tb_TexInfor3.Text = inforSaft.phoneTK.ToString();
                    tb_TexInfor4.Text = inforSaft.phone.ToString();
                    tb_TexInfor5.Text = inforSaft.display_birthday.ToString();
                    switch (inforSaft.gender)
                    {
                        case 1:
                            tb_TexInfor9.Text = "Nam";
                            break;
                        case 2:
                            tb_TexInfor9.Text = "Nữ";
                            break;
                        case 3:
                            tb_TexInfor9.Text = "Khác";
                            break;
                        default:
                            tb_TexInfor9.Text = "Nam";
                            break;
                     }
                   
                    switch (inforSaft.education)
                    {
                        case 0:
                            tb_TexInfor6.Text = "Chưa cập nhật";
                            break;
                        case 1:
                            tb_TexInfor6.Text = "Trên đại học";
                            break;
                        case 2:
                            tb_TexInfor6.Text = "Đại học";
                            break;
                        case 3:
                            tb_TexInfor6.Text = "Cao đẳng";
                            break;
                        case 4:
                            tb_TexInfor6.Text = "Trung cấp";
                            break;
                        case 5:
                            tb_TexInfor6.Text = "Đào tạo nghề";
                            break;
                        case 6:
                            tb_TexInfor6.Text = "Trung hoc phổ thông";
                            break;
                        case 7:
                            tb_TexInfor6.Text = "Trung học cơ sở";
                            break;
                        case 8:
                            tb_TexInfor6.Text = "Tiểu học";
                            break;
                        default:
                            tb_TexInfor6.Text = "Chưa cập nhật";
                            break;
                    }
                    tb_TexInfor7.Text = inforSaft.address.ToString();
                    switch (inforSaft.married)
                    {
                        case 0:
                            tb_TexInfor8.Text = "Chưa cập nhật";
                            break;
                        case 1:
                            tb_TexInfor8.Text = "Độc thân";
                            break;
                        case 2:
                            tb_TexInfor8.Text = "Đã kết hôn";
                            break;
                        default:
                            tb_TexInfor8.Text = "Chưa cập nhật";
                            break;
                    }
                    tb_IdSaff.Text = inforSaft.idQLC.ToString();
                }
            }
            }
            catch (Exception)
            {
            }
           
        }

        private void btn_ChinhSuaThongTin_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (frmMain.Type == 1)
            {
                frmMain.pnlShowPopUp.Children.Add(new ucSuaTaiKhoan(frmMain, inforCom, this));
            }
            else
            {
                ucSuaThaiKhoanNhanVien qlnv = new ucSuaThaiKhoanNhanVien(frmMain, inforSaft);
                frmMain.stp_ShowPopup.Children.Clear();
                object Content = qlnv.Content;
                qlnv.Content = null;
                frmMain.stp_ShowPopup.Children.Add(Content as UIElement);
               
            } 
        }

        private void btn_DoiMatKhau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           frmMain.pnlShowPopUp.Children.Add(new ucSuaTaiKhoan(frmMain, inforCom, inforSaft, this));
        }

        private async void btn_CapNhatAnh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Tệp ảnh (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Tất cả các tệp (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string selectedImagePath = openFileDialog.FileName;
                inforCom.com_logo = selectedImagePath;
                ImageBrush imageBrush = new ImageBrush(new BitmapImage(new Uri(selectedImagePath)));
                byte[] imageBytes = File.ReadAllBytes(selectedImagePath);
                btn_AnhDaiDien.Background = imageBrush;
                img_Logo.Visibility = Visibility.Collapsed;
                if (frmMain.Type == 1)
                {
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/company/updateInfoCompany");
                        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(imageBytes.ToString()), "avatarUser");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        response.EnsureSuccessStatusCode();
                        var resContent = await response.Content.ReadAsStringAsync();

                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
    }
}
