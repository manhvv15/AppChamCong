using ChamCong365.Popup.CaiDatLuong.NghiSaiQuyDinh;
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

namespace ChamCong365.SalarySettings.DiMuonVeSom
{
    /// <summary>
    /// Interaction logic for frmNghiSaiQuyDinh.xaml
    /// </summary>
    public partial class frmNghiSaiQuyDinh : Page,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public class Nam
        {
            public string nam { get; set; }
        }
        List<Nam> lstNam = new List<Nam>();
        List<Nam> lstSearchNam = new List<Nam>();
        private List<OOP.ClsCaLamViec> lstCaLV = new List<OOP.ClsCaLamViec>();
        private List<OOP.ClsCaLamViec> lstCaLV222 = new List<OOP.ClsCaLamViec>();
        private MainWindow Main;
        public frmNghiSaiQuyDinh(MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            main.i = 0;
            LoadDLCaLamViec();
            LoadDLNam();
            Main = main;
            
        }

        private void LoadDLNam()
        {
            textHienThiNam.Text = "Năm " + DateTime.Now.Year.ToString();
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) - 1).ToString() });
            lstNam.Add(new Nam { nam = "Năm " + DateTime.Now.Year });
            lstNam.Add(new Nam { nam = "Năm " + (double.Parse(DateTime.Now.Year.ToString()) + 1).ToString() });
            lsvNam.ItemsSource = lstNam;
        }

        private void LoadDLCaLamViec()
        {
            lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca sáng", ThoiGian = "Từ 08:00:00 - Đến 11:30:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca",Check="0" });
            lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca trưa kinh doanh", ThoiGian = "Từ 11:30:00 - Đến 14:00:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca hành chính", ThoiGian = "Từ 08:00:00 - Đến 17:30:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "1" });
            lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Part time sáng", ThoiGian = "Từ 08:00:00 - Đến 11:30:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca chiều", ThoiGian = "Từ 14:00:00 - Đến 18:00:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca gãy trưa", ThoiGian = "Từ 09:00:00 - Đến 15:00:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            lstCaLV.Add(new OOP.ClsCaLamViec { TenCaLV = "Ca 1", ThoiGian = "Từ 08:00:00 - Đến 18:00:00", NgayBatDau = DateTime.Now.ToString(), MucPhat = "100.000 VND/Ca", Check = "0" });
            lsvCaLamViec.ItemsSource = lstCaLV;
            dgv.ItemsSource = lstCaLV;
        }

        private void borCaLV_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OOP.ClsCaLamViec calv = (sender as Border).DataContext as OOP.ClsCaLamViec;
            if (calv != null)
            {
                foreach(var item in lstCaLV)
                {
                    if (item.TenCaLV == calv.TenCaLV)
                    {
                        if (item.Check == "0")
                        {
                            item.Check = "1";
                        }
                        else
                        {
                            item.Check = "0";
                        }
                    }
                }
                
            }
            lsvCaLamViec.ItemsSource = lstCaLV;
            
        }

        private void lsvCaLamViec_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scrollCaLV.ScrollToVerticalOffset(scrollCaLV.VerticalOffset - e.Delta);
        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void borHienThiNam_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNam.Visibility == Visibility.Collapsed)
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 0, 0);
                borNam.Visibility = Visibility.Visible;
                popup.Visibility = Visibility.Visible;
            }
            else
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
            }
        }

        private void popup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borNam.Visibility = Visibility.Collapsed;
            popup.Visibility = Visibility.Collapsed;
            borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
        }

        private void borNam_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Nam th = (sender as Border).DataContext as Nam;
            if (th != null)
            {
                borHienThiNam.CornerRadius = new CornerRadius(5, 5, 5, 5);
                borNam.Visibility = Visibility.Collapsed;
                popup.Visibility = Visibility.Collapsed;
                textHienThiNam.Text = th.nam;
                //Main.Nam = th.nam;
            }
        }

        private void textSearchNam_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstSearchNam = new List<Nam>();
            foreach (var str in lstNam)
            {
                if (str.nam.Contains(textSearchNam.Text.ToString()))
                {
                    lstSearchNam.Add(str);

                }
            }
            lsvNam.ItemsSource = lstSearchNam;
            if (textSearchNam.Text == "")
            {
                lsvNam.ItemsSource = lstNam;
            }
        }

        private void btnCaiDatMucPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borCaiDatMucPhat.Visibility = Visibility.Visible;
        }

        private void btnApDung_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borCaiDatMucPhat.Visibility = Visibility.Collapsed;
        }

        private void textXemMucPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new PopUpChiTietMucPhatNghiSaiQD());
        }
    }
}
