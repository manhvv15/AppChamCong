using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.DiMuonVeSom
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
        public List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo> lstPhatMuonPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
        List<Thang> lstThang = new List<Thang>();
        List<Thang> lstSearchThang = new List<Thang>();
        List<Nam> lstNam = new List<Nam>();
        private MainWindow Main;
        public frmCaiDatDiMuonVeSom(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            LoadDLCaLamViec();
            loadDLThang();
            LoadDLNam();
            LoadDLDSPhatDiMuonVeSom();
            
            main.i = 0;
        }

        private void LoadDLNam()
        {
            List<string> listNam = new List<string>();
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
            listNam.Add("Năm " + DateTime.Now.Year);
            listNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString());

            lsvNam.ItemsSource = listNam;
            lsvNam.SelectedIndex = 1;
            lsvNam.SelectedItem = listNam[1];
            lsvNam.PlaceHolder = listNam[1];
        }

        private void loadDLThang()
        {
            List<string> listThang = new List<string>();
            listThang.Add("Tháng 1");
            listThang.Add("Tháng 2");
            listThang.Add("Tháng 3");
            listThang.Add("Tháng 4");
            listThang.Add("Tháng 5");
            listThang.Add("Tháng 6");
            listThang.Add("Tháng 7");
            listThang.Add("Tháng 8");
            listThang.Add("Tháng 9");
            listThang.Add("Tháng 10");
            listThang.Add("Tháng 11");
            listThang.Add("Tháng 12");
            lsvThang.ItemsSource = listThang;
            lsvThang.SelectedIndex = DateTime.Now.Month - 1;
            lsvThang.SelectedItem = listThang[DateTime.Now.Month - 1];
            lsvThang.PlaceHolder = listThang[DateTime.Now.Month - 1];
        }
        private async void LoadDLCaLamViec()
        {
            try
            {
                
                using (HttpClient httpClient = new HttpClient())
                {
                    string url = "http://210.245.108.202:3000/api/qlc/shift/list";
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var kq = await httpClient.GetAsync(url);
                    OOP.CaiDatLuong.clsShift.Root CaLV = JsonConvert.DeserializeObject<OOP.CaiDatLuong.clsShift.Root>(kq.Content.ReadAsStringAsync().Result);
                    if (CaLV.data != null)
                    {
                        foreach (var calv in CaLV.data.items)
                        {
                            lstShift.Add(calv);
                           
                        }
                    }
                }
            }
            catch
            {

            }
        }
       
        private async void LoadDLDSPhatDiMuonVeSom()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_phat_muon")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    int nam, thang;
                    if(lsvNam.SelectedItem != null)
                        nam = int.Parse(lsvNam.SelectedItem.ToString().Split(' ')[1]);
                    else nam = DateTime.Now.Year;
                    if(lsvThang.SelectedItem != null) thang = int.Parse(lsvThang.SelectedItem.ToString().Split(' ')[1]);
                    else thang = DateTime.Now.Month;
                    request.AddParameter("pm_time_begin", nam + "-" + thang);
                    request.AddParameter("pm_time_end", nam + "-" + thang);
                    request.AddParameter("pm_id_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = await restclient.ExecuteAsync(request);
                    var b = resAlbum.Content;
                    lstPhatMuon.Clear();
                    lstPhatMuonPage.Clear();
                    OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Root DSPhat = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Root>(b);
                    if (DSPhat.phat_muon_info != null)
                    {
                     
                        foreach (var item in DSPhat.phat_muon_info)
                        {

                            if (item.pm_type == 1)
                            {
                                item.pm_type_str = "Đi muộn";
                                //item.pm_time_begin_format = char.ToLower(item.pm_type_str[0]) + item.pm_type_str.Substring(1);

                            }
                            else if (item.pm_type == 2)
                            {
                                item.pm_type_str = "Về sớm";
                                //item.pm_type_str_format = char.ToLower(item.pm_type_str[0]) + item.pm_type_str.Substring(1);
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
                           item.pm_time_begin_format = $"{item.pm_time_begin.Month} - {item.pm_time_begin.Year}";
                            if(lstPhatMuonPage.Count < NumberPerPage) lstPhatMuonPage.Add(item);
                            lstPhatMuon.Add(item);
                        }
                    }
                    if (lstPhatMuon.Count <= 10) DpPhanTRang.Visibility = Visibility.Collapsed;
                    else DpPhanTRang.Visibility = Visibility.Visible;
                    TongSoTrang = lstPhatMuon.Count / NumberPerPage;
                    SoDu = NumberPerPage - (lstPhatMuon.Count % NumberPerPage);
                    if (lstPhatMuon.Count % NumberPerPage > 0)
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
                    dgv.ItemsSource = lstPhatMuonPage;
                    dgv.Items.Refresh();

                }

            }
            catch
            {

            }

        }
        #region Comons
       
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindChild<T>(child, childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        #endregion

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
        private void DateTime_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            LoadDLDSPhatDiMuonVeSom();
        }
        private int TongSoTrang = 0;
        private int PageNumberCurrent = 1;
        private int SoDu = 0;
        public int NumberPerPage = 10;
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
                lstPhatMuonPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
                for (int i = 0; i < NumberPerPage; i++)
                {
                    lstPhatMuonPage.Add(lstPhatMuon[i]);
                }
                dgv.ItemsSource = lstPhatMuonPage;
                PageNumberCurrent = 1;
            }
            catch (Exception)
            {
            }
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
            lstPhatMuonPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
            for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstPhatMuon.Count; i++)
            {
                lstPhatMuonPage.Add(lstPhatMuon[i]);
            }
            dgv.ItemsSource = lstPhatMuonPage;
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
                    lstPhatMuonPage = new List<DSPhatDiMuonVeSom.PhatMuonInfo>();
                    for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstPhatMuon.Count; i++)
                    {
                        lstPhatMuonPage.Add(lstPhatMuon[i]);
                    }
                    dgv.ItemsSource = lstPhatMuonPage;
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
                            lstPhatMuonPage = new List<DSPhatDiMuonVeSom.PhatMuonInfo>();
                            for (int i = 0; i < 10; i++)
                            {
                                lstPhatMuonPage.Add(lstPhatMuon[i]);
                            }
                            dgv.ItemsSource = lstPhatMuonPage;
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
                            lstPhatMuonPage = new List<DSPhatDiMuonVeSom.PhatMuonInfo>();
                            if (lstPhatMuon.Count > 10)
                            {
                                for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstPhatMuon.Count; i++)
                                {
                                    lstPhatMuonPage.Add(lstPhatMuon[i]);
                                }
                                dgv.ItemsSource = lstPhatMuonPage;
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
                lstPhatMuonPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
                if (lstPhatMuon.Count > NumberPerPage)
                {   
                    for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstPhatMuon.Count; i++)
                    {
                        lstPhatMuonPage.Add(lstPhatMuon[i]);
                    }
                    dgv.ItemsSource = lstPhatMuonPage;
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
                        lstPhatMuonPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
                        for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstPhatMuon.Count; i++)
                        {
                            lstPhatMuonPage.Add(lstPhatMuon[i]);
                        }
                        dgv.ItemsSource = lstPhatMuonPage;
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
                            lstPhatMuonPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
                            for (int i = TongSoTrang * NumberPerPage - NumberPerPage; i < TongSoTrang * NumberPerPage - SoDu; i++)
                            {
                                lstPhatMuonPage.Add(lstPhatMuon[i]);
                            }
                            dgv.ItemsSource = lstPhatMuonPage;
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
                            lstPhatMuonPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
                            if (lstPhatMuon.Count > NumberPerPage)
                            {
                                for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstPhatMuon.Count; i++)
                                {
                                    lstPhatMuonPage.Add(lstPhatMuon[i]);
                                }
                                dgv.ItemsSource = lstPhatMuonPage;
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
            lstPhatMuonPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
            for (int i = PageNumberCurrent * NumberPerPage - NumberPerPage; i < PageNumberCurrent * NumberPerPage && i < lstPhatMuon.Count; i++)
            {
                lstPhatMuonPage.Add(lstPhatMuon[i]);
            }
            dgv.ItemsSource = lstPhatMuonPage;
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
            lstPhatMuonPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo>();
            for (int i = TongSoTrang * NumberPerPage - NumberPerPage; i < TongSoTrang * NumberPerPage - SoDu; i++)
            {
                lstPhatMuonPage.Add(lstPhatMuon[i]);
            }
            //lstLuongCB = luongCB.listResult;
            dgv.ItemsSource = lstPhatMuonPage;
        }

        private void cboSelectNumberPerPage_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(Main != null)
            {
                if (cboSelectNumberPerPage.SelectedIndex == 0) NumberPerPage = 10;
                if (cboSelectNumberPerPage.SelectedIndex == 1) NumberPerPage = 20;
                if (cboSelectNumberPerPage.SelectedIndex == 2) NumberPerPage = 50;
                if (cboSelectNumberPerPage.SelectedIndex == 3) NumberPerPage = 100;
                LoadDLDSPhatDiMuonVeSom();
            }
        }
    }
}
