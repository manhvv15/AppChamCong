using ChamCong365.OOP.DeXuat.TGDuyetDX;
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

namespace ChamCong365
{
    //public class Time
    //{
    //    public string Name { get; set; }
    //    public int Id { get; set; }
    //    public string Image { get; set; }
    //    public string Money { get; set; }
    //    public string Advance { get; set; }
    //    public string Status { get; set; }
    //    public string Note { get; set; }
    //}
    //public class NhanVien
    //{
    //    public int STT { get; set; }
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public string Company { get; set; }
    //    public string Phongban { get; set; }
    //    public string To { get; set; }
    //    public string Nhom { get; set; }
    //    public string Chucvu { get; set; }
    //    public string Sex { get; set; }
    //    public string Namsinh { get; set; }
    //    public string Tinhtranghonnhan { get; set; }
    //    public string Thongtinlienhe { get; set; }
    //    public string Ngaybatdaulamviec { get; set; }
    //    public string Trinhdohocvan { get; set; }
    //    public string Kinhnghiemlamviec { get; set; }
    //}

    public partial class ucInstallTime : UserControl
    {
        public class TimeShift
        {
            public string IdCaLV { get; set; }
            public string Time { get; set; }
        }
        string s = "";
        private List<TimeShift> lst = new List<TimeShift>();
        private List<string> lstStr = new List<string>();
        public List<string> Minute = new List<string>();
        private string DuyetDXDotXuat = "";
        private string TGDuyet = "";
        private List<OOP.CaiDatLuong.clsShift.Item> lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();
        private List<OOP.DeXuat.clsThoiGianDuyetDX.TimeDx> lstDuyetDX = new List<OOP.DeXuat.clsThoiGianDuyetDX.TimeDx>();
        private MainWindow Main;
        public ucInstallTime(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            LoadDLCaLamViec();
            LoadDLTGDuyetDX();
            
            Load();
        }
        List<clsSuddenProposal> lstTimeDX = new List<clsSuddenProposal>();
        private async void LoadHourDX()
        {
            
            try
            {
                lstDuyetDX = new List<OOP.DeXuat.clsThoiGianDuyetDX.TimeDx>();
                var options = new RestClientOptions("http://210.245.108.202:3005")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/api/vanthu/setting/createF", Method.Post);
                request.AddHeader("Authorization", "Bearer " + Properties.Settings.Default.Token);
                RestResponse response = await client.ExecuteAsync(request);
                OOP.DeXuat.TGDuyetDX.clsSettingTimeDX.Root TGDuyetDX = JsonConvert.DeserializeObject<OOP.DeXuat.TGDuyetDX.clsSettingTimeDX.Root>(response.Content);
                if (TGDuyetDX.data != null)
                {
                    //string str = "{\"Id\":\"2025297\",\"Content\":\"10:10\"}";
                    //List<clsSuddenProposal> dx = JsonConvert.DeserializeObject<List<clsSuddenProposal>>(str);
                    string[] ii = TGDuyetDX.data.settingDx.time_limit_l.Split(new[] { "\"],[\"" }, StringSplitOptions.None);
                    foreach (string str in ii)
                    {

                        string[] oo = str.Split(new[] { "\",\"" }, StringSplitOptions.None);
                        clsSuddenProposal DX = new clsSuddenProposal();
                        //oo[0] = oo[0].Remove(oo[0].Length - 1);
                        DX.Id = oo[0];
                        DX.Content = oo[1];
                        lstTimeDX.Add(DX);

                    }
                    string[] aa = lstTimeDX[0].Id.Split(new[] { "[[\"" }, StringSplitOptions.None);
                    lstTimeDX[0].Id = aa[aa.Length - 1];
                    lstTimeDX[lstTimeDX.Count - 1].Content = lstTimeDX[lstTimeDX.Count - 1].Content.Remove(lstTimeDX[lstTimeDX.Count - 1].Content.Length - 1);
                    string[] bb = lstTimeDX[lstTimeDX.Count - 1].Content.Split(new[] { "\"]" }, StringSplitOptions.None);
                    if (bb[0].Contains(Convert.ToChar('"')))
                    {
                        bb[0].TrimStart('"');
                    }
                    lstTimeDX[lstTimeDX.Count - 1].Content = bb[0];
                }
                foreach (var clv in lstShift)
                {
                    foreach (var time in lstTimeDX)
                    {
                        if (clv.shift_id.ToString() == time.Id)
                        {
                            string[] h = time.Content.Split(Convert.ToChar(":"));
                            string[] hhh = h[0].Split(new[] { '\"'},StringSplitOptions.None);

                            clv.hour_agree_propose = hhh[0];
                            string[] mmm = h[h.Length - 1].Split(new[] { '\"' }, StringSplitOptions.None);
                            clv.minute_agree_propose = mmm[mmm.Length - 1];
                        }
                    }
                }
                lsvCaLV.ItemsSource = null;
                lsvCaLV.ItemsSource = lstShift;
            }
            catch
            {

            }
        }

