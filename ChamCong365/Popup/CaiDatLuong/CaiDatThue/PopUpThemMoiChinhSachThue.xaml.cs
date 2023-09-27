using ChamCong365.OOP;
using ChamCong365.SalarySettings.CaiDatThue;
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

namespace ChamCong365.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpThemMoiChinhSachThue.xaml
    /// </summary>
    public partial class PopUpThemMoiChinhSachThue : UserControl
    {
        private MainWindow Main;
        private frmChinhSachThue frmCST;
        public string TenCT;
        public string CTTinh = "";
        private OOP.CaiDatLuong.Tax.clsTax.TaxListDetail clsThue = new OOP.CaiDatLuong.Tax.clsTax.TaxListDetail();
        private OOP.CaiDatLuong.Tax.clsTax.TaxListDetail ThueMoi = new OOP.CaiDatLuong.Tax.clsTax.TaxListDetail();
        public PopUpThemMoiChinhSachThue(MainWindow main, frmChinhSachThue frm)
        {
            InitializeComponent();
            Main = main;
            frmCST = frm;
            textTieuDe.Text = "Thêm mới chính sách thuế";
        }
        public PopUpThemMoiChinhSachThue(MainWindow main, frmChinhSachThue frm, OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cls)
        {
            InitializeComponent();
            Main = main;
            frmCST = frm;
            clsThue = cls;
            textTieuDe.Text = "Chỉnh sửa chính sách thuế";
            textTenThue.Text = cls.cl_name;
            textMieuTa.Text = cls.cl_note;
            foreach(var item in cls.TinhluongFormSalary)
            {
                TenCT = item.fs_name;
                CTTinh = item.fs_repica;
            }
        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnThemCongThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bor.Fill = Brushes.Transparent;
            Main.grShowPopup.Children.Add(new PopUpThietLapCongThuc(Main, this, TenCT, CTTinh));
        }

        private void btnLuu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(textTieuDe.Text=="Thêm mới chính sách thuế")
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_category_tax")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("fs_name", TenCT);
                    request.AddParameter("fs_data", 1);
                    request.AddParameter("fs_repica", CTTinh);
                    request.AddParameter("fs_note", TenCT);
                    request.AddParameter("cl_name", textTenThue.Text);
                    request.AddParameter("cl_note", textMieuTa.Text);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.Tax.clsAddTax.Root tax = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsAddTax.Root>(b);
                    if (tax.data != null)
                    {
                        ThueMoi = new OOP.CaiDatLuong.Tax.clsTax.TaxListDetail();
                        ThueMoi.cl_id = tax.data.bao_hiem_cong_ty.cl_id;
                        ThueMoi.cl_name = tax.data.bao_hiem_cong_ty.cl_name;
                        ThueMoi._id = tax.data.bao_hiem_cong_ty._id;
                        ThueMoi.cl_active = tax.data.bao_hiem_cong_ty.cl_active;
                        ThueMoi.cl_note = tax.data.bao_hiem_cong_ty.cl_note;
                        ThueMoi.cl_type = tax.data.bao_hiem_cong_ty.cl_type;
                        ThueMoi.cl_type_tax = tax.data.bao_hiem_cong_ty.cl_type_tax;
                        ThueMoi.cl_id_form = tax.data.bao_hiem_cong_ty.cl_id_form;
                        ThueMoi.cl_com = tax.data.bao_hiem_cong_ty.cl_com;
                        ThueMoi.cl_type = tax.data.bao_hiem_cong_ty.cl_type;
                        ThueMoi.TinhLuongf = tax.data.thong_tin_bao_hiem.fs_repica;
                        frmCST.lstCST.Insert(0, ThueMoi);
                        frmCST.lsvChinhSachThue.ItemsSource = null;
                        frmCST.lsvChinhSachThue.ItemsSource = frmCST.lstCST;
                        this.Visibility = Visibility.Collapsed;
                    }
                }
            }
            else
            {
                try
                {
                    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/update_tax_com")))
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("cl_name", textTenThue.Text);
                        request.AddParameter("cl_note", textMieuTa.Text);
                        request.AddParameter("cl_id", clsThue.cl_id);
                        request.AddParameter("fs_data", 1);
                        request.AddParameter("fs_id", 2);
                        request.AddParameter("fs_name", TenCT);
                        request.AddParameter("fs_repica", CTTinh);
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;
                        OOP.CaiDatLuong.Tax.clsTax.TaxListDetail tax = new OOP.CaiDatLuong.Tax.clsTax.TaxListDetail();
                        foreach(var item in frmCST.lstCST)
                        {
                            if(item.cl_id== clsThue.cl_id)
                            {
                                item.cl_name = textTenThue.Text;
                                item.cl_note = textMieuTa.Text;
                                item.TinhLuongf = CTTinh;
                                foreach (var item1 in item.TinhluongFormSalary)
                                {
                                    item1.fs_data = 1;
                                    item1.fs_id = 2;
                                    item1.fs_name = TenCT;
                                    item1.fs_repica = CTTinh;
                                }
                            }

                        }
                        frmCST.lsvChinhSachThue.ItemsSource = null;
                        frmCST.lsvChinhSachThue.ItemsSource = frmCST.lstCST;
                        this.Visibility = Visibility.Collapsed;
                    }
                }
                catch
                {

                }
            }
            //clsChinhSachThue cst = new clsChinhSachThue();
            //cst.TenCS = textTenThue.Text;
            //cst.MieuTa = textMieuTa.Text;
            //cst.CongThucTinh = CTTinh;
            ////frmCST.lstCST.Add(cst);
            //frmCST.lsvChinhSachThue.Items.Add(cst);
            //this.Visibility = Visibility.Collapsed;
        }
    }
}
