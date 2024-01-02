using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using QuanLyChung365TruocDangNhap.ChamCongNew.Salarysettings;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatNhapLuongCoBan;
using System.Windows.Input;
using Newtonsoft.Json;
using System.Net;
using System.Text;
using System.Windows.Media;
using System.Data;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.clsNhanVienThuocCongTy;
using System.Linq;
using QuanLyChung365TruocDangNhap.ChamCongNew.Core;
using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using OfficeOpenXml;
using Microsoft.Win32;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings
{

    /// <summary>
    /// Interaction logic for ucBasicSalary.xaml
    /// </summary>
    /// 
    public class SalarySaff
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string anh { get; set; }
        public string phongban { get; set; }
        public int luongcoban { get; set; }
        public string hopdongapdung { get; set; }
        public string chucvu { get; set; }
        public string lienhe { get; set; }


    }
    public partial class ucCaiDatLuongCoBan : System.Windows.Controls.UserControl
    {
        //private int i = 0;
        MainWindow Main;
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<ListUser>();
        public List<ListUser> lstNhanVien = new List<ListUser>();
        private List<ListUser> lstSearchNVTheoPhongBan2 = new List<ListUser>();
        private List<ListUser> lstSearchNVTheoPhongBan = new List<ListUser>();

        List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        private List<ListResult_CDL> lstLuongCB = new List<ListResult_CDL>();
        private List<ListResult_CDL> lstLuongCBPT = new List<ListResult_CDL>();
        private List<ListResult_CDL> lstLuongCBFilter = new List<ListResult_CDL>();
        private List<ListUser> lstLuongCBFilter22222 = new List<ListUser>();
        public static DataTable tb_LuongCB = new DataTable();
        public OOP.OrganizeData SelectedOrganize = new OOP.OrganizeData();
        private int TongSoTrang = 0;
        private int PageNumberCurrent = 1;
        private int SoDu = 0;
        BrushConverter br = new BrushConverter();
        public ucCaiDatLuongCoBan(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            SearchPB = "0";
            SearchNV = "0";
            //ngayBatDau.SelectedDate = DateTime.Parse(DateTime.Now.ToString("yyyy/MM") + "/01");
            //dtp_NgayHienTai.SelectedDate = DateTime.Now;
            LoadDLLuongCaBan();
            LoadDLPhongBan();
            LoadDLNhanVien();
            loadDLThang();
            LoadDLNam();
            ucListSalarySettings ucd = new ucListSalarySettings(main);
            ucBodyHome ucb = new ucBodyHome(main);
            Main.txbLoadChamCong.Text = ucb.txbSalarySettings.Text + " / " + ucd.txbFunction01.Text;
        }
        private void LoadDLNam()
        {
            List<string> listNam = new List<string>();
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
            listNam.Add("Năm " + DateTime.Now.Year);
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString());

            //lsvNam.ItemsSource = listNam;
            //lsvNam.SelectedIndex = 1;
            //lsvNam.SelectedItem = listNam[1];
            //lsvNam.PlaceHolder = listNam[1];
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
            //lsvThang.ItemsSource = listThang;
            //lsvThang.SelectedIndex = DateTime.Now.Month - 1;
            //lsvThang.SelectedItem = listThang[DateTime.Now.Month - 1];
            //lsvThang.PlaceHolder = listThang[DateTime.Now.Month - 1];
        }
        private List<ListResult_CDL> lstLuongCB1 = new List<ListResult_CDL>();
        private List<ListResult_CDL> lstLuongCBPT1 = new List<ListResult_CDL>();
        private void ChonNgayBatDau(object sender, SelectionChangedEventArgs e)
        {
            if (dtp_NgayBatDau.SelectedDate.Value.Month != dtp_NgayHienTai.SelectedDate.Value.Month)
            {
                tb_ValidateChonNgay.Visibility = Visibility.Visible;
                tb_ValidateChonNgay.Text = "Khoảng thời gian tìm kiếm phải cùng một tháng";
            }
            else
            {
                tb_ValidateChonNgay.Visibility = Visibility.Collapsed;
            }
        }
        private void ChonNgayHienTai(object sender, SelectionChangedEventArgs e)
        {
            if (dtp_NgayBatDau.SelectedDate.Value.Month != dtp_NgayHienTai.SelectedDate.Value.Month)
            {
                tb_ValidateChonNgay.Visibility = Visibility.Visible;
                tb_ValidateChonNgay.Text = "Khoảng thời gian tìm kiếm phải cùng một tháng";
            }
            else
            {
                tb_ValidateChonNgay.Visibility = Visibility.Collapsed;
            }
        }
        private void LoadDLLuongCaBan()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    loading.Visibility = Visibility.Visible;
                    lstLuongCB.Clear();
                    lstLuongCBPT.Clear();
                    lstLuongCBFilter.Clear();
                    request.QueryString.Add("com_id", Main.IdAcount.ToString());
                    int Start_Day = dtp_NgayBatDau.SelectedDate.Value.Day;
                    int Start_Month = dtp_NgayBatDau.SelectedDate.Value.Month;
                    int Start_Year = dtp_NgayBatDau.SelectedDate.Value.Year;

                    int End_Day = dtp_NgayHienTai.SelectedDate.Value.Day;
                    int End_Month = dtp_NgayHienTai.SelectedDate.Value.Month;
                    int End_Year = dtp_NgayHienTai.SelectedDate.Value.Year;
                    //int nam, thang;
                    //if(lsvNam.SelectedItem != null)
                    //    nam = int.Parse(lsvNam.SelectedItem.ToString().Split(' ')[1]);
                    //else nam = DateTime.Now.Year;
                    //if(lsvThang.SelectedItem != null) thang = int.Parse(lsvThang.SelectedItem.ToString().Split(' ')[1]);
                    //else thang = DateTime.Now.Month;
                    request.QueryString.Add("month", Start_Month.ToString());
                    request.QueryString.Add("year", Start_Year.ToString());
                    if (Start_Month < 10 && Start_Day < 10)
                    {
                        request.QueryString.Add("start_date", $"{Start_Year}/0{Start_Month}/0{Start_Day}");
                    }
                    else if (Start_Month >= 10 && Start_Day < 10)
                    {
                        request.QueryString.Add("start_date", $"{Start_Year}/{Start_Month}/0{Start_Day}");
                    }
                    else if (Start_Month < 10 && Start_Day >= 10)
                    {
                        request.QueryString.Add("start_date", $"{Start_Year}/0{Start_Month}/{Start_Day}");
                    }
                    else
                    {
                        request.QueryString.Add("start_date", $"{Start_Year}/{Start_Month}/{Start_Day}"); ;
                    }

                    if (End_Month < 10 && End_Day < 10)
                    {
                        request.QueryString.Add("end_date", $"{End_Year}/0{End_Month}/0{End_Day}");
                    }
                    else if (Start_Month >= 10 && Start_Day < 10)
                    {
                        request.QueryString.Add("end_date", $"{End_Year}/{End_Month}/0{End_Day}");
                    }
                    else if (Start_Month < 10 && Start_Day >= 10)
                    {
                        request.QueryString.Add("end_date", $"{End_Year}/0{End_Month}/{End_Day}");
                    }
                    else
                    {
                        request.QueryString.Add("end_date", $"{End_Year}/{End_Month}/{End_Day}");
                    }

                    if (SelectedOrganize.id > 0)
                    {
                        request.QueryString.Add("organizeDetailId", JsonConvert.SerializeObject(SelectedOrganize.listOrganizeDetailId));
                    }

                    if(searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                    {
                        request.QueryString.Add("ep_id", ((ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString());
                    }
                    request.QueryString.Add("skip", "0");
                    request.QueryString.Add("pageNumber", "1");
                    request.QueryString.Add("pageSize", "10000");
                    request.QueryString.Add("token", Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        try
                        {
                           Root_CaiDatLuong  luongCB = JsonConvert.DeserializeObject<Root_CaiDatLuong>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (luongCB.listResult != null)
                            {
                                tb_TongLuongCongTy.Text = luongCB.luong_tong_cong_ty_Format;
                                lstLuongCB = luongCB.listResult;
                                if (luongCB.listResult.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                                else DpPhanTRang.Visibility = Visibility.Visible;
                                TongSoTrang = luongCB.listResult.Count / 10;
                                SoDu = 10 - (luongCB.listResult.Count % 10);
                                if (luongCB.listResult.Count % 10 > 0)
                                {
                                    TongSoTrang++;
                                }
                                for (int i = 0; i < 10 && i < luongCB.listResult.Count; i++)
                                {
                                    lstLuongCBPT.Add(luongCB.listResult[i]);
                                }
                                dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                                dgvListSalaryBasic.Items.Refresh();
                                if (TongSoTrang < 3)
                                {
                                    if (TongSoTrang == 2)
                                    {
                                        borPage3.Visibility = Visibility.Collapsed;
                                        borPage2.Visibility = Visibility.Visible;
                                        borLen1.Visibility = Visibility.Visible;
                                        borPageCuoi.Visibility = Visibility.Visible;
                                    }
                                    else if (TongSoTrang == 1)
                                    {
                                        borPage2.Visibility = Visibility.Collapsed;
                                        borPage3.Visibility = Visibility.Collapsed;
                                        borLen1.Visibility = Visibility.Collapsed;
                                        borPageCuoi.Visibility = Visibility.Collapsed;
                                    }
                                }
                                else
                                {
                                    borLui1.Visibility = Visibility.Collapsed;
                                    borPageDau.Visibility = Visibility.Collapsed;
                                    borPage2.Visibility = Visibility.Visible;
                                    borPage3.Visibility = Visibility.Visible;
                                    borLen1.Visibility = Visibility.Visible;
                                    borPageCuoi.Visibility = Visibility.Visible;
                                }
                                //tb_LuongCB = Function.clsExPortExcel.NewTables("tb_LuongCB", new string[] { "colTenNhanVien", "colSDT", "colDiaChi", "colToChuc", "colChucVu", "colLuongCB", "colHopDongAD", "colCongChuan", "colCongThuc", "colCongSauPhat", "colCongTheoTien", "colCongGhiNhan", "colCongNghiPhep", "colTongCongNhan", "colLuongThuc", "colLuongSauPhat", "colLuongBaoHiem", "colTienPhatMuon", "colCongPhatDiMuonVeSom", "colTongHoaHong", "colTienTamUng", "colThuong", "colLuongNghiLe", "colPhat", "colTienPhatNghiKhongPhep", "colPhatNghiSaiQuyDinh", "colTienPhucLoi", "colTienPhuCap", "colTienPhuCapTheoCa", "colTongBaoHiem", "colTienKhac", "colTongLuong", "colThue", "colLuongTheoGio", "colTienThucNhan", "colLuongDaTra" });
                                //LoadDataInDataTable();
                            }
                            loading.Visibility = Visibility.Collapsed;
                        }
                        catch { loading.Visibility = Visibility.Collapsed; }
                        
                    };
                    request.UploadValuesTaskAsync("https://api.timviec365.vn/api/tinhluong/congty/show_bangluong_nv_theongay", request.QueryString);
                }
            //https://api.timviec365.vn/api/tinhluong/congty/show_bangluong_nv
            }
            catch (Exception)
            {} 
        }


        private void ExportExcelSalary_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                string filePath = "";
                //SaveFileDiaLog lưu file Excel
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                //Đọc ra các file có định dạng excel
                saveFileDialog.Filter = "Excel | *.xlsx | Excel 2016 | *.xls";
                //Lưu đường dẫn file 
                if (saveFileDialog.ShowDialog() == true) { filePath = saveFileDialog.FileName; }
                //File rỗng 
                if (string.IsNullOrEmpty(filePath)) { System.Windows.MessageBox.Show("Đường dẫn không hợp lệ"); return; }
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                try
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        p.Workbook.Properties.Author = "QuanLyChung365TruocDangNhap.ChamCongNew";
                        // đặt tiêu đề cho file
                        p.Workbook.Properties.Title = "Danh sách lương cơ bản";
                        //Tạo một sheet để làm việc trên đó
                        p.Workbook.Worksheets.Add("sheet ExportWork");
                        // lấy sheet vừa add ra để thao tác
                        ExcelWorksheet ws = p.Workbook.Worksheets[0];
                        // đặt tên cho sheet
                        ws.Name = $"Danh sách 1";
                        // fontsize mặc định cho cả sheet
                        ws.Cells.Style.Font.Size = 12;
                        // font family mặc định cho cả sheet
                        ws.Cells.Style.Font.Name = "Calibri";
                        // Tạo danh sách các column header
                        string[] arrColumnHeader = {
                            "Tên nhân viên", "ID nhân viên","Tổ chức", 
                            "Chức vụ", "Lương cơ bản", "Công Chuẩn",
                            "Công thực", "Công sau phạt","Công theo tiền", 
                            "Công ghi nhận","Công nghỉ phép", "Tổng công nhận","Lương thực",
                            "Lương sau phạt", "Lương bảo hiểm","Tiền phạt muộn", 
                            "Công phạt đi muộn về sớm","Tổng hoa hồng", "Tiền tạm ứng",
                            "Thưởng", "Lương nghỉ lễ","Phạt", "Tiền phạt nghỉ không phép",
                            "Phạt nghỉ sai quy định",
                            "Tiền phúc lợi", "Tiền phụ cấp","Tiền phụ cấp theo ca", 
                            "Tổng bảo hiểm","Tiền khác", "Tổng Lương","Thuế",
                            "Lương theo giờ","Tiền thực nhận","Lương đã trả"
                        };
                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();
                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge 
                        ws.Cells[1, 1].Value = $"Bảng lương cơ bản";
                        ws.Cells[1, 1, 1, countColHeader].Merge = true;
                        // in đậm
                        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
                        // căn giữa
                        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                        int colIndex = 1;
                        int rowIndex = 2;
                        //tạo các header từ column header đã tạo từ bên trên
                        foreach (var item in arrColumnHeader)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];
                            //set màu thành gray
                            var fill = cell.Style.Fill; fill.PatternType = ExcelFillStyle.Solid;
                            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);
                            //căn chỉnh các border
                            var border = cell.Style.Border;
                            border.Bottom.Style = border.Top.Style = border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                            //gán giá trị
                            cell.Value = item; colIndex++;
                        }
                        //lấy ra danh sách UserInfo từ ItemSource của ListView
                        List<ListResult_CDL> userList = lstLuongCB.Cast<ListResult_CDL>().ToList();
                        //với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;
                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;
                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = $"{item.userName}";
                            ws.Cells[rowIndex, colIndex++].Value = $"{item.ep_id}";
                            //ws.Cells[rowIndex, colIndex++].Value = item.p;
                            if (item.organizeDetailName != null)
                            {
                                ws.Cells[rowIndex, colIndex++].Value = item.organizeDetailName;
                            }
                            else
                            {
                                ws.Cells[rowIndex, colIndex++].Value = "Chưa cập nhật";
                            }
                            if (item.positionName != null)
                            {
                                ws.Cells[rowIndex, colIndex++].Value = item.positionName;
                            }
                            else
                            {
                                ws.Cells[rowIndex, colIndex++].Value = "Chưa cập nhật";
                            }
                                ws.Cells[rowIndex, colIndex++].Value =  item.luong_co_ban_Format + " VNĐ";
                                ws.Cells[rowIndex, colIndex++].Value = item.cong_chuan;
                                ws.Cells[rowIndex, colIndex++].Value = item.cong_thuc;
                                ws.Cells[rowIndex, colIndex++].Value = item.cong_sau_phat;
                                ws.Cells[rowIndex, colIndex++].Value = item.cong_theo_tien;
                                ws.Cells[rowIndex, colIndex++].Value = item.cong_ghi_nhan;
                                ws.Cells[rowIndex, colIndex++].Value = item.cong_nghi_phep;
                                ws.Cells[rowIndex, colIndex++].Value = item.tong_cong_nhan;
                                ws.Cells[rowIndex, colIndex++].Value = item.luong_thuc_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.luong_sau_phat_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.luong_bao_hiem_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tien_phat_muon_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.cong_phat_di_muon_ve_som;
                                ws.Cells[rowIndex, colIndex++].Value = item.tong_hoa_hong_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tien_tam_ung_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.thuong_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.luong_nghi_le_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.phat_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tien_phat_nghi_khong_phep_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.phat_nghi_sai_quy_dinh_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tien_phuc_loi_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tien_phu_cap_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.phu_cap_theo_ca_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tong_bao_hiem_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tien_khac_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tong_luong_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.thue_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tongTienTheoGio_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.tien_thuc_nhan_display;
                                ws.Cells[rowIndex, colIndex++].Value = item.luong_da_tra_display;
                        }
                        //Lưu file lại
                        Byte[] bin = p.GetAsByteArray();
                        File.WriteAllBytes(filePath, bin);
                    }
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this));
                }
                catch (Exception)
                {
                    ErrorSytem = "Error";
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                }

                if (lstLuongCB == null)
                {
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this, lstLuongCB));
                    return;
                }
            }
            catch (Exception)
            {
                ErrorSytem = "Error";
                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
            }
            //try
            //{
            //    string PathTemplate = "";
            //    var file = Properties.Resources.FileLuongCB;
            //    string path = $"{Environment.GetEnvironmentVariable("APPDATA")}/QuanLyChung365TruocDangNhap.ChamCongNew/";
            //    if (!System.IO.Directory.Exists(path))
            //    {
            //        System.IO.Directory.CreateDirectory(path);
            //    }
            //    if (!File.Exists(path + "FileLuongCB.xlsx"))
            //    {
            //        File.WriteAllBytes(path + "FileLuongCB.xlsx", file);
            //    }
            //    PathTemplate = path + "FileLuongCB.xlsx";
            //    if (File.Exists(PathTemplate))
            //    {
            //        Microsoft.Office.Interop.Excel.Application Ex = new Microsoft.Office.Interop.Excel.Application();
            //        Microsoft.Office.Interop.Excel.Workbook wb = Ex.Workbooks.Open(PathTemplate);
            //        Microsoft.Office.Interop.Excel.Worksheet ws_HoaDon = wb.Worksheets["ChamCong"];
            //        ws_HoaDon.Name = "Bảng lương tháng " + dtpNgayThanhLap.SelectedDate.Value.ToString("MM");
            //        ws_HoaDon.Cells[1, 1] = Main.data.data.user_info.com_name;
            //        ws_HoaDon.Cells[2, 1] = ws_HoaDon.Name;
            //        int DongBatDau = 3;
            //        foreach (DataRow row in tb_LuongCB.Rows)
            //        {
            //            for (int i = 0; i < tb_LuongCB.Columns.Count; i++)
            //            {
            //                ws_HoaDon.Cells[DongBatDau, i + 1] = row[i];
            //            }
            //            DongBatDau++;
            //        }
            //        System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
            //        frm.Filter = "Excel | *.xlsx | Excel 2003 | *.xls";
            //        frm.FileName = ws_HoaDon.Name;
            //        if (frm.ShowDialog() == DialogResult.OK)
            //        {
            //            wb.SaveAs(frm.FileName);
            //            Main.grShowPopup.Children.Add(new Popup.ExportExcelSuccess(frm.FileName));
            //            wb.Close();
            //            Ex.Quit();
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.Message);
            //}
        }
        string ErrorSytem;
        private void LoadDataInDataTable()
        {
            //try
            //{
            //    //DataRow dr1 = tb_LuongCB.NewRow();
            //    //dr1["colTenNhanVien"] = "Tên nhân viên";
            //    //dr1["colSDT"] = "Số điện thoại";
            //    //dr1["colDiaChi"] = "Địa chỉ";
            //    //dr1["colToChuc"] = "Tổ chức";
            //    //dr1["colLuongCB"] = "Lương cơ bản";
            //    //dr1["colHopDongAD"] = "Phần trăm hợp đồng";
            //    //dr1["colChucVu"] = "Chức vụ";
            //    //dr1["colCongChuan"] = "Công Chuẩn";
            //    //dr1["colCongThuc"] = "Công thực";
            //    //dr1["colCongSauPhat"] = "Công sau phạt";
            //    //dr1["colCongTheoTien"] = "Công theo tiền";
            //    //dr1["colCongGhiNhan"] = "Công ghi nhận";
            //    //dr1["colCongNghiPhep"] = "Công nghỉ phép";
            //    //dr1["colTongCongNhan"] = "Tổng công nhận";
            //    //dr1["colLuongThuc"] = "Lương thực";
            //    //dr1["colLuongSauPhat"] = "Lương sau phạt";
            //    //dr1["colLuongBaoHiem"] = "Lương bảo hiểm";
            //    //dr1["colTienPhatMuon"] = "Tiền phạt muộn";
            //    //dr1["colCongPhatDiMuonVeSom"] = "Công phạt đi sớm về muộn";
            //    //dr1["colTongHoaHong"] = "Tổng hoa hồng";
            //    //dr1["colTienTamUng"] = "Tiền tạm ứng";
            //    //dr1["colThuong"] = "Thưởng";
            //    //dr1["colLuongNghiLe"] = "Lương nghỉ lễ";
            //    //dr1["colPhat"] = "Phạt";
            //    //dr1["colTienPhatNghiKhongPhep"] = "Tiền phạt nghỉ không phép";
            //    //dr1["colPhatNghiSaiQuyDinh"] = "Phạt nghỉ sai quy định";
            //    //dr1["colTienPhucLoi"] = "Tiền phúc lợi";
            //    //dr1["colTienPhuCap"] = "Tiền phụ cấp";
            //    //dr1["colTienPhuCapTheoCa"] = "Tiền phụ cấp theo ca";
            //    //dr1["colTongBaoHiem"] = "Tổng bảo hiểm";
            //    //dr1["colTienKhac"] = "Tiền khác";
            //    //dr1["colTongLuong"] = "Tổng Lương";
            //    //dr1["colThue"] = "Thuế";
            //    //dr1["colLuongTheoGio"] = "Lương theo giờ";
            //    //dr1["colTienThucNhan"] = "Tiền thực nhận";
            //    //dr1["colLuongDaTra"] = "Lương đã trả";
            //    //tb_LuongCB.Rows.Add(dr1);
            //    //foreach (var item in lstLuongCB)
            //    //{
            //    //    DataRow dr = tb_LuongCB.NewRow();
            //    //    dr["colTenNhanVien"] = item.userName;
            //    //    dr["colSDT"] = "'" + item.phone;
            //    //    dr["colDiaChi"] = item.address;
            //    //    dr["colLuongCB"] = item.DataLuong.luong_co_ban;
            //    //    dr["colHopDongAD"] = item.DataLuong.phan_tram_hop_dong;
            //    //    if (item.organizeDetail != null)
            //    //    {
            //    //        dr["colToChuc"] = item.organizeDetail.organizeDetailName;
            //    //    }
            //    //    if(item.pos != null)
            //    //        dr["colChucVu"] = item.pos.positionName;
            //    //    dr["colCongChuan"] = item.DataLuong.cong_chuan;
            //    //    dr["colCongThuc"] = item.DataLuong.cong_thuc;
            //    //    dr["colCongSauPhat"] = item.DataLuong.cong_sau_phat;
            //    //    dr["colCongTheoTien"] = item.DataLuong.cong_theo_tien;
            //    //    dr["colCongGhiNhan"] = item.DataLuong.cong_ghi_nhan;
            //    //    dr["colCongNghiPhep"] = item.DataLuong.cong_nghi_phep;
            //    //    dr["colTongCongNhan"] = item.DataLuong.tong_cong_nhan;
            //    //    dr["colLuongThuc"] = item.DataLuong.luong_thuc;
            //    //    dr["colLuongSauPhat"] = item.DataLuong.luong_sau_phat;
            //    //    dr["colLuongBaoHiem"] = item.DataLuong.luong_bao_hiem;
            //    //    dr["colTienPhatMuon"] = item.DataLuong.tien_phat_muon;
            //    //    dr["colCongPhatDiMuonVeSom"] = item.DataLuong.cong_phat_di_muon_ve_som;
            //    //    dr["colTongHoaHong"] = item.DataLuong.tong_hoa_hong;
            //    //    dr["colTienTamUng"] = item.DataLuong.tien_tam_ung;
            //    //    dr["colThuong"] = item.DataLuong.thuong;
            //    //    dr["colLuongNghiLe"] = item.DataLuong.luong_nghi_le;
            //    //    dr["colPhat"] = item.DataLuong.phat;
            //    //    dr["colTienPhatNghiKhongPhep"] = item.DataLuong.tien_phat_nghi_khong_phep;
            //    //    dr["colPhatNghiSaiQuyDinh"] = item.DataLuong.phat_nghi_sai_quy_dinh;
            //    //    dr["colTienPhucLoi"] = item.DataLuong.tien_phuc_loi;
            //    //    dr["colTienPhuCap"] = item.DataLuong.tien_phuc_loi;
            //    //    dr["colTienPhuCapTheoCa"] = item.DataLuong.tien_phu_cap;
            //    //    dr["colTongBaoHiem"] = item.DataLuong.tong_bao_hiem;
            //    //    dr["colTienKhac"] = item.DataLuong.tien_khac;
            //    //    dr["colTongLuong"] = item.DataLuong.tong_luong;
            //    //    dr["colThue"] = item.DataLuong.thue;
            //    //    dr["colLuongTheoGio"] = item.DataLuong.tongTienTheoGio;
            //    //    dr["colTienThucNhan"] = item.DataLuong.tien_thuc_nhan;
            //    //    dr["colLuongDaTra"] = item.DataLuong.luong_da_tra;
            //    //    tb_LuongCB.Rows.Add(dr);
            //    }
            //}
            //catch(Exception) { }

        }

        private void LoadDLNhanVien()
        {
            
            searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
            lstNhanVien = Main.lstNhanVienThuocCongTy;
        }

        private void LoadDLPhongBan()
        {
            searchBarToChuc.ItemsSource = Main.lstOrganizeData;
        }

        private void dapDay_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //txbSelectedDay.Text = dapDay.SelectedDate.ToString();
            //bodCalendarDay.Visibility = Visibility.Collapsed;
        }

        private void bodSelectDaySalary_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            //if (bodCalendarDay.Visibility == Visibility.Collapsed)
            //{
            //    bodCalendarDay.Visibility = Visibility.Visible;
            //    lsvChonNhanVien.Visibility = Visibility.Collapsed;
            //}
            //else
            //{
            //    bodCalendarDay.Visibility = Visibility.Collapsed;
            //}
            //ucCalendar uc = new ucCalendar(Main);
            //Main.grShowPopup.Children.Add(new ucCalendar(Main));
        }
        private void dapDay_SelectedDatesChanged_1(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void bodImportExeclSalary_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bodImportExeclSalary.BorderThickness = new Thickness(1);
        }

        private void bodImportExeclSalary_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bodImportExeclSalary.BorderThickness = new Thickness(0);
        }

        private void ExportExcelSalary_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ExportExcelSalary.BorderThickness = new Thickness(1);
        }

        private void ExportExcelSalary_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ExportExcelSalary.BorderThickness = new Thickness(0);
        }

        private void bodThongTinNhanVien_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ListResult_CDL cls = (sender as System.Windows.Controls.Border).DataContext as ListResult_CDL;
            if (cls != null)
            {
                Main.Back = 10;
                ucHoSoNhanVien uch = new ucHoSoNhanVien(Main, cls);
                ucListSalarySettings ucd = new ucListSalarySettings(Main);
                ucBodyHome ucb = new ucBodyHome(Main);
                Main.dopBody.Children.Clear();
                object Content = uch.Content;
                uch.Content = null;
                Main.dopBody.Children.Add(Content as UIElement);
                Main.LableFunction.Visibility = Visibility.Visible;
                Main.txbLoadChamCong.Text = ucb.txbSalarySettings.Text + " / " + ucd.txbFunction01.Text + " / " + "Hồ Sơ Nhân Viên";
            }

        }

        private void cboloadName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void dgvListSalaryBasic_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if(Main.keyDown != Key.LeftShift && Main.keyDown != Key.RightShift)
                Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
            else
                dgvListSalaryBasic.GetFirstChildOfType<ScrollViewer>().ScrollToHorizontalOffset(dgvListSalaryBasic.GetFirstChildOfType<ScrollViewer>().HorizontalOffset - e.Delta);
        }

        private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lstSearchNV = new List<clsLuongCoBan.ListUser>();
            //foreach (var str in Main.lstNhanVienThuocCongTy)
            //{
            //    if (str.userName.ToLower().RemoveUnicode().Contains(textSearchNhanVien.Text.ToLower().RemoveUnicode()) || str.idQLC.ToString().Contains(textSearchNhanVien.Text.ToString()))
            //    {
            //        lstSearchNV.Add(str);

            //    }
            //}
            //lsvNhanVien.ItemsSource = lstSearchNV;
            //if (tb.Text == "")
            //{
            //    lsvNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
            //}
        }

        private string SearchNV = "";
        private string SearchPB = "";

        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lstLuongCBFilter.Clear();
                if (searchBarNhanVien.SelectedItem != null && ((ListUser)searchBarNhanVien.SelectedItem)._id > 0)
                {
                    foreach (var item in lstLuongCB)
                    {
                        if (IdNV == item.ep_id.ToString())
                        {
                            lstLuongCBFilter.Add(item);
                        }
                        if (lstLuongCBFilter.Count < 10)
                        {
                            DpPhanTRang.Visibility = Visibility.Collapsed;
                        }
                        dgvListSalaryBasic.ItemsSource = lstLuongCBFilter;
                        dgvListSalaryBasic.Items.Refresh();
                    }
                }
                else if (dtp_NgayBatDau.SelectedDate != null  || searchBarToChuc.SelectedItem != null)
                {
                    if (dtp_NgayBatDau.SelectedDate.Value.Month != dtp_NgayHienTai.SelectedDate.Value.Month)
                    {
                        tb_ValidateChonNgay.Visibility = Visibility.Visible;
                        tb_ValidateChonNgay.Text = "Khoảng thời gian tìm kiếm phải cùng một tháng";
                    }
                    else
                    {
                        LoadDLLuongCaBan();
                        tb_ValidateChonNgay.Visibility = Visibility.Collapsed;
                    }
                }
                else
                {
                    if (lstLuongCB.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                    else DpPhanTRang.Visibility = Visibility.Visible;
                    TongSoTrang = lstLuongCB.Count / 10;
                    SoDu = 10 - (lstLuongCB.Count % 10);
                    if (lstLuongCB.Count % 10 > 0)
                    {
                        TongSoTrang++;
                    }
                    for (int i = 0; i < 10 && i < lstLuongCB.Count; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                    dgvListSalaryBasic.Items.Refresh();
                    if (TongSoTrang < 3)
                    {
                        if (TongSoTrang == 2)
                        {
                            borPage3.Visibility = Visibility.Collapsed;
                            borPage2.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }
                        else if (TongSoTrang == 1)
                        {
                            borPage2.Visibility = Visibility.Collapsed;
                            borPage3.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                            borPageCuoi.Visibility = Visibility.Collapsed;
                        }
                    }
                    else
                    {
                        borLui1.Visibility = Visibility.Collapsed;
                        borPageDau.Visibility = Visibility.Collapsed;
                        borPage2.Visibility = Visibility.Visible;
                        borPage3.Visibility = Visibility.Visible;
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                if (TongSoTrang > 2)
                {
                    borPage2.Visibility = Visibility.Visible;
                    borPage3.Visibility = Visibility.Visible;
                }
                else if (TongSoTrang > 1)
                {
                    borPage2.Visibility = Visibility.Visible;
                    borPage3.Visibility = Visibility.Collapsed;
                }
                else
                {
                    borPage2.Visibility = Visibility.Collapsed;
                    borPage3.Visibility = Visibility.Collapsed;
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                }
                if (TongSoTrang > 1)
                {
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                }
                lstLuongCBPT = new List<ListResult_CDL>();
                for (int i = 0; i < 10; i++)
                {
                    lstLuongCBPT.Add(lstLuongCB[i]);
                }
                dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                PageNumberCurrent = 1;
            }
            catch (Exception)
            {
            }
            
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if(PageNumberCurrent > 2)
            {
                textPage1.Text = (PageNumberCurrent - 2).ToString();
                textPage2.Text = (PageNumberCurrent - 1).ToString();
                textPage3.Text = PageNumberCurrent.ToString();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            }
            else if(TongSoTrang > 2)
            {
                textPage1.Text = (PageNumberCurrent - 1).ToString();
                textPage2.Text = (PageNumberCurrent).ToString();
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageDau.Visibility = Visibility.Collapsed;
                borLui1.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPage1.Text = "1";
                textPage2.Text = "2";
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageDau.Visibility = Visibility.Collapsed;
                borLui1.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
            }
            borPageCuoi.Visibility = Visibility.Visible;
            borLen1.Visibility = Visibility.Visible;
            PageNumberCurrent--;
            lstLuongCBPT = new List<ListResult_CDL>();
            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLuongCB.Count; i++)
            {
                lstLuongCBPT.Add(lstLuongCB[i]);
            }
            dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrushConverter brus = new BrushConverter();
                if (textPage1.Text != PageNumberCurrent.ToString() && int.Parse(textPage1.Text) == PageNumberCurrent - 1)
                {
                    if (PageNumberCurrent > 2)
                    {
                        textPage1.Text = (PageNumberCurrent - 2).ToString();
                        textPage2.Text = (PageNumberCurrent - 1).ToString();
                        textPage3.Text = PageNumberCurrent.ToString();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    }
                    else if (TongSoTrang > 2)
                    {
                        textPage1.Text = (PageNumberCurrent - 1).ToString();
                        textPage2.Text = (PageNumberCurrent).ToString();
                        textPage3.Text = (PageNumberCurrent + 1).ToString();
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        textPage1.Text = "1";
                        textPage2.Text = "2";
                        textPage3.Text = (PageNumberCurrent + 1).ToString();
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage3.Visibility = Visibility.Collapsed;
                    }
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    PageNumberCurrent--;
                    lstLuongCBPT = new List<ListResult_CDL>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLuongCB.Count; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                }
                else
                {
                    if (textPage1.Text != PageNumberCurrent.ToString())
                    {
                        if (PageNumberCurrent == 3)
                        {
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
                            if (TongSoTrang > 2)
                            {
                                borPage2.Visibility = Visibility.Visible;
                                borPage3.Visibility = Visibility.Visible;
                            }
                            else if (TongSoTrang > 1)
                            {
                                borPage2.Visibility = Visibility.Visible;
                                borPage3.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                borPage2.Visibility = Visibility.Collapsed;
                                borPage3.Visibility = Visibility.Collapsed;
                                borPageCuoi.Visibility = Visibility.Collapsed;
                                borLen1.Visibility = Visibility.Collapsed;
                            }
                            if (TongSoTrang > 1)
                            {
                                borPageCuoi.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                            }
                            lstLuongCBPT = new List<ListResult_CDL>();
                            for (int i = 0; i < 10; i++)
                            {
                                lstLuongCBPT.Add(lstLuongCB[i]);
                            }
                            dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                            PageNumberCurrent = 1;
                        }
                        else
                        {
                            textPage1.Text = (PageNumberCurrent - 3).ToString();
                            textPage2.Text = (PageNumberCurrent - 2).ToString();
                            textPage3.Text = (PageNumberCurrent - 1).ToString();
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPageCuoi.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageDau.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                            PageNumberCurrent -= 2;
                            lstLuongCBPT = new List<ListResult_CDL>();
                            if (lstLuongCB.Count > 10)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLuongCB.Count; i++)
                                {
                                    lstLuongCBPT.Add(lstLuongCB[i]);
                                }
                                //lstLuongCB = luongCB.listResult;
                                dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                            }
                        }
                    }
                }
            }
            catch { }
        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                
                {
                    BrushConverter brus = new BrushConverter();
                    if (PageNumberCurrent.ToString() != textPage2.Text)
                    {
                        PageNumberCurrent = int.Parse(textPage2.Text);
                        if (TongSoTrang >= 3)
                        {
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPageDau.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Visibility = Visibility.Collapsed;
                            borPageCuoi.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                            borPageDau.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                        }
                    }
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    lstLuongCBPT = new List<ListResult_CDL>();
                    if (lstLuongCB.Count > 10)
                    {
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLuongCB.Count; i++)
                        {
                            lstLuongCBPT.Add(lstLuongCB[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                    }
                }

            }
            catch
            {

            }

        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (PageNumberCurrent != TongSoTrang)
                {
                    if (PageNumberCurrent.ToString() != textPage3.Text && PageNumberCurrent > int.Parse(textPage3.Text) - 2)
                    {
                        if (PageNumberCurrent < TongSoTrang - 1)
                        {
                            textPage1.Text = PageNumberCurrent.ToString();
                            textPage2.Text = (PageNumberCurrent + 1).ToString();
                            textPage3.Text = (PageNumberCurrent + 2).ToString();
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        }
                        else if (TongSoTrang >= 3)
                        {
                            textPage1.Text = (PageNumberCurrent - 1).ToString();
                            textPage2.Text = (PageNumberCurrent).ToString();
                            textPage3.Text = (PageNumberCurrent + 1).ToString();
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPageCuoi.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            textPage1.Text = "1";
                            textPage2.Text = "2";
                            textPage3.Text = (PageNumberCurrent + 1).ToString();
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPageCuoi.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                            borPage3.Visibility = Visibility.Collapsed;
                        }
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                        PageNumberCurrent++;
                        lstLuongCBPT = new List<ListResult_CDL>();
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLuongCB.Count; i++)
                        {
                            lstLuongCBPT.Add(lstLuongCB[i]);
                        }
                        dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                    }
                    else
                    {
                        if (TongSoTrang == 3)
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
                            PageNumberCurrent = TongSoTrang;
                            lstLuongCBPT = new List<ListResult_CDL>();
                            for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                            {
                                lstLuongCBPT.Add(lstLuongCB[i]);
                            }
                            dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                        }
                        else
                        {
                            BrushConverter brus = new BrushConverter();
                            textPage1.Text = (PageNumberCurrent + 1).ToString();
                            textPage2.Text = (PageNumberCurrent + 2).ToString();
                            textPage3.Text = (PageNumberCurrent + 3).ToString();
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPageCuoi.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageDau.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                            PageNumberCurrent += 2;
                            lstLuongCBPT = new List<ListResult_CDL>();
                            if (lstLuongCB.Count > 10)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLuongCB.Count; i++)
                                {
                                    lstLuongCBPT.Add(lstLuongCB[i]);
                                }
                                dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                            }
                        }
                    }
                }
                
            }
            catch { }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(PageNumberCurrent < TongSoTrang - 1)
            {
                textPage1.Text = PageNumberCurrent.ToString();
                textPage2.Text = (PageNumberCurrent + 1).ToString();
                textPage3.Text = (PageNumberCurrent + 2).ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            }
            else if (TongSoTrang >= 3)
            {
                textPage1.Text = (PageNumberCurrent - 1).ToString();
                textPage2.Text = (PageNumberCurrent).ToString();
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPage1.Text = "1";
                textPage2.Text = "2";
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
            }
            borPageDau.Visibility = Visibility.Visible;
            borLui1.Visibility = Visibility.Visible;
            PageNumberCurrent++;
            lstLuongCBPT = new List<ListResult_CDL>();
            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLuongCB.Count; i++)
            {
                lstLuongCBPT.Add(lstLuongCB[i]);
            }
            dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (TongSoTrang >= 3)
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
                }
                else if (TongSoTrang == 2)
                {
                    textPage3.Text = TongSoTrang.ToString();
                    textPage2.Text = "2";
                    textPage1.Text = "1";
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Visibility = Visibility.Collapsed;
                }
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                PageNumberCurrent = TongSoTrang;
                lstLuongCBPT = new List<ListResult_CDL>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstLuongCBPT.Add(lstLuongCB[i]);
                }
                dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
            }
            catch (Exception)
            {
            }

            try
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                if (TongSoTrang >= 3)
                {
                    textPage1.Text = (TongSoTrang - 2).ToString();
                    textPage2.Text = (TongSoTrang - 1).ToString();
                    textPage3.Text = TongSoTrang.ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    lstLuongCBPT = new List<ListResult_CDL>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                }
                else
                {
                    textPage1.Text = (TongSoTrang - 1).ToString();
                    textPage2.Text = TongSoTrang.ToString();
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    lstLuongCBPT = new List<ListResult_CDL>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                }
            }
            catch (Exception)
            { }
        }
        #region Hover Event
        private void gr_ThongTinNhanVien_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                System.Windows.Controls.Border bodThongTinNhanVien = FindChild<System.Windows.Controls.Border>(row, "bodThongTinNhanVien");

                if (bodThongTinNhanVien != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodThongTinNhanVien.Visibility = Visibility.Visible;
                }
            }
        }

        private void gr_ThongTinNhanVien_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            // Lấy hàng (row) được nhấn chuột
            DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);

            if (row != null)
            {
                // Tìm Border có x:Name="bodXoaNhanVien" bên trong hàng
                System.Windows.Controls.Border bodThongTinNhanVien = FindChild<System.Windows.Controls.Border>(row, "bodThongTinNhanVien");

                if (bodThongTinNhanVien != null)
                {
                    // Thực hiện xử lý khi chuột vào Border "bodXoaNhanVien"
                    // Ví dụ: Hiển thị nội dung khi chuột hover vào đó
                    bodThongTinNhanVien.Visibility = Visibility.Collapsed;
                }
            }
        }
        #endregion

        #region Comons
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
        List<ListUser> listSaftSearch = new List<ListUser>();
        List<ListUser> lstSelectSaft = new List<ListUser>();

        private void bod_TenNhanVien_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.ListViewItem row = FindAncestor<System.Windows.Controls.ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {

                System.Windows.Controls.Border bod_TenNhanVien = FindChild<System.Windows.Controls.Border>(row, "bod_TenNhanVien");
                TextBlock tb_TenNhanVien = FindChild<TextBlock>(row, "tb_TenNhanVien");
                if (bod_TenNhanVien != null && tb_TenNhanVien != null)
                {
                    bod_TenNhanVien.Background = (Brush)br.ConvertFrom("#4C5BD4");
                    tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
                }
            }
        }

        private void bod_TenNhanVien_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            System.Windows.Controls.ListViewItem row = FindAncestor<System.Windows.Controls.ListViewItem>((DependencyObject)e.OriginalSource);

            if (row != null)
            {

                System.Windows.Controls.Border bod_TenNhanVien = FindChild<System.Windows.Controls.Border>(row, "bod_TenNhanVien");
                TextBlock tb_TenNhanVien = FindChild<TextBlock>(row, "tb_TenNhanVien");
                if (bod_TenNhanVien != null && tb_TenNhanVien != null)
                {
                    bod_TenNhanVien.Background = (Brush)br.ConvertFrom("#FFFFFF");
                    tb_TenNhanVien.Foreground = (Brush)br.ConvertFrom("#474747");
                }
            }
        }

        private void btn_SelectListSafff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void btnThongKe_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnThongKe.Background = (Brush)br.ConvertFrom("#4AA7FF");
        }

        private void btnThongKe_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            btnThongKe.Background = (Brush)br.ConvertFrom("#4c5bd4");
        }
        public Key keyDown { get; set; }
        private void ucSalary_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                keyDown = e.Key;
            }
        }

        private void ucSalary_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key == keyDown)
            {
                keyDown = Key.Cancel;
            }
        }

        private void bodImportExeclSalary_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ucThemFileLuong ucs = new ucThemFileLuong(Main);
            Main.grShowPopup.Children.Add(new ucThemFileLuong(Main));
        }

        int IdPB;
        private void Organize_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (searchBarToChuc.SelectedItem != null)
                {
                    searchBarToChuc.PlaceHolderForground = "#474747";
                }
                else
                {
                    searchBarToChuc.PlaceHolderForground = "#ACACAC";
                }
                OOP.OrganizeData tc = (OOP.OrganizeData)searchBarToChuc.SelectedItem;
                if (tc != null)
                {
                    borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                    SelectedOrganize = tc;
                    Main.ToChuc = tc.organizeDetailName;
                    SearchPB = tc.organizeDetailName.ToString();
                    if (tc.organizeDetailName == "Tổ chức (tất cả)")
                    {
                        searchBarNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
                    }
                    else
                    {
                        lstSearchNVTheoPhongBan = new List<ListUser>();

                        foreach (var nv in Main.lstNhanVienThuocCongTy)
                        {
                            if (nv.organizeDetail != null)
                            {
                                if (nv.organizeDetail.listOrganizeDetailId.Find(x => x.organizeDetailId == tc.id) != null)
                                {
                                    lstSearchNVTheoPhongBan.Add(nv);
                                }
                            }
                        }
                        ListUser user = new ListUser();
                        user.idQLC = 0;
                        user.userName = "Tất cả nhân viên";
                        lstSearchNVTheoPhongBan2.Insert(0, user);
                        searchBarNhanVien.ItemsSource = lstSearchNVTheoPhongBan;
                        lstNhanVien = lstSearchNVTheoPhongBan;
                    }
                }
            }
            catch (Exception)
            {
            } 
        }
        private string IdNV = "0";
        private void Staff_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (searchBarNhanVien.SelectedItem != null)
            {
                searchBarNhanVien.PlaceHolderForground = "#474747";
                listSaftSearch = new List<ListUser>();
                var chonca = ((ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                IdNV = chonca;
                SearchNV = ((ListUser)searchBarNhanVien.SelectedItem).idQLC.ToString();
                if (!Main.lstNhanVienThuocCongTy.Any(item => item.idQLC.ToString() == chonca))
                {
                    listSaftSearch = Main.lstNhanVienThuocCongTy.ToList();
                }
            }
            else
            {
                searchBarNhanVien.PlaceHolderForground = "#ACACAC";
            }
        }
    }
}
