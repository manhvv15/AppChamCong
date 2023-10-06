using ChamCong365.APIs;

using ChamCong365.funcQuanLyCongTy;
using ChamCong365.funcQuanLyCongTy.AddNewStaffTabList;
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

namespace ChamCong365.Popup.funcCompanyManager.AddNewStaffPopups
{
    /// <summary>
    /// Interaction logic for ucAddNewStaff.xaml
    /// </summary>
    ///

    public partial class ucAddNewStaff : UserControl
    {
        MainWindow Main;
        ucInstallAddNewStaff ucInstallAddNewStaff;
        BrushConverter brus = new BrushConverter();

        int com_id = -1;
        int dep_id = -1;
        int team_id = -1;
        int gr_id = -1;
        int position_id = -1;
        public ucAddNewStaff(MainWindow main, ucInstallAddNewStaff ucInstallAddNewStaff)
        {
            InitializeComponent();
            this.Main = main;
            com_id = Main.IdAcount;
            this.ucInstallAddNewStaff = ucInstallAddNewStaff;

            LoadCompanySelectBox();
            SetUpCombobox();
            LoadListPosition();

        }

        public async void CreateEmployee()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_create_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
                content.Add(new StringContent(dep_id.ToString()), "dep_id");
                content.Add(new StringContent(team_id.ToString()), "team_id");
                content.Add(new StringContent(gr_id.ToString()), "group_id");
                content.Add(new StringContent(cbRole.SelectedValue.ToString()), "role");
                content.Add(new StringContent(txtHoTen.Text), "userName");
                content.Add(new StringContent(Password.Password), "password");
                content.Add(new StringContent(txtAdress.Text), "address");
                content.Add(new StringContent(position_id.ToString()), "position_id");
                content.Add(new StringContent(cbxGender.SelectedValue.ToString()), "gender");
                content.Add(new StringContent(txtPhoneTK.Text), "phoneTK");
                content.Add(new StringContent(txtSDT.Text), "phone");
                content.Add(new StringContent(cbxEducation.SelectedValue.ToString()), "education");
                content.Add(new StringContent(birthDay.Text), "birthday");
                content.Add(new StringContent(timeStartWork.Text), "start_working_time");
                content.Add(new StringContent(cbxExp.SelectedValue.ToString()), "experience");
                content.Add(new StringContent(cbxExp.SelectedValue.ToString()), "married");

                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    ucListAllStaff uc = new ucListAllStaff(this.Main);
                    ucInstallAddNewStaff.stackTabBody.Children.Clear();
                    object Content = uc.Content;
                    uc.Content = null;
                    ucInstallAddNewStaff.stackTabBody.Children.Add(Content as UIElement);
                }
            }
            catch
            {
                MessageBox.Show("Có lỗi khi thêm nhân viên");

            }
        }

        private void SetUpCombobox()
        {
            cbxGender.ItemsSource = ListItemCombobox.ListCbxGender;
            cbxEducation.ItemsSource = ListItemCombobox.ListCbxEducation;
            cbxExp.ItemsSource = ListItemCombobox.ListCbxExp;
            cbxMarried.ItemsSource = ListItemCombobox.ListMarried;
            cbRole.ItemsSource = ListItemCombobox.listCbxPermission;
        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;

        }

        private void ExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
        #region LoadSelectBox
        public void LoadCompanySelectBox()
        {
            Company ParentCompany = new Company() { com_id = Main.IdAcount, com_name = Main.txbNameAccount.Text };
            //GetListChildCompany();
            List<Company> listDataDropBox = new List<Company>();
            listDataDropBox.Add(ParentCompany);
            lsvCompany.ItemsSource = listDataDropBox;
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
                if (com_id != -1) content.Add(new StringContent(com_id.ToString()), "com_id");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);

                //load dropdown box

                lsvDepartment.ItemsSource = result.data.items;

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi khi load danh sách phòng ban" + e.Message);
            }
        }
        public async void LoadListTeam()
        {
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.team_list_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id != -1) content.Add(new StringContent(com_id.ToString()), "com_id");
                if (dep_id != -1) content.Add(new StringContent(dep_id.ToString()), "dep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TeamRoot result = JsonConvert.DeserializeObject<TeamRoot>(responseContent);
                    lsvTo.ItemsSource = result.data.data;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi sảy ra khi load danh sách tổ " + ex.Message);
            }


        }
        public async void LoadListGroup()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, API.group_listAll_api);
            request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
            var content = new MultipartFormDataContent();
            if (com_id != -1) content.Add(new StringContent(com_id.ToString()), "com_id");
            if (dep_id != -1) content.Add(new StringContent(dep_id.ToString()), "dep_id");
            if (team_id != -1) content.Add(new StringContent(team_id.ToString()), "team_id");


            request.Content = content;
            var response = await client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                GroupRoot result = JsonConvert.DeserializeObject<GroupRoot>(responseContent);
                if (result.data.data != null)
                {
                    lsvGroup.ItemsSource = result.data.data;
                }
            }
        }
        public async void LoadListPosition()
        {
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
                    lsvPosition.ItemsSource = result.data.data;
                }

            }
            catch (Exception e)
            {
                MessageBox.Show("có lỗi khi lấy danh sách chức vụ ");

            }

        }

        #endregion

        #region PopupOpen
        private void bodSelectCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCompanyCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListCompanyCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCompanyCollapsed.Visibility -= Visibility.Collapsed;

            }
        }


        private void lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Company)lsvCompany.SelectedItem;
            if (selectedItem != null)
            {
                com_id = selectedItem.com_id;
                txbSelectCompany.Text = selectedItem.com_name;
                txbSelectCompany.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListCompanyCollapsed.Visibility = Visibility.Collapsed;
                LoadListDepartment();
            }
        }

        private void bodSelectDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListDepartmentCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListDepartmentCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListDepartmentCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Department)lsvDepartment.SelectedItem;
            if (selectedItem != null)
            {
                dep_id = selectedItem.dep_id;
                txbSelectDepartment.Text = selectedItem.dep_name;
                txbSelectDepartment.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
                LoadListTeam();
            }

        }

        //dropdown box select to
        private void bodSelectTo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListToCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListToCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListToCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = (Team)lsvTo.SelectedItem;
            if (selectedItem != null)
            {
                team_id = selectedItem.team_id;
                txbSelectTo.Text = selectedItem.team_name;
                txbSelectTo.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListToCollapsed.Visibility = Visibility.Collapsed;
                LoadListGroup();
            }
        }
        //dropbox select group
        private void bodSelectGroup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListGroupCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListGroupCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListGroupCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Group)lsvGroup.SelectedItem;
            if (selectedItem != null)
            {
                gr_id = selectedItem.gr_id;
                txbSelectGroup.Text = selectedItem.gr_name;
                txbSelectGroup.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListGroupCollapsed.Visibility = Visibility.Collapsed;
            }

        }
        private void bodSelectPosition_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListPositionCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListPositionCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListPositionCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Position)lsvPosition.SelectedItem;
            if (selectedItem != null)
            {
                position_id = selectedItem.positionId;
                txbSelectPosition.Text = selectedItem.positionName;
                txbSelectPosition.Foreground = (Brush)brus.ConvertFromString("#474747");
            }
            bodListPositionCollapsed.Visibility = Visibility.Collapsed;
        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = (((Rectangle)sender).Parent) as Grid;
            var bodSelectCollasped = grid.Parent as Border;
            bodSelectCollasped.Visibility = Visibility.Collapsed;
        }

        private void birthDay_CalendarClosed(object sender, RoutedEventArgs e)
        {
            birthDay.Foreground = (Brush)brus.ConvertFromString("#474747");
        }
        private void timeStartWork_CalendarClosed(object sender, RoutedEventArgs e)
        {
            timeStartWork.Foreground = (Brush)brus.ConvertFromString("#474747");
        }


        #endregion

        private void OK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            CreateEmployee();
        }
    }
}
