using QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.AddNewStaffTabList;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager.AddNewStaffPopups;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Them_Xoa_NhanVien;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy
{
    /// <summary>
    /// Interaction logic for ucInstallAddNewStaff.xaml
    /// </summary>
    public partial class ucInstallAddNewStaff : UserControl
    {
        MainWindow Main;
        public ucInstallAddNewStaff(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDanhSachTatCaNhanVienChoDuyet();
            LoadDanhSachTatCaNhanVien();
        }
        public async void LoadDanhSachTatCaNhanVienChoDuyet()
        {
            try
            {

                var searchObject = new
                {
                    ep_status = "Pending",
                    pageSize = 10000


                };
                string searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_DanhSachNhanVien);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new StringContent(searchJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resSaff = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Root_NhanVien resultSaff = JsonConvert.DeserializeObject<Root_NhanVien>(resSaff);

                    tb_CountPendingStaff.Text = "( " + resultSaff.data.total + " )";
                }
            }
            catch (Exception)
            {
            }
        }
        public async void LoadDanhSachTatCaNhanVien()
        {
            try
            {

                var searchObject = new
                {
                    ep_status = "Active",
                    pageSize = 10000


                };
                string searchJson = JsonConvert.SerializeObject(searchObject, Formatting.Indented);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_DanhSachNhanVien);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new StringContent(searchJson, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var resSaff = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    Root_NhanVien resultSaff = JsonConvert.DeserializeObject<Root_NhanVien>(resSaff);

                    tb_CountAllStaff.Text = "( " + resultSaff.data.total + " )";
                }
            }
            catch (Exception)
            {
            }
        }
        private void bodToanBo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            ucTatCaNhanVien uc = new ucTatCaNhanVien(this.Main, this);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);
        }

        private void bodChoDuyet_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            SetDefaultMenuColor();
            ChangeBorderColor((Border)sender);
            ucDanhSachChoDuyet uc = new ucDanhSachChoDuyet(Main, this);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);
        }


        private void stackTabBody_Loaded(object sender, RoutedEventArgs e)
        {
            ucTatCaNhanVien uc = new ucTatCaNhanVien(this.Main, this);
            stackTabBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stackTabBody.Children.Add(Content as UIElement);
        }
        ///<summary>
        /// Change color menu
        /// </summary>
        public void ChangeBorderColor(Border border)
        {
            border.BorderThickness = new Thickness(0, 0, 0, 5);
            border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
            ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        ///<summary>
        /// set default color menu
        /// </summary>
        public void SetDefaultMenuColor()
        {
            foreach (var child in GridMenu.Children)
            {
                if (child is Border)
                {
                    var border = (Border)child;
                    border.BorderThickness = new Thickness(0, 0, 0, 1);
                    border.BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
                    ((TextBlock)border.Child).Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));

                }
            }
        }

        private void bodAddStaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucAddNewStaff(Main, this));
        }
    }
}
