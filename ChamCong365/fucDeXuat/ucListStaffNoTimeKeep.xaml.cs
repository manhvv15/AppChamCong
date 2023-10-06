using ChamCong365.APIs;
using ChamCong365.funcQuanLyCongTy.AddNewStaffTabList;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.Popup.funcCompanyManager;
using ChamCong365.Popup.funcCompanyManager.AddNewStaffPopups;
using Newtonsoft.Json;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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

namespace ChamCong365.fucDeXuat
{
    /// <summary>
    /// Interaction logic for ucListStaff.xaml
    /// </summary>
    public partial class ucListStaffNoTimeKeep : UserControl
    {

        BrushConverter bc = new BrushConverter();
        List<OrgStructureEmployee.Employee> EmployeeList = new List<OrgStructureEmployee.Employee>();
        List<Company> ChildCompanyList = new List<Company>();
        List<Department> DepartmentList = new List<Department>();
        List<Team> TeamList = new List<Team>();
        List<Group> GroupList = new List<Group>();
        List<Position> PositionList = new List<Position>();

        public int com_id = -1;
        int type_timekeep = -1;
        int dep_id = -1;
        int team_id= -1; 
        int gr_id = -1; 
        int positionId = -1;  
        int married=-1;
        int yearOfBirth= -1;
        int TotalItem = 0;
        double TongSoTrang = 0;
        public int pageNumber = 1;
        int pageSize = 10;


        public Border OptionPopup { get; set; } = new Border();
        public ucListStaffNoTimeKeep(int com_id = -1, int dep_id = -1, int team_id = -1, int gr_id = -1)
        {


            InitializeComponent();

            this.com_id = com_id;
            this.dep_id = dep_id;
            this.team_id = team_id;
            this.gr_id = gr_id;
            CheckFrom();
            LoadDropBox();
            LoadListEmployee();


        }
        private void CheckFrom()
        {
            bool isFromDep = (dep_id != -1);
            bool isFromTeam = (team_id != -1);
            bool isFromGroup = (gr_id != -1);
            if (isFromDep)
            {
                ImgSelectPhongBan.Visibility = Visibility.Collapsed;
                bodSelectPhongBan.MouseUp -= bodSelectPhongBan_MouseUp;
            }
            else if (isFromTeam)
            {
                ImgSelectPhongBan.Visibility = Visibility.Collapsed;
                bodSelectPhongBan.MouseUp -= bodSelectPhongBan_MouseUp;

                ImgSelectTo.Visibility = Visibility.Collapsed;
                bodSelectTo.MouseUp -= bodSelecTo_MouseUp;
            }
            else if (isFromGroup)
            {
                ImgSelectPhongBan.Visibility = Visibility.Collapsed;
                bodSelectPhongBan.MouseUp -= bodSelectPhongBan_MouseUp;

                ImgSelectTo.Visibility = Visibility.Collapsed;
                bodSelectTo.MouseUp -= bodSelecTo_MouseUp;

                ImgSelectNhom.Visibility = Visibility.Collapsed;
                bodSelectNhom.MouseUp -= bodSelecNhom_MouseUp;

            }

        }

        public void LoadDropBox()
        {
            LoadListDepartment();
            GetListTeam();
            GetListGroup();
            lsvHonNhan.ItemsSource = ListItemCombobox.ListMarried.Prepend( new Item() { ID="-1",value="Chọn hôn nhân"});
            List<Item> ListYear = new List<Item>();
            for (int i = 1970; i <= DateTime.Now.Year; i++)
            {
                ListYear.Add(new Item() { ID=i.ToString(),value=i.ToString() });
            }

            lsvNgaySinh.ItemsSource = ListYear.Prepend(new Item() { ID="-1",value="Chọn năm sinh" });
        }

        public async void LoadListEmployee()
        {
            //load employee

            PositionList = await GetListPosition();
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.organizationalStructure_listEp);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                //if (com_id != 0) content.Add(new StringContent(this.com_id.ToString()), "com_id");
                if (dep_id != -1) content.Add(new StringContent(dep_id.ToString()), "dep_id");
                if (team_id != -1) content.Add(new StringContent(team_id.ToString()), "team_id");
                if (gr_id != -1) content.Add(new StringContent(gr_id.ToString()), "group_id");
                if (positionId != -1) content.Add(new StringContent(positionId.ToString()), "position_id");
                if (yearOfBirth != -1) content.Add(new StringContent(yearOfBirth.ToString()), "birthday");
                if (married != -1) content.Add(new StringContent(married.ToString()), "married");
                content.Add(new StringContent("2"), "type_timekeep");
                content.Add(new StringContent(this.pageNumber.ToString()), "page");
                content.Add(new StringContent(this.pageSize.ToString()), "pageSize");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    OrgStructureEmployee.Root result = JsonConvert.DeserializeObject<OrgStructureEmployee.Root>(responseContent);
                    if (result.data != null)
                    {
                        TotalItem = result.data.totalCount;
                        EmployeeList = result.data.listEmployee;
                    }

