using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaLCB.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaLCB : UserControl
    {
        private MainWindow Main;
        private clsLuongBaoHiem.DataSalary clsLuongBH = new clsLuongBaoHiem.DataSalary();
        private clsLuongBaoHiem.DataContract clsLuongHD = new clsLuongBaoHiem.DataContract();
        private ucHoSoNhanVien frm;
        public PopUpHoiTruocKhiXoaLCB(MainWindow main, clsLuongBaoHiem.DataSalary cls, ucHoSoNhanVien uc)
        {
            InitializeComponent();
            Main = main;
            clsLuongBH = cls;
            frm = uc;
        }
        public PopUpHoiTruocKhiXoaLCB(MainWindow main, clsLuongBaoHiem.DataContract cls, ucHoSoNhanVien uc)
        {
            InitializeComponent();
            Main = main;
            clsLuongHD = cls;
            frm = uc;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodXacNhanXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(clsLuongBH.sb_id > 0)
            {
                using (WebClient web = new WebClient())
                {

                    web.QueryString.Add("sb_id", clsLuongBH.sb_id.ToString());
                    web.QueryString.Add("token", Properties.Settings.Default.Token);
                    web.UploadValuesCompleted += (s, ee) =>
                    {
                        try
                        {
                            if (UTF8Encoding.UTF8.GetString(ee.Result).Contains("success"))
                            {
                                frm.lstLuongNv.Remove(clsLuongBH);
                                frm.dgDanhSachLuong.ItemsSource = frm.lstLuongNv;
                                frm.dgDanhSachLuong.Items.Refresh();
                                this.Visibility = Visibility.Collapsed;
                            }
                        }
                        catch { }
                    };
                    web.UploadValuesTaskAsync("http://210.245.108.202:3009/api/tinhluong/congty/delete_basic_salary", web.QueryString);
                }
            }
            else if(clsLuongHD.con_id > 0)
            {
                using (WebClient web = new WebClient())
                {

                    web.QueryString.Add("con_id", clsLuongHD.con_id.ToString());
                    web.QueryString.Add("con_id_user", clsLuongHD.con_id_user.ToString());
                    web.QueryString.Add("token", Properties.Settings.Default.Token);
                    web.UploadValuesCompleted += (s, ee) =>
                    {
                        try
                        {
                            frm.lstHopDongNv.Remove(clsLuongHD);
                            frm.dgDanhSachHopDong.ItemsSource = frm.lstHopDongNv;
                            frm.dgDanhSachHopDong.Items.Refresh();
                            this.Visibility = Visibility.Collapsed;
                        }
                        catch { }
                    };
                    web.UploadValuesTaskAsync("https://api.timviec365.vn/api/tinhluong/congty/delete_contract", web.QueryString);
                }
            }
            
        }

        private void bodCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
