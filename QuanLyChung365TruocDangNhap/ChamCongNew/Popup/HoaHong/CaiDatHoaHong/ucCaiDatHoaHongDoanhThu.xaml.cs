using QuanLyChung365TruocDangNhap.ChamCongNew.Core;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CongCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatLichLamViec;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.PopupTimeKeeping;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.HoaHong;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using static QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.CoCau_ViTri_ToChuc.ucSoDoToChuc;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.CaiDatHoaHong
{
    /// <summary>
    /// Interaction logic for ucDanhSachCaiDatHoaHong.xaml
    /// </summary>
    public partial class ucCaiDatHoaHongDoanhThu : UserControl
    {
        MainWindow Main;
        int currentPageIndex = 0;
        int itemPerPage = 10;
        int totalPage = 0;
        private int TongSoTrang = 0;
        private int SoDu = 0;
        private int PageNumberCurrent = 1;
        BrushConverter br = new BrushConverter();
        BrushConverter brus = new BrushConverter();
        private ucCaiDatHoaHong ucSettingRose;
        public List<ListThietLap> lstRose2 = new List<ListThietLap>();
        List<ListThietLap> lstLoadRose2 = new List<ListThietLap>();
        List<ListThietLap> lstLoadRose2CPT = new List<ListThietLap>();

        public ucCaiDatHoaHongDoanhThu(MainWindow main, ucCaiDatHoaHong ucSRose, List<ListThietLap> lstrose2)
        {
            InitializeComponent();
            Main = main;
            ucSettingRose = ucSRose;
            //lstRose2 = lstrose2;
            LoadDoanhThu();
        }
        public async void LoadDoanhThu()
        {
            try
            {
                lstLoadRose2.Clear();
                lstLoadRose2CPT.Clear();
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/take_thiet_lap_com");
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(Main.IdAcount.ToString()), "tl_id_com");
                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resConten = await response.Content.ReadAsStringAsync();
                    Root_HoaHong hoaHong = JsonConvert.DeserializeObject<Root_HoaHong>(resConten);
                    if (hoaHong != null)
                    {
                        foreach (var item in hoaHong.listThietLap)
                        {
                            if (item.tl_id_rose == 2)
                            {
                                lstLoadRose2.Add(item);
                            }
                        }
                        for (int i = 0; i < lstLoadRose2.Count; i++)
                        {
                            lstLoadRose2[i].stt = i + 1;
                        }
                        if (lstLoadRose2.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                        else DpPhanTRang.Visibility = Visibility.Visible;
                        TongSoTrang = lstLoadRose2.Count / 10;
                        SoDu = 10 - (lstLoadRose2.Count % 10);
                        if (lstLoadRose2.Count % 10 > 0)
                        {
                            TongSoTrang++;
                        }
                        for (int i = 0; i < 10 && i < lstLoadRose2.Count; i++)
                        {
                            lstLoadRose2CPT.Add(lstLoadRose2[i]);
                        }
                        dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
                        dgvHoaHongDoanhThu.Items.Refresh();
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
                }
            }
            catch (Exception)
            { }

        }
        #region PhanTrang

        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                if (lstLoadRose2.Count != 0)
                {
                    lstLoadRose2CPT = new List<ListThietLap>();
                    for (int i = 0; i < 10; i++)
                    {
                        lstLoadRose2CPT.Add(lstLoadRose2[i]);
                    }
                    dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
                }
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
                lstLoadRose2CPT = new List<ListThietLap>();
                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose2.Count; i++)
                {
                    lstLoadRose2CPT.Add(lstLoadRose2[i]);
                }
                dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
            }
            catch (Exception)
            {
            }
        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
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
                    lstLoadRose2CPT = new List<ListThietLap>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose2.Count; i++)
                    {
                        lstLoadRose2CPT.Add(lstLoadRose2[i]);
                    }
                    dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
                }
                else
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

                        lstLoadRose2CPT = new List<ListThietLap>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstLoadRose2CPT.Add(lstLoadRose2[i]);
                        }
                        dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
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
                        lstLoadRose2CPT = new List<ListThietLap>();
                        if (lstLoadRose2.Count > 10)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose2.Count; i++)
                            {
                                lstLoadRose2CPT.Add(lstLoadRose2[i]);
                            }
                            dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                BrushConverter brus = new BrushConverter();
                if (PageNumberCurrent.ToString() != textPage2.Text)
                {
                    PageNumberCurrent = int.Parse(textPage2.Text);
                    if (TongSoTrang >= 3)
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
                lstLoadRose2CPT = new List<ListThietLap>();
                if (lstLoadRose2.Count > 10)
                {
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose2.Count; i++)
                    {
                        lstLoadRose2CPT.Add(lstLoadRose2[i]);
                    }
                    dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT; ;
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
                    lstLoadRose2CPT = new List<ListThietLap>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose2.Count; i++)
                    {
                        lstLoadRose2CPT.Add(lstLoadRose2[i]);
                    }
                    dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
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
                        lstLoadRose2CPT = new List<ListThietLap>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstLoadRose2CPT.Add(lstLoadRose2[i]);
                        }
                        dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
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
                        lstLoadRose2CPT = new List<ListThietLap>();
                        if (lstLoadRose2.Count > 10)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose2.Count; i++)
                            {
                                lstLoadRose2CPT.Add(lstLoadRose2[i]);
                            }
                            dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
                        }
                    }
                }
            }
            catch (Exception)
            {
            }
        }

        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
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
                lstLoadRose2CPT = new List<ListThietLap>();
                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose2.Count; i++)
                {
                    lstLoadRose2CPT.Add(lstLoadRose2[i]);
                }
                dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
            }
            catch (Exception)
            {
            }
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (TongSoTrang >= 3)
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
                else if (TongSoTrang == 2)
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
                lstLoadRose2CPT = new List<ListThietLap>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstLoadRose2CPT.Add(lstLoadRose2[i]);
                }
                dgvHoaHongDoanhThu.ItemsSource = lstLoadRose2CPT;
            }
            catch (Exception)
            { }
        }
        //private void view_Filter(object sender, FilterEventArgs e)
        //{
        //    int index = lstLoadRose2.IndexOf((ListThietLap)e.Item);
        //    if (index >= itemPerPage * currentPageIndex && index < itemPerPage * (currentPageIndex + 1))
        //    {
        //        e.Accepted = true;
        //    }
        //    else
        //    {
        //        e.Accepted = false;
        //    }
        //}
        //private void ShowCurrentPageIndex()
        //{
        //    this.tbCurrentPage.Text = (currentPageIndex + 1).ToString();
        //}
        //private void btnFirst_Click(object sender, MouseButtonEventArgs e)
        //{
        //    if (currentPageIndex != 0)
        //    {
        //        currentPageIndex = 0;
        //        view.View.Refresh();
        //    }
        //    ShowCurrentPageIndex();
        //}
        //private void btnBackPage_Click(object sender, MouseButtonEventArgs e)
        //{
        //    if (currentPageIndex > 0)
        //    {
        //        currentPageIndex--;
        //        view.View.Refresh();
        //    }
        //    ShowCurrentPageIndex();
        //}
        //private void btnNextPage_Click(object sender, MouseButtonEventArgs e)
        //{
        //    if (currentPageIndex < totalPage - 1)
        //    {
        //        currentPageIndex++;
        //        view.View.Refresh();
        //    }
        //    ShowCurrentPageIndex();
        //}
        //private void btnTrangCuoi_Click(object sender, MouseButtonEventArgs e)
        //{
        //    if (currentPageIndex != totalPage - 1)
        //    {
        //        currentPageIndex = totalPage - 1;
        //        view.View.Refresh();
        //    }
        //    ShowCurrentPageIndex();
        //}

        //private void btnTrangDau_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    btnTrangDau.Background = (Brush)br.ConvertFrom("#4C5BD4");
        //    tbTrangDau.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        //}

        //private void btnTrangDau_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    btnTrangDau.Background = (Brush)br.ConvertFrom("#FFFFFF");
        //    tbTrangDau.Foreground = (Brush)br.ConvertFrom("#474747");
        //}

        //private void btnBackPage_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    btnBackPage.Background = (Brush)br.ConvertFrom("#4C5BD4");
        //    tbBackPage.Fill = (Brush)br.ConvertFrom("#FFFFFF");
        //}

        //private void btnBackPage_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    btnBackPage.Background = (Brush)br.ConvertFrom("#FFFFFF");
        //    tbBackPage.Fill = (Brush)br.ConvertFrom("#474747");
        //}

        //private void btnNextPage_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    btnNextPage.Background = (Brush)br.ConvertFrom("#4C5BD4");
        //    tbNextPage.Fill = (Brush)br.ConvertFrom("#FFFFFF");
        //}

        //private void btnNextPage_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    btnNextPage.Background = (Brush)br.ConvertFrom("#FFFFFF");
        //    tbNextPage.Fill = (Brush)br.ConvertFrom("#474747");
        //}

        //private void btnTrangCuoi_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    btnTrangCuoi.Background = (Brush)br.ConvertFrom("#4C5BD4");
        //    tbTrangCuoi.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        //}

        //private void btnTrangCuoi_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    btnTrangCuoi.Background = (Brush)br.ConvertFrom("#FFFFFF");
        //    tbTrangCuoi.Foreground = (Brush)br.ConvertFrom("#474747");
        //}

        #endregion
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this .Visibility = Visibility.Collapsed;
        }

        private void btn_Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btn_ChinhSuaHHDoanhThu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListThietLap lstLoadRose2 = (sender as Border).DataContext as ListThietLap;
                if (lstLoadRose2 != null)
                {
                    Main.grShowPopup.Children.Add(new ucThemMoiCacLoaiHoaHong(Main, lstLoadRose2, ucSettingRose, this));
                }
            }
            catch (System.Exception)
            { }
        }

        private void btn_XoaHHDoanhThu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListThietLap lstLoadRose2 = (sender as Border).DataContext as ListThietLap;
                if (lstLoadRose2 != null)
                {
                    Main.grShowPopup.Children.Add(new ucXacNhanXoa(Main, lstLoadRose2, this, null, null, null, ucSettingRose));
                }
            }
            catch (System.Exception)
            {}
           
        }

        private void btn_ThemMoiHoaHongDoanhThu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiCacLoaiHoaHong(Main, this, ucSettingRose));
        }

        private void dgvCongCong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Main.keyDown != Key.LeftShift && Main.keyDown != Key.RightShift)
                Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
            else
                dgvHoaHongDoanhThu.GetFirstChildOfType<ScrollViewer>().ScrollToHorizontalOffset(dgvHoaHongDoanhThu.GetFirstChildOfType<ScrollViewer>().HorizontalOffset - e.Delta);
        }
        public Key keyDown { get; set; }
        private void ucSalary_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.LeftShift || e.Key == Key.RightShift)
            {
                keyDown = e.Key;
            }
        }

        private void ucSalary_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == keyDown)
            {
                keyDown = Key.Cancel;
            }
        }
    }
}
