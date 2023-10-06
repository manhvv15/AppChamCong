using ChamCong365.OOP.ChamCong.CapNhatKhuonMat;
using ChamCong365.OOP.ChamCong.ChamCongKhuonMat;
using ChamCong365.TimeKeeping;
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

namespace ChamCong365.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for ThongTinChamCong.xaml
    /// </summary>
    public partial class ThongTinChamCong : UserControl
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public string latitudeNv;
        public string longituteNv;
        public string ipWifi;
        MainChamCong MainCC;
        MainWindow Window;
        List<ItemShift> ShiftList;
        APICheckFace DataEpFace;


        public string LatitudeNv
        {
            get { return latitudeNv; }
            set
            {
                latitudeNv = value;
                OnPropertyChanged("Latitude");
            }
        }
        public string LongituteNv
        {
            get { return longituteNv; }
            set
            {
                longituteNv = value;
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
        ChamCongKhuonMatNhanVien MainNv;

        public ThongTinChamCong(ChamCongKhuonMatNhanVien mainNv, List<ItemShift> shiftList, APICheckFace dataEpFace, MainChamCong maincc,MainWindow window)
        {
            InitializeComponent();
            this.DataContext = this;
            LatitudeNv = mainNv.latitude;
            LongituteNv = mainNv.longitute;
            IpWifi = mainNv.ipWifi;
            DataEpFace = dataEpFace;
            MainCC = maincc;
            Window = window;
            this.MainNv = mainNv;
            if (shiftList != null && shiftList.Count > 0)
            {
                ListViewShift.ItemsSource = shiftList;
                ShiftNone.Visibility = Visibility.Collapsed;
            }
            else
            {
                ListViewShift.Visibility = Visibility.Collapsed;
                ShiftNone.Visibility = Visibility.Visible;
            }


        }

        private void Attendence_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ListViewShift.SelectedIndex != -1)
            {
                //if (ChamCongMain.Main.Type == 1)
                //{
                //    ChamCongMain.CheckFace(((ItemShift)ListViewShift.SelectedItem).shift_id, DataEpFace.user_id, DataEpFace.company_id);
                //}
                MainNv.DiemDanh(((ItemShift)ListViewShift.SelectedItem).shift_id, DataEpFace);
                
            }
            else
            {
                MessageBox.Show("Vui lòng chọn ca!");
            }
        }

        private void ClosePopup(object sender, MouseButtonEventArgs e)
        {

        }

        private void TrangChu_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MainCC.Back = 6;
            ucBodyHome ucBody = new ucBodyHome(Window);
            ChamCongBangQR ucbodyhome = new ChamCongBangQR(MainCC);
            MainCC.dopBody.Children.Clear();
            object Content = ucbodyhome.Content;
            ucbodyhome.Content = null;
            MainCC.dopBody.Children.Add(Content as UIElement);

            listHistory uc = new listHistory(MainCC, Window);
            ucbodyhome.grLoadFunctionQR.Children.Clear();
            object Content1 = uc.Content;
            uc.Content = null;
            ucbodyhome.grLoadFunctionQR.Children.Add(Content1 as UIElement);
        }
    }
}
