using ChamCong365.SalarySettings.DiMuonVeSom;
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

namespace ChamCong365.Popup.CaiDatLuong.NghiSaiQuyDinh
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaMucPhat.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaMucPhat : UserControl
    {
        private PopUpChiTietMucPhatNghiSaiQD frmChiTiet;
        private OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa phatNghiSaiQD;
        private frmNghiSaiQuyDinh frmNSQD;
        public PopUpHoiTruocKhiXoaMucPhat(MainWindow main, frmNghiSaiQuyDinh frm, OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa phat, PopUpChiTietMucPhatNghiSaiQD frmCT)
        {
            InitializeComponent();
            frmChiTiet = frmCT;
            phatNghiSaiQD = phat;
            frmNSQD = frm;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            frmChiTiet.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Collapsed;
        }

        private void btnHuy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            frmChiTiet.Visibility = Visibility.Visible;
            this.Visibility = Visibility.Collapsed;
        }

        private void btnOK_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_phat_ca")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                request.AddParameter("pc_id", phatNghiSaiQD.pc_id);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                frmNSQD.lstPC.Remove(phatNghiSaiQD);
                frmNSQD.dgv.ItemsSource = null;
                frmNSQD.dgv.ItemsSource = frmNSQD.lstPC;
                this.Visibility = Visibility.Collapsed;
            }
        }
    }
}
