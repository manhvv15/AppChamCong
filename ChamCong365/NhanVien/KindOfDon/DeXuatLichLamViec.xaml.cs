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

namespace ChamCong365.NhanVien.KindOfDon
{
    /// <summary>
    /// Interaction logic for DeXuatLichLamViec.xaml
    /// </summary>
    public partial class DeXuatLichLamViec : UserControl, INotifyPropertyChanged
    {
        public class ChonCaLamViec
        {
            public string CaLamViec { get; set; }
        }
        List<ChonCaLamViec> listCaLamViec = new List<ChonCaLamViec>();
        private void LoadCaLamViec()
        {

            listCaLamViec.Add(new ChonCaLamViec { CaLamViec = "Làm việc theo ca" });
            listCaLamViec.Add(new ChonCaLamViec { CaLamViec = "Làm việc theo giờ" });

            lsvChonCaLamViec.ItemsSource = listCaLamViec;
        }
        public class ChonLichLamViec
        {
            public string LichLamViec { get; set; }
        }
        List<ChonLichLamViec> listLichLamViec = new List<ChonLichLamViec>();
        private void LoadChonLichLamViec()
        {

            listLichLamViec.Add(new ChonLichLamViec { LichLamViec = "Thứ 2 - Thứ 6" });
            listLichLamViec.Add(new ChonLichLamViec { LichLamViec = "Thứ 2 - Thứ 7" });
            listLichLamViec.Add(new ChonLichLamViec { LichLamViec = "Thứ 2 - CN" });
            lsvLichLamViec.ItemsSource = listLichLamViec;
        }
        public class ChonCaLamViec1
        {
            public string CaLamViec1 { get; set; }
        }
        List<ChonCaLamViec1> listCaLamViec1 = new List<ChonCaLamViec1>();
        private void LoadCaLamViec1()
        {

            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca sáng Lương <= 5TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca chiều Lương <= 5TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca sáng 5TR < Lương <= 7TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca chiều 5TR < Lương <= 7TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca tối 5TR < Lương <= 7TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca tối 5TR < Lương <= 7TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca sáng 7TR < Lương <= 10TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca chiều 7TR < Lương <= 10TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca sáng Lương > 10TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca chiều Lương > 10TR" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca trưa KD thẻ" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca tối" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca sáng - phòng 14" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca chiều - phòng 14" });
            listCaLamViec1.Add(new ChonCaLamViec1 { CaLamViec1 = "Ca trưa (Trực phụ)" });

            lsvChonCaLamViec1.ItemsSource = listCaLamViec1;
        }
        MainChamCong Main;
        public DeXuatLichLamViec(MainChamCong main)
        {
            this.DataContext = this;
            InitializeComponent();
            dteSelectedMonth = new Calendar();
            dteSelectedMonth.Visibility = Visibility.Collapsed;
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            dteSelectedMonth.MouseLeftButtonDown += Select_thang;
            dteSelectedMonth.DisplayModeChanged += dteSelectedMonth_DisplayModeChanged;
            cl = new List<Calendar>();
            cl.Add(dteSelectedMonth);
            cl = cl.ToList();
            Main = main;
            LoadCaLamViec();
            LoadChonLichLamViec();
            LoadCaLamViec1();
        }

        private void dteSelectedMonth_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            var x = dteSelectedMonth.DisplayDate.ToString("MM/yyyy");
            if (flag == 0)
                x = "";
            else
                x = dteSelectedMonth.DisplayDate.ToString("MM/yyyy");
            if (textThang != null && !string.IsNullOrEmpty(x))
            {
                textThang.Text = x;
                DateTime a = DateTime.Parse(x);
                DatePicker.SelectedDate = a;
            }
            dteSelectedMonth.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonth.DisplayDate != null && flag > 0)
            {
                dteSelectedMonth.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }

        private void Select_thang(object sender, MouseButtonEventArgs e)
        {
            dteSelectedMonth.Visibility = dteSelectedMonth.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
            flag = 1;
        }

        private void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borCaLamViec.Visibility == Visibility.Collapsed)
            {

                borCaLamViec.Visibility = Visibility.Visible;

            }
            else
            {
                borCaLamViec.Visibility = Visibility.Collapsed;

            }

        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            ChonCaLamViec d = (sender as Border).DataContext as ChonCaLamViec;
            if (d != null)
            {
                textCaLamViec.Text = d.CaLamViec;

            }
        }
        private void scroll_PreviewMouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);
        }

        private void Grid_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            if (borLichLamViec.Visibility == Visibility.Collapsed)
            {

                borLichLamViec.Visibility = Visibility.Visible;

            }
            else
            {
                borLichLamViec.Visibility = Visibility.Collapsed;

            }

        }
        private void Border_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            ChonLichLamViec d = (sender as Border).DataContext as ChonLichLamViec;
            if (d != null)
            {
                textLichLamViec.Text = d.LichLamViec;

            }
        }

        private void Grid_MouseLeftButtonUp_2(object sender, MouseButtonEventArgs e)
        {
            if (borChonCaLamViec.Visibility == Visibility.Collapsed)
            {

                borChonCaLamViec.Visibility = Visibility.Visible;

            }
            else
            {
                borChonCaLamViec.Visibility = Visibility.Collapsed;

            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ChonCaLamViec1 d = (sender as Border).DataContext as ChonCaLamViec1;
            if (d != null)
            {
                textCaLamViec1.Text = d.CaLamViec1;

            }
        }

        private void lsvChonCaLamViec1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCaLamViec1.ScrollToVerticalOffset(scrollCaLamViec1.VerticalOffset - e.Delta);
        }

        private void Grid_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            // Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);

            Main.scrollMainChamCong.ScrollToVerticalOffset(Main.scrollMainChamCong.VerticalOffset - e.Delta);
        }

        
        int flag = 0;

      
        Calendar dteSelectedMonth { get; set; }

        private List<Calendar> _cl;

        public List<Calendar> cl
        {
            get { return _cl; }
            set
            {
                _cl = value; OnPropertyChanged();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
