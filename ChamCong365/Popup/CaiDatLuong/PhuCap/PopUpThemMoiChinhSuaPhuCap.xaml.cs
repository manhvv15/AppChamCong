﻿using ChamCong365.SalarySettings;
using Newtonsoft.Json;
using RestSharp;
using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChamCong365.Popup.CaiDatLuong.PhuCap
{
    /// <summary>
    /// Interaction logic for PopUpThemMoiChinhSuaPhuCap.xaml
    /// </summary>
    public partial class PopUpThemMoiChinhSuaPhuCap : UserControl
    {
        private string Month;
        private MainWindow Main;
        private frmDanhSachPhuCap frmDSPC;
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa phucap = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa();
        public PopUpThemMoiChinhSuaPhuCap(MainWindow main, string TieuDe, frmDanhSachPhuCap pc)
        {
            InitializeComponent();
            Main = main;
            frmDSPC = pc;
            textTieuDe.Text = TieuDe;
            textNamTruocBD.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiBD.Text = DateTime.Now.Year.ToString();
            textNamTruocKT.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiKT.Text = DateTime.Now.Year.ToString();
            
        }
        public PopUpThemMoiChinhSuaPhuCap(MainWindow main, string TieuDe, frmDanhSachPhuCap pc, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa phuc)
        {
            InitializeComponent();
            Main = main;
            frmDSPC = pc;
            textTieuDe.Text = TieuDe;
            phucap = phuc;
            textTenPC.Text = phuc.cl_name;
            textTienPC.Text = phuc.cl_salary.ToString();
            cboTypeTN.Text = phuc.cl_type_tax_s;
            string[] dtp = phuc.cl_day.ToString().Split(Convert.ToChar("/"));
            string[] dtpY = dtp[2].Split(new string[] { "12:00:00 AM" }, StringSplitOptions.None);
            string Y = dtpY[0].Trim();
            textHienThiTGBatDau.Text = dtp[0] + "/" + Y;
            string[] dtpkt = phuc.cl_day_end.ToString().Split(Convert.ToChar("/"));
            string[] dtpYkt = dtpkt[2].Split(new string[] { "12:00:00 AM" }, StringSplitOptions.None);
            string Ykt = dtpYkt[0].Trim();
            textHienThiTGKetThuc.Text = dtpkt[0] + "/" + Ykt;
            textGhiChu.Text = phuc.cl_note;
            textTMCS.Text = "Cập nhật";
            textNamTruocBD.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiBD.Text = DateTime.Now.Year.ToString();
            textNamTruocKT.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiKT.Text = DateTime.Now.Year.ToString();

        }
        private void borTGKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNamKT.Visibility == Visibility.Visible)
            {
                borNamKT.Visibility = Visibility.Collapsed;
                closePopup.Visibility = Visibility.Collapsed;
            }
            else
            {
                borNamKT.Visibility = Visibility.Visible;
                closePopup.Visibility = Visibility.Visible;
            }
        }

        private void borTGBatDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNamBatDauAD.Visibility == Visibility.Visible)
            {
                borNamBatDauAD.Visibility = Visibility.Collapsed;
                closePopup.Visibility = Visibility.Collapsed;
            }
            else
            {
                borNamBatDauAD.Visibility = Visibility.Visible;
                closePopup.Visibility = Visibility.Visible;
            }
        }

        private void closePopup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void borNamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocKT.Visibility = Visibility.Visible;
            DSThangNamSauKT.Visibility = Visibility.Collapsed;
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
        }

        private void borNamHienTaiKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocKT.Visibility = Visibility.Collapsed;
            DSThangNamSauKT.Visibility = Visibility.Visible;
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
        }


        private void NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocBatDau.Visibility = Visibility.Visible;
            DSThangNamSauBatDau.Visibility = Visibility.Collapsed;
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
        }

        private void NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocBatDau.Visibility = Visibility.Collapsed;
            DSThangNamSauBatDau.Visibility = Visibility.Visible;
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
        }

        private void Thang1NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void Thang1NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void btnChonThangNayNamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGBatDau.Text = Month + "/" + textNamHienTaiBD.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;

        }

        private void btnChonThangNayNamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGBatDau.Text = Month + "/" + textNamTruocBD.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void Thang1NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void Thang1NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void textThangNayNamHienTaiKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGKetThuc.Text = Month + "/" + textNamHienTaiKT.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void textHienThiThangNayNamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGKetThuc.Text = Month + "/" + textNamTruocKT.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnThemMoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnThemMoiChinhSua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(textTieuDe.Text=="Thêm mới phụ cấp")
            {
                try
                {
                    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_phuc_loi")))
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("cl_com", Main.IdAcount);
                        request.AddParameter("cl_name", textTenPC.Text);
                        request.AddParameter("cl_salary", textTienPC.Text);
                        string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                        string y = day[day.Length - 1];
                        string[] day1 = day[0].Split(new[] { "Tháng" }, StringSplitOptions.None);
                        string m = day1[day1.Length - 1].Trim();
                        if (int.Parse(m) < 10)
                        {
                            request.AddParameter("cl_day", y + "-" + "0" + m + "-" + "01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("cl_day", y + "-" + m + "-" + "01T00:00:00.000+00:00");
                        }
                        string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                        string ykt = daykt[daykt.Length - 1];
                        string[] day1kt = daykt[0].Split(new[] { "Tháng" }, StringSplitOptions.None);
                        string mkt = day1kt[day1kt.Length - 1].Trim();
                        if (int.Parse(mkt) < 10)
                        {
                            request.AddParameter("cl_day_end", ykt + "-" + "0" + mkt + "-" + "01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("cl_day_end", ykt + "-" + mkt + "-" + "01T00:00:00.000+00:00");
                        }
                        request.AddParameter("cl_active", 1);
                        request.AddParameter("cl_note", textGhiChu.Text);
                        request.AddParameter("cl_type", 4);
                        if (cboTypeTN.Text == "Thu nhập chịu thuế")
                        {
                            request.AddParameter("cl_type_tax", 0);

                        }
                        else if (cboTypeTN.Text == "Thu nhập miễn thuế")
                        {
                            request.AddParameter("cl_type_tax", 1);
                        }
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;
                        OOP.CaiDatLuong.PhucLoi.AddPhucLoi.Root PhucLoi = JsonConvert.DeserializeObject<OOP.CaiDatLuong.PhucLoi.AddPhucLoi.Root>(b);
                        if (PhucLoi.newobj != null)
                        {
                            OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa pv = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa();
                            pv._id = PhucLoi.newobj._id;
                            pv.cl_id = PhucLoi.newobj.cl_id;
                            pv.cl_name = PhucLoi.newobj.cl_name;
                            pv.cl_salary = PhucLoi.newobj.cl_salary;
                            pv.cl_day = PhucLoi.newobj.cl_day;
                            pv.cl_day_end = PhucLoi.newobj.cl_day_end;
                            pv.cl_active = PhucLoi.newobj.cl_active.ToString();
                            pv.cl_note = PhucLoi.newobj.cl_note;
                            pv.cl_type = PhucLoi.newobj.cl_type;
                            pv.cl_type_tax = PhucLoi.newobj.cl_type_tax;
                            if (PhucLoi.newobj.cl_type_tax == 0)
                            {
                                pv.cl_type_tax_s = "Thu nhập chịu thuế";
                            }
                            else if (PhucLoi.newobj.cl_type_tax == 1)
                            {
                                pv.cl_type_tax_s = "Thu nhập miễn thuế";
                            }
                            pv.cl_com = PhucLoi.newobj.cl_com;
                            frmDSPC.lstPC.Add(pv);
                            frmDSPC.dgv.ItemsSource = null;
                            frmDSPC.dgv.ItemsSource = frmDSPC.lstPC;
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                }
                catch
                {

                }
            }
            else if(textTieuDe.Text=="Chỉnh sửa phụ cấp")
            {
                try
                {
                    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/sua_phuc_loi")))
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("cl_id", phucap.cl_id);
                        request.AddParameter("cl_name", textTenPC.Text);
                        request.AddParameter("cl_salary", textTienPC.Text);
                        string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                        string y = day[day.Length - 1];
                        string[] day1 = day[0].Split(new[] { "Tháng" }, StringSplitOptions.None);
                        string m = day1[day1.Length - 1].Trim();
                        if (int.Parse(m) < 10)
                        {
                            request.AddParameter("cl_day", y + "-" + "0" + m + "-" + "01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("cl_day", y + "-" + m + "-" + "01T00:00:00.000+00:00");
                        }
                        string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                        string ykt = daykt[daykt.Length - 1];
                        string[] day1kt = daykt[0].Split(new[] { "Tháng" }, StringSplitOptions.None);
                        string mkt = day1kt[day1kt.Length - 1].Trim();
                        if (int.Parse(mkt) < 10)
                        {
                            request.AddParameter("cl_day_end", ykt + "-" + "0" + mkt + "-" + "01T00:00:00.000+00:00");
                        }
                        else
                        {
                            request.AddParameter("cl_day_end", ykt + "-" + mkt + "-" + "01T00:00:00.000+00:00");
                        }
                        request.AddParameter("cl_active", 1);
                        request.AddParameter("cl_note", textGhiChu.Text);
                        request.AddParameter("cl_type", 4);
                        if (cboTypeTN.Text == "Thu nhập chịu thuế")
                        {
                            request.AddParameter("cl_type_tax", 0);

                        }
                        else if (cboTypeTN.Text == "Thu nhập miễn thuế")
                        {
                            request.AddParameter("cl_type_tax", 1);
                        }
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = restclient.Execute(request);
                        var b = resAlbum.Content;
                        foreach (var item in frmDSPC.lstPC)
                        {
                            if (item.cl_id == phucap.cl_id)
                            {
                                item._id = phucap._id;

                                item.cl_name = textTenPC.Text;
                                item.cl_salary = int.Parse(textTienPC.Text);
                                item.cl_day = Convert.ToDateTime(m + "/" + y);
                                item.cl_day_end = Convert.ToDateTime(mkt + "/" + ykt);
                                item.cl_active = "1";
                                item.cl_note = textGhiChu.Text;
                                item.cl_type = 3;
                                if (cboTypeTN.Text == "Thu nhập chịu thuế")
                                {
                                    item.cl_type_tax = 0;

                                }
                                else if (cboTypeTN.Text == "Thu nhập miễn thuế")
                                {
                                    item.cl_type_tax = 1;
                                }
                                item.cl_type_tax_s = cboTypeTN.Text;
                            }
                        }
                        frmDSPC.dgv.ItemsSource = null;
                        frmDSPC.dgv.ItemsSource = frmDSPC.lstPC;
                        this.Visibility = Visibility.Collapsed;


                    }
                }
                catch
                {

                }
            }
        }
    }
}
