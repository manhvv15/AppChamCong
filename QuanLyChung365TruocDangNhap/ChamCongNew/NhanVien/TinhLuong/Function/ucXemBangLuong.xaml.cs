using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.TinhLuong;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan;
using ClosedXML.Excel;
//using ClosedXML.Excel;
//using NPOI.SS.Formula.Functions;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.TinhLuong.Function
{
    /// <summary>
    /// Interaction logic for ucXemBangLuong.xaml
    /// </summary>
    public partial class ucXemBangLuong : UserControl
    {
        MainChamCong Main;
        string start_date;
        string end_date;
        int month;
        int year;
        public ucXemBangLuong(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            start_date = $"{DateTime.Now.Year}-{DateTime.Now.Month}-01";
            end_date = $"{DateTime.Now.Year + 1}-01-01";
            month = DateTime.Now.Month;
            year = DateTime.Now.Year;
            tb_IDNhanVien.Text = main.Ep_Id.ToString();
            tb_TenNhanVien.Text = main.Name_Nv;
            //dtp_NgayBatDau.Part_TextBox.Text = "Từ ngày";
            //dtp_NgayHienTai.Part_TextBox.Text = "Đến ngày";
            loadDLThang();
            LoadDLNam();
            LoadLuongNhanVien();
        }
        List<ThuongPhatDatum> lstThuong = new List<ThuongPhatDatum>();
        List<ThuongPhatDatum> lstPhat = new List<ThuongPhatDatum>();
        List<DataKoCcDetail> lstPhatNSQD = new List<DataKoCcDetail>();
        Data_LuongNV LuongNV = new Data_LuongNV();
        public async void LoadLuongNhanVien()
        {
            try
            {
                lstThuong.Clear();
                lstPhat.Clear();
                loading.Visibility = Visibility.Visible;
                if (lsvThang.SelectedItem != null && lsvNam.SelectedItem == null)
                {
                    month = int.Parse(lsvThang.Text.Split(' ')[1]);
                    if (month < 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year}-{month + 1}-01";
                    }
                    else if (month == 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year + 1}-01-01";
                    }
                }
                else if (lsvThang.SelectedItem == null && lsvNam.SelectedItem != null)
                {
                    year = int.Parse(lsvNam.Text.Split(' ')[1]);
                    if (month < 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year}-{month + 1}-01";
                    }
                    else if (month == 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year + 1}-01-01";
                    }
                }
                else if (lsvThang.SelectedItem != null && lsvNam.SelectedItem != null)  
                {
                    month = int.Parse(lsvThang.Text.Split(' ')[1]);
                    year = int.Parse(lsvNam.Text.Split(' ')[1]);
                    if (month < 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year}-{month + 1}-01";
                    }
                    else if (month == 12)
                    {
                        start_date = $"{year}-{month}-01";
                        end_date = $"{year + 1}-01-01";
                    }
                }

                int ep_id = Main.Ep_Id;
                int cb = Main.ComdID;
                string token = Properties.Settings.Default.Token;
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/nhanvien/show_payroll_user");
                var DataObject = new
                {
                    token = Properties.Settings.Default.Token,
                    start_date = start_date,
                    end_date = end_date,
                    month = month,
                    year = year,
                    cp = cb,
                    ep_id = ep_id,
                };
                string json = JsonConvert.SerializeObject(DataObject);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resConten = await response.Content.ReadAsStringAsync();
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    Root_LuongNV luongnv = JsonConvert.DeserializeObject<Root_LuongNV>(resConten);
                    if (luongnv.data != null)
                    {
                        LuongNV = luongnv.data;
                        tb_LuongCoBan.Text =      luongnv.data.luong_co_ban_format.ToString();
                        tb_PhanTramHopDong.Text = luongnv.data.phan_tram_hop_dong.ToString();
                        tb_CongChuan.Text =       luongnv.data.cong_chuan.ToString();
                        tb_CongThuc.Text =        luongnv.data.cong_thuc.ToString();
                        tb_CongSauPhat.Text =     luongnv.data.cong_sau_phat.ToString();
                        tb_CongTheoTien.Text =    luongnv.data.caTinhTien_format.ToString();
                        tb_CongGhiNhan.Text =     luongnv.data.cong_ghi_nhan.ToString();
                        tb_CongNghiPhep.Text =    luongnv.data.cong_nghi_phep.ToString();
                        tb_TongCongNhan.Text =    luongnv.data.tong_cong_nhan.ToString();
                        tb_LuongThuc.Text =       luongnv.data.luong_thuc_format.ToString();
                        tb_LuongSauPhat.Text =    luongnv.data.luong_sau_phat_format.ToString();
                        tb_LuongBaoHiem.Text =    luongnv.data.luong_bao_hiem_format.ToString();
                        tb_MSPhatTien.Text =      luongnv.data.tien_phat_muon_format.ToString();
                        tb_MSPhatCong.Text =      luongnv.data.tong_phat_cong.ToString();
                        tb_HoaHong.Text =         luongnv.data.tong_hoa_hong_format.ToString();
                        tb_LuongTheoGio.Text =    luongnv.data.tongTienTheoGio_format.ToString();
                        tb_TienDaTamUng.Text =    luongnv.data.tien_tam_ung_format.ToString();
                        tb_Thuong.Text =          luongnv.data.thuong_format.ToString();
                        tb_ThuongNghiLe.Text =    luongnv.data.luong_nghi_le_format.ToString();
                        tb_Phat.Text =            luongnv.data.phat_format.ToString();
                        tb_PhatNgiKhongPhep.Text= luongnv.data.tien_phat_nghi_khong_phep_format.ToString();
                       tb_PhatNgiSaiQuyDinh.Text= luongnv.data.phat_nghi_sai_quy_dinh_formar.ToString();
                        tb_PhucLoi.Text =         luongnv.data.tien_phuc_loi_format.ToString();
                        tb_PhuCap.Text =          luongnv.data.tien_phu_cap_format.ToString();
                        tb_PhuCapTheoCa.Text =    luongnv.data.phu_cap_theo_ca_format.ToString();
                        tb_BaoHiem.Text =         luongnv.data.tong_bao_hiem_format.ToString();
                        tb_KhoanTienKhac.Text =   luongnv.data.tien_khac_format.ToString();
                        tb_TongLuong.Text =       luongnv.data.tong_luong_format.ToString();
                        tb_Thue.Text =            luongnv.data.thue_format.ToString();
                        tb_LuongThuc.Text =       luongnv.data.luong_thuc_format.ToString();
                        tb_TongLuongDaTra.Text =  luongnv.data.luong_da_tra_format.ToString();
                        tb_TongLuongThucNhan.Text = luongnv.data.tien_thuc_nhan_format.ToString();
                        tb_TongLuongCTyTra.Text = luongnv.data.tien_thuc_nhan_format.ToString();
                        foreach (var item in luongnv.data.thuong_phat_data)
                        {
                            if (item.pay_status == 1)
                            {
                                lstThuong.Add(item);
                            }
                            else
                            {
                                lstPhat.Add(item);
                            }
                        }
                        dgvPhatNV.ItemsSource = lstPhat;
                        dgvThuongNV.ItemsSource = lstThuong;
                        foreach (var item in luongnv.data.data_ko_cc_detail)
                        {
                            item.date_format = $"{item.date.Day}-{item.date.Month}-{item.date.Year}";
                            item.Shift_Name = item.detail.shift_name;
                            lstPhatNSQD.Add(item);
                        }
                        dgvPhatNSQD.ItemsSource = lstPhatNSQD;
                    }
                }
                loading.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
            }
        }

        private void LoadDLNam()
        {
            List<string> listNam = new List<string>();
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
            listNam.Add("Năm " + DateTime.Now.Year);
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
            lsvNam.ItemsSource = listNam;
            lsvNam.SelectedIndex = 1;
            //lsvNam.SelectedItem = listNam[1];
            lsvNam.PlaceHolder = listNam[1];
        }
        private void loadDLThang()
        {
            List<string> listThang = new List<string>();
            listThang.Add("Tháng 1");
            listThang.Add("Tháng 2");
            listThang.Add("Tháng 3");
            listThang.Add("Tháng 4");
            listThang.Add("Tháng 5");
            listThang.Add("Tháng 6");
            listThang.Add("Tháng 7");
            listThang.Add("Tháng 8");
            listThang.Add("Tháng 9");
            listThang.Add("Tháng 10");
            listThang.Add("Tháng 11");
            listThang.Add("Tháng 12");
            lsvThang.ItemsSource = listThang;
            lsvThang.SelectedIndex = DateTime.Now.Month - 1;
            //lsvThang.SelectedItem = listThang[DateTime.Now.Month - 1];
            lsvThang.PlaceHolder = listThang[DateTime.Now.Month - 1];
        }
        private void popupTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            popupTP.Children.Clear();
            popupTP.Visibility = Visibility.Hidden;
            borChiTietThuongNV.Visibility = Visibility.Collapsed;
            borChiTietPhatNV.Visibility = Visibility.Collapsed;
            borChiTietPhatNgiSaiQD.Visibility = Visibility.Collapsed;
        }

        private void bod_ThoatChiTietPhatNV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borChiTietPhatNV.Visibility = Visibility.Collapsed;
            popupTP.Visibility = Visibility.Collapsed;
        }

        private void bod_ThoatChiTietThuongNV_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borChiTietThuongNV.Visibility = Visibility.Collapsed;
            popupTP.Visibility = Visibility.Collapsed;
        }

        private void bod_ThoatChiTietPhatCongNV_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            borChiTietPhatNgiSaiQD.Visibility = Visibility.Collapsed;
            popupTP.Visibility = Visibility.Collapsed;
        }
        private void btn_ChiTietThuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //dgvThuongNV.ItemsSource = tp.tt_thuong.ds_thuong;
            borChiTietThuongNV.Margin = new Thickness(Mouse.GetPosition(popupTP).X - 310, (Mouse.GetPosition(popupTP).Y + 25), 0, 0);
            borChiTietThuongNV.VerticalAlignment = VerticalAlignment.Top;
            borChiTietThuongNV.HorizontalAlignment = HorizontalAlignment.Left;
            popupTP.Visibility = Visibility.Visible;
            borChiTietThuongNV.Visibility = Visibility.Visible;
        }

        private void btn_ChiTietPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //dgvPhatNV.ItemsSource = tp.tt_phat.ds_phat;
            borChiTietPhatNV.VerticalAlignment = VerticalAlignment.Top;
            borChiTietPhatNV.HorizontalAlignment = HorizontalAlignment.Left;
            borChiTietPhatNV.Margin = new Thickness(Mouse.GetPosition(popupTP).X - 310, Mouse.GetPosition(popupTP).Y + 25, 0, 0);
            popupTP.Visibility = Visibility.Visible;
            borChiTietPhatNV.Visibility = Visibility.Visible;
        }

        private void btn_ChiTietPhatNghiSaiQD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //dgvPhatCongNV.ItemsSource = tp.tt_phat_cong.ds_phat_cong;
            borChiTietPhatNgiSaiQD.VerticalAlignment = VerticalAlignment.Top;
            borChiTietPhatNgiSaiQD.HorizontalAlignment = HorizontalAlignment.Left;
            borChiTietPhatNgiSaiQD.Margin = new Thickness(Mouse.GetPosition(popupTP).X - 310, Mouse.GetPosition(popupTP).Y + 25, 0, 0);
            popupTP.Visibility = Visibility.Visible;
            borChiTietPhatNgiSaiQD.Visibility = Visibility.Visible;
        }

        private void btn_ThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //start_date = "";
            //end_date = "";
            LoadLuongNhanVien();
        }

        private void btn_XuatCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (var workbook = new XLWorkbook())
                {
                    var worksheet = workbook.Worksheets.Add("Sheet1");
                   
                    worksheet.Cell(1,  1).Value = "1";
                    worksheet.Cell(2,  1).Value = "2";
                    worksheet.Cell(3,  1).Value = "3";
                    worksheet.Cell(4,  1).Value = "4";
                    worksheet.Cell(5,  1).Value = "5";
                    worksheet.Cell(6,  1).Value = "6";
                    worksheet.Cell(7,  1).Value = "7";
                    worksheet.Cell(8,  1).Value = "8";
                    worksheet.Cell(9,  1).Value = "9";
                    worksheet.Cell(10, 1).Value = "10";
                    worksheet.Cell(11, 1).Value = "11";
                    worksheet.Cell(12, 1).Value = "12";
                    worksheet.Cell(13, 1).Value = "13";
                    worksheet.Cell(14, 1).Value = "14";
                    worksheet.Cell(15, 1).Value = "15";
                    worksheet.Cell(16, 1).Value = "16";
                    worksheet.Cell(17, 1).Value = "17";
                    worksheet.Cell(18, 1).Value = "18";
                    worksheet.Cell(19, 1).Value = "19";
                    worksheet.Cell(20, 1).Value = "20";
                    worksheet.Cell(21, 1).Value = "21";
                    worksheet.Cell(22, 1).Value = "22";
                    worksheet.Cell(23, 1).Value = "23";
                    worksheet.Cell(24, 1).Value = "24";
                    worksheet.Cell(25, 1).Value = "25";
                    worksheet.Cell(26, 1).Value = "26";
                    worksheet.Cell(27, 1).Value = "27";
                    worksheet.Cell(28, 1).Value = "28";
                    worksheet.Cell(29, 1).Value = "29";
                    worksheet.Cell(30, 1).Value = "30";
                    worksheet.Cell(31, 1).Value = "31";

                    // Tiêu đề của các cột
                    worksheet.Cell(1, 2).Value = "Lương cơ bản";
                    worksheet.Cell(2, 2).Value = "Hợp đồng";
                    worksheet.Cell(3, 2).Value = "Công chuẩn";
                    worksheet.Cell(4, 2).Value = "Công thực";
                    worksheet.Cell(5, 2).Value = "Công sau phạt";
                    worksheet.Cell(6, 2).Value = "Công theo tiền";
                    worksheet.Cell(7, 2).Value = "Công ghi nhận";
                    worksheet.Cell(8, 2).Value = "Công nghỉ phép";
                    worksheet.Cell(9, 2).Value = "Tổng công nhận";
                    worksheet.Cell(10, 2).Value = "Lương thực";
                    worksheet.Cell(11, 2).Value = "Lương sau phạt";
                    worksheet.Cell(12, 2).Value = "Lương bảo hiểm";
                    worksheet.Cell(13, 2).Value = "Đi muộn/Về sớm phạt tiền";
                    worksheet.Cell(14, 2).Value = "Đi muộn về sớm phạt công";
                    worksheet.Cell(15, 2).Value = "Hoa hồng";
                    worksheet.Cell(16, 2).Value = "Tiền lương theo giờ";
                    worksheet.Cell(17, 2).Value = "Tiền đã tạm ứng";
                    worksheet.Cell(18, 2).Value = "Thưởng";
                    worksheet.Cell(19, 2).Value = "Thưởng nghỉ lễ";
                    worksheet.Cell(20, 2).Value = "Phạt";
                    worksheet.Cell(21, 2).Value = "Phạt nghỉ không được cho phép";
                    worksheet.Cell(22, 2).Value = "Phạt nghỉ sai quy định";
                    worksheet.Cell(23, 2).Value = "Phúc lợi";
                    worksheet.Cell(24, 2).Value = "Phụ cấp ";
                    worksheet.Cell(25, 2).Value = "Phụ cấp theo ca";
                    worksheet.Cell(26, 2).Value = "Bảo hiểm";
                    worksheet.Cell(27, 2).Value = "Khoản tiển khác";
                    worksheet.Cell(28, 2).Value = "Tổng lương";
                    worksheet.Cell(29, 2).Value = "Thuế";
                    worksheet.Cell(30, 2).Value = "Tổng lương thực nhận";
                    worksheet.Cell(31, 2).Value = "Tổng lương đã trả";
                   


                    // Thêm dữ liệu từ đối tượng Data_LuongNV vào các cột tương ứng
                    worksheet.Cell(1,  3).Value = LuongNV.luong_co_ban_format.ToString();
                    worksheet.Cell(2,  3).Value = LuongNV.phan_tram_hop_dong.ToString();
                    worksheet.Cell(3,  3).Value = LuongNV.cong_chuan.ToString();
                    worksheet.Cell(4,  3).Value = LuongNV.cong_thuc.ToString();
                    worksheet.Cell(5,  3).Value = LuongNV.cong_sau_phat.ToString();
                    worksheet.Cell(6,  3).Value = LuongNV.cong_theo_tien.ToString();
                    worksheet.Cell(7,  3).Value = LuongNV.cong_ghi_nhan.ToString();
                    worksheet.Cell(8,  3).Value = LuongNV.cong_nghi_phep.ToString();
                    worksheet.Cell(9,  3).Value = LuongNV.tong_cong_nhan.ToString();
                    worksheet.Cell(10, 3).Value = LuongNV.luong_thuc_format.ToString();
                    worksheet.Cell(11, 3).Value = LuongNV.luong_sau_phat_format.ToString();
                    worksheet.Cell(12, 3).Value = LuongNV.luong_bao_hiem_format.ToString();
                    worksheet.Cell(13, 3).Value = LuongNV.tien_phat_muon_format.ToString();
                    worksheet.Cell(14, 3).Value = LuongNV.tong_phat_cong.ToString();
                    worksheet.Cell(15, 3).Value = LuongNV.tong_hoa_hong_format.ToString();
                    worksheet.Cell(16, 3).Value = LuongNV.tongTienTheoGio_format.ToString();
                    worksheet.Cell(17, 3).Value = LuongNV.tien_tam_ung_format.ToString();
                    worksheet.Cell(18, 3).Value = LuongNV.thuong_format.ToString();
                    worksheet.Cell(19, 3).Value = LuongNV.luong_nghi_le_format.ToString();
                    worksheet.Cell(20, 3).Value = LuongNV.phat_format.ToString();
                    worksheet.Cell(21, 3).Value = LuongNV.tien_phat_nghi_khong_phep_format.ToString();
                    worksheet.Cell(22, 3).Value = LuongNV.phat_nghi_sai_quy_dinh_formar.ToString();
                    worksheet.Cell(23, 3).Value = LuongNV.tien_phuc_loi_format.ToString();
                    worksheet.Cell(24, 3).Value = LuongNV.tien_phu_cap_format.ToString();
                    worksheet.Cell(25, 3).Value = LuongNV.phu_cap_theo_ca_format.ToString();
                    worksheet.Cell(26, 3).Value = LuongNV.tong_bao_hiem_format.ToString();
                    worksheet.Cell(27, 3).Value = LuongNV.tien_khac_format.ToString();
                    worksheet.Cell(28, 3).Value = LuongNV.tong_luong_format.ToString();
                    worksheet.Cell(29, 3).Value = LuongNV.thue_format.ToString();
                    worksheet.Cell(30, 3).Value = LuongNV.luong_thuc_format.ToString();
                    worksheet.Cell(31, 3).Value = LuongNV.luong_da_tra_format.ToString();

                    // Hộp thoại để chọn thư mục và tên file
                    System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
                    frm.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                    frm.FilterIndex = 1;
                    frm.RestoreDirectory = true;

                    if (frm.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        // Lưu tệp Excel
                        string filePath = frm.FileName;
                        workbook.SaveAs(filePath);
                    }
                }
                Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this));
                if (LuongNV == null)
                {
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this, LuongNV));
                    return;
                }
            }
            catch (Exception)
            {
                ErrorSytem = "Error";
                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
            }
            
        }
        string ErrorSytem;

    }
}
