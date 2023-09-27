using ChamCong365.Popup.CaiDatLuong.CaiDatPhucLoi;
using ChamCong365.Popup.CaiDatLuong.PhuCap;
using Newtonsoft.Json;
using RestSharp;
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
        public List<OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa> lstPC = new List<OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa>();
        public List<OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift> lstPCTC = new List<OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift>();


        public frmDanhSachPhuCap(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            LoadPhuCap();
            
        }

        
        private void LoadPhuCap()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_phuc_loi")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("companyId", Main.IdAcount);

                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.Root PhucLoi = JsonConvert.DeserializeObject<OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.Root>(b);
                    if (PhucLoi.data.list_welfa != null)
                    {
                        foreach (var item in PhucLoi.data.list_welfa)
                        {
                            if (item.cl_type_tax == 0)
                            {
                                item.cl_type_tax_s = "Thu nhập chịu thuế";
                            }
                            else if(item.cl_type_tax == 1)
                            {
                                item.cl_type_tax_s = "Thu nhập miễn thuế";
                            }
                            lstPC.Add(item);
                            //if (item.cl_type_tax == 0)
                            //{
                            //    item.cl_type_tax_s = "Thu nhập chịu thuế";
                            //}
                            //else if (item.cl_type_tax == 1)
                            //{
                            //    item.cl_type_tax_s = "Thu nhập miễn thuế";
                            //}
                            //lstPhucLoi.Add(item);
                        }
                        dgv.ItemsSource = lstPC;
                    }
                    if(PhucLoi.data.wf_shift != null)
                    {
                        foreach (var item in PhucLoi.data.wf_shift)
                        {
                            
                            lstPCTC.Add(item);
                            
                        }
                        dgvTheoCa.ItemsSource = lstPCTC;
                    }
                }
            }
            catch
            {

            }

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
            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa pc = (sender as MenuItem).DataContext as OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa;
            if (pc != null)
            {
                Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaPhuCapPCTheoCa(Main, "Bạn có chắc chắn muốn xoá phụ cấp này?", this, pc));

            }
        }

        private void btnXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift pcs = (sender as Border).DataContext as OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift;
            if (pcs != null)
            {
                Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaPhuCapPCTheoCa(Main, "Bạn có chắc chắn muốn xoá phụ cấp theo ca này?", this, pcs));

            }

        }

        private void btnThemPhuCapKhac_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new PopUpThemMoiChinhSuaPhuCap(Main, "Thêm mới phụ cấp", this));
        }

        private void btnChinhSua_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa pc = (sender as MenuItem).DataContext as OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa;
            if (pc != null)
            {
                Main.grShowPopup.Children.Add(new PopUpThemMoiChinhSuaPhuCap(Main, "Chỉnh sửa phụ cấp", this, pc));

            }
        }

        private void btnThemPhuCapTheoCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new PopUpThemMoiChinhSuaPhuCapTheoCa(Main, "Thêm mới phụ cấp theo ca", this));

        }

        private void btnChinhSuaPCTheoCa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new PopUpThemMoiChinhSuaPhuCapTheoCa(Main, "Chỉnh sửa phụ cấp theo ca", this));

        }

        private void btnDanhSachNV_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa pc = (sender as MenuItem).DataContext as OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa;
            if (pc != null)
            {
                Main.grShowPopup.Children.Add(new ucDanhSachNVHuongPhucLoi(Main, this, pc));

            }
        }

        private void btnThemNV_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa pc = (sender as MenuItem).DataContext as OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa;
            if (pc != null)
            {
                Main.grShowPopup.Children.Add(new ucThemNVHuongPhucLoi(Main, this, pc));

            }
        }
    }
}
