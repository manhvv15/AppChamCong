using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
//using DocumentFormat.OpenXml.Drawing;
using Newtonsoft.Json;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.LoaiHinhChamCong
{
    /// <summary>
    /// Interaction logic for ucLoaiHinhChamCong.xaml
    /// </summary>
    public partial class ucLoaiHinhChamCong : UserControl
    {
        public ucLoaiHinhChamCong()
        {
            InitializeComponent();
            GetTypeTimeSheet();
        }


        private async void GetTypeTimeSheet()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingTS/get_type_timesheet");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TypeTimeSheetEntities.Root result = JsonConvert.DeserializeObject<TypeTimeSheetEntities.Root>(responseContent);

                    gridLoaiHinhChamCong.DataContext = result.data.data.type_timesheet;
                }
            }
            catch
            {

            }
        }

        private async void UpdateTypeTimeSheet(int type)
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/settingTS/update_type_timesheet");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var json = JsonConvert.SerializeObject(new { type_timesheet = type });   
                var content = new StringContent(json, null, "application/json");
                request.Content= content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    GetTypeTimeSheet();
                }
                else
                {
                    CustomMessageBox.Show("Có lỗi xảy ra");
                }
            }
            catch
            {

            }
        }
        private void BackOffTimeKeepWithCanlendar_Shift(object sender, MouseButtonEventArgs e)
        {
            UpdateTypeTimeSheet(1);
        }

        private void BackOnTimeKeepWithCanlendar_Shift(object sender, MouseButtonEventArgs e)
        {
            if (dopOffTimeKeepWithCanlendar_Shift.Visibility == Visibility.Collapsed)
            {
                UpdateTypeTimeSheet(3);
                return;
            }
            UpdateTypeTimeSheet(2);
        }

        private void BackOffTimeKeepWithoutCanlendar_Shift(object sender, MouseButtonEventArgs e)
        {
            UpdateTypeTimeSheet(2);
        }

        private void BackOnTimeKeepWithoutCanlendar_Shift(object sender, MouseButtonEventArgs e)
        {
            if(dopOffTimeKeepWithoutCanlendar_Shift.Visibility == Visibility.Collapsed) {
            UpdateTypeTimeSheet(3);
                return;
            }
            UpdateTypeTimeSheet(1);
        }
    }
    
}
