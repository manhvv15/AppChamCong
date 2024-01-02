﻿using QuanLyChung365TruocDangNhap.ChamCongNew.Entities;
using QuanLyChung365TruocDangNhap.ChamCongNew.Entities.LoginEntity;

using QuanLyChung365TruocDangNhap.ChamCongNew.Popups.RecruitmentManagementPopups.ListOfCandidatesPopups.CandidateDetailPopups;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Components
{
    /// <summary>
    /// Interaction logic for Header.xaml
    /// </summary>
    //public partial class Header : UserControl
    //{
    //    LoginEmployeeEntity loginEmployeeEntity;
    //    LoginCompanyEntity loginCompanyEntity;
    //    public int mode; // 1 là nhân viên, 2 là cty
        //public Header()
        //{
        //    InitializeComponent();
        //}

        //public Header(LoginEmployeeEntity loginEmployeeEntity)
        //{
        //    InitializeComponent();
        //    tbxName.Text = loginEmployeeEntity.data.user_info.ep_name;
        //    tbName.Text = loginEmployeeEntity.data.user_info.ep_name;
        //    string id = loginEmployeeEntity.data.user_info.ep_id;
        //    tbID.Text = "ID-" + id;
        //    string uri_avatar = loginEmployeeEntity.data.user_info.ep_image;
        //    if (uri_avatar != "")
        //    {
        //        imageAvatar.ImageSource = new BitmapImage(new Uri(APIs.API.avatar_uri + uri_avatar));
        //    }
        //    this.loginEmployeeEntity = loginEmployeeEntity;

        //    mode = 1;
        //}

        //public Header(LoginCompanyEntity loginCompanyEntity)
        //{
        //    InitializeComponent();
        //    tbxName.Text = loginCompanyEntity.data.user_info.com_name;
        //    tbName.Text = loginCompanyEntity.data.user_info.com_name;
        //    string id = loginCompanyEntity.data.user_info.com_id;
        //    tbID.Text = "ID-" + id;
        //    string uri_avatar = loginCompanyEntity.data.user_info.com_logo;
        //    if (uri_avatar != "")
        //    {
        //        imageAvatar.ImageSource = new BitmapImage(new Uri(APIs.API.logo_uri + uri_avatar));
        //    }
        //    this.loginCompanyEntity = loginCompanyEntity;

        //    mode = 2;
        //}

        //private void HeaderSizeChanged(object sender, SizeChangedEventArgs e)
        //{
        //    if (header.ActualWidth < 1058)
        //    {
        //        tbxName.Visibility = Visibility.Collapsed;
        //    }
        //    else
        //    {
        //        tbxName.Visibility = Visibility.Visible;
        //    }
        //}

        //private void MessageIconClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (gridMessagePopup.Visibility == Visibility.Collapsed)
        //    {
        //        HideAllPopup();


        //        gridMessagePopup.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        gridMessagePopup.Children.RemoveAt(0);
        //        gridMessagePopup.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void RemindIconClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (gridRemindPopup.Visibility == Visibility.Collapsed)
        //    {
        //        HideAllPopup();

        //        gridRemindPopup.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        gridRemindPopup.Children.RemoveAt(0);
        //        gridRemindPopup.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void NotifyIconClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (gridNotifyPopup.Visibility == Visibility.Collapsed)
        //    {
        //        HideAllPopup();


        //        gridNotifyPopup.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        gridNotifyPopup.Children.RemoveAt(0);
        //        gridNotifyPopup.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void MenuClick(object sender, MouseButtonEventArgs e)
        //{
        //    if (gridMenuPopup.Visibility == Visibility.Collapsed)
        //    {
        //        HideAllPopup();

        //        gridMenuPopup.Visibility = Visibility.Visible;
        //    }
        //    else
        //    {
        //        gridMenuPopup.Children.RemoveAt(0);
        //        gridMenuPopup.Visibility = Visibility.Collapsed;
        //    }
        //}

        //private void HideAllPopup()
        //{
        //    if (gridMenuPopup.Visibility == Visibility.Visible)
        //    {
        //        gridMenuPopup.Children.RemoveAt(0);
        //        gridMenuPopup.Visibility = Visibility.Collapsed;
        //    }

        //    if (gridMessagePopup.Visibility == Visibility.Visible)
        //    {
        //        gridMessagePopup.Children.RemoveAt(0);
        //        gridMessagePopup.Visibility = Visibility.Collapsed;
        //    }

        //    if (gridNotifyPopup.Visibility == Visibility.Visible)
        //    {
        //        gridNotifyPopup.Children.RemoveAt(0);
        //        gridNotifyPopup.Visibility = Visibility.Collapsed;
        //    }

        //    if (gridRemindPopup.Visibility == Visibility.Visible)
        //    {
        //        gridRemindPopup.Children.RemoveAt(0);
        //        gridRemindPopup.Visibility = Visibility.Collapsed;
        //    }
        //}
    //}
}
