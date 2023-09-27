using ChamCong365.SalarySettings.DiMuonVeSom;
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

namespace ChamCong365.Popup.CaiDatLuong.NghiSaiQuyDinh
{
    /// <summary>
    /// Interaction logic for PopUpChiTietMucPhatNghiSaiQD.xaml
    /// </summary>
    public partial class PopUpChiTietMucPhatNghiSaiQD : UserControl
    {
        private MainWindow Main;
        private frmNghiSaiQuyDinh frmNghiSaiQD;
        private OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa phatNghiSaiQD = new OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa();
        private List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa> lstphatNghiSaiQD = new List<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa>();

        public PopUpChiTietMucPhatNghiSaiQD(MainWindow main, frmNghiSaiQuyDinh frm, OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsDSCaiDatNghiSaiQD.ListPhatCa phat)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            frmNghiSaiQD = frm;
            phatNghiSaiQD = phat;
            
            lstphatNghiSaiQD.Add(phat);
            dgv.ItemsSource = lstphatNghiSaiQD;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            phatNghiSaiQD.OnOff = 0;
            this.Visibility = Visibility.Collapsed;
        }

        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void btnChinhSua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            phatNghiSaiQD.OnOff = 1;
        }

        private void btnSave_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            
            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/update_phat_ca")))
            {
                RestRequest request = new RestRequest();
                request.Method = Method.Post;
                request.AlwaysMultipartFormData = true;
                request.AddParameter("pc_money", textTienPhatSauKhiSua);
                request.AddParameter("pc_shift", phatNghiSaiQD.pc_shift);
                request.AddParameter("pc_time", phatNghiSaiQD.pc_time);
                request.AddParameter("pc_type", 1);
                request.AddParameter("pc_id", phatNghiSaiQD.pc_id);
                request.AddParameter("token", Properties.Settings.Default.Token);
                RestResponse resAlbum = restclient.Execute(request);
                var b = resAlbum.Content;
                foreach(var item in frmNghiSaiQD.lstPC)
                {
                    if (item._id == phatNghiSaiQD._id)
                    {
                        item.pc_money = int.Parse(textTienPhatSauKhiSua);
                        item.pc_money_str = textTienPhatSauKhiSua + " VND/Ca";
                    }
                }
                frmNghiSaiQD.dgv.ItemsSource = null;
                frmNghiSaiQD.dgv.ItemsSource = frmNghiSaiQD.lstPC;
                this.Visibility = Visibility.Collapsed;
                phatNghiSaiQD.OnOff = 0;
            }
        }
        private string textTienPhatSauKhiSua;
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            textTienPhatSauKhiSua = textBox.Text;
        }

        private void btnXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
            Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaMucPhat(Main, frmNghiSaiQD, phatNghiSaiQD, this));
        }
    }
}
