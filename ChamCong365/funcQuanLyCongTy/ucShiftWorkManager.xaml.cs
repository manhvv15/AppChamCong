using ChamCong365.Popup;
using ChamCong365.Popup.funcCompanyManager;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ChamCong365
{
    public class CaLamViec
    {
        public int Id { get;set; }
        public string TenCa { get; set; }
        public string GioiVao { get; set; }
        public string GioiRa { get; set; }
    }

    /// <summary>
    /// Interaction logic for ucShiftWorkManager.xaml
    /// </summary>
    /// 

    public partial class ucShiftWorkManager : UserControl
    {
        MainWindow Main;
        List<CaLamViec> dsCaLamViec = new List<CaLamViec>();
        List<string> listCompany = new List<string>() { "PT shop","PT shop2","PT shop3"};
        List<string> dsCongTrenCa = new List<string>() { "0,25 Công / 1 Ca", "0,5 Công / 1 Ca" , "0,75 Công / 1 Ca" , "1 Công / 1 Ca" };
        public ucShiftWorkManager(MainWindow main)
        {
            InitializeComponent();
            #region FaKeCaLamViec
            dsCaLamViec.Add(new CaLamViec() { Id = 1, TenCa = "Ca Sáng", GioiVao = "8:00", GioiRa = "11:30" });
            dsCaLamViec.Add(new CaLamViec() { Id = 1, TenCa = "Ca Chiều", GioiVao = "8:00", GioiRa = "11:30" });
            dsCaLamViec.Add(new CaLamViec() { Id = 2, TenCa = "Ca Tối", GioiVao = "8:00", GioiRa = "11:30" });
            dsCaLamViec.Add(new CaLamViec() { Id = 3, TenCa = "Ca Tối", GioiVao = "8:00", GioiRa = "11:30" });
            dsCaLamViec.Add(new CaLamViec() { Id = 4, TenCa = "Ca Tối", GioiVao = "8:00", GioiRa = "11:30" });
            dsCaLamViec.Add(new CaLamViec() { Id = 5, TenCa = "Ca Tối", GioiVao = "8:00", GioiRa = "11:30" });
            //add last item to create button
            dsCaLamViec.Add(new CaLamViec());
            ListCaLamViec.ItemsSource = dsCaLamViec;
            lsvCompany.ItemsSource = listCompany;
            lsvCongTrenCa.ItemsSource = dsCongTrenCa;
            Main = main;
            #endregion


        }


        //Áp dụng template cho item cuối là button thêm
        private void ListCaLamViec_Loaded(object sender, RoutedEventArgs e)
        {
            if (ListCaLamViec.Items.Count > 0)
            {
                // Get the last item
                var lastItem = ListCaLamViec.Items[ListCaLamViec.Items.Count - 1];

                // Apply the custom template to the last item
                var lastItemContainer = ListCaLamViec.ItemContainerGenerator.ContainerFromItem(lastItem) as ListViewItem;
                if (lastItemContainer != null)
                {
                    lastItemContainer.ContentTemplate = ListCaLamViec.Resources["LastItemTemplate"] as DataTemplate;
                }
            }
        }

        //Xử lý nút ấn lịch làm việc
        private void btnWorkCalendar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //ucInstallCalendarWork uc = new ucInstallCalendarWork();
            //gridShiftManager.Margin = new Thickness(0, 0, 0, 0);
            //gridShiftManager.Children.Clear();
            //object Content = uc.Content;
            //uc.Content = null;
            //gridShiftManager.Children.Add(Content as UIElement);
        }
        //Xử lý drop box chọn công ty
        private void Image_MouseLeftButtonUp_SelectCompany(object sender, MouseButtonEventArgs e)
        {
            if (bodCompany.Visibility == Visibility.Collapsed)
            {
                bodCompany.Visibility = Visibility.Visible;
            }
            else
            {
                bodCompany.Visibility -= Visibility.Collapsed;

            }
        }
        //Xử lý khi ấn chọn công ty
        private void lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectCompany.Text = lsvCompany.SelectedItem.ToString();
            bodCompany.Visibility = Visibility.Collapsed;
        }
        //xử lý khi hover vào các item trong list ca làm việc
        private void LsvItem_CaLamViec_MouseEnter(object sender, MouseEventArgs e)
        {
            var gird = (System.Windows.Controls.Grid)sender;
            gird.Children[3].Visibility = Visibility.Visible;
        }

        private void LsvItem_CaLamViec_MouseLeave(object sender, MouseEventArgs e)
        {
            var gird = (System.Windows.Controls.Grid)sender;
            gird.Children[3].Visibility = Visibility.Collapsed;
        }
        //Sửa ca làm việc
        private void docpSuaCaLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (themMoiCa.Visibility == Visibility.Collapsed)
            {
                themMoiCa.Visibility = Visibility.Visible;
            }
            else
            {
                themMoiCa.Visibility -= Visibility.Collapsed;

            }
        }
        //Xóa ca làm việc
        private void docpXoaCaLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodXacNhanXoa.Visibility = Visibility.Visible;
        }

        //Xử lý button thêm ca làm việc
        private void bodBtnAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCreateShiftWork());
        }
        //Xử lý btn exit thêm ca
        private void btnExit_ThemCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

                themMoiCa.Visibility -= Visibility.Collapsed;

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
        //Xử lý khi ấn nút thêm
        private void bodBtnThemCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            themMoiCa.Visibility = Visibility.Collapsed;
            bodThemThanhCong.Visibility = Visibility.Visible;
        }
        //Xử lý khi ấn ok sau khi thêm
        private void bodThemThanhCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodThemThanhCong.Visibility = Visibility.Collapsed;
        }
        //Xuy lý khi ấn hủy xóa
        private void bodHuyXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodXacNhanXoa.Visibility = Visibility.Collapsed;
        }
        //Xử lý khi ấn đồng ý xóa
        private void bodDongYXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodXacNhanXoa.Visibility = Visibility.Collapsed;
        }
    }
}
