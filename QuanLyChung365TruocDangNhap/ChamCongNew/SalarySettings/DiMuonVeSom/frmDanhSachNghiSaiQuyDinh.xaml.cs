using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom;
using Newtonsoft.Json;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Win32;
using System.Net;
using System.Text;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.DiMuonVeSom
{
    /// <summary>
    /// Interaction logic for frmDanhSachNghiSaiQuyDinh.xaml
    /// </summary>
    public partial class frmDanhSachNghiSaiQuyDinh : Page
    {
        MainWindow Main; 
        BrushConverter brus = new BrushConverter();
        List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public List<OOP.CaiDatLuong.clsShift.Item> lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();
        public DataTable tb_SaiQD = new DataTable();
       
        private string IdNV = "0";
        private string IdPB = "0";
        public frmDanhSachNghiSaiQuyDinh(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            main.i = 3;
            LoadDLNam();
            LoadDLThang();
            LoadDLNhanVien();
            LoadDLCaLV();
            LoadDLNghiSaiQD1();
        }
        private async void LoadDLCaLV()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = "http://210.245.108.202:3000/api/qlc/shift/list";
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var kq = await httpClient.GetAsync(url);
                    OOP.CaiDatLuong.clsShift.Root CaLV = JsonConvert.DeserializeObject<OOP.CaiDatLuong.clsShift.Root>(kq.Content.ReadAsStringAsync().Result);
                    if (CaLV.data != null)
                    {
                        foreach (var calv in CaLV.data.items)
                        {
                            lstShift.Add(calv);
                        }
                    }
                }
            }
            catch
            {
            }
        }

        private List<List_NSQD> lstNghiSaiQD = new List<List_NSQD>();
        private List<List_NSQD> lstNghiSaiQDCPT = new List<List_NSQD>();
        public void LoadDLNghiSaiQD1()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    loadingSpinner.Visibility = Visibility.Visible;
                    lstNghiSaiQD.Clear();
                    lstNghiSaiQDCPT.Clear();
                    int year, month;
                    if (lsvNam.SelectedItem != null) year = int.Parse(lsvNam.SelectedItem.ToString().Split(' ')[1]);
                    else year = DateTime.Now.Year;
                    if (lsvThang.SelectedItem != null) month = int.Parse(lsvThang.SelectedItem.ToString().Split(' ')[1]);
                    else month = DateTime.Now.Month;
                    request.QueryString.Add("com_id", Main.IdAcount.ToString());
                    request.QueryString.Add("start_date", year + "/" + month + "/01");
                    request.QueryString.Add("end_date", year + "/" + month + "/" + DateTime.DaysInMonth(year, month));
                    if (lsvNhanVien.SelectedItem != null)
                    {
                        request.QueryString.Add("ep_id", ((OOP.clsNhanVienThuocCongTy.ListUser)lsvNhanVien.SelectedItem).idQLC.ToString());
                    }
                    request.QueryString.Add("token", Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        try
                        {
                            Root_NSQD NSQD = JsonConvert.DeserializeObject<Root_NSQD>(UnicodeEncoding.UTF8.GetString(e.Result));
                            if (NSQD.data != null)
                            {
                                lstNghiSaiQD = NSQD.data;
                                foreach (var item in lstNghiSaiQD)
                                {
                                    item.avatarUser = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=64&q=75";
                                    item.date_format = $"{item.date.Day}-{item.date.Month}-{item.date.Year}";
                                }

                                if (lstNghiSaiQD.Count > 10)
                                {
                                    TongSoTrang = lstNghiSaiQD.Count / 10;
                                    SoDu = 10 - (lstNghiSaiQD.Count % 10);
                                    if (lstNghiSaiQD.Count % 10 > 0)
                                    {
                                        TongSoTrang++;
                                    }
                                    if (TongSoTrang < 3)
                                    {
                                        borPage3.Visibility = Visibility.Collapsed;
                                    }
                                    for (int i = 0; i < 10; i++)
                                    {
                                        lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                                    }
                                    dgv.ItemsSource = lstNghiSaiQDCPT;

                                    DpPhanTRang.Visibility = Visibility.Visible;
                                }
                                else
                                {
                                    dgv.ItemsSource = lstNghiSaiQDCPT;
                                    DpPhanTRang.Visibility = Visibility.Collapsed;
                                }
                            }
                        }
                        catch (Exception)
                        {
                        }
                        loadingSpinner.Visibility = Visibility.Collapsed;
                    };
                    request.UploadValuesTaskAsync("https://api.timviec365.vn/api/tinhluong/congty/take_listuser_nghi_khong_phep", request.QueryString);
                }
            }
            catch
            {
                loadingSpinner.Visibility = Visibility.Collapsed;
            }
        }
        private void LoadDLNhanVien()
        {
            lsvNhanVien.ItemsSource = Main.lstNhanVienThuocCongTy;
        }

        private void LoadDLThang()
        {
            List<string> lstThang = new List<string>();
            lstThang.Add("Tháng 1");
            lstThang.Add("Tháng 2");
            lstThang.Add("Tháng 3");
            lstThang.Add("Tháng 4");
            lstThang.Add("Tháng 5");
            lstThang.Add("Tháng 6");
            lstThang.Add("Tháng 7");
            lstThang.Add("Tháng 8");
            lstThang.Add("Tháng 9");
            lstThang.Add("Tháng 10");
            lstThang.Add("Tháng 11");
            lstThang.Add("Tháng 12");
            lsvThang.ItemsSource = lstThang;
            lsvThang.PlaceHolder = "Tháng " + DateTime.Now.Month;
        }

        private void LoadDLNam()
        {
            List<string> lstNam = new List<string>();
            lstNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
            lstNam.Add("Năm " + DateTime.Now.Year);
            lstNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
            lsvNam.ItemsSource = lstNam;
            lsvNam.PlaceHolder = lstNam[1];
        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoadDLNghiSaiQD1();
        }

        string ErrorSytem;
        private void btnXuatFileTK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
                if (string.IsNullOrEmpty(filePath)) { MessageBox.Show("Đường dẫn không hợp lệ"); return; }
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                try
                {
                    using (ExcelPackage p = new ExcelPackage())
                    {
                        // đặt tên người tạo file
                        p.Workbook.Properties.Author = "QuanLyChung365TruocDangNhap.ChamCongNew";
                        // đặt tiêu đề cho file
                        p.Workbook.Properties.Title = "Danh sách thưởng phạt";
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
                            "Mã Nv", "Họ tên","Tổ chức", "Thời gian áp dụng", "Ca làm việc", "Mức phạt"
                        };
                        // lấy ra số lượng cột cần dùng dựa vào số lượng header
                        var countColHeader = arrColumnHeader.Count();
                        // merge các column lại từ column 1 đến số column header
                        // gán giá trị cho cell vừa merge 
                        ws.Cells[1, 1].Value = $"Danh sách nghỉ sai quy định";
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
                        List<List_NSQD> userList = lstNghiSaiQD.Cast<List_NSQD>().ToList();
                        //với mỗi item trong danh sách sẽ ghi trên 1 dòng
                        foreach (var item in userList)
                        {
                            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                            colIndex = 1;
                            // rowIndex tương ứng từng dòng dữ liệu
                            rowIndex++;
                            //gán giá trị cho từng cell                      
                            ws.Cells[rowIndex, colIndex++].Value = item.ep_id;
                            ws.Cells[rowIndex, colIndex++].Value = item.userName;
                            ws.Cells[rowIndex, colIndex++].Value = item.organizeDetailName;
                            ws.Cells[rowIndex, colIndex++].Value = item.date_format;
                            ws.Cells[rowIndex, colIndex++].Value = item.shift_name;
                            ws.Cells[rowIndex, colIndex++].Value = "Phạt: " + item.phat + " VNĐ";
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

                if (lstNghiSaiQD == null)
                {
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this, lstNghiSaiQD));
                    return;
                }
            }
            catch (Exception)
            {
                ErrorSytem = "Error";
                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
            }
        }
        private void Staff_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (lsvNhanVien.SelectedItem != null)
            {
                lsvNhanVien.PlaceHolderForground = "#474747";
            }
            else
            {
                lsvNhanVien.PlaceHolderForground = "#ACACAC";
            }
        }
        private int TongSoTrang = 0;
        private int PageNumberCurrent = 1;
        private int SoDu = 0;
        public int NumberPerPage = 10;
        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                borPageCuoi.Visibility = Visibility.Visible;
                lstNghiSaiQDCPT = new List<List_NSQD>();
                for (int i = 0; i < 10; i++)
                {
                    lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                }
                dgv.ItemsSource = lstNghiSaiQDCPT;
            }
            catch (Exception)
            {
            }
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            try
            {
                if (int.Parse(textPage1.Text) > 1)
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
                        lstNghiSaiQDCPT = new List<List_NSQD>();
                        for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                        {
                            lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                        }
                        dgv.ItemsSource = lstNghiSaiQDCPT;
                    }
                    else
                    {
                        if (textPage1.Text == "2")
                        {
                            borLui1.Visibility = Visibility.Collapsed;
                        }
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
                            lstNghiSaiQDCPT = new List<List_NSQD>();
                            for (int i = 0; i < 10; i++)
                            {
                                lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                            }
                            dgv.ItemsSource = lstNghiSaiQDCPT;
                        }
                        else
                        {
                            textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                            textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                            textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                            lstNghiSaiQDCPT = new List<List_NSQD>();
                            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                            {
                                lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                            }
                            dgv.ItemsSource = lstNghiSaiQDCPT;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (int.Parse(textPage1.Text) > 1)
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
                        lstNghiSaiQDCPT = new List<List_NSQD>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                        }
                        dgv.ItemsSource = lstNghiSaiQDCPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstNghiSaiQDCPT = new List<List_NSQD>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                        }
                        dgv.ItemsSource = lstNghiSaiQDCPT;
                    }
                }
                else
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
                    lstNghiSaiQDCPT = new List<List_NSQD>();
                    for (int i = 0; i < 10; i++)
                    {
                        lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                    }
                    dgv.ItemsSource = lstNghiSaiQDCPT;
                }
            }
            catch (Exception)
            {
            }
        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (TongSoTrang == 2)
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    lstNghiSaiQDCPT = new List<List_NSQD>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10 - 10 + (10 - SoDu); i++)
                    {
                        lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                    }
                    dgv.ItemsSource = lstNghiSaiQDCPT;
                }
                else if (TongSoTrang > 2)
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    lstNghiSaiQDCPT = new List<List_NSQD>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                    }
                    dgv.ItemsSource = lstNghiSaiQDCPT;
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
                if (textPage3.Text == TongSoTrang.ToString())
                {
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    lstNghiSaiQDCPT = new List<List_NSQD>();

                    for (int i = int.Parse(textPage3.Text) * 10 - 10; i < int.Parse(textPage3.Text) * 10 - 10 + (10 - SoDu); i++)
                    {
                        lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                    }
                    dgv.ItemsSource = lstNghiSaiQDCPT;
                }
                else
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
                        lstNghiSaiQDCPT = new List<List_NSQD>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                        }
                        dgv.ItemsSource = lstNghiSaiQDCPT;
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
                            lstNghiSaiQDCPT = new List<List_NSQD>();
                            for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                            {
                                lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                            }
                            dgv.ItemsSource = lstNghiSaiQDCPT;
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
                            lstNghiSaiQDCPT = new List<List_NSQD>();
                            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                            {
                                lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                            }
                            dgv.ItemsSource = lstNghiSaiQDCPT;
                        }
                        else
                        {
                            textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                            textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                            textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                            lstNghiSaiQDCPT = new List<List_NSQD>();
                            for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                            {
                                lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                            }
                            dgv.ItemsSource = lstNghiSaiQDCPT;
                        }

                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                    lstNghiSaiQDCPT = new List<List_NSQD>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                    }
                    dgv.ItemsSource = lstNghiSaiQDCPT;
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
                        lstNghiSaiQDCPT = new List<List_NSQD>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                        }
                        dgv.ItemsSource = lstNghiSaiQDCPT;

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
                            lstNghiSaiQDCPT = new List<List_NSQD>();
                            for (int i = 10; i < 20; i++)
                            {
                                lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                            }
                            dgv.ItemsSource = lstNghiSaiQDCPT;

                        }
                        else if (borPageDau.Visibility == Visibility.Visible && borPageCuoi.Visibility == Visibility.Visible)
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
                            lstNghiSaiQDCPT = new List<List_NSQD>();
                            for (int i = 20; i < 30; i++)
                            {
                                lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                            }
                            dgv.ItemsSource = lstNghiSaiQDCPT;
                        }


                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                        lstNghiSaiQDCPT = new List<List_NSQD>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                        }
                        dgv.ItemsSource = lstNghiSaiQDCPT;
                    }

                }
            }
            catch (Exception)
            {
            }
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
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
                    lstNghiSaiQDCPT = new List<List_NSQD>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                    {
                        lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                    }
                    dgv.ItemsSource = lstNghiSaiQDCPT;
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
                    lstNghiSaiQDCPT = new List<List_NSQD>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstNghiSaiQDCPT.Add(lstNghiSaiQD[i]);
                    }
                    dgv.ItemsSource = lstNghiSaiQDCPT;
                }
            }
            catch (Exception)
            { }
        }

        private void cboSelectNumberPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(Main != null)
            //{
            //    if (cboSelectNumberPerPage.SelectedIndex == 0) NumberPerPage = 10;
            //    if (cboSelectNumberPerPage.SelectedIndex == 1) NumberPerPage = 20;
            //    if (cboSelectNumberPerPage.SelectedIndex == 2) NumberPerPage = 50;
            //    if (cboSelectNumberPerPage.SelectedIndex == 3) NumberPerPage = 100;
            //    LoadDLNghiSaiQD1();
            //}
        }
    }
}
