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
using System.Windows.Threading;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong.ChamCongKhuonMat
{
    /// <summary>
    /// Interaction logic for ucThongBaoThanhCong.xaml
    /// </summary>
    public partial class ucThongBaoThanhCong : UserControl
    {
        MainWindow Main;
        string TenNhanVien;
        private int countdownValue = 3;
        private int countdownValue2 = 5;
        private DispatcherTimer countdownTimer;
        public event EventHandler CountdownFinished;
        public ucThongBaoThanhCong(MainWindow main, string tenNhanVien)
        {
            InitializeComponent();
            Main = main;
            TenNhanVien = tenNhanVien;
            tb_TenNhanVien.Text = tenNhanVien;
            StartCountdown();
        }
        private void StartCountdown()
        {
            countdownTimer = new DispatcherTimer();
            countdownTimer.Interval = TimeSpan.FromSeconds(1);
            countdownTimer.Tick += CountdownTimer_Tick;
            countdownTimer.Start();

        }

        private void CountdownTimer_Tick(object sender, EventArgs e)
        {
            //tb_TenNhanVien.Text = countdownValue.ToString();
            progressBar.Value = countdownValue;

            countdownValue--;
            if (countdownValue == 2)
            {
                Margin = new Thickness(0, 0, 0, 400);
            }
            if (countdownValue == 1)
            {
                Margin = new Thickness(0, 0, 0, 800);
            }
            if (countdownValue < 0)
            {
                countdownTimer.Stop();
                this.Visibility = Visibility.Collapsed;
                CountdownFinished?.Invoke(this, EventArgs.Empty);
            }
        }
        private void Rectangle_MouseUp(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
