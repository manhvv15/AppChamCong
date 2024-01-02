using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.RecommendSetting.OOP;
using QuanLyChung365TruocDangNhap.RecommendSetting.Popup;
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


namespace QuanLyChung365TruocDangNhap.RecommendSetting
{
    /// <summary>
    /// Interaction logic for ucCompanyRotation.xaml
    /// </summary>
    public partial class ucCompanyRotation : UserControl
    {
        frmMain Main;
        public ucCompanyRotation(frmMain main)
        {
            InitializeComponent();
            Main = main;
            getDS_NV_company();
        }
        private async void getDS_NV_company(int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/managerUser/listUser");
                request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjp7Il9pZCI6MTM3NzQ0OSwiaWRUaW1WaWVjMzY1IjoyMzI0MTYsImlkUUxDIjoxNjY0LCJpZFJhb05oYW5oMzY1IjowLCJlbWFpbCI6InRyYW5nY2h1b2k0QGdtYWlsLmNvbSIsInBob25lVEsiOiJ0cmFuZ2NodW9pNEBnbWFpbC5jb20iLCJjcmVhdGVkQXQiOjE2NjM4MzY0MDUsInR5cGUiOjEsImNvbV9pZCI6MTY2NCwidXNlck5hbWUiOiJDw7RuZyB0eSBj4buVIHBo4bqnbiB4deG6pXQgbmjhuq1wIGto4bqpdSAxMjMifSwiaWF0IjoxNjk3NTA3MjQ0LCJleHAiOjE2OTc1OTM2NDR9.nH9TyVbYI3BNIITcf9LHyWpUmJ5Hk1SHMMKMhiah26U");
                var content = new StringContent("", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_DS_NV_Company.DS_NV_Compay api = JsonConvert.DeserializeObject<API_DS_NV_Company.DS_NV_Compay>(responseContent);
                    if (api.data.data != null)
                    {
                        int STT = 1;
                        var list = api.data.data.Skip((pageNumber - 1) * 10).Take(10);
                        var fullList = (from item in list
                                        select new API_DS_NV_Company.NV_Company_Infor
                                        {
                                            STT = STT++,
                                            userName = item.userName,
                                            ep_id = item.ep_id,
                                            organizeDetailName = item.organizeDetailName == "" ? "Chưa cập nhật" : item.organizeDetailName,
                                            positionName = (item.positionName == "") ? "Chưa cập nhật" : item.positionName,
                                            phone = item.phone == null ? "Chưa cập nhật" : item.phone

                                        }).ToList();
                        if (paginNV.SelectedPage == 0) paginNV.TotalRecords = (int)api.data.total;

                        dgvNV.ItemsSource = fullList;
                        //dgvNV.ItemsSource = api.data.data;
                    }
                }

            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("loi ds nhan vien company" + ex.Message);
            }
        }
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            getDS_NV_company(paginNV.SelectedPage);

        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            transferCompany.Visibility = Visibility.Collapsed;
        }

        private void Path_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            transferCompany.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            transferCompany.Visibility = Visibility.Collapsed;
        }

        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/managerUser/changeCompany");
                request.Headers.Add("Authorization", "Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJkYXRhIjp7Il9pZCI6MTM3NzQ0OSwiaWRUaW1WaWVjMzY1IjoyMzI0MTYsImlkUUxDIjoxNjY0LCJpZFJhb05oYW5oMzY1IjowLCJlbWFpbCI6InRyYW5nY2h1b2k0QGdtYWlsLmNvbSIsInBob25lVEsiOiJ0cmFuZ2NodW9pNEBnbWFpbC5jb20iLCJjcmVhdGVkQXQiOjE2NjM4MzY0MDUsInR5cGUiOjEsImNvbV9pZCI6MTY2NCwidXNlck5hbWUiOiJDw7RuZyB0eSBj4buVIHBo4bqnbiB4deG6pXQgbmjhuq1wIGto4bqpdSAxMjMifSwiaWF0IjoxNjk3NTA3MjQ0LCJleHAiOjE2OTc1OTM2NDR9.nH9TyVbYI3BNIITcf9LHyWpUmJ5Hk1SHMMKMhiah26U");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ep_id.ToString()), "ep_id");
                content.Add(new StringContent(textNhap.Text), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                // Console.WriteLine(await response.Content.ReadAsStringAsync());
                if (response.IsSuccessStatusCode)
                {
                    getDS_NV_company();
                    transferCompany.Visibility = Visibility.Collapsed;
                    Update uc = new Update(Main);
                    Main.pnlShowPopUp.Children.Add(uc);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.pnlShowPopUp.Children.Add(Content as UIElement);
                }
                else
                {
                   
                    CustomMessageBox.Show("Người dùng không tồn tại");
                   
                }


            }
            catch (Exception ex)
            {
                transferCompany.Visibility = Visibility.Collapsed;
                CustomMessageBox.Show("Người dùng không tồn tại");
            }
        }
        int ep_id;
        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            ep_id = (int)((API_DS_NV_Company.NV_Company_Infor)dgvNV.SelectedItem).ep_id;
            transferCompany.Visibility = Visibility.Visible;
        }
        private void getDsToChuc()
        {

        }
    }
}
