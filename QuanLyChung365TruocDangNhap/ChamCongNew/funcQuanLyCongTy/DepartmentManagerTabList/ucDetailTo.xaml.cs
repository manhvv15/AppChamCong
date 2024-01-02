﻿using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager.DepartmentManagerPopups;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.PopupSalarySettings;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.DepartmentManagerTabList
{

    public partial class ucDetailTo : UserControl
    {
        MainWindow Main;
        List<Employee> EmployeeList = new List<Employee>();
        List<Position> PositionList = new List<Position>();
        string team_id = "";
        public ucDetailTo(MainWindow main, int team_id)
        {
            this.Main = main;
            InitializeComponent();
            this.team_id = team_id.ToString();
            LoadListEmployeeInGroup();
        }

        public async void LoadListEmployeeInGroup()
        {
            //load employee
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.managerUser_list_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();

                content.Add(new StringContent(team_id), "team_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    EmployeeRoot result = JsonConvert.DeserializeObject<EmployeeRoot>(responseContent);
                    if (result.data.items.Count > 0)
                    {
                        EmployeeList = result.data.items;
                    }
                }



            }
            catch
            {
                MessageBox.Show("Có lỗi khi lấy danh sách nhân viên");
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

            //load datagrid
            var query = from e in EmployeeList
                        join p in PositionList on e.position_id equals p.positionId
                        select new
                        {
                            ep_id = e.position_id,
                            ep_image = API.img_source_api + e.ep_image,
                            ep_name = e.ep_name,
                            position_name = p.positionName,
                            start_working_time = JavaTimeStampToDateTime((double)e.start_working_time)

                        };
            var listEP = query.ToList();
            dsStaff.ItemsSource = query.ToList();
        }

        public static DateTime JavaTimeStampToDateTime(double javaTimeStamp)
        {
            // Java timestamp is milliseconds past epoch
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddMilliseconds(javaTimeStamp).ToLocalTime();
            return dateTime;
        }

        private void Delete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDeleteStaff());
        }

        private void bodThemVaoNhom_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucAddSaffFToGroup(Main));

        }
    }
}