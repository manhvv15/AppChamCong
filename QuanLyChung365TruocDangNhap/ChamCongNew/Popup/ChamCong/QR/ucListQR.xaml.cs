using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popups.ChamCong.CaiDatQR;
//using DocumentFormat.OpenXml.Office2010.Excel;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.XuatCong.ListHistoryCheckInEntities;
using Employee = QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy.Employee;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.QR
{
    /// <summary>
    /// Interaction logic for ucListQR.xaml
    /// </summary>
    public partial class ucListQR : UserControl
    {
        MainWindow Main;
        List<ListQREntities.QRInfo> ListQR = new List<ListQREntities.QRInfo>();

        public ucListQR(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            GetListQR();

        }
        #region CallApi
        public async void GetListQR(int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/qrCode/listAll");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);

                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ListQREntities.Root result = JsonConvert.DeserializeObject<ListQREntities.Root>(responseContent);
                    if (pagin.SelectedPage == 0) pagin.TotalRecords = result.data.data.Count;
                    ListQR = result.data.data;
                    int stt = (pageNumber - 1) * 10;
                    foreach (var item in ListQR)
                    {
                        item.STT = stt++;
                    }
                    dgvListQR.ItemsSource = ListQR.Skip((pageNumber - 1) * 10).Take(10);


                }
            }
            catch
            {
            }
        }

        public async void DeleteQR(int id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/qrCode/delete");
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new StringContent("{\"id\":" + id + "}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                MessageEntities.Root result = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {

                    Main.grShowPopup.Children.Add(new ucPopupSuccess(result.data.message));
                    GetListQR(pagin.SelectedPage);
                }
                else
                {
                    Main.grShowPopup.Children.Add(new ucPopupError(result.error.message));
                }

            }
            catch (Exception ex) { Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra")); }
        }

        public async void ChangeQRStatus(int id, int QRstatus)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/qrCode/update");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new StringContent("{\"id\":" + id + ",\"QRstatus\":" + QRstatus + "}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);


                var responseContent = await response.Content.ReadAsStringAsync();
                MessageEntities.Root result = JsonConvert.DeserializeObject<MessageEntities.Root>(responseContent);
                if (response.IsSuccessStatusCode)
                {

                    Main.grShowPopup.Children.Add(new ucPopupSuccess(result.data.message));
                    GetListQR(pagin.SelectedPage);
                }
                else
                {
                    Main.grShowPopup.Children.Add(new ucPopupError(result.error.message));
                }

            }
            catch (Exception ex) { Main.grShowPopup.Children.Add(new ucPopupError("Có lỗi xảy ra")); }
        }

        #endregion

        private void pagin_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            GetListQR(pagin.SelectedPage);
        }

        private void btnTimKiem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            GetListQR();
        }

        private void btnXuatExcel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void TurnOffQR(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var textBlock = sender as TextBlock;
                var QRInfo = textBlock.DataContext as ListQREntities.QRInfo;
                if (QRInfo != null)
                {
                    ChangeQRStatus(QRInfo.id, 2);
                }

            }
            catch { }
        }

        private void TurnOnQR(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var textBlock = sender as TextBlock;
                var QRInfo = textBlock.DataContext as ListQREntities.QRInfo;
                if (QRInfo != null)
                {
                    ChangeQRStatus(QRInfo.id, 1);
                }

            }
            catch { }
        }

        private void DeleteQR_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var textBlock = sender as TextBlock;
                var QRInfo = textBlock.DataContext as ListQREntities.QRInfo;
                if (QRInfo != null)
                {
                    DeleteQR(QRInfo.id);
                }

            }
            catch { }
        }

        private void NavigateToAddQR(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiQR(Main, this));
        }

        private void NavigateToUpdateQR(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var textBlock = sender as TextBlock;
                var QRInfo = textBlock.DataContext as ListQREntities.QRInfo;
                if (QRInfo != null)
                {
                    Main.grShowPopup.Children.Add(new ucChinhSuaQR(Main, QRInfo.id, QRInfo.QRCodeName, this));
                }

            }
            catch { }
        }

        private void dgvListQR_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);

        }
    }

}
