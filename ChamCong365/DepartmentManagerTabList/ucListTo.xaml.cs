using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
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
using ChamCong365.Popup.funcCompanyManager;
namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for ucListTo.xaml
    /// </summary>
    /// <summary>
    /// Class Tổ trong phòng ban
    /// </summary>
    public class To
    {
        public int Id { get; set; }
        public string TenTo { get; set; }
        public string ToTruong { get; set; }
        public string PhoToTruong { get; set; }
        public string PhongBan { get; set; }
        public int soLuongNV { get; set; }
    }
    public partial class ucListTo : UserControl
    {
        MainWindow _main;
        
        public ucListTo(MainWindow main)
        {
            InitializeComponent();
            this._main = main;

            List<To> listTo = new List<To>();
            listTo.Add(new To() { Id = 1, TenTo = "Tổ 1", ToTruong = "Vũ Hà My", PhoToTruong = "Tuấn", soLuongNV = 12, PhongBan = "Kĩ thuật" });
            listTo.Add(new To() { Id = 1, TenTo = "Tổ 2", ToTruong = "Vũ Hà My", PhoToTruong = "Tuấn", soLuongNV = 12 });
            listTo.Add(new To() { Id = 1, TenTo = "Tổ 3", ToTruong = "Vũ Hà My", PhoToTruong = "Tuấn", soLuongNV = 12 });
            listTo.Add(new To() { Id = 1, TenTo = "Tổ 4", ToTruong = "Vũ Hà My", PhoToTruong = "Tuấn", soLuongNV = 12 });
            listTo.Add(new To() { Id = 1, TenTo = "Tổ 5", ToTruong = "Vũ Hà My", PhoToTruong = "Tuấn", soLuongNV = 12 });
            listTo.Add(new To() { Id = 1, TenTo = "Tổ 6", ToTruong = "Vũ Hà My", PhoToTruong = "Tuấn", soLuongNV = 12 });
            listTo.Add(new To() { Id = 1, TenTo = "Tổ 8", ToTruong = "Vũ Hà My", PhoToTruong = "Tuấn", soLuongNV = 12 });
            listTo.Add(new To() { Id = 1, TenTo = "Tổ 9", ToTruong = "Vũ Hà My", PhoToTruong = "Tuấn", soLuongNV = 12 });
            listTo.Add(new To() { Id = 1, TenTo = "Tổ 10", ToTruong = "Vũ Hà My", PhoToTruong = "Tuấn", soLuongNV = 12 });

            lsvDSTo.ItemsSource = listTo;
            List<string> dsTenCongTy = new List<string>() { "Hà hung 2", "PT shop", "PT Shop 2", "PT Shop 3" };
            List<string> dsTenPhongBan = new List<string>() { "Kỹ thuật", "Biên tập", "Kinh doanh", "Đề án", "Phòng seo", "Phòng đào tạo", "Phòng sáng tạo" };
            List<string> dsTenTo = new List<string>() { "Tổ 1", "Tổ 2", "Tổ 3", "Tổ 4" };

            lsvCompany.ItemsSource = dsTenCongTy;
            lsvDepartment.ItemsSource = dsTenPhongBan;
            lsvTo.ItemsSource = dsTenTo;



        }

        //dropdown box select company
        private void bodSelectCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCompany.Visibility == Visibility.Collapsed)
            {
                bodListCompany.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCompany.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectCompany.Text = lsvCompany.SelectedItem.ToString();
            bodListCompany.Visibility = Visibility.Collapsed;
        }

        //dropdown box select department

        private void bodSelectDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListDepartment.Visibility == Visibility.Collapsed)
            {
                bodListDepartment.Visibility = Visibility.Visible;
            }
            else
            {
                bodListDepartment.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectDepartment.Text = lsvDepartment.SelectedItem.ToString();
            bodListDepartment.Visibility = Visibility.Collapsed;
        }

        //dropdown box select to
        private void bodSelectTo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListTo.Visibility == Visibility.Collapsed)
            {
                bodListTo.Visibility = Visibility.Visible;
            }
            else
            {
                bodListTo.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectTo.Text = lsvTo.SelectedItem.ToString();
            bodListTo.Visibility = Visibility.Collapsed;
        }

        //xử lý ấn nút thêm phòng ban
        private void bodAddDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }


        private void UpdateDepartment_MouseUp(object sender, MouseButtonEventArgs e)
        {


        }
      
    }
}
