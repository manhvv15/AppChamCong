using ChamCong365.NhanVien.ChamCongKhuonMat.Function;
using ChamCong365.OOP.NhanVien.ChamCongNhanVien;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for chamCong.xaml
    /// </summary>
    public partial class ChamCongNv : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MainChamCong Main;
        MainWindow Main1;
        public ChamCongNv(MainChamCong main, MainWindow main1)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            Main1 = main1;
            txtNameSaff.Text = Main.txbNameAccount.Text;
            getData();
            //LoadListTimeKeepingForDay();
        }
        private List<Item_LichSuDiemDanh> _lstLichSuDiemDanh;
        public List<Item_LichSuDiemDanh> lstLichSuDiemDanh
        {
            get { return _lstLichSuDiemDanh; }
            set { _lstLichSuDiemDanh = value; OnPropertyChanged(); }
        }

        private List<Item_QuanLy_CaLamViec> _listCa;
        public List<Item_QuanLy_CaLamViec> listCa
        {
            get { return _listCa; }
            set { _listCa = value; OnPropertyChanged(); }
        }

        private async Task<Root_QuanLy_CaLamViec> getCa()
        {
            HttpClient http = new HttpClient();
            var apilink = APIs.API.list_shifts_api;
            var pram = new Dictionary<string, string>();
         
            http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
            apilink += "?com_id=" + Main.Ep_Id;
                         
            HttpResponseMessage respon = await http.GetAsync(apilink);
            Root_QuanLy_CaLamViec api = JsonConvert.DeserializeObject<Root_QuanLy_CaLamViec>(respon.Content.ReadAsStringAsync().Result);
            if (api.data != null) return api;
            return null;
        }

        private async Task<Root_LichSuDiemDanh> getDiemDanh()
        {
            try
            {
                HttpClient http = new HttpClient();
              
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                       
                var apilink = APIs.API.LichSuChamCong_Api;
                var form = new Dictionary<string, string>();
                form.Add("ep_id", Main.Ep_Id.ToString());
                form.Add("page", "1");
                form.Add("pageSize", "10");
                HttpResponseMessage respon = await http.PostAsync(apilink, new FormUrlEncodedContent(form));
                Root_LichSuDiemDanh api = JsonConvert.DeserializeObject<Root_LichSuDiemDanh>(respon.Content.ReadAsStringAsync().Result);
                if (api.data != null) return api;
                return null;
            }
            catch (Exception ex)
            {
                var x = new Popup.ChamCong.Comon.Notify1();
                x.Type = Popup.ChamCong.Comon.Notify1.NotifyType.Success;
                x.Message = ex.Message;
                Main.grShowPopup.Children.Add(x);
                return null;
            }
        }

        private async Task<Root_LichSuDiemDanh> getDiemDanhBD(string startdate, string enddate, int length = 1)
        {
            try
            {
                HttpClient http = new HttpClient();
              
                http.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                   
                var apilink = string.Format("http://210.245.108.202:3000/api/qlc/timekeeping/get_history_time_keeping_by_company?start_date={0}&end_date={1}&length={2}", startdate, enddate, length);
                var form = new Dictionary<string, string>();
                form.Add("ep_id", Main.Ep_Id.ToString());
                form.Add("start_date", startdate);
                form.Add("end_date", enddate);
                form.Add("page", "1");
                form.Add("pageSize", length.ToString());
                HttpResponseMessage respon = await http.PostAsync(apilink, new FormUrlEncodedContent(form));
                Root_LichSuDiemDanh api = JsonConvert.DeserializeObject<Root_LichSuDiemDanh>(respon.Content.ReadAsStringAsync().Result);
                if (api.data != null) return api;
                return null;
            }
            catch (Exception ex)
            {
                var x = new Popup.ChamCong.Comon.Notify1();
                x.Type = Popup.ChamCong.Comon.Notify1.NotifyType.Success;
                x.Message = ex.Message;
                Main.grShowPopup.Children.Add(x);
                return null;
            }
        }

        public DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }

        private async void getData()
        {
            var T2 = StartOfWeek(DateTime.Today, DayOfWeek.Monday);
            var T3 = T2.AddDays(1);
            var T4 = T3.AddDays(1);
            var T5 = T4.AddDays(1);
            var T6 = T5.AddDays(1);
            var T7 = T6.AddDays(1);
            var CN = T7.AddDays(1);

            tblDDLate.Text = tblDDError.Text = tblDDMonth.Text = "0 lần";
            var startMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var endMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month));

            var listDiemDanhMonth = new List<Item_LichSuDiemDanh>();
            var listAllCa = new List<Item_QuanLy_CaLamViec>();

            var k1 = getDiemDanhBD(startMonth.ToString("yyyy-MM-dd"), endMonth.ToString("yyyy-MM-dd")).ContinueWith(tt => this.Dispatcher.Invoke(() =>
            {
                if (tt.Result != null)
                {
                    var late = new List<Item_LichSuDiemDanh>();
                    int dem = int.Parse(tt.Result.data.totalItems.ToString());

                    getDiemDanhBD(startMonth.ToString("yyyy-MM-dd"), endMonth.ToString("yyyy-MM-dd"), dem).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                    {
                        if (zz.Result != null)
                        {
                            listDiemDanhMonth = zz.Result.data.items;
                            int error = 0;
                            int monthdd = 0;
                            listDiemDanhMonth.ForEach(l =>
                            {
                                if (l.is_success.ToString() == "0") error++;
                                if (l.is_success.ToString() == "1") monthdd++;

                            });
                            tblDDError.Text = error.ToString() + " lần";
                            tblDDMonth.Text = monthdd.ToString() + " lần";

                            getCa().ContinueWith(cc => this.Dispatcher.Invoke(() =>
                            {
                                if (cc.Result != null) listAllCa = cc.Result.data.items;
                                int DDlate = 0;
                                listDiemDanhMonth.RemoveAll(x => x.is_success.ToString() == "0");
                                for (int i = 0; i < (endMonth - startMonth).TotalDays; i++)
                                {
                                    DateTime setday = startMonth.AddDays(i);
                                    var listTg = listDiemDanhMonth.Where(x => x.At_time.ToString("yyyy-MM-dd") == setday.ToString("yyyy-MM-dd")).ToList();
                                    if (listTg != null)
                                    {
                                        var ca_in_day = new List<Item_QuanLy_CaLamViec>();
                                        listTg.ForEach(x =>
                                        {
                                            var ca = listAllCa.SingleOrDefault(uu => uu.shift_id == x.shift_id); 
                                            if (ca != null)
                                            {
                                                if (!ca_in_day.Any(z => z.shift_id == x.shift_id))
                                                {
                                                    ca_in_day.Add(ca);
                                                }
                                            }

                                        });
                                        ca_in_day = ca_in_day.OrderBy(x => x.start_timex).ToList();
                                        ca_in_day.ForEach(shift =>
                                        {
                                            var ca = listAllCa.SingleOrDefault(x => x.shift_id == shift.shift_id);
                                            if (ca != null)
                                            {
                                                var listDDinCa = listTg.Where(x => x.shift_id == shift.shift_id).ToList();
                                                listDDinCa = listDDinCa.OrderBy(x => x.At_time).ToList();
                                                if (listDDinCa != null)
                                                {
                                                    DateTime start, startlate;
                                                    DateTime end, endlate;
                                                    bool vao, ra;
                                                    vao = ra = false;
                                                    List<Item_LichSuDiemDanh> seen = new List<Item_LichSuDiemDanh>();
                                                    if ((string.IsNullOrEmpty(ca.start_time_latest) || ca.start_time_latest == "00:00:00") && !string.IsNullOrEmpty(ca.start_time) && DateTime.TryParse(ca.start_time, out start))
                                                    {
                                                        if (listDDinCa.Count > 0)
                                                        {
                                                            foreach (var item in listDDinCa)
                                                            {
                                                                if (!seen.Contains(item)) seen.Add(item);
                                                                if (item.At_time.TimeOfDay <= start.TimeOfDay)
                                                                {
                                                                    vao = true;
                                                                    break;
                                                                }
                                                            }
                                                            if (!vao) DDlate++;
                                                        }
                                                    }
                                                    else if (!string.IsNullOrEmpty(ca.start_time_latest) && ca.start_time_latest != "00:00:00" && !string.IsNullOrEmpty(ca.start_time) && DateTime.TryParse(ca.start_time, out start) && DateTime.TryParse(ca.start_time_latest, out startlate))
                                                    {
                                                        if (listDDinCa.Count > 0)
                                                        {
                                                            foreach (var item in listDDinCa)
                                                            {
                                                                if (!seen.Contains(item)) seen.Add(item);
                                                                if (item.At_time.TimeOfDay >= startlate.TimeOfDay && item.At_time.TimeOfDay <= start.TimeOfDay)
                                                                {
                                                                    vao = true;
                                                                    break;
                                                                }
                                                            }
                                                            if (!vao) DDlate++;
                                                        }
                                                    }
                                                    listDDinCa.Reverse();
                                                    seen.ForEach(s =>
                                                    {
                                                        listDDinCa.RemoveAll(k => k.sheet_id == s.sheet_id);
                                                    });
                                                    if (!string.IsNullOrEmpty(ca.end_time) && (string.IsNullOrEmpty(ca.end_time_earliest) || ca.end_time_earliest == "00:00:00") && DateTime.TryParse(ca.end_time, out end))
                                                    {
                                                        if (listDDinCa.Count > 0)
                                                        {
                                                            foreach (var item in listDDinCa)
                                                            {
                                                                if (item.At_time.TimeOfDay >= end.TimeOfDay)
                                                                {
                                                                    ra = true;
                                                                    break;
                                                                }
                                                            }
                                                            if (!ra) DDlate++;
                                                        }
                                                    }
                                                    else if (!string.IsNullOrEmpty(ca.end_time) && string.IsNullOrEmpty(ca.end_time_earliest) && ca.end_time_earliest != "00:00:00" && DateTime.TryParse(ca.end_time, out end) && DateTime.TryParse(ca.end_time_earliest, out endlate))
                                                    {
                                                        if (listDDinCa.Count > 0)
                                                        {
                                                            foreach (var item in listDDinCa)
                                                            {
                                                                if (item.At_time.TimeOfDay >= end.TimeOfDay && item.At_time.TimeOfDay <= endlate.TimeOfDay)
                                                                {
                                                                    ra = true;
                                                                    break;
                                                                }
                                                            }
                                                            if (!ra) DDlate++;
                                                        }
                                                    }
                                                }
                                            }
                                        });
                                    }

                                }
                                tblDDLate.Text = DDlate.ToString() + " lần";
                            }));
                        }
                    }));
                }
            }));

            // diem danh gan day
            getDiemDanh().ContinueWith(tt => this.Dispatcher.Invoke(() =>
            {
                if (tt.Result != null)
                {
                    lstLichSuDiemDanh = tt.Result.data.items;
                    lsvLichSuDiemDanh.ItemsSource = lstLichSuDiemDanh;
                }
            }));
            //bieu do diem danh theo tuan
            getDiemDanhBD(T2.ToString("yyyy-MM-dd"), CN.ToString("yyyy-MM-dd")).ContinueWith(tt => this.Dispatcher.Invoke(() =>
            {
                if (tt.Result != null)
                {
                    int dem = int.Parse(tt.Result.data.totalItems.ToString());
                    getDiemDanhBD(T2.ToString("yyyy-MM-dd"), CN.ToString("yyyy-MM-dd"), dem).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                    {
                        if (zz.Result != null)
                        {
                            var list = zz.Result.data.items;
                            list.RemoveAll(i => i.is_success.ToString() == "0");
                            var dembd = new List<int>() { 0, 0, 0, 0, 0, 0, 0 };
                            var col = new List<Rectangle>() { DDT2, DDT3, DDT4, DDT5, DDT6, DDT7, DDCN, };
                            list.ForEach(g =>
                            {
                                if (g.date == T2.ToString("dd-MM-yyyy")) dembd[0]++;
                                if (g.date == T3.ToString("dd-MM-yyyy")) dembd[1]++;
                                if (g.date == T4.ToString("dd-MM-yyyy")) dembd[2]++;
                                if (g.date == T5.ToString("dd-MM-yyyy")) dembd[3]++;
                                if (g.date == T6.ToString("dd-MM-yyyy")) dembd[4]++;
                                if (g.date == T7.ToString("dd-MM-yyyy")) dembd[5]++;
                                if (g.date == CN.ToString("dd-MM-yyyy")) dembd[6]++;
                            });
                            for (int i = 0; i < col.Count; i++)
                            {
                                if (dembd[i] > 0) col[i].Visibility = Visibility.Visible;
                                if (dembd[i] > 0 && dembd[i] < 5)
                                {
                                    Grid.SetRow(col[i], Grid.GetRow(col[i]) - dembd[i] + 1);
                                    Grid.SetRowSpan(col[i], dembd[i]);
                                }
                                if (dembd[i] > 0 && dembd[i] >= 5)
                                {
                                    Grid.SetRow(col[i], Grid.GetRow(col[i]) - 5 + 1);
                                    Grid.SetRowSpan(col[i], 5);
                                }
                            }
                        }
                    }));
                }
            }));
        }

       

        //public async void LoadListTimeKeepingForDay()
        //{
        //    try
        //    {
        //        var client = new HttpClient();
        //        var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.LichSuChamCong_Api);
        //        request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
        //        var content = new MultipartFormDataContent();
        //        content.Add(new StringContent(Main.Ep_Id.ToString()), "ep_id");
        //        content.Add(new StringContent("1"), "page");
        //        content.Add(new StringContent("10"), "pageSize");
        //        request.Content = content;
        //        var response = await client.SendAsync(request);
        //        response.EnsureSuccessStatusCode();
        //        var resContent = await response.Content.ReadAsStringAsync();

        //        Root_LichSuDiemDanh result = JsonConvert.DeserializeObject<Root_LichSuDiemDanh>(resContent);

        //        if (result.data.items != null)
        //        {
        //            lstLichSuDiemDanh = result.data.items;
        //            lsvLichSuDiemDanh.ItemsSource = lstLichSuDiemDanh;
        //        }
        //    }
        //    catch (Exception)
        //    {
        //    }
        //}


        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ChupAnh uc = new ChupAnh(Main);
           // CapNhatKhuonMat uc = new CapNhatKhuonMat(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            ChamCongKhuonMatNhanVien uc = new ChamCongKhuonMatNhanVien(Main,Main1);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

      
    }
}
