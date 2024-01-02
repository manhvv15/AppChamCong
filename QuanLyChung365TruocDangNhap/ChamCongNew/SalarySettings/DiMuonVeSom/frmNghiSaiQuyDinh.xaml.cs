using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatDiMuonVeSom;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.NghiSaiQuyDinh;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon;
//using DocumentFormat.OpenXml.Wordprocessing;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.CoCau_ViTri_ToChuc.ucSoDoToChuc;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.DiMuonVeSom
{
    /// <summary>
    /// Interaction logic for frmNghiSaiQuyDinh.xaml
    /// </summary>
    public partial class frmNghiSaiQuyDinh : Page,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa> lstPC = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
        public List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa> lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
        private List<OOP.ClsCaLamViec> lstCaLV222 = new List<OOP.ClsCaLamViec>();
        private MainWindow Main;
        public List<OOP.CaiDatLuong.clsShift.Item> lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();
        private string IdCaLV;
        public frmNghiSaiQuyDinh(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            main.i = 0;
            LoadDLCaLamViec();
            LoadDLNam();
        }

        private async void LoadDLCaLamViec()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    //string url = Properties.Resources.URL + "listGroupCustomer";
                    string url = "http://210.245.108.202:3000/api/qlc/shift/list";
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var kq = await httpClient.GetAsync(url);
                    OOP.CaiDatLuong.clsShift.Root CaLV = JsonConvert.DeserializeObject<OOP.CaiDatLuong.clsShift.Root>(kq.Content.ReadAsStringAsync().Result);
                    if (CaLV.data != null)
                    {
                        foreach (var calv in CaLV.data.items)
                        {
                            lstShift.Add(calv);
                            //cboCaLVApDung.Items.Add("(" + calv.shift_id + ")" + "-" + calv.shift_name);
                        }
                        lsvCaLamViec.ItemsSource = lstShift;
                        LoadDLCaiDatNghiSaiQD();
                    }
                }
            }
            catch
            {

            }
        }

        private void LoadDLNam()
        {
            List<string> lstNam= new List<string>();
            lstNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString());
            lstNam.Add("Năm " + DateTime.Now.Year);
            lstNam.Add("Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString());
            lsvNam.ItemsSource = lstNam;
            lsvNam.PlaceHolder = lstNam[1];
        }
        
        private void LoadDLCaiDatNghiSaiQD()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_phat_ca_com")))
                {
                    Loading.Visibility= Visibility.Visible;
                    lstPC.Clear();
                    lstPCPage.Clear();
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("pc_com", Main.IdAcount.ToString());
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    if(lsvNam.SelectedItem!= null) request.AddParameter("year", lsvNam.SelectedItem.ToString().Split(' ')[1]);
                    else request.AddParameter("year", DateTime.Now.Year.ToString());
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    try
                    {
                        clsDSCaiDatNghiSaiQD.Root DSPhat = JsonConvert.DeserializeObject<clsDSCaiDatNghiSaiQD.Root>(b);
                        if (DSPhat.listPhatCa != null)
                        {
                            foreach (var item in DSPhat.listPhatCa)
                            {
                                item.OnOff = 0;
                                item.pc_time_s = item.pc_time.Day.ToString() + "/" + item.pc_time.Month + "/" + item.pc_time.Year;
                                item.pc_money_str = item.pc_money + " VND/Ca";
                                foreach (var it in lstShift)
                                {
                                    if (item.pc_shift == it.shift_id)
                                    {
                                        item.name_shift = it.shift_name;
                                        item.start_date = it.start_time;
                                        item.end_date = it.end_time;
                                    }
                                }
                                if (lstPCPage.Count < 10)
                                {
                                    lstPCPage.Add(item);
                                }
                                lstPC.Add(item);
                            }
                        }
                        if (lstPC.Count > 10)
                        {
                            TongSoTrang = lstPC.Count / 10;
                            SoDu = 10 - (lstPC.Count % 10);
                            if (lstPC.Count % 10 > 0)
                            {
                                TongSoTrang++;
                            }
                            if (TongSoTrang < 3)
                            {
                                borPage3.Visibility = Visibility.Collapsed;
                            }
                            for (int i = 0; i < 10; i++)
                            {
                                lstPCPage.Add(lstPC[i]);
                            }
                            dgv.ItemsSource = lstPCPage;
                            dgv.Items.Refresh();
                            DpPhanTRang.Visibility = Visibility.Visible;
                        }
                        else
                        {
                            dgv.ItemsSource = lstPCPage;
                            dgv.Items.Refresh();
                            DpPhanTRang.Visibility = Visibility.Collapsed;
                        }
                     }
                    catch (Exception)
                    {
                    }
                }
                Loading.Visibility= Visibility.Collapsed;
            }
            catch
            {
                Loading.Visibility= Visibility.Collapsed;
            }
        }
        
        private void borCaLV_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.clsShift.Item calv = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.clsShift.Item;
            if (calv != null)
            {
                IdCaLV = calv.shift_id.ToString();
            }
            

        }

        private void lsvCaLamViec_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            //scrollCaLV.ScrollToVerticalOffset(scrollCaLV.VerticalOffset - e.Delta);
        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnCaiDatMucPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borCaiDatMucPhat.Visibility == Visibility.Collapsed)
            {
                borCaiDatMucPhat.Visibility = Visibility.Visible;
            }
            else
            {
                borCaiDatMucPhat.Visibility = Visibility.Collapsed;
            }
        }

        private void btnApDung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (string.IsNullOrEmpty(textNhapMucTienPhat.Text))
            {
                allow = false;
                validateTienPhat.Visibility = Visibility.Visible;
                validateTienPhat.Text = "Vui lòng nhập số tiền phạt!";
            }
            else
            {
                if (!IsNumeric(textNhapMucTienPhat.Text))
                {
                    validateTienPhat.Visibility = Visibility.Visible;
                    validateTienPhat.Text = "Tiền phạt phải là 1 số!";
                }
                else validateTienPhat.Visibility = Visibility.Collapsed;
            }
            if(dtpNgayBatDauAD.SelectedDate == null)
            {
                allow= false;
                validateTime.Visibility = Visibility.Visible;
                validateTime.Text = "Bạn vui lòng chọn ngày áp dụng";
            }
            else validateTime.Visibility = Visibility.Collapsed;
            if (allow)
            {
                ApDung();
            }    
        }
        Thread t;
        private void ApDung()
        {
            try
            {
                int dem = 0;
                for (int i = 0; i < lsvCaLamViec.SelectedItems.Count; i++)
                {
                    using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/insert_phat_ca")))
                    {
                        Loading.Visibility = Visibility.Visible;
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AddParameter("pc_com", Main.IdAcount.ToString());
                        request.AddParameter("pc_shift", ((OOP.CaiDatLuong.clsShift.Item)lsvCaLamViec.SelectedItems[i]).shift_id.ToString());
                        request.AddParameter("pc_money", textNhapMucTienPhat.Text);
                        string[] date = dtpNgayBatDauAD.Text.Split(Convert.ToChar("/"));
                        string DateScreen = date[2] + "-" + date[0] + "-" + date[1];
                        request.AddParameter("pc_time", DateScreen);
                        request.AddParameter("pc_type", "1");
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;
                        OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsAddNghiSaiQD.Root add = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsAddNghiSaiQD.Root>(b);
                            if (add.newobj != null)
                            {
                                if (dem == lsvCaLamViec.SelectedItems.Count - 1)
                                {
                                    textNhapMucTienPhat.Text = "";
                                    dtpNgayBatDauAD.SelectedDate = DateTime.Now;
                                    lsvCaLamViec.SelectedItems.Clear();
                                    borCaiDatMucPhat.Visibility = Visibility.Collapsed;
                                    LoadDLCaiDatNghiSaiQD();
                                }
                            }
                            dem++;
                    }
                }
            }
            catch
            {

            }
        }

        private void textXemMucPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa phat = (sender as System.Windows.Controls.Border).DataContext as OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa;
            if (phat != null)
            {
                Main.grShowPopup.Children.Add(new PopUpChiTietMucPhatNghiSaiQD(Main, this, phat));

            }
        }

        private void WrapPanel_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void borCaiDatMucPhat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
        private void lsvNam_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            LoadDLCaiDatNghiSaiQD();
        }

        private int TongSoTrang = 0;
        private int PageNumberCurrent = 1;
        private int SoDu = 0;
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
            if(TongSoTrang > 2)
            {
                borPage2.Visibility = Visibility.Visible;
                borPage3.Visibility = Visibility.Visible;
            }
            else if(TongSoTrang > 1)
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
            lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
            for (int i = 0; i < 10; i++)
            {
                lstPCPage.Add(lstPC[i]);
            }
            //lstLuongCB = luongCB.listResult;
            dgv.ItemsSource = lstPCPage;
            PageNumberCurrent = 1;
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
            lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstPC.Count; i++)
            {
                lstPCPage.Add(lstPC[i]);
            }
            dgv.ItemsSource = lstPCPage;
        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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
                lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstPC.Count; i++)
                {
                    lstPCPage.Add(lstPC[i]);
                }
                dgv.ItemsSource = lstPCPage;

                //BrushConverter brus = new BrushConverter();
                //if (textPage1.Text != PageNumberCurrent.ToString() && int.Parse(textPage1.Text) == PageNumberCurrent - 1)
                //{
                //    if (PageNumberCurrent > 2)
                //    {
                //        textPage1.Text = (PageNumberCurrent - 2).ToString();
                //        textPage2.Text = (PageNumberCurrent - 1).ToString();
                //        textPage3.Text = PageNumberCurrent.ToString();
                //        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                //        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                //        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                //        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                //        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                //        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                //    }
                //    else if (TongSoTrang > 2)
                //    {
                //        textPage1.Text = (PageNumberCurrent - 1).ToString();
                //        textPage2.Text = (PageNumberCurrent).ToString();
                //        textPage3.Text = (PageNumberCurrent + 1).ToString();
                //        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                //        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                //        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                //        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                //        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                //        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                //        borPageDau.Visibility = Visibility.Collapsed;
                //        borLui1.Visibility = Visibility.Collapsed;
                //    }
                //    else
                //    {
                //        textPage1.Text = "1";
                //        textPage2.Text = "2";
                //        textPage3.Text = (PageNumberCurrent + 1).ToString();
                //        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                //        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                //        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                //        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                //        borPageDau.Visibility = Visibility.Collapsed;
                //        borLui1.Visibility = Visibility.Collapsed;
                //        borPage3.Visibility = Visibility.Collapsed;
                //    }
                //    borPageCuoi.Visibility = Visibility.Visible;
                //    borLen1.Visibility = Visibility.Visible;
                //    PageNumberCurrent--;
                //    lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
                //    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstPC.Count; i++)
                //    {
                //        lstPCPage.Add(lstPC[i]);
                //    }
                //    dgv.ItemsSource = lstPCPage;
                //}
                //else
                //{
                //    if (PageNumberCurrent == 3)
                //    {
                //        borPageDau.Visibility = Visibility.Collapsed;
                //        borLui1.Visibility = Visibility.Collapsed;
                //        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                //        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                //        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                //        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                //        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                //        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                //        textPage1.Text = "1";
                //        textPage2.Text = "2";
                //        textPage3.Text = "3";
                //        borLen1.Visibility = Visibility.Visible;
                //        if (TongSoTrang > 2)
                //        {
                //            borPage2.Visibility = Visibility.Visible;
                //            borPage3.Visibility = Visibility.Visible;
                //        }
                //        else if (TongSoTrang > 1)
                //        {
                //            borPage2.Visibility = Visibility.Visible;
                //            borPage3.Visibility = Visibility.Collapsed;
                //        }
                //        else
                //        {
                //            borPage2.Visibility = Visibility.Collapsed;
                //            borPage3.Visibility = Visibility.Collapsed;
                //            borPageCuoi.Visibility = Visibility.Collapsed;
                //            borLen1.Visibility = Visibility.Collapsed;
                //        }
                //        if (TongSoTrang > 1)
                //        {
                //            borPageCuoi.Visibility = Visibility.Visible;
                //            borLen1.Visibility = Visibility.Visible;
                //        }
                //        lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
                //        for (int i = 0; i < 10; i++)
                //        {
                //            lstPCPage.Add(lstPC[i]);
                //        }
                //        //lstLuongCB = luongCB.listResult;
                //        dgv.ItemsSource = lstPC;
                //        PageNumberCurrent = 1;
                //    }
                //    else
                //    {
                //        textPage1.Text = (PageNumberCurrent - 3).ToString();
                //        textPage2.Text = (PageNumberCurrent - 2).ToString();
                //        textPage3.Text = (PageNumberCurrent - 1).ToString();
                //        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                //        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                //        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                //        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                //        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                //        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                //        borPageCuoi.Visibility = Visibility.Visible;
                //        borLen1.Visibility = Visibility.Visible;
                //        borPageDau.Visibility = Visibility.Visible;
                //        borLui1.Visibility = Visibility.Visible;
                //        PageNumberCurrent -= 2;
                //        lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
                //        if (lstPC.Count > 10)
                //        {
                //            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstPC.Count; i++)
                //            {
                //                lstPCPage.Add(lstPC[i]);
                //            }
                //            //lstLuongCB = luongCB.listResult;
                //            dgv.ItemsSource = lstPCPage;
                //        }
                //    }
                //}
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
                lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
                if (lstPC.Count > 10)
                {   
                    for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstPC.Count; i++)
                    {
                        lstPCPage.Add(lstPC[i]);
                    }
                    //lstLuongCB = luongCB.listResult;
                    dgv.ItemsSource = lstPCPage;
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
                lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
                for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstPC.Count; i++)
                {
                    lstPCPage.Add(lstPC[i]);
                }
                dgv.ItemsSource = lstPCPage;
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
                    lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
                    for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
                    {
                        lstPCPage.Add(lstPC[i]);
                    }
                    dgv.ItemsSource = lstPCPage;
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
                    lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
                    if (lstPC.Count > 10)
                    {
                        for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstPC.Count; i++)
                        {
                            lstPCPage.Add(lstPC[i]);
                        }
                        dgv.ItemsSource = lstPCPage;
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
            lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
            for (int i = PageNumberCurrent * 10 - 10; i < PageNumberCurrent * 10 && i < lstPC.Count; i++)
            {
                lstPCPage.Add(lstPC[i]);
            }
            dgv.ItemsSource = lstPCPage;
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
            lstPCPage = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
            for (int i = TongSoTrang * 10 - 10; i < TongSoTrang * 10 - SoDu; i++)
            {
                lstPCPage.Add(lstPC[i]);
            }
            dgv.ItemsSource = lstPCPage;
        }

        private void cboSelect10_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if(Main != null)
            //{
            //    if (cboSelect10.SelectedIndex == 0) 10 = 10;
            //    if (cboSelect10.SelectedIndex == 1) 10 = 20;
            //    if (cboSelect10.SelectedIndex == 2) 10 = 50;
            //    if (cboSelect10.SelectedIndex == 3) 10 = 100;
            //    LoadDLCaiDatNghiSaiQD();
            //}
        }
        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        private void textNhapMucTienPhat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!IsNumeric(e.Text))
            {
                e.Handled= true;
                validateTienPhat.Text = "Tiền phạt phải là 1 số";
                validateTienPhat.Visibility = Visibility.Visible;
                /*tb_ValidateLuong.Visibility = Visibility.Visible;
                tb_ValidateLuong.Text = "Bạn vui lòng nhập đúng % lương, không nhập ký tự khác!";*/
            }
            else
            {
                validateTienPhat.Visibility = Visibility.Collapsed;
                //tb_ValidateLuong.Visibility = Visibility.Collapsed;
            }
        }

        private void textNhapMucTienPhat_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!IsNumeric(tb.Text))
            {
                validateTienPhat.Text = "Tiền phạt phải là 1 số";
                validateTienPhat.Visibility = Visibility.Visible;
                /*tb_ValidateLuong.Visibility = Visibility.Visible;
                tb_ValidateLuong.Text = "Bạn vui lòng nhập đúng % lương, không nhập ký tự khác!";*/
            }
            else
            {
                validateTienPhat.Visibility = Visibility.Collapsed;
                //tb_ValidateLuong.Visibility = Visibility.Collapsed;
            }
        }
    }
}
