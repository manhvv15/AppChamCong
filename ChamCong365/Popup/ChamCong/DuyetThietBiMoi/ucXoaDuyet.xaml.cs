using ChamCong365.TimeKeeping;
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

namespace ChamCong365.Popup.ChamCong.DuyetThietBiMoi
{
    /// <summary>
    /// Interaction logic for ucXoaDuyet.xaml
    /// </summary>
    public partial class ucXoaDuyet : UserControl
    {
        BrushConverter bc = new BrushConverter();
        MainWindow Main;
        private string ed_id;
        ucDuyetThietBiMoi ucDuyet;
        public ucXoaDuyet(MainWindow main, string ID, ucDuyetThietBiMoi ucduyet)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Main = main;
            this.ed_id = ID;
            this.ucDuyet = ucduyet;
        }
        #region Evendef
   

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuy_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            txbHuy.Foreground = (Brush)bc.ConvertFrom("#FFFFFF");
        }

        private void bodHuy_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuy.Background = (Brush)bc.ConvertFrom("#FFFFFF");
            txbHuy.Foreground = (Brush)bc.ConvertFrom("#4C5BD4 ");
        }
        private async void XacNhanXoa(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Delete, APIs.API.Xoa_DuyetTB_Api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (ed_id != null)
                {
                    content.Add(new StringContent(ed_id.ToString()), "ed_id");
                }
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    bodXoaDuyet.Visibility = Visibility.Collapsed;
                    ucDuyet.LoadTBDuyet();
                }

            }
            catch (Exception)
            { }
        }

        private void Huy(object sender, MouseButtonEventArgs e)
        {
            this.Visibility=Visibility.Collapsed;
        }

        #endregion

    }
}
