using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.CaiDatThue;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatThue
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaCaiDatThue.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaCaiDatThue : UserControl
    {
        private string IdThue = "";
        private string IdNV = "";
        private OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserInTax NhanVien = new OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserInTax();
        private PopUpDanhSachNVTrongThue PopUp;
        private frmDanhSachNhanSuDaThietLap frmNSDTL;
        private OOP.CaiDatLuong.Tax.clsNSDaTL.ListU lstUS = new OOP.CaiDatLuong.Tax.clsNSDaTL.ListU();
        MainWindow Main;
        public PopUpHoiTruocKhiXoaCaiDatThue(MainWindow main, OOP.CaiDatLuong.Tax.clsStaffInTax.ListUserInTax staff, string IdT, PopUpDanhSachNVTrongThue Pop)
        {
            InitializeComponent();
            IdThue = IdT;
            IdNV = staff.idQLC.ToString();
            PopUp = Pop;
            NhanVien = staff;
            tb_Xoa.Text = "Bạn có chắc chắn muốn xóa thuế của nhân sự này!";
        }

        public PopUpHoiTruocKhiXoaCaiDatThue(MainWindow main, OOP.CaiDatLuong.Tax.clsNSDaTL.ListU staff, frmDanhSachNhanSuDaThietLap frmnsdtl)
        {
            InitializeComponent();
            Main = main;
            frmNSDTL = frmnsdtl;
            lstUS = staff;
            tb_Xoa.Text = "Bạn có chắc chắn muốn xoá cài đặt thuế của nhân sự này!";
        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Border_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        string Notication;
        private async void btnAgree_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (tb_Xoa.Text == "Bạn có chắc chắn muốn xóa thuế của nhân sự này!")
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/delete_nv_tax");
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(IdThue), "cls_id_cl");
                    content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                    content.Add(new StringContent(IdNV), "cls_id_user");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var restContent = await response.Content.ReadAsStringAsync();
                        this.Visibility = Visibility.Collapsed;
                        PopUp.LoadDataStaffInTax();
                        PopUp.txbCountSaff.Text = PopUp.lstStaff.Count.ToString();
                    }
                }
                catch (Exception)
                {
                }
            }
            else
            {
                try
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/delete_nv_tax");
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(lstUS.cls_id_cl.ToString()), "cls_id_cl");
                    content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                    content.Add(new StringContent(lstUS.cls_id_user.ToString()), "cls_id_user");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var restContent = await response.Content.ReadAsStringAsync();
                        this.Visibility = Visibility.Collapsed;
                        frmNSDTL.LoadDLDSNhanSuDaThietLap($"{DateTime.Now.Year}-{DateTime.Now.Month}");
                        Notication = "Xóa Thiết lập thuế cho nhân viên thành công";
                        Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, Notication, this));
                    }
                }
                catch (Exception)
                {
                    
                }
            }
        }
    }
}
