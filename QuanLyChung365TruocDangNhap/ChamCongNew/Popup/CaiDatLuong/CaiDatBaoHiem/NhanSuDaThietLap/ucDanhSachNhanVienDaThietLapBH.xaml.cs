using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.BaoHiem;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.PopupSalarySettings;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.clsNhanVienThuocCongTy;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ListTabInsurance
{
    /// <summary>
    /// Interaction logic for ucListSaffYesSettings.xaml
    /// </summary>
    public partial class ucDanhSachNhanVienDaThietLapBH : UserControl
    {
        List<InsuranceSafff> itemsis = new List<InsuranceSafff>();
        public MainWindow Main;
        public List<OOP.clsPhongBanThuocCongTy.Item> lstPhongBan = new List<OOP.clsPhongBanThuocCongTy.Item>();
        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        List<OOP.clsNhanVienThuocCongTy.ListUser> lstSearchNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> lstChuaTL = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
        public List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> lstChuaTLPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> lstChuaTLFilter = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
        private List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU> lstChuaTLFilterPT = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
        private string IdNV = "0";
        private string IdPB = "0";
        private int TongSoTrang = 0;
        private int PageNumberCurrent = 1;
        private int SoDu = 0;
        public int NumberPerPage = 10;
        public ucDanhSachNhanVienDaThietLapBH(MainWindow main)
        {
            InitializeComponent();
            
            Main = main;
            LoadDLNam();
            LoadDLThang();
            LoadDLNhanVien();
            LoadDLNSuDaTL();
        }

        public void LoadDLNSuDaTL()
        {

            try
            {
                using (WebClient request = new WebClient())
                {
                    loading.Visibility = Visibility.Visible;
                    lstChuaTLPage.Clear();
                    lstChuaTL.Clear();
                    request.QueryString.Add("cls_id_com", Main.IdAcount.ToString());
                    int month, year;
                    if (lsvThang.SelectedItem != null)
                        month = int.Parse(lsvThang.SelectedItem.ToString().Split(' ')[1]);
                    else month = DateTime.Now.Month;
                    if(lsvNam.SelectedItem != null) year = int.Parse(lsvNam.SelectedItem.ToString().Split(' ')[1]);
                    else year = DateTime.Now.Year;
                    if (month > 10)
                    {
                        request.QueryString.Add("start_date", year + "-" + month + "-01T00:00:00.000+00:00");
                        request.QueryString.Add("end_date", year + "-" + month + "-" + DateTime.DaysInMonth(year, month) + "T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.QueryString.Add("start_date", year + "-0" + month + "-01T00:00:00.000+00:00");
                        request.QueryString.Add("end_date", year + "-0" + month + "-" + DateTime.DaysInMonth(year, month) + "T00:00:00.000+00:00");
                    }
                    request.QueryString.Add("token", Properties.Settings.Default.Token);
                    request.UploadValuesCompleted += (s, e) =>
                    {
                        OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.Root>(UnicodeEncoding.UTF8.GetString(e.Result));
                        if (chuaTL.list_us != null)
                        {
                            if (lsvNhanVien.SelectedItem != null && lsvNhanVien.SelectedIndex != 0)
                            {
                                foreach (var item in chuaTL.list_us)
                                {
                                    if (((OOP.clsNhanVienThuocCongTy.ListUser)lsvNhanVien.SelectedItem).idQLC == item.idQLC)
                                    {
                                        lstChuaTL.Add(item);
                                        if (lstChuaTLPage.Count < NumberPerPage)
                                            lstChuaTLPage.Add(item);
                                    }
                                    if (string.IsNullOrEmpty(item.Detail.avatarUser)) item.Detail.avatarUser = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=64&q=75";
                                }
                            }
                            else
                            {
                                foreach (var item in chuaTL.list_us)
                                {
                                    if (lstChuaTLPage.Count < NumberPerPage)
                                        lstChuaTLPage.Add(item);
                                    if (string.IsNullOrEmpty(item.Detail.avatarUser)) item.Detail.avatarUser = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=64&q=75";
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
                            for (int i = 0; i < NumberPerPage && i < lstChuaTL.Count; i++)
                            {
                                lstChuaTLPage.Add(lstChuaTL[i]);
                            }
                            dgvCTLBH.ItemsSource = lstChuaTLPage;
                            dgvCTLBH.Items.Refresh();
                            if (TongSoTrang < 3)
                            {
                                if (TongSoTrang == 2)
                                {
                                    borPage3.Visibility = Visibility.Collapsed;
                                    borPage2.Visibility = Visibility.Visible;
                                    borLen1.Visibility = Visibility.Visible;
                                    borPageCuoi.Visibility = Visibility.Visible;
                                }
                                else if (TongSoTrang == 1)
                                {
                                    borPage2.Visibility = Visibility.Collapsed;
                                    borPage3.Visibility = Visibility.Collapsed;
                                    borLen1.Visibility = Visibility.Collapsed;
                                    borPageCuoi.Visibility = Visibility.Collapsed;
                                }
                            }
                            else
                            {
                                borLui1.Visibility = Visibility.Collapsed;
                                borPageDau.Visibility = Visibility.Collapsed;
                                borPage2.Visibility = Visibility.Visible;
                                borPage3.Visibility = Visibility.Visible;
                                borLen1.Visibility = Visibility.Visible;
                                borPageCuoi.Visibility = Visibility.Visible;
                            }
                        }
                        loading.Visibility = Visibility.Collapsed;
                    };
                    request.UploadValuesTaskAsync("http://210.245.108.202:3009/api/tinhluong/congty/show_list_user_insrc", request.QueryString);
                }
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
            lsvThang.PlaceHolder = "Tháng " + DateTime.Now.Month;
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

        List<ListUser> listSaftSearch = new List<ListUser>();
        private void Staff_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (lsvNhanVien.SelectedItem != null)
                {
                    lsvNhanVien.PlaceHolderForground = "#474747";
                    listSaftSearch = new List<ListUser>();
                    var chonca = ((ListUser)lsvNhanVien.SelectedItem).idQLC.ToString();
                    IdNV = chonca;
                    if (!Main.lstNhanVienThuocCongTy.Any(item => item.idQLC.ToString() == chonca))
                    {
                        listSaftSearch = Main.lstNhanVienThuocCongTy.ToList();
                        foreach (var item in listSaftSearch)
                        {
                            Main.NhanVien = item.userName;
                            IdNV = item.idQLC.ToString();
                        }
                    }
                }
                else
                {
                    lsvNhanVien.PlaceHolderForground = "#ACACAC";
                }
            }
            catch (Exception)
            {

                //if (lsvNhanVien.SelectedItem != null)
                //{
                //    lsvNhanVien.PlaceHolderForground = "#474747";
                //}
                //else
                //{
                //    lsvNhanVien.PlaceHolderForground = "#ACACAC";
                //}
            }
        }

            private void bodDleteSaffYes_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU data = (OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU)(sender as Border).DataContext;
            Main.grShowPopup.Children.Add(new ucXoaBaoHiemNhanSu(data.cls_id, this));
        }


        private void bodThongKe_MouseEnter(object sender, MouseEventArgs e)
        {
            bodThongKe.BorderThickness = new Thickness(1);
        }

        private void bodThongKe_MouseLeave(object sender, MouseEventArgs e)
        {
            bodThongKe.BorderThickness = new Thickness(0);
        }

        private void lsvListSaffYes_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void bodThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                lstChuaTLFilter.Clear();
                if (lsvNhanVien.SelectedItem != null && ((ListUser)lsvNhanVien.SelectedItem)._id > 0)
                {
                    foreach (var item in lstChuaTL)
                    {
                        if (IdNV == item.cls_id_user.ToString())
                        {
                            lstChuaTLFilter.Add(item);
                        }
                        if (lstChuaTLFilter.Count < 10)
                        {
                            DpPhanTRang.Visibility = Visibility.Collapsed;
                        }
                        dgvCTLBH.ItemsSource = lstChuaTLFilter;
                        dgvCTLBH.Items.Refresh();
                    }
                }
                else
                {
                    LoadDLNSuDaTL();
                }
            }
            catch
            {}
        }
        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = 0; i < NumberPerPage; i++)
                {
                    lstChuaTLPage.Add(lstChuaTL[i]);
                }
                dgvCTLBH.ItemsSource = lstChuaTLPage;
                PageNumberCurrent = 1;
            }
            catch (Exception)
            {
            }
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrushConverter brus = new BrushConverter();
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
                lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
                {
                    lstChuaTLPage.Add(lstChuaTL[i]);
                }
                dgvCTLBH.ItemsSource = lstChuaTLPage;
            }
            catch (Exception)
            {
            }
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
                    lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
                    {
                        lstChuaTLPage.Add(lstChuaTL[i]);
                    }
                    dgvCTLBH.ItemsSource = lstChuaTLPage;
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
                            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                            for (int i = 0; i < 10; i++)
                            {
                                lstChuaTLPage.Add(lstChuaTL[i]);
                            }
                            //lstLuongCB = luongCB.listResult;
                            dgvCTLBH.ItemsSource = lstChuaTLPage;
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
                            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                            if (lstChuaTL.Count > 10)
                            {
                                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstChuaTL.Count; i++)
                                {
                                    lstChuaTLPage.Add(lstChuaTLPage[i]);
                                }
                                dgvCTLBH.ItemsSource = lstChuaTLPage;
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
                lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                if (lstChuaTL.Count > NumberPerPage)
                {   
                    for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
                    {
                        lstChuaTLPage.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvCTLBH.ItemsSource = lstChuaTLPage;
                }
            }
            catch
            {

            }

        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
                lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
                {
                    lstChuaTLPage.Add(lstChuaTL[i]);
                }
                dgvCTLBH.ItemsSource = lstChuaTLPage;
            }
            else
            {
                if(TongSoTrang == 3)
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
                    lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    for (int i = TongSoTrang * NumberPerPage - NumberPerPage; i < TongSoTrang * NumberPerPage - SoDu; i++)
                    {
                        lstChuaTLPage.Add(lstChuaTL[i]);
                    }
                    dgvCTLBH.ItemsSource = lstChuaTLPage;
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
                    lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
                    if (lstChuaTL.Count > NumberPerPage)
                    {
                        for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
                        {
                            lstChuaTLPage.Add(lstChuaTL[i]);
                        }
                        dgvCTLBH.ItemsSource = lstChuaTLPage;
                    }
                }
            }
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
            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
            for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstChuaTL.Count; i++)
            {
                lstChuaTLPage.Add(lstChuaTL[i]);
            }
            dgvCTLBH.ItemsSource = lstChuaTLPage;
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
            lstChuaTLPage = new List<OOP.CaiDatLuong.BaoHiem.clsNSuDaThietLap.ListU>();
            for (int i = TongSoTrang * NumberPerPage - NumberPerPage; i < TongSoTrang * NumberPerPage - SoDu; i++)
            {
                lstChuaTLPage.Add(lstChuaTL[i]);
            }
            //lstLuongCB = luongCB.listResult;
            dgvCTLBH.ItemsSource = lstChuaTLPage;
        }

        private void cboSelectNumberPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Main != null)
            {
                if (cboSelectNumberPerPage.SelectedIndex == 0) NumberPerPage = 10;
                if (cboSelectNumberPerPage.SelectedIndex == 1) NumberPerPage = 20;
                if (cboSelectNumberPerPage.SelectedIndex == 2) NumberPerPage = 50;
                if (cboSelectNumberPerPage.SelectedIndex == 3) NumberPerPage = 100;
                LoadDLNSuDaTL();
            }
        }
    }
}
