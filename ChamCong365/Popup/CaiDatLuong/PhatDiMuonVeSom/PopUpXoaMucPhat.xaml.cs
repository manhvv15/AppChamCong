using ChamCong365.SalarySettings.DiMuonVeSom;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

namespace ChamCong365.Popup.CaiDatLuong.PhatDiMuonVeSom
{
    /// <summary>
    /// Interaction logic for PopUpXoaMucPhat.xaml
    /// </summary>
    public partial class PopUpXoaMucPhat : UserControl
    {
        private MainWindow Main;
        private OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo info;
        private frmCaiDatDiMuonVeSom frmDMVS;
        public PopUpXoaMucPhat(MainWindow main, OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo inf, frmCaiDatDiMuonVeSom frm)
        {
            InitializeComponent();
            Main = main;
            info = inf;
            frmDMVS = frm;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnHuy_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnDongY_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            //using (WebClient httpClient = new WebClient())
            //{
            //    try
            //    {
            //        httpClient.QueryString.Clear();
            //        httpClient.QueryString.Add("pm_id", info.pm_id.ToString());
            //        httpClient.Headers.Add("token", Properties.Settings.Default.Token);
            //        httpClient.UploadValuesAsync(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_phat_muon"), "POST", httpClient.QueryString);
            //        httpClient.UploadValuesCompleted += (s, ec) =>
            //        {
            //            frmDMVS.lstPhatMuon.Remove(info);
            //            frmDMVS.dgv.ItemsSource = null;
            //            frmDMVS.dgv.ItemsSource = frmDMVS.lstPhatMuon;
            //            this.Visibility = Visibility.Collapsed;
            //        };

            //    }
            //    catch
            //    {

            //    }
            //}

            //using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/delete_phat_muon")))
            //{
            //    RestRequest request = new RestRequest();
            //    request.Method = Method.Post;
            //    request.AlwaysMultipartFormData = true;

            //    request.AddParameter("pm_id", Main.IdAcount);
            //    request.AddParameter("token", Properties.Settings.Default.Token);
            //    RestResponse resAlbum = restclient.Execute(request);
            //    var b = resAlbum.Content;
            //    frmDMVS.lstPhatMuon.Remove(info);
            //    frmDMVS.dgv.ItemsSource = null;
            //    frmDMVS.dgv.ItemsSource = frmDMVS.lstPhatMuon;
            //    this.Visibility = Visibility.Collapsed;


            //}
            DeletePhatDMVS();
            

        }
        private async void DeletePhatDMVS()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3009/api/tinhluong/congty/delete_phat_muon");
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(info.pm_id.ToString()), "pm_id");
            content.Add(new StringContent(Properties.Settings.Default.Token), "token");
            request.Content = content;
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            frmDMVS.lstPhatMuon.Remove(info);
            frmDMVS.dgv.ItemsSource = null;
            frmDMVS.dgv.ItemsSource = frmDMVS.lstPhatMuon;
            this.Visibility = Visibility.Collapsed;
        }
    }
}
