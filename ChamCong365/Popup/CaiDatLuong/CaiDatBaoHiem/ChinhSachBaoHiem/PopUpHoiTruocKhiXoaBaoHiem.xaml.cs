using ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem;
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

namespace ChamCong365.Popup.CaiDatLuong.CaiDatBaoHiem.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaBaoHiem.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaBaoHiem : UserControl
    {
        private MainWindow Main;
        private OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList Baohiem;
        private ucLoaiBaoHiem ucLBH;
        public PopUpHoiTruocKhiXoaBaoHiem(MainWindow main, OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList Bh, ucLoaiBaoHiem uc)
        {
            InitializeComponent();
            Main = main;
            Baohiem = Bh;
            ucLBH = uc;
        }

        private void bodCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnDongY_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_nv_insrc")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;

                    request.AddParameter("cl_id", Baohiem.cl_id);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    ucLBH.lstBH.Remove(Baohiem);
                    ucLBH.lsvDSBaoHiem.ItemsSource = null;
                    ucLBH.lsvDSBaoHiem.ItemsSource = ucLBH.lstBH;
                    this.Visibility = Visibility.Collapsed;
                }

            }
            catch
            {

            }
        }
    }
}
