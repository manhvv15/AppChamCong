﻿using QuanLyChung365TruocDangNhap.Hr.Entities.AdministrationEntity.PersonnelChangeEntity;
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

namespace QuanLyChung365TruocDangNhap.Hr.Pages.OrganizationalChart.ListDepPage
{
    /// <summary>
    /// Interaction logic for ListDepTimeKeep.xaml
    /// </summary>
    public partial class ListDepTimeKeep : Page, INotifyPropertyChanged
    {
        string token;
        string id;
        string dep_id;
        string dep_name;

        const int COUNT_RECORD_PER_PAGE2 = 10;
        int page_now2 = 1;
        int total2;
        int total_page2 = 1;
        public string PageNow2
        {
            get { return page_now2.ToString(); }
            set
            {
                page_now2 = Convert.ToInt32(value);
                OnPropertyChanged("PageNow2");
            }
        }

        // deligate event show popups
        public delegate void ShowPopup(object obj);
        public static event ShowPopup onShowPopup;


        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string newName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(newName));
            }
        }

        public ListDepTimeKeep(string token,string id,string dep_id,string dep_name)
        {
            InitializeComponent();
            this.token = token;
            this.id = id;
            this.dep_id = dep_id;
            this.dep_name = dep_name;
            this.DataContext = this;
            txtDepname.Text = dep_name;
            TotalPage.Text = total_page2.ToString();
            GetData();
        }

        private void NavigateBack(object sender, MouseButtonEventArgs e)
        {
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(HomeView))
                {
                    (window as HomeView).MainContent.NavigationService.GoBack();
                }
            }
        }
        //get list employee

        private async void GetData()
        {
            try
            {
                var httpClient = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage();

                httpRequestMessage.Method = HttpMethod.Post;
                string api = "http://210.245.108.202:3006/api/hr/organizationalStructure/listEmUntimed";

                httpRequestMessage.RequestUri = new Uri(api);
                httpRequestMessage.Headers.Add("Authorization", "Bearer " + token);
                var form = new Dictionary<string, string>();
                form.Add("com_id", id);
                form.Add("dep_id", dep_id);
                form.Add("page", page_now2.ToString());
                form.Add("pageSize", COUNT_RECORD_PER_PAGE2.ToString());
                form.Add("type_timekeep", "1");
                form.Add("winform", "1");
                httpRequestMessage.Content = new FormUrlEncodedContent(form);
                var response = await httpClient.SendAsync(httpRequestMessage);
                var responseContent = await response.Content.ReadAsStringAsync();

                getListEmployee result = JsonConvert.DeserializeObject<getListEmployee>(responseContent);
                if (result.data != null)
                {
                    total2 = result.data.totalItems;
                    Paging2();
                    List<Items3> ListEmployee = result.data.items;
                    foreach (var item in ListEmployee)
                    {
                        if (item.dep_name == null)
                        {
                            item.dep_name = "Chưa cập nhật";
                        }
                        item.ep_birth_day = Convert.ToDateTime(item.ep_birth_day).ToString("dd-MM-yyyy");
                    }
                    icListEmployee.Items.Refresh();
                    icListEmployee.ItemsSource = ListEmployee;
                }
            }
            catch (Exception)
            {
                CustomMessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!");
            }
        }
        //end get list employee
        private void NextPage2(object sender, MouseButtonEventArgs e)
        {
            InvalidBtn2();
            int page_next = page_now2 + 1;
            PageNow2 = page_next.ToString();
            GetData();
        }

        private void PrevPage2(object sender, MouseButtonEventArgs e)
        {
            InvalidBtn2();
            int page_prev = page_now2 - 1;
            PageNow2 = page_prev.ToString();
            GetData();
        }

        private void InvalidBtn2()
        {
            btnPrev2.IsEnabled = false;
            btnPrev2.Opacity = 0.3;
            btnNext2.IsEnabled = false;
            btnNext2.Opacity = 0.3;
        }

        private void Paging2()
        {
            if (total2 == 0)
            {
                total_page2 = 1;
            }
            else
            {
                if (total2 % COUNT_RECORD_PER_PAGE2 == 0)
                {
                    total_page2 = total2 / COUNT_RECORD_PER_PAGE2;
                    TotalPage.Text = total_page2.ToString();
                }
                else
                {
                    total_page2 = total2 / COUNT_RECORD_PER_PAGE2 + 1;
                    TotalPage.Text = total_page2.ToString();
                }
            }
            IsSetValidBtn2();
        }

        private void IsSetValidBtn2()
        {
            if (page_now2 == 1)
            {
                btnPrev2.IsEnabled = false;
                btnPrev2.Opacity = 0.3;
            }
            else
            {
                btnPrev2.IsEnabled = true;
                btnPrev2.Opacity = 1;
            }

            if (page_now2 == total_page2)
            {
                btnNext2.IsEnabled = false;
                btnNext2.Opacity = 0.3;
            }
            else
            {
                btnNext2.IsEnabled = true;
                btnNext2.Opacity = 1;
            }
        }

        private void scroll_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftShift) || Keyboard.IsKeyDown(Key.RightShift))
            {
                scData.ScrollToVerticalOffset(scData.VerticalOffset);
                scroll.ScrollToHorizontalOffset(scroll.HorizontalOffset - e.Delta);
            }
        }
    }
}
