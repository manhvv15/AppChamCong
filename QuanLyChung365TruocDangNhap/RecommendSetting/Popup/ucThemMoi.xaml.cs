using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.Hr.Entities.AdministrationEntity.EmployeeManager;
using QuanLyChung365TruocDangNhap.RecommendSetting.OOP;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DonDeXuat.XetDuyetVaTheoDoi;

namespace QuanLyChung365TruocDangNhap.RecommendSetting.Popup
{
    /// <summary>
    /// Interaction logic for ucThemMoi.xaml
    /// </summary>
    public partial class ucThemMoi : UserControl
    {
        frmMain Main;
        int id;
        public ucThemMoi(frmMain main, int id)
        {
            InitializeComponent();
            Main = main;
            this.id = id;
            getDsTimeKH();
        }
        List<API_Time_KH.TimeDx> listAddDon = new List<API_Time_KH.TimeDx>();
        private async void getDsTimeKH(int pagenumber = 1)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3005/api/vanthu/setting/fetchTimeSetting");
                request.Headers.Add("authorization", "Bearer " + Main.Tokens);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_Time_KH.Data_TimeDx api = JsonConvert.DeserializeObject<API_Time_KH.Data_TimeDx>(responseContent);
                    lsvListDonAdd.ItemsSource = api.time_dx;           
                }
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Lỗi lấy danh sách thời gian có kế hoạch " + ex.Message);
            }

        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void iconClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private async void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            if (borlistDOn.Visibility == Visibility.Visible)
            {
                borlistDOn.Visibility = Visibility.Collapsed;
               
            }
            else
            {
                borlistDOn.Visibility = Visibility.Visible;
               
            }
        }

        private void lsvListDonAdd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
          //  huyVaLuu.Visibility = Visibility.Collapsed;
            if (lsvListDonAdd.SelectedItem != null)
            {
                string selectedUserName = ((API_Time_KH.TimeDx)lsvListDonAdd.SelectedItem).name_cate_dx;
                if (!listAddDon.Any(item => item.name_cate_dx == selectedUserName))
                {
                    API_Time_KH.TimeDx infor = new API_Time_KH.TimeDx()
                    {
                        name_cate_dx = ((API_Time_KH.TimeDx)lsvListDonAdd.SelectedItem).name_cate_dx
                   

                    };

                    listAddDon.Add(infor);
                   // lsvListDon = ListXet.ToList();
                    lsvListDon.ItemsSource = listAddDon;
                    lsvListDon.Items.Refresh();
                }
            }
            borlistDOn.Visibility = Visibility.Collapsed;
            //huyVaLuu.Visibility = Visibility.Visible;
        }

        private void xoaAnh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            API_Time_KH.TimeDx infor = ((API_Time_KH.TimeDx)lsvListDon.SelectedItem);


            if (infor != null)
            {
                listAddDon.Remove(infor);
                lsvListDon.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvListDon.ItemsSource = listAddDon;
                
            }
        }
    }
}
