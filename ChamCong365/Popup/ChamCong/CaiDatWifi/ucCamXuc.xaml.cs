using ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi;
using ChamCong365.OOP.ChamCong.CamXuc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
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

namespace ChamCong365.Popups.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucCamXuc.xaml
    /// </summary>
    public partial class ucCamXuc : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MainWindow Main;    
        BrushConverter bc = new BrushConverter();
        public ucCamXuc(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            btnOnOff.HorizontalAlignment = HorizontalAlignment.Left;
            bodBackOnOff.Background = (Brush)bc.ConvertFrom("#ECECEC");
           
            LoadCamXuc();
        }


        #region Call Api Cảm Xúc

        private List<List_CamXuc> _listCamXuc;
        public List<List_CamXuc> listCamXuc
        {
            get { return _listCamXuc; }
            set { _listCamXuc = value; OnPropertyChanged(); }
        }
        public async void LoadCamXuc()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.DanhSach_CamXuc_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resConten = await response.Content.ReadAsStringAsync();

                Root_CamXuc resultCamXuc = JsonConvert.DeserializeObject<Root_CamXuc>(resConten);

                if (resultCamXuc != null)
                {
                    listCamXuc = resultCamXuc.data.list;
                    foreach (var item in listCamXuc)
                    {
                        tbDiemChuan.Text = item.avg_pass_score.ToString();
                    }
                    lsvDanhSachCamSuc.ItemsSource = listCamXuc;
                }
            }
            catch (Exception)
            {
            }
        }
        #endregion
        private async void BackOnOff(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.OnOff_CamXuc_Api);
                request.Headers.Add("Authorization", "Bearer "+ Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var resOnOff = await response.Content.ReadAsStringAsync();

                Root_OnOff_CamXuc result = JsonConvert.DeserializeObject<Root_OnOff_CamXuc>(resOnOff);
                if (result.data != null)
                {
                    if (btnOnOff.HorizontalAlignment == HorizontalAlignment.Left)
                    {
                        result.data.emotion_setting = true;
                        btnOnOff.HorizontalAlignment = HorizontalAlignment.Right;
                        bodBackOnOff.Background = (Brush)bc.ConvertFrom("#12DD00");
                    }
                    else
                    {
                        result.data.emotion_setting = false;
                        btnOnOff.HorizontalAlignment = HorizontalAlignment.Left;
                        bodBackOnOff.Background = (Brush)bc.ConvertFrom("#ECECEC");
                    }
                }
            }
            catch (Exception)
            {}
        }

        private void ThemThangDiem(object sender, MouseButtonEventArgs e)
        {
            List_CamXuc cx = new List_CamXuc();
            if (cx != null)
            {
                Main.grShowPopup.Children.Add(new ucThemMoiCamXuc(Main,cx,this));
            }
        }

        private void CapNhatCamXuc(object sender, MouseButtonEventArgs e)
        {
            List_CamXuc listCamXuc = (sender as Border).DataContext as List_CamXuc;
            if (listCamXuc != null)
            {
                Main.grShowPopup.Children.Add(new ucCapNhatCamXuc(Main, listCamXuc, this));
            }
        }

        string tbStart = "";
        private void tb_Start(object sender, TextChangedEventArgs e)
        {
            ListView lv = sender as ListView;
      
        }

        private void XoaCamXuc(object sender, MouseButtonEventArgs e)
        {
            List_CamXuc listCamXuc = (sender as Border).DataContext as List_CamXuc;
            if (listCamXuc != null)
            {
                Main.grShowPopup.Children.Add(new ucXoaCamXuc(Main,listCamXuc, this));
            }
        }

        private async void LuuDiemChuanCamSuc(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, APIs.API.CapNhat_DiemChuan_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(tbDiemChuan.Text), "avg_score");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                   var resDiemChuan = await response.Content.ReadAsStringAsync();
                    LoadCamXuc();
                }  
            }
            catch (Exception)
            {
            }
        }

        private void btnHuyCamSuc_MouseEnter(object sender, MouseEventArgs e)
        {
            btnHuyCamSuc.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txtHuy.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void btnHuyCamSuc_MouseLeave(object sender, MouseEventArgs e)
        {
            btnHuyCamSuc.Background = (Brush)bc.ConvertFrom("#6666");
            txtHuy.Foreground = (Brush)bc.ConvertFrom("#474747");
        }
    }
}
