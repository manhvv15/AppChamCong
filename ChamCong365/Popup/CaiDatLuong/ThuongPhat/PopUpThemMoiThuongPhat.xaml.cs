using ChamCong365.SalarySettings;
using Newtonsoft.Json;
using RestSharp;
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
    /// Interaction logic for PopUpThemMoiThuongPhat.xaml
    /// </summary>
    public partial class PopUpThemMoiThuongPhat : UserControl
    {
        private MainWindow Main;
        private frmThuongPhat.ThuongPhat ThuongPhat;
        private int stt;
        public OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal thuongP = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal();
        public PopUpThemMoiThuongPhat(MainWindow main, OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp)
        {
            InitializeComponent();
            Main = main;
            dgvThuong.ItemsSource = tp.tt_thuong.ds_thuong;
            dgvPhat.ItemsSource = tp.tt_phat.ds_phat;
            thuongP = tp;
            stt = 1;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void RadioTienThuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            borVienTT.BorderBrush = (Brush)bc.ConvertFrom("#4c5bd4");
            borTienThuong.Background= (Brush)bc.ConvertFrom("#4c5bd4");
            borTienPhat.Background = (Brush)bc.ConvertFrom("#c4c4c4");
            borVienTP.BorderBrush = (Brush)bc.ConvertFrom("#c4c4c4");
            stt = 1;
        }

        private void RadioTienPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            borVienTT.BorderBrush = (Brush)bc.ConvertFrom("#c4c4c4");
            borTienThuong.Background = (Brush)bc.ConvertFrom("#c4c4c4");
            borTienPhat.Background = (Brush)bc.ConvertFrom("#4c5bd4");
            borVienTP.BorderBrush = (Brush)bc.ConvertFrom("#4c5bd4");
            stt = 2;
        }

        private void btnThemTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_thuong_phat")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;
                    request.AddParameter("pay_id_user", thuongP.inforUser.idQLC);
                    request.AddParameter("pay_id_com", Main.IdAcount);
                    request.AddParameter("pay_price", textTienTP.Text);
                    request.AddParameter("pay_status", stt);
                    request.AddParameter("pay_case", textGhiChu.Text);
                    string[] year1 = dtpNgayAD.Text.Split(Convert.ToChar("/"));
                    string y = year1[2];
                    string d = year1[1];
                    string m = year1[0];
                    if (int.Parse(d) < 10 && int.Parse(m) < 10)
                    {
                        request.AddParameter("pay_day", y + "-0" + m + "-0" + d + "T00:00:00.000+00:00");

                    }
                    else if (int.Parse(d) > 10 && int.Parse(m) < 10)
                    {
                        request.AddParameter("pay_day", y + "-0" + m + "-" + d + "T00:00:00.000+00:00");
                    }
                    else if (int.Parse(d) < 10 && int.Parse(m) > 10)
                    {
                        request.AddParameter("pay_day", y + "-" + m + "-0" + d + "T00:00:00.000+00:00");
                    }
                    else
                    {
                        request.AddParameter("pay_day", y + "-" + m + "-" + d + "T00:00:00.000+00:00");

                    }
                    request.AddParameter("pay_month", m);
                    request.AddParameter("pay_year", y);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.ThuongPhat.clsAddTP.Root add = JsonConvert.DeserializeObject<OOP.CaiDatLuong.ThuongPhat.clsAddTP.Root>(b);
                    if (add.data != null)
                    {
                        if (add.data.newobj1.pay_status == 1)
                        {
                            thuongP.tt_thuong.ds_thuong.Add(add.data.newobj1);
                            dgvThuong.ItemsSource = null;
                            dgvThuong.ItemsSource = thuongP.tt_thuong.ds_thuong;
                        }
                        else if (add.data.newobj1.pay_status == 2)
                        {
                            OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat dtp = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat();
                            dtp.pay_id = add.data.newobj1.pay_id;
                            dtp.pay_case = add.data.newobj1.pay_case;
                            dtp.pay_day = add.data.newobj1.pay_day;
                            dtp.pay_group = add.data.newobj1.pay_group;
                            dtp.pay_id_com = add.data.newobj1.pay_id_com;
                            dtp.pay_id_user = add.data.newobj1.pay_id_user;
                            dtp.pay_month = add.data.newobj1.pay_month;
                            dtp.pay_nghi_le = add.data.newobj1.pay_nghi_le;
                            dtp.pay_price = add.data.newobj1.pay_price;
                            dtp.pay_status = add.data.newobj1.pay_status;
                            dtp.pay_time_created = add.data.newobj1.pay_time_created;
                            dtp.pay_year = add.data.newobj1.pay_year;
                            dtp._id = add.data.newobj1._id;
                            thuongP.tt_phat.ds_phat.Add(dtp);
                            dgvPhat.ItemsSource = null;
                            dgvPhat.ItemsSource = thuongP.tt_phat.ds_phat;
                        }
                    }
                }

            }
            catch
            {

            }
        }

        private void dgvThuong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void dgvPhat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void btnXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat phat = (sender as Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat;
            if (phat != null)
            {
                Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaThuongPhat(Main, phat, "Bạn có chắc chăn muốn xoá mức phạt này không?", this));
            }
        }
    }
}
