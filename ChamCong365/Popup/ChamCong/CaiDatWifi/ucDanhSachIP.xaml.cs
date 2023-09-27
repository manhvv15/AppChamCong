﻿using ChamCong365.Popup.ChamCong.CaiDatWifi;
using ChamCong365.TimeKeeping;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.ChamCong.CaiDatWifi
{
    /// <summary>
    /// Interaction logic for ucListIP.xaml
    /// </summary>
    public partial class ucDanhSachIP : UserControl
    {
        private MainWindow Main;
        BrushConverter bcWifi = new BrushConverter();
        List<Wifi> itemsWifi = new List<Wifi>();
        public ucDanhSachIP(MainWindow main)
        {
            InitializeComponent();
            #region FakeWifi
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 1", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 2", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 3", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 4", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 5", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 6", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 7", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 8", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 9", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 10", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 11", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 12", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 13", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 14", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 15", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 16", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 17", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 18", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 19", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            itemsWifi.Add(new Wifi() { NameWifi = "John Doe 20", AddressWifi = "Triều Khúc", AddressIP = "192.168.1.1", UpdateWifi = DateTime.Now });
            
            lsvLoadIP.ItemsSource = itemsWifi;
            #endregion
            Main = main;
        }

        private void borAddIp_MouseEnter(object sender, MouseEventArgs e)
        {
            borAddIp.BorderThickness = new Thickness(1);
        }

        private void borAddIp_MouseLeave(object sender, MouseEventArgs e)
        {
            borAddIp.BorderThickness = new Thickness(0);
        }

        private void borAddIp_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThemDiaChiIP());
        }

        private void Border_MouseLeftButtonUp_UpdateIP(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCapNhatWifi());
        }

        private void Border_MouseLeftButtonUp_DeleteIP(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDelete());
        }
    }
}
