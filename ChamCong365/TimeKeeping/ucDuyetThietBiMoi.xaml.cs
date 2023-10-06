using ChamCong365.OOP.ChamCong;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ChamCong365.OOP.ChamCong.DuyetThietBiMoi;
using System.Net.Http;
using System;
using Newtonsoft.Json;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec;
using ChamCong365.Popup.ChamCong.DuyetThietBiMoi;
using System.Net;
using ChamCong365.APIs;

namespace ChamCong365.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucConfirmationNewDevice.xaml
    /// </summary>

    public partial class ucDuyetThietBiMoi : UserControl, INotifyPropertyChanged
    {
        MainWindow Main;
        int com_id;
        int dep_id;
        int ep_id;
        int TotalItem = 0;
        int SoDu = 0;
        int TongSoTrang = 0;
        List<Item_TBDuyet> lstPTDuyet = new List<Item_TBDuyet>();
        
        int pageSize = 10;
       
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        BrushConverter bc = new BrushConverter();
        public ucDuyetThietBiMoi(MainWindow main)
        {
            InitializeComponent();

            this.DataContext = this;
            this.Main = main;
            com_id = Main.IdAcount;
            dgvDuyetThietBi.ItemsSource = lstTest;
            LoadTBDuyet();
            LoadlistPB(com_id);
            LoadListChillCompany();
            LoadListSaff(dep_id);
        }
        private void XoaDuyet(object sender, MouseButtonEventArgs e)
        {
            Item_TBDuyet phongban = (sender as Border).DataContext as Item_TBDuyet;
            if (phongban != null)
            {
                Main.grShowPopup.Children.Add(new ucXoaDuyet(Main, phongban.ed_id, this));
            } 
        }
        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodPhongBan.Visibility == Visibility.Visible)
            {
                bodPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;

            }
            else if (borNhanVien.Visibility == Visibility.Visible)
            {
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;

            }
        }

        private void btnLoc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           var listTBDuyetFilter = new List<Item_TBDuyet>();
            if (com_id != 0 && dep_id !=0 || ep_id != 0)
            {
                listTBDuyetFilter = listTBDuyet.Where(x => x.com_id == com_id && x.dep_id == dep_id && x.ep_id == ep_id).ToList();
                dgvDuyetThietBi.ItemsSource = listTBDuyetFilter;
                LoadTableData(listTBDuyetFilter);
            }
            LoadTableData(listTBDuyetFilter);
        }

        #region Call API
        private List<Item_TBDuyet> _listTBDuyet;
        public List<Item_TBDuyet> listTBDuyet
        {
            get { return _listTBDuyet; }
            set { _listTBDuyet = value; OnPropertyChanged(); }
        }

        List<Item_TBDuyet> lstTest = new List<Item_TBDuyet>();
        public async void LoadTBDuyet()
        {
            try
            {
                 var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Duyet_ThietBi_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id.ToString() != null)
                {
                    content.Add(new StringContent(com_id.ToString()), "com_id");
                }
                if (dep_id.ToString() != null)
                {
                    content.Add(new StringContent(dep_id.ToString()), "de_id");
                }
                if (ep_id.ToString() != null)
                {
                    content.Add(new StringContent(ep_id.ToString()), "ep_id");
                }
                request.Content = content;
                var response = await client.SendAsync(request);
                var resContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    Root_TBDuyet resultTBD = JsonConvert.DeserializeObject<Root_TBDuyet>(resContent);
                    if (resultTBD.data.items != null)
                    {
                        TotalItem = resultTBD.data.totalItems;
                        listTBDuyet = resultTBD.data.items;
                    }
                    foreach (Item_TBDuyet item in listTBDuyet)
                    {
                        if (item.ep_image == "")
                        {
                            item.ep_image = "https://tinhluong.timviec365.vn/img/add.png";
                        }
                        else
                        {
                            item.ep_image = "https://chamcong.24hpay.vn/upload/employee/" + item.ep_image;
                        }
                    }
                }
                LoadTableData(listTBDuyet);

            }
            catch (System.Exception)
            { 
            }  
        }
        #endregion

        #region even1

        private void CheckDeviceAll_Checked(object sender, RoutedEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Visible;
        }

        private void CheckDeviceAll_Unchecked(object sender, RoutedEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Collapsed;
        }

        
        private void bodOkMessageYesAll_MouseUp(object sender, MouseButtonEventArgs e)
        {
            bodMessageboxYesAll.Visibility = Visibility.Collapsed;
        }
       

       

        #endregion

        #region Checked 
        private bool _IsCheckAll = false;
        private List<string> ListIDSelected = new List<string>();
        public bool IsCheckAll
        {
            get { return _IsCheckAll; }
            set { _IsCheckAll = value; OnPropertyChanged(); }
        }
        private void btnDuyet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                foreach (var item in ListIDSelected)
                {
                    using (WebClient http = new WebClient())
                    {
                        var apilink = APIs.API.XacNhan_Duyet_Api;
                        var pram = new List<string>();
                        http.Headers.Add("Authorization","Bearer " + Properties.Settings.Default.Token);
                        http.QueryString.Add("arr_ed_id", item);
                        http.UploadValuesCompleted += (sender1, e1) =>
                        {
                            Main.grShowPopup.Children.Add(new ucThongBaoDuyetID(Main));
                            int index = listTBDuyet.FindIndex(i => i.ed_id == item);
                            listTBDuyet.RemoveAt(index);
                            listTBDuyet = listTBDuyet.ToList();
                            LoadTableData(listTBDuyet);
                        };
                        http.UploadValuesTaskAsync(new Uri(apilink), http.QueryString);
                    }
                }
            }
            catch (Exception)
            {}
            
        }
        private void DuyetThietBiTatCa_Checked(object sender, RoutedEventArgs e)
        {

            if (IsCheckAll == false)
            {
                ListIDSelected.Clear();
                foreach (var item in listTBDuyet)
                {
                    item.isCheck = true;
                    ListIDSelected.Add(item.ed_id);
                }
                IsCheckAll = true;
            } 
        }

        private void DuyetThietBiTatCa_Unchecked(object sender, RoutedEventArgs e)
        {
            if (IsCheckAll == true)
            {
                ListIDSelected.Clear();
                foreach (var item in listTBDuyet)
                {
                    item.isCheck = false;
                }
                IsCheckAll = false;
            }
        }
        private void DuyetThietBi_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Item_TBDuyet tb = (Item_TBDuyet)cb.DataContext;
            if (tb.isCheck == false)
            {
                ListIDSelected.Add(tb.ed_id.ToString());
                tb.isCheck = true;
            }

        }
        private void DuyetThietBi_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            Item_TBDuyet tb = (Item_TBDuyet)cb.DataContext;
            if (tb.isCheck == true)
            {  
                tb.isCheck = false;
                ListIDSelected.Remove(tb.ed_id.ToString());
            }
        }
        private void cboChonPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }
        private void bodCheckBoxFaceSelected_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
        #endregion

        #region Nhân viên
        private List<ItemAllSaff> _lstAllSaff;
        public List<ItemAllSaff> lstAllSaff
        {
            get { return _lstAllSaff; }
            set { _lstAllSaff = value; }
        }

        private List<ItemAllSaff> _lstAllSaff1;
        public List<ItemAllSaff> lstAllSaff1
        {
            get { return _lstAllSaff1; }
            set { _lstAllSaff1 = value; }
        }
        public async void LoadListSaff(int dep_id)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_Saff_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
               if (this.com_id !=0)
                {
                    content.Add(new StringContent(this.com_id.ToString()), "com_id");
                } 
                if (this.dep_id !=0)
                {
                    content.Add(new StringContent(dep_id.ToString()), "dep_id");
                }
                content.Add(new StringContent(pageSize.ToString()), "pageSize");
                content.Add(new StringContent("1"), "pageNumber");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();
                RootAllSaff result = JsonConvert.DeserializeObject<RootAllSaff>(resContent);
                if (result.data.items != null)
                {

                    lstAllSaff = result.data.items;
                    lsvNhanVien.ItemsSource = lstAllSaff;

                }
                foreach (ItemAllSaff item in lstAllSaff)
                {
                    if (item.ep_image == "")
                    {
                        item.ep_image = "https://tinhluong.timviec365.vn/img/add.png";
                    }
                    else
                    {
                        item.ep_image = "https://chamcong.24hpay.vn/upload/employee/" + item.ep_image;
                    }
                }
            }
            catch (System.Exception)
            {


            }
        }
        private void borHienThiNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNhanVien.Visibility == Visibility.Collapsed)
            {
                borNhanVien.Visibility = Visibility.Visible;
                bodPhongBan.Visibility = Visibility.Collapsed;
            }
            else
            {
                borNhanVien.Visibility = Visibility.Collapsed;
            }
        }

        private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstAllSaff1 = new List<ItemAllSaff>();
            foreach (var str in lstAllSaff)
            {
                if (str.ep_name.ToLower().RemoveUnicode().Contains(textSearchNhanVien.Text.ToLower().RemoveUnicode()))
                {
                    lstAllSaff1.Add(str);
                }
            }
            lsvNhanVien.ItemsSource = lstAllSaff1;
            if (textSearchNhanVien.Text == "")
            {
                lsvNhanVien.ItemsSource = lstAllSaff1;
            }
        }

        private void InputNameSearch_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiNhanVien.Text = "Nhập tên cần tìm";
        }
    
        private void lsvNhanVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNhanVien.ScrollToVerticalOffset(scrollNhanVien.VerticalOffset - e.Delta);
        }

        private void borTenNhanVien_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ItemAllSaff nv = (sender as Border).DataContext as ItemAllSaff;
            if (nv != null)
            {
                textHienThiNhanVien.Text = nv.ep_name;
                ep_id = nv.ep_id;
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.NhanVien = nv.ep_name.ToString();
            }
        }

        #endregion

        #region Phòng Ban
        private List<Item_PhongBan> _lstPhongBan;
        public List<Item_PhongBan> lstPhongBan
        {
            get { return _lstPhongBan; }
            set { _lstPhongBan = value; }
        }
      
        public async void LoadlistPB(int com_id)
        {
            try
            {
                var client = new HttpClient();
                var request1 = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_Room_Api);
                request1.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
                request1.Content = content;
                var response = await client.SendAsync(request1);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();

                Root_PhongBan result = JsonConvert.DeserializeObject<Root_PhongBan>(resContent);

                if (result.data.items != null)
                {
                    lstPhongBan = result.data.items;
                    Item_PhongBan dep = new Item_PhongBan();
                    dep.dep_id = 0;
                    dep.dep_name = "Phòng ban (tất cả)";
                    lstPhongBan.Insert(0, dep);
                    lsvPhongBan.ItemsSource = lstPhongBan;
                    var phongban = lstPhongBan
                        .Where(Item_PhongBan => Item_PhongBan.dep_id == 0)
                        .Select(Item_PhongBan => Item_PhongBan.dep_name)
                        .FirstOrDefault();
                    txtHienThiPhongBan.Text = phongban;
                }

            }
            catch (Exception) { }
        }
        private void bodChonPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodPhongBan.Visibility == Visibility.Collapsed)
            {
                bodPhongBan.Visibility = Visibility.Visible;
                borNhanVien.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodPhongBan.Visibility = Visibility.Collapsed;
            }
        }

        private void bodTenPhongBan_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Item_PhongBan phongban = (sender as Border).DataContext as Item_PhongBan;
            if (phongban != null)
            {
                txtHienThiPhongBan.Text = phongban.dep_name;
                dep_id = phongban.dep_id;
                bodPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.PhongBan = phongban.dep_name.ToString();
            }
            LoadListSaff(dep_id);
            LoadTBDuyet();
        }

        private void lsvPhongBan_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is System.Windows.Controls.ListView && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((System.Windows.Controls.Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
        }

        #endregion#

        #region Công Ty
        private List<Item_CtyCon> _ctyCon;
        public List<Item_CtyCon> ctyCon
        {
            get { return _ctyCon; }
            set { _ctyCon = value; OnPropertyChanged(); }
        }
        public async void LoadListChillCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.List_ChillCompany_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();

                Root_CongTyCon result = JsonConvert.DeserializeObject<Root_CongTyCon>(resContent);

                if (result.data.items != null)
                {
                    ctyCon = result.data.items;
                    Item_CtyCon chillcom = new Item_CtyCon();
                    chillcom.com_id = Main.IdAcount;
                    chillcom.com_name = "Chọn công ty";
                    ctyCon.Insert(0, chillcom);
                    cboDSCongTy.ItemsSource = ctyCon;
                    var congty = ctyCon
                        .Where(Item_CtyCon => Item_CtyCon.com_id == Main.IdAcount)
                        .Select(Item_CtyCon => Item_CtyCon.com_name)
                        .FirstOrDefault();
                    cboDSCongTy.Text = congty.ToString();
                    cboDSCongTy.SelectedIndex = 0;

                }

            }
            catch (Exception)
            {
            }

        }
        private void cboDSCongTy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cty = cboDSCongTy.SelectedItem as Item_CtyCon;
            if (cty != null)
            {
                LoadlistPB(com_id);
                LoadlistPB(cty.com_id);
            }
        }

        #endregion

        #region Phân Trang 2
        public void LoadTableData(List<Item_TBDuyet> listTBDuyet)
        {
            lstPTDuyet = new List<Item_TBDuyet>();
            // ẩn phân trang khi không có dũ liệu
            if (listTBDuyet.Count > 0)
            {
                TotalItem = listTBDuyet.Count;
                TongSoTrang = (int)Math.Ceiling(TotalItem / 10.0);
                SoDu = 10 - (TotalItem % 10);
                var numberOfItem = (TongSoTrang == 1) ? TotalItem : 10;
                for (int i = 0; i < numberOfItem; i++)
                {
                    lstPTDuyet.Add(listTBDuyet[i]);
                }
                dgvDuyetThietBi.ItemsSource = lstPTDuyet;


                textPage1.Text = "1";
                textPage2.Text = "2";
                textPage3.Text = "3";
                borLui1.Visibility = Visibility.Collapsed;
                borPageDau.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                borPageCuoi.Visibility = Visibility.Collapsed;

                if (TongSoTrang <= 3)
                {
                    borPage3.Visibility = Visibility.Visible;

                    borLen1.Visibility = Visibility.Collapsed;
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    if (TongSoTrang == 2)
                    {
                        borPage3.Visibility = Visibility.Collapsed;
                        borPage2.Visibility = Visibility.Visible;

                    }
                    if (TongSoTrang == 1)
                    {
                        borPage3.Visibility = Visibility.Collapsed;
                        borPage2.Visibility = Visibility.Collapsed;

                    }
                }
                else
                {
                    borLui1.Visibility = Visibility.Collapsed;
                    borPageDau.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Visible;
                    borPageCuoi.Visibility = Visibility.Visible;
                }
            }
            else
            {
                lstPTDuyet = new List<Item_TBDuyet>();
                borLui1.Visibility = Visibility.Collapsed;
                borPageDau.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                borPageCuoi.Visibility = Visibility.Collapsed;
                borPage1.Visibility = Visibility.Visible;
                borPage2.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
            }
        }

        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            textPage1.Text = "1";
            textPage2.Text = "2";
            textPage3.Text = "3";
            borLen1.Visibility = Visibility.Visible;
            borPageCuoi.Visibility = Visibility.Visible;
            lstPTDuyet = new List<Item_TBDuyet>();
            for (int i = 0; i < 10; i++)
            {
                if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
            }
            dgvDuyetThietBi.ItemsSource = lstPTDuyet;
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage3.Text == TongSoTrang.ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                {
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    lstPTDuyet = new List<Item_TBDuyet>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                    }
                    
                    listTBDuyet = lstPTDuyet;
                }
                else
                {
                    if (textPage1.Text == "1")
                    {
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                        lstPTDuyet = new List<Item_TBDuyet>();
                        for (int i = 0; i < 10; i++)
                        {
                            if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                        }
                        listTBDuyet = lstPTDuyet;
                        
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstPTDuyet = new List<Item_TBDuyet>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                        }
                     
                        listTBDuyet = lstPTDuyet;
                    }


                }
            }

        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage1.Text == (TongSoTrang - 2).ToString() && borPageCuoi.Visibility == Visibility.Collapsed && TongSoTrang > 3)
                {

                    textPage1.Text = (TongSoTrang - 3).ToString();
                    textPage2.Text = (TongSoTrang - 2).ToString();
                    textPage3.Text = (TongSoTrang - 1).ToString();


                    BrushConverter brus = new BrushConverter();

                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    if (TongSoTrang > 2)
                    {
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                    }

                    lstPTDuyet = new List<Item_TBDuyet>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                    }
                   
                    listTBDuyet = lstPTDuyet;
                }
                else
                {

                    if (textPage1.Text == "1")
                    {
                        BrushConverter brus = new BrushConverter();
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        if (TongSoTrang > 3)
                        {
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }

                        lstPTDuyet = new List<Item_TBDuyet>();
                        var numberOfItem = (TongSoTrang == 1) ? TotalItem : 10;
                        for (int i = 0; i < numberOfItem; i++)
                        {
                            if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                        }
                 
                        listTBDuyet = lstPTDuyet;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstPTDuyet = new List<Item_TBDuyet>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                        }
                      
                        listTBDuyet = lstPTDuyet;
                    }
                }
            }
        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            lstPTDuyet = new List<Item_TBDuyet>();
            var numOfData = (TongSoTrang == 2) ? TotalItem : int.Parse(textPage2.Text) * 10;
            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < numOfData; i++)
            {
                if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
            }
            if (TongSoTrang > 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;

                if (textPage2.Text == (TongSoTrang - 1).ToString())
                {
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                }
            }
           
            listTBDuyet = lstPTDuyet;

        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TongSoTrang == 3)
            {
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstPTDuyet = new List<Item_TBDuyet>();
                for (int i = TongSoTrang * 10 - 10; i < TotalItem; i++)
                {
                    if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                }
           
                listTBDuyet = lstPTDuyet;
            }
            else if (TongSoTrang > 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                if (textPage3.Text == TongSoTrang.ToString())
                {

                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    lstPTDuyet = new List<Item_TBDuyet>();

                    for (int i = TongSoTrang * 10 - 10; i < TotalItem; i++)
                    {
                        if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                    }
                    dgvDuyetThietBi.ItemsSource = lstPTDuyet;
                }
                else if (textPage3.Text == "3")
                {
                    textPage1.Text = "2";
                    textPage2.Text = "3";
                    textPage3.Text = "4";
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    lstPTDuyet = new List<Item_TBDuyet>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                    }
                   
                    listTBDuyet = lstPTDuyet;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    lstPTDuyet = new List<Item_TBDuyet>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                    }
                   
                    listTBDuyet = lstPTDuyet;
                }

            }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (TongSoTrang == 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstPTDuyet  = new List<Item_TBDuyet>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                }
                
                listTBDuyet = lstPTDuyet;
            }
            else if (TongSoTrang > 3)
            {
                if (textPage3.Text == TongSoTrang.ToString())
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    lstPTDuyet = new List<Item_TBDuyet>();
                    for (int i = TongSoTrang * 10 - 10; i < TotalItem; i++)
                    {
                        if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                    }
                   
                    listTBDuyet = lstPTDuyet;

                }
                else if (textPage3.Text == "3")
                {

                    if (borPageDau.Visibility == Visibility.Collapsed && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                        lstPTDuyet = new List<Item_TBDuyet>();
                        for (int i = 10; i < 20; i++)
                        {
                            if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                        }
                       
                        listTBDuyet = lstPTDuyet;

                    }
                    else if (borPageDau.Visibility == Visibility.Visible && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        textPage1.Text = "2";
                        textPage2.Text = "3";
                        textPage3.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        lstPTDuyet = new List<Item_TBDuyet>();
                        for (int i = 20; i < 30; i++)
                        {
                            if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                        }
                     
                        listTBDuyet = lstPTDuyet;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    lstPTDuyet = new List<Item_TBDuyet>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
                    }
                   
                    listTBDuyet = lstPTDuyet;
                }

            }
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textPage3.Text = TongSoTrang.ToString();
            textPage2.Text = (TongSoTrang - 1).ToString();
            textPage1.Text = (TongSoTrang - 2).ToString();
            borPageDau.Visibility = Visibility.Visible;
            borLui1.Visibility = Visibility.Visible;
            BrushConverter brus = new BrushConverter();
            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPageCuoi.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            lstPTDuyet = new List<Item_TBDuyet>();
            for (int i = TongSoTrang * 10 - 10; i < TotalItem; i++)
            {
                if (listTBDuyet != null) lstPTDuyet.Add(listTBDuyet[i]);
            }

          
            listTBDuyet = lstPTDuyet;
        }
        #endregion

    }
}
