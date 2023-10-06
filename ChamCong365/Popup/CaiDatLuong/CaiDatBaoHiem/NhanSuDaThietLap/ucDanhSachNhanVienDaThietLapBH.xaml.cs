﻿using ChamCong365.Popup.CaiDatLuong;
using ChamCong365.Popup.PopupSalarySettings;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.ListTabInsurance
{
    /// <summary>
    /// Interaction logic for ucListSaffYesSettings.xaml
    /// </summary>
    public partial class ucDanhSachNhanVienDaThietLapBH : UserControl
    {
        List<InsuranceSafff> itemsis = new List<InsuranceSafff>();
        MainWindow Main;
        public class NhanVien
        {
            public string TenNhanVien { get; set; }
        }
        public class PhongBan
        {
            public string TenPB { get; set; }
        }
        public class Thang
        {
            public string thang { get; set; }
        }
        public class Nam
        {
            public string nam { get; set; }
        }
        //private string SearchNV = "";
        //private string SearchPB = "";
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        public List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> lstChuaTL = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> lstChuaTLFilter = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
        private string IdNV = "0";
        private string IdPB = "0";
        private int TongSoTrang = 0;
        private int SoDu = 0;
        public ucDanhSachNhanVienDaThietLapBH(MainWindow main)
        {
            InitializeComponent();
            
            Main = main;
            LoadDLNam();
            LoadDLThang();
            LoadDLPhongBan();
            LoadDLNhanVien();
            LoadDLNSuDaTL();
        }

        private void LoadDLNSuDaTL()
        {

            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_list_user_insrc")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("cls_id_com", Main.IdAcount);
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
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.Root>(b);
                    if (chuaTL.list_us != null)
                    {
                        foreach (var item in chuaTL.list_us)
                        {
                            foreach (var item1 in item.TinhluongListClass)
                            {
                                item.Tl_List_cls = item1.cl_name;
                            }
                            foreach (var itemdep in item.Deparment)
                            {
                                item.dep_id = itemdep.dep_id.ToString();
                                item.dep_name = itemdep.dep_name;
                            }
                            //foreach (var it in luongCB.listUser)
                            //{
                            //    if (it.idQLC == item.ep_id)
                            //    {
                            //        item.ep_phone = it.phone;
                            //        item.ep_email = it.email;
                            //        item.ep_address = it.address;
                            //        item.ep_name = it.userName;
                            //        item.ngay_bat_dau_lv = it.createdAt.ToString();
                            //        WebClient httpClient2 = new WebClient();
                            //        httpClient2.QueryString.Clear();
                            //        httpClient2.QueryString.Add("ID", it._id.ToString());
                            //        var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            //        APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            //        if (receiveInfoAPI.data != null)
                            //        {
                            //            item.ep_avatar = receiveInfoAPI.data.user_info.AvatarUser;
                            //        }
                            //    }
                            //}
                            //WebClient httpClient2 = new WebClient();
                            //httpClient2.QueryString.Clear();
                            //httpClient2.QueryString.Add("ID", item.ep_id.ToString());
                            //var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            //APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                            //if (receiveInfoAPI.data != null)
                            //{
                            //    item.ep_name = receiveInfoAPI.data.user_info.UserName;
                            //    item.ep_avatar = receiveInfoAPI.data.user_info.AvatarUser;
                            //}
                            WebClient httpClient2 = new WebClient();
                            httpClient2.QueryString.Clear();
                            httpClient2.QueryString.Add("ep_id", item.cls_id_user.ToString());
                            httpClient2.QueryString.Add("cp", Main.IdAcount.ToString());
                            httpClient2.QueryString.Add("token", Properties.Settings.Default.Token);
                            var response = httpClient2.UploadValues(new Uri("http://210.245.108.202:3009/api/tinhluong/nhanvien/qly_ho_so_ca_nhan"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                            OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root receiveInfoAPI = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.cls_Profile.Root>(UnicodeEncoding.UTF8.GetString(response));
                            if (receiveInfoAPI.data != null)
                            {
                                item.cls_name_user = receiveInfoAPI.data.info_dep_com.user.userName;

                                if (receiveInfoAPI.data.info_dep_com.user.avatarUser == "" || receiveInfoAPI.data.info_dep_com.user.avatarUser == null)
                                {
                                    item.Avatar_Us = "Resource/image/llll.jpg";
                                }
                                else
                                {
                                    item.Avatar_Us = "https://chamcong.24hpay.vn/upload/employee/" + receiveInfoAPI.data.info_dep_com.user.avatarUser;
                                }
                            }
                            lstChuaTL.Add(item);
                        }
                        //lstLuongCB = luongCB.listResult;
                        if (lstChuaTL.Count > 10)
                        {
                            TongSoTrang = chuaTL.list_us.Count / 10;
                            SoDu = 10 - (chuaTL.list_us.Count % 10);
                            if (chuaTL.list_us.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            if (TongSoTrang < 3)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                            }
                            for (int i = 0; i < 10; i++)
                            {
                                lstChuaTLPT.Add(chuaTL.list_us[i]);
                            }
                            //lstLuongCB = luongCB.listResult;
                            lsvListSaffYes.ItemsSource = lstChuaTLPT;

                            docPhanTrang.Visibility = Visibility.Visible;

                        }
                        else
                        {
                            lsvListSaffYes.ItemsSource = lstChuaTL;
                            docPhanTrang.Visibility = Visibility.Collapsed;
                        }

                    }
                }

            }
            catch
            {

            }
        }

        private void LoadDLNhanVien()
        {
            textHienThiNhanVien.Text = "Tất cả nhân viên";
            lstNhanVien = Main.lstNhanVienThuocCongTy;

            lsvNhanVien.ItemsSource = lstNhanVien;
            //lsvNhanVien.Items.Add("Tất cả nhân viên");
            //lsvNhanVien.Items.Add("(111788) Đỗ Văn Hoàng");
            //lsvNhanVien.Items.Add("(90229) Nguyễn Công Tiến");
        }

        private void LoadDLPhongBan()
        {
            textHienThiPhongBan.Text = "Phòng ban (tất cả)";

            lstPhongBan = Main.lstPhongBanThuocCongTy;
            lsvPhongBan.ItemsSource = lstPhongBan;
        }

        private void LoadDLThang()
        {
            textHienThiThang.Text = "Tháng " + DateTime.Now.Month.ToString();
            lstThang.Add(new Thang { thang = "Tháng 1" });
            lstThang.Add(new Thang { thang = "Tháng 2" });
            lstThang.Add(new Thang { thang = "Tháng 3" });
            lstThang.Add(new Thang { thang = "Tháng 4" });
            lstThang.Add(new Thang { thang = "Tháng 5" });
            lstThang.Add(new Thang { thang = "Tháng 6" });
            lstThang.Add(new Thang { thang = "Tháng 7" });
            lstThang.Add(new Thang { thang = "Tháng 8" });
            lstThang.Add(new Thang { thang = "Tháng 9" });
            lstThang.Add(new Thang { thang = "Tháng 10" });
            lstThang.Add(new Thang { thang = "Tháng 11" });
            lstThang.Add(new Thang { thang = "Tháng 12" });
            lsvThang.ItemsSource = lstThang;
        }

        private void LoadDLNam()
        {
            textHienThiNam.Text = "Năm " + DateTime.Now.Year.ToString();
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lsvNam.ItemsSource = lstNam;
        }
        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Visible)
            {
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
            }
            else if (borNam.Visibility == Visibility.Visible)
            {
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
            }
            else if (borPhongBan.Visibility == Visibility.Visible)
            {
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
            }
            else if (borNhanVien.Visibility == Visibility.Visible)
            {
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
            }
        }

        private void bodDleteSaffYes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucXoaBaoHiemNhanSu());
            
        }

        
        private void bodEditInsuranceSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucChinhSuaBHNhanVien());
        }

        private void bodThongKe_MouseEnter(object sender, MouseEventArgs e)
        {
            bodThongKe.BorderThickness = new Thickness(1);
        }

        private void bodThongKe_MouseLeave(object sender, MouseEventArgs e)
        {
            bodThongKe.BorderThickness = new Thickness(0);
        }

        private void borHienThiNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNhanVien.Visibility == Visibility.Collapsed)
            {
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borNhanVien.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void borHienThiPhongBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borPhongBan.Visibility == Visibility.Collapsed)
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borPhongBan.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void borHienThiNam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNam.Visibility == Visibility.Collapsed)
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borNam.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void borHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Collapsed)
            {
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borThang.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        private void textSearchThang_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchThang = new List<Thang>();
            foreach (var str in lstThang)
            {
                if (str.thang.Contains(textSearchThang.Text.ToString()))
                {
                    lstSearchThang.Add(str);

                }
            }
            lsvThang.ItemsSource = lstSearchThang;
            if (textSearchThang.Text == "")
            {
                lsvThang.ItemsSource = lstThang;
            }
        }
        private void lsvPhongBan_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (sender is ListView && !e.Handled)
            {
                e.Handled = true;
                var eventArg = new MouseWheelEventArgs(e.MouseDevice, e.Timestamp, e.Delta);
                eventArg.RoutedEvent = UIElement.MouseWheelEvent;
                eventArg.Source = sender;
                var parent = ((Control)sender).Parent as UIElement;
                parent.RaiseEvent(eventArg);
            }
            //scrollPhongBan.ScrollToVerticalOffset(scrollPhongBan.VerticalOffset - e.Delta);
        }
        private void lsvNhanVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNhanVien.ScrollToVerticalOffset(scrollNhanVien.VerticalOffset - e.Delta);
        }

        private void borTenPB_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            OOP.clsPhongBanThuocCongTy.Item pb = (sender as Border).DataContext as OOP.clsPhongBanThuocCongTy.Item;
            if (pb != null)
            {
                borHienThiPhongBan.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borPhongBan.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                textHienThiPhongBan.Text = pb.dep_name;
                Main.PhongBan = pb.dep_name;
                IdPB = pb.dep_id.ToString();
                if (IdPB != "0")
                {
                    foreach (var nv in Main.lstNhanVienThuocCongTy)
                    {
                        if (nv.department != null)
                        {
                            foreach (var item in nv.department)
                            {
                                if (IdPB == item.dep_id)
                                {
                                    lstSearchNV.Add(nv);
                                }
                            }
                        }

                    }
                    lsvNhanVien.ItemsSource = lstSearchNV;
                }
                else
                {
                    lsvNhanVien.ItemsSource = lstNhanVien;
                }

            }
        }
        private void textSearchNhanVien_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
            foreach (var str in lstNhanVien)
            {
                if (str.userName.Contains(textSearchNhanVien.Text.ToString()))
                {
                    lstSearchNV.Add(str);

                }
            }
            lsvNhanVien.ItemsSource = lstSearchNV;
            if (textSearchNhanVien.Text == "")
            {
                lsvNhanVien.ItemsSource = lstNhanVien;
            }
        }

        private void borThang_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thang th = (sender as Border).DataContext as Thang;
            if (th != null)
            {
                borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                textHienThiThang.Text = th.thang;
                Main.Thang = th.thang;
            }
        }

        private void borNam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Nam th = (sender as Border).DataContext as Nam;
            if (th != null)
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                textHienThiNam.Text = th.nam;
                Main.Nam = th.nam;
            }
        }

        private void textSearchNam_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNam = new List<Nam>();
            foreach (var str in lstNam)
            {
                if (str.nam.Contains(textSearchNam.Text.ToString()))
                {
                    lstSearchNam.Add(str);

                }
            }
            lsvNam.ItemsSource = lstSearchNam;
            if (textSearchNam.Text == "")
            {
                lsvNam.ItemsSource = lstNam;
            }
        }

        private void lsvNam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNam.ScrollToVerticalOffset(scrollNam.VerticalOffset - e.Delta);
        }
        private void borTenNhanVien_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OOP.clsNhanVienThuocCongTy.ListUser nv = (sender as Border).DataContext as OOP.clsNhanVienThuocCongTy.ListUser;
            if (nv != null)
            {
                textHienThiNhanVien.Text = nv.userName;
                borHienThiNhanVien.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNhanVien.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.NhanVien = nv.userName;
                IdNV = nv.idQLC.ToString();
                //SearchNV = nv.idQLC.ToString();
            }
        }
        private void lsvThang_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollThang.ScrollToVerticalOffset(scrollThang.VerticalOffset - e.Delta);
        }

        private void lsvListSaffYes_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        

        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            textPage1.Text = "1";
            textPage2.Text = "2";
            textPage3.Text = "3";
            borLen1.Visibility = Visibility.Visible;
            borPageCuoi.Visibility = Visibility.Visible;
            lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
            for (int i = 0; i < 10; i++)
            {
                lstChuaTLPT.Add(lstChuaTL[i]);
            }
            //lstLuongCB = luongCB.listResult;
            lsvListSaffYes.ItemsSource = lstChuaTLPT;
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage3.Text == TongSoTrang.ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                {
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    if (textPage1.Text == "1")
                    {
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                        lstChuaTL = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLPT;
                    }


                }
            }

        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(textPage1.Text) > 1)
            {
                if (textPage1.Text == (TongSoTrang - 2).ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                {
                    textPage1.Text = (TongSoTrang - 3).ToString();
                    textPage2.Text = (TongSoTrang - 2).ToString();
                    textPage3.Text = (TongSoTrang - 1).ToString();
                    BrushConverter brus = new BrushConverter();

                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borLen1.Visibility = Visibility.Visible;
                    borPageCuoi.Visibility = Visibility.Visible;
                    lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLPT;
                }
            }
            else
            {
                BrushConverter brus = new BrushConverter();
                borPageDau.Visibility = Visibility.Collapsed;
                borLui1.Visibility = Visibility.Collapsed;
                borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                borLen1.Visibility = Visibility.Visible;
                borPageCuoi.Visibility = Visibility.Visible;
                lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = 0; i < 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLPT;
            }
        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TongSoTrang == 2)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLPT;
            }
            else if (TongSoTrang > 2)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                borPageCuoi.Visibility = Visibility.Visible;
                borLen1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLPT;
            }
        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textPage3.Text == TongSoTrang.ToString())
            {
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();

                for (int i = int.Parse(textPage3.Text) * 10 - 10; i < int.Parse(textPage3.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLPT;
            }
            else
            {
                if (TongSoTrang == 3)
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLPT;
                }
                else if (TongSoTrang > 3)
                {
                    if (textPage3.Text == TongSoTrang.ToString())
                    {
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPageCuoi.Visibility = Visibility.Collapsed;
                        borLen1.Visibility = Visibility.Collapsed;
                        lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLPT;
                    }
                    else if (textPage3.Text == "3")
                    {
                        textPage1.Text = "2";
                        textPage2.Text = "3";
                        textPage3.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLPT;
                    }

                }
            }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TongSoTrang == 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLPT;
            }
            else if (TongSoTrang > 3)
            {
                if (textPage3.Text == TongSoTrang.ToString())
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;
                    lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLPT;

                }
                else if (textPage3.Text == "3")
                {

                    if (borPageDau.Visibility == Visibility.Collapsed && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                        lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = 10; i < 20; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLPT;

                    }
                    else if (borPageDau.Visibility == Visibility.Visible && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        textPage1.Text = "2";
                        textPage2.Text = "3";
                        textPage3.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = 20; i < 30; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLPT;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLPT;
                }

            }
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borPageDau.Visibility = Visibility.Visible;
            borLui1.Visibility = Visibility.Visible;
            borPageCuoi.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            if (TongSoTrang >= 3)
            {
                textPage1.Text = (TongSoTrang - 2).ToString();
                textPage2.Text = (TongSoTrang - 1).ToString();
                textPage3.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLPT;
            }

            else
            {
                textPage1.Text = (TongSoTrang - 1).ToString();
                textPage2.Text = TongSoTrang.ToString();
                //textPage3S.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                //borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                //textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLPT;
            }
        }

        private void bodThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lstChuaTLFilter = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
            if (IdPB == "0" && IdNV == "0")
            {
                if (lstChuaTL.Count > 10)
                {
                    lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    TongSoTrang = lstChuaTL.Count / 10;
                    SoDu = 10 - (lstChuaTL.Count % 10);
                    if (lstChuaTL.Count % 10 > 0)
                    {
                        TongSoTrang++;
                    }
                    if (TongSoTrang < 3)
                    {
                        borPage3S.Visibility = Visibility.Collapsed;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLPT;
                    //if (TongSoTrang < 3)
                    //{
                    //    if (TongSoTrang == 2)
                    //    {
                    //        borPage3.Visibility = Visibility.Collapsed;
                    //        borLen1.Visibility = Visibility.Collapsed;
                    //        borPageCuoi.Visibility = Visibility.Collapsed;
                    //    }
                    //    else if (TongSoTrang == 1)
                    //    {
                    //        borPage2.Visibility = Visibility.Collapsed;
                    //        borPage3.Visibility = Visibility.Collapsed;
                    //        borLen1.Visibility = Visibility.Collapsed;
                    //        borPageCuoi.Visibility = Visibility.Collapsed;
                    //    }
                    //}
                    //else
                    //{
                    //    borLui1.Visibility = Visibility.Collapsed;
                    //    borPageDau.Visibility = Visibility.Collapsed;
                    //    borLen1.Visibility = Visibility.Visible;
                    //    borPageCuoi.Visibility = Visibility.Visible;
                    //}
                    docPhanTrang.Visibility = Visibility.Visible;
                    docPhanTrangS.Visibility = Visibility.Collapsed;

                }
                else
                {
                    lsvListSaffYes.ItemsSource = lstChuaTL;
                    docPhanTrang.Visibility = Visibility.Collapsed;
                    docPhanTrangS.Visibility = Visibility.Collapsed;
                }
            }
            else
            {
                foreach (var item in lstChuaTL)
                {
                    if (item.cls_id_user.ToString() == IdNV && IdPB == "0")
                    {
                        lstChuaTLFilter.Add(item);
                    }
                    else if (IdNV == "0" && IdPB == item.dep_id)
                    {
                        lstChuaTLFilter.Add(item);
                    }
                    else if (IdNV == item.cls_id_user.ToString() && IdPB == item.dep_id)
                    {
                        lstChuaTLFilter.Add(item);
                    }
                }
                if (lstChuaTLFilter.Count > 10)
                {
                    docPhanTrang.Visibility = Visibility.Collapsed;
                    docPhanTrangS.Visibility = Visibility.Visible;
                    TongSoTrang = lstChuaTLFilter.Count / 10;
                    SoDu = 10 - (lstChuaTLFilter.Count % 10);
                    if (lstChuaTLFilter.Count % 10 > 0)
                    {
                        TongSoTrang++;
                    }
                    if (TongSoTrang < 3)
                    {
                        borPage3S.Visibility = Visibility.Collapsed;
                    }
                    for (int i = 0; i < 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                }
                else
                {
                    docPhanTrang.Visibility = Visibility.Collapsed;
                    docPhanTrangS.Visibility = Visibility.Collapsed;
                    lsvListSaffYes.ItemsSource = lstChuaTLFilter;
                }

            }
        }
        private void borPageDauS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPageDauS.Visibility = Visibility.Collapsed;
            borLui1S.Visibility = Visibility.Collapsed;
            borPage1S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
            textPage1S.Text = "1";
            textPage2S.Text = "2";
            textPage3S.Text = "3";
            borLen1S.Visibility = Visibility.Visible;
            borPageCuoiS.Visibility = Visibility.Visible;
            lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> ();
            for (int i = 0; i < 10; i++)
            {
                lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
            }
            //lstLuongCB = luongCB.listResult;
            lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
        }

        private void borLui1S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (int.Parse(textPage1S.Text) >= 1)
            {
                if (textPage3S.Text == TongSoTrang.ToString() && borPageCuoiS.Visibility == Visibility.Collapsed)
                {
                    borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoiS.Visibility = Visibility.Visible;
                    borLen1S.Visibility = Visibility.Visible;
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                }
                else
                {
                    if (textPage1S.Text == "1")
                    {
                        borPageDauS.Visibility = Visibility.Collapsed;
                        borLui1S.Visibility = Visibility.Collapsed;
                        borPage1S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borLen1S.Visibility = Visibility.Visible;
                        borPageCuoiS.Visibility = Visibility.Visible;
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                    }
                    else
                    {
                        textPage1S.Text = (int.Parse(textPage1S.Text) - 1).ToString();
                        textPage2S.Text = (int.Parse(textPage2S.Text) - 1).ToString();
                        textPage3S.Text = (int.Parse(textPage3S.Text) - 1).ToString();
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                    }


                }
            }
        }

        private void borPage1S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(textPage1S.Text) > 1)
            {
                if (textPage1S.Text == (TongSoTrang - 2).ToString() && borPageCuoiS.Visibility == Visibility.Collapsed)
                {
                    textPage1S.Text = (TongSoTrang - 3).ToString();
                    textPage2S.Text = (TongSoTrang - 2).ToString();
                    textPage3S.Text = (TongSoTrang - 1).ToString();
                    BrushConverter brus = new BrushConverter();

                    borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borLen1S.Visibility = Visibility.Visible;
                    borPageCuoiS.Visibility = Visibility.Visible;
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                }
                else
                {
                    textPage1S.Text = (int.Parse(textPage1S.Text) - 1).ToString();
                    textPage2S.Text = (int.Parse(textPage2S.Text) - 1).ToString();
                    textPage3S.Text = (int.Parse(textPage3S.Text) - 1).ToString();
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                }
            }
            else
            {
                BrushConverter brus = new BrushConverter();
                borPageDauS.Visibility = Visibility.Collapsed;
                borLui1S.Visibility = Visibility.Collapsed;
                borPage1S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borLen1S.Visibility = Visibility.Visible;
                borPageCuoiS.Visibility = Visibility.Visible;
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = 0; i < 10; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
            }
        }

        private void borPage2S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TongSoTrang == 2)
            {
                borPageDauS.Visibility = Visibility.Visible;
                borLui1S.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
            }
            else if (TongSoTrang > 2)
            {
                borPageDauS.Visibility = Visibility.Visible;
                borLui1S.Visibility = Visibility.Visible;
                borPageCuoiS.Visibility = Visibility.Visible;
                borLen1S.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
            }
        }

        private void borPage3S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textPage3S.Text == TongSoTrang.ToString())
            {
                BrushConverter brus = new BrushConverter();
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoiS.Visibility = Visibility.Collapsed;
                borLen1S.Visibility = Visibility.Collapsed;
                borPageDauS.Visibility = Visibility.Visible;
                borLui1S.Visibility = Visibility.Visible;
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();

                for (int i = int.Parse(textPage3S.Text) * 10 - 10; i < int.Parse(textPage3S.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
            }
            else
            {
                if (TongSoTrang == 3)
                {
                    borPageDauS.Visibility = Visibility.Visible;
                    borLui1S.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                }
                else if (TongSoTrang > 3)
                {
                    if (textPage3S.Text == TongSoTrang.ToString())
                    {
                        borPageDauS.Visibility = Visibility.Visible;
                        borLui1S.Visibility = Visibility.Visible;
                        BrushConverter brus = new BrushConverter();
                        borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPageCuoiS.Visibility = Visibility.Collapsed;
                        borLen1S.Visibility = Visibility.Collapsed;
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                    }
                    else if (textPage3S.Text == "3")
                    {
                        textPage1S.Text = "2";
                        textPage2S.Text = "3";
                        textPage3S.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3S.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3S.Foreground = (Brush)brus.ConvertFrom("#474747");
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                    }
                    else
                    {
                        textPage1S.Text = (int.Parse(textPage1S.Text) + 1).ToString();
                        textPage2S.Text = (int.Parse(textPage2S.Text) + 1).ToString();
                        textPage3S.Text = (int.Parse(textPage3S.Text) + 1).ToString();
                        lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                        for (int i = int.Parse(textPage2S.Text) * 10 - 10; i < int.Parse(textPage2S.Text) * 10; i++)
                        {
                            lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
                    }

                }
            }
        }

        private void borLen1S_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void borPageCuoiS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borPageDauS.Visibility = Visibility.Visible;
            borLui1S.Visibility = Visibility.Visible;
            borPageCuoiS.Visibility = Visibility.Collapsed;
            borLen1S.Visibility = Visibility.Collapsed;
            if (TongSoTrang >= 3)
            {
                textPage1S.Text = (TongSoTrang - 2).ToString();
                textPage2S.Text = (TongSoTrang - 1).ToString();
                textPage3S.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
            }

            else
            {
                textPage1S.Text = (TongSoTrang - 1).ToString();
                textPage2S.Text = TongSoTrang.ToString();
                //textPage3S.Text = TongSoTrang.ToString();
                BrushConverter brus = new BrushConverter();
                borPage1S.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1S.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                //borPage3S.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                //textPage3S.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLFilterPT.Add(lstChuaTLFilter[i]);
                }
                //lstLuongCB = luongCB.listResult;
                lsvListSaffYes.ItemsSource = lstChuaTLFilterPT;
            }
        }
    }
}
