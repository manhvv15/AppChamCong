using ChamCong365.SalarySettings;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.CaiDatPhucLoi
{
    /// <summary>
    /// Interaction logic for ucXoaPhucLoi.xaml
    /// </summary>
    public partial class ucXoaPhucLoi : UserControl
    {
        BrushConverter br = new BrushConverter();
        private ucCaiDatPhucLoi frmSettingPL;
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf PL;
        public ucXoaPhucLoi(MainWindow main,ucCaiDatPhucLoi uc, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf pl)
        {
            InitializeComponent();
            frmSettingPL = uc;
            PL = pl;
        }

        private void bodCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void bodXacNhanXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_phuc_loi")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("cl_id", PL.cl_id);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    frmSettingPL.lstPhucLoi.Remove(PL);
                    frmSettingPL.dgvCaiDatPhucLoi.ItemsSource = null;
                    frmSettingPL.dgvCaiDatPhucLoi.ItemsSource = frmSettingPL.lstPhucLoi;
                    this.Visibility = Visibility.Collapsed;
                }
            }
            catch
            {

            }
        }

        private void bodXacNhanXoa_MouseEnter(object sender, MouseEventArgs e)
        {
            bodXacNhanXoa.BorderThickness = new Thickness(1);
        }

        private void bodXacNhanXoa_MouseLeave(object sender, MouseEventArgs e)
        {
            bodXacNhanXoa.BorderThickness = new Thickness(0);
        }
    }
}
