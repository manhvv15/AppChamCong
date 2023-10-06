using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace ChamCong365.funcQuanLyCongTy.AddNewStaffTabList
{

    public partial class ucListWaitingStaff : UserControl, INotifyPropertyChanged
    {
        private int _isCheckedAll = 1;

        public int IsCheckedAll
        {
            get { return _isCheckedAll; }
            set
            {
                if (_isCheckedAll != value)
                {
                    _isCheckedAll = value;
                    OnPropertyChanged(nameof(IsCheckedAll));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        MainWindow Main;
        BrushConverter bc = new BrushConverter();
        List<Employee> EmployeeList = new List<Employee>();
        List<Employee> SelectedEmployeeList = new List<Employee>();
        List<Company> ChildCompanyList = new List<Company>();
        List<Department> DepartmentList = new List<Department>();
        List<Position> PositionList = new List<Position>();

        int com_id = 0;
        int dep_id = 0;
        int ep_id = 0;
        string ep_name = "";
        int TotalItem = 0;
        double TongSoTrang = 0;
        public int pageNumber = 1;
        int pageSize = 10;
        public ucListWaitingStaff(MainWindow main)
        {

            InitializeComponent();
            Main = main;
            com_id = Main.IdAcount;
            LoadListEmployee();
            LoadListChildCompany();
            LoadListDepartment();
        }

        #region LoadDataFromAPI
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
                content.Add(new StringContent("Pending"), "ep_status");
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
                        if (ep_name != "")
                        {
                            EmployeeList = EmployeeList.Where(x => x.ep_name.Contains(ep_name)).ToList();

                        }
                    }

                    TongSoTrang = (int)Math.Ceiling((double)TotalItem / pageSize);

                    if (pageNumber == 1) LoadTableDataPagging();
                    lsvPendingStaff.ItemsSource = EmployeeList;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lấy danh sách nhân viên" + ex.Message);
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
                            positionName = PositionList.Where(x=>x.positionId==e.position_id).FirstOrDefault()?.positionName??"Chưa cập nhật",
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
            lsvPendingStaff.ItemsSource = query.ToList();
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
                    lsvSelectCompaty.ItemsSource = ChildCompanyList;
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
                lsvSelectDepartment.ItemsSource = DepartmentList;

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

        #endregion



        public void CloseSearchPopup()
        {
            bodSelectCompanyCollapsed.Visibility = Visibility.Collapsed;
            bodSelectDepartmentCollapsed.Visibility = Visibility.Collapsed;
            bodSelectNameSaffCollapsed.Visibility = Visibility.Collapsed;
            rectangleClosePopup.Margin = new Thickness(0);
        }

        private void bodSelectCompany_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //làm cho vùng đóng popup bao hết màn hình
            rectangleClosePopup.Margin = new Thickness(-2000);
            bodSelectCompanyCollapsed.Visibility = Visibility.Visible;
        }

        private void bodSearchStaffName_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //làm cho vùng đóng popup bao hết màn hình
            rectangleClosePopup.Margin = new Thickness(-2000);
            bodSelectNameSaffCollapsed.Visibility = Visibility.Visible;
        }

        private void bodSearchDepartment_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //làm cho vùng đóng popup bao hết màn hình
            rectangleClosePopup.Margin = new Thickness(-2000);
            bodSelectDepartmentCollapsed.Visibility = Visibility.Visible;
        }

        private void wrapBtnOptions_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var wrap = (WrapPanel)sender;
            var grid = wrap.Parent as Grid;
            var parentBorder = grid.Parent as Border;
            var border = grid.Children[1] as Border;
            border.Margin = new Thickness(-320, 0, 0, -150);
            border.Visibility = Visibility.Visible;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CloseSearchPopup();
        }

        private void lsvSelectCompaty_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CloseSearchPopup();
            var selectedItem = (Company)lsvSelectCompaty.SelectedItem;
            if (selectedItem != null)
            {
                this.com_id = selectedItem.com_id;
                txbSelectCompany.Text = selectedItem.com_name;
                txbSelectCompany.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodSelectCompanyCollapsed.Visibility = Visibility.Collapsed;
            }
            pageNumber = 1;

        }



        private void lsvSelectDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CloseSearchPopup();

            var selectedItem = (Department)lsvSelectDepartment.SelectedItem;
            if (selectedItem != null)
            {
                this.dep_id = selectedItem.dep_id;
                txbSearchDepartment.Text = selectedItem.dep_name;
                txbSearchDepartment.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodSelectDepartmentCollapsed.Visibility = Visibility.Collapsed;
            }
            pageNumber = 1;
        }

        private void lsvListNameSaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CloseSearchPopup();

            var selectedItem = (Employee)lsvListNameSaff.SelectedItem;
            if (selectedItem != null)
            {
                this.ep_id = selectedItem.ep_id;
                ep_name = selectedItem.ep_name;
                txbSearchStaffName.Text = selectedItem.ep_name;
                txbSearchStaffName.Foreground = (Brush)bc.ConvertFromString("#474747");
                txtSearchNameSaff.Text = selectedItem.ep_name;
                txtSearchNameSaff.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodSelectNameSaffCollapsed.Visibility = Visibility.Collapsed;
            }
            pageNumber = 1;


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

        private void bodFind_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LoadListEmployee();
        }


        private void Employee_Checked(object sender, RoutedEventArgs e)
        {
            var i = e;
            var selectedItem = (Employee)lsvPendingStaff.SelectedItem;
            if (selectedItem != null)
            {
                SelectedEmployeeList.Add(selectedItem);
            }
        }

        private void Employee_UnChecked(object sender, RoutedEventArgs e)
        {
            var selectedItem = (Employee)lsvPendingStaff.SelectedItem;
            if (selectedItem != null)
            {
                SelectedEmployeeList.Remove(selectedItem);
            }
        }

        private void Employee_CheckAll(object sender, RoutedEventArgs e)
        {


            //check all item in listview
            for (int i = 0; i < EmployeeList.Count; i++)
            {
                EmployeeList[i].isCheck = true;

            }
            lsvPendingStaff.ItemsSource = null;
            lsvPendingStaff.ItemsSource = EmployeeList;
        }

        private void Employee_UnCheckAll(object sender, RoutedEventArgs e)
        {

            //Uncheck all item in listview
            for (int i = 0; i < EmployeeList.Count; i++)
            {
                EmployeeList[i].isCheck = false;

            }
            lsvPendingStaff.ItemsSource = null;
            lsvPendingStaff.ItemsSource = EmployeeList;
        }

        public async void ActiveEmployee()
        {
            string listEmId = "";
            if (SelectedEmployeeList != null)
            {

                foreach (var item in EmployeeList)
                {
                    if (item.isCheck)
                    {
                        listEmId += "," + item.ep_id;
                    }



                }
            }
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/qlc/managerUser/verifyListUsers");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(listEmId), "listUsers");

                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Duyệt thành công");

                }
                else
                {
                    MessageBox.Show("Duyệt thất bại");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi duyệt");
            }
        }

        private void bodBtnDuyet_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ActiveEmployee();
        }

        private void borCheck_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var border = (Border)sender;
            var grid = (Grid)border.Parent;
            var checkbox = grid.Children[0] as CheckBox;
            if (checkbox.IsChecked == false)
            {
                checkbox.IsChecked = true;
            }
            else
            {
                checkbox.IsChecked = false;
            }
        }
    }

}
