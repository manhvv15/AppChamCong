﻿using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
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
    /// Interaction logic for ucUpdateNhom.xaml
    /// </summary>
    public partial class ucUpdateNhom : UserControl
    {
        ucListGroup ucListGroup;
        BrushConverter brus = new BrushConverter();
        string com_id = "";
        string dep_id = "";
        string team_id = "";
        string gr_id = "";
        public ucUpdateNhom(ucListGroup ucListGroup, Group group)
        {
            InitializeComponent();
            this.ucListGroup = ucListGroup;
            lsvCompany.ItemsSource = ucListGroup.dataCompanySelectBox;
            lsvDepartment.ItemsSource = ucListGroup.DepartmentList;
            lsvTo.ItemsSource = ucListGroup.TeamList;
            this.com_id = ucListGroup.Main.IdAcount.ToString();
            dep_id = group.dep_id.ToString();
            team_id = group.team_id.ToString();
            gr_id = group.gr_id.ToString(); 
            txbSelectDepartment.Text = group.dep_name;
            txbSelectTo.Text = group.team_name;
            txtGroupName.Text = group.gr_name;
        }

        private async void UpdateGroup()
        {
            try
            {
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, API.edit_group_api);
                request.Headers.Add("authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(com_id), "com_id");
                content.Add(new StringContent(txtGroupName.Text), "gr_name");
                content.Add(new StringContent(gr_id), "gr_id");
                content.Add(new StringContent(dep_id), "dep_id");
                content.Add(new StringContent(team_id), "team_id");
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.IsSuccessStatusCode)
                {
                    this.ucListGroup.Main.grShowPopup.Children.Add(new ucUpdateSuccess());
                    this.Visibility = Visibility.Collapsed;
                    this.ucListGroup.LoadListGroup();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi sửa nhóm");
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
                txbSelectCompany.Text = selectedItem.com_name;
                com_id = selectedItem.com_id.ToString();
                txbSelectCompany.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListCompanyCollapsed.Visibility = Visibility.Collapsed;
            }

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
                txbSelectDepartment.Text = selectedItem.dep_name;
                dep_id = selectedItem.dep_id.ToString();
                txbSelectDepartment.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
            }

        }

        //dropdown box select to
        private void bodSelectTo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
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

        private void lsvTo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            var selectedItem = (Team)lsvTo.SelectedItem;
            if (selectedItem != null)
            {
                txbSelectTo.Text = selectedItem.team_name;
                team_id = selectedItem.team_id.ToString();
                txbSelectTo.Foreground = (Brush)brus.ConvertFromString("#474747");
                bodListToCollapsed.Visibility = Visibility.Collapsed;
            }

        }



        private void SelectPopUpClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            bodListCompanyCollapsed.Visibility = Visibility.Collapsed;
            bodListDepartmentCollapsed.Visibility = Visibility.Collapsed;
            bodListToCollapsed.Visibility = Visibility.Collapsed;

        }

        private void OK_MouseUp(object sender, MouseButtonEventArgs e)
        {
            UpdateGroup();
        }
    }
}
