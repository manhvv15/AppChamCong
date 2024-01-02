﻿using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.funcCompanyManager
{
    /// <summary>
    /// Interaction logic for ucUpdateDepartment.xaml
    /// </summary>
    public partial class ucUpdateDepartment : UserControl
    {
        private ucListDpartment ucListDpartment;
        BrushConverter bc = new BrushConverter();
        Department department;
        private string com_id = "";
        public ucUpdateDepartment(ucListDpartment ucListDpartment, Department department)
        {
            InitializeComponent();
            this.ucListDpartment = ucListDpartment;

            this.department = department;
            txtDepartmentName.Text = department.dep_name;
            this.com_id = department.com_id.ToString();
            lsvCompany.ItemsSource = ucListDpartment.dataCompanySelectBox;
            //GetListChildCompany();
        }
        private async void UpdateDepartment()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.edit_department_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                content.Add(new StringContent(txtDepartmentName.Text), "dep_name");
                content.Add(new StringContent(department.dep_id.ToString()), "dep_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    this.Visibility = Visibility.Collapsed;
                    MessageBox.Show("Sửa thành công");

                    ucListDpartment.LoadListDepartment();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi khi sửa phòng ban");
            }
        }

        //get danh sách tên công ty con đổ vào dropdown box chọn công ty
        private async void GetListChildCompany()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.list_ChildCompany_api);
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var response = await client.SendAsync(request);
                var responseContent = await response.Content.ReadAsStringAsync();

                CompanyRoot result = JsonConvert.DeserializeObject<CompanyRoot>(responseContent);

                lsvCompany.ItemsSource = result.data.items;

            }
            catch (Exception e)
            {
                MessageBox.Show("Đã xảy ra lỗi,vui lòng kiểm tra lại!" + e.Message);
            }
        }
        private void bodExitPopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodSelectCompany_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (bodListCompany.Visibility == Visibility.Collapsed)
            {
                bodListCompany.Visibility = Visibility.Visible;
            }
            else
            {
                bodListCompany.Visibility -= Visibility.Collapsed;

            }
        }

        private void lsvCompany_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = lsvCompany.SelectedItem as Company;
            if (selectedItem != null)
            {
                com_id = selectedItem.com_id.ToString();
                txbSelectCompany.Text = selectedItem.com_name;
                txbSelectCompany.Foreground = (Brush)bc.ConvertFromString("#474747");
                bodListCompany.Visibility = Visibility.Collapsed;
            }
        }

        private void Update_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateDepartment();
            ucListDpartment.LoadListDepartment();

        }
    }
}

