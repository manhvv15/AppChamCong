using ChamCong365.SalarySettings;
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

namespace ChamCong365.Popup.CaiDatLuong.ThuongPhat
{
    /// <summary>
    /// Interaction logic for PopUpChiTietThuongNV.xaml
    /// </summary>
    public partial class PopUpChiTietThuongNV : UserControl
    {
        private frmThuongPhat frmTP;
        public PopUpChiTietThuongNV(frmThuongPhat frm, frmThuongPhat.ThuongPhat tp,MainWindow main)
        {
            InitializeComponent();
            this.DataContext = this;
            //this.Margin = new Thickness(Mouse.GetPosition(main).X-240, (Mouse.GetPosition(main).Y+20), 0, 0);
            borChiTietThuongNV.Margin = new Thickness(Mouse.GetPosition(main).X - 200, (Mouse.GetPosition(main).Y - 20), 0, 0);
            borChiTietThuongNV.VerticalAlignment = VerticalAlignment.Top;
            borChiTietThuongNV.HorizontalAlignment = HorizontalAlignment.Left;
            dgv.ItemsSource = tp.lst;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("ok");
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
