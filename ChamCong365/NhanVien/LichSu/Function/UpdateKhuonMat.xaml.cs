using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using Microsoft.Win32;
//using AForge.Video;
//using AForge.Video.DirectShow;
//using AForge.Imaging.Filters;

using System.Drawing;
using System.Runtime.CompilerServices;
using System.IO;
using OfficeOpenXml.Drawing.Chart;
using System.Threading;
using System.Net.Http;
using System.Windows.Threading;
using System.Timers;

using System.Windows.Controls.Primitives;

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for UpdateKhuonMat.xaml
    /// </summary>
    public partial class UpdateKhuonMat : Page,INotifyPropertyChanged
    {
        MainChamCong Main;
        public UpdateKhuonMat(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }
       
       
        DispatcherTimer timer;
        //public FilterInfoCollection divices;
        //VideoCaptureDevice cam;
        int tick = 5;
        public string latitude;
        public string longitute;
        public string ipWifi;
        private BitmapImage bitmapFace;
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public BitmapImage BitmapFace
        {
            get { return bitmapFace; }
            set
            {
                bitmapFace = value;

                OnPropertyChanged("BitmapFace");
            }
        }
        public enum EditType { DiemDanh, UpdateFace }
        private EditType _Type;
        public EditType Type
        {
            get { return _Type; }
            set { _Type = value; OnPropertyChanged(); }
        }
        string listPhoto = "";
        private void btnStart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            startChamCong();
           
        }
        public async void startChamCong()
        {
            //PopupChamCong.Visibility = Visibility.Collapsed;
            //divices = new FilterInfoCollection(FilterCategory.VideoInputDevice); //so luong camera
            //if (divices.Count <= 0)
            //{
            //    var x = new KhuonMatError(Main);
            //    x.Message = "Không tìm thấy thiết bị ghi hình được kết nối";
            //    x.Message2 = "Hãy thử bật thiết bị ghi hình của bạn hoặc kết nối với thiết bị ghi hình dời và thử chấm công lại";
            //    Popup.NavigationService.Navigate(x);
            //    PopupChamCong.Visibility = Visibility.Visible;
            //}
            //else if (IsWebCamInUse())
            //{
            //    var x = new KhuonMatError(Main);
            //    x.Message = "Thiết bị ghi hình đang được sử dụng bởi một ứng dụng khác, ";
            //    x.Message2 = "hãy thử tắt các thiết bị phần mềm đang sử dụng thiết bị ghi hình và thử chấm công lại";
            //    Popup.NavigationService.Navigate(x);
            //    PopupChamCong.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    try
            //    {
            //        if (cam == null)
            //        {
            //            cam = new VideoCaptureDevice(divices[0].MonikerString); // camera chon de dung
            //            cam.NewFrame += Cam_NewFrame;  //camera tao ra frame khac
            //        }
            //        if (cam != null && !cam.IsRunning) cam.Start();
            //        tick = 5;
            //        if (timer != null)
            //        {
            //            timer.Stop();
            //        }
            //        timer = new DispatcherTimer();
            //        PageChamCongStart.Visibility = Visibility.Hidden;
            //        PageChamCongMain.Visibility = Visibility.Visible;
            //        timer.Interval = new System.TimeSpan(0, 0, 0, 0, 1000);
            //        timer.Tick += Timer_Tick;
            //        timer.Start();
            //        //while (true)
            //        //{
            //        //    ipWifi = "";
            //        //    string externalIpString = await new HttpClient().GetStringAsync("http://checkip.dyndns.org/");
            //        //    externalIpString = externalIpString.Replace("Current IP Address: ", "").Replace("\\r\\n", "").Replace("\\n", "").Trim();

            //        //    var x = externalIpString.Split('.');
            //        //    var x1 = x[0].Substring(x[0].LastIndexOf('>') + 1);
            //        //    var x2 = x[3].Substring(0, x[3].IndexOf("<"));

            //        //    externalIpString = String.Format("{0}:{1}:{2}:{3}", x1, x[1], x[2], x2);
            //        //    ipWifi = (externalIpString);
            //        //    break;
            //        //}
            //    }
            //    catch { }
            //}
        }
        private void Timer_Tick(object sender, System.EventArgs e)
        {
            //switch (Type)
            //{
            //    case EditType.DiemDanh:
            //        if (!IsWebCamInUse() && BitmapFace == null)
            //        {
            //            if (cam != null) cam.Stop();
            //            if (timer != null) timer.Stop();

            //            var x = new KhuonMatError(Main);
            //            x.Message = "Thiết bị ghi hình đang được sử dụng bởi một ứng dụng khác, ";
            //            x.Message2 = "hãy thử tắt các thiết bị phần mềm đang sử dụng thiết bị ghi hình và thử chấm công lại";
            //            Popup.NavigationService.Navigate(x);
            //            PopupChamCong.Visibility = Visibility.Visible;
            //        }
            //        else if (tick > 0)
            //        {
            //            tblTimer.Visibility = Visibility.Visible;
            //            tblTimer.Text = "Nhận diện trong " + tick.ToString();
            //        }
            //        else if (cam != null)
            //        {
            //            if (cam.IsRunning)
            //            {
            //                cam.SignalToStop();

            //                //if (Main.Type == 1)
            //                //{
            //                //    getInfoChamCong(new APICheckFace() { user_id = Main.APIStaff.data.user_info.ep_id, company_id = Main.APIStaff.data.user_info.com_id });
            //                //}
            //                //else
            //                //{
            //                //    CheckFace("", "", Main.APICompany.data.user_info.com_id);
            //                //}
            //            }
            //            else
            //            {
            //                cam.Stop();
            //                timer.Stop();
            //            }
            //            tblTimer.Visibility = Visibility.Collapsed;
            //        }
                   
            //        break;
            //    case EditType.UpdateFace:
            //        if (tick > 0)
            //        {
            //            tblTimer.Visibility = Visibility.Visible;
            //            tblTimer.Text = "Nhận diện trong " + tick.ToString();
            //            if (tick == 2)
            //            {
            //                listPhoto = "";
            //                Thread threa = new Thread(() =>
            //                {
            //                    for (int i = 0; i < 10; i++)
            //                    {
            //                        using (MemoryStream bmp = new MemoryStream())
            //                        {
            //                            BitmapEncoder enc = new BmpBitmapEncoder();
            //                            enc.Frames.Add(BitmapFrame.Create(BitmapFace));
            //                            enc.Save(bmp);
            //                            if (i == 0)
            //                            {
            //                                listPhoto += "[{\"image\":\"" + Convert.ToBase64String(bmp.ToArray()) + "\"},";
            //                            }
            //                            else if (i == 9)
            //                            {
            //                                listPhoto += "{\"image\":\"" + Convert.ToBase64String(bmp.ToArray()) + "\"}]";
            //                            }
            //                            else
            //                            {
            //                                listPhoto += "{\"image\":\"" + Convert.ToBase64String(bmp.ToArray()) + "\"},";
            //                            }
            //                            Thread.Sleep(100);
            //                        }
            //                    }
            //                    //if (Main.Type == 1)
            //                    //{
            //                    //    NhanDienKhuonMat();
            //                    //}
            //                });
            //                threa.Start();
            //            }
            //        }
            //        else
            //        {
            //            if (cam.IsRunning)
            //            {
            //                cam.SignalToStop();
                           
            //            }
            //            else
            //            {
            //                cam.Stop();
            //                timer.Stop();
            //            }
            //            tblTimer.Visibility = Visibility.Collapsed;
            //        }
                   
            //        break;
            //    default:
            //        break;
            //}
            //tick--;
        }

        //BitmapImage BitmapToImageSource(Bitmap bitmap)
        //{

        //    //Bitmap bitmap2 = new Crop(new System.Drawing.Rectangle((bitmap.Width - bitmap.Height) / 2, 0, bitmap.Height, bitmap.Height)).Apply(bitmap);
        //    //using (MemoryStream memory = new MemoryStream())
        //    //{
        //    //    bitmap2.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
        //    //    memory.Position = 0;
        //    //    BitmapImage bitmapimage = new BitmapImage();
        //    //    bitmapimage.BeginInit();
        //    //    bitmapimage.StreamSource = memory;
        //    //    bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
        //    //    bitmapimage.EndInit();
        //    //    return bitmapimage;

        //    //}
        //}
        //private void Cam_NewFrame(object sender, NewFrameEventArgs eventArgs)
        //{
        //    try
        //    {
        //        var image = (Bitmap)eventArgs.Frame.Clone();
        //        if (image != null)
        //        {
        //            var filter = new Mirror(false, true);
        //            filter.ApplyInPlace(image);
        //            this.Dispatcher.Invoke(() => BitmapFace = BitmapToImageSource(image)); //show anh len
        //        }
        //        else
        //        {
        //            MessageBox.Show("lỗi");
        //        }
        //    }
        //    catch { }

        //}
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

        
    }
}
