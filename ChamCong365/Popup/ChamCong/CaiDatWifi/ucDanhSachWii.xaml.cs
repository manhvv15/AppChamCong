using ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi;
using ChamCong365.OOP.Login;
using ChamCong365.TimeKeeping;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucListWifi.xaml
    /// </summary>
    public partial class ucDanhSachWii : UserControl
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
       

        public ucDanhSachWii(MainWindow main)
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
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.list_wifi_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();

                RootWifi loadListWifi = JsonConvert.DeserializeObject<RootWifi>(responseContent);

                if (loadListWifi.data.items != null)
                {

                    LstWifi = loadListWifi.data.items;
                    foreach (var item in LstWifi)
                    {
                        if (double.TryParse(item.create_time, out double createdTime))
                        {
                            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                            dateTime = dateTime.AddSeconds(createdTime).ToLocalTime();
                            item.create_time = dateTime.ToString("MM/dd/yyyy hh:mm:ss tt");
                        }
                        else
                        {
                            throw new ArgumentException("Định dạng thời gian không hợp lệ");
                        }

                    }
                    lsvLoadWifi.ItemsSource = LstWifi;

                }
            }
            catch (Exception)
            {
            }
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
                Main.grShowPopup.Children.Add(new ucXoaWifi(itemWifi,this));
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
    }
}
