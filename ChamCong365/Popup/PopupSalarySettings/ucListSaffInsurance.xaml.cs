using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChamCong365.Popup.PopupSalarySettings
{
    /// <summary>
    /// Interaction logic for ucListSaffInsurance.xaml
    /// </summary>
    /// 
    public class InsuranceSaff
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string anh { get; set; }
        public string thoigianapdung { get; set; }
        public int tienbaohiem { get; set; }
        public string timeout { get; set; }

    }
    public partial class ucListSaffInsurance : System.Windows.Controls.UserControl
    {
        List<InsuranceSaff> itemIs = new List<InsuranceSaff>();

        public ucListSaffInsurance()
        {
            InitializeComponent();
            itemIs.Add(new InsuranceSaff() { Id = 1, name = "KyTo01", anh = "./Resource/image/KyTo.jpg", thoigianapdung = "Tháng Tám 2023",  timeout = "Chưa cập nhật", tienbaohiem = 10000000});
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
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodSaffs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
