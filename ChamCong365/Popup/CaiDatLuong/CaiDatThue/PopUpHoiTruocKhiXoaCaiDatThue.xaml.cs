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
    /// Interaction logic for PopUpHoiTruocKhiXoaCaiDatThue.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaCaiDatThue : UserControl
    {
        private string IdThue = "";
        private string IdNV = "";
        private OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserFinal NhanVien = new OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserFinal();
        private PopUpDanhSachNVTrongThue PopUp;
        public PopUpHoiTruocKhiXoaCaiDatThue(MainWindow main, OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserFinal staff, string IdT, PopUpDanhSachNVTrongThue Pop)
        {
            InitializeComponent();
            IdThue = IdT;
            IdNV = staff.idQLC.ToString();
            PopUp = Pop;
            NhanVien = staff;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void btnAgree_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_nv_tax")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                request.AddParameter("cls_id_cl", IdThue);
                request.AddParameter("cls_id_user", IdNV);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                PopUp.lstStaff.Remove(NhanVien);
                PopUp.dgvStaffInTax.ItemsSource = null;
                PopUp.dgvStaffInTax.ItemsSource = PopUp.lstStaff;
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