                    TongSoTrang = (int)Math.Ceiling((double)TotalItem / pageSize);

                    if (pageNumber == 1) LoadTableDataPagging();
                    //lsvAllStaff.ItemsSource = EmployeeList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lấy danh sách nhân viên" + ex.Message);
            }

            int STT = 1;
            //get position name
            var query = from e in EmployeeList

                        select new OrgStructureEmployee.Employee
                        {
                            STT = STT++,
                            _id = e._id,
                            idQLC = e.idQLC,
                            userName = e.userName,
                            phone = e.phone,
                            phoneTK = (e.phoneTK == "") ? "Chưa cập nhật" : e.phoneTK,
                            email = e.email,
                            address = e.address,
                            birthday = e.birthday,
                            gender = e.gender,
                            married = e.married,
                            experience = e.experience,
                            education = e.education,
                            com_id = e.com_id,
                            Company = e.Company,
                            dep_id = e.dep_id,
                            Department = e?.Department ?? "Chưa cập nhật",
                            group_id = e.group_id,
                            team_id = e.team_id,
                            position_id = e.position_id,
                            start_working_time = e.start_working_time,
                            positionName = (PositionList.Where(x => x.positionId == e.position_id).FirstOrDefault())?.positionName ?? "Chưa cập nhật",
                            Team = e?.Team ?? "Chưa cập nhật",
                            Group = e?.Group?? "Chưa cập nhật",
                            ep_birthday = (e.birthday==null)?"Chưa cập nhật": ConvertToVST( (double)e.birthday),
                            ep_start_working_time= (e.start_working_time == null)?"Chưa cập nhật": ConvertToVST((double)e.start_working_time), 
                            ly_do_nghi = (e.ly_do_nghi=="")?"Chưa cập nhật":e.ly_do_nghi
                        };
            STT = 1;
            EmployeeList = query.ToList();
            lsvAllStaff.ItemsSource = EmployeeList;
            //lsvListNameSaff.ItemsSource = query.ToList();
        }

        public void LoadTableDataPagging()
        {
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borPage1.Visibility = Visibility.Collapsed;
            borPage2.Visibility = Visibility.Collapsed;
            borPage3.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            borPageCuoi.Visibility = Visibility.Collapsed;

            if (TongSoTrang == 1)
            {
                borPage1.Visibility = Visibility.Visible;
            }
            if (TongSoTrang == 2)
            {
                borPage1.Visibility = Visibility.Visible;
                borPage2.Visibility = Visibility.Visible;
            }
            if (TongSoTrang == 3)
            {
                borPage1.Visibility = Visibility.Visible;
                borPage2.Visibility = Visibility.Visible;
                borPage3.Visibility = Visibility.Visible;
            }
            if (TongSoTrang > 3)
            {
                borPage1.Visibility = Visibility.Visible;
                borPage2.Visibility = Visibility.Visible;
                borPage3.Visibility = Visibility.Visible;
                borLen1.Visibility = Visibility.Visible;
                borPageCuoi.Visibility = Visibility.Visible;
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
                content.Add(new StringContent(com_id.ToString()), "com_id");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);

                //load dropdown box
                DepartmentList = result.data.items;
                lsvPhongban.ItemsSource = DepartmentList.Prepend(new Department() { dep_id = -1, dep_name = "Chọn phòng ban" }); ;

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi khi load danh sách phòng ban" + e.Message);
            }
        }
        public async void GetListTeam()
        {
            List<Team> list = new List<Team>();
            try
            {

                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.team_list_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
                if(dep_id!=-1)content.Add(new StringContent(dep_id.ToString()), "dep_id");

                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TeamRoot result = JsonConvert.DeserializeObject<TeamRoot>(responseContent);
                    list = result.data.data;
                    lsvTo.ItemsSource = list.Prepend(new Team() { team_id = -1, team_name = "Chọn tổ" });
                }
        

            }
            catch
            {

            }
     
        }
        public async void GetListGroup()
        {
            List<Group> list = new List<Group>(); 
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.group_listAll_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id.ToString()), "com_id");
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

                        list = result.data.data;
                        lsvNhom.ItemsSource = list.Prepend(new Group() { gr_id = -1, gr_name = "Chọn nhóm" }); ;
                    }

                }

  
            }
            catch
            {

            }
       
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
                        lsvChucVu.ItemsSource = list.Prepend(new Position() { positionId = -1, positionName = "Chọn chức vụ" });
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
        string ConvertToVST(double unixTimestamp)
        {
            // Define the Vietnam Standard Time (VST) offset in seconds
            int vstOffsetSeconds = 25200; // UTC+7

            // Create a DateTime representing the Unix epoch (January 1, 1970, 00:00:00 UTC)
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            // Add the Unix timestamp and VST offset to the epoch
            DateTime vstDateTime = epoch.AddSeconds(unixTimestamp + vstOffsetSeconds);

            return vstDateTime.ToShortDateString();
        }


        //xử lý popup sửa,phân quyền, xóa trong listview




        #region phantrang
        private void borPageDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPageDau.Visibility = Visibility.Collapsed;
            borLui1.Visibility = Visibility.Collapsed;
            borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
            textPage1.Text = "1";
            textPage2.Text = "2";
            textPage3.Text = "3";
            borLen1.Visibility = Visibility.Visible;
            borPageCuoi.Visibility = Visibility.Visible;

            pageNumber = 1;
            LoadListEmployee();
        }

        private void borLui1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage3.Text == TongSoTrang.ToString() && borPageCuoi.Visibility == Visibility.Collapsed)
                {
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;

                }
                else
                {
                    if (textPage1.Text == "1")
                    {
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;

                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();

                    }


                }
            }
            pageNumber--;
            LoadListEmployee();

        }

        private void borPage1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (int.Parse(textPage1.Text) >= 1)
            {
                if (textPage1.Text == (TongSoTrang - 2).ToString() && borPageCuoi.Visibility == Visibility.Collapsed && TongSoTrang > 3)
                {

                    textPage1.Text = (TongSoTrang - 3).ToString();
                    textPage2.Text = (TongSoTrang - 2).ToString();
                    textPage3.Text = (TongSoTrang - 1).ToString();


                    BrushConverter brus = new BrushConverter();

                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                    if (TongSoTrang > 2)
                    {
                        borLen1.Visibility = Visibility.Visible;
                        borPageCuoi.Visibility = Visibility.Visible;
                    }



                }
                else
                {

                    if (textPage1.Text == "1")
                    {
                        BrushConverter brus = new BrushConverter();
                        borPageDau.Visibility = Visibility.Collapsed;
                        borLui1.Visibility = Visibility.Collapsed;
                        borPage1.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        if (TongSoTrang > 3)
                        {
                            borLen1.Visibility = Visibility.Visible;
                            borPageCuoi.Visibility = Visibility.Visible;
                        }


                    }
                    else
                    {
                        textPage1.Text = (int.Parse(textPage1.Text) - 1).ToString();
                        textPage2.Text = (int.Parse(textPage2.Text) - 1).ToString();
                        textPage3.Text = (int.Parse(textPage3.Text) - 1).ToString();

                    }
                }
            }
            if (int.Parse(textPage1.Text) > 1)
            {
                pageNumber = int.Parse(textPage2.Text);
                LoadListEmployee();
            }
            if (int.Parse(textPage1.Text) == 1)
            {
                pageNumber = 1;
                LoadListEmployee();
            }

        }

        private void borPage2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");

            if (TongSoTrang > 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;

                if (textPage2.Text == (TongSoTrang - 1).ToString())
                {
                    borPageCuoi.Visibility = Visibility.Visible;
                    borLen1.Visibility = Visibility.Visible;
                }
            }

            pageNumber = int.Parse(textPage2.Text);
            LoadListEmployee();


            //listDepartment = result.data.items;

        }

        private void borPage3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {


            if (TongSoTrang == 3)
            {
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");

            }
            else if (TongSoTrang > 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                if (textPage3.Text == TongSoTrang.ToString())
                {

                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;

                }
                else if (textPage3.Text == "3")
                {
                    textPage1.Text = "2";
                    textPage2.Text = "3";
                    textPage3.Text = "4";
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");

                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();


                }
            }

            if (pageNumber == TongSoTrang - 1) { pageNumber = int.Parse(textPage3.Text); }
            else pageNumber = int.Parse(textPage2.Text);
            LoadListEmployee();


        }
        private void borLen1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            if (TongSoTrang == 3)
            {
                borPageDau.Visibility = Visibility.Visible;
                borLui1.Visibility = Visibility.Visible;
                BrushConverter brus = new BrushConverter();
                borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");

            }
            else if (TongSoTrang > 3)
            {
                if (textPage3.Text == TongSoTrang.ToString())
                {
                    borPageDau.Visibility = Visibility.Visible;
                    borLui1.Visibility = Visibility.Visible;
                    BrushConverter brus = new BrushConverter();
                    borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
                    textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
                    borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                    textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                    borPageCuoi.Visibility = Visibility.Collapsed;
                    borLen1.Visibility = Visibility.Collapsed;


                }
                else if (textPage3.Text == "3")
                {

                    if (borPageDau.Visibility == Visibility.Collapsed && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPageDau.Visibility = Visibility.Visible;
                        borLui1.Visibility = Visibility.Visible;


                    }
                    else if (borPageDau.Visibility == Visibility.Visible && borPageCuoi.Visibility == Visibility.Visible)
                    {
                        textPage1.Text = "2";
                        textPage2.Text = "3";
                        textPage3.Text = "4";
                        BrushConverter brus = new BrushConverter();
                        borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
                        borPage2.Background = (Brush)brus.ConvertFrom("#4c5bd4");
                        textPage2.Foreground = (Brush)brus.ConvertFrom("#ffffff");
                        borPage3.Background = (Brush)brus.ConvertFrom("#ffffff");
                        textPage3.Foreground = (Brush)brus.ConvertFrom("#474747");

                    }


                }
                else
                {
                    textPage1.Text = (int.Parse(textPage1.Text) + 1).ToString();
                    textPage2.Text = (int.Parse(textPage2.Text) + 1).ToString();
                    textPage3.Text = (int.Parse(textPage3.Text) + 1).ToString();

                }

            }
            pageNumber++;
            LoadListEmployee();
        }

        private void borPageCuoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textPage3.Text = TongSoTrang.ToString();
            textPage2.Text = (TongSoTrang - 1).ToString();
            textPage1.Text = (TongSoTrang - 2).ToString();
            borPageDau.Visibility = Visibility.Visible;
            borLui1.Visibility = Visibility.Visible;
            BrushConverter brus = new BrushConverter();
            borPage1.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage1.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage2.Background = (Brush)brus.ConvertFrom("#ffffff");
            textPage2.Foreground = (Brush)brus.ConvertFrom("#474747");
            borPage3.Background = (Brush)brus.ConvertFrom("#4c5bd4");
            textPage3.Foreground = (Brush)brus.ConvertFrom("#ffffff");
            borPageCuoi.Visibility = Visibility.Collapsed;
            borLen1.Visibility = Visibility.Collapsed;
            pageNumber = (int)TongSoTrang;
            LoadListEmployee();
        }
        #endregion

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var rectangle = (Rectangle)sender;
            var grid = rectangle.Parent as Grid;
            var border = grid.Parent as Border;
            border.Visibility = Visibility.Collapsed;
        }
        private void Send_CurrentPageMumber(object sender, int e)
        {
            pageNumber = e;
        }


      



        private void bodSelecTo_MouseUp(object sender, MouseButtonEventArgs e)
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

        private void bodSelecNhom_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListNhomCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListNhomCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListNhomCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void bodSelecChucVu_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListChucVuCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListChucVuCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListChucVuCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void bodSelectNgaySinh_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListNgaySinhCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListNgaySinhCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListNgaySinhCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void bodSelectHonNhan_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListHonNhanCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListHonNhanCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListHonNhanCollapsed.Visibility -= Visibility.Collapsed;

            }
        }

        private void bodSelectPhongBan_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListPhongbanCollapsed.Visibility == Visibility.Collapsed)
            {
                bodListPhongbanCollapsed.Visibility = Visibility.Visible;
            }
            else
            {
                bodListPhongbanCollapsed.Visibility -= Visibility.Collapsed;
            }
        }

        private void lsvPhongBan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Department)lsvPhongban.SelectedItem;
            if (selectedItem != null) {
                dep_id = selectedItem.dep_id;
                GetListTeam();
                bodListPhongbanCollapsed.Visibility = Visibility.Collapsed;
                LoadListEmployee();
            }
        }

        private void lsvTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Team)lsvTo.SelectedItem;
            if (selectedItem != null)
            {
                team_id = selectedItem.team_id;
                GetListGroup();
                bodListToCollapsed.Visibility = Visibility.Collapsed;
                LoadListEmployee();
            }
        }
        private void lsvNhom_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Group)lsvNhom.SelectedItem;
            if (selectedItem != null)
            {
                gr_id = selectedItem.gr_id;
                bodListNhomCollapsed.Visibility = Visibility.Collapsed;
                LoadListEmployee();
            }
        }

        private void lsvChucVu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Position)lsvChucVu.SelectedItem;
            if (selectedItem != null)
            {
                positionId = selectedItem.positionId;
                bodListChucVuCollapsed.Visibility = Visibility.Collapsed;
                LoadListEmployee();
            }
        }

        private void lsvHonNhan_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Item)lsvHonNhan.SelectedItem;
            if (selectedItem != null)
            {
                married = int.Parse(selectedItem.ID);
                bodListHonNhanCollapsed.Visibility = Visibility.Collapsed;
                LoadListEmployee();
            }
        }

        private void lsvNamSinh_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (Item)lsvNgaySinh.SelectedItem;
            if (selectedItem != null)
            {
                yearOfBirth = int.Parse(selectedItem.ID);
                bodListNgaySinhCollapsed.Visibility = Visibility.Collapsed;
                LoadListEmployee();
            }
        }

        private void borExportExcel_MouseUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Microsoft.Win32.SaveFileDialog sv = new Microsoft.Win32.SaveFileDialog();
                sv.Filter = "Microsoft Excel Worksheet | *.xlsx";
                sv.FileName = "Employee";
                if (sv.ShowDialog() == true)
                {
                    ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;
                    var file = new FileInfo(sv.FileName);
                    ExportExcel<OrgStructureEmployee.Employee>(EmployeeList, file).ContinueWith(zz => this.Dispatcher.Invoke(() =>
                    {


                    }));


                }
            }
            catch { }
        }
        private async Task ExportExcel<T>(List<OrgStructureEmployee.Employee> data, FileInfo file)
        {
            if (file.Exists)
            {
                file.Delete();
            }
            using (var package = new ExcelPackage(file))
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách giảm biên chế");

                // Add headers to the worksheet for the selected properties
                worksheet.Cells["A1"].Value = "Số thứ tự";
                worksheet.Cells["B1"].Value = "ID Nhân viên";
                worksheet.Cells["C1"].Value = "Họ và tên";
                worksheet.Cells["D1"].Value = "Công ty";
                worksheet.Cells["E1"].Value = "Phòng ban";
                worksheet.Cells["F1"].Value = "Tổ";
                worksheet.Cells["G1"].Value = "Nhóm";
                worksheet.Cells["H1"].Value = "Chức vụ";
                worksheet.Cells["I1"].Value = "Giới tính";
                worksheet.Cells["J1"].Value = "Ngày sinh";
                worksheet.Cells["K1"].Value = "Tình trạng hôn nhân";
                worksheet.Cells["L1"].Value = "Thông tin liên hệ";
                worksheet.Cells["M1"].Value = "Ngày bắt đầu làm việc";
                worksheet.Cells["N1"].Value = "Trình độ học vấn";
                worksheet.Cells["O1"].Value = "Kinh nghiệm làm việc";
                worksheet.Cells["P1"].Value = "Lý do nghỉ";
 

                // Populate the Excel sheet with data from the list for the selected properties
                int row = 2;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = item.STT;
                    worksheet.Cells[row, 2].Value = item.idQLC;
                    worksheet.Cells[row, 3].Value = item.userName;
                    worksheet.Cells[row, 4].Value = item.Company;
                    worksheet.Cells[row, 5].Value = item.Department;
                    worksheet.Cells[row, 6].Value = item.Team;
                    worksheet.Cells[row, 7].Value = item.Group;
                    worksheet.Cells[row, 8].Value = item.positionName;
                    worksheet.Cells[row, 9].Value = item.ep_gender;
                    worksheet.Cells[row, 10].Value = item.ep_birthday;
                    worksheet.Cells[row, 11].Value = item.ep_married;
                    worksheet.Cells[row, 12].Value = item.phoneTK;
                    worksheet.Cells[row, 13].Value = item.ep_start_working_time;
                    worksheet.Cells[row, 14].Value = item.ep_education;
                    worksheet.Cells[row, 15].Value = item.ep_experience;
                    worksheet.Cells[row, 16].Value = item.ly_do_nghi;

                    row++;
                }
                package.Save();
            }
        }
    }
}
