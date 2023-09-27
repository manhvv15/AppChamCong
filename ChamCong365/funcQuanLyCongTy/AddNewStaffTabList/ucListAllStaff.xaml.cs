using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.Popup.funcCompanyManager;
using ChamCong365.Popup.funcCompanyManager.AddNewStaffPopups;
using ChamCong365.TimeKeeping;
using ChamCong365.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
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

namespace ChamCong365.funcQuanLyCongTy.AddNewStaffTabList
{
    /// <summary>
    /// Interaction logic for ucListAllStaff.xaml
    /// </summary>
    /// 

    public partial class ucListAllStaff : UserControl
    {
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        List<Employee> EmployeeList = new List<Employee>();
        List<Company> ChildCompanyList = new List<Company>();
        List<Department> DepartmentList = new List<Department>();
        List<Position> PositionList = new List<Position>();

        public int com_id = 0;
        int dep_id = 0;
        int ep_id = 0;
        int TotalItem = 0;
        double TongSoTrang = 0;
        public int pageNumber = 1;
        int pageSize = 10;
        public Border OptionPopup { get; set; } = new Border();
        public ucListAllStaff(MainWindow main)
        {

            InitializeComponent();
            this.Main = main;
            com_id = Main.IdAcount;
            LoadListEmployee();
            LoadListChildCompany();
            LoadListDepartment();

        }

        public async void LoadListEmployee()
        {
            //load employee
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_list_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                if (com_id != 0) content.Add(new StringContent(this.com_id.ToString()), "com_id");
                if (dep_id != 0) content.Add(new StringContent(this.dep_id.ToString()), "dep_id");
                content.Add(new StringContent(this.pageNumber.ToString()), "pageNumber");
                content.Add(new StringContent(this.pageSize.ToString()), "pageSize");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    EmployeeRoot result = JsonConvert.DeserializeObject<EmployeeRoot>(responseContent);
                    if (result.data != null)
                    {
                        TotalItem = result.data.totalItems;
                        EmployeeList = result.data.items;
                    }

                    TongSoTrang = (int)Math.Ceiling((double)TotalItem / pageSize);

                    if (pageNumber == 1) LoadTableDataPagging();
                    lsvAllStaff.ItemsSource = EmployeeList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lấy danh sách nhân viên", ex.Message);
            }


