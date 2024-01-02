using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.CaiDatHoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Salarysettings;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.HoaHong
{
    /// <summary>
    /// Interaction logic for ucCaiDatHoaHong.xaml
    /// </summary>
    public partial class ucCaiDatHoaHong : UserControl
    {
        MainWindow Main;
        public ucCaiDatHoaHong(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadListHoaHong();
        }
        public List<ListThietLap> lstThietLap = new List<ListThietLap>();
        public async void LoadListHoaHong()
        {
            try
            {
                lstThietLap.Clear();
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
                        lstThietLap = hoaHong.listThietLap;
                    }
                }
            }
            catch (Exception)
            { }
        }
        public void btn_HoaHongDoanhThu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try{Main.grShowPopup.Children.Add(new ucCaiDatHoaHongDoanhThu(Main, this, lstThietLap));}catch (Exception){ }  
        }

        private void btn_HoaHongLoiNhuan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try { Main.grShowPopup.Children.Add(new ucCaiDatHoaHongLoiNhuan(Main, this, lstThietLap)); } catch (Exception) { }
        }

        private void btn_HoaHongViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try { Main.grShowPopup.Children.Add(new ucCaiDatHoaHongLePhiViTri(Main, this, lstThietLap)); } catch (Exception) { }
        }

        private void btn_HoaHongKeHoach_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try { Main.grShowPopup.Children.Add(new ucCaiDatHoaHongKeHoach(Main, this, lstThietLap)); } catch (Exception) { }
        }

        private void btn_HoaHongTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 21;
            ucListSalarySettings ucl = new ucListSalarySettings(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucHoaHong uc = new ucHoaHong(Main, Main.Back, lstThietLap);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text + " / " + "Hoa hồng tiền";
        }

        private void btn_ThemHoaHongDoanhThu_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 22;
            ucListSalarySettings ucl = new ucListSalarySettings(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucHoaHong uc = new ucHoaHong(Main, Main.Back, lstThietLap);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text + " / " + "Hoa hồng doanh thu";
        }

        private void btn_ThemHoaHongLoiNhuan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 23;
            ucListSalarySettings ucl = new ucListSalarySettings(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucHoaHong uc = new ucHoaHong(Main, Main.Back, lstThietLap);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text + " / " + "Hoa hồng lợi nhuận";
        }

        private void HoaHongViTri_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 24;
            ucListSalarySettings ucl = new ucListSalarySettings(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucHoaHong uc = new ucHoaHong(Main, Main.Back, lstThietLap);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text + " / " + "Hoa hồng lệ phí vị trí";
        }

        private void btn_ThemHoaHongKeHoch_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 25;
            ucListSalarySettings ucl = new ucListSalarySettings(Main);
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            ucHoaHong uc = new ucHoaHong(Main, Main.Back, lstThietLap);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
            Main.LableFunction.Visibility = Visibility.Visible;
            Main.txbLoadChamCong.Text = ucbodyhome.txbSalarySettings.Text + " / " + ucl.txbFunction09.Text + " / " + "Hoa hồng Kế hoạch";
        }
    }
}
