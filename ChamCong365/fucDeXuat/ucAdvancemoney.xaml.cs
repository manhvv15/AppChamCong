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

namespace ChamCong365
{

    /// logic for ucAdvancemoney

    public partial class ucAdvancemoney : UserControl
    {
        List<string> listSearch = new List<string>();
        //List<Time> items = new List<Time>();
        List<string> listYear = new List<string>() { "Năm 2023", "Năm 2024", "Năm 2025", "Năm 2026", "Năm 2027" };
        List<string> ListMonth = new List<string>() { "Tháng 1", "Tháng 2", "Tháng 3", "Tháng 4", "Tháng 5", "Tháng 6" };
        List<string> ListName = new List<string>() { "Tất cả nhân viên", "Hồ Mạnh Hùng", "Hồ Mạnh Hùng", "Hồ Mạnh Hùng", "Hồ Mạnh Hùng" };
        public class TamUngTien
        {
            
            public string Anh { get; set; }
            public string Ten { get; set; }
            public string ID { get; set; }
            public string NgayTamUng { get; set; }
            public string TienTamUng { get; set; }
            public string TrangThai { get; set; }
            
        }
        private List<TamUngTien> lst = new List<TamUngTien>();
        private MainWindow Main;
        public ucAdvancemoney(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            txtSelectYear.Text = listYear[0];

            txtSelectMonth.Text = ListMonth[0];
            #region FakeName
            //items.Add(new Time() { Id = 504004, Name = "Hồ Mạnh Hùng", Image = "./Resource/image/Ellipse 1125.png", Advance = "10/5/2023", Money = "5.000.000 VNĐ", Status = "Đã Duyệt", Note = "Xem chi tiết " });
            //items.Add(new Time() { Id = 504004, Name = "Hồ Mạnh Hùng", Image = "./Resource/image/Ellipse 1125.png", Advance = "10/5/2023", Money = "5.000.000 VNĐ", Status = "Chờ duyệt", Note = "Xem chi tiết" });
            //items.Add(new Time() { Id = 504004, Name = "Hồ Mạnh Hùng", Image = "./Resource/image/Ellipse 1125.png", Advance = "10/5/2023", Money = "5.000.000 VNĐ", Status = "Đã Duyệt", Note = "Xem chi tiết" });
            //items.Add(new Time() { Id = 504004, Name = "Hồ Mạnh Hùng", Image = "./Resource/image/Ellipse 1125.png", Advance = "10/5/2023", Money = "5.000.000 VNĐ", Status = "Chờ duyệt", Note = "Xem chi tiết" });
            //items.Add(new Time() { Id = 504004, Name = "Hồ Mạnh Hùng", Image = "./Resource/image/Ellipse 1125.png", Advance = "10/5/2023", Money = "5.000.000 VNĐ", Status = "Đã Duyệt", Note = "Xem chi tiết" });
            //items.Add(new Time() { Id = 504004, Name = "Hồ Mạnh Hùng", Image = "./Resource/image/Ellipse 1125.png", Advance = "10/5/2023", Money = "5.000.000 VNĐ", Status = "Đã Duyệt", Note = "Xem chi tiết" });
            //items.Add(new Time() { Id = 504004, Name = "Hồ Mạnh Hùng", Image = "./Resource/image/Ellipse 1125.png", Advance = "10/5/2023", Money = "5.000.000 VNĐ", Status = "Đã Duyệt", Note = "Xem chi tiết" });

            #endregion

            lsvSelectYear.ItemsSource = listYear;
            lsvSelectMonth.ItemsSource = ListMonth;
            lsvSelectNV.ItemsSource = ListName;
            //lsvListSaffInFace.ItemsSource = items;
            loadDL();


        }

        private void loadDL()
        {
            lst.Add(new TamUngTien { Anh = "F:/anhbia.jpg", ID = "11", Ten = "Đỗ Văn Hoàng", NgayTamUng = "10/10/2021", TienTamUng = "2.000.000 VND", TrangThai = "Đã duyệt" });
            lst.Add(new TamUngTien { Anh = "F:/anh-buon-den-trang-dep-nhat_044703210.jpg", ID = "22", Ten = "Đỗ Văn Hoàng", NgayTamUng = "10/10/2021", TienTamUng = "2.000.000 VND", TrangThai = "Đã duyệt" });
            lst.Add(new TamUngTien { Anh = "F:/anh-dep-dem-trang_091459242.jpg", ID = "33", Ten = "Thân Đức Toàn", NgayTamUng = "10/10/2021", TienTamUng = "2.000.000 VND", TrangThai = "Đã duyệt" });
            lst.Add(new TamUngTien { Anh = "F:/anh-dep-ve-tinh-yeu-tinh-cam.jpg", ID = "44", Ten = "Nguyễn Văn Công", NgayTamUng = "10/10/2021", TienTamUng = "2.000.000 VND", TrangThai = "Đã duyệt" });
            lst.Add(new TamUngTien { Anh = "F:/anonymous-muc-dic-hoat-dong.jpg", ID = "55", Ten = "Nguyễn Thị Thu Hằng", NgayTamUng = "10/10/2021", TienTamUng = "2.000.000 VND", TrangThai = "Đã duyệt" });
            lst.Add(new TamUngTien { Anh = "F:/cho-thue-am-thanh-anh-sang-3.jpg", ID = "66", Ten = "Nguyễn Bá Đạt", NgayTamUng = "10/10/2021", TienTamUng = "2.000.000 VND", TrangThai = "Đã duyệt" });
            lsvThuongPhat.ItemsSource = lst;
        }

