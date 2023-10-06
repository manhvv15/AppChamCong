using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.Popup.funcCompanyManager;
using ChamCong365.Popup.funcCompanyManager.DepartmentManagerPopups;
using ChamCong365.Popup.PopupTimeKeeping;
using ChamCong365.TimeKeeping;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.LexicalAnalysis;
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

namespace ChamCong365
{


    public partial class ucListLuanChuyen : UserControl
    {

        public MainWindow Main;
        BrushConverter brus = new BrushConverter();
        public List<TranferJob> TranferJobList = new List<TranferJob>();
        public List<Company> ChildCompanyList = new List<Company>();
        public List<Department> DepartmentList = new List<Department>();
        public List<Employee> AllEmployeeList = new List<Employee>();

        int update_dep_id = -1;
        int ep_id = -1;
        DateTime? fromDate = new DateTime();
        int TotalItem = 0;
        double TongSoTrang = 0;
        public int pageNumber = 1;
        int pageSize = 10;

        public List<Company> dataCompanySelectBox = new List<Company>();
        public ucListLuanChuyen(MainWindow main)
        {
            InitializeComponent();
            this.Main = main;


            LoadListDepartment();
            LoadSearchNameStaff();
            LoadListLuanChuyen();

        }

        public async void LoadListLuanChuyen()
        {
            //var client = new HttpClient();
            //var request = new HttpRequestMessage(HttpMethod.Post, API.list_TranferJob_api);
            //request.Headers.Add("Authorization", "Bearer " + "Bearer " + Properties.Settings.Default.Token);

            //var response = await client.SendAsync(request);
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_TranferJob_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);

                var content = new MultipartFormDataContent();
                content.Add(new StringContent(pageNumber.ToString()), "page");
                content.Add(new StringContent(pageSize.ToString()), "pageSize");
                if (update_dep_id != -1) content.Add(new StringContent(update_dep_id.ToString()), "update_dep_id");
                if (ep_id != -1) content.Add(new StringContent(ep_id.ToString()), "ep_id");
                content.Add(new StringContent(fromDate.ToString()), "fromDate");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    TranferJobRoot result = JsonConvert.DeserializeObject<TranferJobRoot>(responseContent);
                    string current_com_name = Main.txbNameAccount.Text;
                    if (result.data.data != null)
                    {
                        TranferJobList = (from item in result.data.data
                                          select new TranferJob
                                          {
                                              _id = item._id,
                                              ep_id = item.ep_id,
                                              created_at = item.created_at,
                                              note = (item.note == "") ? "Chưa cập nhât" : item.note,
                                              mission = (item.mission == "") ? "Chưa cập nhât" : item.mission,
                                              userName = (item.userName == "") ? "Chưa cập nhât" : item.userName,
                                              old_dep_name = (item.old_dep_name == "") ? "Chưa cập nhât" : item.old_dep_name,
                                              old_dep_id = item.old_dep_id,
                                              old_position = (item.old_position == "") ? "Chưa cập nhât" : item.old_position,
                                              id_old_position = item.id_old_position,
                                              old_com_name = current_com_name,
                                              new_com_name = (item.new_com_name == "") ? "Chưa cập nhât" : item.new_com_name,
                                              new_dep_name = (item.new_dep_name == "") ? "Chưa cập nhât" : item.new_dep_name,
                                              new_dep_id = item.new_dep_id,
                                              new_position = (item.new_position == "") ? "Chưa cập nhât" : item.new_position,
                                              id_new_position = item.id_new_position,
                                              team_name = (item.team_name == "") ? "Chưa cập nhât" : item.team_name,
                                              gr_name = (item.gr_name == "") ? "Chưa cập nhât" : item.gr_name,
                                          }).ToList();
                        TotalItem = result.data.totalCount;
                        TongSoTrang = Math.Ceiling((double)TotalItem / pageSize);
                    }

                }
                if (pageNumber == 1) LoadTableDataPagging();
                dgTranferJob.ItemsSource = TranferJobList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi lấy danh sách luân chuyển" + ex.Message);
            }


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

                CompanyRoot result = JsonConvert.DeserializeObject<CompanyRoot>(responseContent);
                ChildCompanyList = result.data.items;


            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
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
                lsvDepartment.ItemsSource = DepartmentList.Prepend(new Department() { dep_id = -1, dep_name = "Chọn phòng ban" });

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }
        public async void LoadSearchNameStaff()
        {
            try
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
                    lsvListNameSaff.ItemsSource = AllEmployeeList.Prepend(new Employee() { ep_id = -1, ep_name = "Tất cả" });
                }
            }
            catch 
            {

            }

        }

        //dropdown box select department

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
                this.update_dep_id = selectedItem.dep_id;
                txbSelectDepartment.Text = selectedItem.dep_name;
                txbSelectDepartment.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
            }
            pageNumber = 1;
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
                this.ep_id = selectedItem.ep_id;
                txbSelectStaffName.Text = selectedItem.ep_name;
                txbSelectStaffName.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListStaffNameCollapsed.Visibility = Visibility.Collapsed;
            }
        }

        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var grid = ((Rectangle)sender).Parent as Grid;
            var bodPopUp = grid.Parent as Border;
            bodPopUp.Visibility = Visibility.Collapsed;


        }

        private void Update_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (TranferJob)dgTranferJob.SelectedItem;
            Main.grShowPopup.Children.Add(new ucUpdateLuanChuyen(Main, this, selectedItem));
        }

        private void Delete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            var selectedItem = (TranferJob)dgTranferJob.SelectedItem;
            if (selectedItem != null)
            {
                int Ep_TranferJob_Id = selectedItem.ep_id;
                Main.grShowPopup.Children.Add(new ucDeleteLuanChuyen(this, Ep_TranferJob_Id));
            }

        }

        private void bodAddLuanChuyen_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

            Main.grShowPopup.Children.Add(new ucCreateLuanChuyen(Main, this));

        }



        private void dgTranferJob_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            Main.scrollMain.ScrollToVerticalOffset(Main.scrollMain.VerticalOffset - e.Delta);

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
            LoadListLuanChuyen();
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
            LoadListLuanChuyen();

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
                LoadListLuanChuyen();
            }
            if (int.Parse(textPage1.Text) == 1)
            {
                pageNumber = 1;
                LoadListLuanChuyen();
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
            LoadListLuanChuyen();


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
            LoadListLuanChuyen();


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
            LoadListLuanChuyen();
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
            LoadListLuanChuyen();
        }
        #endregion
        #region LocDuLieu
        private void timeTranferJob_CalendarClosed(object sender, RoutedEventArgs e)
        {
            timeTranferJob.Foreground = (Brush)brus.ConvertFromString("#474747");
            fromDate = timeTranferJob.SelectedDate;
            pageNumber = 1;
        }

        #endregion

        private void bodFind_MouseUp(object sender, MouseButtonEventArgs e)
        {
            LoadListLuanChuyen();
        }

        private void txtSearchNameSaff_TextChanged(object sender, TextChangedEventArgs e)
        {
            var textName = txtSearchNameSaff.Text;
            lsvListNameSaff.ItemsSource = AllEmployeeList.Where(x => x.ep_name.ToUpper().Contains(textName.ToUpper())).Prepend(new Employee() { ep_id = -1, ep_name = "Tất cả" }); ;
        }
    }
}

