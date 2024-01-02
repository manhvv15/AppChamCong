using QuanLyChung365TruocDangNhap.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Comons;

namespace QuanLyChung365TruocDangNhap
{
    /// <summary>
    /// Interaction logic for ucShiftWorkManager.xaml
    /// </summary>
    /// 

    public partial class ucShiftWorkManager : UserControl
    {
        frmMain Main;
        BrushConverter br = new BrushConverter();
        string com_id = "";
        List<Shift> listShift = new List<Shift>();
        List<Shift> listShiftSearch = new List<Shift>();
        List<Shift> lstCaChon = new List<Shift>();
        Dictionary<string, string> ListShiftSearch = new Dictionary<string, string>();
        public ucShiftWorkManager(frmMain main)
        {
            this.Main = main;
            InitializeComponent();
            tb_TimKiemCaLamViec.Focus();
            tb_TimKiemCaLamViec.Focusable = true;
            GetListShift();
        }
        #region Call API
        //get danh sách ca làm việc
        public async void GetListShift()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                string api = APIs.list_shift_api;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();
                ShiftRoot result = JsonConvert.DeserializeObject<ShiftRoot>(responseContent);
                
                listShift = result.data.items;
                lstCaChon = result.data.items;

                // Thêm một phần tử trống
                listShift.Add(new Shift());

                ListCaLamViec.ItemsSource = listShift;
                lsvDanhSachCa.ItemsSource = lstCaChon;

                // thêm last item  là button thêm ca làm việc

                if (ListCaLamViec.Items.Count > 0)
                {
                    // Get the last item
                    var lastItem = ListCaLamViec.Items[ListCaLamViec.Items.Count -1];

                    // Apply the custom template to the last item
                    var lastItemContainer = ListCaLamViec.ItemContainerGenerator.ContainerFromItem(lastItem) as ListViewItem;
                    if (lastItemContainer != null)
                    {
                        lastItemContainer.ContentTemplate = ListCaLamViec.Resources["LastItemTemplate"] as DataTemplate;
                    }
                }
            }
            catch (Exception e)
            {
                CustomMessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }
        #endregion

