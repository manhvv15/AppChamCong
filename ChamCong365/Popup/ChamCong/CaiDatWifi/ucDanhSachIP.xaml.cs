using ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucListIP.xaml
    /// </summary>
    public partial class ucDanhSachIP : UserControl
    {
         MainWindow Main;
        int Idcom;
        BrushConverter bcWifi = new BrushConverter();
        
        private List<ListIP> _listIP;
        public List<ListIP> listIP
        {
            get { return _listIP; }
            set { _listIP = value; }
        }

       
        public ucDanhSachIP(MainWindow main, int com_id)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.Idcom = com_id;
            LoadListIP();
          
            
           
        }
        public async void LoadListIP()
        {
           
            try
            {
                //Lỗi
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.list_ip_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Idcom.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responsIp = await response.Content.ReadAsStringAsync();
                RootIp result = JsonConvert.DeserializeObject<RootIp>(responsIp);
                if (result.data.data != null)
                {
                    listIP = result.data.data;
                    foreach (var item in listIP)
                    {
                        if (double.TryParse(item.created_time, out double createdTime))
                        {
                            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                            dateTime = dateTime.AddSeconds(createdTime).ToLocalTime();
                            item.created_time = dateTime.ToString("MM/dd/yyyy hh:mm:ss tt");
                        }
                        else
                        {
                            throw new ArgumentException("Định dạng thời gian không hợp lệ");
                        }

                    }
                    lsvLoadIP.ItemsSource = listIP;
                }
                   
                
            }
            catch (Exception)
            {


            }
        }    
        

        private void borAddIp_MouseEnter(object sender, MouseEventArgs e)
        {
            borAddIp.BorderThickness = new Thickness(1);
        }

        private void borAddIp_MouseLeave(object sender, MouseEventArgs e)
        {
            borAddIp.BorderThickness = new Thickness(0);
        }

        private void borAddIp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListIP listIP = new ListIP();
            if (listIP != null)
            {
                Main.grShowPopup.Children.Add(new ucThemDiaChiIP(Main,listIP,this));
            }
            
        }

        private void Border_MouseLeftButtonUp_UpdateIP(object sender, MouseButtonEventArgs e)
        {
          ListIP listIP = (sender as Border).DataContext as ListIP;
            if (listIP != null)
            {
                Main.grShowPopup.Children.Add(new ucCapNhatIP(Main,listIP,this));
            }
           
        }

        private void Border_MouseLeftButtonUp_DeleteIP(object sender, MouseButtonEventArgs e)
        {
            ListIP listIP = (sender as Border).DataContext as ListIP;
            if (listIP != null)
            {
                Main.grShowPopup.Children.Add(new ucXoaIP(listIP, this));
            }
            
        }
    }
}
