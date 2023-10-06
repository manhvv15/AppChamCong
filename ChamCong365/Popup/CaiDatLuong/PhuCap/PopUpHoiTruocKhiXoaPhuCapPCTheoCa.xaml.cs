using ChamCong365.SalarySettings;
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

namespace ChamCong365.Popup.CaiDatLuong.PhuCap
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaPhuCapPCTheoCa.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaPhuCapPCTheoCa : UserControl
    {
        private MainWindow Main;
        private frmDanhSachPhuCap frmDSPC;
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa PhuCap = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa();
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift Pctc = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift();

        public PopUpHoiTruocKhiXoaPhuCapPCTheoCa(MainWindow main, string NoiDung,frmDanhSachPhuCap dspc, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa pc)
        {
            InitializeComponent();
            textNoiDung.Text = NoiDung;
            Main = main;
            frmDSPC = dspc;
            PhuCap = pc;
        }
        public PopUpHoiTruocKhiXoaPhuCapPCTheoCa(MainWindow main, string NoiDung, frmDanhSachPhuCap dspc, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift pcs)
        {
            InitializeComponent();
            textNoiDung.Text = NoiDung;
            Main = main;
            frmDSPC = dspc;
            Pctc = pcs;


        }
        public PopUpHoiTruocKhiXoaPhuCapPCTheoCa(MainWindow main, string NoiDung)
        {
            InitializeComponent();
            textNoiDung.Text = NoiDung;
        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnDongY_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(textNoiDung.Text== "Bạn có chắc chắn muốn xoá phụ cấp này?")
            {
                try
                {
                    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_phuc_loi")))
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("cl_id", PhuCap.cl_id);
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;
                        frmDSPC.lstPC.Remove(PhuCap);
                        frmDSPC.dgv.ItemsSource = null;
                        frmDSPC.dgv.ItemsSource = frmDSPC.lstPC;
                        this.Visibility = Visibility.Collapsed;
                    }
                }
                catch
                {

                }
            }
            else if(textNoiDung.Text == "Bạn có chắc chắn muốn xoá phụ cấp theo ca này?")
            {
                try
                {
                    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_wf_shift")))
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("wf_id", Pctc.wf_id);
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;
                        frmDSPC.lstPCTC.Remove(Pctc);
                        frmDSPC.dgvTheoCa.ItemsSource = null;
                        frmDSPC.dgvTheoCa.ItemsSource = frmDSPC.lstPCTC;
                        this.Visibility = Visibility.Collapsed;
                    }
                }
                catch
                {

                }
            }
        }
    }
}
