using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.Login;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
//using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ViTri.API_Location;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucListWifi.xaml
    /// </summary>
    public partial class ucDanhSachWifi : UserControl
    {
        private MainWindow Main;
        string ip_address = "192.168.0.1";

        BrushConverter bcWifi = new BrushConverter();
        private List<ItemWifi> _lstWifi;

        public List<ItemWifi> LstWifi
        {
            get { return _lstWifi; }
            set { _lstWifi = value; }
        }


        public ucDanhSachWifi(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadListWifi();

        }

        #region CallApi
        public async void LoadListWifi()
        {
            try
            {
                var listLocation = await GetListLocation();
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.list_wifi_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                RootWifi loadListWifi = JsonConvert.DeserializeObject<RootWifi>(responseContent);

                if (loadListWifi.data.data != null)
                {

                    LstWifi = (from item in loadListWifi.data.data
                               select new ItemWifi
                               {
                                   _id = item._id,
                                   id = item.id,
                                   id_com = item.id_com,
                                   ip_access = (item.ip_access == "") ? "Chưa cập nhật" : item.ip_access,
                                   name_wifi = (item.name_wifi == "") ? "Chưa cập nhật" : item.name_wifi,
                                   id_loc = item.id_loc,
                                   location = (listLocation.Where(x => x.cor_id == item.id_loc).FirstOrDefault()?.cor_location_name == "") ? "Chưa cập nhật" : listLocation.Where(x => x.cor_id == item.id_loc).FirstOrDefault()?.cor_location_name,
                               }).ToList();

                    lsvLoadWifi.ItemsSource = LstWifi;

                }
            }
            catch (Exception)
            {
            }
        }
        public async Task<List<Location>> GetListLocation()
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
                    return listLocation;
                }
            }
            catch
            {
            }
            return new List<Location>();

        }
        #endregion

        #region Hover
        private void bodAddWifi_MouseEnter(object sender, MouseEventArgs e)
        {
            bodAddWifi.BorderThickness = new Thickness(1);
        }

        private void bodAddWifi_MouseLeave(object sender, MouseEventArgs e)
        {
            bodAddWifi.BorderThickness = new Thickness(0);
        }
        #endregion



        private void MouseLeftButtonUp_updateWifi(object sender, MouseButtonEventArgs e)
        {
            ItemWifi itemWifi = (sender as Border).DataContext as ItemWifi;
            if (itemWifi != null)
            {
                Main.grShowPopup.Children.Add(new ucCapNhatWifi(Main, itemWifi, this));

            }

        }

        private void Border_MouseLeftButtonUp_DeleteWifi(object sender, MouseButtonEventArgs e)
        {
            ItemWifi itemWifi = (sender as Border).DataContext as ItemWifi;
            if (itemWifi != null)
            {
                Main.grShowPopup.Children.Add(new ucXoaWifi(itemWifi, this, Main));
            }
        }

        private void bodAddWifi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ItemWifi itemWifi = new ItemWifi();
            if (itemWifi != null)
            {
                Main.grShowPopup.Children.Add(new ucThemMoiWifi(Main, itemWifi, this));
            }


        }

        private void lsvLoadWifi_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
    }
}
