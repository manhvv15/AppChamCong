using System.Windows;
using System.Windows.Input;
using OfficeOpenXml;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using MessageBox = System.Windows.Forms.MessageBox;
using UserControl = System.Windows.Controls.UserControl;
using ChamCong365.SalarySettings;
using System.Collections.Generic;
using System.IO;

namespace ChamCong365.Popup.PopupSalarySettings
{
    /// <summary>
    /// Interaction logic for ucCreateFileSalary.xaml
    /// </summary>
    public partial class ucCreateFileSalary : UserControl
    {
        
        MainWindow Main;
        public ucCreateFileSalary(MainWindow main)
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
         
            string filePath = "";
            OpenFileDialog dialog = new OpenFileDialog();
            if (dialog.ShowDialog() == true)
            {
                filePath = dialog.FileName;
            }

            if (string.IsNullOrEmpty(filePath))
            {
                MessageBox.Show("Bạn cần chọn đúng file");
                return;
            }
            List<SalarySaff> salarySaffs = new List<SalarySaff>();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable.Columns.Add("Id");
            dataTable.Columns.Add("name");
            dataTable.Columns.Add("luongcoban");
            dataTable.Columns.Add("phongban");
            dataTable.Columns.Add("chucvu");
            dataTable.Columns.Add("lienhe");
            try
            {
                var pakage = new ExcelPackage(new FileInfo(filePath));
                ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
                ExcelWorksheet workSheet = pakage.Workbook.Worksheets[0];
                for (int i = workSheet.Dimension.Start.Row +2; i < workSheet.Dimension.End.Row; i++)
                {
                    try
                    {
                        int j = 1;
                        var Id = workSheet.Cells[i, j++].Value.ToString();
                        string name = workSheet.Cells[i, j++].Value.ToString();
                        var luongcoban = workSheet.Cells[i, j++].Value.ToString();
                        string phonhban = workSheet.Cells[i, j++].Value.ToString();
                        string chucvu = workSheet.Cells[i, j++].Value.ToString();
                        string lienhe = workSheet.Cells[i, j++].Value.ToString();
                        dataTable.Rows.Add(Id,name,luongcoban, phonhban, chucvu, lienhe);

                    }
                    catch (System.Exception)
                    {

                        MessageBox.Show("Lỗi chuyển dữ liệu");
                    }
                }
            }
            catch (System.Exception)
            {

                MessageBox.Show("Error import File");
            }
            ucBasicSalary ucb = new ucBasicSalary(Main);
            //ucb.lsvListSalarySaff.ItemsSource =salarySaffs;
        }
    }
}
