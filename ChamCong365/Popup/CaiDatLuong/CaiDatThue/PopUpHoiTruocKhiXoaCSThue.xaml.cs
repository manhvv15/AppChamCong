using ChamCong365.OOP;
using ChamCong365.SalarySettings.CaiDatThue;
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

namespace ChamCong365.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaCSThue.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaCSThue : UserControl
    {
        private OOP.CaiDatLuong.Tax.clsTax.TaxListDetail clsTax = new OOP.CaiDatLuong.Tax.clsTax.TaxListDetail();
        private frmChinhSachThue frmCTT;
        public PopUpHoiTruocKhiXoaCSThue(MainWindow main, frmChinhSachThue frm, OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cls)
        {
            InitializeComponent();
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

        private void btnDongY_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

            }
        }
    }
}
