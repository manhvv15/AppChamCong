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

namespace ChamCong365.Popup.CaiDatLuong.ThuongPhat
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaThuongPhat.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaThuongPhat : UserControl
    {
        MainWindow Main;
        OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat ItemP = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat();
        private PopUpThemMoiThuongPhat popUp;
        public PopUpHoiTruocKhiXoaThuongPhat(MainWindow main, OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat phat, string TieuDe, PopUpThemMoiThuongPhat pop)
        {
            InitializeComponent();
            Main = main;
            ItemP = phat;
            textNoiDung.Text = TieuDe;
            popUp = pop;
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
            if(textNoiDung.Text== "Bạn có chắc chăn muốn xoá mức phạt này không?")
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_thuong_phat")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("pay_id", ItemP.pay_id);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    popUp.thuongP.tt_phat.ds_phat.Remove(ItemP);
                    popUp.dgvPhat.ItemsSource = null;
                    popUp.dgvPhat.ItemsSource = popUp.thuongP.tt_phat.ds_phat;
                    this.Visibility = Visibility.Collapsed;
                }
            }
        }
    }
}
