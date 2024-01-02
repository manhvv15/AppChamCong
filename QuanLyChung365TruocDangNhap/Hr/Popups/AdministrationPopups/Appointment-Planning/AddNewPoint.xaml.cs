using QuanLyChung365TruocDangNhap.Hr.Entities.AdministrationEntity.PersonnelChangeEntity;
using QuanLyChung365TruocDangNhap.Hr.Entities.ListItemCombobox;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.Hr.Popups.AdministrationPopups.Appointment_Planning
{
    /// <summary>
    /// Interaction logic for AddNewPoint.xaml
    /// </summary>
    public partial class AddNewPoint : UserControl
    {
        string Authorization;
        string id;
        int page_now = 1;
        const int COUNT_RECORD_PER_PAGE = 1000000;
        int TypeUser;
        // deligate event hide popups
        public delegate void HidePopup(int mode); //0: 0 load lại, 1:load lại
        public static event HidePopup hidePopup;

        List<Items3> listEmployee = new List<Items3>();
        List<Organize> listDepartment = new List<Organize>();
        List<Position> listPosition = new List<Position>();
        public AddNewPoint(string id, string Authorization, int TypeUser)
        {
            InitializeComponent();
            this.id = id;
            this.Authorization = Authorization;
            this.TypeUser = TypeUser;
            SetUpCombobox();
            GetDatalistEmployee();
            GetDatalistDepartment();
            GetDataPosition();
        }
        private void SetUpCombobox()
        {
            cbxQHBN.ItemsSource = ListItemCombobox.ListPositionApply;
            cbxChonQuyDinh.ItemsSource = ListItemCombobox.ListRegulations;
        }
        private void CancelPopup(object sender, MouseButtonEventArgs e)
        {
            hidePopup(0);
        }
        //lấy toàn bộ danh sách nhân viên 
        private async void GetDatalistEmployee()
        {
            var httpClient = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;


            string api = "https://api.timviec365.vn/api/qlc/managerUser/listUser";

            Dictionary<string, string> form = new Dictionary<string, string>();
            form.Add("com_id", id.ToString());
            form.Add("pageNumber", page_now.ToString());
            form.Add("pageSize", COUNT_RECORD_PER_PAGE.ToString());

            httpRequestMessage.RequestUri = new Uri(api);
            httpRequestMessage.Headers.Add("Authorization", "Bearer " + Authorization);
            httpRequestMessage.Content = new FormUrlEncodedContent(form);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            getListEmployee result = JsonConvert.DeserializeObject<getListEmployee>(responseContent);
            try
            {
                listEmployee = result.data.data;
                /*foreach (var item in listEmployee)
                {
                    string dep_name = " (Chưa cập nhật)";
                    if (item.dep_name != null && item.dep_name != "")
                    {
                        dep_name = " (" + item.dep_name + " - ID: " + item.ep_id + ")";
                    }
                    item.ep_name = item.ep_name + dep_name;


                }*/
                cbxTenNV.ItemsSource = listEmployee;
            }
            catch
            {

            }

        }
        //Lấy toàn bộ danh sách phòng ban
        private async void GetDatalistDepartment()
        {
            var httpClient = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            string api = "https://api.timviec365.vn/api/qlc/organizeDetail/listAll";
            Dictionary<string, string> form = new Dictionary<string, string>();
            form.Add("com_id", id.ToString());
            httpRequestMessage.RequestUri = new Uri(api);
            httpRequestMessage.Headers.Add("Authorization", "Bearer " + Authorization);
            httpRequestMessage.Content = new FormUrlEncodedContent(form);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            APIOrganize result = JsonConvert.DeserializeObject<APIOrganize>(responseContent);
            listDepartment = result.data.data;
            cbxTenPhongBanMoi.ItemsSource = listDepartment;
        }
        private async void GetDataPosition()
        {
            var httpClient = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;
            string api = "https://api.timviec365.vn/api/qlc/positions/listAll";
            Dictionary<string, string> form = new Dictionary<string, string>();
            form.Add("com_id", id.ToString());
            httpRequestMessage.RequestUri = new Uri(api);
            httpRequestMessage.Headers.Add("Authorization", "Bearer " + Authorization);
            httpRequestMessage.Content = new FormUrlEncodedContent(form);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            ApiPosition result = JsonConvert.DeserializeObject<ApiPosition>(responseContent);
            listPosition = result.data.data;
            cbxQHBN.ItemsSource = listPosition;
        }
        private void AddNewDataPoint(object sender, MouseButtonEventArgs e)
        {
            if (ValidateForm())
            {
                GetDataAddNewPoint();

            }
        }
        //Thêm mới quy hoạch bổ nhiệm
        public class DataAddNewPoint
        {
            public int ep_id { get; set; }
            public int current_position_id { get; set; }
            public int current_organizeDetailId { get; set; }
            public List<ListOrganizeDetailId_Hr> current_listOrganizeDetailId { get; set; }
            public string created_at { get; set; }
            public int decision_id { get; set; }
            public string note { get; set; }
            public int update_position_id { get; set; }
            public List<ListOrganizeDetailId> update_listOrganizeDetailId { get; set; }
            public int update_organizeDetailId { get; set; }
        }
        private async void GetDataAddNewPoint()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();

                httpRequestMessage.Method = HttpMethod.Post;
                string api = APIs.API.addAppoint;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + Authorization);
                var parameters = new List<KeyValuePair<string, string>>();


                 Items3 items3 = (Items3)cbxTenNV.SelectedItem;

                DataAddNewPoint data = new DataAddNewPoint();
                data.ep_id = int.Parse(cbxTenNV.SelectedValue.ToString());
                data.current_position_id = int.Parse(items3.position_id);
                data.current_listOrganizeDetailId = items3.listOrganizeDetailId;
                data.update_organizeDetailId = ((Organize)cbxTenPhongBanMoi.SelectedItem).id;
                data.update_listOrganizeDetailId = ((Organize)cbxTenPhongBanMoi.SelectedItem).listOrganizeDetailId;
                data.update_position_id = ((Position)cbxQHBN.SelectedItem).id;
                data.created_at = DTdate.SelectedDate.Value.ToString("yyyy-MM-dd");
                if (cbxChonQuyDinh.SelectedIndex > -1)
                {
                    data.decision_id = int.Parse(cbxChonQuyDinh.SelectedValue.ToString());

                }
                data.note = StringFromRichTextBox(rtbLyDo);
                
                // Thiết lập Content
                var content = new StringContent(JsonConvert.SerializeObject(data), null, "application/json");
                httpRequestMessage.Content = content;

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();
                EntitySuccessMessage result = JsonConvert.DeserializeObject<EntitySuccessMessage>(responseContent);
                if (result.data != null && result.data.result)
                {
                    hidePopup(1);
                    CustomMessageBox.Show("Thêm thành công");
                    NotificationPersonnelChangeAppChat();
                }
                else
                {
                    CustomMessageBox.Show(result.error.message);
                }
            }
            catch
            {
                CustomMessageBox.Show("Có lỗi xảy ra, vui lòng thử lại!");
            }
        }

        private string StringFromRichTextBox(RichTextBox rtb)
        {
            TextRange textRange = new TextRange(
                rtb.Document.ContentStart,
                rtb.Document.ContentEnd
            );

            return textRange.Text;
        }
        public T GetFirstChildOfType<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject == null)
            {
                return null;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);

                var result = (child as T) ?? GetFirstChildOfType<T>(child);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
        private void cbxTenNV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //int indexSelection = cbxTenNV.SelectedIndex;
            //List<Items3> listEmployee = (List<Items3>)cbxTenNV.ItemsSource;
            //Items3 items3 = listEmployee[indexSelection];
            Items3 items3 = (Items3)cbxTenNV.SelectedItem;
            if (cbxTenNV.SelectedIndex == -1)
            {
                txtPhongBan.Text = "";
                txtChucVuHT.Text = "";
            }
            else if (items3.organizeDetailName == null)
            {
                items3.organizeDetailName = "Chưa cập nhật";
                txtPhongBan.Text = items3.organizeDetailName;
            }
            else
            {
                txtPhongBan.Text = items3.organizeDetailName;
                txtChucVuHT.Text = items3.positionName;
            }
            ComboBox cb = sender as ComboBox;
            cb.IsEditable = false;
            cbxQHBN.ForceCursor = true;
            cb.IsEditable = true;
            TextBox txt = GetFirstChildOfType<TextBox>(cbxTenNV);
            if (txt != null)
            {
                txt.SelectionStart = 0;
                txt.SelectionLength = 0;
            }
        }

        public double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        private bool ValidateForm()
        {
            if (cbxTenNV.SelectedIndex == -1)
            {
                CustomMessageBox.Show("Vui lòng chọn nhân viên.");
                return false;
            }

            if (cbxQHBN.SelectedIndex == -1)
            {
                CustomMessageBox.Show("Vui lòng chọn chức vụ.");
                return false;
            }

            if (cbxTenPhongBanMoi.SelectedIndex == -1)
            {
                CustomMessageBox.Show("Vui lòng chọn phòng ban.");
                return false;
            }

            if (DTdate.SelectedDate == null)
            {
                CustomMessageBox.Show("Vui lòng điền thời gian luân chuyển công tác.");
                return false;
            }

            return true;
        }

        private void cbxQHBN_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxQHBN.SelectedIndex = -1;
            string textSearch = cbxQHBN.Text + e.Text;
            cbxQHBN.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxQHBN.Text = "";
                cbxQHBN.Items.Refresh();
                cbxQHBN.ItemsSource = ListItemCombobox.ListPositionApply;
                cbxQHBN.SelectedIndex = -1;
            }
            else
            {
                cbxQHBN.ItemsSource = "";
                cbxQHBN.Items.Refresh();
                cbxQHBN.ItemsSource = ListItemCombobox.ListPositionApply.Where(t => t.value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxQHBN_KeyUp(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.Back)
            {
                cbxQHBN.SelectedIndex = -1;
                string textSearch = cbxQHBN.Text;
                cbxQHBN.Items.Refresh();
                cbxQHBN.ItemsSource = ListItemCombobox.ListPositionApply.Where(t => t.value.ToLower().Contains(textSearch.ToLower()));
            }*/
        }

        private void cbxTenNV_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxTenNV.SelectedIndex = -1;
            string textSearch = cbxTenNV.Text + e.Text;
            cbxTenNV.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxTenNV.Text = "";
                cbxTenNV.Items.Refresh();
                cbxTenNV.ItemsSource = listEmployee;
                cbxTenNV.SelectedIndex = -1;
            }
            else
            {
                cbxTenNV.ItemsSource = "";
                cbxTenNV.Items.Refresh();
                cbxTenNV.ItemsSource = listEmployee.Where(t => t.ep_name.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxTenNV_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxTenNV.SelectedIndex = -1;
                string textSearch = cbxTenNV.Text;
                cbxTenNV.Items.Refresh();
                cbxTenNV.ItemsSource = listEmployee.Where(t => t.ep_name.ToLower().Contains(textSearch.ToLower()));
            }
        }


        private void cbxTenPhongBan_KeyUp(object sender, KeyEventArgs e)
        {
            /*if (e.Key == Key.Back)
            {
                cbxTenPhongBanMoi.SelectedIndex = -1;
                string textSearch = cbxTenPhongBanMoi.Text;
                cbxTenPhongBanMoi.Items.Refresh();
                cbxTenPhongBanMoi.ItemsSource = listDepartment.Where(t => t.dep_name.ToLower().Contains(textSearch.ToLower()));
            }*/
        }

        private void cbxTenPhongBan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            /*cbxTenPhongBanMoi.SelectedIndex = -1;
            string textSearch = cbxTenPhongBanMoi.Text + e.Text;
            cbxTenPhongBanMoi.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxTenPhongBanMoi.Text = "";
                cbxTenPhongBanMoi.Items.Refresh();
                cbxTenPhongBanMoi.ItemsSource = listDepartment;
                cbxTenPhongBanMoi.SelectedIndex = -1;
            }
            else
            {
                cbxTenPhongBanMoi.ItemsSource = "";
                cbxTenPhongBanMoi.Items.Refresh();
                cbxTenPhongBanMoi.ItemsSource = listDepartment.Where(t => t.dep_name.ToLower().Contains(textSearch.ToLower()));
            }*/
        }

        private async void NotificationPersonnelChangeAppChat()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();

                httpRequestMessage.Method = HttpMethod.Post;
                string api = APIs.API.NotificationPersonnelChange;

                httpRequestMessage.RequestUri = new Uri(api);
                var parameters = new List<KeyValuePair<string, string>>();

                parameters.Add(new KeyValuePair<string, string>("SenderId", id));
                parameters.Add(new KeyValuePair<string, string>("EmployeeId", cbxTenNV.SelectedValue.ToString()));

                //string Status_com = "1";
                //string Status_emp = "2";

                string Status_com = TypeUser.ToString();

                if (Status_com == "1")
                {
                    parameters.Add(new KeyValuePair<string, string>("Status", Status_com));
                }
                else
                {
                    parameters.Add(new KeyValuePair<string, string>("Status", Status_com));
                }

                parameters.Add(new KeyValuePair<string, string>("ListReceive", "[" + cbxTenNV.SelectedValue.ToString() + "]"));
                parameters.Add(new KeyValuePair<string, string>("CompanyId", id));

                //int indexSelection = cbxTenNV.SelectedIndex;
                //List<Items3> listEmployee = (List<Items3>)cbxTenNV.ItemsSource;
                //Items3 items3 = listEmployee[indexSelection];
                Items3 items3 = (Items3)cbxTenNV.SelectedItem;

                parameters.Add(new KeyValuePair<string, string>("Position", cbxQHBN.Text));
                parameters.Add(new KeyValuePair<string, string>("Department", items3.dep_name));
                parameters.Add(new KeyValuePair<string, string>("CreateAt", DTdate.SelectedDate.Value.ToString("yyyy-MM-dd")));

                string type = "Appoint";
                parameters.Add(new KeyValuePair<string, string>("Type", type));
                parameters.Add(new KeyValuePair<string, string>("NewCompanyId", id));

                parameters.Add(new KeyValuePair<string, string>("NewCompanyName", items3.com_name));
                // Thiết lập Content
                var content = new FormUrlEncodedContent(parameters);
                httpRequestMessage.Content = content;
                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();
                MessNofiChat result = JsonConvert.DeserializeObject<MessNofiChat>(responseContent);
            }
            catch { }
            
            //if (result.data.result != null)
            //{
            //    CustomMessageBox.Show(result.data.message);
            //}
        }
    }
}
