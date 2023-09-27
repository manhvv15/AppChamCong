using ChamCong365.NhanVien.KindOfDon.LichLamViec;
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
    /// Interaction logic for DeXuatBoNhiem.xaml
    /// </summary>
    public partial class DeXuatBoNhiem : UserControl
    {
        MainChamCong Main;
        public class ChonChucVu
        {
            public string ChucVu { get; set; }
        }
        public class PhongBan1
        {
            public string PhongBan { get; set; }
        }
        public class ThanhVien
        {
            public string thanhVien { get; set; }
        }
       
        List<ChonChucVu> listChucVu = new List<ChonChucVu>();
        List<PhongBan1> listPhongBan = new List<PhongBan1>();
        List<ThanhVien> listThanhVien = new List<ThanhVien>();
        private void LoadChucVu()
        {

            listChucVu.Add(new ChonChucVu { ChucVu = "Sinh Viên Thực Tập " });
            listChucVu.Add(new ChonChucVu { ChucVu = "Sinh viên Part Time" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Nhân viên Chính Thức" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Trưởng Nhóm" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Nhóm Phó" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Tổ Trưởng" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Tổ Trưởng" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Trưởng Ban Dự Án" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Ban Dự Án" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Trưởng Phòng" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Trưởng Phòng" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Giám Đốc" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Giám Đốc" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Ban Dự Án" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Tổng Giám Đốc" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Tổng Giám Đốc" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Tổng Giám Đốc Tập Đoàn" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Tổng Giám Đốc Tập Đoàn" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Chủ Tịch Hội Đồng Quản Trị" });
            listChucVu.Add(new ChonChucVu { ChucVu = "Phó Chủ Tịch Hội Đồng Quản Trị" });
            lsvChonChucVu.ItemsSource = listChucVu;
        }
        private void LoadPhongBan()
        {

            listPhongBan.Add(new PhongBan1 { PhongBan = "Kỹ Thuật" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Biên Tập" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Nhân viên Chính Thức" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Kinh Doanh" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Nhóm Phó" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Đề Án" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Phòng Seo" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Phòng Đào Tạo" });
            listPhongBan.Add(new PhongBan1 { PhongBan = "Phòng Sáng Tạo" });
           
            lsvPhongBan.ItemsSource = listPhongBan;
        }
        private void LoadThanhVien()
        {

            listThanhVien.Add(new ThanhVien { thanhVien = "Kỹ Thuật" });
            listThanhVien.Add(new ThanhVien { thanhVien = "Biên Tập" });
            listThanhVien.Add(new ThanhVien { thanhVien = "Nhân viên Chính Thức" });
            listThanhVien.Add(new ThanhVien { thanhVien = "Kinh Doanh" });
            listThanhVien.Add(new ThanhVien { thanhVien = "Nhóm Phó" });
            listThanhVien.Add(new ThanhVien { thanhVien = "Đề Án" });
            listThanhVien.Add(new ThanhVien { thanhVien = "Phòng Seo" });
            listThanhVien.Add(new ThanhVien { thanhVien = "Phòng Đào Tạo" });
            listThanhVien.Add(new ThanhVien { thanhVien = "Phòng Sáng Tạo" });

            lsvThanhVien.ItemsSource = listThanhVien;
        }
        public DeXuatBoNhiem(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            LoadChucVu();
            LoadPhongBan();
            LoadThanhVien();
            getData();
            getData1();
        
            }


        private void borTenChonLoai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChonChucVu d = (sender as Border).DataContext as ChonChucVu;
            if (d != null)
            {
                textChon.Text = d.ChucVu;

            }
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borChonChucVu.Visibility == Visibility.Collapsed)
            {

                borChonChucVu.Visibility = Visibility.Visible;

            }
            else
            {
                borChonChucVu.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvChonChucVu_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollChonChucVu.ScrollToVerticalOffset(scrollChonChucVu.VerticalOffset - e.Delta);
        }

       

        private void borPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            PhongBan1 d = (sender as Border).DataContext as PhongBan1;
            if (d != null)
            {
                textPhongBan.Text = d.PhongBan; ;

            }
        }


        private void Grid_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            if (borThanhVien.Visibility == Visibility.Collapsed)
            {

                borThanhVien.Visibility = Visibility.Visible;

            }
            else
            {
                borThanhVien.Visibility = Visibility.Collapsed;

            }

        }

        

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ThanhVien d = (sender as Border).DataContext as ThanhVien;
            if (d != null)
            {
                textThanhVien.Text = d.thanhVien; ;

            }
        }

        private void lsvThanhVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollThanhVienn.ScrollToVerticalOffset(scrollThanhVienn.VerticalOffset - e.Delta);

        }

        private void Grid_MouseLeftButtonUp_3(object sender, MouseButtonEventArgs e)
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

        private void lsvPhongBann_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollPhongBann.ScrollToVerticalOffset(scrollPhongBann.VerticalOffset - e.Delta);
        }

        private void borTenChonLoai_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChonChucVu d = (sender as Border).DataContext as ChonChucVu;
            if (d != null)
            {
                textChon.Text = d.ChucVu;

            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            PhongBan1 d = (sender as Border).DataContext as PhongBan1;
            if (d != null)
            {
                textPhongBan.Text = d.PhongBan; ;

            }
        }

        private void Border_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            ThanhVien d = (sender as Border).DataContext as ThanhVien;
            if (d != null)
            {
                textThanhVien.Text = d.thanhVien; ;

            }
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private async void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/dexuat/De_Xuat_Xin_Bo_Nhiem");
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
                content.Add(new StringContent("2342"), "thanhviendc_bn");
                content.Add(new StringContent("2346"), "name_ph_bn");
                content.Add(new StringContent("4"), "chucvu_hientai");
                content.Add(new StringContent("5"), "chucvu_dx_bn");
                content.Add(new StringContent("234"), "phong_ban_moi");
                //content.Add(new StreamContent(File.OpenRead("")), "fileKem", "");
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
    }
}
