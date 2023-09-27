
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucInputInfoNewInsurance.xaml
    /// </summary>
    /// 

    public partial class ucThemMoiChinhSachBH : UserControl
    {
        
        //private List<CongThuc> lstCT = new List<CongThuc>();
        MainWindow Main;
        //List<Insurance> Insurances = new List<Insurance>();
        private OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList BaoHiem = new OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList();
        public string TenCT;
        public string ChiTietCT;
        private ucLoaiBaoHiem ucLBH;
        public ucThemMoiChinhSachBH(MainWindow main,string TDe, ucLoaiBaoHiem uc)
        {
            InitializeComponent();
            bodExitNextSaff.Text = TDe;
            Main = main;
            ucLBH = uc;
        }
        public ucThemMoiChinhSachBH(MainWindow main, OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList Bh, string TDe, ucLoaiBaoHiem uc)
        {
            InitializeComponent();
            bodExitNextSaff.Text = TDe;
            BaoHiem = Bh;
            textTenBH.Text = Bh.cl_name;
            textMieuTa.Text = Bh.cl_note;
            Main = main;
            ucLBH = uc;
        }
        
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ExitNextSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void dopNameRecipe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bor.Fill = Brushes.Transparent;
            if (bodExitNextSaff.Text == "Thêm mới chính sách bảo hiểm")
            {
                Main.grShowPopup.Children.Add(new ucNhapMoiChinhSachBH(Main, this));
            }
            else
            {
                Main.grShowPopup.Children.Add(new ucNhapMoiChinhSachBH(Main, this, BaoHiem));

            }
        }

        private void bodSaveNewInsurance_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodExitNextSaff.Text == "Thêm mới chính sách bảo hiểm")
            {
                try
                {
                    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_category_insrc")))
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("com_id", Main.IdAcount);
                        request.AddParameter("fs_name", textTenBH.Text);
                        request.AddParameter("fs_data", 1);
                        request.AddParameter("fs_repica", ChiTietCT);
                        request.AddParameter("fs_note", textMieuTa.Text);
                        request.AddParameter("cl_name", textTenBH.Text);
                        request.AddParameter("cl_note", textMieuTa.Text);
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;
                        OOP.CaiDatLuong.BaoHiem.clsAddBH.Root add = JsonConvert.DeserializeObject<OOP.CaiDatLuong.BaoHiem.clsAddBH.Root>(b);
                        if (add.data != null)
                        {
                            OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList tax = new OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList();
                            tax.cl_id = add.data.thong_tin_bao_hiem.fs_id;
                            tax.cl_com = add.data.thong_tin_bao_hiem.fs_id_com;
                            tax.cl_type = add.data.thong_tin_bao_hiem.fs_type;
                            tax.cl_name = add.data.thong_tin_bao_hiem.fs_name;
                            tax.cl_note = add.data.thong_tin_bao_hiem.fs_note;
                            tax.calculation_formula = ChiTietCT;
                            ucLBH.lstBH.Add(tax);
                            ucLBH.lsvDSBaoHiem.ItemsSource = null;
                            ucLBH.lsvDSBaoHiem.ItemsSource = ucLBH.lstBH;
                            this.Visibility = Visibility.Collapsed;
                        }
                    }

                }
                catch
                {

                }
            }
            else
            {
                try
                {
                    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/update_insrc")))
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("cl_name", textTenBH.Text);
                        request.AddParameter("cl_note", textMieuTa.Text);
                        request.AddParameter("cl_id", BaoHiem.cl_id);
                        request.AddParameter("fs_data", 1);
                        request.AddParameter("fs_id", BaoHiem.cl_id_form);
                        request.AddParameter("fs_name", TenCT);
                        request.AddParameter("fs_repica", ChiTietCT);
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;
                        foreach(var item in ucLBH.lstBH)
                        {
                            if(item.cl_id== BaoHiem.cl_id)
                            {
                                item.cl_name = textTenBH.Text;
                                item.cl_note = textMieuTa.Text;
                                item.calculation_formula = ChiTietCT;
                            }
                        }
                        ucLBH.lsvDSBaoHiem.ItemsSource = null;
                        ucLBH.lsvDSBaoHiem.ItemsSource = ucLBH.lstBH;
                        this.Visibility = Visibility.Collapsed;
                    }

                }
                catch
                {

                }
            }
            //ucAddNewInsurance uca = new ucAddNewInsurance();
            //ucInsurancePolicy ucp = new ucInsurancePolicy(Main);
            //uca.txbInputNameInsurance.Text = txbNamePolicyInsurance.Text;
            //uca.txbInputDescribeInsurance.Text = txbInputMoney.Text;
            //object Content = uca.Content;
            //uca.Content = null;
            //ucp.lsvloadInsurancePolicy.Children.Add(Content as UIElement);
            //Insurance loadI = new Insurance();
           
            //loadI.NameInsurance = txbNamePolicyInsurance.Text.ToString();
            //loadI.DescInsurance = txbInputMoney.Text.ToString();
            //loadI.RecipeInsurance = txbSetupRecipe.Text.ToString();
            //ucLoaiBaoHiem ucp = new ucLoaiBaoHiem(Main);
            //Insurances.Add(loadI);
            //ucp.lsvloadInsurancePolicy.ItemsSource = Insurances;




        }
    }
}
