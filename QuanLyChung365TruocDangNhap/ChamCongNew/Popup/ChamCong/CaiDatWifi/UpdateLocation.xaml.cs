using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ViTri;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ViTri;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Them_Xoa_NhanVien;
using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Device.Location;
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
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew
{
    /// <summary>
    /// Interaction logic for TestMap.xaml
    /// </summary>
    public partial class ucUpdateLocation : UserControl
    {
        string BingMapsApiKey = "C3OmWtJbGaUQT7vF8qKX~4DkIaQRhigyTcZiMI8Fkhg~AkWTDsz3PUa_eD4ca72ykDkd3QczE8qJFMGssoIWWpYUca5HTUtCoV3GLCA3zfhw";
        MainWindow Main;
        ucDanhSachViTri ucDanhSachViTri;
        int cor_id;
        public ucUpdateLocation(MainWindow main, ucDanhSachViTri ucDanhSachViTri, API_Location.Location location)
        {
            InitializeComponent();
            Main = main;
            this.ucDanhSachViTri = ucDanhSachViTri;
            cor_id = location.cor_id.Value;
            cor_location_name.Text = location.cor_location_name;
            cor_radius.Text = location.cor_radius.ToString();
            cor_lat.Text = location.cor_lat.ToString();
            cor_long.Text = location.cor_long.ToString();

            borChooseLocaion.Visibility = Visibility.Visible;
            //GeoCoordinate currentLocation = GetCurrentLocation();
        }

        #region CallAPI
        private async Task<string> GetLocationNameAsync(Location location)
        {
            using (HttpClient client = new HttpClient())
            {
                string apiUrl = $"http://dev.virtualearth.net/REST/v1/Locations/{location.Latitude},{location.Longitude}?key={BingMapsApiKey}";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(apiUrl);

                    if (response.IsSuccessStatusCode)
                    {
                        var responseContent = await response.Content.ReadAsStringAsync();
                        BingMapAPI.Root MapRoot = JsonConvert.DeserializeObject<BingMapAPI.Root>(responseContent);
                        // You need to parse the responseContent to get the location name or address.
                        // For brevity, this example does not include parsing logic.
                        return MapRoot.resourceSets[0].resources[0].name;
                    }
                    else
                    {
                        return "Address not found";
                    }
                }
                catch (Exception ex)
                {
                    return "Address not found";
                }
            }
        }

        private async void UpdateLocation()
        {
            try
            {
                var bodyObject = new
                {
                    cor_id = cor_id,
                    cor_location_name = cor_location_name.Text,
                    cor_radius = double.Parse(cor_radius.Text),
                    cor_lat = double.Parse(cor_lat.Text),
                    cor_long = double.Parse(cor_long.Text)


                };
                string bodyJson = JsonConvert.SerializeObject(bodyObject);


                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/location/add");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent(bodyJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root result = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {

                    this.Visibility = Visibility.Collapsed;
                    ucDanhSachViTri.GetListLocation();
                    Main.grShowPopup.Children.Add(new ucPopupSuccess(result.data.message));
                }
                else
                {
                    Main.grShowPopup.Children.Add(new ucPopupError(result.error.message));
                }

            }
            catch (Exception ex) { }
        }
        #endregion
        private async void Map_MouseClick(object sender, MouseButtonEventArgs e)
        {
            borChooseLocaion.Visibility = Visibility.Visible;
            e.Handled = true;
            var mouseClickPoint = e.GetPosition((IInputElement)sender);
            Location location = new Location();
            location = ((Map)sender).ViewportPointToLocation(mouseClickPoint);
            cor_lat.Text = location.Latitude.ToString();
            cor_long.Text = location.Longitude.ToString();
            string address = await GetLocationNameAsync(location);
            cor_location_name.Text = address;
            // Use the 'location' object to get the latitude and longitude of the selected point.
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void AddLoctaion(object sender, MouseButtonEventArgs e)
        {
            UpdateLocation();
        }
    }
}
