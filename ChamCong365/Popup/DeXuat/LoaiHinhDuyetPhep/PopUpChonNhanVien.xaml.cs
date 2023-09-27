using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
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

namespace ChamCong365.Popup.DeXuat.LoaiHinhDuyetPhep
{
    /// <summary>
    /// Interaction logic for PopUpChonNhanVien.xaml
    /// </summary>
    public partial class PopUpChonNhanVien : UserControl
    {
        int com_id = -1;
        int dep_id = -1;
        List<Employee> EmployeeList = new List<Employee>();
        List<Employee> SelectedEmployeeList = new List<Employee>();
        List<Position> PositionList = new List<Position>();
        private MainWindow Main;
        BrushConverter brus = new BrushConverter();
        public PopUpChonNhanVien(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            this.com_id = main.IdAcount;
            LoadListDepartment();

            LoadSearchNameStaff();

        }
        public async void UpdateListUserSetting()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, API.vanthu_setting_api);
            request.Headers.Add("Authorization", "Bearer "+Properties.Settings.Default.Token);
            var content = new MultipartFormDataContent();
            string listUserString = "";
            foreach(var user in SelectedEmployeeList)
            {
                listUserString += user.ep_id + ",";
            }
            content.Add(new StringContent(listUserString), "list_user");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                MessageBox.Show("Thành công");
            }

        }
        public async void LoadListDepartment()
        {
            try
            {

                var httpClient = new HttpClient();
                var request = new HttpRequestMessage();
                request.Method = HttpMethod.Post;
                string api = API.list_department_api;

                request.RequestUri = new Uri(api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id != -1) content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);

                //load dropdown box

                cbDepartment.ItemsSource = result.data.items.Prepend(new Department() { dep_id = -1, dep_name = "Chọn phòng ban" });

            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi khi lấy dữ liệu phòng ban" + e.Message);
            }
        }

        public async void LoadSearchNameStaff()
        {
            LoadListPosition();
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_list_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id != -1) content.Add(new StringContent(com_id.ToString()), "com_id");
                if (dep_id != -1) content.Add(new StringContent(dep_id.ToString()), "dep_id");
                //mặc đinh công ty có dưới 100000 nhân viên
                content.Add(new StringContent("100000"), "pageSize");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    EmployeeRoot result = JsonConvert.DeserializeObject<EmployeeRoot>(responseContent);
                    EmployeeList = result.data.items;

                }
                var query = from e in EmployeeList
                            join p in PositionList on e.position_id equals p.positionId
                            select new Employee
                            {
                                _id = e._id,
                                ep_id = e.ep_id,
                                ep_email = e.ep_email,
                                ep_email_lh = e.ep_email_lh,
                                ep_phone_tk = e.ep_phone_tk,
                                ep_name = e.ep_name,
                                ep_education = e.ep_education,
                                ep_exp = e.ep_exp,
                                ep_phone = e.ep_phone,
                                ep_image = e.ep_image,
                                ep_address = e.ep_address,
                                ep_gender = e.ep_gender,
                                ep_married = e.ep_married,
                                ep_birth_day = e.ep_birth_day,
                                ep_description = e.ep_description,
                                create_time = e.create_time,
                                role_id = e.role_id,
                                group_id = e.group_id,
                                start_working_time = e.start_working_time,
                                position_id = e.position_id,
                                ep_status = e.ep_status,
                                allow_update_face = e.allow_update_face,
                                com_id = e.com_id,
                                dep_id = e.dep_id,
                                nameDeparment = e.nameDeparment,
                                gr_name = e.gr_name,
                                positionName = p.positionName,
                                ep_married_status = e.ep_married_status,
                                dep_name  = e.dep_name,
                            };

                EmployeeList = query.ToList();
                lsvListNameSaff.ItemsSource = EmployeeList.Prepend(new Employee() { ep_id=0,ep_name="Tất cả"});
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra khi lấy danh sách nhân viên");
            }

        }
        public async void LoadListPosition()
        {
            List<Position> listPosition = new List<Position>();
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_position_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    PositionRoot result = JsonConvert.DeserializeObject<PositionRoot>(responseContent);
                    PositionList = result.data.data;

                }

            }
            catch (Exception e)
            {
                MessageBox.Show("có lỗi khi lấy danh sách chức vụ ");

            }


        }



        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void lsvThuongPhat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void cbDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            dep_id = dep_id = int.Parse(cbDepartment.SelectedValue.ToString());
            LoadSearchNameStaff();
        }
        private void txtSearchNameSaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textName = txtSearchNameSaff.Text;
            lsvListNameSaff.ItemsSource = EmployeeList.Where(x => x.ep_name.ToUpper().Contains(textName.ToUpper())).Prepend(new Employee() { ep_id = -1, ep_name = "Tất cả" }); ;
        }
        private void bodSelectStaffName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListStaffNameCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListStaffNameCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListStaffNameCollapsed.Visibility -= Visibility.Collapsed;

            }
        }
        private void lsvStaffName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = (Employee)lsvListNameSaff.SelectedItem;
            if (selectedItem != null)
            {
                txbSelectStaffName.Text = selectedItem.ep_name;
                txbSelectStaffName.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
                if (selectedItem.ep_id == 0)
                {
                    SelectedEmployeeList = EmployeeList;
                    foreach(var item in SelectedEmployeeList)
                    {
                        item.isCheck = true;
                    }
                    lsvDSNhanVien.ItemsSource = null;
                    lsvDSNhanVien.ItemsSource = SelectedEmployeeList;
                }
                else
                {
                    if (SelectedEmployeeList.Where(x => x.ep_id == selectedItem.ep_id).FirstOrDefault() != null)
                    {
                        selectedItem.isCheck = true;
                        SelectedEmployeeList.Add(selectedItem);
                        lsvDSNhanVien.ItemsSource = null;
                        lsvDSNhanVien.ItemsSource = SelectedEmployeeList;
                    }

                }



            }
        }
        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = ((Rectangle)sender).Parent as Grid;
            var bodPopUp = grid.Parent as Border;
            bodPopUp.Visibility = Visibility.Collapsed;


        }
        private void Employee_CheckAll(object sender, RoutedEventArgs e)
        {


            //check all item in listview
            for (int i = 0; i < SelectedEmployeeList.Count; i++)
            {
                SelectedEmployeeList[i].isCheck = true;

            }
            lsvDSNhanVien.ItemsSource = null;
            lsvDSNhanVien.ItemsSource = SelectedEmployeeList;
        }

        private void Employee_UnCheckAll(object sender, RoutedEventArgs e)
        {

            //Uncheck all item in listview
            for (int i = 0; i < SelectedEmployeeList.Count; i++)
            {
                SelectedEmployeeList[i].isCheck = false;

            }
            lsvDSNhanVien.ItemsSource = null;
            lsvDSNhanVien.ItemsSource = SelectedEmployeeList;
        }

        private void OK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateListUserSetting();
        }
    }
}
