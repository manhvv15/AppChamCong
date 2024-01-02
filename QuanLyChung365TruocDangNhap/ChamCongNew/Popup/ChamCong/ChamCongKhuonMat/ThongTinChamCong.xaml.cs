
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien;
using QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.LichSu.Function;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.ChamCongKhuonMat;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ChamCongKhuonMat
{
    /// <summary>
    /// Interaction logic for Info_ChamCong.xaml
    /// </summary>
    public partial class ThongTinChamCong : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string latitude;
       
        public string longitute;
      
        public string ipWifi;
        public APICheckFace DataEpFace;
        public string Latitude
        {
            get { return latitude; }
            set
            {
                latitude = value;
                OnPropertyChanged("Latitude");
            }
        }
        
        public string Longitute
        {
            get { return longitute; }
            set
            {
                longitute = value;
                OnPropertyChanged("Longitute");
            }
        }
      
        public string IpWifi
        {
            get { return ipWifi; }
            set
            {
                ipWifi = value;
                OnPropertyChanged("IpWifi");
            }
        }
        MainWindow Main;
        MainChamCong MainCC;
        private ChamCongKhuonMatNhanVien MainNv;
        private ChamCong_Main ChamCongMain;
        public ThongTinChamCong(ChamCong_Main main, List<ItemShift> shifts, APICheckFace dataEpFace, MainWindow main1)
        {
            InitializeComponent();
            this.DataContext = this;
            Latitude = main.latitude;
            Longitute = main.longitute;
            IpWifi = main.ipWifi;
            DataEpFace = dataEpFace;
            ChamCongMain = main;
            this.Main = main1;
            if (shifts != null && shifts.Count > 0)
            {
                ListViewShift.ItemsSource = shifts;
                ShiftNone.Visibility = Visibility.Collapsed;
            }
            else
            {
                ListViewShift.Visibility = Visibility.Collapsed;
                ShiftNone.Visibility = Visibility.Visible;
            }
        }

       

        private void TrangChu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Main.Back = 1;
            ucBodyHome ucbodyhome = new ucBodyHome(Main);
            Main.dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);

            ucDanhSachChucNangChamCong uc = new ucDanhSachChucNangChamCong(Main, Main.IdAcount);
            ucbodyhome.grLoadFunction.Children.Clear();
            object Content1 = uc.Content;
            uc.Content = null;
            ucbodyhome.grLoadFunction.Children.Add(Content1 as UIElement);
        }

        private void ClosePopup(object sender, MouseButtonEventArgs e)
        {
            if (ChamCongMain.IsWebCamInUse())
            {
                var x = new Error_NhanDien(ChamCongMain, Main);
                x.Message = "Thiết bị ghi hình đang được sử dụng bởi một ứng dụng khác, ";
                x.Message2 = "hãy thử tắt các thiết bị phần mềm đang sử dụng thiết bị ghi hình và thử chấm công lại";
                ChamCongMain.Popup.NavigationService.Navigate(x);
                ChamCongMain.PopupChamCong.Visibility = Visibility.Visible;
            }
            else if (ChamCongMain.divices.Count <= 0)
            {
                var x = new Popup.ChamCong.ChamCongKhuonMat.Error_NhanDien(ChamCongMain, Main);
                x.Message = "Không tìm thấy thiết bị ghi hình được kết nối";
                x.Message2 = "Hãy thử bật thiết bị ghi hình của bạn hoặc kết nối với thiết bị ghi hình dời và thử chấm công lại";
                ChamCongMain.Popup.NavigationService.Navigate(x);
                ChamCongMain.PopupChamCong.Visibility = Visibility.Visible;
            }
            else
            {
                this.Visibility = Visibility.Collapsed;
                ChamCongMain.startCapture(true);
            }
        }

        private void Attendence_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
