using Newtonsoft.Json;
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
using static ChamCong365.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;

namespace ChamCong365.NhanVien.KindOfDon.LichLamViec
{
    /// <summary>
    /// Interaction logic for NguoiTheoDoi.xaml
    /// </summary>
    public partial class NguoiTheoDoi : UserControl
    {
        public NguoiTheoDoi()
        {
            InitializeComponent();
            getData1();
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
    }
}
