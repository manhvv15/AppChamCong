﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucDeleteInsuranceSaff.xaml
    /// </summary>
    public partial class ucXoaBaoHiemNhanVien : UserControl
    {
        BrushConverter br = new BrushConverter();
        public ucXoaBaoHiemNhanVien()
        {
            InitializeComponent();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCancel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }
  

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbTextCancel.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }
    }
}
