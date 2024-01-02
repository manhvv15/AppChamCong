﻿using QuanLyChung365TruocDangNhap.Hr.Entities.AdministrationEntity.PersonnelChangeEntity;
using QuanLyChung365TruocDangNhap.Hr.Popups.AdministrationPopups.WorkingRotation;
using QuanLyChung365TruocDangNhap.Hr.View;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Net;
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

namespace QuanLyChung365TruocDangNhap.Hr.Pages.AdministrationPages.PersonalChangePages
{
    /// <summary>
    /// Interaction logic for WorkingRotationPage.xaml
    /// </summary>
    public partial class WorkingRotationPage : Page, INotifyPropertyChanged
    {
        const int COUNT_RECORD_PER_PAGE = 10;
        int page_now = 1;
        int total_page = 1;
        int total;
        string token;
        public string id_com;
        public string ep_id;
        public string status_id;
        int TypeUser;

        public Dictionary<string, string> listCom = new Dictionary<string, string>();
        public Dictionary<string, string> listDepartment = new Dictionary<string, string>();
        public Dictionary<string, string> listAllDepartment = new Dictionary<string, string>();
        public Dictionary<string, string> listEmployee = new Dictionary<string, string>();
        List<string> listComIdGotDepartment = new List<string>();

        bool perAdd, perEdit, perDel, perShow; // quyền thêm, sửa, xóa,xem

        public bool PerAdd
        {
            get { return perAdd; }
            set
            {
                perAdd = value;
                OnPropertyChanged("PerAdd");
            }
        }

        public bool PerEdit
        {
            get { return perEdit; }
            set
            {
                perEdit = value;
                OnPropertyChanged("PerEdit");
            }
        }

        public bool PerDel
        {
            get { return perDel; }
            set
            {
                perDel = value;
                OnPropertyChanged("PerDel");
            }
        }
        public bool PerShow
        {
            get { return perShow; }
            set
            {
                perShow = value;
                OnPropertyChanged("PerShow");
            }
        }
        // deligate event show popups
        public delegate void ShowPopup(object obj);
        public static event ShowPopup onShowPopup;
        public WorkingRotationPage(string token, string id_com,int TypeUser,bool perAdd, bool perEdit, bool perDel, bool perShow)
        {
            InitializeComponent();
            this.token = token;
            this.id_com = id_com;
            this.TypeUser = TypeUser;
            this.PerAdd = perAdd;
            this.PerEdit = perEdit;
            this.PerDel = perDel;
            this.PerShow = perShow;
            DataContext = this;
            TotalPgae.Text = total_page.ToString();
            void1();
            if (PerShow)
            {
                UpDownSalary.Visibility = Visibility.Visible;
            }
            else
            {
                UpDownSalary.Visibility = Visibility.Collapsed;
            }
        

            //void1();
            AddNewWorkingRotation.hidePopup += HidePopup;
            DeleteJWorkingRotation.hidePopup += HidePopup;
            EditWorkingRotation.hidePopup += HidePopup;

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy"; //for the second type
            Thread.CurrentThread.CurrentCulture = ci;
        }
        private void HidePopup(int mode)
        {
            if (mode == 1)
            {
                GetData();
            }
            onShowPopup("");
        }



        private void cbxTenNV_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            tbSearch.SelectedIndex = -1;
            string textSearch = tbSearch.Text + e.Text;
            tbSearch.IsDropDownOpen = true;
            if (textSearch == "")
            {
                tbSearch.Text = "";
                tbSearch.Items.Refresh();
                tbSearch.ItemsSource = listEmployee;
                tbSearch.SelectedIndex = -1;
            }
            else
            {
                tbSearch.ItemsSource = "";
                tbSearch.Items.Refresh();
                tbSearch.ItemsSource = listEmployee.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }
        public async void void1()
        {
            await GetDatalistCom();
            await GetDatalistDepartment();
            GetData();
            GetDatalistEmployee();
        }

