using ChamCong365.Popup.CaiDatLuong.CaiDatPhucLoi;
using ChamCong365.Popup.CaiDatLuong.CongCong;
using ChamCong365.Popup.ChamCong;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ChamCong365.SalarySettings
{
    /// <summary>
    /// Interaction logic for ucCongCong.xaml
    /// </summary>
    /// 
    public class NhanVienCongCong
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string PhongBan { get; set; }
        public string ChucVu { get; set; }
        public string CaCong { get; set; }
        public DateTime ThoiGianNhanCong { get; set; }
        public DateTime NgayGhiNhan { get; set; }
        public string NguoiXetDuyet { get; set; }
        public string GhiChu { get; set; }

    }
    public class Thang
    {
        public string thang { get; set; }
    }
    public class Nam
    {
        public string nam { get; set; }
    }
    public partial class ucCongCong : UserControl
    {
        MainWindow Main;
        List<NhanVienCongCong > nhanViens = new List<NhanVienCongCong> ();
        List<string> thang = new List<string> ();
        int month, year;
        BrushConverter br = new BrushConverter ();
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        private int TongSoTrang = 0;
        private int SoDu = 0;
        private List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat> lstChuaTL = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
        private List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat> lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
        private List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat> lstChuaTLFilter = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
        private List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat> lstChuaTLFilterPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
        public ucCongCong(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDLNam();
            LoadDLThang();
            LoadDLCongCong();
            //main.scrollMain.HorizontalScrollBarVisibility = ScrollBarVisibility.Visible;
        }

        private void LoadDLCongCong()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/list_user_cong_cong")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
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
                    //request.AddParameter("start_date", "2023-01-01T00:00:00.000+00:00");
                    //request.AddParameter("end_date", "2023-10-01T00:00:00.000+00:00");

                    request.AddParameter("com_id", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CongCong.clsDSCongCong.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CongCong.clsDSCongCong.Root>(b);
                    if (chuaTL.data.list_dexuat != null)
                    {
                        foreach (var item in chuaTL.data.list_dexuat)
                        {
                            foreach (var it in item.userDuyet)
                            {
                                if (item.id_user_duyet == it.idQLC.ToString())
                                {
                                    item.name_user_duyet = it.userName;
                                }
                            }
                            lstChuaTL.Add(item);
                        }
                        if (lstChuaTL.Count > 10)
                        {
                            TongSoTrang = chuaTL.data.list_dexuat.Count / 10;
                            SoDu = 10 - (chuaTL.data.list_dexuat.Count % 10);
                            if (chuaTL.data.list_dexuat.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            if (TongSoTrang < 3)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                            }
                            for (int i = 0; i < 10; i++)
                            {
                                lstChuaTLPT.Add(chuaTL.data.list_dexuat[i]);
                            }
                            //lstLuongCB = luongCB.listResult;
                            dgvCongCong.ItemsSource = lstChuaTLPT;

                            docPhanTrang.Visibility = Visibility.Visible;

                        }
                        else
                        {
                            dgvCongCong.ItemsSource = lstChuaTL;
                            docPhanTrang.Visibility = Visibility.Collapsed;
                        }
                        //dgvCongCong.ItemsSource = lstChuaTL;
                    }
                }

            }
            catch
            {

            }
        }

        private void LoadDLNam()
        {
            textHienThiNam.Text = "Năm " + DateTime.Now.Year.ToString();
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lsvNam.ItemsSource = lstNam;
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
        private void bodXoaCongCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucXoaCongCong());
        }

        private void XacNhanCongCongTatCa_Checked(object sender, RoutedEventArgs e)
        {
            //bodXacNhanCongCongTatCa.Visibility = Visibility.Visible;
           
        }
        private void XacNhanCongCongTatCa_Unchecked(object sender, RoutedEventArgs e)
        {
           //bodXacNhanCongCongTatCa.Visibility = Visibility.Collapsed;
        }

        private void XacNhanCongCongChoNhanVien_Checked(object sender, RoutedEventArgs e)
        {
           //bodXacNhanCongCongnNv.Visibility = Visibility.Visible;
           
        }
        private void XacNhanCongCongChoNhanVien_Unchecked(object sender, RoutedEventArgs e)
        {
            //bodXacNhanCongCongnNv.Visibility = Visibility.Collapsed;
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
        private void lsvNam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollNam.ScrollToVerticalOffset(scrollNam.VerticalOffset - e.Delta);
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

        private void lsvNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            textHienThiNam.Text = lsvNam.SelectedItem.ToString();
            borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
            borNam.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
            Main.Nam = lsvNam.SelectedItem.ToString();
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
        private void btnHienThiThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
            Main.Thang = lsvThang.SelectedItem.ToString();
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

        private void dgvCongCong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnHienThiNam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
            lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
            for (int i = 0; i < 10; i++)
            {
                lstChuaTLPT.Add(lstChuaTL[i]);
            }
            //lstLuongCB = luongCB.listResult;
            dgvCongCong.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                    for (int i = TongSoTrang * 10 - 20; i < TongSoTrang * 10 - 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvCongCong.ItemsSource = lstChuaTLPT;
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
                        lstChuaTL = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                        for (int i = 0; i < 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvCongCong.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvCongCong.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvCongCong.ItemsSource = lstChuaTLPT;
                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvCongCong.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                for (int i = 0; i < 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgvCongCong.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgvCongCong.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgvCongCong.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();

                for (int i = int.Parse(textPage3.Text) * 10 - 10; i < int.Parse(textPage3.Text) * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgvCongCong.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvCongCong.ItemsSource = lstChuaTLPT;
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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                        for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvCongCong.ItemsSource = lstChuaTLPT;
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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvCongCong.ItemsSource = lstChuaTLPT;
                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                        lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                        for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvCongCong.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgvCongCong.ItemsSource = lstChuaTLPT;
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
                    lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvCongCong.ItemsSource = lstChuaTLPT;

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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                        for (int i = 10; i < 20; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvCongCong.ItemsSource = lstChuaTLPT;

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
                        lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                        for (int i = 20; i < 30; i++)
                        {
                            lstChuaTLPT.Add(lstChuaTL[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvCongCong.ItemsSource = lstChuaTLPT;
                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();
                    lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                    for (int i = int.Parse(textPage2.Text) * 10 - 10; i < int.Parse(textPage2.Text) * 10; i++)
                    {
                        lstChuaTLPT.Add(lstChuaTL[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgvCongCong.ItemsSource = lstChuaTLPT;
                }

            }
        }

        private void bodThongKe_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            borPage1.Background = (Brush)bc.ConvertFrom("#4c5bd4");
            borPage2.Background = (Brush)bc.ConvertFrom("#ffffff");
            borPage3.Background = (Brush)bc.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)bc.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)bc.ConvertFrom("#474747");
            textPage3.Foreground = (Brush)bc.ConvertFrom("#474747");
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Visible;
            borPageCuoi.Visibility = Visibility.Visible;
            lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
            lstChuaTL = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
            string[] year1 = textHienThiNam.Text.Split(new[] { "Năm" }, StringSplitOptions.None);
            string y = year1[year1.Length - 1].Trim();
            string y1 = year1[year1.Length - 1].Trim();
            string[] month1 = textHienThiThang.Text.Split(new[] { "Tháng" }, StringSplitOptions.None);
            string mo = month1[month1.Length - 1].Trim();
            string mo1 = (int.Parse(mo) + 1).ToString();
            if (int.Parse(mo) < 10)
            {
                mo = "0" + mo;
            }
            if (mo == "12")
            {
                mo1 = "01";
                y1 = (int.Parse(y) + 1).ToString();
            }
            if(int.Parse(mo1) < 10)
            {
                mo1 = "0" + mo1;
            }
            
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/list_user_cong_cong")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;

                request.AddParameter("start_date", $"{y}-{mo}-01T00:00:00.000+00:00");
                request.AddParameter("end_date", $"{y1}-{mo1}-01T00:00:00.000+00:00");

                request.AddParameter("com_id", Main.IdAcount);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                OOP.CaiDatLuong.CongCong.clsDSCongCong.Root chuaTL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CongCong.clsDSCongCong.Root>(b);
                if (chuaTL.data.list_dexuat != null)
                {
                    foreach (var item in chuaTL.data.list_dexuat)
                    {
                        foreach (var it in item.userDuyet)
                        {
                            if (item.id_user_duyet == it.idQLC.ToString())
                            {
                                item.name_user_duyet = it.userName;
                            }
                        }
                        lstChuaTL.Add(item);
                    }
                    if (lstChuaTL.Count > 10)
                    {
                        TongSoTrang = chuaTL.data.list_dexuat.Count / 10;
                        SoDu = 10 - (chuaTL.data.list_dexuat.Count % 10);
                        if (chuaTL.data.list_dexuat.Count % 10 > 0)
                        {
                            TongSoTrang++;
                        }
                        if (TongSoTrang < 3)
                        {
                            borPage3.Visibility = Visibility.Collapsed;
                        }
                        for (int i = 0; i < 10; i++)
                        {
                            lstChuaTLPT.Add(chuaTL.data.list_dexuat[i]);
                        }
                        //lstLuongCB = luongCB.listResult;
                        dgvCongCong.ItemsSource = lstChuaTLPT;

                        docPhanTrang.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        dgvCongCong.ItemsSource = lstChuaTL;
                        docPhanTrang.Visibility = Visibility.Collapsed;
                    }
                    //dgvCongCong.ItemsSource = lstChuaTL;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - 10 + (10 - SoDu); i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgvCongCong.ItemsSource = lstChuaTLPT;
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
                lstChuaTLPT = new List<OOP.CaiDatLuong.CongCong.clsDSCongCong.ListDexuat>();
                for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                {
                    lstChuaTLPT.Add(lstChuaTL[i]);
                }
                //lstLuongCB = luongCB.listResult;
                dgvCongCong.ItemsSource = lstChuaTLPT;
            }
        }
    }
}
