using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.HoaHong;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.CaiDatHoaHong
{
    /// <summary>
    /// Interaction logic for ucCaiDatHoaHongLePhiViTri.xaml
    /// </summary>
    public partial class ucCaiDatHoaHongLePhiViTri : UserControl
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
        List<ListThietLap> lstRose4 = new List<ListThietLap>();
        CollectionViewSource view = new CollectionViewSource();
        List<ListThietLap> lstLoadRose4 = new List<ListThietLap>();
        List<ListThietLap> lstLoadRose4CPT = new List<ListThietLap>();
        public ucCaiDatHoaHongLePhiViTri(MainWindow main, ucCaiDatHoaHong ucSRose, List<ListThietLap> lstrose4)
        {
            InitializeComponent();
            Main = main;
            ucSettingRose = ucSRose;
            lstRose4 = lstrose4;
            LoadHHViTri();
        }

        public async void LoadHHViTri()
        {
            try
            {
                lstLoadRose4.Clear();
                lstLoadRose4CPT.Clear();
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
                            if (item.tl_id_rose == 4)
                            {
                                lstLoadRose4.Add(item);
                            }
                            for (int i = 0; i < lstLoadRose4.Count; i++)
                            {
                                lstLoadRose4[i].stt = i + 1;
                            }
                        }
                        if (lstLoadRose4.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                        else DpPhanTRang.Visibility = Visibility.Visible;
                        TongSoTrang = lstLoadRose4.Count / 10;
                        SoDu = 10 - (lstLoadRose4.Count % 10);
                        if (lstLoadRose4.Count % 10 > 0)
                        {
                            TongSoTrang++;
                        }
                        for (int i = 0; i < 10 && i < lstLoadRose4.Count; i++)
                        {
                            lstLoadRose4CPT.Add(lstLoadRose4[i]);
                        }
                        dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
                        dgvHoaHongViTri.Items.Refresh();
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
                if (lstLoadRose4.Count != 0)
                {
                    lstLoadRose4CPT = new List<ListThietLap>();
                    for (int i = 0; i < 10; i++)
                    {
                        lstLoadRose4CPT.Add(lstLoadRose4[i]);
                    }
                    dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
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
                lstLoadRose4CPT = new List<ListThietLap>();
                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose4.Count; i++)
                {
                    lstLoadRose4CPT.Add(lstLoadRose4[i]);
                }
                dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
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
                    lstLoadRose4CPT = new List<ListThietLap>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose4.Count; i++)
                    {
                        lstLoadRose4CPT.Add(lstLoadRose4[i]);
                    }
                    dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
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

                        lstLoadRose4CPT = new List<ListThietLap>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstLoadRose4CPT.Add(lstLoadRose4[i]);
                        }
                        dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
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
                        lstLoadRose4CPT = new List<ListThietLap>();
                        if (lstLoadRose4.Count > 10)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose4.Count; i++)
                            {
                                lstLoadRose4CPT.Add(lstLoadRose4[i]);
                            }
                            dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
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
                lstLoadRose4CPT = new List<ListThietLap>();
                if (lstLoadRose4.Count > 10)
                {
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose4.Count; i++)
                    {
                        lstLoadRose4CPT.Add(lstLoadRose4[i]);
                    }
                    dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT; ;
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
                    lstLoadRose4CPT = new List<ListThietLap>();
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose4.Count; i++)
                    {
                        lstLoadRose4CPT.Add(lstLoadRose4[i]);
                    }
                    dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
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
                        lstLoadRose4CPT = new List<ListThietLap>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstLoadRose4CPT.Add(lstLoadRose4[i]);
                        }
                        dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
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
                        lstLoadRose4CPT = new List<ListThietLap>();
                        if (lstLoadRose4.Count > 10)
                        {
                            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose4.Count; i++)
                            {
                                lstLoadRose4CPT.Add(lstLoadRose4[i]);
                            }
                            dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
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
                lstLoadRose4CPT = new List<ListThietLap>();
                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstLoadRose4.Count; i++)
                {
                    lstLoadRose4CPT.Add(lstLoadRose4[i]);
                }
                dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
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
                lstLoadRose4CPT = new List<ListThietLap>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstLoadRose4CPT.Add(lstLoadRose4[i]);
                }
                dgvHoaHongViTri.ItemsSource = lstLoadRose4CPT;
            }
            catch (Exception)
            { }
        }
        #endregion
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btn_Exit_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btn_SuaHHViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListThietLap lstLoadRose4 = (sender as Border).DataContext as ListThietLap;
                if (lstLoadRose4 != null)
                {
                    Main.grShowPopup.Children.Add(new ucThemMoiCacLoaiHoaHong(Main, lstLoadRose4.tl_hoahong, lstLoadRose4.tl_name, lstLoadRose4.tl_id.ToString(), ucSettingRose, this));
                }
            }
            catch (System.Exception)
            { }
        }

        private void btn_XoaHHViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListThietLap lstLoadRose4 = (sender as Border).DataContext as ListThietLap;
                if (lstLoadRose4 != null)
                {
                    Main.grShowPopup.Children.Add(new ucXacNhanXoa(Main, lstLoadRose4, null, null, this, null, ucSettingRose));
                }
            }
            catch (System.Exception)
            { }
        }

        private void btn_ThemMoiHHLePhiViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemMoiCacLoaiHoaHong(Main, this, ucSettingRose));
        }

        private void dgvCongCong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
    }
}