        private void bodSelectNV_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectNVCollapsed.Visibility == Visibility.Collapsed)
            {
                bodSelectNVCollapsed.Visibility = Visibility.Visible;
                bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
                bodSelectMonthCollapsed.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Visible;

            }
            else
            {
                bodSelectNVCollapsed.Visibility = Visibility.Collapsed;
            }
        }


        private void bodSelectYear_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectYearCollapsed.Visibility == Visibility.Collapsed)
            {
                bodSelectYearCollapsed.Visibility = Visibility.Visible;
                bodSelectMonthCollapsed.Visibility = Visibility.Collapsed;
                bodSelectNVCollapsed.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Visible;
                txtSelectYear.Focus();
            }
            else
            {
                bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void bodSelectMonth_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectMonthCollapsed.Visibility == Visibility.Collapsed)
            {
                bodSelectMonthCollapsed.Visibility = Visibility.Visible;
                bodSelectNVCollapsed.Visibility = Visibility.Collapsed;
                bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Visible;
                txtSelectMonth.Focus();
            }
            else
            {
                bodSelectMonthCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
        }

        private void txtSearchYear_TextChanged(object sender, TextChangedEventArgs e)
        {
            foreach (var itemYear in listYear)
            {
                if (itemYear.ToLower().Contains(txtSearchYear.Text.ToString()))
                {
                    listSearch.Add(itemYear);
                }

            }
            lsvListYear.ItemsSource = listSearch;
        }

        private void lsvSelectMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSelectMonth.Text = lsvSelectMonth.SelectedItem.ToString();
            bodSelectMonthCollapsed.Visibility = Visibility.Collapsed;
        }

        private void lsvSelectNV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectCompany.Text = lsvSelectNV.SelectedItem.ToString();
            bodSelectNVCollapsed.Visibility = Visibility.Collapsed;

        }


        private void checkManager_Checked(object sender, RoutedEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Visible;
            bodMessageboxNoAll.Visibility = Visibility.Collapsed;
        }

        private void checkManager_Unchecked(object sender, RoutedEventArgs e)
        {
            bodMessageboxNoAll.Visibility = Visibility.Visible;
            bodMessageboxYesAll.Visibility = Visibility.Collapsed;
        }

        private void bodOkMessageYesAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Collapsed;
        }
        private void bodOkMessageNoAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxNoAll.Visibility = Visibility.Collapsed;
        }

        private void bodOkMessageYesSelected_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxYesSelected.Visibility = Visibility.Collapsed;
        }

        private void bodOkMessageNoSelected_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxNoSelected.Visibility = Visibility.Collapsed;
        }

        private void checkManagerSelected_Checked(object sender, RoutedEventArgs e)
        {
            bodMessageboxYesSelected.Visibility = Visibility.Visible;
            bodMessageboxNoSelected.Visibility = Visibility.Collapsed;
        }

        private void checkManagerSelected_Unchecked(object sender, RoutedEventArgs e)
        {
            bodMessageboxNoSelected.Visibility = Visibility.Visible;
            bodMessageboxYesSelected.Visibility = Visibility.Collapsed;
        }

        private void bodYear_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodYear.Visibility == Visibility.Collapsed)
            {
                bodYear.Visibility = Visibility.Visible;
                txtSearchYear.Focus();

            }
            else
            {
                bodYear.Visibility -= Visibility.Collapsed;
            }
        }

        private void lsvListYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSelectYear.Text = lsvSelectYear.SelectedItem.ToString();
            bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
        }

        private void txtSearchMonth_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtSearchNV_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void bodAddMoney_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (bodAddMoney.Visibility == Visibility.Collapsed)
            //{
            //    bodAddMoney.Visibility = Visibility.Visible;

            //    bodAddInfo.Visibility = Visibility.Visible;




            //}
            //else
            //{
            //    bodAddMoney.Visibility = Visibility.Collapsed;

            //    bodAddInfo.Visibility = Visibility.Collapsed;
            //}
        }

        private void bodCheckBoxAll_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (checkManagerAll.Background == Brushes.White)
            //{
            //    checkManagerAll.Background = Brushes.Black;
            //    bodMessageboxYesAll.Visibility = Visibility.Collapsed;

            //    bodMessageboxYesAll.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    if (bodMessageboxNoAll.Visibility == Visibility.Collapsed)
            //    {

            //        bodMessageboxNoAll.Visibility = Visibility.Visible;

            //        checkManagerAll.Background = Brushes.White;




            //    }
            //}
        }

        private void lsvThuongPhat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodSelectMonthCollapsed.Visibility = Visibility.Collapsed;
            bodSelectNVCollapsed.Visibility = Visibility.Collapsed;
            bodSelectYearCollapsed.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
        }

        private void checkDuyet_Checked(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.DeXuat.TamUngTien.PopUpHoiTruocKhiDuyet(Main, "Việc duyệt sẽ ảnh hưởng đến mức lương của nhân viên bạn vẫn muốn tiếp tục ?"));
        }

        private void checkDuyet_Unchecked(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.DeXuat.TamUngTien.PopUpHoiTruocKhiDuyet(Main, "Bạn có chắc chắn muốn bỏ duyệt không ?"));

        }
    }

}












