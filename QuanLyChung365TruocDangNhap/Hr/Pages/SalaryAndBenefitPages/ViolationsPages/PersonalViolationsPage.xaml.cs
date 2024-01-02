﻿using QuanLyChung365TruocDangNhap.Hr.Entities.SalaryAndBenefits;
using QuanLyChung365TruocDangNhap.Hr.Popups.SarlaryAndBenefitPopups.ViolationsPopups;
using QuanLyChung365TruocDangNhap.Hr.View;
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

namespace QuanLyChung365TruocDangNhap.Hr.Pages.SalaryAndBenefitPages.ViolationsPages
{
    /// <summary>
    /// Interaction logic for PersonalViolationsPage.xaml
    /// </summary>
    public partial class PersonalViolationsPage : Page, INotifyPropertyChanged
    {
        // deligate event show popups
        public delegate void ShowPopup(object obj);
        public static event ShowPopup onShowPopup;

        const int COUNT_RECORD_PER_PAGE = 10;
        int page_now = 1;
        int total;
        int total_page = 1;
        public string token;
        string comid;
        int TypeUser;
        bool perAdd, perEdit, perDel; // quyền thêm, sửa, xóa

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
        public PersonalViolationsPage(string token,string comid,int TypeUser, bool perAdd, bool perEdit, bool perDel)
        {
            InitializeComponent();
            this.token = token;
            this.comid = comid;
            this.TypeUser = TypeUser;
            this.PerAdd = perAdd;
            this.PerEdit = perEdit;
            this.PerDel = perDel;
            GetData();
            DataContext = this;
            TotalPage.Text = total_page.ToString();
            PersonalAddViolationPopup.hidePopup += HidePopup;
            PersonalEditViolationPopup.hidePopup += HidePopup;
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
        private async void GetData()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();
                httpRequestMessage.Method = HttpMethod.Get;
                string api = APIs.API.listInfringes;


                var parameters = new List<KeyValuePair<string, string>>();
                httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                api = api + string.Format("?type={0}&page={1}&pageSize={2}&keyword={3}","1",page_now.ToString(),COUNT_RECORD_PER_PAGE.ToString(), tbSearch.Text);
                httpRequestMessage.RequestUri = new Uri(api);
/*
                parameters.Add(new KeyValuePair<string, string>("rowno", page_now.ToString()));
                parameters.Add(new KeyValuePair<string, string>("rowperpage", COUNT_RECORD_PER_PAGE.ToString()));
                parameters.Add(new KeyValuePair<string, string>("keyword", tbSearch.Text));
                parameters.Add(new KeyValuePair<string, string>("type", "1"));

                var content = new FormUrlEncodedContent(parameters);
                httpRequestMessage.Content = content;*/

                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                ListInfringesEntity result = JsonConvert.DeserializeObject<ListInfringesEntity>(responseContent);


                if (result.success != null)
                {
                    total = result.success.data.total;
                    Paging();
                    List<DataEntity1> listAchievement = result.success.data.data;
                    foreach (var item in listAchievement)
                    {
                        item.infringe_at = ConvertDate(item.infringe_at, "dd/MM/yyyy");

                    }
                    icPersonalViolationsPage.ItemsSource = listAchievement;
                    icPersonalViolationsPage.Items.Refresh();
                }
            }
            catch (Exception)
            {
                //CustomMessageBox.Show("hihi");
            }

        }
        private void NavigateToPage(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;
            string name = textBlock.Name;
            switch (name)
            {
                case "Personal":
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(HomeView))
                        {
                            (window as HomeView).MainContent.Navigate(new PersonalViolationsPage(token,comid, TypeUser, PerAdd, PerEdit, PerDel));
                        }
                    };
                    break;
                case "Group":
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(HomeView))
                        {
                            (window as HomeView).MainContent.Navigate(new GroupViolationsPage(token,comid, TypeUser, PerAdd, PerEdit, PerDel));
                        }
                    };
                    break;
                case "List":
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(HomeView))
                        {
                            (window as HomeView).MainContent.Navigate(new ListViolationsPage(token,comid, TypeUser, PerAdd, PerEdit, PerDel));
                        }
                    };
                    break;
            }
        }

        private void Paging()
        {
            if (total == 0)
            {
                total_page = 1;
            }
            else
            {
                if (total % COUNT_RECORD_PER_PAGE == 0)
                {
                    total_page = total / COUNT_RECORD_PER_PAGE;
                    TotalPage.Text = total_page.ToString();
                }
                else
                {
                    total_page = total / COUNT_RECORD_PER_PAGE + 1;
                    TotalPage.Text = total_page.ToString();
                }
            }

            IsSetValidBtn();

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
            InvalidBtn();
            int page_next = page_now + 1;
            PageNow = page_next.ToString();
            GetData();
        }

        private void PrevPage(object sender, MouseButtonEventArgs e)
        {
            InvalidBtn();
            int page_prev = page_now - 1;
            PageNow = page_prev.ToString();
            GetData();
        }

        private void InvalidBtn()
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
        //Show popup thêm tvi phạm
        private void ShowPopupAddViolation(object sender, MouseButtonEventArgs e)
        {
            PersonalAddViolationPopup addListAchievementsPopup = new PersonalAddViolationPopup(token,comid,TypeUser);
            onShowPopup(addListAchievementsPopup);
        }
        private void ShowPopupEditViolation(object sender, MouseButtonEventArgs e)
        {
            Grid grid = sender as Grid;
            DataEntity1 dataEntity = (DataEntity1)grid.DataContext;

            string id = dataEntity.id;
            string infringe_name = dataEntity.infringe_name;
            string regulatory_basis = dataEntity.regulatory_basis;
            string number_violation = dataEntity.number_violation;
            string infringe_at = dataEntity.infringe_at;
            string infringe_type = dataEntity.infringe_type;
            string created_by = dataEntity.created_by;
            string list_user_name = dataEntity.list_user_name;
            string list_user = dataEntity.list_user;
            PersonalEditViolationPopup personalEditViolationPopup = new PersonalEditViolationPopup(token, id, infringe_name, regulatory_basis, number_violation, infringe_at, infringe_type, created_by, list_user, list_user_name);
            onShowPopup(personalEditViolationPopup);
        }
        private void HidePopup(int mode)
        {
            if (mode == 1)
            {
                GetData();
            }
            onShowPopup("");
        }

        private void scroll1_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                scroll1.ScrollToVerticalOffset(scroll1.VerticalOffset);
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta);
            }
            else
                scroll1.ScrollToVerticalOffset(scroll1.VerticalOffset - e.Delta);
        }

        private void ClickSearch(object sender, MouseButtonEventArgs e)
        {
            PageNow = "1";
            GetData();
        }

        private string ConvertDate(string date, string format)
        {
            DateTime myDate;
            try
            {
                myDate = DateTime.ParseExact(date, "yyyy-MM-ddTHH:mm",
                                              System.Globalization.CultureInfo.InvariantCulture);
                return myDate.ToString(format);
            }
            catch
            {
                try
                {
                    myDate = DateTime.ParseExact(date, "yyyy-MM-dd",
                                                  System.Globalization.CultureInfo.InvariantCulture);
                    return myDate.ToString(format);
                }
                catch
                {
                    try
                    {
                        myDate = DateTime.ParseExact(date, "yyyy/MM/dd",
                                                      System.Globalization.CultureInfo.InvariantCulture);
                        return myDate.ToString(format);
                    }
                    catch
                    {
                        try
                        {
                            myDate = DateTime.ParseExact(date, "dd/MM/yyyy",
                                                          System.Globalization.CultureInfo.InvariantCulture);
                            return myDate.ToString(format);
                        }
                        catch
                        {
                            try
                            {
                                myDate = DateTime.ParseExact(date, "MM/dd/yyyy",
                                                              System.Globalization.CultureInfo.InvariantCulture);
                                return myDate.ToString(format);
                            }
                            catch
                            {
                                try
                                {
                                    myDate = DateTime.ParseExact(date, "M/d/yyyy",
                                                                  System.Globalization.CultureInfo.InvariantCulture);
                                    return myDate.ToString(format);
                                }
                                catch
                                {
                                    return "";
                                }
                            }
                        }
                    }
                }
            }

        }
    }
}
