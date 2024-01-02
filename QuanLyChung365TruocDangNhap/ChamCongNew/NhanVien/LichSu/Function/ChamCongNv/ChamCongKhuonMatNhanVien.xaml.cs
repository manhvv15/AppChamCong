
using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ChamCongKhuonMat;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ChamCongKhuonMat;
using Microsoft.Win32;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Device.Location;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for ChamCongNhanVien.xaml
    /// </summary>
    public partial class ChamCongKhuonMatNhanVien : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      
        MainChamCong Main;
        MainWindow Main1;
        DispatcherTimer timer;
        public FilterInfoCollection divices;
        VideoCaptureDevice cam;
        int tick = 5;
        public string latitude;
        public string longitute;
        public string ipWifi;
        public GeoCoordinateWatcher watcher = new GeoCoordinateWatcher();
      
        //
        public ChamCongKhuonMatNhanVien(MainChamCong main, MainWindow main1)
        {
            try
            {
                InitializeComponent();
                this.DataContext = this;
                Main = main;
                Main1 = main1;
                if (IsWebCamInUse())
                {
                    var x = new Error_NhanDien(null, Main1);
                    x.Message = "Thiết bị ghi hình đang được sử dụng bởi một ứng dụng khác, ";
                    x.Message2 = "hãy thử tắt các thiết bị phần mềm đang sử dụng thiết bị ghi hình và thử chấm công lại";
                    Popup.NavigationService.Navigate(x);
                    PopupChamCong.Visibility = Visibility.Visible;
                }
                startCapture(false);
            }
            catch (Exception)
            {
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
                MessageBox.Show(exception.Message);
            }
            return false;
        }
        public async Task startCapture(bool flag)
        {
            watcher.StatusChanged += Watcher_StatusChanged; ;
            watcher.Start();
            divices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (divices.Count > 0)
            {
                cam = new VideoCaptureDevice(divices[0].MonikerString);
                cam.NewFrame += Cam_NewFrame;
                cam.Start();
            }
            else
            {
                var x = new Popup.ChamCong.ChamCongKhuonMat.Error_NhanDien(null,Main1);
                x.Message = "Không tìm thấy thiết bị ghi hình được kết nối";
                x.Message2 = "Hãy thử bật thiết bị ghi hình của bạn hoặc kết nối với thiết bị ghi hình dời và thử chấm công lại";
                Popup.NavigationService.Navigate(x);
                PopupChamCong.Visibility = Visibility.Visible;

            }
            if (flag)
            {
                startChamCong();
            }
        }
        private void Start_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startChamCong();
        }
        public async void startChamCong()
        {
            PopupChamCong.Visibility = Visibility.Collapsed;
            divices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (divices.Count <= 0)
            {
                var x = new Popup.ChamCong.ChamCongKhuonMat.Error_NhanDien(null, Main1);
                x.Message = "Không tìm thấy thiết bị ghi hình được kết nối";
                x.Message2 = "Hãy thử bật thiết bị ghi hình của bạn hoặc kết nối với thiết bị ghi hình dời và thử chấm công lại";
                Popup.NavigationService.Navigate(x);
                PopupChamCong.Visibility = Visibility.Visible;
            }
            else if (IsWebCamInUse())
            {
                var x = new Error_NhanDien(null, Main1);
                x.Message = "Thiết bị ghi hình đang được sử dụng bởi một ứng dụng khác, ";
                x.Message2 = "hãy thử tắt các thiết bị phần mềm đang sử dụng thiết bị ghi hình và thử chấm công lại";
                Popup.NavigationService.Navigate(x);
                PopupChamCong.Visibility = Visibility.Visible;
            }
            else
            {
                try
                {
                    if (cam == null)
                    {
                        cam = new VideoCaptureDevice(divices[0].MonikerString);
                        cam.NewFrame += Cam_NewFrame;
                    }
                    if (cam != null && !cam.IsRunning) cam.Start();
                    tick = 5;
                    if (timer != null)
                    {
                        timer.Stop();
                    }
                    timer = new DispatcherTimer();
                    PageChamCongStart.Visibility = Visibility.Hidden;
                    PageChamCongMain.Visibility = Visibility.Visible;
                    timer.Interval = new System.TimeSpan(0, 0, 0, 0, 1000);
                    timer.Tick += Timer_Tick;
                    timer.Start();
                    while (true)
                    {
                        ipWifi = "";
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
     
        private void Timer_Tick(object sender, System.EventArgs e)
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
                    cam.SignalToStop();

                    getInfoChamCong(new APICheckFace() { user_id = Main.Ep_Id.ToString(), company_id = Main.ComdID.ToString() });
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

        public void getInfoChamCong(APICheckFace dataEpFace)
        {
            try
            {
                HttpClient httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Properties.Settings.Default.Token);
                Dictionary<string, string> form = new Dictionary<string, string>();
                form.Add("u_id", dataEpFace.user_id);
                form.Add("c_id", dataEpFace.company_id);
                var respon = httpClient.PostAsync("http://210.245.108.202:3000/api/qlc/shift/list_shift_user", new FormUrlEncodedContent(form));
                APIShift result = JsonConvert.DeserializeObject<APIShift>(respon.Result.Content.ReadAsStringAsync().Result);
                if (result.data != null && dataEpFace.user_id != "Unknown")
                {
                    Popup.NavigationService.Navigate(new ThongTinChamCong(this, result.data.shift, dataEpFace, Main, Main1));
                }
                PopupChamCong.Visibility = Visibility.Visible;
            }
            catch
            {

            }
        }

      
        public async Task DiemDanh(string shiftId, APICheckFace infoFace)
        {
            try
            {
                using (MemoryStream bmp = new MemoryStream())
                {
                    HttpClient httpClient = new HttpClient();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",Properties.Settings.Default.Token);
                    MultipartFormDataContent formData = new MultipartFormDataContent();
                    formData.Add(new StreamContent(new MemoryStream(Convert.FromBase64String(infoFace.image))), "data_img", "image_" + DateTime.Now.Ticks + ".jpg");
                    formData.Add(new StringContent(infoFace.company_id), "company_id");
                    formData.Add(new StringContent(infoFace.user_id), "user_id");
                    if (latitude != null) formData.Add(new StringContent(latitude), "ts_lat");
                    if (longitute != null) formData.Add(new StringContent(longitute), "ts_long");
                    formData.Add(new StringContent(ipWifi.Replace(":", ".")), "wifi_ip");
                    formData.Add(new StringContent(shiftId), "shift_id");
                    //formData.Add(new StringContent(Main.Type + ""), "type");

                    var respon = await httpClient.PostAsync("http://210.245.108.202:3000/api/qlc/timekeeping/create/web", formData);
                    APICheckFace result = JsonConvert.DeserializeObject<APICheckFace>(respon.Content.ReadAsStringAsync().Result);
                    if (result.status.Equals("true"))
                    {
                        var x = new ChamCongHoanTat(this,Main1);
                        x.Avatar = BitmapFace;
                        x.Type = true;
                        Popup.NavigationService.Navigate(x);
                        PopupChamCong.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        var x = new ChamCongHoanTat(this, Main1);
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

                MessageBox.Show(ex.Message);
            }
        }
        private bool block = false;
        private void Watcher_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            try
            {
                if (e.Status == GeoPositionStatus.Ready)
                {
                    // Display the latitude and longitude.  

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