        public string PageNow
        {
            get { return page_now.ToString(); }
            set
            {
                page_now = Convert.ToInt32(value);
                OnPropertyChanged("PageNow");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        private void NavigateToPage(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            string name = textBlock.Name;
            switch (name)
            {
                case "Appointment":
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(HomeView))
                        {
                            (window as HomeView).MainContent.Navigate(new AppointmentPage(token, id_com, TypeUser, PerAdd, PerEdit, PerDel, PerShow));
                        }
                    };
                    break;
                case "Downsizing":
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(HomeView))
                        {
                            (window as HomeView).MainContent.Navigate(new DownsizingPage(token, id_com, TypeUser, PerAdd, PerEdit, PerDel, PerShow));
                        }
                    };
                    break;
                case "UpDownSalary":
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(HomeView))
                        {
                            (window as HomeView).MainContent.Navigate(new UpDownSalaryPage(token, id_com, TypeUser, PerAdd, PerEdit, PerDel, PerShow));
                        }
                    };
                    break;
                case "WorkingRotation":
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(HomeView))
                        {
                            (window as HomeView).MainContent.Navigate(new WorkingRotationPage(token, id_com, TypeUser, PerAdd, PerEdit, PerDel, PerShow));
                        }
                    };
                    break;
                case "BreakTheRules":
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(HomeView))
                        {
                            (window as HomeView).MainContent.Navigate(new BreakTheRules(token, id_com, TypeUser, PerAdd, PerEdit, PerDel, PerShow));
                        }
                    };
                    break;
                case "Chart":
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(HomeView))
                        {
                            (window as HomeView).MainContent.Navigate(new ChartPage(token, id_com, TypeUser, PerAdd, PerEdit, PerDel, PerShow));
                        }
                    };
                    break;
            }
        }

        private async void GetDatalistEmployee()
        {
            var httpClient = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Post;


            string api = "http://210.245.108.202:3000/api/qlc/managerUser/list";
            Dictionary<string, string> form = new Dictionary<string, string>();
            form.Add("pageNumber", "1");
            form.Add("pageSize", "10000");
            form.Add("com_id", id_com.ToString());
            httpRequestMessage.RequestUri = new Uri(api);
            httpRequestMessage.Headers.Add("Authorization", "Bearer " + token);
            httpRequestMessage.Content = new FormUrlEncodedContent(form);
            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            getListEmployee result = JsonConvert.DeserializeObject<getListEmployee>(responseContent);
            try
            {
                listEmployee = new Dictionary<string, string>();
                listEmployee.Add("", "Chọn nhân viên");
                
                foreach (var item in result.data.items)
                {
                    string dep_name = " (Chưa cập nhật)";
                    if (item.dep_name != null && item.dep_name != "")
                    {
                        dep_name = " (" + item.dep_name + "- ID: " + item.ep_id + ")";
                    }
                    string name = item.ep_name + dep_name;
                    listEmployee.Add(item.ep_id, name);
                }
                tbSearch.ItemsSource = listEmployee;
            }
            catch
            {

            }

        }

        //lấy toàn bộ danh sách nhân viên 
        /*private async void GetDatalistEmployee()
        {
            var httpClient = new HttpClient();
            var httpRequestMessage = new HttpRequestMessage();
            httpRequestMessage.Method = HttpMethod.Get;


            string api = "https://chamcong.24hpay.vn/service/get_list_employee_from_company.php?" + "&offset=0" + "&length=" + COUNT_RECORD_PER_PAGE.ToString() + "&filter_by[active]=true" + " &id_com=" + id_com;

            httpRequestMessage.RequestUri = new Uri(api);
            httpRequestMessage.Headers.Add("Authorization", token);

            var response = await httpClient.SendAsync(httpRequestMessage);
            var responseContent = await response.Content.ReadAsStringAsync();

            getListEmployee result = JsonConvert.DeserializeObject<getListEmployee>(responseContent);
            try
            {
                listEmployee = new Dictionary<string, string>();
                listEmployee.Add("", "Chọn nhân viên");
                foreach (var item in result.data.items)
                {
                    string dep_name = " (Chưa cập nhật)";
                    if (item.dep_name != null && item.dep_name != "")
                    {
                        dep_name = " (" + item.dep_name + ")";
                    }
                    string name = item.ep_name + dep_name;
                    listEmployee.Add(item.ep_id, name);
                }
                tbSearch.ItemsSource = listEmployee;
            }
            catch
            {

            }

        }*/



        private async void GetData()
        {
            try
            {
                Loading.Visibility = Visibility.Visible;
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                string api = "";
                string date1 = "";
                string date2 = "";
                if (dp1.SelectedDate != null)
                {
                    long unixSeconds = DateTimeOffset.Now.ToUnixTimeSeconds();
                    date1 = dp1.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                if (dp2.SelectedDate != null)
                {
                    date2 = dp2.SelectedDate.Value.ToString("yyyy-MM-dd");
                }
                string dep_id = "";
                api = "http://210.245.108.202:3006/api/hr/personalChange/getListTranferJob";
                Dictionary<string, string> form = new Dictionary<string, string>();
                form.Add("page", page_now.ToString());
                form.Add("pageSize", COUNT_RECORD_PER_PAGE.ToString());
                form.Add("fromDate", date1);
                form.Add("toDate", date2);
                form.Add("winform", "1");
                if(tbSearch.SelectedValue != null)
                    form.Add("ep_id", tbSearch.SelectedValue.ToString());
                if(cbxTenPhongBan.SelectedIndex > 0)
                {
                    form.Add("update_dep_id", cbxTenPhongBan.SelectedValue.ToString());
                }
                httpRequestMessage.RequestUri = new Uri(api);
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                httpRequestMessage.Content = new FormUrlEncodedContent(form);
                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                ListJobRotation result = JsonConvert.DeserializeObject<ListJobRotation>(responseContent);

                if (result.data != null)
                {
                    total = result.data.totalItems;
                    Paging();
                    foreach (var item in result.data.items)
                    {
                      
                        item.create_time = DateTime.Parse(item.create_time).ToString("dd/MM/yyyy");
                    }
                    List<Items> listJobRotation = result.data.items;
                    icWorkingRotationPage.Items.Refresh();
                    icWorkingRotationPage.ItemsSource = listJobRotation;
                }
                Loading.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            {
                //CustomMessageBox.Show("error");
            }
        }
        private async Task GetDatalistDepartment()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                string api = APIs.API.listDepartment;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + token);

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                listDepartment result = JsonConvert.DeserializeObject<listDepartment>(responseContent);


                if (result.data == null) return;
                listDepartment.Add("", "Tất cả phòng ban");
                foreach (var item in result.data.items)
                {
                    listDepartment.Add(item.dep_id, item.dep_name);
                    if (!listAllDepartment.ContainsKey(item.dep_id))
                    {
                        listAllDepartment.Add(item.dep_id, item.dep_name);
                    }
                }

                cbxTenPhongBan.Items.Refresh();
                cbxTenPhongBan.ItemsSource = listDepartment;
                listComIdGotDepartment.Add(id_com);
            }
            catch (Exception)
            {

                //CustomMessageBox.Show("Error");
            }
        }
        private async Task GetDatalistCom()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                string api = APIs.API.listCompany;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + token);

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                listCompany result = JsonConvert.DeserializeObject<listCompany>(responseContent);


                if (result.data == null) return;
                foreach (var item in result.data.items)
                {
                    listCom.Add(item.com_id, item.com_name);
                }
            }
            catch (Exception)
            {

                //CustomMessageBox.Show("Error");
            }
        }

        private void ShowPopupDelete(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            Items dataEntity = grid.DataContext as Items;
            string ep_id = dataEntity.ep_id;
            string com_id = dataEntity.com_id;

            DeleteJWorkingRotation deleteJWorkingRotation = new DeleteJWorkingRotation(token, ep_id, com_id);
            onShowPopup(deleteJWorkingRotation);
        }

        private void ShowPopupAddNew(object sender, MouseButtonEventArgs e)
        {
          
            AddNewWorkingRotation addNewWorkingRotation = new AddNewWorkingRotation(token,id_com, TypeUser);
            onShowPopup(addNewWorkingRotation);
        }

        private void tbSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                PageNow = "1";
                GetData();
            }
        }


        private void NextPage(object sender, MouseButtonEventArgs e)
        {
            DisableBtn();
            int page_next = page_now + 1;
            PageNow = page_next.ToString();
            GetData();
        }

        private void PrevPage(object sender, MouseButtonEventArgs e)
        {
            DisableBtn();
            int page_prev = page_now - 1;
            PageNow = page_prev.ToString();
            GetData();
        }

        private void DisableBtn()
        {
            btnPrev.IsEnabled = false;
            btnPrev.Opacity = 0.3;
            btnNext.IsEnabled = false;
            btnNext.Opacity = 0.3;
        }

        private void IsSetValidBtn()
        {
            if (page_now == 1)
            {
                btnPrev.IsEnabled = false;
                btnPrev.Opacity = 0.3;
            }
            else
            {
                btnPrev.IsEnabled = true;
                btnPrev.Opacity = 1;
            }

            if (page_now == total_page)
            {
                btnNext.IsEnabled = false;
                btnNext.Opacity = 0.3;
            }
            else
            {
                btnNext.IsEnabled = true;
                btnNext.Opacity = 1;
            }
        }

        private void Paging()
        {
            if (total == 0)
            {
                txtNoData.Visibility = Visibility.Visible;
                total_page = 1;
            }
            else
            {
                txtNoData.Visibility = Visibility.Collapsed;
                if (total % COUNT_RECORD_PER_PAGE == 0)
                {
                    total_page = total / COUNT_RECORD_PER_PAGE;
                    TotalPgae.Text = total_page.ToString();
                }
                else
                {
                    total_page = total / COUNT_RECORD_PER_PAGE + 1;
                    TotalPgae.Text = total_page.ToString();
                }
            }

            IsSetValidBtn();
        }

        private DateTime ConvertDate(string date)
        {
            DateTime myDate;
            try
            {
                myDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                                              System.Globalization.CultureInfo.InvariantCulture);
                return myDate;
            }
            catch
            {
                try
                {
                    myDate = DateTime.ParseExact(date, "d/MM/yyyy",
                                                  System.Globalization.CultureInfo.InvariantCulture);
                    return myDate;
                }
                catch
                {
                    return new DateTime();
                }
            }
        }

        private async Task GetOldDepName(string com_id)
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Post;
                string api = APIs.API.listDepartment;

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + token);

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                listDepartment result = JsonConvert.DeserializeObject<listDepartment>(responseContent);


                if (result.data == null) ;
                foreach (var item in result.data.items)
                {
                    if (!listAllDepartment.ContainsKey(item.dep_id))
                    {
                        listAllDepartment.Add(item.dep_id, item.dep_name);
                    }
                }
                listComIdGotDepartment.Add(com_id);
            }
            catch (Exception)
            {

            }
        }

        private string ConvertTicksToDate(string ticks)
        {
            try
            {
                System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
                dtDateTime = dtDateTime.AddSeconds(Convert.ToDouble(ticks)).ToLocalTime();
                return dtDateTime.ToString("dd/MM/yyyy");
            }
            catch
            {
                return "";
            }
        }

        private void ClickSearch(object sender, MouseButtonEventArgs e)
        {
            PageNow = "1";
            GetData();
        }

        public double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }

        /*private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            dp1.IsDropDownOpen = true;
        }
        private void Border_MouseUp1(object sender, MouseButtonEventArgs e)
        {
            dp2.IsDropDownOpen = true;
        }*/

        private void ScrollViewer_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                scroll.ScrollToVerticalOffset(scroll.VerticalOffset);
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta);
            }
        }

        private void ScrollViewer_PreviewMouseWheel_1(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                scData.ScrollToVerticalOffset(scData.VerticalOffset);
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta);
            }
            else
                scData.ScrollToVerticalOffset(scData.VerticalOffset - e.Delta);
        }

        private void ShowPopupEdit(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            Items dataEntity = grid.DataContext as Items;
            EditWorkingRotation editWorkingRotation = new EditWorkingRotation(token, dataEntity);
            onShowPopup(editWorkingRotation);
        }
        private void cbxTenPhongBan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                cbxTenPhongBan.SelectedIndex = -1;
                string textSearch = cbxTenPhongBan.Text;
                cbxTenPhongBan.Items.Refresh();
                cbxTenPhongBan.ItemsSource = listDepartment.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }

        private void cbxTenPhongBan_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            cbxTenPhongBan.SelectedIndex = -1;
            string textSearch = cbxTenPhongBan.Text + e.Text;
            cbxTenPhongBan.IsDropDownOpen = true;
            if (textSearch == "")
            {
                cbxTenPhongBan.Text = "";
                cbxTenPhongBan.Items.Refresh();
                cbxTenPhongBan.ItemsSource = listDepartment;
                cbxTenPhongBan.SelectedIndex = -1;
            }
            else
            {
                cbxTenPhongBan.ItemsSource = "";
                cbxTenPhongBan.Items.Refresh();
                cbxTenPhongBan.ItemsSource = listDepartment.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }
        private void cbxTenNV_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Back)
            {
                tbSearch.SelectedIndex = -1;
                string textSearch = tbSearch.Text;
                tbSearch.Items.Refresh();
                tbSearch.ItemsSource = listEmployee.Where(t => t.Value.ToLower().Contains(textSearch.ToLower()));
            }
        }
    }
}
