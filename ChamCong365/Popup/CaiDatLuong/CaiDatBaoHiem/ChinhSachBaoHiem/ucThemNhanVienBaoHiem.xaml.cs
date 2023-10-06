using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using ChamCong365.TimeKeeping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucAddSaffForInsurance.xaml
    /// </summary>
    /// 

    public partial class ucThemNhanVienBaoHiem : UserControl
    {
        //List<Saff> itemsSaff = new List<Saff>();
        List<string> itemGround = new List<string>() {"Nhóm 1", "Nhóm 2", "Nhóm 3", "Nhóm 4", "Nhóm 5", "Nhóm 6", "Nhóm 7" };
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        private string IdNV = "";
        private int IdbH;
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();

        public ucThemNhanVienBaoHiem(MainWindow main)
        {
            InitializeComponent();
            Main = main;
            
            
            lsvListGround.ItemsSource = itemGround;
            bodSelectSaff.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSelectGround.BorderThickness = new Thickness(0);
            bodSelectSaff.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            foreach(var item in main.lstNhanVienThuocCongTy)
            {
                if (item._id != 0)
                {
                    WebClient httpClient2 = new WebClient();
                    httpClient2.QueryString.Clear();
                    httpClient2.QueryString.Add("ID", item._id.ToString());
                    var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                    APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                    if (receiveInfoAPI.data != null)
                    {
                        item.avatarUser = receiveInfoAPI.data.user_info.AvatarUser;
                        lstNV.Add(item);
                    }
                    else
                    {
                        item.avatarUser = "Resource/image/llll.jpg";
                        lstNV.Add(item);
                    }
                    
                }
            }
            lsvListSaff.ItemsSource = lstNV;
        }
        public ucThemNhanVienBaoHiem(MainWindow main, int id)
        {
            InitializeComponent();
            Main = main;

            
            lsvListGround.ItemsSource = itemGround;
            bodSelectSaff.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSelectGround.BorderThickness = new Thickness(0);
            bodSelectSaff.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            foreach (var item in main.lstNhanVienThuocCongTy)
            {
                if (item._id != 0)
                {
                    WebClient httpClient2 = new WebClient();
                    httpClient2.QueryString.Clear();
                    httpClient2.QueryString.Add("ID", item._id.ToString());
                    var response = httpClient2.UploadValues(new Uri("http://43.239.223.142:9000/api/users/GetInfoUser"), "POST", httpClient2.QueryString);//User/GetInfoUserSendMessage
                    APIUser receiveInfoAPI = JsonConvert.DeserializeObject<APIUser>(UnicodeEncoding.UTF8.GetString(response));
                    if (receiveInfoAPI.data != null)
                    {
                        item.avatarUser = receiveInfoAPI.data.user_info.AvatarUser;
                        lstNV.Add(item);
                    }
                    else
                    {
                        item.avatarUser = "Resource/image/llll.jpg";
                        lstNV.Add(item);
                    }

                }
            }
            lsvListSaff.ItemsSource = lstNV;
            IdbH = id;
        }

        private void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodSelectSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodSelectSaff.BorderThickness = new Thickness(0,0,0,3);
            bodSelectGround.BorderThickness = new Thickness(0);
            bodSelectSaff.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            txbGround.Foreground = (Brush)br.ConvertFrom("#474747");
            stpLoadListSaff.Visibility = Visibility.Visible;
            stpLoadListGround.Visibility = Visibility.Collapsed;

        }

        private void bodSelectGround_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bodSelectGround.BorderThickness = new Thickness(0, 0, 0, 3);
            bodSelectSaff.BorderThickness = new Thickness(0);
            bodSelectGround.BorderBrush = (Brush)br.ConvertFrom("#4C5BD4");
            txbGround.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
            txbSaff.Foreground = (Brush)br.ConvertFrom("#474747");
            stpLoadListSaff.Visibility = Visibility.Collapsed;
            stpLoadListGround.Visibility = Visibility.Visible;
        }

        private void bodNextSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bor.Fill = Brushes.Transparent;
            Main.grShowPopup.Children.Add(new ucThoiGianApDungBHNhanVien(Main, this, IdNV,IdbH));
            
        }

        private void bodNextGroundInsurance_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Main.grShowPopup.Children.Add(new ucThoiGianApDungChoNhomBH(Main));
        }

        private void lsvListSaff_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void borNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.clsNhanVienThuocCongTy.ListUser us = (sender as Border).DataContext as OOP.clsNhanVienThuocCongTy.ListUser;
            if (us != null)
            {
                IdNV = us.idQLC.ToString();
            }
        }
    }
}
