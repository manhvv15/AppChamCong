using ChamCong365.Popup.CaiDatLuong.PhuCap;
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

namespace ChamCong365.SalarySettings
{
    /// <summary>
    /// Interaction logic for frmDanhSachPhuCap.xaml
    /// </summary>
    public partial class frmDanhSachPhuCap : Page
    {
        public class PhuCap
        {
            public string TenPhuCap { get; set; }
            public string MucPhuCap { get; set; }
            public string LoaiThuNhap { get; set; }
            public string ApDungTuThang { get; set; }
            public string DenThang { get; set; }
        }
        public class PhuCapTheoCa
        {
            public string TenCa { get; set; }
            public string TienPhuCap { get; set; }
            public string ApDungTuThang { get; set; }
            public string DenThang { get; set; }
        }
        private MainWindow Main;
        private List<PhuCap> lstPhuCap = new List<PhuCap>();
        private List<PhuCapTheoCa> lstPhuCapTheoCa = new List<PhuCapTheoCa>();
        public frmDanhSachPhuCap(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            LoadPhuCap();
            LoadPhuCapTheoCa();
        }

        private void LoadPhuCapTheoCa()
        {
            lstPhuCapTheoCa.Add(new PhuCapTheoCa { TenCa = "Ca sáng 7TR < LƯƠNG <= 10TR", TienPhuCap = "50.000", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCapTheoCa.Add(new PhuCapTheoCa { TenCa = "Ca sáng 7TR < LƯƠNG <= 10TR", TienPhuCap = "50.000", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCapTheoCa.Add(new PhuCapTheoCa { TenCa = "Ca sáng 7TR < LƯƠNG <= 10TR", TienPhuCap = "50.000", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCapTheoCa.Add(new PhuCapTheoCa { TenCa = "Ca sáng 7TR < LƯƠNG <= 10TR", TienPhuCap = "50.000", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCapTheoCa.Add(new PhuCapTheoCa { TenCa = "Ca sáng 7TR < LƯƠNG <= 10TR", TienPhuCap = "50.000", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCapTheoCa.Add(new PhuCapTheoCa { TenCa = "Ca sáng 7TR < LƯƠNG <= 10TR", TienPhuCap = "50.000", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCapTheoCa.Add(new PhuCapTheoCa { TenCa = "Ca sáng 7TR < LƯƠNG <= 10TR", TienPhuCap = "50.000", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            dgvTheoCa.ItemsSource = lstPhuCapTheoCa;

        }

        private void LoadPhuCap()
        {
            lstPhuCap.Add(new PhuCap { TenPhuCap = "Phụ cấp 1", MucPhuCap = "100.000", LoaiThuNhap = "Thu nhập miễn thuế", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCap.Add(new PhuCap { TenPhuCap = "Phụ cấp 1", MucPhuCap = "100.000", LoaiThuNhap = "Thu nhập miễn thuế", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCap.Add(new PhuCap { TenPhuCap = "Phụ cấp 1", MucPhuCap = "100.000", LoaiThuNhap = "Thu nhập miễn thuế", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCap.Add(new PhuCap { TenPhuCap = "Phụ cấp 1", MucPhuCap = "100.000", LoaiThuNhap = "Thu nhập miễn thuế", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCap.Add(new PhuCap { TenPhuCap = "Phụ cấp 1", MucPhuCap = "100.000", LoaiThuNhap = "Thu nhập miễn thuế", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            lstPhuCap.Add(new PhuCap { TenPhuCap = "Phụ cấp 1", MucPhuCap = "100.000", LoaiThuNhap = "Thu nhập miễn thuế", ApDungTuThang = "6/2023", DenThang = "7/2023" });
            dgv.ItemsSource = lstPhuCap;

        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void dgvTheoCa_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnTuyChinh_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaPhuCapPCTheoCa(Main, "Bạn có chắc chắn muốn xoá phụ cấp này?"));
        }

        private void btnXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaPhuCapPCTheoCa(Main, "Bạn có chắc chắn muốn xoá phụ cấp theo ca này?"));
        }
    }
}
