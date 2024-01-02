using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ListTabInsurance
{
    /// <summary>
    /// Interaction logic for ucListSaffNotSettings.xaml
    /// </summary>
    /// 
    public class InsuranceSafff
    {
        public string Name { get; set; }
        public int ID { get; set; }
        public string PhongBan { get; set; }
        public string Anh { get; set; }
        public string ChinhSachBaoHiem { get; set; }
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int TienBaoHiem { get; set; }
    }
    public partial class ucDanhSachNhanVienChuaThietLapBH : System.Windows.Controls.UserControl
    {
        MainWindow Main;
        //private string SearchNV = "";
        //private string SearchPB = "";
        public List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal> lstChuaTL = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal> lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal> lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal> lstChuaTLFilter = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal> lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
        private string IdNV = "0";
        private string IdPB = "0";
        private int TongSoTrang = 0;
        private int PageNumberCurrent = 1;
        private int SoDu = 0;
        public int NumberPerPage = 10;
        public ucDanhSachNhanVienChuaThietLapBH(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDLNam();
            LoadDLThang();
            LoadDLNhanVien();
            LoadDLNguoiChuaThietLap();
        }
        private async void LoadDLNguoiChuaThietLap()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/show_list_user_noinsrc")))
                {
                    loading.Visibility = Visibility.Visible;
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;

                    request.AddParameter("cls_id_com", Main.IdAcount);
                    int month, year;
                    if (lsvThang.SelectedItem != null)
                        month = int.Parse(lsvThang.SelectedItem.ToString().Split(' ')[1]);
                    else month = DateTime.Now.Month;
                    if(lsvNam.SelectedItem != null) year = int.Parse(lsvNam.SelectedItem.ToString().Split(' ')[1]);
                    else year = DateTime.Now.Year;
                    if (month > 10)
                    {
                        request.AddParameter("start_date", year + "-" + month + "-01T00:00:00.000+00:00");
                        request.AddParameter("end_date", year + "-" + month + "-" + DateTime.DaysInMonth(year, month) + "T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("start_date", year + "-0" + month + "-01T00:00:00.000+00:00");
                        request.AddParameter("end_date", year + "-0" + month + "-" + DateTime.DaysInMonth(year, month) + "T00:00:00.000+00:00");
                    }
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = await restclient.ExecuteAsync(request);
                    var b = resAlbum.Content;
                    lstChuaTLPage.Clear();
                    lstChuaTL.Clear();
                    OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.Root>(b);
                    if (chuaTL.listUserFinal != null)
                    {

                        foreach (var item in chuaTL.listUserFinal)
                        {
                            item.Avatar_Us = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=64&q=75";
                            if(lsvNhanVien.SelectedItem != null && lsvNhanVien.SelectedIndex != 0)
                            {
                                if(((OOP.clsNhanVienThuocCongTy.ListUser)lsvNhanVien.SelectedItem).idQLC == item.idQLC)
                                {
                                    if (lstChuaTLPage.Count < NumberPerPage)
                                        lstChuaTLPage.Add(item);
                                    lstChuaTL.Add(item);
                                }
                            }
                            else
                            {
                                if(lstChuaTLPage.Count < NumberPerPage)
                                    lstChuaTLPage.Add(item);
                                lstChuaTL.Add(item);
                            }
                        }
                        if (lstChuaTL.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                        else DpPhanTRang.Visibility = Visibility.Visible;
                        TongSoTrang = lstChuaTL.Count / NumberPerPage;
                        SoDu = NumberPerPage - (lstChuaTL.Count % NumberPerPage);
                        if (lstChuaTL.Count % NumberPerPage > 0)
                        {
                            TongSoTrang++;
                        }
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
                        if (TongSoTrang > 2)
                        {
                            borPage2.Visibility = Visibility.Visible;
                            borPage3.Visibility = Visibility.Visible;
                        }
                        else if (TongSoTrang > 1)
                        {
                            borPage2.Visibility = Visibility.Visible;
                            borPage3.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            borPage2.Visibility = Visibility.Collapsed;
                            borPage3.Visibility = Visibility.Collapsed;
                            borPageCuoi.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                        }
                        if (TongSoTrang > 1)
                        {
                            borPageCuoi.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                        }
                        PageNumberCurrent = 1;
                        lsvLoadSaffs.ItemsSource = lstChuaTLPage;
                        lsvLoadSaffs.Items.Refresh();
                    }
                }
                loading.Visibility = Visibility.Collapsed;
            }
            catch
            {
                loading.Visibility = Visibility.Collapsed;
            }
        }

        private void LoadDLNhanVien()
        {
            lstNhanVien = Main.lstNhanVienThuocCongTy;

            lsvNhanVien.ItemsSource = lstNhanVien;
            //lsvNhanVien.Items.Add("Tất cả nhân viên");
            //lsvNhanVien.Items.Add("(111788) Đỗ Văn Hoàng");
            //lsvNhanVien.Items.Add("(90229) Nguyễn Công Tiến");
        }

        private void LoadDLThang()
        {
            List<string> lstThang = new List<string>();
            lstThang.Add("Tháng 1");
            lstThang.Add("Tháng 2");
            lstThang.Add("Tháng 3");
            lstThang.Add("Tháng 4");
            lstThang.Add("Tháng 5");
            lstThang.Add("Tháng 6");
            lstThang.Add("Tháng 7");
            lstThang.Add("Tháng 8");
            lstThang.Add("Tháng 9");
            lstThang.Add("Tháng 10");
            lstThang.Add("Tháng 11");
            lstThang.Add("Tháng 12");
            lsvThang.ItemsSource = lstThang;
            lsvThang.PlaceHolder = lstThang[DateTime.Now.Month - 1];
        }

        private void LoadDLNam()
        {
            List<string> lstNam = new List<string>();
            lstNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
            lstNam.Add("Năm " + DateTime.Now.Year);
            lstNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
            lsvNam.ItemsSource = lstNam;
            lsvNam.PlaceHolder = lstNam[1];
        }

        private void bodSetupInsurance_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal data = (sender as Border).DataContext as OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal;
            if (data != null)
            {
                ucThietLapBHNhanVien ucs = new ucThietLapBHNhanVien(Main, data);
                Main.grShowPopup.Children.Add(ucs);
            }
        }

        private void lsvLoadSaffs_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void bodSetupInsurance_MouseEnter(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void bodSetupInsurance_MouseLeave(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void lsvLoadSaffs_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            LoadDLNguoiChuaThietLap();
        }

        
        private void Staff_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            if (lsvNhanVien.SelectedItem != null)
            {
                lsvNhanVien.PlaceHolderForground = "#474747";
            }
            else
            {
                lsvNhanVien.PlaceHolderForground = "#ACACAC";
            }
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
            if(TongSoTrang > 2)
            {
                borPage2.Visibility = Visibility.Visible;
                borPage3.Visibility = Visibility.Visible;
            }
            else if(TongSoTrang > 1)
            {
                borPage2.Visibility = Visibility.Visible;
                borPage3.Visibility = Visibility.Collapsed;
            }
            else
            {
                borPage2.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
            }
            if (TongSoTrang > 1)
            {
                borPageCuoi.Visibility = Visibility.Visible;
                borLen1.Visibility = Visibility.Visible;
            }
            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
            for (int i = 0; i < NumberPerPage; i++)
            {
                lstChuaTLPage.Add(lstChuaTL[i]);
            }
            //lstLuongCB = luongCB.listResult;
            lsvLoadSaffs.ItemsSource = lstChuaTLPage;
            PageNumberCurrent = 1;
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if(PageNumberCurrent > 2)
            {
                textPage1.Text = (PageNumberCurrent - 2).ToString();
                textPage2.Text = (PageNumberCurrent - 1).ToString();
                textPage3.Text = PageNumberCurrent.ToString();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            }
            else if(TongSoTrang > 2)
            {
                textPage1.Text = (PageNumberCurrent - 1).ToString();
                textPage2.Text = (PageNumberCurrent).ToString();
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageDau.Visibility = Visibility.Collapsed;
                borLui1.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPage1.Text = "1";
                textPage2.Text = "2";
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageDau.Visibility = Visibility.Collapsed;
                borLui1.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
            }
            borPageCuoi.Visibility = Visibility.Visible;
            borLen1.Visibility = Visibility.Visible;
            PageNumberCurrent--;
            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
            for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
            {
                lstChuaTLPage.Add(lstChuaTL[i]);
            }
            lsvLoadSaffs.ItemsSource = lstChuaTLPage;
        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrushConverter brus = new BrushConverter();
                if (textPage1.Text != PageNumberCurrent.ToString() && int.Parse(textPage1.Text) == PageNumberCurrent - 1)
                {
                    if (PageNumberCurrent > 2)
                    {
                        textPage1.Text = (PageNumberCurrent - 2).ToString();
                        textPage2.Text = (PageNumberCurrent - 1).ToString();
                        textPage3.Text = PageNumberCurrent.ToString();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    }
                    else if (TongSoTrang > 2)
                    {
                        textPage1.Text = (PageNumberCurrent - 1).ToString();
                        textPage2.Text = (PageNumberCurrent).ToString();
                        textPage3.Text = (PageNumberCurrent + 1).ToString();
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        textPage1.Text = "1";
                        textPage2.Text = "2";
                        textPage3.Text = (PageNumberCurrent + 1).ToString();
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage3.Visibility = Visibility.Collapsed;
                    }
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                    PageNumberCurrent--;
                    lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
                    for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                    {
                        lstChuaTLPage.Add(lstChuaTL[i]);
                    }
                    lsvLoadSaffs.ItemsSource = lstChuaTLPage;
                }
                else
                {
                    if (textPage1.Text != PageNumberCurrent.ToString())
                    {
                        if (PageNumberCurrent == 3)
                        {
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
                            if (TongSoTrang > 2)
                            {
                                borPage2.Visibility = Visibility.Visible;
                                borPage3.Visibility = Visibility.Visible;
                            }
                            else if (TongSoTrang > 1)
                            {
                                borPage2.Visibility = Visibility.Visible;
                                borPage3.Visibility = Visibility.Collapsed;
                            }
                            else
                            {
                                borPage2.Visibility = Visibility.Collapsed;
                                borPage3.Visibility = Visibility.Collapsed;
                                borPageCuoi.Visibility = Visibility.Collapsed;
                                borLen1.Visibility = Visibility.Collapsed;
                            }
                            if (TongSoTrang > 1)
                            {
                                borPageCuoi.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                            }
                            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
                            for (int i = 0; i < NumberPerPage; i++)
                            {
                                lstChuaTLPage.Add(lstChuaTL[i]);
                            }
                            lsvLoadSaffs.ItemsSource = lstChuaTLPage;
                            PageNumberCurrent = 1;
                        }
                        else
                        {
                            textPage1.Text = (PageNumberCurrent - 3).ToString();
                            textPage2.Text = (PageNumberCurrent - 2).ToString();
                            textPage3.Text = (PageNumberCurrent - 1).ToString();
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPageCuoi.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageDau.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                            PageNumberCurrent -= 2;
                            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
                            if (lstChuaTL.Count > 10)
                            {
                                for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
                                {
                                    lstChuaTLPage.Add(lstChuaTL[i]);
                                }
                                lsvLoadSaffs.ItemsSource = lstChuaTLPage;
                            }
                        }
                    }
                }
            }
            catch { }

        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrushConverter brus = new BrushConverter();
                if(PageNumberCurrent.ToString() != textPage2.Text)
                {
                    PageNumberCurrent = int.Parse(textPage2.Text);
                    if(TongSoTrang >= 3)
                    {
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageDau.Visibility = Visibility.Visible;
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Visibility = Visibility.Collapsed;
                        borPageCuoi.Visibility = Visibility.Collapsed;
                        borLen1.Visibility = Visibility.Collapsed;
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                    }
                }
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
                if (lstChuaTL.Count > NumberPerPage)
                {   
                    for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
                    {
                        lstChuaTLPage.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    lsvLoadSaffs.ItemsSource = lstChuaTLPage;
                }
            }
            catch
            {

            }

        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (PageNumberCurrent != TongSoTrang)
                {
                    if (PageNumberCurrent.ToString() != textPage3.Text && PageNumberCurrent > int.Parse(textPage3.Text) - 2)
                    {
                        if (PageNumberCurrent < TongSoTrang - 1)
                        {
                            textPage1.Text = PageNumberCurrent.ToString();
                            textPage2.Text = (PageNumberCurrent + 1).ToString();
                            textPage3.Text = (PageNumberCurrent + 2).ToString();
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        }
                        else if (TongSoTrang >= 3)
                        {
                            textPage1.Text = (PageNumberCurrent - 1).ToString();
                            textPage2.Text = (PageNumberCurrent).ToString();
                            textPage3.Text = (PageNumberCurrent + 1).ToString();
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPageCuoi.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            textPage1.Text = "1";
                            textPage2.Text = "2";
                            textPage3.Text = (PageNumberCurrent + 1).ToString();
                            BrushConverter brus = new BrushConverter();
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPageCuoi.Visibility = Visibility.Collapsed;
                            borLen1.Visibility = Visibility.Collapsed;
                            borPage3.Visibility = Visibility.Collapsed;
                        }
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;
                        PageNumberCurrent++;
                        lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
                        for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                        {
                            lstChuaTLPage.Add(lstChuaTL[i]);
                        }
                        lsvLoadSaffs.ItemsSource = lstChuaTLPage;
                    }
                    else
                    {
                        if (TongSoTrang == 3)
                        {
                            textPage3.Text = TongSoTrang.ToString();
                            textPage2.Text = (TongSoTrang - 1).ToString();
                            textPage1.Text = (TongSoTrang - 2).ToString();
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
                            PageNumberCurrent = TongSoTrang;
                            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
                            for (int i = TongSoTrang * NumberPerPage - NumberPerPage; i < TongSoTrang * NumberPerPage - SoDu; i++)
                            {
                                lstChuaTLPage.Add(lstChuaTL[i]);
                            }
                            lsvLoadSaffs.ItemsSource = lstChuaTLPage;
                        }
                        else
                        {
                            BrushConverter brus = new BrushConverter();
                            textPage1.Text = (PageNumberCurrent + 1).ToString();
                            textPage2.Text = (PageNumberCurrent + 2).ToString();
                            textPage3.Text = (PageNumberCurrent + 3).ToString();
                            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                            borPageCuoi.Visibility = Visibility.Visible;
                            borLen1.Visibility = Visibility.Visible;
                            borPageDau.Visibility = Visibility.Visible;
                            borLui1.Visibility = Visibility.Visible;
                            PageNumberCurrent += 2;
                            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
                            if (lstChuaTL.Count > 10)
                            {
                                for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
                                {
                                    lstChuaTLPage.Add(lstChuaTL[i]);
                                }
                                lsvLoadSaffs.ItemsSource = lstChuaTLPage;
                            }
                        }
                    }
                }

            }
            catch { }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(PageNumberCurrent < TongSoTrang - 1)
            {
                textPage1.Text = PageNumberCurrent.ToString();
                textPage2.Text = (PageNumberCurrent + 1).ToString();
                textPage3.Text = (PageNumberCurrent + 2).ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            }
            else if (TongSoTrang >= 3)
            {
                textPage1.Text = (PageNumberCurrent - 1).ToString();
                textPage2.Text = (PageNumberCurrent).ToString();
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
            }
            else
            {
                textPage1.Text = "1";
                textPage2.Text = "2";
                textPage3.Text = (PageNumberCurrent + 1).ToString();
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPageCuoi.Visibility = Visibility.Collapsed;
                borLen1.Visibility = Visibility.Collapsed;
                borPage3.Visibility = Visibility.Collapsed;
            }
            borPageDau.Visibility = Visibility.Visible;
            borLui1.Visibility = Visibility.Visible;
            PageNumberCurrent++;
            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
            for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
            {
                lstChuaTLPage.Add(lstChuaTL[i]);
            }
            lsvLoadSaffs.ItemsSource = lstChuaTLPage;
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(TongSoTrang >= 3)
            {
                textPage3.Text = TongSoTrang.ToString();
                textPage2.Text = (TongSoTrang - 1).ToString();
                textPage1.Text = (TongSoTrang - 2).ToString();
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            }
            else if(TongSoTrang == 2)
            {
                textPage3.Text = TongSoTrang.ToString();
                textPage2.Text = "2";
                textPage1.Text = "1";
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                borPage3.Visibility = Visibility.Collapsed;
            }
            borPageCuoi.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            PageNumberCurrent = TongSoTrang;
            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal>();
            for (int i = TongSoTrang * NumberPerPage - NumberPerPage; i < TongSoTrang * NumberPerPage - SoDu; i++)
            {
                lstChuaTLPage.Add(lstChuaTL[i]);
            }
            //lstLuongCB = luongCB.listResult;
            lsvLoadSaffs.ItemsSource = lstChuaTLPage;
        }

        private void cboSelectNumberPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Main != null)
            {
                if (cboSelectNumberPerPage.SelectedIndex == 0) NumberPerPage = 10;
                if (cboSelectNumberPerPage.SelectedIndex == 1) NumberPerPage = 20;
                if (cboSelectNumberPerPage.SelectedIndex == 2) NumberPerPage = 50;
                if (cboSelectNumberPerPage.SelectedIndex == 3) NumberPerPage = 100;
                LoadDLNguoiChuaThietLap();
            }
        }
    }
}
