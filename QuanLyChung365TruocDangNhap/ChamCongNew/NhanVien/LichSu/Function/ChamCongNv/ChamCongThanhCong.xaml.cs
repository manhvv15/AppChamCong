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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.LichSu.Function
{
    /// <summary>
    /// Interaction logic for ChamCongThanhCong.xaml
    /// </summary>
    public partial class ChamCongThanhCong : UserControl
    {
        MainChamCong Main;
        public ChamCongThanhCong(MainChamCong main)
        {
            InitializeComponent();
            Main = main;
        }
    }
}