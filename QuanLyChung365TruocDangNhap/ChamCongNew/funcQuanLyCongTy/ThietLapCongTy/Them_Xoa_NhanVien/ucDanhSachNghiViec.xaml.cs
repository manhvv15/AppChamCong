﻿using Newtonsoft.Json;

using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;

using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupQuanLyNhanVien;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Them_Xoa_NhanVien
{
    /// <summary>
    /// Interaction logic for ucTatCaNhanVien.xaml
    /// </summary>
    public partial class ucDanhSachNghiViec : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        ucQuanLyNhanVien ucManager;

        private readonly int Total;
        private List<List_NhanVien> _lstNhanVien;
        public List<List_NhanVien> lstNhanVien
        {
            get { return _lstNhanVien; }
            set { _lstNhanVien = value; OnPropertyChanged(); }
        }

        private List<List_NhanVien> _lstNhanVienSearch;
        public List<List_NhanVien> lstNhanVienSearch
        {
            get { return _lstNhanVienSearch; }
            set { _lstNhanVienSearch = value; OnPropertyChanged(); }
        }
        private List<List_NhanVien> _lstNhanVienSearchAll;
        public List<List_NhanVien> lstNhanVienSearchAll
        {
            get { return _lstNhanVienSearchAll; }
            set { _lstNhanVienSearchAll = value; OnPropertyChanged(); }
        }
        public ucDanhSachNghiViec(MainWindow main, ucQuanLyNhanVien ucmanager)
        {
            InitializeComponent();
            Main = main;
            LoadDanhSachNhanVien();
            LoadDanhSachTatCaNhanVien();
            GetListOrganize();
            GetListPosition();
            this.ucManager = ucmanager;
        }

        #region call api
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            LoadDanhSachNhanVien(paginNV.SelectedPage);

        }
        int pageNumber = 1;
        public async void LoadDanhSachNhanVien(int pageNumber = 1)
        {
            try
            {
                int position_id = 0;
                if (cboViTri.SelectedIndex != -1)
                {
                    position_id = (int)(cboViTri.SelectedItem as ListPositionEntities.PositionData).id;
                }
                List<ListOrganizeEntities.ListOrganizeDetailId> listOrganizeDetailId = null;

                if (cboTenToChuc.SelectedIndex != -1)
                {
                    listOrganizeDetailId = (cboTenToChuc.SelectedItem as ListOrganizeEntities.OrganizeData).listOrganizeDetailId;
                }
                string userName = "";

                if (cboHoVaTen.SelectedIndex != -1)
                {
                    userName = ((cboHoVaTen.SelectedItem as List_NhanVien).ep_id == 0) ? "" : ((cboHoVaTen.SelectedItem as List_NhanVien).ep_id == 0) ? "" : (cboHoVaTen.SelectedItem as List_NhanVien).userName;
                }
                var searchObject = new
                {
                    ep_status = "Deny",
                    pageSize = 10000,
                    userName = userName,
                    position_id = position_id,
                    listOrganizeDetailId = listOrganizeDetailId

                };
                string searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_DanhSachNhanVien);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new StringContent(searchJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resSaff = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Root_NhanVien resultSaff = JsonConvert.DeserializeObject<Root_NhanVien>(resSaff);
                    lstNhanVienSearchAll = resultSaff.data.data;
                    var lstSaff = resultSaff.data.data.Skip((pageNumber - 1) * 10).Take(10);
                    var STT = (pageNumber - 1) * 10 + 1;
                    lstNhanVien = (from item in lstSaff
                                   select new List_NhanVien
                                   {
                                       STTNV = STT++,
                                       userName = item.userName,
                                       ep_id = item.ep_id,
                                       phone = (item.phone == null) ? "Chưa cập nhật!" : item.phone,
                                       avatarUser = item.avatarUser,
                                       address = item.address,
                                       email = item.email,
                                       gender = item.gender,
                                       listOrganizeDetailId = item.listOrganizeDetailId,
                                       organizeDetailId = item.organizeDetailId,
                                       organizeDetailName = (item.organizeDetailName == "") ? "Chưa cập nhật!" : item.organizeDetailName,
                                       positionName = (item.positionName == "") ? "Chưa cập nhật" : item.positionName,
                                       position_id = item.position_id,
                                       start_working_time = item.start_working_time,

                                   }).ToList();
                    if (paginNV.SelectedPage == 0) paginNV.TotalRecords = (int)resultSaff.data.total;
                    dgvDanhSachNhanVien.ItemsSource = lstNhanVien;
                }
            }
            catch (Exception)
            {
            }
        }

        public async void LoadDanhSachTatCaNhanVien()
        {
            try
            {

                var searchObject = new
                {
                    ep_status = "Deny",
                    pageSize = 10000


                };
                string searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_DanhSachNhanVien);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new StringContent(searchJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resSaff = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Root_NhanVien resultSaff = JsonConvert.DeserializeObject<Root_NhanVien>(resSaff);

                    ucManager.tb_CountSaftdow.Text = "(" + resultSaff.data.total.ToString() + ")";
                    lstUserData = resultSaff.data.data;
                    cboHoVaTen.ItemsSource = lstUserData;
                }
            }
            catch (Exception)
            {
            }
        }
        #region GetListOrganize
        private List<ListOrganizeEntities.OrganizeData> _lstOrganizeData;
        public List<ListOrganizeEntities.OrganizeData> lstOrganizeData
        {
            get { return _lstOrganizeData; }
            set { _lstOrganizeData = value; }
        }

        Dictionary<string, string> ListOrganize = new Dictionary<string, string>();
        Dictionary<string, string> ListPosition = new Dictionary<string, string>();
        public async void GetListOrganize()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.listAll_organize);

                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responseContent);

                    if (result.data.data != null)
                    {
                        lstOrganizeData = result.data.data;
                        cboTenToChuc.ItemsSource = lstOrganizeData.Prepend(new ListOrganizeEntities.OrganizeData() { id = 0, organizeDetailName = "Tất cả" });
                    }
                }
            }
            catch
            {

            }
        }
        #endregion

        #region GetPosition
        private List<ListPositionEntities.PositionData> _lstPositionData;
        public List<ListPositionEntities.PositionData> lstPositionData
        {
            get { return _lstPositionData; }
            set { _lstPositionData = value; }
        }
        private List<List_NhanVien> _lstUserData;
        public List<List_NhanVien> lstUserData
        {
            get { return _lstUserData; }
            set { _lstUserData = value; }
        }
        private async void GetListPosition()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.list_position);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ListPositionEntities.Root result = JsonConvert.DeserializeObject<ListPositionEntities.Root>(responseContent);
                    if (result.data.data != null)
                    {
                        lstPositionData = result.data.data;
                        cboViTri.ItemsSource = lstPositionData.Prepend(new ListPositionEntities.PositionData() { id = 0, positionName = "Tất cả" });
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
        #endregion

        #region Hover Event



        private void bod_TimKiem_MouseEnter(object sender, MouseEventArgs e)
        {
            bod_TimKiem.BorderThickness = new Thickness(2);
        }

        private void bod_TimKiem_MouseLeave(object sender, MouseEventArgs e)
        {
            bod_TimKiem.BorderThickness = new Thickness(0);
        }

        private void bod_ViTriNhanVien_MouseEnter(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bodThayDoiViTri = FindChild<Border>(row, "bodThayDoiViTri");

                if (bodThayDoiViTri != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodThayDoiViTri.Visibility = Visibility.Visible;
                }
            }
        }
        private void bod_ViTriNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bodThayDoiViTri = FindChild<Border>(row, "bodThayDoiViTri");

                if (bodThayDoiViTri != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodThayDoiViTri.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void bod_LuanChuyenNhanVien_MouseEnter(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bodLuanChuyenPhongBan = FindChild<Border>(row, "bodLuanChuyenPhongBan");

                if (bodLuanChuyenPhongBan != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodLuanChuyenPhongBan.Visibility = Visibility.Visible;
                }
            }
        }
        private void bod_LuanChuyenNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bodLuanChuyenPhongBan = FindChild<Border>(row, "bodLuanChuyenPhongBan");

                if (bodLuanChuyenPhongBan != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodLuanChuyenPhongBan.Visibility = Visibility.Collapsed;
                }
            }
        }
        private void bod_XoaNhanVien_MouseEnter(object sender, MouseEventArgs e)
        {

            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bodXoaNhanVien = FindChild<Border>(row, "bodXoaNhanVien");

                if (bodXoaNhanVien != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodXoaNhanVien.Visibility = Visibility.Visible;
                }
            }
        }
        private void bod_XoaNhanVien_MouseLeave(object sender, MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                Border bodXoaNhanVien = FindChild<Border>(row, "bodXoaNhanVien");

                if (bodXoaNhanVien != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodXoaNhanVien.Visibility = Visibility.Collapsed;
                }
            }
        }
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

        #region Click Event


        private void bod_ViTriNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThayDoiViTri());
        }



        private void bod_TimKiem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            paginNV.SelectedPage = 0;
            LoadDanhSachNhanVien(pageNumber = 1);

        }

        #endregion

        private void cboTenToChuc_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboTenToChuc.SelectedIndex = -1;
                string textSearch = cboTenToChuc.Text;
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cboTenToChuc_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboTenToChuc.SelectedIndex = -1;
            string textSearch = cboTenToChuc.Text + e.Text;
            cboTenToChuc.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboTenToChuc.Text = "";
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData;
                cboTenToChuc.SelectedIndex = -1;
            }
            else
            {
                cboTenToChuc.ItemsSource = "";
                cboTenToChuc.Items.Refresh();
                cboTenToChuc.ItemsSource = lstOrganizeData.Where(t => t.organizeDetailName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }

        }
        private void cboViTri_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboViTri.SelectedIndex = -1;
                string textSearch = cboViTri.Text;
                cboViTri.Items.Refresh();
                cboViTri.ItemsSource = lstPositionData.Where(t => t.positionName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cboViTri_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboViTri.SelectedIndex = -1;
            string textSearch = cboViTri.Text + e.Text;
            cboViTri.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboViTri.Text = "";
                cboViTri.Items.Refresh();
                cboViTri.ItemsSource = lstPositionData;
                cboViTri.SelectedIndex = -1;
            }
            else
            {
                cboViTri.ItemsSource = "";
                cboViTri.Items.Refresh();
                cboViTri.ItemsSource = lstPositionData.Where(t => t.positionName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }
        private void cboHoVaTen_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cboHoVaTen.SelectedIndex = -1;
                string textSearch = cboHoVaTen.Text;
                cboHoVaTen.Items.Refresh();
                cboHoVaTen.ItemsSource = lstNhanVien.Where(t => t.userName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cboHoVaTen_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cboHoVaTen.SelectedIndex = -1;
            string textSearch = cboHoVaTen.Text + e.Text;
            cboHoVaTen.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cboHoVaTen.Text = "";
                cboHoVaTen.Items.Refresh();
                cboHoVaTen.ItemsSource = lstNhanVien;
                cboHoVaTen.SelectedIndex = -1;
            }
            else
            {
                cboHoVaTen.ItemsSource = "";
                cboHoVaTen.Items.Refresh();
                cboHoVaTen.ItemsSource = lstNhanVien.Where(t => t.userName.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

    }
}