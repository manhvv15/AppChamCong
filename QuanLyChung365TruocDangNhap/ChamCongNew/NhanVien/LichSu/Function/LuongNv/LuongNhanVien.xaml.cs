using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.LuongNhanVien;
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
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for LuongDieuChinh.xaml
    /// </summary>
    public partial class LuongNhanVien : Window
    {
        MainChamCong Main;
        BrushConverter br = new BrushConverter();
        int month, year;
        public LuongNhanVien(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            tb_NameAccount.Text = Main.txbNameAccount.Text;
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            LoadProfileAccount();
            LoadSalaryTable();
            dgvLuongCoBan.Visibility = Visibility.Visible;
            
            luongCoBanAn.Background = (Brush)br.ConvertFrom("#4D5CD5");
            bod_LuongCoBan.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");  
            icon_LuongCB.Stroke = (Brush)br.ConvertFrom("#FFFFFF");
            tb_LuongCB.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
            tb_LuongCB1.Foreground = (Brush)br.ConvertFrom("#FFFFFF");

        }

        //private List<InfoBasicSalary> _lstLuongNhanVien;
        //public List<InfoBasicSalary> lstLuongNhanVien
        //{
        //    get { return _lstLuongNhanVien; }
        //    set { _lstLuongNhanVien = value; }
        //}
        List<InfoBasicSalary> lstLuongNhanVien = new List<InfoBasicSalary>();

        private List<InfoBasicSalary> _lstLuongNhanVienFillter;
        public List<InfoBasicSalary> lstLuongNhanVienFillter
        {
            get { return _lstLuongNhanVienFillter; }
            set { _lstLuongNhanVienFillter = value; }
        }
        private List<InfoContract> _lstHopDongNhanVien;
        public List<InfoContract> lstHopDongNhanVien
        {
            get { return _lstHopDongNhanVien; }
            set { _lstHopDongNhanVien = value; }
        }

        private List<LichSuDieuChinhLuong> _lstLichSuDieuChinh;
        public List<LichSuDieuChinhLuong> lstLichSuDieuChinh
        {
            get { return _lstLichSuDieuChinh; }
            set { _lstLichSuDieuChinh = value; }
        }
        public async void LoadSalaryTable()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3009/api/tinhluong/nhanvien/show_payroll_user");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.Ep_Id.ToString()), "ep_id");
                content.Add(new StringContent(Main.ComdID.ToString()), "cp");
                content.Add(new StringContent((month - 1).ToString()), "month");
                content.Add(new StringContent(year.ToString()), "year");
                if (DateTime.Now.Month < 10)
                {
                    content.Add(new StringContent($"{DateTime.Now.Year}-0{DateTime.Now.Month}-01T00:00:00.000+00:00"), "start_date");
                }
                else
                {
                    content.Add(new StringContent($"{DateTime.Now.Year}-{DateTime.Now.Month}-01T00:00:00.000+00:00"), "start_date");
                }
                if (DateTime.Now.Month == 12)
                {
                    content.Add(new StringContent($"{DateTime.Now.Year + 1}-01-01T00:00:00.000+00:00"), "end_date");

                }
                else
                {
                    if (DateTime.Now.Month + 1 < 10)
                    {
                        content.Add(new StringContent($"{DateTime.Now.Year}-0{DateTime.Now.Month + 1}-01T00:00:00.000+00:00"), "end_date");
                    }
                    else
                    {
                        content.Add(new StringContent($"{DateTime.Now.Year}-{DateTime.Now.Month + 1}-01T00:00:00.000+00:00"), "end_date");
                    }
                }
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
               
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent =  await response.Content.ReadAsStringAsync();

                Root_LuongNhanVien resultSalary = JsonConvert.DeserializeObject<Root_LuongNhanVien>(resContent);

                if (resultSalary.data != null)
                {
                    
                }
            }
            catch (Exception)
            {
            }
        }


       
        public async void LoadProfileAccount()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.Ep_Id.ToString()), "ep_id");
                content.Add(new StringContent(Main.ComdID.ToString()), "cp");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token"); 
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resProfile = await response.Content.ReadAsStringAsync();

                Root_HoSoNhanVien resultProfile = JsonConvert.DeserializeObject<Root_HoSoNhanVien>(resProfile);

                if (resultProfile.data != null)
                {
                    lstLuongNhanVienFillter = resultProfile.data.info_basic_salary;
                    InfoBasicSalary lstLuongNv = lstLuongNhanVienFillter.OrderByDescending(p => p.sb_time_up).FirstOrDefault();
                    lstLuongNhanVien.Add(lstLuongNv);
                    dgvLuongCoBan.ItemsSource = lstLuongNhanVien;
                    dgvLuongHienTai.ItemsSource = lstLuongNhanVien;
                    lstHopDongNhanVien = resultProfile.data.info_contract;
                    dgvThongTinHopDong.ItemsSource = lstHopDongNhanVien;
                    var query = from e in lstLuongNhanVien
                                join p in lstHopDongNhanVien on e.sb_id_user equals p.con_id_user
                                select new LichSuDieuChinhLuong
                                {
                                    luong_cb = e.sb_salary_basic,
                                    ngay_apdung = p.DateApplyHD,
                                    vitri = p.pos_name == ""? "Chưa cập nhật" :p.pos_name,
                                };
                    if (query != null)
                    {
                        lstLichSuDieuChinh = query.ToList();
                        dgvLichSuDieuChinh.ItemsSource = lstLichSuDieuChinh;
                    }
                }

            }
            catch (Exception)
            {
            }
        }

        #region even
        private void luongCoBanAn_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dgvLuongCoBan.Visibility == Visibility.Collapsed)
            {
                dgvLuongCoBan.Visibility = Visibility.Visible;

                dgvLuongHienTai.Visibility = Visibility.Collapsed;
                dgvThongTinHopDong.Visibility = Visibility.Collapsed;
                dgvLichSuDieuChinh.Visibility = Visibility.Collapsed;

                luongCoBanAn.Background = (Brush)br.ConvertFrom("#4D5CD5");
                bod_LuongCoBan.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
                icon_LuongCB.Stroke = (Brush)br.ConvertFrom("#FFFFFF");
                tb_LuongCB.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                tb_LuongCB1.Foreground = (Brush)br.ConvertFrom("#FFFFFF");

                LuongHienTai.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_LuongHienTai.BorderBrush = (Brush)br.ConvertFrom("#FF42D778");
                icon_LuongHienTai.Stroke = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongHT.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongHT1.Foreground = (Brush)br.ConvertFrom("#474747");

                ThongTinHopDong.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_ThongTinHopDong.BorderBrush = (Brush)br.ConvertFrom("#FE5C4B");
                icon_TongTinHopDong.Stroke = (Brush)br.ConvertFrom("#FE5C4B");
                tb_ThongTinHD.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_ThongTinHD1.Foreground = (Brush)br.ConvertFrom("#474747");

                LichSuDieuChinhLuong.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_LichSuDieuChinhLuong.BorderBrush = (Brush)br.ConvertFrom("#FE5C4B");
                icon_LichSuDC.Stroke = (Brush)br.ConvertFrom("#FE5C4B");
                tb_LichSuDC.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LichSuDC1.Foreground = (Brush)br.ConvertFrom("#474747");


            }
            else
            {
                dgvLuongCoBan.Visibility = Visibility.Collapsed;

            }
        }

        private void LuongHienTai_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dgvLuongHienTai.Visibility == Visibility.Collapsed)
            {
                dgvLuongHienTai.Visibility = Visibility.Visible;

                dgvLuongCoBan.Visibility = Visibility.Collapsed;
                dgvThongTinHopDong.Visibility = Visibility.Collapsed;
                dgvLichSuDieuChinh.Visibility = Visibility.Collapsed;

                LuongHienTai.Background = (Brush)br.ConvertFrom("#4AA7FF");
                bod_LuongHienTai.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
                icon_LuongHienTai.Stroke = (Brush)br.ConvertFrom("#FFFFFF");
                tb_LuongHT.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                tb_LuongHT1.Foreground = (Brush)br.ConvertFrom("#FFFFFF");

                luongCoBanAn.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_LuongCoBan.BorderBrush = (Brush)br.ConvertFrom("#4D5CD5");
                icon_LuongCB.Stroke = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongCB.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongCB1.Foreground = (Brush)br.ConvertFrom("#474747");

                ThongTinHopDong.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_ThongTinHopDong.BorderBrush = (Brush)br.ConvertFrom("#FE5C4B");
                icon_TongTinHopDong.Stroke = (Brush)br.ConvertFrom("#FE5C4B");
                tb_ThongTinHD.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_ThongTinHD1.Foreground = (Brush)br.ConvertFrom("#474747");

                LichSuDieuChinhLuong.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_LichSuDieuChinhLuong.BorderBrush = (Brush)br.ConvertFrom("#FE5C4B");
                icon_LichSuDC.Stroke = (Brush)br.ConvertFrom("#FE5C4B");
                tb_LichSuDC.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LichSuDC1.Foreground = (Brush)br.ConvertFrom("#474747");
            }
            else
            {
                dgvLuongHienTai.Visibility = Visibility.Collapsed;

            }
        }

        private void ThongTinHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dgvThongTinHopDong.Visibility == Visibility.Collapsed)
            {
                dgvThongTinHopDong.Visibility = Visibility.Visible;

                dgvLuongCoBan.Visibility = Visibility.Collapsed;
                dgvLuongHienTai.Visibility = Visibility.Collapsed;
                dgvLichSuDieuChinh.Visibility = Visibility.Collapsed;

                ThongTinHopDong.Background = (Brush)br.ConvertFrom("#FE5C4B");
                bod_ThongTinHopDong.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
                icon_TongTinHopDong.Stroke = (Brush)br.ConvertFrom("#FFFFFF");
                tb_ThongTinHD.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                tb_ThongTinHD1.Foreground = (Brush)br.ConvertFrom("#FFFFFF");

                luongCoBanAn.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_LuongCoBan.BorderBrush = (Brush)br.ConvertFrom("#4D5CD5");
                icon_LuongCB.Stroke = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongCB.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongCB1.Foreground = (Brush)br.ConvertFrom("#474747");

                LuongHienTai.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_LuongHienTai.BorderBrush = (Brush)br.ConvertFrom("#FF42D778");
                icon_LuongHienTai.Stroke = (Brush)br.ConvertFrom("#FE5C4B");
                tb_LuongHT.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongHT1.Foreground = (Brush)br.ConvertFrom("#474747");

                LichSuDieuChinhLuong.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_LichSuDieuChinhLuong.BorderBrush = (Brush)br.ConvertFrom("#FE5C4B");
                icon_LichSuDC.Stroke = (Brush)br.ConvertFrom("#FE5C4B");
                tb_LichSuDC.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LichSuDC1.Foreground = (Brush)br.ConvertFrom("#474747");
            }
            else
            {
                dgvThongTinHopDong.Visibility = Visibility.Collapsed;
            }
        }

        private void LichSuDieuChinhLuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (dgvLichSuDieuChinh.Visibility == Visibility.Collapsed)
            {
                dgvLichSuDieuChinh.Visibility = Visibility.Visible;

                dgvLuongCoBan.Visibility = Visibility.Collapsed;
                dgvThongTinHopDong.Visibility = Visibility.Collapsed;
                dgvLuongHienTai.Visibility = Visibility.Collapsed;

                LichSuDieuChinhLuong.Background = (Brush)br.ConvertFrom("#FE5C4B");
                bod_LichSuDieuChinhLuong.BorderBrush = (Brush)br.ConvertFrom("#FFFFFF");
                icon_LichSuDC.Stroke = (Brush)br.ConvertFrom("#FFFFFF");
                tb_LichSuDC.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                tb_LichSuDC1.Foreground = (Brush)br.ConvertFrom("#FFFFFF");


                luongCoBanAn.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_LuongCoBan.BorderBrush = (Brush)br.ConvertFrom("#4D5CD5");
                icon_LuongCB.Stroke = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongCB.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongCB1.Foreground = (Brush)br.ConvertFrom("#474747");

                LuongHienTai.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_LuongHienTai.BorderBrush = (Brush)br.ConvertFrom("#FF42D778");
                icon_LuongHienTai.Stroke = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongHT.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_LuongHT1.Foreground = (Brush)br.ConvertFrom("#474747");

                ThongTinHopDong.Background = (Brush)br.ConvertFrom("#FFFFFF");
                bod_ThongTinHopDong.BorderBrush = (Brush)br.ConvertFrom("#FE5C4B");
                icon_TongTinHopDong.Stroke = (Brush)br.ConvertFrom("#FE5C4B");
                tb_ThongTinHD.Foreground = (Brush)br.ConvertFrom("#4D5CD5");
                tb_ThongTinHD1.Foreground = (Brush)br.ConvertFrom("#474747");

            }
            else
            {
                dgvLichSuDieuChinh.Visibility = Visibility.Collapsed;
            }
        }
        #endregion
    }
}
