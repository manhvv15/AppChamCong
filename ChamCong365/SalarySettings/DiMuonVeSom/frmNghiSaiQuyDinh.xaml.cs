using ChamCong365.Popup.CaiDatLuong.NghiSaiQuyDinh;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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
    /// Interaction logic for frmNghiSaiQuyDinh.xaml
    /// </summary>
    public partial class frmNghiSaiQuyDinh : Page,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class Nam
        {
            public string nam { get; set; }
        }
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        public List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa> lstPC = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();
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
            LoadDLCaiDatNghiSaiQD();
            LoadDLNam();
            
            
        }

        private void LoadDLCaLamViec()
        {
            try
            {
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
                        lsvCaLamViec.ItemsSource = lstShift;
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

        private void LoadDLCaiDatNghiSaiQD()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_phat_ca_com")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;

                    request.AddParameter("pc_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.Root DSPhat = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.Root>(b);
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
                            lstPC.Add(item);


                        }
                        dgv.ItemsSource = lstPC;
                    }
                }

            }
            catch
            {

            }
            //lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca sáng", ThoiGian = "Từ 08:00:00 - Đến 11:30:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca",Check="0" });
            //lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca trưa kinh doanh", ThoiGian = "Từ 11:30:00 - Đến 14:00:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            //lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca hành chính", ThoiGian = "Từ 08:00:00 - Đến 17:30:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "1" });
            //lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Part time sáng", ThoiGian = "Từ 08:00:00 - Đến 11:30:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            //lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca chiều", ThoiGian = "Từ 14:00:00 - Đến 18:00:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            //lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca gãy trưa", ThoiGian = "Từ 09:00:00 - Đến 15:00:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            //lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca 1", ThoiGian = "Từ 08:00:00 - Đến 18:00:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            //lsvCaLamViec.ItemsSource = lstCaLV;
            //dgv.ItemsSource = lstCaLV;
        }
        
        private void borCaLV_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.clsShift.Item calv = (sender as Border).DataContext as OOP.CaiDatLuong.clsShift.Item;
            if (calv != null)
            {
                IdCaLV = calv.shift_id.ToString();
            }
            

        }

        private void lsvCaLamViec_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCaLV.ScrollToVerticalOffset(scrollCaLV.VerticalOffset - e.Delta);
        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
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
            borNam.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
            borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
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
                //Main.Nam = th.nam;
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
            ApDung();
        }

        private void ApDung()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_phat_ca")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("pc_com", Main.IdAcount);
                    request.AddParameter("pc_shift", IdCaLV);
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
                        OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa pc = new OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa();
                        pc._id = add.newobj._id;
                        pc.pc_id = add.newobj.pc_id;
                        pc.pc_money = add.newobj.pc_money;
                        pc.pc_money_str = add.newobj.pc_money.ToString() + " VND/Ca";
                        pc.pc_time = add.newobj.pc_time;
                        pc.pc_time_s = add.newobj.pc_time.Day.ToString() + "/" + add.newobj.pc_time.Month + "/" + add.newobj.pc_time.Year;
                        pc.pc_shift = add.newobj.pc_shift;
                        foreach (var item in lstShift)
                        {
                            if (pc.pc_shift == item.shift_id)
                            {
                                pc.name_shift = item.shift_name;
                                pc.start_date = item.start_time;
                                pc.end_date = item.end_time;
                            }
                        }
                        pc.pc_com = add.newobj.pc_com;

                        pc.pc_type = add.newobj.pc_type;
                        lstPC.Add(pc);
                        dgv.ItemsSource = null;
                        dgv.ItemsSource = lstPC;
                    }

                }




            }
            catch
            {

            }
            borCaiDatMucPhat.Visibility = Visibility.Collapsed;
        }

        private void textXemMucPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa phat = (sender as Border).DataContext as OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa;
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
    }
}
