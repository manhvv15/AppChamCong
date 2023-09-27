using ChamCong365.TimeKeeping;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.PopupTimeKeeping
{
    /// <summary>
    /// Interaction logic for ucListWifi.xaml
    /// </summary>
    public partial class ucListWifi : UserControl
    {
        private MainWindow Main;
        BrushConverter bcWifi = new BrushConverter();
        List<Wifi> itemsWifi = new List<Wifi>();
        public ucListWifi(MainWindow main)
        {
            InitializeComponent();
            Main = main;
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
            lsvLoadWifi.ItemsSource = itemsWifi;
            
            #endregion
        }

        private void bodAddWifi_MouseEnter(object sender, MouseEventArgs e)
        {
            bodAddWifi.BorderThickness = new Thickness(1);
        }

        private void bodAddWifi_MouseLeave(object sender, MouseEventArgs e)
        {
            bodAddWifi.BorderThickness = new Thickness(0);
        }

        private void bodAddWifi_MouseUp_1(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucCreateWifi());
        }

        private void Border_MouseLeftButtonUp_UpdateWifi(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucUpdateWifi());
        }

        private void Border_MouseLeftButtonUp_DeleteWifi(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucDelete());
        }
    }
}
