using QuanLyChung365TruocDangNhap.ChamCongNew.Core;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucListSaffInsurance.xaml
    /// </summary>
    /// 

    public partial class ucDanhSachNhanVienBH : System.Windows.Controls.UserControl
    {
        BrushConverter br = new BrushConverter();
       
        MainWindow Main;
        private List<OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBH.ListU> lstUs = new List<OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBH.ListU>();
        private List<OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBHList.ListUserFinal> lstUsLs = new List<OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBHList.ListUserFinal>();
        public OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBHList.ListUserFinal UserSelected = new OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBHList.ListUserFinal();
        public int cl_id;
        public ucDanhSachNhanVienBH(MainWindow main, int s,string Tieude)
        {
            InitializeComponent();
            //ucDanhSachBHNhanVien ucL = new ucDanhSachBHNhanVien(Main);
            //grLoadListInsurance.Children.Clear();
            //object Content = ucL.Content;
            //ucL.Content = null;
            //grLoadListInsurance.Children.Add(Content as UIElement);
            textTieuDe.Text = Tieude;
            bodSaffs.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSaffs.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbTextSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            Main = main;
            bodAddSaffs.Visibility = Visibility.Collapsed;
            btnclose.Visibility = Visibility.Visible;
            ucDanhSachBHNhanVien ucb = new ucDanhSachBHNhanVien(Main);
            ucDanhSachNhomBH ucg = new ucDanhSachNhomBH(Main);
            //txbCountSaff.Text = ucb.dgvListSaffInsurance.Items.Count.ToString();
            //txbCountGrounds.Text = ucg.dgvListGroundInsurance.Items.Count.ToString();
            LoadDLNhanVienBHList(s);
            cl_id = s;
        }

        private async void LoadDLNhanVienBHList(int IdBH)
        {
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_list_nv_insrc")))
            {
                try
                {
                    Pageloading.Visibility= Visibility.Visible;
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;

                    request.AddParameter("cls_id_cl", IdBH);
                    request.AddParameter("cls_id_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = await restclient.ExecuteAsync(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBHList.Root nvbh = JsonConvert.DeserializeObject<OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBHList.Root>(b);
                    if (nvbh.listUserFinal != null)
                    {
                        Pageloading.Visibility= Visibility.Collapsed;
                        foreach (var item in nvbh.listUserFinal)
                        {
                            if (string.IsNullOrEmpty(item.avatarUser)) item.avatarUser = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=64&q=75";
                            lstUsLs.Add(item);

                        }
                        txbCountSaff.Text = lstUsLs.Count.ToString();
                        dgvListSaffInsuranceLst.Visibility = Visibility.Visible;
                        dgvListSaffInsurance.Visibility = Visibility.Collapsed;
                        dgvListSaffInsuranceLst.ItemsSource = lstUsLs;

                    }
                }
                catch
                {
                    Pageloading.Visibility= Visibility.Collapsed;
                }
            }
        }

        private void LoadDLNhanVienBH(int id_cl)
        {
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_list_user_insrc")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                if (DateTime.Now.Month < 10)
                {
                    request.AddParameter("start_date", $"{DateTime.Now.Year}-0{DateTime.Now.Month}-01T00:00:00.000+00:00");
                }
                else
                {
                    request.AddParameter("start_date", $"{DateTime.Now.Year}-{DateTime.Now.Month}-01T00:00:00.000+00:00");
                }
                if (DateTime.Now.Month == 12)
                {
                    request.AddParameter("end_date", $"{DateTime.Now.Year + 1}-01-01T00:00:00.000+00:00");

                }
                else
                {
                    if (DateTime.Now.Month + 1 < 10)
                    {
                        request.AddParameter("end_date", $"{DateTime.Now.Year}-0{DateTime.Now.Month + 1}-01T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("end_date", $"{DateTime.Now.Year}-{DateTime.Now.Month + 1}-01T00:00:00.000+00:00");
                    }

                }
                request.AddParameter("cls_id_com", Main.IdAcount);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBH.Root nvbh = JsonConvert.DeserializeObject<OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBH.Root>(b);
                if (nvbh.list_us != null)
                {
                    foreach(var item in nvbh.list_us)
                    {
                        if (item.cls_id_cl == id_cl)
                        {
                            if (string.IsNullOrEmpty(item.cls_Img)) item.cls_Img = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=64&q=75";

                            lstUs.Add(item);
                        }
                        
                    }
                    dgvListSaffInsuranceLst.Visibility = Visibility.Collapsed;
                    dgvListSaffInsurance.Visibility = Visibility.Visible;
                    dgvListSaffInsurance.ItemsSource = lstUs;

                }
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodSaffs_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            /*bodAddSaffs.Visibility = Visibility.Visible;
            bodAddGround.Visibility = Visibility.Collapsed;
            bodSaffs.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSaffs.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbTextSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            bodGrounds.BorderThickness = new Thickness(0, 0, 0, 0);
            ucDanhSachBHNhanVien ucL = new ucDanhSachBHNhanVien(Main);
            grLoadListInsurance.Children.Clear();
            object Content = ucL.Content;
            ucL.Content = null;
            grLoadListInsurance.Children.Add(Content as UIElement);*/
            
        }

        private void bodGrounds_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodAddGround.Visibility = Visibility.Visible;
            bodAddSaffs.Visibility = Visibility.Collapsed;
            //bodGrounds.BorderThickness = new Thickness(0, 0, 0, 3);
            //bodGrounds.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            //txbTextGround.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            txbTextSaff.Foreground = (Brush)br.ConvertFrom("#474747");
            bodSaffs.BorderThickness = new Thickness(0, 0, 0, 0);
            ucDanhSachNhomBH ucL = new ucDanhSachNhomBH(Main);
            grLoadListInsurance.Children.Clear();
            object Content = ucL.Content;
            ucL.Content = null;
            grLoadListInsurance.Children.Add(Content as UIElement);
        }

        private void bodAddSaffs_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bodAddSaffs.BorderThickness = new Thickness(1);
        }

        private void bodAddSaffs_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {
            bodAddSaffs.BorderThickness = new Thickness(0);
        }

        private void bodAddGround_MouseLeave(object sender, MouseEventArgs e)
        {
            bodAddGround.BorderThickness = new Thickness(1);
        }

        private void bodAddGround_MouseEnter(object sender, MouseEventArgs e)
        {
            bodAddGround.BorderThickness = new Thickness(0);
        }

        private void bodAddGround_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            grLoadPopup.Children.Add(new ucThemNhomBaoHiem()); 
        }

        private void dgvListSaffInsurance_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void dgvListSaffInsuranceLst_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if(Main.keyDown == Key.LeftShift || Main.keyDown == Key.RightShift)
            {
                dgvListSaffInsuranceLst.GetFirstChildOfType<ScrollViewer>().ScrollToHorizontalOffset(dgvListSaffInsuranceLst.GetFirstChildOfType<ScrollViewer>().HorizontalOffset - e.Delta);
                //scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta);
            }
            else
                scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void btnExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            popup.Visibility = Visibility.Hidden;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            using(WebClient web = new WebClient())
            {
                try
                {
                    web.QueryString.Add("cl_id", UserSelected.cls_id.ToString());
                    web.QueryString.Add("token", Properties.Settings.Default.Token);
                    web.UploadValuesAsync(new Uri("https://api.timviec365.vn/api/tinhluong/congty/delete_nv_insrc"), "POST", web.QueryString);
                    web.UploadValuesCompleted += (s1, e1) =>
                    {
                        var check = UTF8Encoding.UTF8.GetString(e1.Result);
                        if (check.Contains("success"))
                        {
                            popup.Visibility = Visibility.Hidden;
                            lstUsLs.Remove(UserSelected);
                            dgvListSaffInsuranceLst.ItemsSource = lstUsLs;
                            dgvListSaffInsuranceLst.Items.Refresh();
                        }
                    };
                }
                catch { }
            }
        }

        private void bodDeletes_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UserSelected = (OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBHList.ListUserFinal)(sender as Border).DataContext;
            popup.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            popup.Visibility = Visibility.Hidden;
        }

        private void bodAddSaffs_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Main.grShowPopup.Children.Add(new ucThemNhanVienBaoHiem(Main, cl_id));
        }

        private void btnclose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
