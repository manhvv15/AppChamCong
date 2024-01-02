using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using OfficeOpenXml;
using System.Windows.Forms; 
using UserControl = System.Windows.Controls.UserControl;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using System.Collections.Generic;
using System.IO;
using MessageBox = System.Windows.Forms.MessageBox;
using System;
using DataFormats = System.Windows.Forms.DataFormats;
using System.Data.SqlTypes;
//using QuanLyChung365TruocDangNhap.ChamCongNew.Properties;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for ucCreateFileSalary.xaml
    /// </summary>
    public partial class ucThemFileLuong : UserControl
    {
        public class FileLuong
        {
            public string FileName { get; set; }
        }
        private List<FileLuong> lstFileL = new List<FileLuong>();
        private List<string> DanhSachDuongDan = new List<string>();
        MainWindow Main;
        public ucThemFileLuong(MainWindow main)
        {
            InitializeComponent();
           Main = main;
        }

        private void bodExitCreateFileSalary_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed; 
        }

        private void bodAddFileSalarys_MouseUp(object sender, MouseButtonEventArgs e)
        {
         
            //string filePath = "";
            //OpenFileDialog dialog = new OpenFileDialog();
            //if (dialog.ShowDialog() == true)
            //{
            //    filePath = dialog.FileName;
            //}

            //if (string.IsNullOrEmpty(filePath))
            //{
            //    MessageBox.Show("Bạn cần chọn đúng file");
            //    return;
            //}
            //List<SalarySaff> salarySaffs = new List<SalarySaff>();
            //System.Data.DataTable dataTable = new System.Data.DataTable();
            //dataTable.Columns.Add("Id");
            //dataTable.Columns.Add("name");
            //dataTable.Columns.Add("luongcoban");
            //dataTable.Columns.Add("phongban");
            //dataTable.Columns.Add("chucvu");
            //dataTable.Columns.Add("lienhe");
            //try
            //{
            //    var pakage = new ExcelPackage(new FileInfo(filePath));
            //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //    ExcelWorksheet workSheet = pakage.Workbook.Worksheets[0];
            //    for (int i = workSheet.Dimension.Start.Row +2; i < workSheet.Dimension.End.Row; i++)
            //    {
            //        try
            //        {
            //            int j = 1;
            //            var Id = workSheet.Cells[i, j++].Value.ToString();
            //            string name = workSheet.Cells[i, j++].Value.ToString();
            //            var luongcoban = workSheet.Cells[i, j++].Value.ToString();
            //            string phonhban = workSheet.Cells[i, j++].Value.ToString();
            //            string chucvu = workSheet.Cells[i, j++].Value.ToString();
            //            string lienhe = workSheet.Cells[i, j++].Value.ToString();
            //            dataTable.Rows.Add(Id,name,luongcoban, phonhban, chucvu, lienhe);

            //        }
            //        catch (System.Exception)
            //        {

            //            MessageBox.Show("Lỗi chuyển dữ liệu");
            //        }
            //    }
            //}
            //catch (System.Exception)
            //{

            //    MessageBox.Show("Error import File");
            //}
            //ucCaiDatLuongCoBan ucb = new ucCaiDatLuongCoBan(Main);
            ////ucb.lsvListSalarySaff.ItemsSource =salarySaffs;
        }

        private void borAddFileLuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            string filePath = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "xlsx|*xlsx|csv|*csv";
            dialog.Multiselect = false;
            System.Windows.Forms.DialogResult dr = dialog.ShowDialog();
            if (dr==System.Windows.Forms.DialogResult.OK)
            {
                filePath = dialog.FileName;
                string[] NameFile = filePath.Split(Convert.ToChar("\\"));
                string NameFile1 = NameFile[NameFile.Length - 1];
                FileLuong fl = new FileLuong();
                fl.FileName = NameFile1;
                lsvFileLuong.Items.Clear();
                lsvFileLuong.Items.Add(fl);
                //lsvFileLuong.ItemsSource = lstFileL;
            }

            
            //List<SalarySaff> salarySaffs = new List<SalarySaff>();
            //System.Data.DataTable dataTable = new System.Data.DataTable();
            //dataTable.Columns.Add("Id");
            //dataTable.Columns.Add("name");
            //dataTable.Columns.Add("luongcoban");
            //dataTable.Columns.Add("phongban");
            //dataTable.Columns.Add("chucvu");
            //dataTable.Columns.Add("lienhe");
            //try
            //{
            //    var pakage = new ExcelPackage(new FileInfo(filePath));
            //    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            //    ExcelWorksheet workSheet = pakage.Workbook.Worksheets[0];
            //    for (int i = workSheet.Dimension.Start.Row + 2; i < workSheet.Dimension.End.Row; i++)
            //    {
            //        try
            //        {
            //            int j = 1;
            //            var Id = workSheet.Cells[i, j++].Value.ToString();
            //            string name = workSheet.Cells[i, j++].Value.ToString();
            //            var luongcoban = workSheet.Cells[i, j++].Value.ToString();
            //            string phonhban = workSheet.Cells[i, j++].Value.ToString();
            //            string chucvu = workSheet.Cells[i, j++].Value.ToString();
            //            string lienhe = workSheet.Cells[i, j++].Value.ToString();
            //            dataTable.Rows.Add(Id, name, luongcoban, phonhban, chucvu, lienhe);

            //        }
            //        catch (System.Exception)
            //        {

            //            MessageBox.Show("Lỗi chuyển dữ liệu");
            //        }
            //    }
            //}
            //catch (System.Exception)
            //{

            //    MessageBox.Show("Error import File");
            //}
            //ucCaiDatLuongCoBan ucb = new ucCaiDatLuongCoBan(Main);
            ////ucb.lsvListSalarySaff.ItemsSource =salarySaffs;
        }

        private void borAddFileLuong_Drop(object sender, System.Windows.DragEventArgs e)
        {
            FileLuong fl = new FileLuong();
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                //NhieuFile.Visibility = Visibility.Visible;
                //MotFile.Visibility = Visibility.Collapsed;
                // Note that you can have more than one file.
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string file in files)
                {
                    //DanhSachDuongDan.Add(file);

                    string[] ff = file.Split(Convert.ToChar("\\"));
                    if (ff[ff.Length - 1].Split('.')[ff[ff.Length - 1].Split('.').Length-1] == "xlsx" || ff[ff.Length - 1].Split('.')[ff[ff.Length - 1].Split('.').Length-1] == "xls" ||ff[ff.Length - 1].Split('.')[ff[ff.Length - 1].Split('.').Length-1] == "csv")
                        fl.FileName = ff[ff.Length - 1];
                    //lstFileL.Add(fl);
                }
                if (string.IsNullOrEmpty(fl.FileName))
                {
                    lsvFileLuong.Items.Clear();
                    lsvFileLuong.Items.Add(fl);
                }
                //lsvFileLuong.ItemsSource = lstFileL;
                //test = test.ToList();
            }
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            FileLuong file = (sender as Border).DataContext as FileLuong;
            if (file != null)
            {
                lsvFileLuong.Items.Remove(file);
            }
        }

        private void dop_ThemFileMau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            var resourceStream = System.Windows.Application.GetResourceStream(new Uri("/QuanLyChung365TruocDangNhap.ChamCongNew;component/Resource/FileMauLuongCoBan.xlsx", UriKind.Relative));

            if (resourceStream != null && resourceStream.Stream != null)
            {
                // Đọc dữ liệu từ tài nguyên thành một MemoryStream
                using (var memoryStream = new MemoryStream())
                {
                    resourceStream.Stream.CopyTo(memoryStream);

                    // Sử dụng SaveFileDialog để cho phép người dùng chọn nơi lưu file
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.FileName = "FileMauLuongCoBan.xlsx"; // Tên mặc định của file khi người dùng chọn nơi lưu
                    saveFileDialog.Filter = "Excel Files (*.xlsx)|*.xlsx";

                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        // Ghi dữ liệu từ MemoryStream vào file đã chọn
                        using (var fileStream = new FileStream(saveFileDialog.FileName, FileMode.Create))
                        {
                            memoryStream.WriteTo(fileStream);
                        }
                        MessageBox.Show("File mẫu đã được tải và lưu tại: " + saveFileDialog.FileName);
                    }
                }
            }
            else
            {
                MessageBox.Show("Không thể tải file mẫu.");
            }
            
        }

        private void bod_TaiLen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
