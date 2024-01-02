using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using Newtonsoft.Json;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.LichLamViecPopups
{

    /// <summary>
    /// Interaction logic for ucListSaff.xaml
    /// </summary>
    public partial class ucDanhSachNhanVienLichLamViec : UserControl, INotifyCollectionChanged
    {
        frmMain Main;
        public int cy_id;
        public string Apply_Month;
        CollectionViewSource view = new CollectionViewSource();
        ObservableCollection<ListSaff> listsaff = new ObservableCollection<ListSaff>();
        private List<ListSaff> _listSaffs;
        public List<ListSaff> ListSaffs
        {
            get { return _listSaffs; }
            set { _listSaffs = value; }
        }
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        int currentPageIndex = 0;
        int itemPerPage = 5;
        int totalPage = 0;
        BrushConverter br = new BrushConverter();
        public ucDanhSachNhanVienLichLamViec(frmMain main, int ID, string apply_month)
        {

            InitializeComponent();
            this.DataContext = this;
            this.cy_id = ID;
            this.Apply_Month = apply_month;
            Main = main;
            LoadListSaffInCalendarWork();

        }
        #region CallAPI

        public async void LoadListSaffInCalendarWork()
        {
            try
            {


                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/cycle/list_employee");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(cy_id.ToString()), "cy_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                RootSaff result = JsonConvert.DeserializeObject<RootSaff>(responseContent);
                if (result != null)
                {
                    DateTime aDay;
                    DateTime.TryParse(Apply_Month, out aDay);
                    Apply_Month = aDay.ToString("MM/yyyy");
                    txbHeaderListSaff.Text = Apply_Month;
                    ListSaffs = result.data.list;
                    foreach (var item in ListSaffs)
                    {
                        listsaff.Add(item);
                    }
                    txbCountSaff.Text = "(" + listsaff.Count + ")";
                }
                foreach (ListSaff item in listsaff)
                {
                    if (item.avatarUser == "" || item.avatarUser == null)
                    {
                        item.avatarUser = "https://chamcong.timviec365.vn/images/ep_logo.png";
                    }
                    else
                    {
                        item.avatarUser = "https://chamcong.24hpay.vn/upload/employee/" + item.avatarUser;
                    }
                }
                foreach (ListSaff item in listsaff)
                {
                    if (item.phone == "" || item.phone == null)
                    {
                        item.phone = "Chưa cập nhật!";
                    }
                    else
                    {
                        item.phone = item.phone;
                    }
                }
                foreach (ListSaff item in listsaff)
                {
                    if (item.email == "" || item.email == null)
                    {
                        item.email = "Chưa cập nhật!";
                    }
                    else
                    {
                        item.email = item.email;
                    }
                }
                totalPage = listsaff.Count / itemPerPage;
                if (listsaff.Count % itemPerPage != 0)
                {
                    totalPage += 1;
                }

                tbTotalPage.Text = totalPage.ToString();
                view.Source = listsaff;

                view.Filter += new FilterEventHandler(view_Filter);

                lsvListSaffSmall.DataContext = view;
                this.tbTotalPage.Text = totalPage.ToString();
                lsvListSaffSmall.Items.Refresh();
            }
            catch (System.Exception)
            {
            }
        }
        #endregion
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;


        }

        private void bodDeleteSaffOnList_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Border p = sender as Border;
            ListSaff data = (ListSaff)p.DataContext;

            Main.pnlShowPopUp.Children.Add(new ucXoaNhanVienLichLamViec(this, cy_id, data.ep_id, view));
        }

        #region Phân Trang
        private void view_Filter(object sender, FilterEventArgs e)
        {
            int index = listsaff.IndexOf((ListSaff)e.Item);
            if (index >= itemPerPage * currentPageIndex && index < itemPerPage * (currentPageIndex + 1))
            {
                e.Accepted = true;
            }
            else
            {
                e.Accepted = false;
            }
        }
        private void ShowCurrentPageIndex()
        {
            this.tbCurrentPage.Text = (currentPageIndex + 1).ToString();
        }
        private void btnFirst_Click(object sender, MouseButtonEventArgs e)
        {
            if (currentPageIndex != 0)
            {
                currentPageIndex = 0;
                view.View.Refresh();
            }
            ShowCurrentPageIndex();
        }
        private void btnBackPage_Click(object sender, MouseButtonEventArgs e)
        {
            if (currentPageIndex > 0)
            {
                currentPageIndex--;
                view.View.Refresh();
            }
            ShowCurrentPageIndex();
        }
        private void btnNextPage_Click(object sender, MouseButtonEventArgs e)
        {
            if (currentPageIndex < totalPage - 1)
            {
                currentPageIndex++;
                view.View.Refresh();
            }
            ShowCurrentPageIndex();
        }
        private void btnTrangCuoi_Click(object sender, MouseButtonEventArgs e)
        {
            if (currentPageIndex != totalPage - 1)
            {
                currentPageIndex = totalPage - 1;
                view.View.Refresh();
            }
            ShowCurrentPageIndex();
        }

        #endregion

        private void btnTrangDau_MouseEnter(object sender, MouseEventArgs e)
        {
            btnTrangDau.Background = (Brush)br.ConvertFrom("#4C5BD4");
            tbTrangDau.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void btnTrangDau_MouseLeave(object sender, MouseEventArgs e)
        {
            btnTrangDau.Background = (Brush)br.ConvertFrom("#FFFFFF");
            tbTrangDau.Foreground = (Brush)br.ConvertFrom("#474747");
        }

        private void btnBackPage_MouseEnter(object sender, MouseEventArgs e)
        {
            btnBackPage.Background = (Brush)br.ConvertFrom("#4C5BD4");
            tbBackPage.Fill = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void btnBackPage_MouseLeave(object sender, MouseEventArgs e)
        {
            btnBackPage.Background = (Brush)br.ConvertFrom("#FFFFFF");
            tbBackPage.Fill = (Brush)br.ConvertFrom("#474747");
        }

        private void btnNextPage_MouseEnter(object sender, MouseEventArgs e)
        {
            btnNextPage.Background = (Brush)br.ConvertFrom("#4C5BD4");
            tbNextPage.Fill = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void btnNextPage_MouseLeave(object sender, MouseEventArgs e)
        {
            btnNextPage.Background = (Brush)br.ConvertFrom("#FFFFFF");
            tbNextPage.Fill = (Brush)br.ConvertFrom("#474747");
        }

        private void btnTrangCuoi_MouseEnter(object sender, MouseEventArgs e)
        {
            btnTrangCuoi.Background = (Brush)br.ConvertFrom("#4C5BD4");
            tbTrangCuoi.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void btnTrangCuoi_MouseLeave(object sender, MouseEventArgs e)
        {
            btnTrangCuoi.Background = (Brush)br.ConvertFrom("#FFFFFF");
            tbTrangCuoi.Foreground = (Brush)br.ConvertFrom("#474747");
        }
    }
}
