using ChamCong365.Popup.ChamCong.ChamCongKhuonMat;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for ChamCongHoanTat.xaml
    /// </summary>
    public partial class ChamCongHoanTat : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        ChamCongKhuonMatNhanVien ChamCong;
        MainWindow Main;
        public ChamCongHoanTat(ChamCongKhuonMatNhanVien chamCong, MainWindow main)
        {
            InitializeComponent();
            string h = DateTime.Now.Hour < 10 ? "0" + DateTime.Now.Hour.ToString() : DateTime.Now.Hour.ToString();
            string m = DateTime.Now.Minute < 10 ? "0" + DateTime.Now.Minute.ToString() : DateTime.Now.Minute.ToString();

            Day = h + ":" + m + " - " + DateTime.Now.ToString("dddd, dd/MM/yyyy", new System.Globalization.CultureInfo("vi-VN"));
            this.DataContext = this;
            ChamCong = chamCong;
            Main = main;
        }

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
        private void Attendence_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChamCong.startCapture(true);
        }

        private void TrangChu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChamCongBangQR ucbodyhome = new ChamCongBangQR(null);
            MainChamCong mainCC = new MainChamCong(null,null);
            mainCC.dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            mainCC.dopBody.Children.Add(Content as UIElement);

            listHistory uc = new listHistory(mainCC, Main);
            ucbodyhome.grLoadFunctionQR.Children.Clear();
            object content = uc.Content;
            uc.Content = null;
            ucbodyhome.grLoadFunctionQR.Children.Add(content as UIElement);
        }
    }
}
