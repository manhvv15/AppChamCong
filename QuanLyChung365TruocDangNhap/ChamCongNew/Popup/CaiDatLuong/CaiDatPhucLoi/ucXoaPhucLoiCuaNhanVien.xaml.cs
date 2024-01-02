using RestSharp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatPhucLoi
{
    /// <summary>
    /// Interaction logic for ucXoaPhucLoiCuaNhanVien.xaml
    /// </summary>
    public partial class ucXoaPhucLoiCuaNhanVien : UserControl
    {
        BrushConverter br = new BrushConverter();
        private MainWindow Main;
        private ucDanhSachNVHuongPhucLoi frmNVHuongPL;
        OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.ListUserFinal NVTrongPL = new OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.ListUserFinal();
        private string IdPL = "";
        private int type;
        public ucXoaPhucLoiCuaNhanVien(MainWindow main, ucDanhSachNVHuongPhucLoi uc, OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.ListUserFinal xoa,int id, int type = 0)
        {
            InitializeComponent();
            Main = main;
            frmNVHuongPL = uc;
            NVTrongPL = xoa;
            IdPL = id.ToString();
            this.type = type;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodXacNhanXoa_MouseEnter(object sender, MouseEventArgs e)
        {
            bodXacNhanXoa.BorderThickness = new Thickness(1);
        }

        private void bodXacNhanXoa_MouseLeave(object sender, MouseEventArgs e)
        {
            bodXacNhanXoa.BorderThickness = new Thickness(0);
        }

        private async void bodXacNhanXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_nv_nhom")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("cls_id_cl", IdPL);
                    request.AddParameter("cls_id_user", NVTrongPL.idQLC);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = await restclient.ExecuteAsync(request);
                    var b = resAlbum.Content;
                    if (b.Contains("success"))
                    {
                        if(type == 0)
                        {
                            frmNVHuongPL.lstFinal.Remove(NVTrongPL);
                            frmNVHuongPL.dgvListSaffInsurance.ItemsSource = null;
                            frmNVHuongPL.dgvListSaffInsurance.ItemsSource = frmNVHuongPL.lstFinal;
                        }
                        else
                        {
                            frmNVHuongPL.lstFinal.Remove(NVTrongPL);
                            frmNVHuongPL.dgvPC.ItemsSource = null;
                            frmNVHuongPL.dgvPC.ItemsSource = frmNVHuongPL.lstFinal;
                        }
                        this.Visibility = Visibility.Collapsed;
                    }
                }
            }
            catch
            {

            }
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {

            bodCancel.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
           
        }

        private void bodCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
