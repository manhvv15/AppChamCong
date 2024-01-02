using Microsoft.Office.Interop.Excel;
using QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan.Entities;
using QuanLyChung365TruocDangNhap.LuanChuyenCongTy.Popups;
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

namespace QuanLyChung365TruocDangNhap.CacChucNangTaiKhoan.Popups
{
    /// <summary>
    /// Interaction logic for ucSuaThaiKhoanNhanVien.xaml
    /// </summary>
    public partial class ucSuaThaiKhoanNhanVien : UserControl
    {
        frmMain Main;
        Data_InforSaft inforSaft = new Data_InforSaft();
        public ucSuaThaiKhoanNhanVien(frmMain main, Data_InforSaft inforsaft)
        {
            InitializeComponent();
            Main = main;
            inforSaft = inforsaft;
            cboEducation.ItemsSource = ListItemComboboxUser.ListCbxEducationEmployee;
            cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee;
            cboMarried.ItemsSource = ListItemComboboxUser.ListCbxMarriedEmployee;
            cboGioiTinh.ItemsSource = ListItemComboboxUser.ListCbxGenderEmployee;
            tb_ID.Text = inforsaft?.idQLC.ToString();
            tb_HoVaTen.Text = inforsaft?.userName;
            tb_Email.Text = inforsaft?.emailContact.ToString();
            tb_SDT.Text = inforsaft?.phone;
            tb_DiaChi.Text = inforsaft?.address;
            dpBirthDay.Part_TextBox.Text = inforsaft?.display_birthday;
            cboGioiTinh.SelectedValue = inforsaft?.gender;
            cboMarried.SelectedValue = inforsaft?.married;
            cboEducation.SelectedValue = inforsaft?.education;
            cboExperience.SelectedValue = inforsaft?.experience;
        }