        #region Loaded Event
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
        private void textTimKiemNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            listShiftSearch = new List<Shift>();
            foreach (var str in lstCaChon)
            {
                if (str.shift_name != null)
                {
                    if (str.shift_name.ToLower().RemoveUnicode1().Contains(tb_TimKiemCaLamViec.Text.ToLower().RemoveUnicode1()))
                    {
                        listShiftSearch.Add(str);
                    }
                }
            }
            lsvDanhSachCa.ItemsSource = listShiftSearch;
            // Thêm một phần tử trống
            listShiftSearch.Add(new Shift());
            ListCaLamViec.ItemsSource = listShiftSearch;
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
            if (tb_TimKiemCaLamViec.Text == "")
            {
                lsvDanhSachCa.ItemsSource = lstCaChon;
                ListCaLamViec.ItemsSource = listShift;
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
        }
        private void lsvDanhSachCa_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvDanhSachCa.SelectedItem != null)
            {
                listShiftSearch = new List<Shift>();
                var chonca = lsvDanhSachCa.SelectedItem.ToString();
                if (!lstCaChon.Any(item => item.shift_id.ToString() == chonca))
                {
                    Shift infor = new Shift()
                    {
                        shift_name = ((Shift)lsvDanhSachCa.SelectedItem).shift_name,
                        shift_id = ((Shift)lsvDanhSachCa.SelectedItem).shift_id,

                    };
                    listShiftSearch = lstCaChon.ToList();
                    tb_TimKiemCaLamViec.Text = infor.shift_name;
                    bod_DSCaLamViec.Visibility = Visibility.Collapsed;
                    popup.Visibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        #region Click Event
        //Xử lý nút ấn lịch làm việc
        private void btnWorkCalendar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        //Sửa ca làm việc
        private void docpSuaCaLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedShift = ListCaLamViec.SelectedItem as Shift;
            Main.pnlShowPopUp.Children.Add(new ucUpdateShiftWork(this, selectedShift));
        }
        //Xóa ca làm việc
        private void docpXoaCaLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var selectedShift = ListCaLamViec.SelectedItem as Shift;
            var shift_id = selectedShift.shift_id;
            Main.pnlShowPopUp.Children.Add(new ucDeleteShiftWork(this, shift_id.ToString()));
        }
        //Xử lý button thêm ca làm việc
        private void bodBtnAdd_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.pnlShowPopUp.Children.Add(new ucCreateShiftWork(this, com_id));
        }
        public bool shouldProcessEvent = true;
        private void bod_CaLamViec_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (shouldProcessEvent)
            {
                if (bod_DSCaLamViec.Visibility == Visibility.Collapsed)
                {
                    bod_DSCaLamViec.Visibility = Visibility.Visible;
                    popup.Visibility = Visibility.Visible;
                }
                else
                {
                    bod_DSCaLamViec.Visibility = Visibility.Collapsed;
                    popup.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void bod_DeleteTenCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            tb_TimKiemCaLamViec.Text = "";
            bod_DSCaLamViec.Visibility -= Visibility.Collapsed;
        }
        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bod_DSCaLamViec.Visibility == Visibility.Visible)
            {
                bod_DSCaLamViec.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        #endregion

        #region Hover Event
        private void bodBtnAdd_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {

                Border bodBtnAdd = FindChild<Border>(row, "bodBtnAdd");
               
                if (bodBtnAdd != null)
                {
                    bodBtnAdd.Background = (Brush)br.ConvertFrom("#4C5BD4");
                    bodBtnAdd.BorderThickness = new Thickness(0.5);
                }
            }
        }

        private void bodBtnAdd_MouseLeave(object sender, MouseEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {

                Border bodBtnAdd = FindChild<Border>(row, "bodBtnAdd");

                if (bodBtnAdd != null)
                {
                    bodBtnAdd.Background = (Brush)br.ConvertFrom("#6C7AEB");
                    bodBtnAdd.BorderThickness = new Thickness(0);
                }
            }
        }
        //xử lý khi hover vào các item trong items ca làm việc
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
        private void bod_CaLamViec_MouseEnter(object sender, MouseEventArgs e)
        {
            if (tb_TimKiemCaLamViec.Text != "")
            {
                bod_DeleteTenCa.Visibility = Visibility.Visible;
                btn_SelectListSafff.Visibility = Visibility.Collapsed;
            }
        }
        private void bod_CaLamViec_MouseLeave(object sender, MouseEventArgs e)
        {
            bod_DeleteTenCa.Visibility = Visibility.Collapsed;
            btn_SelectListSafff.Visibility = Visibility.Visible;
        }
        private void bod_DeleteTenCa_MouseEnter(object sender, MouseEventArgs e)
        {
            bod_DeleteTenCa.Background = (Brush)br.ConvertFrom("#FF5B4D");

        }
        private void bod_DeleteTenCa_MouseLeave(object sender, MouseEventArgs e)
        {
            bod_DeleteTenCa.Background = (Brush)br.ConvertFrom("#6666");
        }
        private void bod_TenCa_MouseEnter(object sender, MouseEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {

                Border bod_TenCa = FindChild<Border>(row, "bod_TenCa");
                TextBlock tb_TenCa = FindChild<TextBlock>(row, "tb_TenCa");
                if (bod_TenCa != null && tb_TenCa != null)
                {
                    bod_TenCa.Background = (Brush)br.ConvertFrom("#4C5BD4");
                    tb_TenCa.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                }
            }
        }
        private void bod_TenCa_MouseLeave(object sender, MouseEventArgs e)
        {
            ListViewItem row = FindAncestor<ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bod_TenCa = FindChild<Border>(row, "bod_TenCa");
                TextBlock tb_TenCa = FindChild<TextBlock>(row, "tb_TenCa");
                if (bod_TenCa != null && tb_TenCa != null)
                {
                    bod_TenCa.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_TenCa.Foreground = (Brush)br.ConvertFrom("#474747");
                }
            }
        }
        //private void btnWorkCalendar_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    btnWorkCalendar.Background = (Brush)br.ConvertFrom("#4AA7FF");
        //    btnWorkCalendar.BorderThickness = new Thickness(0.5);
        //}

        //private void btnWorkCalendar_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    btnWorkCalendar.Background = (Brush)br.ConvertFrom("#4C5BD4");
        //    btnWorkCalendar.BorderThickness = new Thickness(0);
        //}
        #endregion

        #region Comons
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindChild<T>(child, childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        #endregion

        private void ListCaLamViec_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset -e.Delta);
        }
    }
}
