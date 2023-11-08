using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucConfirmationNewDevice.xaml
    /// </summary>
    public class TestSaff
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string anh { get; set; }
        public string phongban { get; set; }
        public string Thietbi { get; set; }
        public string Thietbimoi { get; set; }
       
    }
   

    public partial class ucConfirmationNewDevice : UserControl
    {
        List<TestSaff> itemsSaffs = new List<TestSaff>();
        List<string> ListRoom = new List<string>() { " Kỹ Thuật", "Kinh Doanh", "Đề Án", "Sale", "Đào tạo", "Sáng Tạo" };
        List<string> ListCompany = new List<string>() { " FPT", "365", "Monsta-Lab", "Sun*" };
        #region Phân trang
        CollectionViewSource view = new CollectionViewSource();
        ObservableCollection<Saff> itemsSaff = new ObservableCollection<Saff>();
        int currentPageIndex = 0;
        int itemPerPage = 3;
        int totalPage = 0;
        #endregion
        public ucConfirmationNewDevice()
        {

            InitializeComponent();
            cboSelectRoom.ItemsSource = ListRoom;
            #region FakeSaff
            itemsSaffs.Add(new TestSaff() { Id = 50001, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50002, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50003, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50004, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kinh Doanh", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50005, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kinh Doanh", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50006, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kinh Doanh", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50007, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Đề Án", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50008, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Đề Án", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50009, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Đề Án", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50010, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Sale", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50011, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50012, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50013, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50014, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50015, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50016, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50017, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });
            itemsSaffs.Add(new TestSaff() { Id = 50001, name = "Tô Ngọc Ký", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", Thietbi = "Tháng 6-2023", Thietbimoi = "ngockyto001@gmail.com" });

            lsvSelectCompaty.ItemsSource = ListCompany;
            //lsvSelectRoom.ItemsSource = ListRoom;
            lsvListNameSaff.ItemsSource = itemsSaffs;
            lsvListSaffInDevice.ItemsSource = itemsSaffs;
            var namesaff = from testSaff in itemsSaffs select testSaff.name;
            lsvListNameSaff.ItemsSource = namesaff;
            #endregion
            #region phan trang
            int itemcount = itemsSaff.Count();
            totalPage = itemcount / itemPerPage;
            if (itemcount % itemPerPage != 0)
            {
                totalPage += 1;
            }
            view.Source = itemsSaff;
            view.Filter += new FilterEventHandler(view_Filter);
            //lsvListSaffInDevice.ItemsSource = itemsSaff;

            this.lsvListSaffInDevice.DataContext = view;
            ShowCurrentPageIndex();
            this.txbTotalPaget.Text = totalPage.ToString();
            #endregion
        }

        #region Phân trang
        private void ShowCurrentPageIndex()
        {
            this.txbCurrentPage.Text = (currentPageIndex + 1).ToString();
            txbTotalPaget.Text = (int.Parse(txbTotalPaget.Text) + 1).ToString();
        }
       
        void view_Filter(object sender, FilterEventArgs e)
        {
            int index = itemsSaff.IndexOf((Saff)e.Item);

            if (index >= itemPerPage * currentPageIndex && index < itemPerPage * (currentPageIndex + 1))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }

        private void bodNext_MouseUp(object sender, MouseButtonEventArgs e)
        {
            // Display next page
            if (currentPageIndex < totalPage - 1)
            {
                currentPageIndex++;
                //view.View.Refresh();
            }
            ShowCurrentPageIndex();
        }
        #endregion

        #region even1
        private void bodSelectCompany_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectCompanyCollapsed.Visibility == Visibility.Collapsed)
            {
                bodSelectCompanyCollapsed.Visibility = Visibility.Visible;
                bodSelectNameSaffCollapsed.Visibility = Visibility.Collapsed;
                //bodSelectRoomCollapsed.Visibility = Visibility.Collapsed;
                txtSelectCompany.Focus();

            }
            else
            {
                bodSelectCompanyCollapsed.Visibility = Visibility.Collapsed;
            }
        }
       

        private void bodSelectNameSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectNameSaffCollapsed.Visibility == Visibility.Collapsed)
            {
                bodSelectNameSaffCollapsed.Visibility = Visibility.Visible;
                //bodSelectRoomCollapsed.Visibility = Visibility.Collapsed;
                bodSelectCompanyCollapsed.Visibility = Visibility.Collapsed;
                txtSelectNameSaff.Focus();
            }
            else
            {
                bodSelectNameSaffCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvListNameSaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSelectNameSaff.Text = lsvListNameSaff.SelectedItem.ToString();
            bodSelectNameSaffCollapsed.Visibility = Visibility.Collapsed;
            txtSelectNameSaff.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void lsvSelectRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txtSelectRoom.Text = lsvSelectRoom.SelectedItem.ToString();
            //bodSelectRoomCollapsed.Visibility = Visibility.Collapsed;
            //txtSelectRoom.Foreground = new SolidColorBrush(Colors.Black);
        }
        private void lsvSelectCompaty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectCompany.Text = lsvSelectCompaty.SelectedItem.ToString();
            bodSelectCompanyCollapsed.Visibility = Visibility.Collapsed;
            txbSelectCompany.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void CheckDeviceAll_Checked(object sender, RoutedEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Visible;
        }

        private void CheckDeviceAll_Unchecked(object sender, RoutedEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Collapsed;
        }

        private void CheckDeviceSelected_Checked(object sender, RoutedEventArgs e)
        {
           
            bodMessageboxYesSelected.Visibility = Visibility.Visible;
        }

        private void CheckDeviceSelected_Unchecked(object sender, RoutedEventArgs e)
        {
            bodMessageboxYesSelected.Visibility = Visibility.Collapsed;
        }
        private void bodOkMessageYesAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Collapsed;
        }
        private void bodOkMessageYesSelected_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxYesSelected.Visibility = Visibility.Collapsed;
        }

        private void bodDeleteListDecvice_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodDeleteListDevice.Visibility = Visibility.Visible;
        }

        private void bodCancel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodDeleteListDevice.Visibility= Visibility.Collapsed;
        }

        #endregion

        private void bodCheckBoxFace_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCheckBoxFaceAll.Background = new SolidColorBrush(Colors.Blue);
        }

        private void bodCheckBoxFace_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCheckBoxFaceAll.Background = new SolidColorBrush(Colors.White);
        }

        

        private void bodCheckBoxFaceAll_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodCheckBoxFaceAll.Background == Brushes.Blue)
            {
                bodMessageboxYesAll.Visibility = Visibility.Collapsed;
                bodCheckBoxFaceAll.Background = Brushes.White;
                
            }
            else
            {
                if (bodMessageboxYesAll.Visibility == Visibility.Collapsed)
                {
                    bodCheckBoxFaceAll.Background = Brushes.Blue;
                   
                    bodMessageboxYesAll.Visibility = Visibility.Visible;


                }
            }
        }

        private void bodCheckBoxFaceSelected_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }
    }
}
