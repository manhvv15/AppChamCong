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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong
{
    /// <summary>
    /// Interaction logic for ucHuongDan.xaml
    /// </summary>
    public partial class ucHuongDan : UserControl
    {
        MainWindow Main;
        public ucHuongDan()
        {
            InitializeComponent();
            stp_HuongDanCaiDatHoaHong.Visibility = Visibility.Visible;
            stp_HuongDanHoaHongTien.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongDoanhThu.Visibility = Visibility.Collapsed;
        }

        string Next;
        public ucHuongDan(MainWindow main, string next)
        {
            InitializeComponent();
            Main = main;
            Next = next;
            stp_HuongDanCaiDatHoaHong.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongTien.Visibility = Visibility.Visible;
            stp_HuongDanHoaHongDoanhThu.Visibility = Visibility.Collapsed;
        }

        long HoaHongDoanhThu;
        public ucHuongDan(MainWindow main, long hhdt)
        {
            InitializeComponent();
            Main = main;
            HoaHongDoanhThu = hhdt;
            stp_HuongDanCaiDatHoaHong.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongTien.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongDoanhThu.Visibility = Visibility.Visible;
        }

        int HuongDanHHLoiNhuan;
        public ucHuongDan(MainWindow main, int hdhhloinhuan)
        {
            InitializeComponent();
            Main = main;
            HuongDanHHLoiNhuan = hdhhloinhuan;
            stp_HuongDanCaiDatHoaHong.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongTien.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongLoiNhuan.Visibility = Visibility.Visible;
        }

        float HuongDanHHVitri;
        public ucHuongDan(MainWindow main, float hdhhvitri)
        {
            InitializeComponent();
            Main = main;
            HuongDanHHVitri = hdhhvitri;
            stp_HuongDanCaiDatHoaHong.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongTien.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongViTri.Visibility = Visibility.Visible;
        }

        decimal HuongDanHHKeHoach;
        public ucHuongDan(MainWindow main, decimal hdhhkehoach)
        {
            InitializeComponent();
            Main = main;
            HuongDanHHKeHoach = hdhhkehoach;
            stp_HuongDanCaiDatHoaHong.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongTien.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongDoanhThu.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongLoiNhuan.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongViTri.Visibility = Visibility.Collapsed;
            stp_HuongDanHoaHongKeHoach.Visibility = Visibility.Visible;


        }
    }
}
