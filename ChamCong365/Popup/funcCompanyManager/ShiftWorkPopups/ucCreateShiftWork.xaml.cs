using ChamCong365.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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

namespace ChamCong365.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucCreateShiftWork.xaml
    /// </summary>
    public partial class ucCreateShiftWork : UserControl
    {

        ucShiftWorkManager ucShiftWorkManager;
        BrushConverter bc = new BrushConverter();
        List<double> dsCongTrenCa = new List<double>() { 0.25, 0.5, 0.75, 1 };

        private string com_id = "";
        private string shift_type = "1";
        public ucCreateShiftWork(ucShiftWorkManager ucShiftWorkManager, string com_id)
        {
            InitializeComponent();

            lsvCongTrenCa.ItemsSource = dsCongTrenCa;
            this.ucShiftWorkManager = ucShiftWorkManager;
            this.com_id = com_id;
        }
        //Xử lý button thêm ca làm việc

        //Xử lý btn exit thêm ca
        private void btnExit_ThemCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            this.Visibility = Visibility.Collapsed;

        }

        //xử lý khi ấn "cài đặt giới hạn thời gian" trong màn hình thêm mới ca
        private void wraplimitTimeSetting_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (warpLimitTimeSettingZone.Visibility == Visibility.Collapsed)
            {
                warpLimitTimeSettingZone.Visibility = Visibility.Visible;
                bodCongTrenCa.Margin = new Thickness(-2, 714, 0, 0);

            }
            else
            {
                warpLimitTimeSettingZone.Visibility -= Visibility.Collapsed;
                bodCongTrenCa.Margin = new Thickness(-2, 550, 0, 0);

            }
        }
        //xử lý chọn công tính ca
        private void bodBtnTinhTheoSoCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            shift_type = "1";
            bodBtnTinhTheoTien.Background = Brushes.White;
            bodBtnTinhTheoGio.Background = Brushes.White;
            txbTinhTheoTien.Foreground = Brushes.DarkGray;
            txbTinhTheoGio.Foreground = Brushes.DarkGray;
            bodBtnTinhTheoSoCa.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            txbTinhTheoSoCa.Foreground = Brushes.White;
            warpSoCongTuongUng.Visibility = Visibility.Visible;
            wrapSoTienTuongUong.Visibility = Visibility.Collapsed;
            warpInOutTime.Visibility = Visibility.Visible;



        }
        private void bodBtnTinhTheoTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            shift_type = "2";

            bodBtnTinhTheoSoCa.Background = Brushes.White;
            bodBtnTinhTheoGio.Background = Brushes.White;
            txbTinhTheoSoCa.Foreground = Brushes.DarkGray;
            txbTinhTheoGio.Foreground = Brushes.DarkGray;
            bodBtnTinhTheoTien.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            txbTinhTheoSoCa.Foreground = Brushes.DarkGray;
            txbTinhTheoTien.Foreground = Brushes.White;
            warpSoCongTuongUng.Visibility = Visibility.Collapsed;
            wrapSoTienTuongUong.Visibility = Visibility.Visible;
            warpInOutTime.Visibility = Visibility.Visible;



        }
        private void bodBtnTinhTheoGio_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            shift_type = "3";

            bodBtnTinhTheoTien.Background = Brushes.White;
            bodBtnTinhTheoSoCa.Background = Brushes.White;
            txbTinhTheoTien.Foreground = Brushes.DarkGray;
            txbTinhTheoSoCa.Foreground = Brushes.DarkGray;
            bodBtnTinhTheoGio.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            txbTinhTheoSoCa.Foreground = Brushes.DarkGray;
            txbTinhTheoGio.Foreground = Brushes.White;
            warpSoCongTuongUng.Visibility = Visibility.Collapsed;
            wrapSoTienTuongUong.Visibility = Visibility.Collapsed;
            warpInOutTime.Visibility = Visibility.Collapsed;




        }
        //Xử lý chọn dropdownbox số công tương ứng
        private void Image_MouseLeftButtonUp_ChonCa(object sender, MouseButtonEventArgs e)
        {
            if (bodCongTrenCa.Visibility == Visibility.Collapsed)
            {
                bodCongTrenCa.Visibility = Visibility.Visible;
            }
            else
            {
                bodCongTrenCa.Visibility -= Visibility.Collapsed;

            }
        }
        //Xử lý khi ấn chọn công ty
        private void lsvCongTrenCa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbChonCa.Text = lsvCongTrenCa.SelectedItem.ToString();
            bodCongTrenCa.Visibility = Visibility.Collapsed;

        }

        //Xử lý khi ấn nút thêm
        private void bodBtnThemCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            AddNewShift();

        }
        //Xử lý khi ấn ok sau khi ấn thêm
        private void bodThemThanhCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        #region SelectTimeCheckIn    
        private void bodSelectTimeCheckIn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListTimeCheckInCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListTimeCheckInCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListTimeCheckInCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvTimeCheckIn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectTimeCheckIn.Text = tpTimeCheckIn.Text;
            txbSelectTimeCheckIn.Foreground = (Brush)bc.ConvertFromString("#474747");

            bodListTimeCheckInCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectTimeCheckOut
        private void bodSelectTimeCheckOut_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListTimeCheckOutCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListTimeCheckOutCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListTimeCheckOutCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvTimeCheckOut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectTimeCheckOut.Text = tpTimeCheckOut.Text;
            txbSelectTimeCheckOut.Foreground = (Brush)bc.ConvertFromString("#474747");

            bodListTimeCheckOutCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectCheckInEarliest 
        private void bodSelectCheckInEarliest_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCheckInEarliestCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListCheckInEarliestCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCheckInEarliestCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCheckInEarliest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectCheckInEarliest.Text = tpCheckInEarliest.Text;
            txbSelectCheckInEarliest.Foreground = (Brush)bc.ConvertFromString("#474747");

            bodListCheckInEarliestCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectCheckOutLatest 
        private void bodSelectCheckOutLatest_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCheckOutLatestCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListCheckOutLatestCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCheckOutLatestCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCheckOutLatest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectCheckOutLatest.Text = tpCheckOutLatest.Text;
            txbSelectCheckOutLatest.Foreground = (Brush)bc.ConvertFromString("#474747");

            bodListCheckOutLatestCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = ((Rectangle)sender).Parent as Grid;
            var bodPopUp = grid.Parent as Border;
            bodPopUp.Visibility = Visibility.Collapsed;
        }



        private async void AddNewShift()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.create_shift_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(txtShiftName.Text), "shift_name");
                content.Add(new StringContent(txbSelectTimeCheckIn.Text), "start_time");
                content.Add(new StringContent(txbSelectTimeCheckOut.Text), "end_time");
                content.Add(new StringContent(txbSelectCheckInEarliest.Text), "start_time_latest");
                content.Add(new StringContent(txbSelectCheckOutLatest.Text), "end_time_earliest");
                content.Add(new StringContent(shift_type), "shift_type");
                if (shift_type == "1") content.Add(new StringContent(txbChonCa.Text), "num_to_calculate");

                if (shift_type == "2") content.Add(new StringContent(txbSoTienTuongUng.Text), "num_to_money");
                content.Add(new StringContent(com_id), "com_id");

                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {

                    bodCreateShift.Visibility = Visibility.Collapsed;
                    bodThemThanhCong.Visibility = Visibility.Visible;
                    ucShiftWorkManager.GetListShift();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi thêm ca làm việc", ex.Message);
            }




        }

    }
}
