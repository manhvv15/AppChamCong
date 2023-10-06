using ChamCong365.APIs;
using ChamCong365.NhanVien.DetailOfDon;
using ChamCong365.OOP.NhanVien.DeXuatCuaToi;
using Newtonsoft.Json;
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
using System.Windows.Shapes;

namespace ChamCong365.NhanVien.Propose
{
    /// <summary>
    /// Interaction logic for DeXuatCuaToi.xaml
    /// </summary>
    public partial class DeXuatCuaToi : Window
    {
        MainChamCong Main;
        int ep_id;
        int com_id;
        string com_name;
        public DeXuatCuaToi(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
            lsvDeXuatGanDay.ItemsSource = new List<int>() { 1, 2, 3, 4 };
            GetInforHome();
        }

        public async void GetInforHome()
        {
            List<ListCateDxEntites.Showcatedx> listCateDx = new List<ListCateDxEntites.Showcatedx>();
            listCateDx = await GetListCateDx();
            try {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.dexuat_ShowHome);
                request.Headers.Add("authorization", "Bearer " +Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if(response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();   
                    DeXuatCuaToi_Entities.Root result = JsonConvert.DeserializeObject<DeXuatCuaToi_Entities.Root>(responseContent);
                    DeXuatCuaToi_Entities.Data data = result.data;
                    foreach(var item in data.data)
                    {
                        item.type_dx_string = listCateDx.Where(x => x._id == item.type_dx).FirstOrDefault()?.name_cate_dx;
                    }
                    ShowData(data);
                }

            }
            catch(Exception ex) {
                MessageBox.Show(ex.Message);
            }

        }

        public async Task<List<ListCateDxEntites.Showcatedx>> GetListCateDx()
        {
            List<ListCateDxEntites.Showcatedx> list = new List<ListCateDxEntites.Showcatedx>();
            try {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.showlistcate_api);
                request.Headers.Add("Authorization", "Bearer "+Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if( response.IsSuccessStatusCode ) {
                    var responseContent = await response.Content.ReadAsStringAsync();   
                    ListCateDxEntites.Root result = JsonConvert.DeserializeObject<ListCateDxEntites.Root>(responseContent);
                    list = result.data.showcatedx;
                    return list;
                }

            }
            catch { } 
            return list;
        }

        public void ShowData(DeXuatCuaToi_Entities.Data data)
        {
            txbTotaldx.Text = data.totaldx.ToString();
            txbDxCanduyet.Text = data.dxCanduyet.ToString();
            txbDxChoDuyet.Text = data.dxChoDuyet .ToString();
            txbDxduyet.Text = data.dxduyet.ToString();
            lsvDeXuatGanDay.ItemsSource = data.data;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            listTess uc = new listTess(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            ToiGuiDi uc = new ToiGuiDi(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void DetailDeXuat_MouseUp(object sender, MouseButtonEventArgs e)
        {

            var dx = ((Border)sender).DataContext as DeXuatCuaToi_Entities.InforDx;
            if(dx != null)
            {
                int dx_id = dx._id;
                ucChiTietDeXuat uc = new ucChiTietDeXuat(Main,dx_id);
                Main.dopBody.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                Main.dopBody.Children.Add(Content as UIElement);
            }

        }
    }
}