        private async void bodLuuThongTinNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (tb_HoVaTen.Text == null || tb_HoVaTen.Text == "")
            {
                tb_ValidateHoVaTen.Visibility = Visibility.Visible;
                tb_ValidateHoVaTen.Text = "Bạn vui lòng nhập họ tên";
                allow = false;
            }
            else
            {
                tb_ValidateHoVaTen.Visibility = Visibility.Collapsed;
            }
            if (tb_Email.Text == null || tb_Email.Text == "")
            {
                tb_ValidateEmail.Visibility = Visibility.Visible;
                tb_ValidateEmail.Text = "Bạn vui lòng nhập email";
                allow = false;
            }
            else
            {
                tb_ValidateEmail.Visibility = Visibility.Collapsed;
            }
            if (tb_SDT.Text == null || tb_SDT.Text == "")
            {
                tb_ValidateSDT.Visibility = Visibility.Visible;
                tb_ValidateSDT.Text = "Bạn vui lòng nhập số điện thoại";
                allow = false;
            }
            else
            {
                tb_ValidateSDT.Visibility = Visibility.Collapsed;
            }
            if (tb_DiaChi.Text == null || tb_DiaChi.Text == "")
            {
                tb_ValidateDiaChi.Visibility = Visibility.Visible;
                tb_ValidateDiaChi.Text = "Bạn vui lòng nhập địa chỉ";
                allow = false;
            }
            else
            {
                tb_ValidateDiaChi.Visibility = Visibility.Collapsed;
            }
            if (cboGioiTinh.Text == "")
            {
                tb_ValidateGioiTinh.Visibility = Visibility.Visible;
                tb_ValidateGioiTinh.Text = "Bạn vui lòng chọn giới tính";
                allow = false;
            }
            else
            {
                tb_ValidateGioiTinh.Visibility = Visibility.Collapsed;
            }
            if (cboExperience.Text == "")
            {
                tb_ValidateExperience.Visibility = Visibility.Visible;
                tb_ValidateExperience.Text = "Bạn vui lòng chọn kinh nghiệm";
                allow = false;
            }
            else
            {
                tb_ValidateExperience.Visibility = Visibility.Collapsed;
            }
            if (cboEducation.Text == "")
            {
                tb_ValidateEducation.Visibility = Visibility.Visible;
                tb_ValidateEducation.Text = "Bạn vui lòng chọn học vấn";
                allow = false;
            }
            else
            {
                tb_ValidateEducation.Visibility = Visibility.Collapsed;
            }
            if (cboMarried.Text == "")
            {
                tb_ValidateMarried.Visibility = Visibility.Visible;
                tb_ValidateMarried.Text = "Bạn vui lòng chọn tình trạng hôn nhân";
                allow = false;
            }
            else
            {
                tb_ValidateMarried.Visibility = Visibility.Collapsed;
            }
            if (allow)
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/employee/updateInfoEmployee");
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(tb_HoVaTen.Text), "userName");
                    content.Add(new StringContent(tb_Email.Text), "emailContact");
                    content.Add(new StringContent(tb_SDT.Text), "phone");
                    content.Add(new StringContent(tb_DiaChi.Text), "address");
                    if (dpBirthDay.SelectedDate != null)
                    {
                        content.Add(new StringContent($"{dpBirthDay.SelectedDate.Value.Year}-{dpBirthDay.SelectedDate.Value.Month}-{dpBirthDay.SelectedDate.Value.Day}"), "birthday");
                    }
                    else
                    {
                        DateTime dao;
                        if (DateTime.TryParseExact(inforSaft.display_birthday, "dd-MM-yyyy", null, System.Globalization.DateTimeStyles.None, out dao))
                        {
                            string birdayFormat = dao.ToString("yyyy-MM-dd");
                            content.Add(new StringContent(birdayFormat), "birthday");
                        }
                    }
                    content.Add(new StringContent(cboGioiTinh.SelectedValue.ToString()), "gender");
                    content.Add(new StringContent(cboMarried.SelectedValue.ToString()), "married");
                    content.Add(new StringContent(cboExperience.SelectedValue.ToString()), "experience");
                    content.Add(new StringContent(cboEducation.SelectedValue.ToString()), "education");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resContent = await response.Content.ReadAsStringAsync();
                        ThongTinTaiKhoan qlnv = new ThongTinTaiKhoan(Main);
                        Main.stp_ShowPopup.Children.Clear();
                        object Content = qlnv.Content;
                        qlnv.Content = null;
                        Main.stp_ShowPopup.Children.Add(Content as UIElement);
                        Main.pnlShowPopUp.Children.Add(new ucThongBaoThanhCong(inforSaft));
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        private void cboEducation_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboEducation.SelectedIndex = -1;
                string textSearch = cboEducation.Text;
                cboEducation.Items.Refresh();
                cboEducation.ItemsSource = ListItemComboboxUser.ListCbxEducationEmployee.Where(t => t.value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cboEducation_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboEducation.SelectedIndex = -1;
            string textSearch = cboEducation.Text + e.Text;
            cboEducation.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboEducation.Text = "";
                cboEducation.Items.Refresh();
                cboEducation.ItemsSource = ListItemComboboxUser.ListCbxEducationEmployee;
                cboEducation.SelectedIndex = -1;
            }
            else
            {
                cboEducation.ItemsSource = "";
                cboEducation.Items.Refresh();
                cboEducation.ItemsSource = ListItemComboboxUser.ListCbxEducationEmployee.Where(t => t.value.ToLower().Contains(textSearch.ToLower()));
            }

        }

        private void cboExperience_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboExperience.SelectedIndex = -1;
                string textSearchKN = cboExperience.Text;
                cboExperience.Items.Refresh();
                cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee.Where(t => t.value.ToLower().Contains(textSearchKN.ToLower()));
            }
        }

        private void cboExperience_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboExperience.SelectedIndex = -1;
            string textSearchKN = cboExperience.Text + e.Text;
            cboExperience.IsDropDownOpen = true;
            if (textSearchKN == "")
            {
                cboExperience.Text = "";
                cboExperience.Items.Refresh();
                cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee;
                cboExperience.SelectedIndex = -1;
            }
            else
            {
                cboExperience.ItemsSource = "";
                cboExperience.Items.Refresh();
                cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee.Where(t => t.value.ToLower().Contains(textSearchKN.ToLower()));
            }

        }
        private void cboGioiTinh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboGioiTinh.SelectedIndex = -1;
                string textSearchGT = cboGioiTinh.Text;
                cboGioiTinh.Items.Refresh();
                cboGioiTinh.ItemsSource = ListItemComboboxUser.ListCbxGenderEmployee.Where(t => t.value.ToLower().Contains(textSearchGT.ToLower()));
            }
        }

        private void cboGioiTinh_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboGioiTinh.SelectedIndex = -1;
            string textSearchGT = cboGioiTinh.Text + e.Text;
            cboGioiTinh.IsDropDownOpen = true;
            if (textSearchGT == "")
            {
                cboGioiTinh.Text = "";
                cboGioiTinh.Items.Refresh();
                cboGioiTinh.ItemsSource = ListItemComboboxUser.ListCbxGenderEmployee;
                cboGioiTinh.SelectedIndex = -1;
            }
            else
            {
                cboGioiTinh.ItemsSource = "";
                cboGioiTinh.Items.Refresh();
                cboGioiTinh.ItemsSource = ListItemComboboxUser.ListCbxGenderEmployee.Where(t => t.value.ToLower().Contains(textSearchGT.ToLower()));
            }
        }

        private void cboMarried_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cboMarried_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ThongTinTaiKhoan qlnv = new ThongTinTaiKhoan(Main);
            Main.stp_ShowPopup.Children.Clear();
            object Content = qlnv.Content;
            qlnv.Content = null;
            Main.stp_ShowPopup.Children.Add(Content as UIElement);
        }

        private void bodLuuThongTinNhanVien_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
