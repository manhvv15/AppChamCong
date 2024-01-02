using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.CaiDatThue;
//using DocumentFormat.OpenXml.Spreadsheet;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaCSThue.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaCSThue : UserControl
    {
        private OOP.CaiDatLuong.Tax.clsTax.TaxListDetail clsTax = new OOP.CaiDatLuong.Tax.clsTax.TaxListDetail();
        private frmChinhSachThue frmCTT;
        MainWindow  Main;
        public PopUpHoiTruocKhiXoaCSThue(MainWindow main, frmChinhSachThue frm, OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cls)
        {
            InitializeComponent();
            Main = main;
            clsTax = cls;
            frmCTT = frm;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void btnHuy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        string ErrorSytem;
        private void btnDongY_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_tax_com")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("cl_id", clsTax.cl_id);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    frmCTT.lstCST.Remove(clsTax);
                    frmCTT.lsvChinhSachThue.ItemsSource = null;
                    frmCTT.lsvChinhSachThue.ItemsSource = frmCTT.lstCST;
                    this.Visibility = Visibility.Collapsed;
                    frmCTT.LoadDLChinhSachThue1();
                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main));
                }
            }
            catch (Exception)
            {
                ErrorSytem = "Error";
                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
            }
        }
    }
}
