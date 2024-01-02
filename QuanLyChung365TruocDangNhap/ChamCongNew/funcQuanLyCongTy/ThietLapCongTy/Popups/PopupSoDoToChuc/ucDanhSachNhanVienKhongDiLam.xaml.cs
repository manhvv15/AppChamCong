
using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Comons;
using QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Entities;
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


namespace QuanLyChung365TruocDangNhap.ChamCongNew.ThietLapCongTy.Popups.PopupSoDoToChuc
{
    /// <summary>
    /// Interaction logic for ucDanhSachNhanVienDiLam.xaml
    /// </summary>
    public partial class ucDanhSachNhanVienKhongDiLam : UserControl
    {
        private List<API_List_Employee_Untimed.EmployeeData> listAllUserNoTimekeep = new List<API_List_Employee_Untimed.EmployeeData>();
        private Dictionary<string, string> ListStaffName = new Dictionary<string, string>();
        string Token = "";
        List<API_Tree_SoDoToChuc.ListOrganizeDetailId> listOrganizeDetailId;
        public ucDanhSachNhanVienKhongDiLam()
        {
            InitializeComponent();

        }

        public ucDanhSachNhanVienKhongDiLam(string token, List<API_Tree_SoDoToChuc.ListOrganizeDetailId> listOrganizeDetailId)
        {
            InitializeComponent();
            this.Token = token;
            this.listOrganizeDetailId = listOrganizeDetailId;
            GetListEmployeeTimeKeep();
            GetListEmployeeTimeKeepNoPaging();
        }
        private void ClosePopup_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        private void cbxStaffName_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxStaffName.SelectedIndex = -1;
                string textSearch = cbxStaffName.Text;
                cbxStaffName.Items.Refresh();
                cbxStaffName.ItemsSource = ListStaffName.Where(t => t.Value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
            }
        }

        private void cbxStaffName_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            try
            {
                cbxStaffName.SelectedIndex = -1;
                string textSearch = cbxStaffName.Text + e.Text;
                cbxStaffName.IsDropDownOpen = true;
                if (textSearch == "")
                {
                    cbxStaffName.Text = "";
                    cbxStaffName.Items.Refresh();
                    cbxStaffName.ItemsSource = ListStaffName;
                    cbxStaffName.SelectedIndex = -1;
                }
                else
                {
                    cbxStaffName.ItemsSource = "";
                    cbxStaffName.Items.Refresh();
                    cbxStaffName.ItemsSource = ListStaffName.Where(t => t.Value.ToLower().Contains(textSearch.RemoveUnicode().ToLower()));
                }
            }
            catch { }
        }



        #region CallApi 

        public async void GetListEmployeeTimeKeep(int pageNumber = 1)
        {
            try
            {
                var ContentData = new
                {
                    pageSize = 10,
                    pageNumber = pageNumber,
                    type_timekeep = 2,
                    listOrganizeDetailId = listOrganizeDetailId

                };
                string json = JsonConvert.SerializeObject(ContentData);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_Employee_listEmUntimed);
                request.Headers.Add("Authorization", "Bearer " + Token);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_List_Employee_Untimed.Root result = JsonConvert.DeserializeObject<API_List_Employee_Untimed.Root>(responseContent);
                    List<API_List_Employee_Untimed.EmployeeData> list = result.data.data;
                    if (paginNV.SelectedPage == 0) paginNV.TotalRecords = (int)result.data.totalCount;
                    dgDanhSachNhanVienDiLam.ItemsSource = list;

                }

            }
            catch { }
        }

        public async void GetListEmployeeTimeKeepNoPaging()
        {
            try
            {
                var ContentData = new
                {
                    pageSize = 10000,
                    pageNumber = 1,
                    type_timekeep = 2,
                    listOrganizeDetailId = listOrganizeDetailId

                };
                string json = JsonConvert.SerializeObject(ContentData);

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, Api_ThietLapCongTy.Api_Employee_listEmUntimed);
                request.Headers.Add("Authorization", "Bearer " + Token);
                var content = new StringContent(json, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    API_List_Employee_Untimed.Root result = JsonConvert.DeserializeObject<API_List_Employee_Untimed.Root>(responseContent);
                    ListStaffName.Add("0", "Tất cả");
                    foreach (var item in result.data.data)
                    {
                        ListStaffName.Add(item.idQLC.ToString(), item.userName);
                    }
                    cbxStaffName.ItemsSource = ListStaffName;
                    listAllUserNoTimekeep = result.data.data;


                }

            }
            catch { }
        }
        private void paginNV_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            GetListEmployeeTimeKeep(paginNV.SelectedPage);
        }
        #endregion

        private void cbxStaffName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cbxStaffName.SelectedIndex != -1 && cbxStaffName.SelectedIndex != 0)
                {
                    dgDanhSachNhanVienDiLam.ItemsSource = listAllUserNoTimekeep.Where(x => x.idQLC == int.Parse(cbxStaffName.SelectedValue.ToString()));
                }
                else
                {
                    dgDanhSachNhanVienDiLam.ItemsSource = listAllUserNoTimekeep.Take(10);
                }
            }
            catch { }
        }

        private void dgDanhSachNhanVienDiLam_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }
    }
}