        private async void LoadDLTGDuyetDX()
        {
            try
            {
                lstDuyetDX = new List<OOP.DeXuat.clsThoiGianDuyetDX.TimeDx>();
                var options = new RestClientOptions("http://210.245.108.202:3005")
                {
                    MaxTimeout = -1,
                };
                var client = new RestClient(options);
                var request = new RestRequest("/api/vanthu/setting/fetchTimeSetting", Method.Post);
                request.AddHeader("Authorization", "Bearer " + Properties.Settings.Default.Token);
                RestResponse response = await client.ExecuteAsync(request);
                OOP.DeXuat.clsThoiGianDuyetDX.Root DuyetDX = JsonConvert.DeserializeObject<OOP.DeXuat.clsThoiGianDuyetDX.Root>(response.Content);
                if (DuyetDX.time_dx != null)
                {
                    foreach (var item in DuyetDX.time_dx)
                    {
                        item.on_off = "1";
                        item.name_cate_dx_lo = item.name_cate_dx.ToLower();
                        if (item.id_dx == 1.ToString())
                        {
                            item.Text1 = "- Đề xuất nghỉ có kế hoạch là đề xuất được tạo bởi nhân viên với những lý do như nghỉ cưới, nghỉ đi học, nghỉ đi du lịch,... Trong đó người tạo chủ động báo trước với trưởng phòng để sắp xếp công việc";
                            item.Text2 = "- Thời gian duyệt đề xuất có kế hoạch là khoảng thời gian tối đa để lãnh đạo duyệt phép cho nhân viên kể từ thời điểm nhân viên tạo đề xuất xin nghỉ phép có kế hoạch. Nếu vượt quá khoảng thời gian này mà đề xuất không được lãnh đạo duyệt, hệ thống tính lương sẽ xét đó vào diện nghỉ sai quy định";
                            item.Text3 = "Lưu ý: ";
                            item.Text4 = "chỗ này nếu để trống, tức là không cài đặt thời gian thì lãnh đạo có thể duyệt phép bất cứ khi nào";
                        }
                        if (item.Text1 == null)
                        {
                            item.Text1 = "";
                        }
                        lstDuyetDX.Add(item);
                    }
                    lsvTGDuyetDX.ItemsSource = lstDuyetDX;
                }
            }
            catch
            {

            }
        }

        private void Load()
        {
            Hours = new List<string>();
            for (int i = 0; i <= 23; i++)
            {
                s = i.ToString();
                if (i < 10)
                {
                    s = "0" + i.ToString();
                }
                Hours.Add(s);

            }
            Minute = new List<string>();
            lsv.ItemsSource = Hours;
            for (int i = 0; i <= 59; i++)
            {
                s = i.ToString();
                if (i < 10)
                {
                    s = "0" + i.ToString();
                }
                Minute.Add(s);

            }
            lsv1.ItemsSource = Minute;
        }

