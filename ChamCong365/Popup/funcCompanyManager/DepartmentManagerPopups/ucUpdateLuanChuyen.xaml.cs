using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.Popup.PopupTimeKeeping;
using ChamCong365.TimeKeeping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace ChamCong365.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucCreateLuanChuyen.xaml
    /// </summary>
    public partial class ucUpdateLuanChuyen : UserControl
    {
        MainWindow Main;
        ucListLuanChuyen ucListLuanChuyen;
        public Company parentCompany = new Company();
        public List<Company> ChildCompanyList = new List<Company>();
        public List<Department> DepartmentList = new List<Department>();
        public List<Employee> AllEmployeeList = new List<Employee>();
        public List<Team> TeamList = new List<Team>();
        public List<Group> GroupList = new List<Group>();
        public List<Position> PositionList = new List<Position>();
        public List<Provision> ProvisionList = new List<Provision>();
        BrushConverter bc = new BrushConverter();

        public int old_com_id = -1;
        public int old_dep_id = -1;
        public int old_position_id = -1;
        public int ep_id = -1;
        public int new_com_id = -1;
        public int new_dep_id = -1;
        public int new_position_id = -1;
        public int team_id = -1;
        public int gr_id = -1;
        public int decision_id = -1;
        public string team_name = "";
        public string group_name = "";
        public string created_at = "";

        public ucUpdateLuanChuyen(MainWindow main, ucListLuanChuyen ucListLuanChuyen, TranferJob tranfer)
        {
            InitializeComponent();
            Main = main;
            this.ucListLuanChuyen = ucListLuanChuyen;
            LoadInforTextBox(tranfer);
            this.old_com_id = tranfer.old_com_id;
            old_dep_id = tranfer.old_dep_id;
            old_position_id = tranfer.id_old_position;
            ep_id = tranfer.ep_id;
            new_com_id = tranfer.new_com_id;
            new_dep_id = tranfer.new_dep_id;
            new_position_id = tranfer.id_new_position;
            decision_id = (int)tranfer?.decision_id;
            created_at = tranfer.created_at.ToLocalTime().ToString();

            parentCompany = new Company() { com_id = Main.IdAcount, com_name = Main.txbNameAccount.Text };


            LoadListChildCompany();
            LoadListNewPosition();
            LoadListProvision();

        }
        public void LoadInforTextBox(TranferJob tranferJob)
        {
            txbSelectOldCompany.Text = tranferJob.old_com_name;
            txbSelectCurrentDepartment.Text = tranferJob.old_dep_name;
            txbSelectCurrentPosition.Text = tranferJob.old_position;
            txbSelectStaffName.Text = tranferJob.userName;

        }
        public async void UpdateTranferJob()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "http://210.245.108.202:3006/api/hr/personalChange/updateTranferJob");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ep_id.ToString()), "ep_id");
                content.Add(new StringContent(old_position_id.ToString()), "position_id");
                content.Add(new StringContent(old_dep_id.ToString()), "dep_id");
                content.Add(new StringContent(created_at.ToString()), "created_at");
                string note = new TextRange(txtNote.Document.ContentStart, txtNote.Document.ContentEnd).Text;
                content.Add(new StringContent(note), "note");
                content.Add(new StringContent(new_position_id.ToString()), "update_position");
                content.Add(new StringContent(new_dep_id.ToString()), "update_dep_id");
                content.Add(new StringContent(old_com_id.ToString()), "com_id");
                string mission = new TextRange(txtMission.Document.ContentStart, txtMission.Document.ContentEnd).Text;

                content.Add(new StringContent(mission), "mission");
                content.Add(new StringContent(decision_id.ToString()), "decision_id");
                content.Add(new StringContent(new_com_id.ToString()), "new_com_id");
                content.Add(new StringContent(team_id.ToString()), "new_team_id");
                content.Add(new StringContent(gr_id.ToString()), "new_group_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    ucListLuanChuyen.LoadListLuanChuyen();
                    this.Visibility = Visibility.Collapsed;
                }

            }
            catch
            {
                System.Windows.MessageBox.Show("Có lỗi khi tạo mới luân chuyển");
            }

        }
        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }


        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = (((Rectangle)sender).Parent) as Grid;
            var bodSelectCollasped = grid.Parent as Border;
            bodSelectCollasped.Visibility = Visibility.Collapsed;
        }
        #region SelectOldCompany    
        private void bodSelectOldCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListOldCompanyCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListOldCompanyCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListOldCompanyCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvOldCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Company)lsvOldCompany.SelectedItem;
            if (selectedItem != null)
            {
                old_com_id = selectedItem.com_id;
                txbSelectOldCompany.Text = selectedItem.com_name;
                txbSelectOldCompany.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListOldCompanyCollapsed.Visibility = Visibility.Collapsed;
                LoadListOldDepartment();
            }

        }
        #endregion
        #region SelectCurrentDepartment    
        private void bodSelectCurrentDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCurrentDepartmentCollapsed.Visibility == Visibility.Collapsed)
            {


                bodListCurrentDepartmentCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCurrentDepartmentCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvCurrentDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Department)lsvCurrentDepartment.SelectedItem;
            if (selectedItem != null)
            {
                old_dep_id = selectedItem.dep_id;
                txbSelectCurrentDepartment.Text = selectedItem.dep_name;
                txbSelectCurrentDepartment.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListCurrentDepartmentCollapsed.Visibility = Visibility.Collapsed;
                LoadSearchNameStaff();
            }

        }
        #endregion
        #region SelectStaffName    
        private void bodSelectStaffName_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListStaffNameCollapsed.Visibility == Visibility.Collapsed)
            {


                bodListStaffNameCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvStaffName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Employee)lsvStaffName.SelectedItem;
            if (selectedItem != null)
            {
                ep_id = selectedItem.ep_id;
                old_position_id = selectedItem.position_id;
                txbSelectStaffName.Text = selectedItem.ep_name;
                txbSelectStaffName.Foreground = (Brush)bc.ConvertFromString("#474747");
                LoadListOldPosition();
            }
            bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectCurrentPosition    
        private void bodSelectCurrentPosition_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCurrentPositionCollapsed.Visibility == Visibility.Collapsed)
            {
                LoadListProvision();
                bodListCurrentPositionCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCurrentPositionCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvCurrentPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Position)lsvCurrentPosition.SelectedItem;
            if (selectedItem != null)
            {
                old_position_id = selectedItem.positionId;
                txbSelectCurrentPosition.Text = selectedItem.positionName;
                txbSelectCurrentPosition.Foreground = (Brush)bc.ConvertFromString("#474747");
            }
            bodListCurrentPositionCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectNewCompany    
        private void bodSelectNewCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListNewCompanyCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListNewCompanyCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListNewCompanyCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvNewCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Company)lsvNewCompany.SelectedItem;
            if (selectedItem != null)
            {
                new_com_id = selectedItem.com_id;
                txbSelectNewCompany.Text = selectedItem.com_name;
                txbSelectNewCompany.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListNewCompanyCollapsed.Visibility = Visibility.Collapsed;
                LoadListNewDepartment();
            }



            bodListNewCompanyCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectNewDepartment    
        private void bodSelectNewDepartment_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListNewDepartmentCollapsed.Visibility == Visibility.Collapsed)
            {


                bodListNewDepartmentCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListNewDepartmentCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvNewDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Department)lsvNewDepartment.SelectedItem;
            if (selectedItem != null)
            {
                new_dep_id = selectedItem.dep_id;
                txbSelectNewDepartment.Text = selectedItem.dep_name;
                txbSelectNewDepartment.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListNewDepartmentCollapsed.Visibility = Visibility.Collapsed;
                LoadListTeam();
            }
        }
        #endregion
        #region SelectNest    
        private void bodSelectNest_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListNestCollapsed.Visibility == Visibility.Collapsed)
            {


                bodListNestCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListNestCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvNest_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Team)lsvNest.SelectedItem;
            if (selectedItem != null)
            {
                team_id = selectedItem.team_id;
                txbSelectNest.Text = selectedItem.team_name;
                txbSelectNest.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListNestCollapsed.Visibility = Visibility.Collapsed;
                LoadListGroup();
            }
        }
        #endregion
        #region SelectGroup    
        private void bodSelectGroup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListGroupCollapsed.Visibility == Visibility.Collapsed)
            {


                bodListGroupCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListGroupCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvGroup_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Group)lsvGroup.SelectedItem;
            if (selectedItem != null)
            {
                gr_id = selectedItem.gr_id;
                txbSelectGroup.Text = selectedItem.gr_name;
                txbSelectGroup.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListGroupCollapsed.Visibility = Visibility.Collapsed;

            }
        }
        #endregion
        #region SelectNewPosition    
        private void bodSelectNewPosition_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListNewPositionCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListNewPositionCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListNewPositionCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvNewPosition_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Position)lsvNewPosition.SelectedItem;
            if (selectedItem != null)
            {
                new_position_id = selectedItem.positionId;
                txbSelectNewPosition.Text = selectedItem.positionName;
                txbSelectNewPosition.Foreground = (Brush)bc.ConvertFromString("#474747");
            }
            bodListNewPositionCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region SelectRule    
        private void bodSelectRule_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListRuleCollapsed.Visibility == Visibility.Collapsed)
            {
                LoadListProvision();

                bodListRuleCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListRuleCollapsed.Visibility = Visibility.Collapsed;

            }
        }

        private void lsvRule_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Provision)lsvRule.SelectedItem;
            if (selectedItem != null)
            {
                decision_id = selectedItem.id;
                txbSelectRule.Text = selectedItem.name;
                txbSelectRule.Foreground = (Brush)bc.ConvertFromString("#474747");
            }
            bodListRuleCollapsed.Visibility = Visibility.Collapsed;
        }
        #endregion
        #region LoadSelectBox
        private async void LoadListChildCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_ChildCompany_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                CompanyRoot result = JsonConvert.DeserializeObject<CompanyRoot>(responseContent);
                ChildCompanyList = result.data.items;
                ChildCompanyList.Add(parentCompany);
                ChildCompanyList.Reverse();
                lsvOldCompany.ItemsSource = ChildCompanyList;
                lsvNewCompany.ItemsSource = ChildCompanyList;

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }

        public async void LoadListOldDepartment()
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
                content.Add(new StringContent(old_com_id.ToString()), "com_id");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);

                //load dropdown box
                DepartmentList = result.data.items;
                lsvCurrentDepartment.ItemsSource = DepartmentList;
                lsvNewDepartment.ItemsSource = DepartmentList;

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }

        public async void LoadSearchNameStaff()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_list_api);
            request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
            var content = new MultipartFormDataContent();
            content.Add(new StringContent(old_com_id.ToString()), "com_id");
            content.Add(new StringContent(old_dep_id.ToString()), "dep_id");
            //mặc đinh công ty có dưới 100000 nhân viên
            content.Add(new StringContent("100000"), "pageSize");
            request.Content = content;
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                EmployeeRoot result = JsonConvert.DeserializeObject<EmployeeRoot>(responseContent);
                AllEmployeeList = result.data.items;
                lsvStaffName.ItemsSource = AllEmployeeList;
            }
        }
        public async void LoadListNewDepartment()
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
                content.Add(new StringContent(new_com_id.ToString()), "com_id");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);

                //load dropdown box
                DepartmentList = result.data.items;
                lsvCurrentDepartment.ItemsSource = DepartmentList;
                lsvNewDepartment.ItemsSource = DepartmentList;

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }

        public async void LoadListOldPosition()
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
                    PositionList = result.data.data;
                    lsvCurrentPosition.ItemsSource = PositionList;
                }
                lsvCurrentPosition.ItemsSource = PositionList.Where(x => x.positionId == old_position_id);
            }
            catch (Exception e)
            {
                MessageBox.Show("có lỗi khi lấy danh sách chức vụ ");

            }

        }

        public async void LoadListNewPosition()
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
                    PositionList = result.data.data;
                    lsvCurrentPosition.ItemsSource = PositionList;
                }
                lsvNewPosition.ItemsSource = PositionList;
            }
            catch (Exception e)
            {
                MessageBox.Show("có lỗi khi lấy danh sách chức vụ ");

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
                content.Add(new StringContent(this.new_com_id.ToString()), "com_id");
                content.Add(new StringContent(this.new_dep_id.ToString()), "dep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    TeamRoot result = JsonConvert.DeserializeObject<TeamRoot>(responseContent);
                    TeamList = result.data.data;
                    lsvNest.ItemsSource = TeamList;
                }
                Console.WriteLine(await response.Content.ReadAsStringAsync());
            }
            catch (Exception ex)
            {
                MessageBox.Show("có lỗi khi lấy danh sách tổ: " + ex.Message);
            }
        }
        public async void LoadListGroup()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.search_group_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(this.new_com_id.ToString()), "com_id");
                content.Add(new StringContent(this.new_dep_id.ToString()), "dep_id");
                content.Add(new StringContent(this.team_id.ToString()), "team_id");
                content.Add(new StringContent("1"), "page");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    GroupRoot result = JsonConvert.DeserializeObject<GroupRoot>(responseContent);
                    GroupList = result.data.data;
                    lsvGroup.ItemsSource = GroupList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lấy danh sách nhóm");
            }
        }

        public async void LoadListProvision()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Get, API.provision_list_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    var responseContent = await response.Content.ReadAsStringAsync();
                    ProvisionRoot result = JsonConvert.DeserializeObject<ProvisionRoot>(responseContent);
                    ProvisionList = result.success.data.data;
                    lsvRule.ItemsSource = ProvisionList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lấy danh sách nhóm");
            }
        }
        #endregion

        private void timeTranferJob_CalendarClosed(object sender, RoutedEventArgs e)
        {
            created_at = timeTranferJob.Text;
        }

        private void OK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateTranferJob();
        }
    }
}
