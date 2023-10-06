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
    public partial class ucUpdateNghiViec : UserControl
    {
        MainWindow Main;
        ucListNghiViec ucListNghiViec;
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
        public int? old_dep_id = -1;
        public int old_position_id = -1;
        public int ep_id = -1;
        public string created_at = "";

        public ucUpdateNghiViec(MainWindow main, ucListNghiViec ucListNghiViec, QuitJobNew.QuitJob quitJob)
        {
            InitializeComponent();
            Main = main;
            this.ucListNghiViec = ucListNghiViec;
            ep_id = quitJob.ep_id;
            parentCompany = new Company() { com_id = Main.IdAcount, com_name = Main.txbNameAccount.Text };
            this.old_com_id = Main.IdAcount;
        
            LoadSearchNameStaff();
            LoadTextBox(quitJob);
            //LoadListChildCompany();

        }

        public async void UpdateQuitJob()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.create_QuitJobNew_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(ep_id.ToString()), "ep_id");
                content.Add(new StringContent(old_position_id.ToString()), "current_position");
                content.Add(new StringContent(old_dep_id.ToString()), "dep_id");
                content.Add(new StringContent(created_at.ToString()), "created_at");
                string note = new TextRange(txtNote.Document.ContentStart, txtNote.Document.ContentEnd).Text;
                content.Add(new StringContent(note), "note");

                content.Add(new StringContent(old_com_id.ToString()), "com_id");

                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    ucListNghiViec.LoadListNghiViec();
                    this.Visibility = Visibility.Collapsed;
                }

            }
            catch
            {
                System.Windows.MessageBox.Show("Có lỗi khi tạo mới luân chuyển");
            }

        }
        private void LoadTextBox(QuitJobNew.QuitJob quitJob)
        {
            txbSelectStaffName.Text = quitJob.ep_name;
            txbSelectStaffName.Foreground = (Brush)bc.ConvertFromString("#474747");

            txbSelectOldCompany.Text = Main.txbNameAccount.Text;
            txbSelectOldCompany.Foreground = (Brush)bc.ConvertFromString("#474747");

            txbSelectCurrentDepartment.Text = quitJob.dep_name;
            txbSelectCurrentDepartment.Foreground = (Brush)bc.ConvertFromString("#474747");

            txbSelectCurrentPosition.Text = quitJob.position_name;
            txbSelectCurrentPosition.Foreground = (Brush)bc.ConvertFromString("#474747");


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
                ;
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
                old_com_id = selectedItem.com_id;
                if (selectedItem.dep_id != null) old_dep_id = selectedItem.dep_id;
                old_position_id = selectedItem.position_id;

            }
            bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
        }

        #endregion
        #region SelectCurrentPosition    
        private void bodSelectCurrentPosition_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCurrentPositionCollapsed.Visibility == Visibility.Collapsed)
            {

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


            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }

        public async void LoadSearchNameStaff()
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_all);
            request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
            var response = await client.SendAsync(request);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                EmployeeRoot result = JsonConvert.DeserializeObject<EmployeeRoot>(responseContent);
                AllEmployeeList = result.data.items;
                lsvStaffName.ItemsSource = AllEmployeeList;
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

                }
                txbSelectCurrentPosition.Text = PositionList.Where(x => x.positionId == old_position_id).FirstOrDefault().positionName;
                txbSelectCurrentPosition.Foreground = (Brush)bc.ConvertFromString("#474747");
            }
            catch (Exception e)
            {
                MessageBox.Show("có lỗi khi lấy danh sách chức vụ ");

            }

        }
        #endregion
        private void timeTranferJob_CalendarClosed(object sender, RoutedEventArgs e)
        {
            created_at = timeTranferJob.Text;
        }

        private void OK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateQuitJob();
        }

        private void SearchStaffName_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchText = txtSelectStaffName.Text;
            lsvStaffName.ItemsSource = AllEmployeeList.Where(x => x.ep_name.ToUpper().Contains(searchText.ToUpper()));
        }
    }
}
