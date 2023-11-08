using ChamCong365.TimeKeeping;
using Microsoft.Win32;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using ChamCong365.Popup.DatePicker;
using ChamCong365.Popup.PopupSalarySettings;

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
    public partial class ucBasicSalary : UserControl
    {
        private int i = 0;
        MainWindow Main;
        List<SalarySaff> salarSaffs = new List<SalarySaff>();
        List<string> ListRoom = new List<string>() { " Kỹ Thuật", "Kinh Doanh, Bán Hàng & Maketing", "Đề Án", "Sale", "Đào tạo", "Sáng Tạo" };
        public ucBasicSalary(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc A", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001",lienhe= "0393039393 ngockyto@gmail.com",luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc B", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc C", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc D", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc E", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc F", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc G", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc A", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc B", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc C", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc D", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc E", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc F", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            salarSaffs.Add(new SalarySaff() { Id = 1, name = "Tô Ngọc G", anh = "./Resource/image/KyTo.jpg", phongban = "Kỹ Thuật", chucvu = "ok", hopdongapdung = "ngockyto001", lienhe = "0393039393 ngockyto@gmail.com", luongcoban = 100000000 });
            lsvSelectRoomBS.ItemsSource = ListRoom;
            dgvListSalaryBasic.ItemsSource = salarSaffs;
            txbSelectedDays.Text = DateTime.Now.ToShortDateString();
            var namesaff = from saff in salarSaffs select saff.name;
            cboloadName.ItemsSource = namesaff;
            
            //lsvListSalarySaff.ItemsSource = salarSaffs;
        }

        private void ExportExcelSalary_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string filePath = "";

            //SaveFileDiaLog lưu file Excel
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            //Đọc ra các file có định dạng excel
            saveFileDialog.Filter = "Excel | *.xlsx | Excel 2016 | *.xls";
            //Lưu đường dẫn file 
            if (saveFileDialog.ShowDialog() == true)
            {
                filePath = saveFileDialog.FileName;
            }
            //File rỗng 
            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Đường dẫn không hợp lệ");
                return;
            }
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            try
            {
                using (ExcelPackage p = new ExcelPackage())
                {

                    // đặt tên người tạo file
                    p.Workbook.Properties.Author = "Chamcong365";

                    // đặt tiêu đề cho file
                    p.Workbook.Properties.Title = "Xuất công";

                    //Tạo một sheet để làm việc trên đó
                    p.Workbook.Worksheets.Add("sheet ExportWork");

                    // lấy sheet vừa add ra để thao tác
                    ExcelWorksheet ws = p.Workbook.Worksheets[0];

                    // đặt tên cho sheet
                    ws.Name = "ExportWork";
                    // fontsize mặc định cho cả sheet
                    ws.Cells.Style.Font.Size = 12;
                    // font family mặc định cho cả sheet
                    ws.Cells.Style.Font.Name = "Calibri";

                    // Tạo danh sách các column header
                    string[] arrColumnHeader = {
                        "Mã Nv", "Họ tên","Lương cơ bản", "Phòng ban","Chức vụ","Liên hệ "
                    };
                    // lấy ra số lượng cột cần dùng dựa vào số lượng header
                    var countColHeader = arrColumnHeader.Count();
                    // merge các column lại từ column 1 đến số column header
                    // gán giá trị cho cell vừa merge 
                    ws.Cells[1, 1].Value = "Lương cơ bản";
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
                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(System.Drawing.Color.LightBlue);

                        //căn chỉnh các border
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;

                        //gán giá trị
                        cell.Value = item;

                        colIndex++;
                    }
                    // lấy ra danh sách UserInfo từ ItemSource của ListView
                    //List<SalarySaff> userList = lsvListSalarySaff.ItemsSource.Cast<SalarySaff>().ToList();
                    // với mỗi item trong danh sách sẽ ghi trên 1 dòng
                    //foreach (var item in userList)
                    //{
                    //    // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
                    //    colIndex = 1;

                    //    // rowIndex tương ứng từng dòng dữ liệu
                    //    rowIndex++;

                    //    //gán giá trị cho từng cell                      
                    //    ws.Cells[rowIndex, colIndex++].Value = item.Id;
                    //    ws.Cells[rowIndex, colIndex++].Value = item.name;
                    //    ws.Cells[rowIndex, colIndex++].Value = item.luongcoban;
                    //    ws.Cells[rowIndex, colIndex++].Value = item.phongban;
                    //    ws.Cells[rowIndex, colIndex++].Value = item.chucvu;
                    //    ws.Cells[rowIndex, colIndex++].Value = item.lienhe;

                    //}
                    //Lưu file lại
                    Byte[] bin = p.GetAsByteArray();
                    File.WriteAllBytes(filePath, bin);
                }
                MessageBox.Show("Xuất excel thành công!");
            }
            catch (Exception)
            {

                MessageBox.Show("Có lỗi khi lưu file!");
            }
        }

        private void bodImportExeclSalary_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ucCreateFileSalary ucs = new ucCreateFileSalary(Main);
            Main.grShowPopup.Children.Add(new ucCreateFileSalary(Main));
        }

        private void dapDay_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            //txbSelectedDay.Text = dapDay.SelectedDate.ToString();
            //bodCalendarDay.Visibility = Visibility.Collapsed;
        }

        private void bodSelectDaySalary_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (bodCalendarDay.Visibility == Visibility.Collapsed)
            {
                bodCalendarDay.Visibility = Visibility.Visible;
                bodSelectRoomCollapsed.Visibility = Visibility.Collapsed;
            }
            else
            {
                bodCalendarDay.Visibility = Visibility.Collapsed;
            }
            //ucCalendar uc = new ucCalendar(Main);
            //Main.grShowPopup.Children.Add(new ucCalendar(Main));
        }



        private void bodSelectRoom_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (bodSelectRoomCollapsed.Visibility == Visibility.Collapsed)
            {
                bodSelectRoomCollapsed.Visibility = Visibility.Visible;
                bodCalendarDay.Visibility = Visibility.Collapsed;

            }
            else
            {
                bodSelectRoomCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvSelectRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSelectRooms.Text = lsvSelectRoomBS.SelectedItem.ToString();
            bodSelectRoomCollapsed.Visibility = Visibility.Collapsed;
        }

        private void dapDay_SelectedDatesChanged_1(object sender, SelectionChangedEventArgs e)
        {
            txbSelectedDays.Text = dapDay.SelectedDates.ToString();
            bodCalendarDay.Visibility= Visibility.Collapsed;
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
            ExportExcelSalary.BorderThickness= new Thickness(1);
        }

        private void ExportExcelSalary_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            ExportExcelSalary.BorderThickness = new Thickness(0);
        }
    }
}
