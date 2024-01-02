using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using Newtonsoft.Json;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.ThuongPhat.clsAddTP;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ThuongPhat
{
    /// <summary>
    /// Interaction logic for PopUpHoiTruocKhiXoaThuongPhat.xaml
    /// </summary>
    public partial class PopUpHoiTruocKhiXoaThuongPhat : UserControl
    {
        MainWindow Main;
        OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat ItemP = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat();
        OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong ItemT = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong();
        OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong ItemPC = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong();
        private PopUpThemMoiThuongPhat popUp;
        private frmThuongPhat frmTP;
        public PopUpHoiTruocKhiXoaThuongPhat(MainWindow main, OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat phat, string TieuDeP, PopUpThemMoiThuongPhat pop, frmThuongPhat frmtp)
        {
            InitializeComponent();
            Main = main;
            ItemP = phat;
            frmTP = frmtp;
            textNoiDung.Text = TieuDeP;
            popUp = pop;
        }

        public PopUpHoiTruocKhiXoaThuongPhat(MainWindow main, OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong thuong, string TieuDeT, PopUpThemMoiThuongPhat pop, frmThuongPhat frmtp)
        {
            InitializeComponent();
            Main = main;
            ItemT = thuong;
            frmTP = frmtp;
            textNoiDung.Text = TieuDeT;
            popUp = pop;
        }

        public PopUpHoiTruocKhiXoaThuongPhat(MainWindow main, OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong phatcong, string TieuDeT, PopUpThemMoiThuongPhat pop, frmThuongPhat frmtp)
        {
            InitializeComponent();
            Main = main;
            ItemPC = phatcong;
            frmTP = frmtp;
            textNoiDung.Text = TieuDeT;
            popUp = pop;
        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnHuy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        int payID;
        Newobj1 newobj1 = new Newobj1();
        string ThongBao;
        char OK;
        private async void btnDongY_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                if (textNoiDung.Text == "Bạn có chắc chăn muốn xoá mức phạt này không?")
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_thuong_phat")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("pay_id", ItemP.pay_id);
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            RestResponse resAlbum = restclient.Execute(request);
                            var b = resAlbum.Content;
                            if (popUp != null)
                            {
                                popUp.thuongP.tt_phat.ds_phat.Remove(ItemP);
                                popUp.dgvPhat.ItemsSource = null;
                                popUp.dgvPhat.ItemsSource = popUp.thuongP.tt_phat.ds_phat;
                            }
                            if (frmTP != null)
                            {
                                frmTP.Tp.tt_phat.ds_phat.Remove(ItemP);
                                frmTP.dgvPhatNV.ItemsSource = null;
                                frmTP.dgvPhatNV.ItemsSource = frmTP.Tp.tt_phat.ds_phat;
                                
                            }
                            Main.grShowPopup.Children.Add(new ucThongBaoAll(ThongBao, OK));
                            this.Visibility = Visibility.Collapsed;
                        }

                    }
                    catch (Exception)
                    {}
                }
                else if (textNoiDung.Text == "Bạn có chắc chăn muốn xoá phạt công này không?")
                {
                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/xoaphatcong");
                    var content = new MultipartFormDataContent();
                    content.Add(new StringContent(ItemPC.id_phatcong.ToString()), "id_phatcong");
                    content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                    {
                        var resconten = await response.Content.ReadAsStringAsync();
                        if (popUp != null)
                        {
                            popUp.thuongP.tt_phat_cong.ds_phat_cong.Remove(ItemPC);
                            popUp.dgvPhatCong.ItemsSource = null;
                            popUp.dgvPhatCong.ItemsSource = popUp.thuongP.tt_phat_cong.ds_phat_cong;
                            
                        }
                        if (frmTP != null)
                        {
                            frmTP.Tp.tt_phat_cong.ds_phat_cong.Remove(ItemPC);
                            frmTP.dgvPhatCongNV.ItemsSource = null;
                            frmTP.dgvPhatCongNV.ItemsSource = frmTP.Tp.tt_phat_cong.ds_phat_cong;
                            
                        }
                        Main.grShowPopup.Children.Add(new ucThongBaoAll(ThongBao, OK));
                        this.Visibility = Visibility.Collapsed; 
                    }
                }
                else
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_thuong_phat")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("pay_id", ItemT.pay_id);
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            RestResponse resAlbum = restclient.Execute(request);
                            var b = resAlbum.Content;
                            if (popUp != null)
                            {
                                popUp.thuongP.tt_thuong.ds_thuong.Remove(ItemT);
                                popUp.dgvThuong.ItemsSource = null;
                                popUp.dgvThuong.ItemsSource = popUp.thuongP.tt_thuong.ds_thuong;
                            }
                            if (frmTP != null)
                            {
                                frmTP.Tp.tt_thuong.ds_thuong.Remove(ItemT);
                                frmTP.dgvThuongNV.ItemsSource = null;
                                frmTP.dgvThuongNV.ItemsSource = frmTP.Tp.tt_thuong.ds_thuong;
                            }
                            Main.grShowPopup.Children.Add(new ucThongBaoAll(ThongBao, OK));
                            this.Visibility = Visibility.Collapsed;
                        }

                    }
                    catch (Exception)
                    {}
                }
            }
            catch (Exception)
            {
            }
        }
    }
}
