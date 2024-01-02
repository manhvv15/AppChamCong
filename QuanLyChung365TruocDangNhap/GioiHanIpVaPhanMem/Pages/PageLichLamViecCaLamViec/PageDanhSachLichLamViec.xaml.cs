using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCong365.Entities;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.LichLamViecPopups;
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

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Pages.PageLichLamViecCaLamViec
{
    /// <summary>
    /// Interaction logic for PageDanhSachCaLamViec.xaml
    /// </summary>
    public partial class PageDanhSachLichLamViec : Page
    {
        frmMain Main;
        public PageDanhSachLichLamViec(frmMain Main)
        {
            InitializeComponent();
            this.Main = Main;
            GetCycleList();
        }

        public async void GetCycleList(int pageNumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/cycle/list");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_LichLamViec_List.Root result = JsonConvert.DeserializeObject<API_LichLamViec_List.Root>(responseContent);
                    if (pagingShift.SelectedPage == 0) pagingShift.TotalRecords = (int)result.data.data.Count;
                    int stt = 1;
                    var bindingList = (from item in result.data.data
                                       select new API_LichLamViec_List.Cycle
                                       {
                                           STT = stt++,
                                           cy_id = item.cy_id,
                                           com_id = item.cy_id,
                                           cy_name = (item.cy_name == "") ? "Chưa cập nhật" : item.cy_name,
                                           apply_month = item.apply_month,
                                           str_apply_month = (item.apply_month == null) ? "Chưa cập nhật" : item.apply_month?.ToString("MM/yyyy"),
                                           start_date = (item.apply_month == null) ? "Chưa cập nhật" : item.apply_month?.ToString("dd/MM/yyyy"),
                                           cy_detail = item.cy_detail,
                                           status = (item.status == "") ? "Chưa cập nhật" : item.status,
                                           ep_count = (item.ep_count == "") ? "Chưa cập nhật" : item.ep_count

                                       }).ToList();
                    var listPaging = bindingList.Skip((pageNumber - 1) * 10).Take(10);
                    dsLichLamViec.ItemsSource = listPaging;

                }

            }
            catch { }
        }
        private void Edit_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delelte_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedCycel = ((Border)sender).DataContext as API_LichLamViec_List.Cycle;
                Main.pnlShowPopUp.Children.Add(new ucXoaLichLamViec(Main, this, (int)selectedCycel.cy_id));
            }
            catch
            {

            }
        }
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            GetCycleList(pagingShift.SelectedPage);
        }

        private void NavigateToDSCaLamViec(object sender, MouseButtonEventArgs e)
        {
            try
            {
                PageDanhSachCaLamViec qlnv = new PageDanhSachCaLamViec(Main);
                Main.stp_ShowPopup.Children.Clear();
                object Content = qlnv.Content;
                qlnv.Content = null;
                Main.stp_ShowPopup.Children.Add(Content as UIElement);
            }
            catch { }

        }

        private void btnTaoLichLamViec_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.pnlShowPopUp.Children.Add(new ucThemMoiLichLamViec(Main));
        }

        private void NavigateToListEmployee(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedCycel = ((Border)sender).DataContext as API_LichLamViec_List.Cycle;
                Main.pnlShowPopUp.Children.Add(new ucDanhSachNhanVienLichLamViec(Main, (int)selectedCycel.cy_id, selectedCycel.apply_month.ToString()));
            }
            catch
            {

            }
        }

        private void NavigateToAddEmployeePopup(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedCycel = ((Border)sender).DataContext as API_LichLamViec_List.Cycle;
                Main.pnlShowPopUp.Children.Add(new ucThemNhanVienVaoLichLamViec(Main, this, (int)selectedCycel.cy_id, selectedCycel.apply_month.ToString()));
            }
            catch
            {

            }
        }


        private void dsLichLamViec_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                var scrollViewer = FindVisualChild<ScrollViewer>(dsLichLamViec);
                if (scrollViewer != null)
                {
                    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
                    e.Handled = true;
                }
            }
            else
                Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset - e.Delta);



        }

        private T FindVisualChild<T>(DependencyObject visual) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(visual, i);
                if (child != null && child is T)
                {
                    return (T)child;
                }
                else
                {
                    T childItem = FindVisualChild<T>(child);
                    if (childItem != null)
                        return childItem;
                }
            }
            return null;
        }

        private void Copy_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var selectedCycel = ((Border)sender).DataContext as API_LichLamViec_List.Cycle;
                Main.pnlShowPopUp.Children.Add(new ucTuyChonSaoChepLich(Main, selectedCycel));
            }
            catch
            {

            }
        }
    }
}
