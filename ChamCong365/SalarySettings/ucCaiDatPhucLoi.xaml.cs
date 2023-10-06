using ChamCong365.Popup.ChamCong;
using ChamCong365.Popup.CaiDatLuong.CaiDatPhucLoi;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace ChamCong365.SalarySettings
{
    /// <summary>
    /// Interaction logic for ucCaiDatPhucLoi.xaml
    /// </summary>
    /// 
    
    public partial class ucCaiDatPhucLoi : UserControl
    {
        MainWindow Main;
        BrushConverter br = new BrushConverter();
        public List<OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf> lstPhucLoi = new List<OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf>();
        public ucCaiDatPhucLoi(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDLPhucLoi();
        }

        private void LoadDLPhucLoi()
        {
            
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_phuc_loi")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("companyId", Main.IdAcount);

                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.Root PhucLoi = JsonConvert.DeserializeObject<OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.Root>(b);
                    if (PhucLoi.data.list_welf != null)
                    {
                        foreach (var item in PhucLoi.data.list_welf)
                        {
                            if (item.cl_type_tax == 0)
                            {
                                item.cl_type_tax_s = "Thu nhập chịu thuế";
                            }
                            else if (item.cl_type_tax == 1)
                            {
                                item.cl_type_tax_s = "Thu nhập miễn thuế";
                            }
                            lstPhucLoi.Add(item);
                        }
                        dgvCaiDatPhucLoi.ItemsSource = lstPhucLoi;
                    }
                }
            }
            catch
            {

            }

        }

        private void btnThemNhanVien_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf pl = (sender as MenuItem).DataContext as OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf;
            if (pl != null)
            {
                Main.grShowPopup.Children.Add(new ucThemNVHuongPhucLoi(Main, pl));


            }
        }

        private void btnXoaNhanVien_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf pl = (sender as MenuItem).DataContext as OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf;
            if (pl != null)
            {
                Main.grShowPopup.Children.Add(new ucXoaPhucLoi(Main, this, pl));

            }
        }

        private void btnDanhSachNhanVien_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf pl = (sender as MenuItem).DataContext as OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf;
            if (pl != null)
            {
                Main.grShowPopup.Children.Add(new ucDanhSachNVHuongPhucLoi(Main, pl));
            }
        }

        private void bodThemMoiPhucLoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiPhucLoi(Main, this));
        }

        private void bodThemMoiPhucLoi_MouseEnter(object sender, MouseEventArgs e)
        {
            bodThemMoiPhucLoi.BorderThickness = new Thickness(1);
        }

        private void bodThemMoiPhucLoi_MouseLeave(object sender, MouseEventArgs e)
        {
            bodThemMoiPhucLoi.BorderThickness = new Thickness(0);
        }

        private void btnChinhSuaNhanVien_Click(object sender, RoutedEventArgs e)
        {
            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf pl = (sender as MenuItem).DataContext as OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf;
            if (pl != null)
            {
                Main.grShowPopup.Children.Add(new ucThemMoiPhucLoi(Main, this, pl));

            }
            
        }

        private void dgvCaiDatPhucLoi_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
    }
}
