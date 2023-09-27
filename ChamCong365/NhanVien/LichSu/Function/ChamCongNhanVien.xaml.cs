//using AForge.Imaging.Filters;
//using AForge.Video;
//using AForge.Video.DirectShow;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.IO;
using System.Linq;
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
using System.Windows.Threading;
using static ChamCong365.NhanVien.LichSu.Function.UpdateKhuonMat;

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for ChamCongNhanVien.xaml
    /// </summary>
    public partial class ChamCongNhanVien : UserControl
    {
       // FilterInfoCollection videoDevices;
        //VideoCaptureDevice videoSource;
        Bitmap currentFrame;
        MainChamCong Main;
        int tick = 5;
        DispatcherTimer timer;
        public ChamCongNhanVien(MainChamCong main)
        {
            InitializeComponent();
            InitializeCamera();
            Main = main;

        }
        private void InitializeCamera()
        {
            //videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            //if (videoDevices.Count == 0)
            //{
            //    MessageBox.Show("No webcam found.");
            //    return;
            //}

            //videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            //videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
            //tick = 5;
            //if (timer != null)
            //{
            //    timer.Stop();
            //}
            //timer = new DispatcherTimer();
            ////PageChamCongStart.Visibility = Visibility.Hidden;
            ////PageChamCongMain.Visibility = Visibility.Visible;
            //timer.Interval = new System.TimeSpan(0, 0, 0, 0, 1000);
            //timer.Tick += Timer_Tick;
            //timer.Start();
        }
        private void Timer_Tick(object sender, System.EventArgs e)
        {
            //if (tick > 0)
            //{
            //    tblTimer.Visibility = Visibility.Visible;
            //    tblTimer.Text = "Nhận diện trong " + tick.ToString();
            //    if(tick == 1)
            //    {
            //        if (currentFrame != null)
            //        {
            //            BitmapImage bitmapImage = BitmapToImageSource(currentFrame);

            //            // Thêm ảnh đã chụp vào ObservableCollection
            //            capturedImages.Add(bitmapImage);

            //            // Cập nhật nguồn dữ liệu của ListBox
            //            imageListBox.ItemsSource = capturedImages;
            //            ChamCongThanhCongAn.Visibility = Visibility.Visible;
            //            path.Visibility = Visibility.Collapsed;
            //        }

            //    }
            //}
            //else
            //{
            //    //MessageBox.Show("jn");
            //    if (videoSource.IsRunning)
            //    {
            //        videoSource.SignalToStop();
            //        ////cam.NewFrame -= Cam_NewFrame;
            //    }
            //    else
            //    {
            //        videoSource.Stop();
            //        timer.Stop();
            //    }
            //    tblTimer.Visibility = Visibility.Collapsed;
            //}
            //tick--;
        }
        //private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        //{
        //    // Lấy khung hình hiện tại từ sự kiện NewFrame
        //    currentFrame = (Bitmap)eventArgs.Frame.Clone();

        //    // Hiển thị khung hình trên giao diện người dùng
        //    webcamImage.Dispatcher.Invoke(() =>
        //    {
        //        webcamImage.Source = BitmapToImageSource(currentFrame);
        //    });

        //}
        //ObservableCollection<BitmapImage> capturedImages = new ObservableCollection<BitmapImage>();
        ////System.Drawing.Bitmap anhChamCong = new System.Drawing.Bitmap(filePath);

        //// Thêm danh sách các tệp ảnh đã chụp vào ListBox hoặc ListView

        //public FilterInfoCollection divices;
        public void startChamCong()
        {
            //videoSource.Start();

        }
        public bool IsWebCamInUse()
        {
            try
            {
                using (var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\CapabilityAccessManager\ConsentStore\webcam\NonPackaged"))
                {
                    if (key != null && key.GetSubKeyNames() != null)
                    {
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
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            return false;
        }

        //private BitmapImage BitmapToImageSource(Bitmap bitmap)
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

        private void btnStart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //anhHien.Visibility = Visibility.Collapsed;
            //anhAn.Visibility = Visibility.Visible;
            startChamCong();

        }

        private void Image_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            //if (currentFrame != null)
            //{
            //    // Chuyển đổi Bitmap thành BitmapImage
            //    int numberOfImages = capturedImages.Count;
            //    if (numberOfImages < 5)
            //    {
            //        BitmapImage bitmapImage = BitmapToImageSource(currentFrame);

            //        // Thêm ảnh đã chụp vào ObservableCollection
            //        capturedImages.Add(bitmapImage);

            //        // Cập nhật nguồn dữ liệu của ListBox
            //        imageListBox.ItemsSource = capturedImages;
            //    }
            //    if (numberOfImages == 5)
            //    {
            //        capNhatThanhCong uc = new capNhatThanhCong(Main);
            //        // Main.grShowPopup.Children.Clear();
            //        object Content = uc.Content;
            //        uc.Content = null;
            //        Main.dopBody.Children.Add(Content as UIElement);
            //        //videoSource.Stop(); 
            //        if (videoSource != null && videoSource.IsRunning) videoSource.Stop();
            //    }
            //}
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //ChamCongThanhCongAn.Visibility = Visibility.Collapsed;
            //ThongTinAn.Visibility = Visibility.Visible;
        }

        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            //ThongTinAn.Visibility = Visibility.Collapsed;
            //ChamCongHoanTatAn.Visibility = Visibility.Visible;
        }
    }
}
