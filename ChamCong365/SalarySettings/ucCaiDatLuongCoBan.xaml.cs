using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using ChamCong365.Popup.PopupSalarySettings;
using ChamCong365.Popup.CaiDatLuong;
using ChamCong365.Salarysettings;
using ChamCong365.Popup.CaiDatLuong.CaiDatNhapLuongCoBan;
using System.Windows.Input;
using RestSharp;
using Newtonsoft.Json;
using System.Net;
using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using System.Text;
using System.Windows.Media;
using System.Data;
using System.Windows.Forms;

namespace ChamCong365.SalarySettings
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
        
        public class PhongBan
        {
            public string TenPB { get; set; }
        }
        MainWindow Main;
        List<SalarySaff> salarSaffs = new List<SalarySaff>();
        List<string> ListRoom = new List<string>() { " Kỹ Thuật", "Kinh Doanh, Bán Hàng & Maketing", "Đề Án", "Sale", "Đào tạo", "Sáng Tạo" };
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNVTheoPhongBan = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        private List<clsLuongCoBan.ListResult> lstLuongCB = new List<clsLuongCoBan.ListResult>();
        private List<clsLuongCoBan.ListResult> lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
        private List<clsLuongCoBan.ListResult> lstLuongCBFilter = new List<clsLuongCoBan.ListResult>();
        private List<clsLuongCoBan.ListResult> lstLuongCBFilter22222 = new List<clsLuongCoBan.ListResult>();
        public static DataTable tb_LuongCB = new DataTable();
        private int TongSoTrang = 0;
        private int SoDu = 0;
        public ucCaiDatLuongCoBan(MainWindow main)
        {
            InitializeComponent();
            
            tb_LuongCB = Function.clsExPortExcel.NewTables("tb_LuongCB", new string[] {"colIDNhanVien", "colTenNhanVien", "colLuongCB", "colHopDongAD", "colPhongBan", "colChucVu", "colLienHe" }, new int[] { 100, 150, 250, 100, 150, 150, 150, 150 });
            Main = main;
            SearchPB = "0";
            SearchNV = "0";
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDLLuongCaBan();
            

        }

        private void LoadDLLuongCaBan()
        {

            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_bangluong_coban")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddHeader("Authorization", Properties.Settings.Default.Token);
                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("month", DateTime.Now.Month);
                    request.AddParameter("year", DateTime.Now.Year);
                    if (DateTime.Now.Month < 10)
                    {
                        request.AddParameter("start_date", $"{DateTime.Now.Year}-0{DateTime.Now.Month}-01T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("start_date", $"{DateTime.Now.Year}-{DateTime.Now.Month}-01T00:00:00.000+00:00");
                    }
                    if (DateTime.Now.Month == 12)
                    {
                        request.AddParameter("end_date", $"{DateTime.Now.Year + 1}-01-01T00:00:00.000+00:00");

                    }
                    else
                    {
                        if (DateTime.Now.Month + 1 < 10)
                        {
                            request.AddParameter("end_date", $"{DateTime.Now.Year}-0{DateTime.Now.Month + 1}-01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("end_date", $"{DateTime.Now.Year}-{DateTime.Now.Month + 1}-01T00:00:00.000+00:00");
                        }

                    }
                    request.AddParameter("skip", "0");
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan.Root luongCB = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan.Root>(b);
                    if (luongCB.listResult != null)
                    {
                        foreach (var item in luongCB.listResult)
                        {
                            item.luong_co_ban_string = item.luong_co_ban.ToString() + " VND";
                            item.phan_tram_hop_dong_string = item.phan_tram_hop_dong.ToString() + "% hợp đồng";
                            foreach (var it in luongCB.listUser)
                            {
                                if (it.idQLC == item.ep_id)
                                {
                                    item.ep_phone = it.phone;
                                    item.ep_email = it.email;
                                    item.ep_address = it.address;
                                    item.ep_name = it.userName;
                                    item.ngay_bat_dau_lv = it.createdAt.ToString();
                                    WebClient httpClient2 = new WebClient();
                                    httpClient2.QueryString.Clear();
                                    httpClient2.QueryString.Add("ID", it._id.ToString());
                                    var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                                    APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                                    if (receiveInfoAPI.data != null)
                                    {
                                        if (receiveInfoAPI.data.user_info.AvatarUser == null)
                                        {
                                            item.ep_avatar = "/Resource/image/llll.jpg";
                                        }
                                        else
                                        {
                                            item.ep_avatar = receiveInfoAPI.data.user_info.AvatarUser;
                                        }
                                    }
                                }
                            }
                            //WebClient httpClient2 = new WebClient();
                            //httpClient2.QueryString.Clear();
                            //httpClient2.QueryString.Add("ID", item.ep_id.ToString());
                            //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            //APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            //if (receiveInfoAPI.data != null)
                            //{
                            //    item.ep_name = receiveInfoAPI.data.user_info.UserName;
                            //    item.ep_avatar = receiveInfoAPI.data.user_info.AvatarUser;
                            //}
                            lstLuongCB.Add(item);
                        }
                        //lstLuongCB = luongCB.listResult;
                        TongSoTrang = luongCB.listResult.Count / 10;
                        SoDu = 10 - (luongCB.listResult.Count % 10);
                        if (luongCB.listResult.Count % 10 > 0)
                        {
                            TongSoTrang++;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            lstLuongCBPT.Add(luongCB.listResult[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                        if (TongSoTrang < 3)
                        {
                            if (TongSoTrang == 2)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                                borLen1.Visibility = Visibility.Collapsed;
                                borPageCuoi.Visibility = Visibility.Collapsed;
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
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }
                        LoadDataInDataTable();
                    }
                }

            }
            catch
            {

            }
        }

        private void LoadDataInDataTable()
        {
            
            foreach(var item in lstLuongCB)
            {
                DataRow dr = tb_LuongCB.NewRow();
                
                dr["colIDNhanVien"] = item.ep_id;
                dr["colTenNhanVien"] = item.ep_name;
                dr["colLuongCB"] = item.luong_co_ban_string;
                dr["colHopDongAD"] = item.phan_tram_hop_dong_string;
                if (item.infordepartment != null)
                {
                    dr["colPhongBan"] = item.infordepartment.dep_name;
                }
                dr["colChucVu"] = item.infoposition;
                dr["colLienHe"] = item.ep_phone + " + " + item.ep_email + " + " + item.ep_address;
                tb_LuongCB.Rows.Add(dr);
            }
            
        }

        private void LoadDLNhanVien()
        {
            
            lsvNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
        }

        private void LoadDLPhongBan()
        {
            lsvPhongBan.ItemsSource = Main.lstPhongBanThuocCongTy;
        }
        private string DuongDanEx = Environment.CurrentDirectory + "\\TempExcel\\FileLuongCB.xltx";
        private void ExportExcelSalary_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (File.Exists(DuongDanEx))
            {
                Microsoft.Office.Interop.Excel.Application Ex = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook wb = Ex.Workbooks.Open(DuongDanEx);
                Microsoft.Office.Interop.Excel.Worksheet ws_HoaDon = wb.Worksheets["ChamCong"];
                int DongBatDau = 2;
                foreach (DataRow row in tb_LuongCB.Rows)
                {
                    for (int i = 0; i < tb_LuongCB.Columns.Count; i++)
                    {
                        ws_HoaDon.Cells[DongBatDau, i + 1] = row[i];
                    }
                    DongBatDau++;
                }
                System.Windows.Forms.SaveFileDialog frm = new System.Windows.Forms.SaveFileDialog();
                frm.Filter = "|*.xlsx";
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    wb.SaveAs(frm.FileName);
                    wb.Close();
                    Ex.Quit();
                }
            }
            
            



            //string filePath = "";

            ////SaveFileDiaLog lưu file Excel
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            ////Đọc ra các file có định dạng excel
            //saveFileDialog.Filter = "Excel | *.xlsx | Excel 2016 | *.xls";
            ////Lưu đường dẫn file 
            //if (saveFileDialog.ShowDialog() == true)
            //{
            //    filePath = saveFileDialog.FileName;
            //}
            ////File rỗng 
            //if (string.IsNullOrEmpty(filePath))
            //{
            //    MessageBox.Show("Đường dẫn không hợp lệ");
            //    return;
            //}
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //try
            //{
            //    using (ExcelPackage p = new ExcelPackage())
            //    {

            //        // đặt tên người tạo file
            //        p.Workbook.Properties.Author = "Chamcong365";

            //        // đặt tiêu đề cho file
            //        p.Workbook.Properties.Title = "Xuất công";

            //        //Tạo một sheet để làm việc trên đó
            //        p.Workbook.Worksheets.Add("sheet ExportWork");

            //        // lấy sheet vừa add ra để thao tác
            //        ExcelWorksheet ws = p.Workbook.Worksheets[0];

            //        // đặt tên cho sheet
            //        ws.Name = "ExportWork";
            //        // fontsize mặc định cho cả sheet
            //        ws.Cells.Style.Font.Size = 12;
            //        // font family mặc định cho cả sheet
            //        ws.Cells.Style.Font.Name = "Calibri";

            //        // Tạo danh sách các column header
            //        string[] arrColumnHeader = {
            //            "Mã Nv", "Họ tên","Lương cơ bản", "Phòng ban","Chức vụ","Liên hệ "
            //        };
            //        // lấy ra số lượng cột cần dùng dựa vào số lượng header
            //        var countColHeader = arrColumnHeader.Count();
            //        // merge các column lại từ column 1 đến số column header
            //        // gán giá trị cho cell vừa merge 
            //        ws.Cells[1, 1].Value = "Lương cơ bản";
            //        ws.Cells[1, 1, 1, countColHeader].Merge = true;
            //        // in đậm
            //        ws.Cells[1, 1, 1, countColHeader].Style.Font.Bold = true;
            //        // căn giữa
            //        ws.Cells[1, 1, 1, countColHeader].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
            //        int colIndex = 1;
            //        int rowIndex = 2;

            //        //tạo các header từ column header đã tạo từ bên trên
            //        foreach (var item in arrColumnHeader)
            //        {
            //            var cell = ws.Cells[rowIndex, colIndex];

            //            //set màu thành gray
            //            var fill = cell.Style.Fill;
            //            fill.PatternType = ExcelFillStyle.Solid;
            //            fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

            //            //căn chỉnh các border
            //            var border = cell.Style.Border;
            //            border.Bottom.Style =
            //                border.Top.Style =
            //                border.Left.Style =
            //                border.Right.Style = ExcelBorderStyle.Thin;

            //            //gán giá trị
            //            cell.Value = item;

            //            colIndex++;
            //        }
            //        // lấy ra danh sách UserInfo từ ItemSource của ListView
            //        //List<SalarySaff> userList = lsvListSalarySaff.ItemsSource.Cast<SalarySaff>().ToList();
            //        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
            //        //foreach (var item in userList)
            //        //{
            //        //    // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
            //        //    colIndex = 1;

            //        //    // rowIndex tương ứng từng dòng dữ liệu
            //        //    rowIndex++;

            //        //    //gán giá trị cho từng cell                      
            //        //    ws.Cells[rowIndex, colIndex++].Value = item.Id;
            //        //    ws.Cells[rowIndex, colIndex++].Value = item.name;
            //        //    ws.Cells[rowIndex, colIndex++].Value = item.luongcoban;
            //        //    ws.Cells[rowIndex, colIndex++].Value = item.phongban;
            //        //    ws.Cells[rowIndex, colIndex++].Value = item.chucvu;
            //        //    ws.Cells[rowIndex, colIndex++].Value = item.lienhe;

            //        //}
            //        //Lưu file lại
            //        Byte[] bin = p.GetAsByteArray();
            //        File.WriteAllBytes(filePath, bin);
            //    }
            //    MessageBox.Show("Xuất excel thành công!");
            //}
            //catch (Exception)
            //{

            //    MessageBox.Show("Có lỗi khi lưu file!");
            //}

        }

        private void bodImportExeclSalary_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ucThemFileLuong ucs = new ucThemFileLuong(Main);
            Main.grShowPopup.Children.Add(new ucThemFileLuong(Main));
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
            clsLuongCoBan.ListResult cls = (sender as System.Windows.Controls.Border).DataContext as clsLuongCoBan.ListResult;
            if (cls != null)
            {
                ucHoSoNhanVien uch = new ucHoSoNhanVien(Main, cls);
                ucListSalarySettings ucd = new ucListSalarySettings(Main);
                ucBodyHome ucb = new ucBodyHome(Main);
                Main.dopBody.Children.Clear();
                object Content = uch.Content;
                uch.Content = null;
                Main.dopBody.Children.Add(Content as UIElement);
                Main.LableFunction.Visibility = Visibility.Visible;
                //Main.txbLoadNameFunction.Text = ucb.txbSalarySettings.Text + " / " + ucd.txbFuncSalary01.Text + " / " + "Hồ Sơ Nhân Viên";
            }

        }

        private void cboloadName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void dgvListSalaryBasic_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void borHienThiNhanVien_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (borNhanVien.Visibility == Visibility.Collapsed)
            {
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borNhanVien.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }


        private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            foreach (var str in Main.lstNhanVienThuocCongTy)
            {
                if (str.userName.Contains(textSearchNhanVien.Text.ToString()) || str.idQLC.ToString().Contains(textSearchNhanVien.Text.ToString()))
                {
                    lstSearchNV.Add(str);

                }
            }
            lsvNhanVien.ItemsSource = lstSearchNV;
            if (textSearchNhanVien.Text == "")
            {
                lsvNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
            }
        }

        private void popup_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            
            if (borPhongBan.Visibility == Visibility.Visible)
            {
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
            }
            else if (borNhanVien.Visibility == Visibility.Visible)
            {
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
            }
        }

        private void lsvNhanVien_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            scrollNhanVien.ScrollToVerticalOffset(scrollNhanVien.VerticalOffset - e.Delta);
        }

        private void lsvPhongBan_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
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

        private void borTenPB_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            OOP.clsPhongBanThuocCongTy.Item pb = (sender as System.Windows.Controls.Border).DataContext as OOP.clsPhongBanThuocCongTy.Item;
            if (pb != null)
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                textHienThiPhongBan.Text = pb.dep_name;
                Main.PhongBan = pb.dep_name;
                SearchPB = pb.dep_id.ToString();
                if(pb.dep_name=="Phòng ban(tất cả)")
                {
                    lsvNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
                }
                else
                {
                    lstSearchNVTheoPhongBan = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
                    foreach (OOP.clsNhanVienThuocCongTy.ListUser nv in Main.lstNhanVienThuocCongTy)
                    {
                        if (nv.department != null)
                        {
                            foreach (var item in nv.department)
                            {
                                if (item.dep_name == pb.dep_name)
                                {
                                    lstSearchNVTheoPhongBan.Add(nv);

                                    
                                }
                            }
                        }
                    }
                    OOP.clsNhanVienThuocCongTy.ListUser user = new OOP.clsNhanVienThuocCongTy.ListUser();
                    user.idQLC = 0;
                    user.userName = "Tất cả nhân viên";
                    lstSearchNVTheoPhongBan.Insert(0, user);
                    lsvNhanVien.ItemsSource = null;
                    lsvNhanVien.ItemsSource = lstSearchNVTheoPhongBan;
                }
            }
        }

        private void borHienThiPhongBan_MouseLeftButtonUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (borPhongBan.Visibility == Visibility.Collapsed)
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borPhongBan.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void borTenNhanVien_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OOP.clsNhanVienThuocCongTy.ListUser nv = (sender as System.Windows.Controls.Border).DataContext as OOP.clsNhanVienThuocCongTy.ListUser;
            if (nv != null)
            {
                textHienThiNhanVien.Text = nv.userName;
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.NhanVien = nv.userName;
                SearchNV = nv.idQLC.ToString();
            }
        }
        private string SearchNV = "";
        private string SearchPB = "";
        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstLuongCBFilter = new List<clsLuongCoBan.ListResult>();
            lstLuongCBFilter22222 = new List<clsLuongCoBan.ListResult>();
        
            foreach(var item in lstLuongCB)
            {
                if (item.ep_id.ToString() == SearchNV && SearchPB == "0")
                {
                    lstLuongCBFilter.Add(item);
                    dgvListSalaryBasic.ItemsSource = lstLuongCBFilter;
                }
                if (item.infordepartment != null)
                {
                    if (SearchNV == "0" && Main.PhongBan == item.infordepartment.dep_name)
                    {
                        lstLuongCBFilter22222.Add(item);
                        dgvListSalaryBasic.ItemsSource = null;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBFilter22222;
                    }
                    else if (SearchNV == item.ep_id.ToString() && Main.PhongBan == item.infordepartment.dep_name)
                    {
                        lstLuongCBFilter.Add(item);
                        dgvListSalaryBasic.ItemsSource = null;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBFilter;
                    }
                }
                
            }
            if (SearchPB == "0" && SearchNV == "0")
            {
                dgvListSalaryBasic.ItemsSource = null;
                dgvListSalaryBasic.ItemsSource = lstLuongCB;
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
            lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
            for (int i = 0; i < 10; i++)
            {
                lstLuongCBPT.Add(lstLuongCB[i]);
            }
            //lstLuongCB = luongCB.listResult;
            dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
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
                    lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
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
                        lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstLuongCBPT.Add(lstLuongCB[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstLuongCBPT.Add(lstLuongCB[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                    }
                    
                    
                }
            }
            
        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage1.Text == (TongSoTrang - 2).ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
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
                    borLen1.Visibility = Visibility.Visible;
                    borPageCuoi.Visibility = Visibility.Visible;
                    lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
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
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                        lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstLuongCBPT.Add(lstLuongCB[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstLuongCBPT.Add(lstLuongCB[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                    }
                }
            }
        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage2.Foreground= (Brush)brus.ConvertFrom("#ffffff");
            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
            {
                lstLuongCBPT.Add(lstLuongCB[i]);
            }
            //lstLuongCB = luongCB.listResult;
            dgvListSalaryBasic.ItemsSource = lstLuongCBPT;

        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
                lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstLuongCBPT.Add(lstLuongCB[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
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
                    lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
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
                    lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
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
                lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstLuongCBPT.Add(lstLuongCB[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
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
                    lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;

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
                        lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                        for (int i = 10; i < 20; i++)
                        {
                            lstLuongCBPT.Add(lstLuongCB[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBPT;

                    }
                    else if(borPageDau.Visibility == Visibility.Visible && borPageCuoi.Visibility == Visibility.Visible)
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
                        lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                        for (int i = 20; i < 30; i++)
                        {
                            lstLuongCBPT.Add(lstLuongCB[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
                    }
                    

                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstLuongCBPT.Add(lstLuongCB[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
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
            textPage1.Foreground= (Brush)brus.ConvertFrom("#474747");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPageCuoi.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            lstLuongCBPT = new List<clsLuongCoBan.ListResult>();
            for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
            {
                lstLuongCBPT.Add(lstLuongCB[i]);
            }
            //lstLuongCB = luongCB.listResult;
            dgvListSalaryBasic.ItemsSource = lstLuongCBPT;
        }
    }
}
