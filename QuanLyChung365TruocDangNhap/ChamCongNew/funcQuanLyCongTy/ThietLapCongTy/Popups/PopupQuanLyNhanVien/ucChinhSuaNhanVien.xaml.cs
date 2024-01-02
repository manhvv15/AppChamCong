using Newtonsoft.Json;

using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Them_Xoa_NhanVien;
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
using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using System.Globalization;
using System.Text.RegularExpressions;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupQuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for ucThemMoiNhanVien.xaml
    /// </summary>
    public partial class ucChinhSuaNhanVien : UserControl
    {
        MainWindow Main;
        ucTatCaNhanVien ucTatCaNhanVien;
        BrushConverter br = new BrushConverter();

        public List<ListPositionEntities.PositionData> lstPositionData;
        public List<ListOrganizeEntities.OrganizeData> lstOrganizeData;

        List<API_List_District.City> listCities = new List<API_List_District.City>();
        List<API_List_District.District> listDistricts = new List<API_List_District.District>();
        List<API_List_District.District> listDistrictsByCity = new List<API_List_District.District>();
        public ucChinhSuaNhanVien(MainWindow Main, ucTatCaNhanVien ucTatCaNhanVien, List_NhanVien employee)
        {
            InitializeComponent();
            this.Main = Main;
            this.ucTatCaNhanVien = ucTatCaNhanVien;
            cboEducation.ItemsSource = ListItemComboboxUser.ListCbxEducationEmployee;
            cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee;
            cboMarried.ItemsSource = ListItemComboboxUser.ListCbxMarriedEmployee;
            cboGioiTinh.ItemsSource = ListItemComboboxUser.ListCbxGenderEmployee;
            LoadEmployeeInfo(employee);


        }
        public async void LoadEmployeeInfo(List_NhanVien employee)
        {
            try
            {
                textPhoneTaiKhoan.Text = await GetPhoneTKEmployee((int)employee.ep_id);
                await GetListOrganize();
                await GetListPosition();
                if (employee.ep_id != null)
                {
                    textEmId.Text = employee.ep_id.ToString();

                }

                textNhapCompanyName.Text = Main.txbNameAccount.Text;
                if (employee.userName != null)
                {
                    textNhapHoTen.Text = employee.userName;

                }
                if (employee.phone != null)
                {
                    textSoDT.Text = employee.phone;

                }
                if (employee.address != null)
                {
                    textDiaChi2.Text = employee.address;
                }
                if (employee.gender != null)
                {
                    cboGioiTinh.SelectedItem = ListItemComboboxUser.ListCbxGenderEmployee.FirstOrDefault(x => x.ID == (employee.gender)?.ToString());
                }
                if (employee.education != null)
                {
                    cboEducation.SelectedItem = ListItemComboboxUser.ListCbxEducationEmployee.FirstOrDefault(x => x.ID == (employee.education)?.ToString());
                }
                if (employee.experience != null)
                {
                    cboExperience.SelectedItem = ListItemComboboxUser.ListCbxExpEmployee.FirstOrDefault(x => x.ID == (employee.experience)?.ToString());
                }
                if (employee.birthday != null)
                {
                    DateTime? birthDay = (DateTimeOffset.FromUnixTimeSeconds((long)(employee?.birthday)).ToLocalTime()).DateTime;
                    dpBirthDay.SelectedDate = birthDay;

                }
                if (employee.married != null)
                {
                    cboMarried.SelectedItem = ListItemComboboxUser.ListCbxMarriedEmployee.FirstOrDefault(x => x.ID == (employee.married)?.ToString());
                }
                if (employee.organizeDetailId != null)
                {

                    cboTenToChuc.SelectedItem = lstOrganizeData?.FirstOrDefault(x => x.id == employee?.organizeDetailId);
                }
                if (employee.position_id != null)
                {

                    cboViTri.SelectedItem = lstPositionData?.FirstOrDefault(x => x.id == employee.position_id);
                }
                if (employee.start_working_time != null)
                {
                    dpStartWork.SelectedDate = (DateTimeOffset.FromUnixTimeSeconds((long)(employee?.start_working_time)).ToLocalTime()).DateTime;
                }
            }
            catch
            {
                Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra"));
            }

        }
        bool allow = true;
        public bool ValidateAddSaff()
        {
            bool allow = true;

            if (string.IsNullOrEmpty(textNhapHoTen.Text))
            {
                allow = false;
                tb_ValidateHoVaTen.Visibility = Visibility.Visible;
                tb_ValidateHoVaTen.Text = "Họ tên không được để trống";
            }
            else
            {
                tb_ValidateHoVaTen.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(textSoDT.Text))
            {
                allow = false;
                tb_ValidateSDT.Visibility = Visibility.Visible;
                tb_ValidateSDT.Text = "Email không được để trống";
            }
            else
            {
                tb_ValidateSDT.Visibility = Visibility.Collapsed;
            }

            if (string.IsNullOrEmpty(textDiaChi2.Text))
            {
                allow = false;
                tb_ValidateDiaChi.Visibility = Visibility.Visible;
                tb_ValidateDiaChi.Text = "Địa chỉ không được để trống";
            }
            else
            {
                tb_ValidateDiaChi.Visibility = Visibility.Collapsed;
            }
            if (cboGioiTinh.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateGioiTinh.Visibility = Visibility.Visible;
                tb_ValidateGioiTinh.Text = "Giới Tính không được để trống";
            }
            else
            {
                tb_ValidateGioiTinh.Visibility = Visibility.Collapsed;
            }
            if (string.IsNullOrEmpty(textPhoneTaiKhoan.Text))
            {
                allow = false;
                tb_ValidatePhoneTaiKhoan.Visibility = Visibility.Visible;
                tb_ValidatePhoneTaiKhoan.Text = "không được để trống";
            }
            else
            {
                tb_ValidatePhoneTaiKhoan.Visibility = Visibility.Collapsed;
            }

            if (cboTenToChuc.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateTenToChuc.Visibility = Visibility.Visible;
                tb_ValidateTenToChuc.Text = "không được để trống";
            }
            else
            {
                tb_ValidateTenToChuc.Visibility = Visibility.Collapsed;
            }
            if (cboViTri.Text == null)
            {
                allow = false;
                tb_ValidateViTri.Visibility = Visibility.Visible;
                tb_ValidateViTri.Text = "không được để trống";
            }
            else
            {
                tb_ValidateViTri.Visibility = Visibility.Collapsed;
            }
            if (cboEducation.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateEducation.Visibility = Visibility.Visible;
                tb_ValidateEducation.Text = "không được để trống";
            }
            else
            {
                tb_ValidateEducation.Visibility = Visibility.Collapsed;
            }
            if (cboExperience.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateExperience.Visibility = Visibility.Visible;
                tb_ValidateExperience.Text = "không được để trống";
            }
            else
            {
                tb_ValidateExperience.Visibility = Visibility.Collapsed;
            }
            if (cboMarried.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateMarried.Visibility = Visibility.Visible;
                tb_ValidateMarried.Text = "không được để trống";
            }
            else
            {
                tb_ValidateMarried.Visibility = Visibility.Collapsed;
            }
            if (cboViTri.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateViTri.Visibility = Visibility.Visible;
                tb_ValidateViTri.Text = "không được để trống";
            }
            else
            {
                tb_ValidateViTri.Visibility = Visibility.Collapsed;
            }
            if (cboMarried.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateMarried.Visibility = Visibility.Visible;
                tb_ValidateMarried.Text = "không được để trống";
            }
            else
            {
                tb_ValidateMarried.Visibility = Visibility.Collapsed;
            }
            if (cboGioiTinh.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateGioiTinh.Visibility = Visibility.Visible;
                tb_ValidateGioiTinh.Text = "không được để trống";
            }
            else
            {
                tb_ValidateGioiTinh.Visibility = Visibility.Collapsed;
            }
            if (dpBirthDay.SelectedDate >= DateTime.Now || !IsDateInputValid(dpBirthDay.Part_TextBox.Text))
            {
                allow = false;
                tb_ValidateNgaySinh.Visibility = Visibility.Visible;
                tb_ValidateNgaySinh.Text = "Ngày sinh không hợp lệ";
            }
            else
            {
                tb_ValidateNgaySinh.Visibility = Visibility.Collapsed;
            }
            if (dpStartWork.SelectedDate >= DateTime.Now || dpStartWork.SelectedDate <= dpBirthDay.SelectedDate || !IsDateInputValid(dpBirthDay.Part_TextBox.Text))
            {
                allow = false;
                tb_ValidateStartWork.Visibility = Visibility.Visible;
                tb_ValidateStartWork.Text = "Ngày bắt đầu làm không hợp lệ";
            }
            else
            {
                tb_ValidateStartWork.Visibility = Visibility.Collapsed;
            }
            return allow;

        }
        private bool IsDateInputValid(string input)
        {
            // Use a regular expression to validate the date format "dd/MM/yyyy"
            // Adjust the pattern according to your specific requirements for date format
            string pattern = @"^\d{1,2}/\d{1,2}/\d{4}$";
            if (!Regex.IsMatch(input, pattern))
            {
                return false;
            }

            // Check if the entered date is a valid DateTime
            if (!DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out _))
            {
                return false;
            }

            return true;
        }
        #region Clicl Event
        private void bodLuuThongTinNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ValidateAddSaff())
            {
                UpdateUser();
            }
        }

        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Hover Event
        private void bodQuayLai_MouseEnter(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#4C5DB4");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodQuayLai_MouseLeave(object sender, MouseEventArgs e)
        {
            bodQuayLai.Background = (Brush)br.ConvertFrom("#FFFFFF");
            tb_QuayLai.Foreground = (Brush)br.ConvertFrom("#4C5DB4");
        }

        private void bodLuuThongTinNhanVien_MouseEnter(object sender, MouseEventArgs e)
        {
            bodLuuThongTinNhanVien.BorderThickness = new Thickness(1);
        }

        private void bodLuuThongTinNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            bodLuuThongTinNhanVien.BorderThickness = new Thickness(0);
        }
        #endregion

        #region CallApi
        private async void UpdateUser()
        {
            try
            {
                string phoneTK = textPhoneTaiKhoan.Text;
                string userName = textNhapHoTen.Text;
                string phone = textSoDT.Text;

                string address = textDiaChi2.Text;
                int gender = int.Parse(cboGioiTinh.SelectedValue.ToString());
                DateTime birthday = dpBirthDay.SelectedDate.Value;
                long ep_birth_day = (long)(birthday - new DateTime(1970, 1, 1)).TotalMilliseconds;

                List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailId = (cboTenToChuc.SelectedItem as ListOrganizeEntities.OrganizeData).listOrganizeDetailId;
                int organizeDetailId = (int)(cboTenToChuc.SelectedItem as ListOrganizeEntities.OrganizeData).id;
                int position_id = (int)cboViTri.SelectedValue;
                int education = int.Parse(cboEducation.SelectedValue.ToString());
                int married = int.Parse(cboMarried.SelectedValue.ToString());
                int experience = int.Parse(cboExperience.SelectedValue.ToString());
                DateTime startWorkingTime = dpStartWork.SelectedDate.Value;

                long start_working_time = (long)(startWorkingTime - new DateTime(1970, 1, 1)).TotalMilliseconds;

                var bodyObject = new
                {
                    ep_id = int.Parse(textEmId.Text),
                    ep_name = userName,
                    ep_phone = phone,
                    phone = phone,
                    address = address,
                    ep_gender = gender,
                    ep_birth_day = ep_birth_day,
                    dep_id = organizeDetailId,
                    position_id = position_id,
                    education = education,
                    married = married,
                    experience = experience,
                    start_working_time = start_working_time,
                    listOrganizeDetailId = listOrganizeDetailId,
                    organizeDetailId = organizeDetailId,
                };

                string bodyJson = JsonConvert.SerializeObject(bodyObject);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/edit");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent(bodyJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root result = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {
                    await LuanChuyenPhongBan();
                    ucInstallAddNewStaff qlnv = new ucInstallAddNewStaff(Main);
                    Main.dopBody.Children.Clear();
                    object Content = qlnv.Content;
                    qlnv.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                    this.Visibility = Visibility.Collapsed;
                    Main.grShowPopup.Children.Add(new ucPopupSuccess(result.data.message));
                }
                else
                {
                    Main.grShowPopup.Children.Add(new ucPopupError(result.error.message));
                }

            }
            catch (Exception ex) { Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra")); }
        }

        public async Task GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.listAll_organize);

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);

                    if (result.data.data != null)
                    {
                        lstOrganizeData = result.data.data;
                        cboTenToChuc.ItemsSource = lstOrganizeData;
                    }
                }
            }
            catch
            {

            }
        }
        private async Task LuanChuyenPhongBan()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/changeOrganizeDetail");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var selectedNv = cboTenToChuc.SelectedItem as ListOrganizeEntities.OrganizeData;

                var bodyObject = new
                {
                    ep_id = int.Parse(textEmId.Text),
                    organizeDetailId = selectedNv.id,
                    listOrganizeDetailId = selectedNv.listOrganizeDetailId

                };
                string bodyJson = JsonConvert.SerializeObject(bodyObject, Formatting.Indented);
                var content = new StringContent(bodyJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();


            }
            catch { }
        }
        private async Task GetListPosition()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.list_position);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListPositionEntities.Root result = JsonConvert.DeserializeObject<ListPositionEntities.Root>(responseContent);
                    if (result.data.data != null)
                    {
                        lstPositionData = result.data.data;
                        cboViTri.ItemsSource = lstPositionData;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task<string> GetPhoneTKEmployee(int ep_id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/employee/info");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ep_id.ToString()), "idQLC");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    EmployeeInfo.Root result = JsonConvert.DeserializeObject<EmployeeInfo.Root>(responseContent);
                    return result.data.data.phoneTK;
                }

            }
            catch { };
            return "";
        }
        #endregion



        private void cboTenToChuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboTenToChuc.SelectedIndex = -1;
                string textSearch = cboTenToChuc.Text;
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cboTenToChuc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboTenToChuc.SelectedIndex = -1;
            string textSearch = cboTenToChuc.Text + e.Text;
            cboTenToChuc.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboTenToChuc.Text = "";
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData;
                cboTenToChuc.SelectedIndex = -1;
            }
            else
            {
                cboTenToChuc.ItemsSource = "";
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }

        }
        private void cboViTri_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboViTri.SelectedIndex = -1;
                string textSearch = cboViTri.Text;
                cboViTri.Items.Refresh();
                cboViTri.ItemsSource = lstPositionData.Where(t => t.positionName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cboViTri_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboViTri.SelectedIndex = -1;
            string textSearch = cboViTri.Text + e.Text;
            cboViTri.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboViTri.Text = "";
                cboViTri.Items.Refresh();
                cboViTri.ItemsSource = lstPositionData;
                cboViTri.SelectedIndex = -1;
            }
            else
            {
                cboViTri.ItemsSource = "";
                cboViTri.Items.Refresh();
                cboViTri.ItemsSource = lstPositionData.Where(t => t.positionName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cboGioiTinh_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cboGioiTinh_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
        private void cboEducation_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboEducation.SelectedIndex = -1;
                string textSearch = cboEducation.Text;
                cboEducation.Items.Refresh();
                cboEducation.ItemsSource = ListItemComboboxUser.ListCbxEducationEmployee.Where(t => t.value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
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
                cboEducation.ItemsSource = ListItemComboboxUser.ListCbxEducationEmployee.Where(t => t.value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }

        }

        private void cboExperience_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboExperience.SelectedIndex = -1;
                string textSearch = cboExperience.Text;
                cboExperience.Items.Refresh();
                cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee.Where(t => t.value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cboExperience_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboExperience.SelectedIndex = -1;
            string textSearch = cboExperience.Text + e.Text;
            cboExperience.IsDropDownOpen = true;
            if (textSearch == "")
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
                cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee.Where(t => t.value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }

        }

        private void cboMarried_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cboMarried_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
