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

namespace ChamCong365
{
    /// <summary>
    /// Interaction logic for ucListNghiViec.xaml
    /// </summary>
    /// 
    public class NghiViec
    {
        public int IdNV { get; set; }
        public string HoTen { get; set; }
        public string PhongBan { get; set; }
        public string ChucVu { get; set; }
        public DateTime NgayBatDauNghi { get; set; }
    }
    public partial class ucListNghiViec : UserControl
    {
        MainWindow _main;

        public ucListNghiViec(MainWindow main)
        {
            InitializeComponent();
            this._main = main;
            List<NghiViec> listNghiViec = new List<NghiViec>();
            for (int i = 1; i <= 7; i++)
            {
                NghiViec nghiViec = new NghiViec
                {
                    IdNV = i,
                    HoTen = "Employee " + i,
                    PhongBan = "Department" + i,
                    ChucVu = "Position " + i,
                    NgayBatDauNghi = DateTime.Now.AddDays(i)

                };

                listNghiViec.Add(nghiViec);
            }
            lsvDSNghiViec.ItemsSource = listNghiViec;

            List<string> dsTenCongTy = new List<string>() { "Hà hung 2", "PT shop", "PT Shop 2", "PT Shop 3" };
            List<string> dsTenPhongBan = new List<string>() { "Kỹ thuật", "Biên tập", "Kinh doanh", "Đề án", "Phòng seo", "Phòng đào tạo", "Phòng sáng tạo" };

            lsvCompany.ItemsSource = dsTenCongTy;
            lsvDepartment.ItemsSource = dsTenPhongBan;

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


        //xử lý ấn nút thêm phòng ban
        private void bodAddDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }


        private void UpdateDepartment_MouseUp(object sender, MouseButtonEventArgs e)
        {


        }

    }
}
