﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace QuanLyChung365TruocDangNhap.RecommendSetting.Popup
{
    /// <summary>
    /// Interaction logic for Refresh.xaml
    /// </summary>
    public partial class Refresh : UserControl
    {
        frmMain Main;
        private DispatcherTimer timer;
        public Refresh(frmMain main)
        {
            InitializeComponent();
            Main = main;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += Timer_Tick;
            timer.Start();
        }
  
        private void Timer_Tick(object sender, EventArgs e)
        {
            refresh.Visibility = Visibility.Collapsed;
            timer.Stop();
        }
    }
}