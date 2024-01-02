using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.HoaHongNhanDuoc;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.HoaHong;
//using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings
{
    /// <summary>
    /// Interaction logic for ucHoaHong.xaml
    /// </summary>
    public partial class ucHoaHong : UserControl
    {
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        string Next1;
        string Next2;
        public ucHoaHong(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            btn_BorderHeader1.BorderThickness = new Thickness(0, 0, 0, 2);
            btn_BorderHeader1.BorderBrush = (Brush)br.ConvertFrom("#4c5bd4");
            tb_textHeader1.Foreground = (Brush)br.ConvertFrom("#4c5bd4");

            btn_BorderHeader2.Visibility = Visibility.Collapsed;

            ucCaiDatHoaHong uc = new ucCaiDatHoaHong(main);
            stp_ShowPopup.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stp_ShowPopup.Children.Add(Content as UIElement);
        }

        int Back;
        double LoiNhuan;
        decimal ViTri;
        uint KeHoach;
        public List<ListThietLap> lstRose2 = new List<ListThietLap>();

        public ucHoaHong(MainWindow main, int back, List<ListThietLap> lstrose2)
        {
            InitializeComponent();
            this.Main = main;
            lstRose2 = lstrose2;
            btn_BorderHeader1.BorderThickness = new Thickness(0, 0, 0, 2);
            btn_BorderHeader1.BorderBrush = (Brush)br.ConvertFrom("#4c5bd4");
            tb_textHeader1.Foreground = (Brush)br.ConvertFrom("#4c5bd4");
            tb_textHeader1.Text = "Hoa hồng cá nhân";
            tb_textHeader3.Text = "Tổng hoa hồng";
            tb_textHeader4.Text = "Hướng dẫn";

            btn_BorderHeader2.Visibility = Visibility.Collapsed;
            Back = back;
            if (back == 21)
            {
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(main, back);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (back == 22)
            {
                Next2 = "RoseMoney2";
                int CN = 1;
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(main, back, Next2, lstrose2, CN);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if(back == 23)
            {
                LoiNhuan = 23;
                btn_BorderHeader2.Visibility = Visibility.Collapsed;
                tb_textHeader2.Text = "Hoa hồng nhóm";
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(main, LoiNhuan, lstrose2);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (back == 24)
            {
                ViTri = 24;
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(main, ViTri, lstrose2);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (back == 25)
            {
                KeHoach = 25;
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(main, KeHoach, lstrose2);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
          
        }
        int CN;
        private void btn_BorderHeader1_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CN = 1;
            btn_BorderHeader1.BorderThickness = new Thickness(0, 0, 0, 2);
            btn_BorderHeader1.BorderBrush = (Brush)br.ConvertFrom("#4c5bd4");
            tb_textHeader1.Foreground = (Brush)br.ConvertFrom("#4c5bd4");

            btn_BorderHeader2.BorderThickness = new Thickness(0);
            btn_BorderHeader2.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader2.Foreground = (Brush)br.ConvertFrom("#474747");

            btn_BorderHeader3.BorderThickness = new Thickness(0);
            btn_BorderHeader3.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader3.Foreground = (Brush)br.ConvertFrom("#474747");

            btn_BorderHeader4.BorderThickness = new Thickness(0);
            btn_BorderHeader4.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader4.Foreground = (Brush)br.ConvertFrom("#474747");

            if (Back == 21)
            {
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, Back);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 22)
            {
                Next2 = "RoseMoney2";
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, Back, Next2, lstRose2, CN);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 23)
            {
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, LoiNhuan, lstRose2);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 24)
            {
                ViTri = 24;
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, ViTri, lstRose2);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 25)
            {
                KeHoach = 25;
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, KeHoach, lstRose2);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else
            {
                ucCaiDatHoaHong uc = new ucCaiDatHoaHong(Main);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
        }

        bool NhomLoiNhuan;
        private void btn_BorderHeader2_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btn_BorderHeader1.BorderThickness = new Thickness(0);
            btn_BorderHeader1.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader1.Foreground = (Brush)br.ConvertFrom("#474747");

            btn_BorderHeader2.BorderThickness = new Thickness(0, 0, 0, 2);
            btn_BorderHeader2.BorderBrush = (Brush)br.ConvertFrom("#4c5bd4");
            tb_textHeader2.Foreground = (Brush)br.ConvertFrom("#4c5bd4");

            btn_BorderHeader3.BorderThickness = new Thickness(0);
            btn_BorderHeader3.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader3.Foreground = (Brush)br.ConvertFrom("#474747");

            btn_BorderHeader4.BorderThickness = new Thickness(0);
            btn_BorderHeader4.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader4.Foreground = (Brush)br.ConvertFrom("#474747");
            NhomLoiNhuan = true;
            ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, NhomLoiNhuan);
            stp_ShowPopup.Children.Clear();
            object Content = uc.Content;
            uc.Content = null;
            stp_ShowPopup.Children.Add(Content as UIElement);
        }

        long HoaHongDoanhThu;
        float HoaHongLoiNhuan;
        short HoaHongViTri;
        int HoaHongKeHoach;
        private void btn_BorderHeader3_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CN = 2;
            btn_BorderHeader1.BorderThickness = new Thickness(0);
            btn_BorderHeader1.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader1.Foreground = (Brush)br.ConvertFrom("#474747");

            btn_BorderHeader2.BorderThickness = new Thickness(0);
            btn_BorderHeader2.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader2.Foreground = (Brush)br.ConvertFrom("#474747");

            btn_BorderHeader3.BorderThickness = new Thickness(0,0,0,2);
            btn_BorderHeader3.BorderBrush = (Brush)br.ConvertFrom("#4c5bd4");
            tb_textHeader3.Foreground = (Brush)br.ConvertFrom("#4c5bd4");

            btn_BorderHeader4.BorderThickness = new Thickness(0);
            btn_BorderHeader4.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader4.Foreground = (Brush)br.ConvertFrom("#474747");

            if (Back == 21)
            {
                Next1 = "RoseMoney1";
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, Next1);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 22)
            {
                HoaHongDoanhThu = 22;
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, HoaHongDoanhThu, CN);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 23)
            {
                HoaHongLoiNhuan = 23;
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, HoaHongLoiNhuan);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 24)
            {
                HoaHongViTri = 24;
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, HoaHongViTri);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 25)
            {
                KeHoach = 25;
                HoaHongKeHoach = 25;
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main, KeHoach, HoaHongKeHoach);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else
            {
                ucHoaHongNhanDuoc uc = new ucHoaHongNhanDuoc(Main);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
        }

        int HuongDanHHLoiNhuan;
        float HuongDanHHViTri;
        decimal HuongDanHHKeHoach;
        private void btn_BorderHeader4_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            btn_BorderHeader1.BorderThickness = new Thickness(0);
            btn_BorderHeader1.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader1.Foreground = (Brush)br.ConvertFrom("#474747");

            btn_BorderHeader2.BorderThickness = new Thickness(0);
            btn_BorderHeader2.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader2.Foreground = (Brush)br.ConvertFrom("#474747");

            btn_BorderHeader3.BorderThickness = new Thickness(0);
            btn_BorderHeader3.BorderBrush = (Brush)br.ConvertFrom("#FFF");
            tb_textHeader3.Foreground = (Brush)br.ConvertFrom("#474747");

            btn_BorderHeader4.BorderThickness = new Thickness(0, 0, 0, 2);
            btn_BorderHeader4.BorderBrush = (Brush)br.ConvertFrom("#4c5bd4");
            tb_textHeader4.Foreground = (Brush)br.ConvertFrom("#4c5bd4");

            if (Back == 21)
            {
                Next1 = "RoseMoney1";
                ucHuongDan uc = new ucHuongDan(Main, Next1);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 22)
            {
                HoaHongDoanhThu = 22;
                ucHuongDan uc = new ucHuongDan(Main, HoaHongDoanhThu);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 23)
            {
                HuongDanHHLoiNhuan = 23;
                ucHuongDan uc = new ucHuongDan(Main, HuongDanHHLoiNhuan);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 24)
            {
                HuongDanHHViTri = 24;
                ucHuongDan uc = new ucHuongDan(Main, HuongDanHHViTri);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else if (Back == 25)
            {
                HuongDanHHKeHoach = 25;
                ucHuongDan uc = new ucHuongDan(Main, HuongDanHHKeHoach);
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
            else
            {
                ucHuongDan uc = new ucHuongDan();
                stp_ShowPopup.Children.Clear();
                object Content = uc.Content;
                uc.Content = null;
                stp_ShowPopup.Children.Add(Content as UIElement);
            }
        }

    }
}
