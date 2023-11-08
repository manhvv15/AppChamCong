using Microsoft.Win32;

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucOutWork.xaml
    /// </summary>

    public class SaffOut
    {
        public string Image { get; set; }    
        public string Name { get; set; }
        public int ID { get; set; }
        public DateTime DateMonth { get; set; }
        public DateTime TimeOut { get; set; }
    }
    public partial class ucOutWork : UserControl
    {
        List<SaffOut> saffout = new List<SaffOut>();
        List<string> listCompany = new List<string>()
        {
            " FPT", "CTCP THANH TOANS HUNG HA 365", "Monsta-Lab", "Sun*"
        };
        List<string> ListRoom = new List<string>() { " Kỹ Thuật", "Kinh Doanh, Bán Hàng & Maketing", "Đề Án", "Sale", "Đào tạo", "Sáng Tạo" };
        public ucOutWork()
        {
            InitializeComponent();
            #region Fake
            saffout.Add(new SaffOut(){ ID = 1, Image = "./Resource/image/KyTo.jpg", Name = "Tô Ngọc Ký", DateMonth = DateTime.Now, TimeOut = DateTime.Now});
            saffout.Add(new SaffOut() { ID = 2, Image = "./Resource/image/KyTo.jpg", Name = "Nguyễn văn A", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 3, Image = "./Resource/image/KyTo.jpg", Name = "Nguyễn văn A", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 4, Image = "./Resource/image/KyTo.jpg", Name = "Tô Ngọc Ký", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 1, Image = "./Resource/image/KyTo.jpg", Name = "Tô Ngọc Ký", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 2, Image = "./Resource/image/KyTo.jpg", Name = "Nguyễn văn A", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 2, Image = "./Resource/image/KyTo.jpg", Name = "Nguyễn văn A", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 1, Image = "./Resource/image/KyTo.jpg", Name = "Tô Ngọc Ký", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 1, Image = "./Resource/image/KyTo.jpg", Name = "Tô Ngọc Ký", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 2, Image = "./Resource/image/KyTo.jpg", Name = "Nguyễn văn A", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 2, Image = "./Resource/image/KyTo.jpg", Name = "Nguyễn văn A", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 1, Image = "./Resource/image/KyTo.jpg", Name = "Tô Ngọc Ký", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 1, Image = "./Resource/image/KyTo.jpg", Name = "Tô Ngọc Ký", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 2, Image = "./Resource/image/KyTo.jpg", Name = "Nguyễn văn A", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 2, Image = "./Resource/image/KyTo.jpg", Name = "Nguyễn văn A", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            saffout.Add(new SaffOut() { ID = 1, Image = "./Resource/image/KyTo.jpg", Name = "Tô Ngọc Ký", DateMonth = DateTime.Now, TimeOut = DateTime.Now });
            #endregion
            lsvListSaffInOutWork.ItemsSource = saffout;
            lsvSelectCompaty.ItemsSource = listCompany;
            lsvSelectRoom.ItemsSource = ListRoom;
            var namesaff = from saff in saffout select saff.Name;
            lsvListNameSmallSaff.ItemsSource = namesaff;



        }

      
        private void ExportExcel_MouseUp(object sender, MouseButtonEventArgs e)
        {
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
            //            "Mã Nv", "Họ tên","Ngày Tháng", "Thời Gian"
            //        };
            //        // lấy ra số lượng cột cần dùng dựa vào số lượng header
            //        var countColHeader = arrColumnHeader.Count();
            //        // merge các column lại từ column 1 đến số column header
            //        // gán giá trị cho cell vừa merge 
            //        ws.Cells[1, 1].Value = "Thống kê Ngày công";
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
            //        List<SaffOut> userList = lsvListSaffInOutWork.ItemsSource.Cast<SaffOut>().ToList();
            //        // với mỗi item trong danh sách sẽ ghi trên 1 dòng
            //        foreach (var item in userList)
            //        {
            //            // bắt đầu ghi từ cột 1. Excel bắt đầu từ 1 không phải từ 0
            //            colIndex = 1;

            //            // rowIndex tương ứng từng dòng dữ liệu
            //            rowIndex++;

            //            //gán giá trị cho từng cell                      
            //            ws.Cells[rowIndex, colIndex++].Value = item.ID;
            //            ws.Cells[rowIndex, colIndex++].Value = item.Name;
            //            ws.Cells[rowIndex, colIndex++].Value = item.DateMonth.ToShortDateString();
            //            ws.Cells[rowIndex, colIndex++].Value = item.TimeOut.ToShortDateString();

            //        }
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

        private void lsvSelectCompaty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txbSelectCompany.Text = lsvSelectCompaty.SelectedItem.ToString();
            bodShowListCompany.Visibility = System.Windows.Visibility.Collapsed;
            //txbSelectCompany.Foreground = new SolidColorBrush(Colors.Black);

        }

        private void bodSelectCompanyOutWork_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodShowListCompany.Visibility == System.Windows.Visibility.Collapsed) 
            { 
                bodShowListCompany.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                bodShowListCompany.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void bodSelectListRoom_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodShowSelectListRoom.Visibility == System.Windows.Visibility.Collapsed)
            {
                bodShowSelectListRoom.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                bodShowSelectListRoom.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void lsvSelectRoom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //txtRoomText.Text = lsvSelectRoom.SelectedItem.ToString();
            bodShowSelectListRoom.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void txtSelectNameSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodSelectNameSaff.Visibility == System.Windows.Visibility.Collapsed)
            {
                bodSelectNameSaff.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                bodSelectNameSaff.Visibility = System.Windows.Visibility.Collapsed;
            }
        }

        private void lsvListNameSmallSaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txtSelectNameSaff.Text = lsvListNameSmallSaff.SelectedItem.ToString();
            txtSelectNameSaff.Foreground =  new SolidColorBrush(Colors.Black);
            bodShowSelectListRoom.Visibility = System.Windows.Visibility.Collapsed;
            bodShowListCompany.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void lsvSelectRoom_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            //txtRoomText.Text = lsvSelectRoom.SelectedItem.ToString();
            //txtRoomText.Foreground = new SolidColorBrush(Colors.Black);
            bodShowSelectListRoom.Visibility= System.Windows.Visibility.Collapsed;
        }
    }
}
