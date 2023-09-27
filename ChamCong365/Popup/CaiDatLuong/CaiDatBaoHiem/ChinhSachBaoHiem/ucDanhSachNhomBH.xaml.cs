using ChamCong365.Popup.CaiDatLuong;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucListInsiranceGround.xaml
    /// </summary>
    public partial class ucDanhSachNhomBH : UserControl
    {
        List<InsuranceSaff> itemIs = new List<InsuranceSaff>();
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        public ucDanhSachNhomBH(MainWindow main)
        {
            InitializeComponent();
            itemIs.Add(new InsuranceSaff() {ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000 , thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            itemIs.Add(new InsuranceSaff() { ground = "Nhóm nhân viên Full-Time", tienbaohiem = 10000000, thoigianapdung = "Tháng Tám 2023", timeout = "Chưa cập nhật", });
            dgvListGroundInsurance.ItemsSource = itemIs;
            Main = main;
        }

        private void bodlistAddEdit_MouseEnter(object sender, MouseEventArgs e)
        {
            txbAddSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            txbEditInsurance.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodlistAddEdit_MouseLeave(object sender, MouseEventArgs e)
        {
            txbAddSaff.Foreground = (Brush)br.ConvertFrom("#474747");
            txbEditInsurance.Foreground = (Brush)br.ConvertFrom("#474747");
        }

        private void bodEditInsuranceGround_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucChinhSuaBaoHiemNhom());
        }

        private void bodDeleteInsuranceGround_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            grShowFormToListGround.Children.Add(new ucXoaNhomBaoHiem());
        }

        private void btnThemNhanVienVaoNhomBaoHiem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemNhanVienVaoNhomBH());
        }


        private void btnChinhSuaNhanVienTrongNhomBH_Click_1(object sender, System.Windows.RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucChinhSuaBaoHiemNhom());
        }
    }
}
