using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CauHinhChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ChamCongKhuonMat;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ChamCongKhuonMat;
//using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping
{
    /// <summary>
    /// Interaction logic for ChamCong_Main.xaml
    /// </summary>
    public partial class ChamCong_Main : System.Windows.Controls.Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public MainWindow Main { get; set; }
       
        public ChamCong_Main(MainWindow main)
        {
            try
            {
                InitializeComponent();
                this.DataContext = this;
                Main = main;
                getInfoChamCong();
                if (IsWebCamInUse())
                {
                    var popNhanDien = new Error_NhanDien(this, Main);
                    popNhanDien.Message = "Thiết bị ghi hình đang được sử dụng bởi một ứng dụng khác, hãy thử tắt các thiết bị phần mềm đang sử dụng thiết bị ghi hình và thử chấm công lại";
                    popNhanDien.Message2 = "";
                    Popup.NavigationService.Navigate(popNhanDien);
                    PopupChamCong.Visibility = Visibility.Visible;
                }   
                 getComDetail().ContinueWith(tt => this.Dispatcher.Invoke(() =>
                 {
                    if (tt.Result != null)
                    {
                        switch (main.Type)
                        {
                            case 1:
                                if (tt.Result.data.data.type_timekeeping.Contains("4") == false)
                                {
                                    block = true;
                                    if (cam != null && cam.IsRunning) cam.Stop();
                                    if (timer != null) timer.Stop();
                                    var y = new Popup.ChamCong.Comon.Notify1();
                                    y.Type = QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon.Notify1.NotifyType.Error;
                                    y.Message = "Chức năng này đang tạm khoá!<br>Vui lòng liên hệ nhân sự để được hướng dẫn thêm";
                                    Popup.NavigationService.Navigate(y);
                                    PopupChamCong.Visibility = Visibility.Visible;
                                }
                                break;
                            case 2:
                                if (tt.Result.data.data.type_timekeeping.Contains("3") == false)
                                {
                                    block = true;
                                    if (cam != null && cam.IsRunning) cam.Stop();
                                    if (timer != null) timer.Stop();
                                    var y = new QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon.Notify1();
                                    y.Type = QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon.Notify1.NotifyType.Error;
                                    y.Message = "Chức năng này đang tạm khoá!<br>Vui lòng liên hệ nhân sự để được hướng dẫn thêm";
                                    Popup.NavigationService.Navigate(y);
                                    PopupChamCong.Visibility = Visibility.Visible;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                 }));
                startCapture(false);
                Main.videoSource = cam;
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public bool IsWebCamInUse()
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam\NonPackaged"))
                {
                    if (key != null && key.GetSubKeyNames() != null)
                        foreach (var subKeyName in key.GetSubKeyNames())
                        {
                            using (var subKey = key.OpenSubKey(subKeyName))
                            {
                                if (subKey.GetValueNames().Contains("LastUsedTimeStop"))
                                {
                                    var endTime = subKey.GetValue("LastUsedTimeStop") is long ? (long)subKey.GetValue("LastUsedTimeStop") : -1;
                                    if (endTime <= 0)
                                    {
                                        return true;
                                    }
                                    else
                                    {
                                        return false;
                                    }
                                }
                            }
                        }
                    else return false;
                }
            }
            catch (Exception exception)
            {
                System.Windows.MessageBox.Show(exception.Message);
            }
            return false;
        }
        private async Task<API_Com_ChiTiet> getComDetail()
        {
            try
            {
                string apilink = "http://210.245.108.202:3000/api/qlc/company/info";
                HttpClient httpClient = new HttpClient();
                Dictionary<string, string> form = new Dictionary<string, string>();

                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                form.Add("com_id", Main.IdAcount.ToString());

                int i = 0;
                List<ChildCompany> list = new List<ChildCompany>();

                var respon = await httpClient.PostAsync(apilink, new FormUrlEncodedContent(form));
                API_Com_ChiTiet api = JsonConvert.DeserializeObject<API_Com_ChiTiet>(respon.Content.ReadAsStringAsync().Result);
                if (api.data != null) return api;
                return null;
            }
            catch (Exception ex)
            {

                var x = new QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon.Notify1();
                x.Type = QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.Comon.Notify1.NotifyType.Error;
                x.Message = ex.Message;
                return null;
            }
        }
        private bool block = false;
        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            try
            {
                if (e.Status == GeoPositionStatus.Ready)
                {
                    if (watcher.Position.Location.IsUnknown)
                    {
                        latitude = "0";
                        longitute = "0";
                    }
                    else
                    {
                        latitude = watcher.Position.Location.Latitude.ToString();
                        longitute = watcher.Position.Location.Longitude.ToString();
                    }
                }
                else
                {
                    latitude = "0";
                    longitute = "0";
                }
            }
            catch (Exception)
            {
                latitude = "0";
                longitute = "0";
            }
        }

        private void Start_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startChamCong();
        }

        public async void startChamCong()
        {
            PopupChamCong.Visibility = Visibility.Collapsed;
            {
                try
                {
                    if (cam == null)
                    {
                        cam = new VideoCaptureDevice(divices[0].MonikerString);
                        cam.NewFrame += new NewFrameEventHandler(Cam_NewFrame);
                    }
                    if (cam != null && !cam.IsRunning) cam.Start();
                    tick = 3;
                    Main.videoSource = cam;
                    if (timer != null)
                    {
                        timer.Stop();
                        Main.videoSource = cam;
                    }
                    timer = new DispatcherTimer();
                    PageChamCongStart.Visibility = Visibility.Collapsed;
                    //bor_PageChamCongAll.Visibility = Visibility.Hidden;
                    PageChamCongMain.Visibility = Visibility.Visible;
                    timer.Interval = new System.TimeSpan(0, 0, 0, 0, 1000);
                    timer.Tick += Timer_Tick;
                    timer.Start();
                    while (true)
                    {
                        ipWifi = "";
                        Main.videoSource = cam;
                        string externalIpString = await new HttpClient().GetStringAsync("http://checkip.dyndns.org/");
                        externalIpString = externalIpString.Replace("Current IP Address: ", "").Replace("\\r\\n", "").Replace("\\n", "").Trim();

                        var x = externalIpString.Split('.');
                        var x1 = x[0].Substring(x[0].LastIndexOf('>') + 1);
                        var x2 = x[3].Substring(0, x[3].IndexOf("<"));

                        externalIpString = String.Format("{0}:{1}:{2}:{3}", x1, x[1], x[2], x2);
                        ipWifi = (externalIpString);
                        break;
                    }
                }
                catch { }
            }
        }
     
        private BitmapImage bitmapFace;

        public BitmapImage BitmapFace
        {
            get { return bitmapFace; }
            set
            {
                bitmapFace = value;

                OnPropertyChanged("BitmapFace");
            }
        }
        
        public Action Success
        {
            get { return (Action)GetValue(SuccessProperty); }
            set { SetValue(SuccessProperty, value); }
        }
        public static readonly DependencyProperty SuccessProperty = DependencyProperty.Register("Success", typeof(Action), typeof(ChamCong_Main));

        DispatcherTimer timer;
        public FilterInfoCollection divices;
        VideoCaptureDevice cam;
        int tick = 5;
        public string latitude;
        public string longitute;
        public string ipWifi;
        public GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
        string TenNhanVien;
        public async void DiemDanh1(string img,string Id_NhanVien)
        {
            try
            {
                TenNhanVien = "";
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/timekeeping/create/winform");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                //content.Add(new StringContent("Winform"), "device");
                content.Add(new StringContent(Id_NhanVien), "listIds");
                content.Add(new StringContent(img), "img");
                content.Add(new StringContent(ipWifi.Replace(":", ".")), "ip");
                content.Add(new StringContent(latitude), "lat");
                content.Add(new StringContent(longitute), "long");
                content.Add(new StringContent(DateTime.Now.ToString()), "time");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resConten = await response.Content.ReadAsStringAsync();
                    Root_ThanhCong result = JsonConvert.DeserializeObject<Root_ThanhCong>(resConten);
                    if (result.data.result == true)
                    {
                        foreach (var item in lstInforSaft)
                        {
                            if (Id_NhanVien == item.idQLC.ToString())
                            {
                                TenNhanVien = item.userName;
                            }
                        }
                        Main.grShowPopup.Children.Add(new ucThongBaoThanhCong(Main, TenNhanVien));
                        //bor_TimeTick.Visibility = Visibility.Collapsed;
                        //startChamCong();
                    }
                    else
                    {
                        bor_TimeTick.Visibility = Visibility.Collapsed;
                        startCapture(true);
                    }
                    bor_TimeTick.Visibility = Visibility.Collapsed;
                    startChamCong();
                }
                else
                {
                    bor_TimeTick.Visibility = Visibility.Visible;
                    startCapture(true);
                }
            }
            catch (Exception)
            {
                bor_TimeTick.Visibility = Visibility.Visible;
                startCapture(true);
            }
        }
        public async Task DiemDanh(string shiftId, APICheckFace infoFace)
        {
            try
            {
                using (MemoryStream bmp = new MemoryStream())
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",  Properties.Settings.Default.Token);
                    MultipartFormDataContent formData = new MultipartFormDataContent();
                    formData.Add(new StreamContent(new MemoryStream(Convert.FromBase64String(infoFace.image))), "data_img", "image_" + DateTime.Now.Ticks + ".jpg");
                    formData.Add(new StringContent(infoFace.company_id), "company_id");
                    formData.Add(new StringContent(infoFace.user_id), "user_id");
                    if (latitude != null) formData.Add(new StringContent(latitude), "ts_lat");
                    if (longitute != null) formData.Add(new StringContent(longitute), "ts_long");
                    formData.Add(new StringContent(ipWifi.Replace(":", ".")), "wifi_ip");
                    formData.Add(new StringContent(shiftId), "shift_id");
                    formData.Add(new StringContent(Main.Type + ""), "type");

                    var respon = await httpClient.PostAsync(APIs.API.DiemDanh_KhuonMat_Api, formData);
                    APICheckFace result = JsonConvert.DeserializeObject<APICheckFace>(respon.Content.ReadAsStringAsync().Result);
                    if (result.status.Equals("true"))
                    {
                        var x = new DiemDanh_TC_TB(this,Main);
                        x.Avatar = BitmapFace;
                        x.Type = true;
                        Popup.NavigationService.Navigate(x);
                        PopupChamCong.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        var x = new DiemDanh_TC_TB(this,Main);
                        x.Message = result.mess;
                        x.Avatar = BitmapFace;
                        x.Type = false;
                        Popup.NavigationService.Navigate(x);
                        PopupChamCong.Visibility = Visibility.Visible;
                    }
                }
            }
            catch (System.Exception ex)
            {

                System.Windows.MessageBox.Show(ex.Message);
            }
        }

        public static byte[] ImageToByte(Bitmap img)
        {
            using (var stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Jpeg);

                return stream.ToArray();
            }
        }
        List<Data_ChamCong> lstResultCheckFacke = new List<Data_ChamCong>();
        class Data
        {
            public string company_id { get; set; }
            public string image { get; set; }
        }
        List<Data> data = new List<Data>();
        string image;
        public async void CheckFacke2()
        {
            try
            {
                image = "";
                data.Clear();
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(BitmapFace));
                enc.Save(bmp);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(bmp);
                data.Add(new Data() { company_id = Main.IdAcount.ToString(), image = "data:image/jpeg;base64," + Convert.ToBase64String((ImageToByte(bitmap))) });
                HttpClient httpClient = new HttpClient();
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://43.239.223.11:1900/verify_multi_no_direct");
                request.Content = new StringContent(JsonConvert.SerializeObject(data), System.Text.Encoding.UTF8, "application/json");
                var check = JsonConvert.SerializeObject(data);
                var response = await httpClient.SendAsync(request);
                var result = response.Content.ReadAsStringAsync().Result.Replace("\n", "");
                var res = result.Replace(" ", "");
                var res1 = res.Replace("[", "");
                var res2 = res1.Replace("]", "");
                Data_ChamCong userInfor = JsonConvert.DeserializeObject<Data_ChamCong>(res2);
                if (userInfor != null)
                {
                    string Id_NhanVien = userInfor.user_id;
                   
                    foreach (var item in data)
                    {
                        image = item.image;
                    }
                    DiemDanh1(image, Id_NhanVien);
                }
                else
                {
                    bor_TimeTick.Visibility = Visibility.Collapsed;
                    startCapture(true);
                }
            }
            catch (Exception)
            {
                bor_TimeTick.Visibility = Visibility.Collapsed;
                startCapture(true);
            }
        }

        MemoryStream bmp = new MemoryStream();
        private void Timer_Tick(object sender, System.EventArgs e)
        {
            try
            {
                if (tick > 0)
                {
                    tblTimer.Visibility = Visibility.Visible;
                    tblTimer.Text = "Nhận diện trong " + tick.ToString();
                }
                else if (cam != null)
                {
                    if (cam.IsRunning)
                    {
                        //cam.SignalToStop();
                        CheckFacke2();
                    }
                    else
                    {
                        cam.Stop();
                        timer.Stop();
                    }
                    tblTimer.Visibility = Visibility.Collapsed;
                }
                tick--;
            }
            catch (Exception)
            {} 
        }

        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstInforSaft = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        public async void getInfoChamCong()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/list_em")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddHeader("Authorization", Properties.Settings.Default.Token);
                    request.AddParameter("id_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = await restclient.ExecuteAsync(request);
                    var b = resAlbum.Content;
                    OOP.clsNhanVienThuocCongTy.Root NVthuocCty = JsonConvert.DeserializeObject<OOP.clsNhanVienThuocCongTy.Root>(b);
                    if (NVthuocCty != null)
                    {
                        lstInforSaft = NVthuocCty.data.listUser;
                       
                    }
                }
            }
            catch
            {

            }
        }
       
        public async Task startCapture(bool flag)
        {
            watcher.StatusChanged += Watcher_StatusChanged ;
            watcher.Start();
            divices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (divices.Count > 0)
            {
                cam = new VideoCaptureDevice(divices[0].MonikerString);
                cam.NewFrame += new NewFrameEventHandler(Cam_NewFrame);
                cam.Start();
                Main.videoSource = cam;
            }
            else
            {
                var x = new Popup.ChamCong.ChamCongKhuonMat.Error_NhanDien(this, Main);
                x.Message = "Không tìm thấy thiết bị ghi hình được kết nối, Hãy thử bật thiết bị ghi hình của bạn hoặc kết nối với thiết bị ghi hình dời và thử chấm công lại";
                x.Message2 = "";
                Popup.NavigationService.Navigate(x);
                PopupChamCong.Visibility = Visibility.Visible;
            }
            if (flag)
            {
                startChamCong();
                Main.videoSource = cam;
            }
        }
       
        private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            try
            {
                var image = (Bitmap)eventArgs.Frame.Clone();
                if (image != null)
                {
                    var filter = new Mirror(false, true);
                    filter.ApplyInPlace(image);
                    webcamImage.Dispatcher.Invoke(() => webcamImage.Source = BitmapFace = BitmapToImageSource(image));
                }
                else
                {
                    System.Windows.MessageBox.Show("lỗi");
                }
            }
            catch { }

        }
        private static System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, System.Drawing.Size size)
        {
            //Get the image current width  
            int sourceWidth = imgToResize.Width;
            //Get the image current height  
            int sourceHeight = imgToResize.Height;
            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size  
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size  
            nPercentH = ((float)size.Height / (float)sourceHeight);
            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width  
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height  
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height  
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }
        BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            Bitmap bitmap2 = new Crop(new Rectangle((bitmap.Width - bitmap.Height) / 2, 0, bitmap.Height, bitmap.Height)).Apply(bitmap);
            using (MemoryStream memory = new MemoryStream())
            {
                bitmap2.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                BitmapImage bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();
                return bitmapimage;

            }
        }
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //if (cam != null && cam.IsRunning) cam.Stop();
            //if (timer != null)
            //{
            //    timer.Stop();
            //}
            //Main.grShowPopup.Children.Add(new ucThongBaoThanhCong(Main, Id_NhanVien));
            //Task k = new Task(() =>
            //{
          
            //});
            //k.Start();

        }

        private void ClosePopup(object sender, MouseButtonEventArgs e)
        {
            if (block)
            {
                if (cam != null && cam.IsRunning) cam.SignalToStop();
                if (timer != null)
                {
                    timer.Stop();
                }

            }
            else
            {
                startChamCong();
            }
        }
    }
}
