using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCong365.Entities;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.CaLamViec;
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
    /// 

    public partial class PageDanhSachCaLamViec : Page
    {
        frmMain Main;
        public PageDanhSachCaLamViec(frmMain main)
        {
            InitializeComponent();
            Main = main;
            ucShiftWorkManager qlnv = new ucShiftWorkManager(Main);
         
            object Content = qlnv.Content;
            qlnv.Content = null;
            this.borCaLamViec.Child = (Content as UIElement);
            //GetListShift();
        }
        #region CallAPI
        //public async void GetListShift(int pageNumber = 1)
        //{
        //    try
        //    {

        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3000/api/qlc/shiftsNew/list");
        //        request.Headers.Add("authorization", "Bearer " + Main.Tokens);
        //        var response = await client.SendAsync(request);
        //        var responseContent = await response.Content.ReadAsStringAsync();
        //        if (response.IsSuccessStatusCode)
        //        {
        //            API_ShifNew_List.Root result = JsonConvert.DeserializeObject<API_ShifNew_List.Root>(responseContent);
        //            if (pagingShift.SelectedPage == 0) pagingShift.TotalRecords = (int)result.data.data.Count();
        //            dsCaLamViec.ItemsSource = result.data.data.Skip((pageNumber - 1) * 10).Take(10);

        //        }

        //    }
        //    catch { }
        //}
        #endregion
        private void Edit_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Delelte_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            //GetListShift(pagingShift.SelectedPage);
        }

        private void NavigateToDSLich(object sender, MouseButtonEventArgs e)
        {
            PageDanhSachLichLamViec qlnv = new PageDanhSachLichLamViec(Main);
            Main.stp_ShowPopup.Children.Clear();
            object Content = qlnv.Content;
            qlnv.Content = null;
            Main.stp_ShowPopup.Children.Add(Content as UIElement);
        }

        private void btnThemCa_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Main.pnlShowPopUp.Children.Add(new ucCreateShiftWork(this, Main.IdAcount));
        }

        //private void dsCaLamViec_MouseWheel(object sender, MouseWheelEventArgs e)
        //{
        //    if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
        //    {
        //        var scrollViewer = FindVisualChild<ScrollViewer>(dsCaLamViec);
        //        if (scrollViewer != null)
        //        {
        //            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - e.Delta);
        //            e.Handled = true;
        //        }
        //    }
        //    else
        //        Main.ScrollMain.ScrollToVerticalOffset(Main.ScrollMain.VerticalOffset - e.Delta);



        //}

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
    }
}
