using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem
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
            ucDanhSachBHNhanVien ucb = new ucDanhSachBHNhanVien(Main);
            ucDanhSachNhomBH ucg = new ucDanhSachNhomBH(Main);
            //txbCountSaff.Text = ucb.dgvListSaffInsurance.Items.Count.ToString();
            //txbCountGrounds.Text = ucg.dgvListGroundInsurance.Items.Count.ToString();
            if (s != 3 || s != 4 || s != 5)
            {
                LoadDLNhanVienBHList(s);
            }
            else
            {
                LoadDLNhanVienBH(s);
            }
        }

        private void LoadDLNhanVienBHList(int IdBH)
        {
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/take_list_nv_insrc")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                
                request.AddParameter("cls_id_cl", IdBH);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBHList.Root nvbh = JsonConvert.DeserializeObject<OOP.CaiDatLuong.BaoHiem.clsDSNhanVienBHList.Root>(b);
                if (nvbh.listUserFinal != null)
                {
                    foreach (var item in nvbh.listUserFinal)
                    {
                        if (item._id == IdBH)
                        {
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ep_id", item.idQLC.ToString());
                            httpClient2.QueryString.Add("cp", Main.IdAcount.ToString());
                            httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
                            var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                //item.cls_name = receiveInfoAPI.data.info_dep_com.user.userName;
                                if (receiveInfoAPI.data.info_dep_com.user.avatarUser == "")
                                {
                                    item.avatarUser = "Resource/image/llll.jpg";
                                }
                                else
                                {
                                    item.avatarUser = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;

                                }
                            }

                            lstUsLs.Add(item);
                        }

                    }
                    dgvListSaffInsuranceLst.Visibility = Visibility.Visible;
                    dgvListSaffInsurance.Visibility = Visibility.Collapsed;
                    dgvListSaffInsuranceLst.ItemsSource = lstUsLs;

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
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ep_id", item.cls_id_user.ToString());
                            httpClient2.QueryString.Add("cp", Main.IdAcount.ToString());
                            httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
                            var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                item.cls_name = receiveInfoAPI.data.info_dep_com.user.userName;
                                if (receiveInfoAPI.data.info_dep_com.user.avatarUser == "")
                                {
                                    item.cls_Img = "Resource/image/llll.jpg";
                                }
                                else
                                {
                                    item.cls_Img = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;

                                }
                            }

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
            bodAddSaffs.Visibility = Visibility.Visible;
            bodAddGround.Visibility = Visibility.Collapsed;
            bodSaffs.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSaffs.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbTextSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            bodGrounds.BorderThickness = new Thickness(0, 0, 0, 0);
            ucDanhSachBHNhanVien ucL = new ucDanhSachBHNhanVien(Main);
            grLoadListInsurance.Children.Clear();
            object Content = ucL.Content;
            ucL.Content = null;
            grLoadListInsurance.Children.Add(Content as UIElement);
            
            
        }

        private void bodGrounds_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodAddGround.Visibility = Visibility.Visible;
            bodAddSaffs.Visibility = Visibility.Collapsed;
            bodGrounds.BorderThickness = new Thickness(0, 0, 0, 3);
            bodGrounds.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
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
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }
    }
}
