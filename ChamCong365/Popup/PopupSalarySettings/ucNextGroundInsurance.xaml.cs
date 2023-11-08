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

namespace ChamCong365.Popup.PopupSalarySettings
{
    /// <summary>
    /// Interaction logic for ucNextGroundInsurance.xaml
    /// </summary>
    public partial class ucNextGroundInsurance : UserControl
    {
        MainWindow Main;
        public ucNextGroundInsurance(MainWindow main)
        {
            InitializeComponent();
            Main = main;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ExitNextGround_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }
    }
}
