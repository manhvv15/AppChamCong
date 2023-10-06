using Newtonsoft.Json;
using RestSharp;
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

namespace ChamCong365.SalarySettings.DiMuonVeSom
{
    /// <summary>
    /// Interaction logic for frmCaiDatDiMuonVeSom.xaml
    /// </summary>
    public partial class frmCaiDatDiMuonVeSom : Page
    {
        public class Thang
        {
            public string thang { get; set; }
        }
        public class Nam
        {
            public string nam { get; set; }
        }
        public List<OOP.CaiDatLuong.clsShift.Item> lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();

        public List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo> lstPhatMuon = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        List<Nam> lstNam = new List<Nam>();
        private MainWindow Main;
        public frmCaiDatDiMuonVeSom(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            textHienThiNam.Text = "Năm " + DateTime.Now.Year;
            LoadDLCaLamViec();
            loadDLThang();
            LoadDLNam();
            LoadDLDSPhatDiMuonVeSom();
            
            main.i = 0;
        }

        private void LoadDLNam()
        {
            lstNam.Add(new Nam() { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam() { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam() { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lsvNam.ItemsSource = lstNam;
        }

        private void loadDLThang()
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
        private void LoadDLCaLamViec()
        {
            try
            {
                //lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();
                //var client = new HttpClient();
                //var request = new HttpRequestMessage(HttpMethod.Get, "http://210.245.108.202:3000/api/qlc/shift/list");
                //request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                //var response = await client.SendAsync(request);
                //response.EnsureSuccessStatusCode();
                ////Console.WriteLine(await response.Content.ReadAsStringAsync());
                //OOP.CaiDatLuong.clsShift.Root CaLV = JsonConvert.DeserializeObject<OOP.CaiDatLuong.clsShift.Root>(await response.Content.ReadAsStringAsync());
                //if (CaLV.data != null)
                //{
                //    foreach (var calv in CaLV.data.items)
                //    {
                //        lstShift.Add(calv);
                //        //cboCaLVApDung.Items.Add("(" + calv.shift_id + ")" + "-" + calv.shift_name);
                //    }
                //}
                using (HttpClient httpClient = new HttpClient())
                {
                    //string url = Properties.Resources.URL + "listGroupCustomer";
                    string url = "http://210.245.108.202:3000/api/qlc/shift/list";
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var kq = httpClient.GetAsync(url);
                    OOP.CaiDatLuong.clsShift.Root CaLV = JsonConvert.DeserializeObject<OOP.CaiDatLuong.clsShift.Root>(kq.Result.Content.ReadAsStringAsync().Result);
                    if (CaLV.data != null)
                    {
                        foreach (var calv in CaLV.data.items)
                        {
                            lstShift.Add(calv);
                            //cboCaLVApDung.Items.Add("(" + calv.shift_id + ")" + "-" + calv.shift_name);
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private void LoadDLDSPhatDiMuonVeSom()
        {

            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_phat_muon")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    if (int.Parse(DateTime.Now.Month.ToString()) < 10)
                    {
                        request.AddParameter("pm_time_begin", DateTime.Now.Year + "-0" + DateTime.Now.Month + "-01T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("pm_time_begin", DateTime.Now.Year + "-" + DateTime.Now.Month + "-01T00:00:00.000+00:00");

                    }
                    if (DateTime.Now.Month == 12)
                    {
                        request.AddParameter("pm_time_end", (int.Parse(DateTime.Now.Year.ToString()) + 1).ToString() + "-01-01T00:00:00.000+00:00");

                    }
                    else
                    {
                        if (int.Parse(DateTime.Now.Month.ToString() + 1) < 10)
                        {
                            request.AddParameter("pm_time_end", DateTime.Now.Year + "-0" + (int.Parse(DateTime.Now.Month.ToString()) + 1).ToString() + "-01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("pm_time_end", DateTime.Now.Year + "-" + (int.Parse(DateTime.Now.Month.ToString()) + 1).ToString() + "-01T00:00:00.000+00:00");

                        }
                    }
                    request.AddParameter("pm_id_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Root DSPhat = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Root>(b);
                    if (DSPhat.phat_muon_info != null)
                    {

                        foreach (var item in DSPhat.phat_muon_info)
                        {

                            if (item.pm_type == 1)
                            {
                                item.pm_type_str = "Phạt đi muộn";
                            }
                            else if (item.pm_type == 2)
                            {
                                item.pm_type_str = "Phạt về sớm";
                            }
                            if (item.pm_time_end == null)
                            {
                                item.pm_time_end_str = "Chưa cập nhât";
                            }
                            else
                            {
                                item.pm_time_end_str = item.pm_time_end.ToString();
                            }
                            if (item.pm_type_phat == 1)
                            {
                                item.pm_monney_str = item.pm_monney + " VNĐ/ca";
                            }
                            else if (item.pm_type_phat == 2)
                            {
                                item.pm_monney_str = item.pm_monney + " công/ca";
                            }
                            lstPhatMuon.Add(item);
                        }
                        dgv.ItemsSource = lstPhatMuon;
                    }
                }

            }
            catch
            {

            }

        }

        private void borHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borThang.Visibility == Visibility.Collapsed)
            {
                borHienThiThang.CornerRadius = new CornerRadius(5,5,0,0);
                borThang.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiThang.CornerRadius = new CornerRadius(5,5,5,5);
                borThang.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void lsvThang_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollThang.ScrollToVerticalOffset(scrollThang.VerticalOffset - e.Delta);
        }

        private void lsvThang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textHienThiThang.Text = lsvThang.SelectedItem.ToString();
            borHienThiThang.CornerRadius = new CornerRadius(5, 5, 5, 5);
            borThang.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
        }
        private List<Nam> lstSearchNam = new List<Nam>();
        private void textSearchThang_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchThang = new List<Thang>();
            foreach(var str in lstThang)
            {
                if (str.thang.Contains(textSearchThang.Text.ToString()))
                {
                    lstSearchThang.Add(str);
                    
                }
            }
            lsvThang.ItemsSource = lstThang;
            if (textSearchThang.Text == "")
            {
                lsvThang.ItemsSource = lstThang;
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

        }

        private void lsvNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textHienThiNam.Text = lsvNam.SelectedItem.ToString();
            borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
            borNam.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
        }

        private void btnThemMoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.PhatDiMuonVeSom.PopUpChinhSuaMucPhat("Thêm mới mức phạt đi muộn về sớm", Main, this));
        }

        private void btnChinhSua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo info = (sender as Border).DataContext as OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo;
            if (info != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.PhatDiMuonVeSom.PopUpChinhSuaMucPhat("Chỉnh sửa mức phạt đi muộn về sớm", Main, this, info));

            }
        }

        private void btnXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo info = (sender as Border).DataContext as OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo;
            if(info != null)
            {
                Main.grShowPopup.Children.Add(new Popup.CaiDatLuong.PhatDiMuonVeSom.PopUpXoaMucPhat(Main, info, this));

            }

        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void DockPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void Border_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);

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
    }
}
