using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.APIs;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.Hr.Entities.ListItemCombobox;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Them_Xoa_NhanVien;
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

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupQuanLyNhanVien
{
    /// <summary>
    /// Interaction logic for ucThemMoiNhanVien.xaml
    /// </summary>
    public partial class ucThemMoiNhanVien : UserControl
    {
        frmMain Main;
        ucTatCaNhanVien ucTatCaNhanVien;
        BrushConverter br = new BrushConverter();

        public List<ListPositionEntities.PositionData> lstPositionData;
        public List<ListOrganizeEntities.OrganizeData> lstOrganizeData;

        List<API_List_District.City> listCities = new List<API_List_District.City>();
        List<API_List_District.District> listDistricts = new List<API_List_District.District>();
        List<API_List_District.District> listDistrictsByCity = new List<API_List_District.District>();
        public ucThemMoiNhanVien(frmMain Main, ucTatCaNhanVien ucTatCaNhanVien)
        {
            InitializeComponent();
            this.Main = Main;
            this.ucTatCaNhanVien = ucTatCaNhanVien;
            cboEducation.ItemsSource = ListItemComboboxUser.ListCbxEducationEmployee;
            cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee;
            cboMarried.ItemsSource = ListItemComboboxUser.ListCbxMarriedEmployee;
            cboGioiTinh.ItemsSource = ListItemComboboxUser.ListCbxGenderEmployee;
            GetListCity();
            GetListOrganize();
            GetListPosition();
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
            if (cboTinhThanh.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateTinhThanh.Visibility = Visibility.Visible;
                tb_ValidateTinhThanh.Text = "không được để trống";
            }
            else
            {
                tb_ValidateTinhThanh.Visibility = Visibility.Collapsed;
            }
            if (cboQuanHuyen.SelectedIndex == -1)
            {
                allow = false;
                tb_ValidateQuanHuyen.Visibility = Visibility.Visible;
                tb_ValidateQuanHuyen.Text = "không được để trống";
            }
            else
            {
                tb_ValidateQuanHuyen.Visibility = Visibility.Collapsed;
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
            if (dpBirthDay.SelectedDate == null)
            {
                allow = false;
                tb_ValidateNgaySinh.Visibility = Visibility.Visible;
                tb_ValidateNgaySinh.Text = "không được để trống";
            }
            else
            {
                tb_ValidateNgaySinh.Visibility = Visibility.Collapsed;
            }
            return allow;

        }

        #region Clicl Event
        private void bodLuuThongTinNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (ValidateAddSaff())
            {
                CreateUer();
            }
        }

        private void bodQuayLai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ucQuanLyNhanVien qlnv = new ucQuanLyNhanVien(Main);
            Main.stp_ShowPopup.Children.Clear();
            object Content = qlnv.Content;
            qlnv.Content = null;
            Main.stp_ShowPopup.Children.Add(Content as UIElement);
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
        private async void CreateUer()
        {
            try
            {
                string phoneTK = textPhoneTaiKhoan.Text;
                string userName = textNhapHoTen.Text;
                string phone = phoneTK;
                int city_id = (int)cboTinhThanh.SelectedValue;
                int district_id = (int)cboQuanHuyen.SelectedValue;
                string address = textDiaChi2.Text;
                int gender = int.Parse(cboGioiTinh.SelectedValue.ToString());
                string birthday = dpBirthDay.SelectedDate?.ToString("yyyy-MM-dd");
                List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailId = (cboTenToChuc.SelectedItem as ListOrganizeEntities.OrganizeData).listOrganizeDetailId;
                int organizeDetailId = (int)(cboTenToChuc.SelectedItem as ListOrganizeEntities.OrganizeData).id;
                int position_id = (int)cboViTri.SelectedValue;
                int education = int.Parse(cboEducation.SelectedValue.ToString());
                int married = int.Parse(cboMarried.SelectedValue.ToString());
                int experience = int.Parse(cboExperience.SelectedValue.ToString());

                var bodyObject = new
                {
                    phoneTK = phoneTK,
                    userName = userName,
                    phone = phone,
                    city_id = city_id,
                    district_id = district_id,
                    address = address,
                    gender = gender,
                    birthday = birthday,
                    listOrganizeDetailId = listOrganizeDetailId,
                    organizeDetailId = organizeDetailId,
                    position_id = position_id,
                    education = education,
                    married = married,
                    experience = experience
                };

                string bodyJson = JsonConvert.SerializeObject(bodyObject);
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/createUserNew");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent(bodyJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    ucQuanLyNhanVien qlnv = new ucQuanLyNhanVien(Main);
                    Main.stp_ShowPopup.Children.Clear();
                    object Content = qlnv.Content;
                    qlnv.Content = null;
                    Main.stp_ShowPopup.Children.Add(Content as UIElement);
                }
                else
                {
                    CustomMessageBox.Show("Tài khoản đã tồn tại");
                }

            }
            catch (Exception ex) { CustomMessageBox.Show("Có lỗi xảy ra"); }
        }
        private async void GetListCity()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, Api_ThietLapCongTy.Api_get_district);
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                API_List_District.Root result = JsonConvert.DeserializeObject<API_List_District.Root>(responseContent);
                listCities = result.city;
                listDistricts = result.district;
                cboTinhThanh.ItemsSource = listCities;
            }

        }
        public async void GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.listAll_organize);

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount), "com_id");
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
        private async void GetListPosition()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.list_position);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount), "com_id");
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
                CustomMessageBox.Show(ex.Message);
            }
        }

        #endregion


        private void cboTinhThanh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var city = cboTinhThanh.SelectedItem as API_List_District.City;
            if (city != null)
            {

                cboQuanHuyen.ItemsSource = listDistricts.Where(x => x.cit_id == city.cit_id).ToList();
            }
        }
        private void cboTenToChuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboTenToChuc.SelectedIndex = -1;
                string textSearch = cboTenToChuc.Text;
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.ToLower()));
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
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.ToLower()));
            }

        }
        private void cboViTri_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboViTri.SelectedIndex = -1;
                string textSearch = cboViTri.Text;
                cboViTri.Items.Refresh();
                cboViTri.ItemsSource = lstPositionData.Where(t => t.positionName.ToLower().Contains(textSearch.ToLower()));
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
                cboViTri.ItemsSource = lstPositionData.Where(t => t.positionName.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cboGioiTinh_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cboGioiTinh_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }

        private void cboQuanHuyen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboQuanHuyen.SelectedIndex = -1;
                string textSearch = cboQuanHuyen.Text;
                cboQuanHuyen.Items.Refresh();
                cboQuanHuyen.ItemsSource = listDistrictsByCity.Where(t => t.cit_name.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cboQuanHuyen_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboQuanHuyen.SelectedIndex = -1;
            string textSearch = cboQuanHuyen.Text + e.Text;
            cboQuanHuyen.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboQuanHuyen.Text = "";
                cboQuanHuyen.Items.Refresh();
                cboQuanHuyen.ItemsSource = listDistrictsByCity;
                cboQuanHuyen.SelectedIndex = -1;
            }
            else
            {
                cboQuanHuyen.ItemsSource = "";
                cboQuanHuyen.Items.Refresh();
                cboQuanHuyen.ItemsSource = listDistrictsByCity.Where(t => t.cit_name.ToLower().Contains(textSearch.ToLower()));
            }

        }

        private void cboTinhThanh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboTinhThanh.SelectedIndex = -1;
                string textSearch = cboTinhThanh.Text;
                cboTinhThanh.Items.Refresh();
                cboTinhThanh.ItemsSource = listDistrictsByCity.Where(t => t.cit_name.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cboTinhThanh_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboTinhThanh.SelectedIndex = -1;
            string textSearch = cboTinhThanh.Text + e.Text;
            cboTinhThanh.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboTinhThanh.Text = "";
                cboTinhThanh.Items.Refresh();
                cboTinhThanh.ItemsSource = listCities;
                cboTinhThanh.SelectedIndex = -1;
            }
            else
            {
                cboTinhThanh.ItemsSource = "";
                cboTinhThanh.Items.Refresh();
                cboTinhThanh.ItemsSource = listCities.Where(t => t.cit_name.ToLower().Contains(textSearch.ToLower()));
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
                string textSearch = cboExperience.Text;
                cboExperience.Items.Refresh();
                cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee.Where(t => t.value.ToLower().Contains(textSearch.ToLower()));
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
                cboExperience.ItemsSource = ListItemComboboxUser.ListCbxExpEmployee.Where(t => t.value.ToLower().Contains(textSearch.ToLower()));
            }

        }

        private void cboTinhThanh_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                var selectedCity = cboTinhThanh.SelectedItem as API_List_District.City;
                if (selectedCity != null)
                {
                    listDistrictsByCity = listDistricts.Where(x => x.cit_parent == selectedCity.cit_id).ToList();
                    cboQuanHuyen.ItemsSource = listDistrictsByCity;
                }
            }
            catch
            {

            }
        }



        private void cboMarried_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void cboMarried_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {

        }
    }
}
