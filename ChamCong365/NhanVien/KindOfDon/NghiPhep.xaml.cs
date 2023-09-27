using ChamCong365.NhanVien.Propose;
using ChamCong365.Popup.CaiDatLuong.CaiDatThue;
using MaterialDesignThemes.Wpf;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static ChamCong365.NhanVien.KindOfDon.NghiPhep;
using static ChamCong365.NhanVien.Propose.listTess;
using static ChamCong365.OOP.CaiDatLuong.CongCong.clsDSCongCong;
using static ChamCong365.OOP.NhanVien.DonDeXuat.CaLamViec;
using static ChamCong365.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;
using static ChamCong365.OOP.NhanVien.ToiGuiDi.DeXuatToiGuiDi;

namespace ChamCong365.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for NghiPhep.xaml
    /// </summary>

    public partial class NghiPhep : UserControl, INotifyPropertyChanged
    {
        MainChamCong Main;
        ChamCong365.NhanVien.KindOfDon.LichLamViec.NguoiXetDuyet ListNg;
        public class ChonLoai
        {
            public string Loai { get; set; }
        }
        public class ChonLuong
        {
            public string Luong { get; set; }
        }

        public class NguoiXetDuyet
        {
            public string nguoiXetDuyet { get; set; }
        }
        public class NguoiTheoDoi
        {
            public string nguoiTheoDoi { get; set; }
        }
        public class InforNghiPhep
        {
            public int stt { get; set; }
            public string caNghi { get; set; }
            public string ngayBatDau { get; set; }
            public string ngayKetThuc { get; set; }


        }
        private class XetDuyet
        {
            public string nguoixetduyet { get; set; }
        }
        //List<InforNghiPhep> listInfor = new List<InforNghiPhep>();
        private ObservableCollection<InforNghiPhep> listInfor = new ObservableCollection<InforNghiPhep>();
        List<ChonLoai> listLoai = new List<ChonLoai>();
        List<ChonLuong> listLuong = new List<ChonLuong>();
        List<string> listCaNghi = new List<string>();
        List<NguoiXetDuyet> listNguoiXetDuyet = new List<NguoiXetDuyet>();
        List<NguoiTheoDoi> listNguoiTheoDoi = new List<NguoiTheoDoi>();
        public NghiPhep(MainChamCong main, ChamCong365.NhanVien.KindOfDon.LichLamViec.NguoiXetDuyet listNg)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            LoadChonLoai();
            LoadChonLuong();
            LoadChonCaNghi();
            getData();
            getDataCaLamViec();
            getData1();
            getIdNgXeDuyet();
            ListNg = listNg;
        }

        private void LoadChonLoai()
        {

            listLoai.Add(new ChonLoai { Loai = "1/4 ngày" });
            listLoai.Add(new ChonLoai { Loai = "1/2 ngày" });
            listLoai.Add(new ChonLoai { Loai = "3/4 ngày" });
            listLoai.Add(new ChonLoai { Loai = "Ngày" });
            lsvChonLoai.ItemsSource = listLoai;
        }
        private void LoadChonLuong()
        {

            listLuong.Add(new ChonLuong { Luong = "Không lương" });
            listLuong.Add(new ChonLuong { Luong = "Có lương" });

            lsvLoaiNghiPhep.ItemsSource = listLuong;
        }
        private void LoadChonCaNghi()
        {

            listCaNghi.Add("Nghỉ cả ngày(tất cả các ca)");
            listCaNghi.Add("Ca sáng 7TR < LƯƠNG <= 10TR ");
            listCaNghi.Add("Ca chiều 7TR < LƯƠNG <= 10TR ");

            //lsvCaNghi.ItemsSource = listCaNghi;
        }


        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borChonLoai.Visibility == Visibility.Collapsed)
            {

                borChonLoai.Visibility = Visibility.Visible;

            }
            else
            {
                borChonLoai.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvChonLoai_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollChonLoai.ScrollToVerticalOffset(scrollChonLoai.VerticalOffset - e.Delta);
        }

        private void borTenChonLoai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChonLoai d = (sender as Border).DataContext as ChonLoai;
            if (d != null)
            {
                textChonLoai.Text = d.Loai;

            }
        }

        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (BorLuong.Visibility == Visibility.Collapsed)
            {

                BorLuong.Visibility = Visibility.Visible;

            }
            else
            {
                BorLuong.Visibility = Visibility.Collapsed;

            }
        }

        private void borTenChonLoai_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ChonLuong d = (sender as Border).DataContext as ChonLuong;
            if (d != null)
            {
                textLuong.Text = d.Luong;

            }
        }

        private void borCaNghi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Grid_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            if (BorCaNghi.Visibility == Visibility.Collapsed)
            {

                BorCaNghi.Visibility = Visibility.Visible;

            }
            else
            {
                BorCaNghi.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvCaNghi_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCaNghi.ScrollToVerticalOffset(scrollCaNghi.VerticalOffset - e.Delta);
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

        private void lsvNguoiXetDuyet_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNguoiXetDuyet.ScrollToVerticalOffset(scrollNguoiXetDuyet.VerticalOffset - e.Delta);
        }



        private void borTenChonLoai_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {

        }

        private void lsvNguoiTheoDoi_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNguoiTheoDoi.ScrollToVerticalOffset(scrollNguoiTheoDoi.VerticalOffset - e.Delta);
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


        private void lsvNguoiXetDuyet_PreviewMouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            scrollNguoiXetDuyet.ScrollToVerticalOffset(scrollNguoiXetDuyet.VerticalOffset - e.Delta);
        }

        private void lsvCaNghi_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (NgayBatDau.Text == "" || NgayKetThuc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ngày", "Tìm việc 365 said:", MessageBoxButton.OK, MessageBoxImage.Error);

                BorCaNghi.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (dgv.Visibility == Visibility.Collapsed)
                {
                    dgv.Visibility = Visibility.Visible;
                }
                DateTime date1, date2;
                date1 = DateTime.Parse(NgayBatDau.Text);
                date2 = DateTime.Parse(NgayKetThuc.Text);
                if (date1 <= date2)
                {
                    GetInfor1();
                }
                else
                {
                    MessageBox.Show("Ngày kết thúc nghỉ phải lớn hơn hoặc bằng ngày bắt đầu nghỉ", "Tìm việc 365 said:", MessageBoxButton.OK, MessageBoxImage.Error);
                    BorCaNghi.Visibility = Visibility.Collapsed;
                }


            }

        }


        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        public int count = 1;

        private void GetInfor()
        {
            dgv.ItemsSource = listInfor;

        }
        private void GetInfor1()
        {
            InforNghiPhep infor = new InforNghiPhep()
            {
                caNghi = ((Item)lsvCaLamViec.SelectedItem).shift_name,
                ngayBatDau = NgayBatDau.Text,
                ngayKetThuc = NgayKetThuc.Text
            };
            listInfor.Add(infor);
            infor.stt = listInfor.IndexOf(infor) + 1;
            dgv.ItemsSource = listInfor;
            BorCaNghi.Visibility = Visibility.Collapsed;
        }

        private void Image_MouseUp(object sender, MouseButtonEventArgs e)
        {
            InforNghiPhep index = (InforNghiPhep)dgv.SelectedItem;
            if (index != null)
            {
                listInfor.Remove(index);
                for (int i = 0; i < listInfor.Count; i++)
                {
                    listInfor[i].stt = i + 1;
                };
                dgv.ClearValue(ItemsControl.ItemsSourceProperty);
                dgv.ItemsSource = listInfor;
            }
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            cbDotXuat.IsChecked = false;
        }

        private void CheckBox_Checked_1(object sender, RoutedEventArgs e)
        {
            cbKeHoach.IsChecked = false;
        }


        List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet> listUsersDuyets = new List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet>();
        List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersTheoDoi> listUsersTheoDoi1 = new List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersTheoDoi>();
        List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet> listUsersDuyetsTimKiem = new List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet>();
        List<OOP.NhanVien.DonDeXuat.CaLamViec.Item> listCaLamViec = new List<OOP.NhanVien.DonDeXuat.CaLamViec.Item>();
        //List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet> listIdDuyet = new List<OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi.ListUsersDuyet>();
        private async void getData()
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
        private class IdDuyet
        {
            public int userId;
        }
        List<IdDuyet> listIdDuyet = new List<IdDuyet>();
        private async void getIdNgXeDuyet()
        {
        }
        private async void getDataCaLamViec()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "http://210.245.108.202:3000/api/qlc/shift/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {

                    // Xử lý phản hồi ở đây
                    ChamCong365.OOP.NhanVien.DonDeXuat.CaLamViec.API_CaLamViec api = JsonConvert.DeserializeObject<ChamCong365.OOP.NhanVien.DonDeXuat.CaLamViec.API_CaLamViec>(responseContent);
                    if (api.data.items != null)
                    {
                        lsvCaLamViec.ItemsSource = api.data.items;
                        listCaLamViec = api.data.items;

                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại! " + ex.Message);
            }

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
        public class IDNgTheoDoi
        {
            int id { get; set; }


        }
        int idTheoDoi;
        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/De_Xuat_Xin_Nghi");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(textNhapTenDeXuat.Text), "name_dx");
                if (cbKeHoach.IsChecked == true)
                {
                    content.Add(new StringContent("1"), "loai_np");
                }
                if (cbDotXuat.IsChecked == true)
                {
                    content.Add(new StringContent("2"), "loai_np");
                }
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
                string ngayBatDau = NgayBatDau.Text;
                string ngayKetThuc = NgayKetThuc.Text;
                DateTime ngayBatDauDate = DateTime.Parse(ngayBatDau);
                string ngayBatDauFormat = ngayBatDauDate.ToString("yyyy-MM-ddTHH:mm:ss.fff+00:00");
                DateTime ngayKetThucDate = DateTime.Parse(ngayKetThuc);
                string ngayKetThucFormat = ngayKetThucDate.ToString("yyyy-MM-ddTHH:mm:ss.fff+00:00");
                // // content.Add(new StringContent("{\"nghi_phep\": [[\"2023-12-04T00:00:00.000+00:00\",\"2023-12-05T00:00:00.000+00:00\",\"2\"]]}"), "noi_dung");
               
                StringBuilder jsonStringBuilder = new StringBuilder();
                jsonStringBuilder.Append("{\"nghi_phep\": [[");
                jsonStringBuilder.Append($"\"{ngayBatDauFormat}\", \"{ngayKetThucFormat}\", \"2\"");
                jsonStringBuilder.Append("]]}");
                content.Add(new StringContent(jsonStringBuilder.ToString()), "noi_dung");
                content.Add(new StringContent(textNhapLiDo.Text), "ly_do");

                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("hrr");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        static readonly HttpClient client = new HttpClient();


        ObservableCollection<XetDuyet> lsvxetDUyet = new ObservableCollection<XetDuyet>();


        ObservableCollection<XetDuyet> list = new ObservableCollection<XetDuyet>();
        // List<XetDuyet> list = new List<XetDuyet>();

        private List<ListUsersDuyet> listXet = new List<ListUsersDuyet>();
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private List<ListUsersDuyet> ListXet
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

        private void borTenChonLoai_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListUsersTheoDoi d = (sender as Border).DataContext as ListUsersTheoDoi;
            if (d != null)
            {
                //textNguoiXetDuyet.Text = d.userName;

            }
        }
        private void TextBlock_MouseUp(object sender, MouseButtonEventArgs e)
        {
            listXetDuyet.Visibility = Visibility.Visible;
            textNg.Visibility = Visibility.Collapsed;
        }

        private void lsvCaLamViec_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCaNghi.ScrollToVerticalOffset(scrollCaNghi.VerticalOffset - e.Delta);
        }


        private void lsvCaLamViec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NgayBatDau.Text == "" || NgayKetThuc.Text == "")
            {
                MessageBox.Show("Vui lòng nhập ngày", "Tìm việc 365 said:", MessageBoxButton.OK, MessageBoxImage.Error);

                BorCaNghi.Visibility = Visibility.Collapsed;
            }
            else
            {
                if (dgv.Visibility == Visibility.Collapsed)
                {
                    dgv.Visibility = Visibility.Visible;
                }
                DateTime date1, date2;
                date1 = DateTime.Parse(NgayBatDau.Text);
                date2 = DateTime.Parse(NgayKetThuc.Text);
                if (date1 <= date2)
                {
                    GetInfor1();
                }
                else
                {
                    MessageBox.Show("Ngày kết thúc nghỉ phải lớn hơn hoặc bằng ngày bắt đầu nghỉ", "Tìm việc 365 said:", MessageBoxButton.OK, MessageBoxImage.Error);
                    BorCaNghi.Visibility = Visibility.Collapsed;
                }

                ////Ptu p = new Ptu();
                ////p.canghi
                ////p.Count = IndexOf(p) + 1;
            }
        }



        private void xoaAnh_MouseEnter(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.DarkGray);
            ((Border)sender).Background = redBrush;
        }

        private void xoaAnh_MouseLeave(object sender, MouseEventArgs e)
        {
            SolidColorBrush redBrush = new SolidColorBrush(Colors.Aqua);
            ((Border)sender).Background = redBrush;
        }
        List<ListUsersTheoDoi> listTheoDoi = new List<ListUsersTheoDoi>();

        private void borTenChonLoai_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            ListUsersTheoDoi d = (sender as Border).DataContext as ListUsersTheoDoi;
            if (d != null)
            {
                textNguoiTheoDoi.Text = d.userName;
                d.idQLC = d.idQLC;
                SolidColorBrush redBrush = new SolidColorBrush(Colors.Aqua);
                borTheoDoi.Background = redBrush;
            }
        }

        private void textSearchNguoiXetDuyet_TextChanged(object sender, TextChangedEventArgs e)
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
        public class FileLuong
        {
            public string FileName { get; set; }
        }
        private List<FileLuong> lstFileL = new List<FileLuong>();
        private void Border_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Thiết lập các tùy chọn của OpenFileDialog
            openFileDialog.Title = "Chọn tệp";
            openFileDialog.Filter = "Tất cả các tệp|*.*";
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            if (openFileDialog.ShowDialog() == true)
            {
                // Lấy đường dẫn tệp đã chọn
                string selectedFilePath = openFileDialog.FileName;

                // Xử lý tệp ở đây (ví dụ: hiển thị tên tệp lên một TextBlock)
                tepDinhKem.Text = selectedFilePath;
            }
        }
        private bool shouldProcessEvent = true;
        private void lsvNguoiXetDuyet_SelectionChanged(object sender, SelectionChangedEventArgs e)
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
        private void xoaAnh_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void textNguoiXetDuyet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
