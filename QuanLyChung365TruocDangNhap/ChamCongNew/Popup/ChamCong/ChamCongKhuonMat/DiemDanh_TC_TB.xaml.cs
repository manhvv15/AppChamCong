
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
    /// Interaction logic for Attenden_Fail.xaml
    /// </summary>
    public partial class DiemDanh_TC_TB : Page, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MainWindow Main;
        public DiemDanh_TC_TB(ChamCong_Main chamCong_Main, MainWindow main)
        {
            InitializeComponent();
            ChamCongMain = chamCong_Main;
            string h = DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            string m = DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();

            Day = h + ":" + m + " - " + DateTime.Now.ToString("dddd, dd/MM/yyyy", new System.Globalization.CultureInfo("vi-VN"));
            this.DataContext = this;
            Main = main;
        }

        private ChamCong_Main ChamCongMain;
        private string day;

        public string Day
        {
            get { return day; }
            set { day = value; OnPropertyChanged("Day"); }
        }


        public bool Type
        {
            get { return (bool)GetValue(TypeProperty); }
            set { SetValue(TypeProperty, value); }
        }
        public static readonly DependencyProperty TypeProperty =
            DependencyProperty.Register("Type", typeof(bool), typeof(DiemDanh_TC_TB));

        public ImageSource Avatar
        {
            get { return (ImageSource)GetValue(AvatarProperty); }
            set { SetValue(AvatarProperty, value); }
        }
        public static readonly DependencyProperty AvatarProperty =
            DependencyProperty.Register("Avatar", typeof(ImageSource), typeof(DiemDanh_TC_TB));

        public string Message
        {
            get { return (string)GetValue(MessageProperty); }
            set { SetValue(MessageProperty, value); }
        }
        public static readonly DependencyProperty MessageProperty =
            DependencyProperty.Register("Message", typeof(string), typeof(DiemDanh_TC_TB));

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

        private void Attendence_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChamCongMain.startCapture(true);
        }
    }
}
