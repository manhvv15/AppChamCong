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
using ChamCong365.Popup;
using ChamCong365.Popup.funcCompanyManager;

namespace ChamCong365
{
    /// <summary>
    /// Class Phòng ban
    /// </summary>
    public class PhongBan
    {
        public int Id { get; set; }
        public string TenPhongBan { get; set; }
        public string TenTruongPhong { get; set; }
        public string TenPhoPhong { get; set; }
        public int soLuongNV { get; set; }
    }
    /// <summary>
    /// Interaction logic for ucListDpartment.xaml
    /// </summary>
    public partial class ucListDpartment : UserControl
    {
        MainWindow _main;
        
        public ucListDpartment(MainWindow main)
        {
            InitializeComponent();
            this._main = main;
            List<PhongBan> listPhongBan = new List<PhongBan>();
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 1", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 2", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 3", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 4", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 5", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 6", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 7", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 8", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 9", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            listPhongBan.Add(new PhongBan() { Id = 1, TenPhongBan = "Kỹ Thuật 10", TenTruongPhong = "Vũ Hà My", TenPhoPhong = "Tuấn", soLuongNV = 12 });
            lsvDSPhongBan.ItemsSource = listPhongBan;

            List<string> dsTenCongTy = new List<string>() { "Hà hung 2", "PT shop", "PT Shop 2", "PT Shop 3" };
            List<string> dsTenPhongBan = new List<string>() { "Kỹ thuật", "Biên tập", "Kinh doanh", "Đề án", "Phòng seo", "Phòng đào tạo", "Phòng sáng tạo" };

            lsvCompany.ItemsSource = dsTenCongTy;
            lsvDepartment.ItemsSource = dsTenPhongBan;
            
            
        }

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
            _main.grShowPopup.Children.Add(new ucCreateDepartment(this));
        }


        private void UpdateDepartment_MouseUp(object sender, MouseButtonEventArgs e)
        {
            _main.grShowPopup.Children.Add(new ucUpdateDepartment(this));

        }
    
    }
}
