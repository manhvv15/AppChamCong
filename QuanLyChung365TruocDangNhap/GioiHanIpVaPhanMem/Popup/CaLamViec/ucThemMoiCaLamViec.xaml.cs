using System;
using System.Collections.Generic;
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

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Popup.CaLamViec
{
    /// <summary>
    /// Interaction logic for ucThemMoiCaLamViec.xaml
    /// </summary>
    public partial class ucThemMoiCaLamViec : UserControl
    {
        Dictionary<string, string> ListTimeArea = new Dictionary<string, string>();
        public ucThemMoiCaLamViec()
        {
            InitializeComponent();
            foreach (var timeZone in TimeZoneInfo.GetSystemTimeZones())
            {
                cbxTimeArea.Items.Add(timeZone.DisplayName);
            }
            cbxTimeArea.SelectedItem = TimeZoneInfo.Local.DisplayName;
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void Close(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

        }




        private void cbxTimeArea_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxTimeArea.SelectedIndex = -1;
                string textSearch = cbxTimeArea.Text;
                cbxTimeArea.Items.Refresh();
                cbxTimeArea.ItemsSource = ListTimeArea.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxTimeArea_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxTimeArea.SelectedIndex = -1;
            string textSearch = cbxTimeArea.Text + e.Text;
            cbxTimeArea.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxTimeArea.Text = "";
                cbxTimeArea.Items.Refresh();
                cbxTimeArea.ItemsSource = ListTimeArea;
                cbxTimeArea.SelectedIndex = -1;
            }
            else
            {
                cbxTimeArea.ItemsSource = "";
                cbxTimeArea.Items.Refresh();
                cbxTimeArea.ItemsSource = ListTimeArea.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }
        private void btnOnGioiHanTG_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            stackSettingTimeLimit.Visibility = Visibility.Collapsed;
            btnOffGioiHanTG.Visibility = Visibility.Visible;
            btnOnGioiHanTG.Visibility = Visibility.Collapsed;
        }

        private void btnOffGioiHanTG_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnOnGioiHanTG.Visibility = Visibility.Visible;
            btnOffGioiHanTG.Visibility = Visibility.Collapsed;
            stackSettingTimeLimit.Visibility = Visibility.Visible;
        }
        private void btnOnGioLinhHoat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            stackThoiGianLinhHoat.Visibility = Visibility.Collapsed;
            btnOffGioLinhHoat.Visibility = Visibility.Visible;
            btnOnGioLinhHoat.Visibility = Visibility.Collapsed;
        }

        private void btnOffGioLinhHoat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btnOnGioLinhHoat.Visibility = Visibility.Visible;
            btnOffGioLinhHoat.Visibility = Visibility.Collapsed;
            stackThoiGianLinhHoat.Visibility = Visibility.Visible;
        }
    }
}
