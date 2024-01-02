using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.APIs;
using QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ThietLapCongTy.Comons;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Net.Http;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Serialization;
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
using static QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupSoDoToChuc.ucThemToChuc;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Popups.PopupSoDoToChuc
{
    /// <summary>
    /// Interaction logic for ucXemChiTiet.xaml
    /// </summary>
    /// 

    public partial class ucXemChiTiet : UserControl
    {
        public string com_id = "";
        public int org_id = 0;
        public string Token;
        public string json = "";
        List<ListUserEntities.UserData> listEmployee = new List<ListUserEntities.UserData>();

        public class Saff2
        {
            public string name { get; set; }
            public string id { get; set; }
            public string vitri { get; set; }
        }
        List<Saff2> saffList2 = new List<Saff2>();

        ObservableCollection<Saff2> listsaff21;
        CollectionViewSource view = new CollectionViewSource();
        BrushConverter br = new BrushConverter();
        public event NotifyCollectionChangedEventHandler CollectionChanged;
        int currentPageIndex = 0;
        int itemPerPage = 5;
        int totalPage = 0;
        public ucXemChiTiet(string com_id, int org_id, string Token)
        {
            InitializeComponent();
            this.com_id = com_id;
            this.org_id = org_id;
            this.Token = Token;
            getAllData();

        }

        #region Click Event
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnThoatChiTiet_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        #endregion

        #region Hover Event
        private void btnThoatChiTiet_MouseEnter(object sender, MouseEventArgs e)
        {
            btnThoatChiTiet.Background = (Brush)br.ConvertFrom("#FF451C");
        }

        private void btnThoatChiTiet_MouseLeave(object sender, MouseEventArgs e)
        {
            btnThoatChiTiet.Background = (Brush)br.ConvertFrom("#6666");
        }

        #endregion
        #region CallApi
        public async Task GetOrganizeDescription()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.listAll_organize);

                request.Headers.Add("authorization", "Bearer " + Token);

                var content = new StringContent("{\"id\":" + org_id + ",\"com_id\":" + com_id + "}", null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ListOrganizeEntities.Root result = JsonConvert.DeserializeObject<ListOrganizeEntities.Root>(responContent);
                    ListOrganizeEntities.OrganizeData organize = result.data.data[0];

                    foreach (var item in organize.content)
                    {

                    }
                    lsvListContent.ItemsSource = organize.content;

                    var DataObject = new
                    {
                        listOrganizeDetailId = organize.listOrganizeDetailId,
                        ep_status = "Active",
                        pageSize = 1000
                    };
                    json = JsonConvert.SerializeObject(DataObject);

                }


            }

            catch
            {

            }
        }

        public async void GetListUser()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.userManager_listUsers);
                request.Headers.Add("authorization", "Bearer " + Token);

                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    ListUserEntities.Root result = JsonConvert.DeserializeObject<ListUserEntities.Root>(responContent);
                    listEmployee = result.data.data;
                    lsvDanhSachNhanVienChiTiet.ItemsSource = listEmployee.Take(paginNV.DisplayNumber);
                    if (paginNV.SelectedPage == 0) paginNV.TotalRecords = listEmployee.Count;

                }


            }
            catch
            {

            }
        }

        public async void getAllData()
        {
            await GetOrganizeDescription();
            GetListUser();
        }
        #endregion

        #region Paging
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            Paging(paginNV.SelectedPage);
        }
        public void Paging(int pageNumber)
        {
            lsvDanhSachNhanVienChiTiet.ItemsSource = listEmployee.Skip((pageNumber - 1) * paginNV.DisplayNumber).Take(paginNV.DisplayNumber);
        }
        #endregion

        private void lsvDanhSachNhanVienChiTiet_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset- e.Delta);
        }
    }
}
