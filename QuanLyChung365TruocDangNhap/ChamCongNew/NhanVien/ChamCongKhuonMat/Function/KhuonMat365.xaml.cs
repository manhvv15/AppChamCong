﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.ChamCongKhuonMat.Function
{
    /// <summary>
    /// Interaction logic for KhuonMat365.xaml
    /// </summary>
    public partial class KhuonMat365 : Window
    {
        MainChamCong Main;
        public KhuonMat365(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Border_MouseUp(object sender, MouseButtonEventArgs e)
        {
            KhuonMat365 uc = new KhuonMat365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            KhuonMatChat365 uc = new KhuonMatChat365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }

        private void Border_MouseUp_2(object sender, MouseButtonEventArgs e)
        {
            KhuonMatPC365 uc = new KhuonMatPC365(Main);
            Main.dopBody.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            Main.dopBody.Children.Add(Content as UIElement);
        }
        void InstructionImageCollapsedAll()
        {
            DienThoai12.Visibility = Visibility.Collapsed;
            DienThoai13.Visibility = Visibility.Collapsed;
            DienThoai11.Visibility = Visibility.Collapsed;
        }
        private void Buoc11Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {


            InstructionImageCollapsedAll();
            DienThoai11.Visibility = Visibility.Visible;
        }

        private void Buoc12Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            InstructionImageCollapsedAll();
            Buoc12Hien.Visibility = Visibility.Collapsed;
            Buoc12An.Visibility = Visibility.Visible;
            DienThoai12.Visibility = Visibility.Visible;
        }


        private void Buoc13Hien_MouseUp(object sender, MouseButtonEventArgs e)
        {
            InstructionImageCollapsedAll();
            Buoc13Hien.Visibility = Visibility.Collapsed;
            Buoc13An.Visibility = Visibility.Visible;

            DienThoai13.Visibility = Visibility.Visible;
        }
    }
}
