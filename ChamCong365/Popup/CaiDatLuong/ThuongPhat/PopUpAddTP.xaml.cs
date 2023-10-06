using ChamCong365.SalarySettings;
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
    /// Interaction logic for PopUpAddTP.xaml
    /// </summary>
    public partial class PopUpAddTP : UserControl
    {
        private MainWindow Main;
        private frmThuongPhat frmTP;
        public PopUpAddTP(MainWindow main, frmThuongPhat frm)
        {
            InitializeComponent();
            Main = main;
            frmTP = frm;
            LoadDLNhanVien();
        }

        private void LoadDLNhanVien()
        {
            foreach(var item in Main.lstNhanVienThuocCongTy)
            {
                cboNV.Items.Add(item.idQLC + "-" + item.userName);
            }
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnThemMoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_thuong_phat")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                string[] Id = cboNV.Text.Split(Convert.ToChar("-"));
                string IdNV = Id[0];
                request.AddParameter("pay_id_user", IdNV);
                request.AddParameter("pay_id_com", Main.IdAcount);
                request.AddParameter("pay_price", textTienTP.Text);
                if (cboTP.Text == "Thưởng")
                {
                    request.AddParameter("pay_status", 1);
                }
                else if(cboTP.Text == "Phạt")
                {
                    request.AddParameter("pay_status", 2);

                }
                request.AddParameter("pay_case", textLyDo.Text);
                string[] year1 = dtpThoiGian.Text.Split(Convert.ToChar("/"));
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
                frmTP.LoadDLThuongPhat();
                this.Visibility = Visibility.Collapsed;
                //OOP.CaiDatLuong.ThuongPhat.clsAddTP.Root add = JsonConvert.DeserializeObject<OOP.CaiDatLuong.ThuongPhat.clsAddTP.Root>(b);
                //if (add.data != null)
                //{
                //    if (add.data.newobj1.pay_status == 1)
                //    {
                //        thuongP.tt_thuong.ds_thuong.Add(add.data.newobj1);
                //        dgvThuong.ItemsSource = null;
                //        dgvThuong.ItemsSource = thuongP.tt_thuong.ds_thuong;
                //    }
                //    else if (add.data.newobj1.pay_status == 2)
                //    {
                //        OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat dtp = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat();
                //        dtp.pay_id = add.data.newobj1.pay_id;
                //        dtp.pay_case = add.data.newobj1.pay_case;
                //        dtp.pay_day = add.data.newobj1.pay_day;
                //        dtp.pay_group = add.data.newobj1.pay_group;
                //        dtp.pay_id_com = add.data.newobj1.pay_id_com;
                //        dtp.pay_id_user = add.data.newobj1.pay_id_user;
                //        dtp.pay_month = add.data.newobj1.pay_month;
                //        dtp.pay_nghi_le = add.data.newobj1.pay_nghi_le;
                //        dtp.pay_price = add.data.newobj1.pay_price;
                //        dtp.pay_status = add.data.newobj1.pay_status;
                //        dtp.pay_time_created = add.data.newobj1.pay_time_created;
                //        dtp.pay_year = add.data.newobj1.pay_year;
                //        dtp._id = add.data.newobj1._id;
                //        thuongP.tt_phat.ds_phat.Add(dtp);
                //        dgvPhat.ItemsSource = null;
                //        dgvPhat.ItemsSource = thuongP.tt_phat.ds_phat;
                //    }
                //}
            }
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
