using System.Windows.Controls;

namespace ChamCong365.Popup.ChamCong
{
    /// <summary>
    /// Interaction logic for ucLoadFirstDaySmall.xaml
    /// </summary>
    public partial class ucHienThiNgayDauTien : UserControl
    {
        public ucHienThiNgayDauTien()
        {
            InitializeComponent();
        }
        public void days(int numday)
        {
            txbFirstDaySmall.Text = numday + "";
        }
    }
}
