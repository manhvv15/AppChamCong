using System.Windows.Controls;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.DatePicker
{
    /// <summary>
    /// Interaction logic for ucFirstDay.xaml
    /// </summary>
    public partial class ucFirstDay : UserControl
    {
        public ucFirstDay()
        {
            InitializeComponent();
        }
        public void daysfirst(int numday)
        {
            txbFirstDay.Text = numday + "";
        }
    }
}
