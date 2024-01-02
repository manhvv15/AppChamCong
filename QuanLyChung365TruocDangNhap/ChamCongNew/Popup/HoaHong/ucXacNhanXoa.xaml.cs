using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.CaiDatHoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.HoaHongNhanDuoc;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.HoaHong;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong
{
    /// <summary>
    /// Interaction logic for ucXacNhanXoa.xaml
    /// </summary>
    public partial class ucXacNhanXoa : UserControl
    {
        MainWindow Main;
        ListThietLap lstRose = new ListThietLap();
        ObservableCollection<ListThietLap> lstLoadRose = new ObservableCollection<ListThietLap>();
        ucCaiDatHoaHongDoanhThu ucDoanhThu;
        ucCaiDatHoaHongLoiNhuan ucLoiNhuan;
        ucCaiDatHoaHongLePhiViTri ucViTri;
        ucCaiDatHoaHongKeHoach ucKeHoach;
        ucCaiDatHoaHong ucSettingRose;
        public ucXacNhanXoa(MainWindow main, ListThietLap lstrose, ucCaiDatHoaHongDoanhThu ucdoanhthu, ucCaiDatHoaHongLoiNhuan ucloinhuan, ucCaiDatHoaHongLePhiViTri ucvitri, ucCaiDatHoaHongKeHoach uckehoach ,ucCaiDatHoaHong ucstrose)
        {
            InitializeComponent();
            Main = main;
            lstRose = lstrose;
            ucDoanhThu = ucdoanhthu;
            ucLoiNhuan = ucloinhuan;
            ucViTri = ucvitri;
            ucKeHoach = uckehoach;
            ucSettingRose = ucstrose;
        }

        List_Rose_DoanhThu lstDT = new List_Rose_DoanhThu();
        ucHoaHongNhanDuoc ucHoaHongNhanDuoc;
        public ucXacNhanXoa(MainWindow main, List_Rose_DoanhThu lstdt, ucHoaHongNhanDuoc uchhdt)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            lstDT = lstdt;
            this.ucHoaHongNhanDuoc = uchhdt;
            tb_TextNoiDungXoa.Text = "Bạn có chắc chắn muốn xóa hoa hồng doanh thu này?";

        }

        List_Rose_ViTri lstViTri = new List_Rose_ViTri();
        
        public ucXacNhanXoa(MainWindow main, List_Rose_ViTri lstvt, ucHoaHongNhanDuoc uchhvt)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.lstViTri = lstvt;
            this.ucHoaHongNhanDuoc = uchhvt;
            tb_TextNoiDungXoa.Text = "Bạn có chắc chắn muốn xóa hoa hồng lệ phí vị trí này?";

        }

        List_Rose_LoiNhuan lstLoiNhuan = new List_Rose_LoiNhuan();
        double LoiNhuan;
        public ucXacNhanXoa(MainWindow main, List_Rose_LoiNhuan lstLN, ucHoaHongNhanDuoc uchhvt, double loinhuan)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.lstLoiNhuan = lstLN;
            LoiNhuan = loinhuan;
            this.ucHoaHongNhanDuoc = uchhvt;
            tb_TextNoiDungXoa.Text = "Bạn có chắc chắn muốn xóa hoa hồng lợi nhuận này";

        }

        RoseUser Rose;
        ucHoaHongNhanDuoc ucRose;
        public ucXacNhanXoa(MainWindow main, RoseUser rose, ucHoaHongNhanDuoc ucrose)
        {
            InitializeComponent();
            Main = main;
            Rose = rose;
            this.ucRose = ucrose;
            tb_TextNoiDungXoa.Text = "Bạn có chắc chắn muốn xóa nhân viên hưởng hoa hồng kế hoạch này?";
        }

        int Back;
        public ucXacNhanXoa(MainWindow main, RoseUser rose, ucHoaHongNhanDuoc ucrose, int back)
        {
            InitializeComponent();
            Main = main;
            Rose = rose;
            this.ucRose = ucrose;
            Back = back;
            tb_TextNoiDungXoa.Text = "Bạn có chắc chắn muốn xóa nhân viên hưởng hoa hồng tiền này?";
          
        }
        private async void btnDongY_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (tb_TextNoiDungXoa.Text == "Bạn có chắc chắn muốn xóa hoa hồng doanh thu này?")
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/delete_rose");
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(lstDT.ro_id.ToString()), "ro_id");
                    content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                    request.Content = content;
                    var response = await client.SendAsync(request);

                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resConten = await response.Content.ReadAsStringAsync();

                        if (ucHoaHongNhanDuoc != null)
                        {
                            ucHoaHongNhanDuoc.LoadHoaHongDoanhThuCaNhan();
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (tb_TextNoiDungXoa.Text == "Bạn có chắc chắn muốn xóa nhân viên hưởng hoa hồng tiền này?")
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/delete_rose");
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(Rose.ro_id.ToString()), "ro_id");
                    content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                    request.Content = content;
                    var response = await client.SendAsync(request);

                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resConten = await response.Content.ReadAsStringAsync();

                        if (ucRose != null)
                        {
                            ucRose.LoadListRoseSaff();
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (tb_TextNoiDungXoa.Text == "Bạn có chắc chắn muốn xóa hoa hồng lợi nhuận này")
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/delete_rose");
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(lstLoiNhuan.ro_id.ToString()), "ro_id");
                    content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                    request.Content = content;
                    var response = await client.SendAsync(request);

                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resConten = await response.Content.ReadAsStringAsync();

                        if (ucHoaHongNhanDuoc != null)
                        {
                            ucHoaHongNhanDuoc.LoadHoaHongLoiNhuan();
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (tb_TextNoiDungXoa.Text == "Bạn có chắc chắn muốn xóa hoa hồng lệ phí vị trí này?")
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/delete_rose");
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(lstViTri.ro_id.ToString()), "ro_id");
                    content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                    request.Content = content;
                    var response = await client.SendAsync(request);

                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resConten = await response.Content.ReadAsStringAsync();

                        if (ucHoaHongNhanDuoc != null)
                        {
                            ucHoaHongNhanDuoc.LoadHoaHongViTri();
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            else if (tb_TextNoiDungXoa.Text == "Bạn có chắc chắn muốn xóa nhân viên hưởng hoa hồng kế hoạch này?")
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/delete_rose");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Rose.ro_id.ToString()), "ro_id");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                request.Content = content;
                var response = await client.SendAsync(request);

                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resConten = await response.Content.ReadAsStringAsync();

                    if (ucRose != null)
                    {
                        ucRose.LoadHoaHongKeHoach();
                        this.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/delete_thiet_lap");
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(lstRose.tl_id.ToString()), "tl_id");
                    content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                    request.Content = content;
                    var response = await client.SendAsync(request);

                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resConten = await response.Content.ReadAsStringAsync();

                        if (ucDoanhThu != null)
                        {
                            ucSettingRose.LoadListHoaHong();
                            ucDoanhThu.Visibility = Visibility.Collapsed;
                        }
                        else if (ucLoiNhuan != null)
                        {
                            ucSettingRose.LoadListHoaHong();
                            ucLoiNhuan.Visibility = Visibility.Collapsed;
                        }
                        else if (ucViTri != null)
                        {
                            ucSettingRose.LoadListHoaHong();
                            ucViTri.Visibility = Visibility.Collapsed;
                        }
                        else if (ucKeHoach != null)
                        {
                            ucSettingRose.LoadListHoaHong();
                            ucKeHoach.Visibility = Visibility.Collapsed;
                        }
                        this.Visibility = Visibility.Collapsed;
                    }


                }
                catch (Exception)
                { }
            }
           
        }

        private void btnHuy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
