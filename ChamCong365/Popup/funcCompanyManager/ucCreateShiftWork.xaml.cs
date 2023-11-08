using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ChamCong365.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucCreateShiftWork.xaml
    /// </summary>
    public partial class ucCreateShiftWork : UserControl
    {
        public ucCreateShiftWork()
        {
            InitializeComponent();
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
            }
            else
            {
                warpLimitTimeSettingZone.Visibility -= Visibility.Collapsed;

            }
        }
        //xử lý chọn công tính ca
        private void bodBtnTinhTheoSoCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodBtnTinhTheoTien.Background = Brushes.White;
            bodBtnTinhTheoGio.Background = Brushes.White;
            txbTinhTheoTien.Foreground = Brushes.DarkGray;
            txbTinhTheoGio.Foreground = Brushes.DarkGray;
            bodBtnTinhTheoSoCa.Background = Brushes.Blue;
            txbTinhTheoSoCa.Foreground = Brushes.White;
            warpSoCongTuongUng.Visibility = Visibility.Visible;
            wrapSoTienTuongUong.Visibility = Visibility.Collapsed;
            warpInOutTime.Visibility = Visibility.Visible;



        }
        private void bodBtnTinhTheoTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodBtnTinhTheoSoCa.Background = Brushes.White;
            bodBtnTinhTheoGio.Background = Brushes.White;
            txbTinhTheoSoCa.Foreground = Brushes.DarkGray;
            txbTinhTheoGio.Foreground = Brushes.DarkGray;
            bodBtnTinhTheoTien.Background = Brushes.Blue;
            txbTinhTheoSoCa.Foreground = Brushes.DarkGray;
            txbTinhTheoTien.Foreground = Brushes.White;
            warpSoCongTuongUng.Visibility = Visibility.Collapsed;
            wrapSoTienTuongUong.Visibility = Visibility.Visible;
            warpInOutTime.Visibility = Visibility.Visible;



        }
        private void bodBtnTinhTheoGio_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodBtnTinhTheoTien.Background = Brushes.White;
            bodBtnTinhTheoSoCa.Background = Brushes.White;
            txbTinhTheoTien.Foreground = Brushes.DarkGray;
            txbTinhTheoSoCa.Foreground = Brushes.DarkGray;
            bodBtnTinhTheoGio.Background = Brushes.Blue;
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

        private void bodBtnThemCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