            //Load Position
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
                        PositionList = result.data.data;
                    }
                }


            }
            catch
            {
                MessageBox.Show("Có lỗi khi lấy danh sách chức vụ");
            }

            //get position name
            var query = from e in EmployeeList
                        join p in PositionList on e.position_id equals p.positionId
                        select new Employee
                        {
                            _id = e._id,
                            ep_id = e.ep_id,
                            ep_email = (e.ep_email == "") ? "Chưa cập nhật" : e.ep_email,
                            ep_email_lh = (e.ep_email_lh == "") ? "Chưa cập nhật" : e.ep_email_lh,
                            ep_phone_tk = (e.ep_phone_tk == "") ? "Chưa cập nhật" : e.ep_phone_tk,
                            ep_name = (e.ep_name == "") ? "Chưa cập nhật" : e.ep_name,
                            dep_name = (e.dep_name == "") ? "Chưa cập nhật" : e.dep_name,
                            ep_phone = (e.ep_phone == "") ? "Chưa cập nhật" : e.ep_phone,
                            ep_image = (e.ep_image == "") ? "Chưa cập nhật" : e.ep_image,
                            ep_address = (e.ep_address == "") ? "Chưa cập nhật" : e.ep_address,
                            ep_description = (e.ep_description == "") ? "Chưa cập nhật" : e.ep_description,
                            ep_status = (e.ep_status == "") ? "Chưa cập nhật" : e.ep_status,
                            gr_name = (e.gr_name == "") ? "Chưa cập nhật" : e.gr_name,
                            positionName = (p.positionName == "") ? "Chưa cập nhật" : p.positionName,
                            ep_married_status = (e.ep_married_status == "") ? "Chưa cập nhật" : e.ep_married_status,

                            ep_education = e.ep_education,
                            ep_exp = e.ep_exp,

                            ep_gender = e.ep_gender,
                            ep_married = e.ep_married,
                            ep_birth_day = e.ep_birth_day,

                            create_time = e.create_time,
                            role_id = e.role_id,
                            group_id = e.group_id,
                            start_working_time = e.start_working_time,
                            position_id = e.position_id,

                            allow_update_face = e.allow_update_face,
                            com_id = e.com_id,
                            dep_id = e.dep_id


                        };

            EmployeeList = query.ToList();
            lsvAllStaff.ItemsSource = query.ToList();
            lsvListNameSaff.ItemsSource = query.ToList();
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

        private async void LoadListChildCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_ChildCompany_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    CompanyRoot result = JsonConvert.DeserializeObject<CompanyRoot>(responseContent);
                    ChildCompanyList = result.data.items;
                    lsvCompany.ItemsSource = ChildCompanyList;
                }



            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi  load danh sách công ty!" + e.Message);
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
                content.Add(new StringContent(Main.IdAcount.ToString()), "com_id");
                request.Content = content;
                var response = await httpClient.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                DepartmentRoot result = JsonConvert.DeserializeObject<DepartmentRoot>(responseContent);

                //load dropdown box
                DepartmentList = result.data.items;
                lsvDepartment.ItemsSource = DepartmentList;

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi khi load danh sách phòng ban" + e.Message);
            }
        }
        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is milliseconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dateTime;
        }

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
                this.com_id = selectedItem.com_id;
                txbSelectCompany.Text = selectedItem.com_name;
                txbSelectCompany.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListCompanyCollapsed.Visibility = Visibility.Collapsed;
            }
            pageNumber = 1;
            LoadListEmployee();

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
                this.dep_id = selectedItem.dep_id;
                txbSelectDepartment.Text = selectedItem.dep_name;
                txbSelectDepartment.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
            }
            pageNumber = 1;
            LoadListEmployee();

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
            string ep_name = "";
            var selectedItem = (Employee)lsvListNameSaff.SelectedItem;
            if (selectedItem != null)
            {
                this.ep_id = selectedItem.ep_id;
                ep_name = selectedItem.ep_name;
                txbSelectStaffName.Text = selectedItem.ep_name;
                txbSelectStaffName.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
            }
            var nameSearchList = EmployeeList.Where(x => x.ep_name.Contains(ep_name)).ToList();

            TongSoTrang = Math.Ceiling((double)nameSearchList.Count / pageSize);
            pageNumber = 1;
            lsvAllStaff.ItemsSource = nameSearchList;
            LoadTableDataPagging();

        }

        //xử lý popup sửa,phân quyền, xóa trong listview


        private void detailStaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (Employee)lsvAllStaff.SelectedItem;
            ucDetailNewStaff uc = new ucDetailNewStaff(Main, selectedItem);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }


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
            bodListCompanyCollapsed.Visibility = Visibility.Collapsed;
            bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
            bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
        }
        private void Send_CurrentPageMumber(object sender, int e)
        {
            pageNumber = e;
        }

        //xử lý popup sửa,phân quyền, xóa trong listview
        #region PopupInListview

        private void editStaff_MouseEnter(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        private void editStaff_MouseLeave(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
        }

        private void editStaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //Đóng popup nổi sửa, phân quyền, xóa
            OptionPopup.Visibility = Visibility.Collapsed;

            Main.grShowPopup.Children.Add(new ucUpdateNewStaff());
        }

        private void editStaffRole_MouseEnter(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#4C5BD4"));
        }

        private void editStaffRole_MouseLeave(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
        }

        private void editStaffRole_MouseUp(object sender, MouseButtonEventArgs e)
        {
            OptionPopup.Visibility = Visibility.Collapsed;
            var selectedItem = popupOption.DataContext as Employee;
            if (selectedItem != null)
            {
                int ep_id = selectedItem.ep_id;
                int role_id = selectedItem.role_id;
                Main.grShowPopup.Children.Add(new ucSetRole(ep_id, role_id)); ;
            }

        }

        private void deleteStaff_MouseEnter(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FF5B4D"));
        }

        private void deleteStaff_MouseLeave(object sender, MouseEventArgs e)
        {
            var dockPanel = (DockPanel)sender;
            var detailTextBlock = (TextBlock)dockPanel.Children[1];
            detailTextBlock.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#474747"));
        }

        private void deleteStaff_MouseUp(object sender, MouseButtonEventArgs e)
        {

            //Đóng popup nổi sửa, phân quyền, xóa
            OptionPopup.Visibility = Visibility.Collapsed;
            var selectedItem = popupOption.DataContext as Employee;
            if (selectedItem != null)
            {
                int ep_id = selectedItem.ep_id;
                Main.grShowPopup.Children.Add(new ucDeleteNewStaff(this, ep_id));
            }
        }
        #endregion

        private void wrapBtnOptions_MouseUp(object sender, MouseButtonEventArgs e)
        {

            if (popupOption.Visibility == Visibility.Visible)
            {
                popupOption.Visibility = Visibility.Collapsed;
            }
            else
            {
                var wrap = sender as WrapPanel;
                System.Windows.Point p = e.GetPosition(Main.dopBody);
                double x = p.X - popupOption.Width + 10;
                double y = p.Y - 60;
                if (Main.Width < 1322) y = y - 60;
                Thickness thickness = new Thickness(x, y, 0, 0);
                popupOption.Margin = thickness;
                popupOption.Visibility = Visibility.Visible;
                popupOption.DataContext = wrap.DataContext;
            }
        }

        private void CloseOptionPopup(object sender, MouseButtonEventArgs e)
        {
            popupOption.Visibility = Visibility.Collapsed;
        }
    }
}
