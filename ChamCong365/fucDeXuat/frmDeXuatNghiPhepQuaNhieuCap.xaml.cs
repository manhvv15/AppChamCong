using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

namespace ChamCong365.fucDeXuat
{
    /// <summary>
    /// Interaction logic for frmDeXuatNghiPhepQuaNhieuCap.xaml
    /// </summary>
    public partial class frmDeXuatNghiPhepQuaNhieuCap : Page
    {
        string list_IdUser_2CapDuyet = "";
        string list_IdUser_3CapDuyet = "";
        private MainWindow Main;
        public frmDeXuatNghiPhepQuaNhieuCap(MainWindow main)
        {
            InitializeComponent();
            Main = main; ;
            BrushConverter brus = new BrushConverter();

            dgv.Background = (Brush)brus.ConvertFrom("#D9EAFF");

        }

        private async Task<List<string>> GetListIdUser()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.vanthu_setting_getData_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = response.Content.ReadAsStringAsync();
                    DXSettingEntities.Root result = new DXSettingEntities.Root();
                    list_IdUser_2CapDuyet = result.data.settingDx.list_user_2cap;
                    list_IdUser_3CapDuyet = result.data.settingDx.list_user_3cap;
                    List<string> listId;
                    listId = (list_IdUser_2CapDuyet+list_IdUser_3CapDuyet).Split(',').ToList();
                    return listId;
                }
            }
            catch {
             
            }

            return new List<string>();

        }
        public async Task<List<Position>> GetListPosition()
        {
            List<Position> list = new List<Position>();
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_position_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    PositionRoot result = JsonConvert.DeserializeObject<PositionRoot>(responseContent);
                    if (result.data.data.Count > 0)
                    {
                        list = result.data.data;
                    }
                }

                return list;
            }
            catch
            {
                MessageBox.Show("Có lỗi khi lấy danh sách chức vụ");
            }
            return new List<Position>();
        }
        private async void GetListEmployee()
        {
            try
            {
                List<OrgStructureEmployee.Employee> EmployeeList = new List<OrgStructureEmployee.Employee>();   
                List<string> listIdUser = await GetListIdUser();
                List<Position> listPosition = await GetListPosition();


                foreach(var Id in  listIdUser) {

                    var client = new HttpClient();
                    var request = new HttpRequestMessage(HttpMethod.Post, API.organizationalStructure_listEp);
                    request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var content = new MultipartFormDataContent();
                    //if (com_id != 0) content.Add(new StringContent(this.com_id.ToString()), "com_id");
                    content.Add(new StringContent(Id), "emp_id");

                    request.Content = content;
                    var response = await client.SendAsync(request);
                    var responseContent = await response.Content.ReadAsStringAsync();
                    if (response.IsSuccessStatusCode)
                    {
                        OrgStructureEmployee.Root result = JsonConvert.DeserializeObject<OrgStructureEmployee.Root>(responseContent);
           
                    }
                }
            }
            catch(Exception ex) { }

        }


        private void dgv_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);
        }

        private void btnChonNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new Popup.DeXuat.LoaiHinhDuyetPhep.PopUpChonNhanVien(Main));
        }
    }
}
