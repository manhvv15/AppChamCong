﻿using ChamCong365.NhanVien.KindOfDon.LichLamViec;
using ChamCong365.NhanVien.Propose;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
using static ChamCong365.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;

namespace ChamCong365.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for DeXuatHoaHongDoanhThu.xaml
    /// </summary>
    public partial class DeXuatHoaHongDoanhThu : UserControl
    {
        MainChamCong Main;
        public DeXuatHoaHongDoanhThu(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            getData();
            getData1();
        }
        public async void getData()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/showadd"); request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {

                    // Xử lý phản hồi ở đây
                    ChamCong365.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.XetDuyetTheoDoi api = JsonConvert.DeserializeObject<ChamCong365.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.XetDuyetTheoDoi>(responseContent);
                    if (api.data.listUsersDuyet != null)
                    {
                        lsvNguoiXetDuyet.ItemsSource = api.data.listUsersDuyet;
                        listUsersDuyets = api.data.listUsersDuyet;
                        //listUsersTheoDoi = lsvNguoiTheoDoi;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại! " + ex.Message);
            }
        }
        public void lsvNguoiXetDuyet_PreviewMouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            scrollNguoiXetDuyet.ScrollToVerticalOffset(scrollNguoiXetDuyet.VerticalOffset - e.Delta);
        }
        public void lsvNguoiXetDuyet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lsvNguoiXetDuyet.SelectedItem != null)
            {
                string selectedUserName = ((ListUsersDuyet)lsvNguoiXetDuyet.SelectedItem).userName;
                if (!ListXet.Any(item => item.userName == selectedUserName))
                {
                    ListUsersDuyet infor = new ListUsersDuyet()
                    {
                        userName = ((ListUsersDuyet)lsvNguoiXetDuyet.SelectedItem).userName,
                        idQLC = ((ListUsersDuyet)lsvNguoiXetDuyet.SelectedItem).idQLC
                    };

                    ListXet.Add(infor);
                    ListXet = ListXet.ToList();
                    listXetDuyet.ItemsSource = ListXet;
                    listXetDuyet.Visibility = Visibility.Visible;
                    borNguoiXetDuyet.Visibility = Visibility.Collapsed;

                }
            }
        }
        public List<ListUsersDuyet> listXet = new List<ListUsersDuyet>();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<ListUsersDuyet> ListXet
        {
            get { return listXet; }
            set
            {
                if (listXet != value)
                {
                    listXet = value;
                    OnPropertyChanged(nameof(ListXet));
                }
            }
        }
        List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet> listUsersDuyets = new List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet>();

        List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet> listUsersDuyetsTimKiem = new List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet>();
        List<OOP.NhanVien.DonDeXuat.CaLamViec.Item> listCaLamViec = new List<OOP.NhanVien.DonDeXuat.CaLamViec.Item>();
        public void textSearchNguoiXetDuyet_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet> listUsersDuyetTimKiem = new List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet>();
            string searchText = textSearchNguoiXetDuyet.Text.ToString().ToLower();
            foreach (var str in listUsersDuyets)
            {
                if (str.userName.ToLower().Contains(searchText))
                {
                    listUsersDuyetTimKiem.Add(str);

                }
            }
            lsvNguoiXetDuyet.ItemsSource = listUsersDuyetTimKiem;

            if (textSearchNguoiXetDuyet.Text == "")
            {
                lsvNguoiXetDuyet.ItemsSource = listUsersDuyets;
            }
        }
        public void xoaAnh_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;

        }
        public void xoaAnh_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.Aqua);
            ((Border)sender).Background = redBrush;
        }
        public void xoaAnh_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ListUsersDuyet index = (ListUsersDuyet)listXetDuyet.SelectedItem;
            if (index != null)
            {
                ListXet.Remove(index);
                listXetDuyet.ClearValue(ItemsControl.ItemsSourceProperty);
                listXetDuyet.ItemsSource = ListXet;
                shouldProcessEvent = false;
            }
            shouldProcessEvent = true;
            if (listXet.Count == 0)
            {
                borNhapNgXetD.Visibility = Visibility.Visible;
                listXetDuyet.Visibility = Visibility.Collapsed;
                textNg.Text = "Nhập người xét duyệt";

            }
        }
        public bool shouldProcessEvent = true;
        public void Grid_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
        {
            if (shouldProcessEvent)
            {
                if (borNguoiXetDuyet.Visibility == Visibility.Collapsed)
                {
                    borNguoiXetDuyet.Visibility = Visibility.Visible;
                    //listXetDuyet.Visibility = Visibility.Visible;

                }
                else
                {
                    borNguoiXetDuyet.Visibility = Visibility.Collapsed;

                }
            }
        }
        List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersTheoDoi> listUsersTheoDoi1 = new List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersTheoDoi>();
        private async void getData1()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/showadd");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();


                if (response.IsSuccessStatusCode)
                {

                    // Xử lý phản hồi ở đây
                    ChamCong365.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.XetDuyetTheoDoi api = JsonConvert.DeserializeObject<ChamCong365.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.XetDuyetTheoDoi>(responseContent);
                    if (api.data.listUsersTheoDoi != null)
                    {
                        lsvNguoiTheoDoi.ItemsSource = api.data.listUsersTheoDoi;
                        listUsersTheoDoi1 = api.data.listUsersTheoDoi;
                        //listUsersTheoDoi = lsvNguoiTheoDoi;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại! " + ex.Message);
            }

        }
        public int idTheoDoi;
        private void borTenChonLoai_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            ListUsersTheoDoi d = (sender as Border).DataContext as ListUsersTheoDoi;
            if (d != null)
            {
                textNguoiTheoDoi.Text = d.userName;
                d.idQLC = d.idQLC;
                SolidColorBrush redBrush = new SolidColorBrush(Colors.Aqua);
                borTheoDoi.Background = redBrush;
                //idTheoDoi = ((ListUsersTheoDoi)lsvNguoiTheoDoi.SelectedItem).idQLC;
            }
        }
        private void Grid_MouseLeftButtonUp_4(object sender, MouseButtonEventArgs e)
        {
            if (borNguoiTheoDoi.Visibility == Visibility.Collapsed)
            {

                borNguoiTheoDoi.Visibility = Visibility.Visible;

            }
            else
            {
                borNguoiTheoDoi.Visibility = Visibility.Collapsed;

            }
        }
        private void lsvNguoiTheoDoi_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNguoiTheoDoi.ScrollToVerticalOffset(scrollNguoiTheoDoi.VerticalOffset - e.Delta);
        }
        private void textSearchNguoiTheoDoi_TextChanged(object sender, TextChangedEventArgs e)
        {
            List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersTheoDoi> listUsersTheoDoi1TimKiem = new List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersTheoDoi>();
            string searchText = textSearchNguoiTheoDoi.Text.ToString().ToLower();
            foreach (var str in listUsersTheoDoi1)
            {
                if (str.userName.ToLower().Contains(searchText))
                {
                    listUsersTheoDoi1TimKiem.Add(str);

                }
            }
            lsvNguoiTheoDoi.ItemsSource = listUsersTheoDoi1TimKiem;

            if (textSearchNguoiTheoDoi.Text == "")
            {
                lsvNguoiTheoDoi.ItemsSource = listUsersTheoDoi1;
            }
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/addDXHH");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(textNhapTenDeXuat.Text), "name_dx");
                int id;
                List<StringContent> NgXeDuyet = new List<StringContent>();
                List<string> listString = new List<string>();
                foreach (var item in ListXet)
                {
                    id = item.idQLC;
                    string idString = Convert.ToString(id);

                    listString.Add(idString + ",");

                }
                for (int i = 0; i < listString.Count; i++)
                {
                    if (listString[i].EndsWith(",") && i == listString.Count - 1)
                    {
                        listString[i] = listString[i].Substring(0, listString[i].Length - 1);
                    }
                    //listString = Convert.ToString(listString);
                }
                string idXetDuyet = string.Join("", listString);
                //MessageBox.Show(Convert.ToString(idXetDuyet));
                content.Add(new StringContent(Convert.ToString(idXetDuyet)), "id_user_duyet");
                idTheoDoi = ((ListUsersTheoDoi)lsvNguoiTheoDoi.SelectedItem).idQLC;
                content.Add(new StringContent(Convert.ToString(idTheoDoi)), "id_user_theo_doi");
                content.Add(new StringContent(textNhapLiDo.Text), "ly_do");
                content.Add(new StringContent("2023-06-14T03:19:09.426Z"), "time_hh");
                content.Add(new StringContent("6 tháng"), "chu_ky");
                
                content.Add(new StringContent("50000"), "dt_money");
                content.Add(new StringContent(""), "item_mdt_date");
               // content.Add(new StreamContent(File.OpenRead("/C:/Users/FPTSHOP/Desktop/anh bai tap/áo 1.jfif")), "file_kem", "/C:/Users/FPTSHOP/Desktop/anh bai tap/áo 1.jfif");
                content.Add(new StringContent(""), "name_dt");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TaoDeXuatThanhCong uc = new TaoDeXuatThanhCong(Main);
                    //Main.dopBody.Children.Clear();
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
