
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;

namespace ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucListInsuranceSaff.xaml
    /// </summary>
    /// 
    public class InsuranceSaff
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string anh { get; set; }
        public string thoigianapdung { get; set; }
        public int tienbaohiem { get; set; }
        public int tienthue { get; set; }
        public string timeout { get; set; }
        public string ground { get; set; }


    }
    public partial class ucDanhSachBHNhanVien : UserControl
    {
        List<InsuranceSaff> itemIs = new List<InsuranceSaff>();
        MainWindow Main;
       
        public ucDanhSachBHNhanVien(MainWindow main)
        {
            InitializeComponent();
            itemIs.Add(new InsuranceSaff() { Id = 1, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 2, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 3, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 4, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 5, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 6, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 7, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 8, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 9, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 10, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 11, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            itemIs.Add(new InsuranceSaff() { Id = 12, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", tienbaohiem = 10000000 });
            dgvListSaffInsurance.ItemsSource = itemIs;
            Main = main;
        }

      

        private void bodDeletes_MouseUp(object sender, MouseButtonEventArgs e)
        {
            grShowFormToListIS.Children.Add(new ucXoaBaoHiemNhanVien());
        }

        private void bodEditInsuranceSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            grShowFormToListIS.Children.Add(new ucChinhSuaBHNhanVienTheoTien(Main));
        }

      
    }
}
