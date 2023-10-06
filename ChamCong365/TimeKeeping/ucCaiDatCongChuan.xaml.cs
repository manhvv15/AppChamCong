using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ChamCong365.OOP.ChamCong.CaiDatCongChuan;
using ChamCong365.OOP.ChamCong.CaiDatLichLamViec;
using ChamCong365.Popup.ChamCong.CaiDatCongChuan;
using ChamCong365.TimeKeeping;
using Newtonsoft.Json;

namespace ChamCong365.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ucStandardInstallation.xaml
    /// </summary>
    ///   

    public partial class ucCaiDatCongChuan : UserControl
    {
        MainWindow Main;
        #region Chon Thang Nam
        int thang;
        public class Thang
        {
            public string thang { get; set; }
        }
        List<Thang> listThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        public class Nam
        {
            public string nam { get; set; }
        }
        List<Nam> listNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();

        private ObservableCollection<DataCalendar> calendarList;
        private ICollectionView calendarView;
        #endregion
        
        public ucCaiDatCongChuan(MainWindow main)
        {
            InitializeComponent();
            txbNumberStandard.Focus();
            Main = main;
            LoadDLNam();
            LoadDLThang();
            HienThiDSCongChuan();
        }
        #region Call API
        private ListCongChuan _lstCongChuan;

        public ListCongChuan LstCongChuan
        {
            get { return _lstCongChuan; }
            set
            {
                _lstCongChuan = value;
            }
        }

        private string cw_id;
        string month = "";
        string year = "";
        public async void HienThiDSCongChuan()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.list_CongChuan_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (txbSelectMonth.Text != null)
                {
                    month = txbSelectMonth.Text.Replace("Tháng ", "");
                }
                if (txbSelectYear.Text != null) 
                {
                    year = txbSelectYear.Text.Replace("Năm ", "");
                }
                content.Add(new StringContent(month), "month");
                content.Add(new StringContent(year), "year");
                request.Content = content;
                var response = await client.SendAsync(request);
             
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();

                    RootCongChuan result = JsonConvert.DeserializeObject<RootCongChuan>(responseContent);

                    if (result.data.data != null)
                    {
                        LstCongChuan = result.data.data;
                        cw_id = LstCongChuan.cw_id.ToString();
                        txbNumberStandard.Text = LstCongChuan.num_days.ToString();
                        txtValidateCongChuan.Visibility = Visibility.Hidden;

                    }
                }  
            }
            catch (Exception)
            {
                if (txtValidateCongChuan.Visibility == Visibility.Hidden)
                {
                    txtValidateCongChuan.Visibility = Visibility.Visible;
                    txbNumberStandard.Clear();
                    txtValidateCongChuan.Text = "Bạn chưa cài đặt số ngày công tiêu chuẩn trong tháng " + month + "/" + year;
                }
                else
                {
                    txbNumberStandard.Clear();
                    txtValidateCongChuan.Text = "Bạn chưa cài đặt số ngày công tiêu chuẩn trong tháng " + month + "/" + year;
                }
                
            }
        }
        public async void TaoCongChuan()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.Create_CongChuan_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(txbNumberStandard.Text), "num_days");
                
                content.Add(new StringContent(month), "month");
              
                content.Add(new StringContent(year), "year");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var Add_CongChuan = await response.Content.ReadAsStringAsync();

                    Root_Add_CongChuan result = JsonConvert.DeserializeObject<Root_Add_CongChuan>(Add_CongChuan);
                }

            }
            catch (Exception)
            {
            }
        }
        private void LoadCongChuan(object sender, SelectionChangedEventArgs e)
        {
            string year = "", month = "";
            if (lsvNam.SelectedItem != null)
                year = lsvNam.SelectedItem.ToString().Split(' ')[1];
            else
                year = DateTime.Now.ToString("yyyy");
            if (lsvThang.SelectedIndex != -1)
                month = (lsvThang.SelectedIndex + 1) + "";
            else month = DateTime.Now.ToString("MM");
            HienThiDSCongChuan();
        }
        #endregion

        #region Popup 
        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Visible)
            {
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;

            }
            else if (borNam.Visibility == Visibility.Visible)
            {
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;

            }
        }
        #endregion

        #region Năm
        private void bodHienThiNam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNam.Visibility == Visibility.Collapsed)
            {

                borNam.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        private void LoadDLNam()
        {
            string year = "";
            year = DateTime.Now.ToString("yyyy");
            txbSelectYear.Text = "Năm " + year;
            listNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            listNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            listNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lsvNam.ItemsSource = listNam;
        }

        private void textSearchNam_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNam = new List<Nam>();
            foreach (var str in listNam)
            {
                if (str.nam.Contains(textSearchNam.Text.ToString()))
                {
                    lstSearchNam.Add(str);

                }
            }
            lsvNam.ItemsSource = lstSearchNam;
            if (textSearchNam.Text == "")
            {
                lsvNam.ItemsSource = listNam;
            }
        }

        private void lsvListYear_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectYear.Text = lsvNam.SelectedItem.ToString();
            borNam.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
            Main.Nam = lsvNam.SelectedItem.ToString();
        }

        private void lsvNam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNam.ScrollToVerticalOffset(scrollNam.VerticalOffset - e.Delta);
        }

        private void borNam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Nam th = (sender as Border).DataContext as Nam;
            if (th != null)
            {
                if (th.nam != null)
                {
                    txbSelectYear.Text = th.nam.ToString();
                }
                else
                {
                    txbSelectYear.Text = DateTime.Now.ToString("yyyy");
                }
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.Nam = th.nam;
                HienThiDSCongChuan();
            }
        }

        #endregion

        #region Tháng
        private void bodHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Collapsed)
            {

                borThang.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {

                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }
        private void textSearchThang_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchThang = new List<Thang>();
            foreach (var str in listThang)
            {
                if (str.thang.Contains(textSearchThang.Text.ToString()))
                {
                    lstSearchThang.Add(str);

                }
            }
            lsvThang.ItemsSource = lstSearchThang;
            if (textSearchThang.Text == "")
            {
                lsvThang.ItemsSource = listThang;
            }

        }

        private void borThang_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Thang th = (sender as Border).DataContext as Thang;
            if (th != null)
            {
               
                if (th.thang.Length != -1)
                {
                    txbSelectMonth.Text = th.thang;
                }
                else
                {
                    txbSelectMonth.Text = DateTime.Now.ToString("MM");
                }
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                Main.Thang = th.thang;
                HienThiDSCongChuan();
            }
        }
        ////đag lm 
        private void lsvListMonth_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            txbSelectMonth.Text = lsvThang.SelectedItem.ToString();
            calendarView.Filter = item =>
            {
                if (item is DataCalendar calendar)
                {
                    return calendar.apply_month.ToString() == txbSelectMonth.Text;
                }
                return false;
            };
            calendarView.Refresh();
            borThang.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
            Main.Thang = lsvThang.SelectedItem.ToString();
        }
        private void LoadDLThang()
        {
            txbSelectMonth.Text = "Tháng " + DateTime.Now.Month.ToString();
            for (var i = 1; i <= 12; i++)
            {
                listThang.Add(new Thang { thang = $"Tháng {i}" });
            }
            lsvThang.ItemsSource = listThang;
        }

        private void lsvThang_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollThang.ScrollToVerticalOffset(scrollThang.VerticalOffset - e.Delta);
        }
        #endregion

        private int num_days;
        private void bodSaveStandanrd_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (String.IsNullOrEmpty(txbNumberStandard.Text))
            {
                txtValidateCongChuan.Text = "Số công chuẩn không được để trống!" as string;
            }
            else
            {
                txtValidateCongChuan.Visibility = Visibility.Hidden;
                txtValidateCongChuan.Text = "";
                TaoCongChuan();
                Main.grShowPopup.Children.Add(new ucXacNhanCongChuan(Main));
            }
        }
    }
}
