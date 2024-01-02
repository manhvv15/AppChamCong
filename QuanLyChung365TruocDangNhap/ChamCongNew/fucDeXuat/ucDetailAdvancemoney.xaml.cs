using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.Propose;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.fucDeXuat
{
    /// <summary>
    /// Interaction logic for ucDetailAdvancemoney.xaml
    /// </summary>
    public partial class ucDetailAdvancemoney : UserControl
    {
        MainWindow Main;
        ChiTietDeXuat.DetailDeXuat detailDeXuat = new ChiTietDeXuat.DetailDeXuat();
        int dx_id= 0;
        public ucDetailAdvancemoney(MainWindow Main,int dx_id)
        {
            InitializeComponent();
            this.Main = Main;
            this.dx_id = dx_id;
            GetChiTietDeXuat();
        }
        public async void GetChiTietDeXuat()
        {
            try {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.ChiTietDeXuat);
                request.Headers.Add("Authorization", "Bearer "+Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(dx_id.ToString()), "_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();   
                    ChiTietDeXuat.Root result = JsonConvert.DeserializeObject<ChiTietDeXuat.Root>(responseContent);
                    detailDeXuat = result.data.detailDeXuat[0];
                    ShowDL(detailDeXuat);

                }
            }
            catch(Exception ex) {
                MessageBox.Show("Có lỗi xảy ra" + ex.Message);
            }
        }
        private void ShowDL(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            if (detailDeXuat.type_duyet == 5)
            {
                borDaDuyet.Visibility = Visibility.Visible;
                btnDuyet.Visibility = Visibility.Collapsed;
                Btn_HuyDuyet.Visibility = Visibility.Visible;
            }
            else
            {
                borDaDuyet.Visibility = Visibility.Collapsed;
                btnDuyet.Visibility = Visibility.Visible;
                Btn_HuyDuyet.Visibility = Visibility.Collapsed;
            }

            txbNguoiTao.Text = detailDeXuat?.nguoi_tao;
      { 
            txbCreatedDate.Text = DateTimeOffset.FromUnixTimeSeconds(int.Parse(detailDeXuat.thoi_gian_tao.ToString())).ToString();

            }
            txbCapNhat.Text = detailDeXuat?.cap_nhat.ToString();
            txbHoVaTen.Text = detailDeXuat?.nguoi_tao;
            txbSoTienTamUng.Text = detailDeXuat?.thong_tin_chung.tam_ung.sotien_tam_ung.ToString();
            txbNgayTamUng.Text = DateTimeOffset.FromUnixTimeSeconds(int.Parse(detailDeXuat?.thong_tin_chung.tam_ung.ngay_tam_ung.ToString())).ToString();
            txbLyDo.Text = detailDeXuat?.thong_tin_chung.tam_ung.ly_do;
            txbKieuDuyet.Text = detailDeXuat?.kieu_phe_duyet.ToString();
            lsvLanhDaoDuyet.ItemsSource = detailDeXuat?.lanh_dao_duyet;
            lsvNguoiTheoDoi.ItemsSource = detailDeXuat?.nguoi_theo_doi;
            lsvLichSuDuyet.ItemsSource = detailDeXuat?.lich_su_duyet;


        }
        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ucAdvancemoney uc = new ucAdvancemoney(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

    }
}
