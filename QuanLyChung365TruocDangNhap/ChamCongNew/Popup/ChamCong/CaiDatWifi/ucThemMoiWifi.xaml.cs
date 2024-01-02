using System.Net.Http;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Newtonsoft.Json;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ViTri.API_Location;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucCreateWifi.xaml
    /// </summary>
    public partial class ucThemMoiWifi : UserControl
    {
        MainWindow Main;
        private ItemWifi itemWifi;
        private ucDanhSachWifi ucDanhSachWii;
        Dictionary<string, string> listAllLocation = new Dictionary<string, string>();
        public ucThemMoiWifi(MainWindow main, ItemWifi itemWifi, ucDanhSachWifi ucDanhSachWii)
        {
            InitializeComponent();
            this.itemWifi = itemWifi;
            this.ucDanhSachWii = ucDanhSachWii;
            Main = main;
            GetListLocation();
        }
        public async void AddWifi()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.add_wifi_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tb_TenWifi.Text), "name_wifi");
                content.Add(new StringContent(tb_DiaChiMac.Text), "ip_access");
                content.Add(new StringContent(cbxLocation.SelectedValue.ToString()), "id_loc");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responsWifi = await response.Content.ReadAsStringAsync();

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucDanhSachWii.LoadListWifi();
                }
            }
            catch (System.Exception)
            {

            }
        }
        public async void GetListLocation()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/location/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Root result = JsonConvert.DeserializeObject<Root>(responseContent);
                    List<Location> listLocation = result.data.list;
                    foreach (var item in result.data.list)
                    {
                        listAllLocation.Add(item.cor_id.ToString(), item.cor_location_name.ToString());
                        cbxLocation.ItemsSource = listAllLocation;
                    }

                }
            }
            catch
            {
            }
        }
        private bool ValidateAddForm()
        {
            txtValidateNameWifi.Visibility = Visibility.Collapsed;
            txtvalidateAddressMac.Visibility = Visibility.Collapsed;
            txtvalidateLocation.Visibility = Visibility.Collapsed;
            if (String.IsNullOrEmpty(tb_TenWifi.Text))
            {
                txtValidateNameWifi.Text = "Tên wifi không được để trống!" as string;
                txtValidateNameWifi.Visibility = Visibility.Visible;
                return false;
            }

            if (String.IsNullOrEmpty(tb_DiaChiMac.Text))
            {
                txtvalidateAddressMac.Visibility = Visibility.Visible;
                txtvalidateAddressMac.Text = "Địa chỉ ip không được để trống!" as string;
                return false;
            }
            if (cbxLocation.SelectedIndex == -1)
            {
                txtvalidateLocation.Visibility = Visibility.Visible;
                txtvalidateLocation.Text = "Vị trí không được để trống!" as string;
                return false;
            }


            return true;
        }

        private void CreateWifi_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitCreateWifi_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }



        private void bodThemMoiWifi_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (ValidateAddForm())
            {
                AddWifi();
            }

        }
        private void cbxLocation_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxLocation.SelectedIndex = -1;
                string textSearch = cbxLocation.Text;
                cbxLocation.Items.Refresh();
                cbxLocation.ItemsSource = listAllLocation.Where(t => t.Value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cbxLocation_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxLocation.SelectedIndex = -1;
            string textSearch = cbxLocation.Text + e.Text;
            cbxLocation.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxLocation.Text = "";
                cbxLocation.Items.Refresh();
                cbxLocation.ItemsSource = listAllLocation;
                cbxLocation.SelectedIndex = -1;
            }
            else
            {
                cbxLocation.ItemsSource = "";
                cbxLocation.Items.Refresh();
                cbxLocation.ItemsSource = listAllLocation.Where(t => t.Value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }


    }
}
