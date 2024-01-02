using AForge.Imaging.Filters;
using AForge.Video;
using AForge.Video.DirectShow;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for ChupAnh.xaml
    /// </summary>
    public partial class ChupAnh : UserControl
    {
        FilterInfoCollection videoDevices;
        VideoCaptureDevice videoSource;
        Bitmap currentFrame;
        MainChamCong Main;
        public ChupAnh(MainChamCong main)
        {
            this.DataContext = this;
            InitializeComponent();
            InitializeCamera();
            Main = main;
        }
        private void InitializeCamera()
        {
            videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (videoDevices.Count == 0)
            {
                MessageBox.Show("No webcam found.");
                return;
            }

            videoSource = new VideoCaptureDevice(videoDevices[0].MonikerString);
            videoSource.NewFrame += new NewFrameEventHandler(videoSource_NewFrame);
        }

        private void videoSource_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            // Lấy khung hình hiện tại từ sự kiện NewFrame
            currentFrame = (Bitmap)eventArgs.Frame.Clone();

            // Hiển thị khung hình trên giao diện người dùng
            webcamImage.Dispatcher.Invoke(() =>
            {
                webcamImage.Source = BitmapToImageSource(currentFrame);
            });
        }
        public ObservableCollection<BitmapImage> capturedImages = new ObservableCollection<BitmapImage>();

        // Thêm danh sách các tệp ảnh đã chụp vào ListBox hoặc ListView

        private void startButton_Click(object sender, RoutedEventArgs e)
        {
            // Bắt đầu truyền video từ webcam
            startChamCong();
        }

        public FilterInfoCollection divices;
        public void startChamCong()
        {
            videoSource.Start();
            divices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if (divices.Count <= 0)
            {
                MessageBox.Show("Không tìm thấy thiết bị ghi hình được kết nối");
            }
            else if (IsWebCamInUse())
            {
                MessageBox.Show("Không tìm thấy thiết bị!");
            }
            else
            {
                videoSource.Start();
            }
            bool isCameraInUse = IsWebCamInUse();

            if (isCameraInUse)
            {
                MessageBox.Show("Camera đang được sử dụng.");
            }
            else
            {
                MessageBox.Show("Camera không được sử dụng.");
            }
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
        private void captureButton_Click(object sender, RoutedEventArgs e)
        {
            //Chụp ảnh từ khung hình hiện tại(currentFrame)
            if (currentFrame != null)
            {
                // Tạo tên tệp dựa trên thời gian và ngày chụp
                string fileName = $"captured_{"Aavatar" + DateTime.Now}.jpg";

                // Lưu ảnh đã chụp vào tệp
                currentFrame.Save(fileName, ImageFormat.Jpeg);
                BitmapImage bitmapImage = new BitmapImage(new Uri(fileName));
                capturedImages.Add(bitmapImage);
                imageListBox.ItemsSource = capturedImages;
            }
        }

        private BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            Bitmap bitmap2 = new Crop(new System.Drawing.Rectangle((bitmap.Width - bitmap.Height) / 2, 0, bitmap.Height, bitmap.Height)).Apply(bitmap);
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

        private void btnStart_MouseUp(object sender, MouseButtonEventArgs e)
        {
            anhHien.Visibility = Visibility.Collapsed;
            anhAn.Visibility = Visibility.Visible;
            videoSource.Start();

        }
        string fileName;
        private void ButonChupAnh_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (currentFrame != null)
            {
                // Chuyển đổi Bitmap thành BitmapImage
                int numberOfImages = capturedImages.Count;
                if (numberOfImages < 5)
                {
                    BitmapImage bitmapImage = BitmapToImageSource(currentFrame);

                    // Thêm ảnh đã chụp vào ObservableCollection
                    capturedImages.Add(bitmapImage);

                    // Cập nhật nguồn dữ liệu của ListBox
                    imageListBox.ItemsSource = capturedImages;
                }
                if (numberOfImages == 5)
                {
                    string timestamp = DateTime.Now.ToString("MM_dd_yyyy_hh_mm_ss");
                    fileName  = $"Aavatar_{timestamp}.jpg";
                    

                    // Lưu ảnh đã chụp vào tệp
                    currentFrame.Save(fileName, ImageFormat.Jpeg);
                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.UriSource = new Uri(fileName, UriKind.RelativeOrAbsolute);
                    bitmapImage.EndInit();
                    //BitmapImage bitmapImage = new BitmapImage(new Uri(fileName));
                    capturedImages.Add(bitmapImage);
                    imageListBox.ItemsSource = capturedImages;
                    //Main.dopBody.Children.Add(new capNhatThanhCong());
                    capNhatThanhCong uc = new capNhatThanhCong(Main, this, fileName);
                    object Content = uc.Content;
                    uc.Content = null;
                    Main.dopBody.Children.Add(Content as UIElement);
                    //videoSource.Stop(); 
                    if (videoSource.IsRunning)
                    {
                        videoSource.SignalToStop();
                        ////cam.NewFrame -= Cam_NewFrame;
                    }
                    else
                    {
                        videoSource.Stop();

                    }
                }
            }
        }
    }
}