        private async void LoadDLCaLamViec()
        {
            try
            {
                lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, "http://210.245.108.202:3000/api/qlc/shift/list");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                //Console.WriteLine(await response.Content.ReadAsStringAsync());
                OOP.CaiDatLuong.clsShift.Root CaLV = JsonConvert.DeserializeObject<OOP.CaiDatLuong.clsShift.Root>(await response.Content.ReadAsStringAsync());
                if (CaLV.data != null)
                {
                    foreach (var calv in CaLV.data.items)
                    {
                        
                        lstShift.Add(calv);
                        TimeShift ts = new TimeShift();
                        ts.IdCaLV = '"' + calv.shift_id.ToString() + '"';
                        ts.Time = '"' + calv.hour_agree_propose + ":" + calv.minute_agree_propose + '"';
                        lst.Add(ts);

                    }
                    foreach (var ls in lst)
                    {
                        lstStr.Add("[" + ls.IdCaLV + "," + ls.Time + "]");
                    }
                    string ds = "";
                    foreach (string str in lstStr)
                    {
                        ds += str + ",";
                    }
                    ds = ds.Remove(ds.Length - 1);
                    DuyetDXDotXuat = "[" + ds + "]";
                    lsvCaLV.ItemsSource = lstShift;
                    LoadHourDX();
                }
            }
            catch
            {

            }
        }
        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = ((Rectangle)sender).Parent as Grid;
            var bodPopUp = grid.Parent as Border;
            bodPopUp.Visibility = Visibility.Collapsed;
        }

        private void lsvCaLV_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }
        OOP.CaiDatLuong.clsShift.Item tp = new OOP.CaiDatLuong.clsShift.Item();
        private void borTG_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Load();
            tp = (sender as Border).DataContext as OOP.CaiDatLuong.clsShift.Item;
            if (tp != null)
            {
                Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset);
                //dgvPhatNV.ItemsSource = tp.tt_phat.ds_phat;
                borTGTr.VerticalAlignment = VerticalAlignment.Top;
                borTGTr.HorizontalAlignment = HorizontalAlignment.Left;
                borTGTr.Margin = new Thickness(Mouse.GetPosition(popup).X - 50, Mouse.GetPosition(popup).Y + 10, 0, 0);
                popup.Visibility = Visibility.Visible;
                borTGTr.Visibility = Visibility.Visible;
            }
        }

        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borTGTr.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
        }
        private int _demHours;

        public int demHours
        {
            get { return _demHours; }
            set { _demHours = value; }
        }
        private int _demMinutes;

        public int demMinutes
        {
            get { return _demMinutes; }
            set { _demMinutes = value; }
        }
        //public List<string> Hours { get; set;
        public List<string> Hours = new List<string>();
        private void lsv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                demHours++;
                if (demHours > 23)
                {
                    demHours = 1;
                }
            }
            if (e.Delta > 0)
            {
                demHours--;
                if (demHours < 1)
                {
                    demHours = 23;
                }
            }
            Hours.Clear();
            for (int i = 0; i < 7; i++)
            {
                if (i + demHours < 10)
                    Hours.Add("0" + (i + demHours));
                else
                {
                    if (i + demHours <= 23)
                        Hours.Add((i + demHours).ToString());
                    else
                    {
                        Hours.Add("0" + (i + demHours - 24));
                    }
                }
            }
            Hours = Hours.ToList();
            lsv.ItemsSource = Hours;
        }

        private void lsv1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta < 0)
            {
                demMinutes++;
                if (demMinutes > 59)
                {
                    demMinutes = 1;
                }
            }
            if (e.Delta > 0)
            {
                demMinutes--;
                if (demMinutes < 1)
                {
                    demMinutes = 59;
                }
            }
            Minute.Clear();
            for (int i = 0; i < 7; i++)
            {
                if (i + demMinutes < 10)
                    Minute.Add("0" + (i + demMinutes));
                else
                {
                    if (i + demMinutes <= 59)
                        Minute.Add((i + demMinutes).ToString());
                    else
                    {
                        Minute.Add("0" + (i + demMinutes - 60));
                    }
                }
            }
            Minute = Minute.ToList();
            lsv1.ItemsSource = Minute;
        }

        private void btnOnCoKH_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //spnDXCoKeHoach.Visibility = Visibility.Collapsed;
            //btnOffCoKH.Visibility = Visibility.Visible;
            //btnOnCoKH.Visibility = Visibility.Collapsed;
        }

        private void btnOffCoKH_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //spnDXCoKeHoach.Visibility = Visibility.Visible;
            //btnOffCoKH.Visibility = Visibility.Collapsed;
            //btnOnCoKH.Visibility = Visibility.Visible;
        }

        private void btnOnDXDX_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            spnDXDX.Visibility = Visibility.Collapsed;
            btnOffDXDX.Visibility = Visibility.Visible;
            btnOnDXDX.Visibility = Visibility.Collapsed;
        }

        private void btnOffDXDX_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnOnDXDX.Visibility = Visibility.Visible;
            btnOffDXDX.Visibility = Visibility.Collapsed;
            spnDXDX.Visibility = Visibility.Visible;
        }

        private void btnOnDXTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //btnOnDXTP.Visibility = Visibility.Collapsed;
            //btnOffDXTP.Visibility = Visibility.Visible;
            //spnTGThuongPhat.Visibility = Visibility.Collapsed;
        }
        private void btnOffDXTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //btnOnDXTP.Visibility = Visibility.Visible;
            //btnOffDXTP.Visibility = Visibility.Collapsed;
            //spnTGThuongPhat.Visibility = Visibility.Visible;
        }

        private void btnOnHHDT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //btnOnHHDT.Visibility = Visibility.Collapsed;
            //btnOffHHDT.Visibility = Visibility.Visible;
            //spnTGHHDT .Visibility = Visibility.Collapsed;
        }

        private void btnOffHHDT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //btnOnHHDT.Visibility = Visibility.Visible;
            //btnOffHHDT.Visibility = Visibility.Collapsed;
            //spnTGHHDT.Visibility = Visibility.Visible;
        }
        private string test;
        private void lsv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lst = new List<TimeShift>();
                lstStr = new List<string>();
                foreach (var item in lstShift)
                {
                    if (tp.shift_id == item.shift_id)
                    {
                        if (lsv.SelectedItem != null)
                        {
                            item.hour_agree_propose = lsv.SelectedItem.ToString();
                        }
                    }
                    TimeShift ts = new TimeShift();
                    ts.IdCaLV = '"' + item.shift_id.ToString() + '"';
                    ts.Time = '"' + item.hour_agree_propose + ":" + item.minute_agree_propose + '"';
                    lst.Add(ts);
                }
                foreach (var ls in lst)
                {
                    lstStr.Add("[" + ls.IdCaLV + "," + ls.Time + "]");
                }
                string ds = "";
                foreach (string str in lstStr)
                {
                    ds += str + ",";
                }
                ds = ds.Remove(ds.Length - 1);
                DuyetDXDotXuat = "[" + ds + "]";

                lsvCaLV.ItemsSource = null;
                lsvCaLV.ItemsSource = lstShift;
            }
            catch
            {

            }
        }

        private void lsv1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                lst = new List<TimeShift>();
                lstStr = new List<string>();
                foreach (var item in lstShift)
                {
                    if (tp.shift_id == item.shift_id)
                    {
                        if (lsv1.SelectedItem != null)
                        {
                            item.minute_agree_propose = lsv1.SelectedItem.ToString();
                        }
                    }
                    TimeShift ts = new TimeShift();
                    ts.IdCaLV = '"' + item.shift_id.ToString() + '"';
                    ts.Time = '"' + item.hour_agree_propose + ":" + item.minute_agree_propose + '"';
                    lst.Add(ts);
                }
                foreach (var ls in lst)
                {
                    lstStr.Add("[" + ls.IdCaLV + "," + ls.Time + "]");
                }
                string ds = "";
                foreach (string str in lstStr)
                {
                    ds += str + ",";
                }
                ds = ds.Remove(ds.Length - 1);
                DuyetDXDotXuat = "[" + ds + "]";
                lsvCaLV.ItemsSource = null;
                lsvCaLV.ItemsSource = lstShift;
            }
            catch
            {

            }
        }

        private void btnCapNhatTGDeXuatCoKH_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.DeXuat.clsThoiGianDuyetDX.TimeDx dx = (sender as Border).DataContext as OOP.DeXuat.clsThoiGianDuyetDX.TimeDx;
            if (dx != null)
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3005/api/vanthu/setting/updateTimeSetting")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddHeader("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    request.AddParameter("id_dx", dx.id_dx);
                    request.AddParameter("time", TGDuyet);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    Main.grShowPopup.Children.Add(new Popup.DeXuat.CaiDatThoiGian.PopUpThongBao());
                }
            }
            
        }

        private void btnCapNhatTGDXTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (textTgDuyetDXThuongP.Text == "")
            //{
            //    textTBTP.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    textTBTP.Visibility = Visibility.Collapsed;
            //    try
            //    {
            //        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3005/api/vanthu/setting/editSetting")))
            //        {
            //            RestRequest request = new RestRequest();
            //            request.Method = Method.Post;
            //            request.AlwaysMultipartFormData = true;
            //            request.AddHeader("Authorization", "Bearer " + Properties.Settings.Default.Token);
            //            request.AddParameter("time_tp", textTgDuyetDXThuongP.Text);
            //            RestResponse resAlbum = restclient.Execute(request);
            //            var b = resAlbum.Content;
            //            Main.grShowPopup.Children.Add(new Popup.DeXuat.CaiDatThoiGian.PopUpThongBao());
            //        }
            //    }
            //    catch
            //    {

            //    }

            //}
        }

        private void btnCapNhatHHDT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //if (textHHDT.Text == "")
            //{
            //    textTBHHDT.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    textTBHHDT.Visibility = Visibility.Collapsed;
            //    try
            //    {
            //        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3005/api/vanthu/setting/editSetting")))
            //        {
            //            RestRequest request = new RestRequest();
            //            request.Method = Method.Post;
            //            request.AlwaysMultipartFormData = true;
            //            request.AddHeader("Authorization", "Bearer " + Properties.Settings.Default.Token);
            //            request.AddParameter("time_hh", textHHDT.Text);
            //            RestResponse resAlbum = restclient.Execute(request);
            //            var b = resAlbum.Content;
            //            Main.grShowPopup.Children.Add(new Popup.DeXuat.CaiDatThoiGian.PopUpThongBao());
            //        }
            //    }
            //    catch
            //    {

            //    }

            //}
        }

        private void btnCaiDatDotXuat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3005/api/vanthu/setting/editSetting")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddHeader("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    request.AddParameter("time_limit_l", DuyetDXDotXuat);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    Main.grShowPopup.Children.Add(new Popup.DeXuat.CaiDatThoiGian.PopUpThongBao());
                }
            }
            catch
            {

            }
        }

        private void lsvTGDuyetDX_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnOnDX_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.DeXuat.clsThoiGianDuyetDX.TimeDx dx = (sender as Border).DataContext as OOP.DeXuat.clsThoiGianDuyetDX.TimeDx;
            if (dx != null)
            {
                dx.on_off = "0";
                //foreach (var item in lstDuyetDX)
                //{
                //    if (item.id_dx == dx.id_dx)
                //    {
                        
                //    }
                //}
                //lstDuyetDX = lstDuyetDX.ToList();
                
            }
        }

        private void btnOffDX_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.DeXuat.clsThoiGianDuyetDX.TimeDx dx = (sender as Border).DataContext as OOP.DeXuat.clsThoiGianDuyetDX.TimeDx;
            if (dx != null)
            {
                dx.on_off = "1";
            }
        }
        
        private void textTGDuyetDXCCH_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            TGDuyet = tb.Text;
        }
    }
}