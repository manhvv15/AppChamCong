using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue;
//using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.CaiDatThue
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
            LoadDLChinhSachThue1();
        }
        public async void LoadDLChinhSachThue1()
        {
            try
            {
                lstCST.Clear();
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_tax_com");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resContent = await response.Content.ReadAsStringAsync();
                OOP.CaiDatLuong.Tax.clsTax.Root tax = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsTax.Root>(resContent);
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
                    lstCST.Insert(0, new OOP.CaiDatLuong.Tax.clsTax.TaxListDetail() { cl_id = 2 });
                    lstCST.Insert(0, new OOP.CaiDatLuong.Tax.clsTax.TaxListDetail() { cl_id = 1 });
                    lsvChinhSachThue.ItemsSource = lstCST;
                }
            }
            catch (Exception)
            {
            }
        }

        public void LoadDLChinhSachThue()
        {
            try
            {
                using (WebClient request = new WebClient())
                {
                    loading.Visibility = Visibility.Visible;
                    lstCST.Clear();
                    request.QueryString.Add("com_id", Main.IdAcount.ToString());
                    request.QueryString.Add("token", Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        try
                        {
                            OOP.CaiDatLuong.Tax.clsTax.Root tax = JsonConvert.DeserializeObject<OOP.CaiDatLuong.Tax.clsTax.Root>(UnicodeEncoding.UTF8.GetString(e.Result));
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
                                if (lsvChinhSachThue.Items.Count > 0)
                                {
                                    // Get the last item
                                    var lastItem = lsvChinhSachThue.Items[0];

                                    // Apply the custom template to the last item
                                    var lastItemContainer = lsvChinhSachThue.ItemContainerGenerator.ContainerFromItem(lastItem) as ListViewItem;
                                    if (lastItemContainer != null)
                                    {
                                        lastItemContainer.ContentTemplate = lsvChinhSachThue.Resources["LastItemTemplate"] as DataTemplate;
                                    }
                                }
                                else
                                {
                                    // Get the last item
                                    var lastItem = lsvChinhSachThue.Items[0];

                                    // Apply the custom template to the last item
                                    var lastItemContainer = lsvChinhSachThue.ItemContainerGenerator.ContainerFromItem(lastItem) as ListViewItem;
                                    if (lastItemContainer != null)
                                    {
                                        lastItemContainer.ContentTemplate = lsvChinhSachThue.Resources["LastItemTemplate"] as DataTemplate;
                                    }
                                }
                            }
                            loading.Visibility = Visibility.Collapsed;
                        }
                        catch { }
                    };
                    request.UploadValuesTaskAsync("http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_tax_com",
                        request.QueryString);
                }
            }
            catch (Exception)
            {
            }
        }
        private void btnThemChinhSachThue_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Main.grShowPopup.Children.Add(new PopUpThemMoiChinhSachThue(Main, this));
            }
            catch (Exception)
            {
            }
        }

        private void lsvChinhSachThue_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnXoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
                if (cst != null)
                {
                    Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaCSThue(Main, this, cst));
                }
            }
            catch (Exception)
            {}
        }

        private void btnSua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
                if (cst != null)
                {
                    Main.grShowPopup.Children.Add(new PopUpThemMoiChinhSachThue(Main, this, cst));
                }
            }
            catch (Exception)
            {
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
            try
            {
                OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
                if (cst != null)
                {
                    //Main.LoadDLDanhSachNVThuocCongTy();
                    Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpAddStaffInTax(Main, cst.cl_id));
                }
            }
            catch (Exception)
            {}
        }

        private void dopAddSaff_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
            if (cst != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpAddStaffInTax(Main, cst.cl_id));


            }
        }

        private void dopListSaffSmall_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
            if (cst != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpDanhSachNVTrongThue(Main, cst.cl_id.ToString(), cst.cl_name, cst.cl_day.ToString(), cst.cl_day_end.ToString()));
            }
        }

        private void iconDanhSach1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
            if (cst != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpDanhSachNVTrongThue(Main, cst.cl_id.ToString(), cst.cl_name, cst.cl_day.ToString(), cst.cl_day_end.ToString()));
            }
        }

        private void dopListSaffSmall0_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.Tax.clsTax.TaxListDetail cst = (sender as MenuItem).DataContext as OOP.CaiDatLuong.Tax.clsTax.TaxListDetail;
            if (cst != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.CaiDatThue.PopUpDanhSachNVTrongThue(Main, cst.cl_id.ToString(), cst.cl_name, cst.cl_day.ToString(), cst.cl_day_end.ToString()));
            }
        }
    }
}
