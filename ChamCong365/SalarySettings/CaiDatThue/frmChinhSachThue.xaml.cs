using ChamCong365.OOP;
using ChamCong365.Popup.CaiDatLuong.CaiDatThue;
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

namespace ChamCong365.SalarySettings.CaiDatThue
{
    /// <summary>
    /// Interaction logic for frmChinhSachThue.xaml
    /// </summary>
    public partial class frmChinhSachThue : Page
    {
        private MainWindow Main;
        
        public List<OOP.CaiDatLuong.Tax.clsTax.TaxListDetail> lstCST = new List<OOP.CaiDatLuong.Tax.clsTax.TaxListDetail>();
        public frmChinhSachThue(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDLChinhSachThue();
        }

        private void LoadDLChinhSachThue()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_tax_com")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.Tax.clsTax.Root tax = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsTax.Root>(b);
                    if (tax.tax_list_detail != null)
                    {
                        foreach (var item in tax.tax_list_detail)
                        {
                            foreach (var re in item.TinhluongFormSalary)
                            {
                                item.TinhLuongf = item.TinhLuongf + " " + re.fs_name;
                            }
                            lstCST.Add(item);
                        }
                        lsvChinhSachThue.ItemsSource = lstCST;
                    }
                }
            }
            catch
            {

            }
        }

        private void btnThemChinhSachThue_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new PopUpThemMoiChinhSachThue(Main, this));
        }

        private void lsvChinhSachThue_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
            if (cst != null)
            {
                Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaCSThue(Main, this, cst));

            }
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
            if (cst != null)
            {
                Main.grShowPopup.Children.Add(new PopUpThemMoiChinhSachThue(Main, this, cst));

            }
        }


        private void btnDanhSachNVLuyTien_Click(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpDanhSachNVTrongThue(Main, "0","Thuế theo luỹ tiền","1970-01-01", "1970-01-01"));

        }

        private void btnDanhSachNVHSQuyDinh_Click(object sender, RoutedEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpDanhSachNVTrongThue(Main, "1", "Thuế theo hệ số mặc định", "1970-01-01", "1970-01-01"));

        }

        private void btnDanhSachNVLst_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
            if (cst != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpDanhSachNVTrongThue(Main, cst.cl_id.ToString(), cst.cl_name, cst.cl_day.ToString(), cst.cl_day_end.ToString()));


            }
        }

        private void btnThemNVLst_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
            if (cst != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpAddStaffInTax(Main, cst.cl_id));


            }
        }
    }
}
