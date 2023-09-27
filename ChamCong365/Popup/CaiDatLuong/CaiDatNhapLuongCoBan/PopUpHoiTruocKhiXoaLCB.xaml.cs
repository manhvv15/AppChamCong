using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace ChamCong365.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaLCB.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaLCB : UserControl
    {
        private MainWindow Main;
        private clsLuongBaoHiem.DataSalary clsLuongBH = new clsLuongBaoHiem.DataSalary();
        private ucHoSoNhanVien frm;
        public PopUpHoiTruocKhiXoaLCB(MainWindow main, clsLuongBaoHiem.DataSalary cls, ucHoSoNhanVien uc)
        {
            InitializeComponent();
            Main = main;
            clsLuongBH = cls;
            frm = uc;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodXacNhanXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            using (WebClient web = new WebClient())
            {
                
                web.QueryString.Add("sb_id", clsLuongBH.sb_id.ToString());
                web.QueryString.Add("token", Properties.Settings.Default.Token);
                web.UploadValuesCompleted += (s, ee) =>
                {
                    frm.lstLuongBH.Remove(clsLuongBH);
                    frm.dgDanhSachLuong.ItemsSource = null;
                    frm.dgDanhSachLuong.ItemsSource = frm.lstLuongBH;
                    this.Visibility = Visibility.Collapsed;
                };
                web.UploadValuesTaskAsync("http://210.245.108.202:3009/api/tinhluong/congty/delete_basic_salary", web.QueryString);
            }
        }

        private void bodCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
