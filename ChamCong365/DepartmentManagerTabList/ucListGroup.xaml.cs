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
    /// Interaction logic for ucListGroup.xaml
    /// </summary>
    public class Nhom
    {
        public int Id { get; set; }
        public string TenNhom { get; set; }
        public string NhomTruong { get; set; }
        public string NhomPho { get; set; }
        public string To { get; set; }
        public string PhongBan { get; set; }
        public string SoLuongNV { get; set; }
    }
    public partial class ucListGroup : UserControl
    {
        MainWindow _main;

        public ucListGroup(MainWindow main)
        {
            InitializeComponent();
            this._main = main;
            List<Nhom> listNhom = new List<Nhom>();
            listNhom.Add(new Nhom() { Id = 1, TenNhom = "Nhom1" });
            listNhom.Add(new Nhom() { Id = 1, TenNhom = "Nhom1" });
            listNhom.Add(new Nhom() { Id = 1, TenNhom = "Nhom1" });
            listNhom.Add(new Nhom() { Id = 1, TenNhom = "Nhom1" });
            listNhom.Add(new Nhom() { Id = 1, TenNhom = "Nhom1" });
            listNhom.Add(new Nhom() { Id = 1, TenNhom = "Nhom1" });
            listNhom.Add(new Nhom() { Id = 1, TenNhom = "Nhom1" });
            lsvDSNhom.ItemsSource = listNhom;

            List<string> dsTenCongTy = new List<string>() { "Hà hung 2", "PT shop", "PT Shop 2", "PT Shop 3" };
            List<string> dsTenPhongBan = new List<string>() { "Kỹ thuật", "Biên tập", "Kinh doanh", "Đề án", "Phòng seo", "Phòng đào tạo", "Phòng sáng tạo" };
            List<string> dsTenTo = new List<string>() { "Tổ 1", "Tổ 2", "Tổ 3", "Tổ 4" };
            List<string> dsTenNhom = new List<string>() { "Nhóm A", "Nhóm 1", "Nhóm 2", "Nhóm 3" };

            lsvCompany.ItemsSource = dsTenCongTy;
            lsvDepartment.ItemsSource = dsTenPhongBan;
            lsvTo.ItemsSource = dsTenTo;
            lsvGroup.ItemsSource = dsTenNhom;
            
           



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

        //dropdown box select tổ
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

        //dropdown select group
        private void bodSelectGroup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListGroup.Visibility == Visibility.Collapsed)
            {
                bodListGroup.Visibility = Visibility.Visible;
            }
            else
            {
                bodListGroup.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectGroup.Text = lsvGroup.SelectedItem.ToString();
            bodListGroup.Visibility = Visibility.Collapsed;
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
