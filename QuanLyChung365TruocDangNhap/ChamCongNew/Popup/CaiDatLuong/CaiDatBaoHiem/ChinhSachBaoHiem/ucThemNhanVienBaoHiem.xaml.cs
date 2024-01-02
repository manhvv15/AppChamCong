using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucAddSaffForInsurance.xaml
    /// </summary>
    /// 

    public partial class ucThemNhanVienBaoHiem : UserControl, INotifyPropertyChanged
    {
        //List<Saff> itemsSaff = new List<Saff>();, INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        List<string> itemGround = new List<string>() {"Nhóm 1", "Nhóm 2", "Nhóm 3", "Nhóm 4", "Nhóm 5", "Nhóm 6", "Nhóm 7" };
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        private string IdNV = "";
        private int IdbH;
        private List<OOP.clsNhanVienThuocCongTy.ListUser> lstNV = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
        private List<OOP.clsNhanVienThuocCongTy.ListUser> _lstNVSearch;

        public  List<OOP.clsNhanVienThuocCongTy.ListUser> lstNVSearch
        {
            get { return _lstNVSearch; }
            set { _lstNVSearch = value; OnPropertyChanged(); }
        }


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
            lstNVSearch = lstNV;
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
                    //if (string.IsNullOrEmpty(item.avatarUser))
                    {
                        item.avatarUser = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=64&q=75";
                    }
                    lstNV.Add(item) ;
                }
            }
            lstNVSearch = lstNV;
            IdbH = id;
        }

        public void ExitCreateSaff_MouseUp(object sender, MouseButtonEventArgs e)
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
            if(lsvListSaff.SelectedItems.Count > 0)
            {
                List<string> list = new List<string>();
                List<clsNhanVienThuocCongTy.ListUser> listus = new List<clsNhanVienThuocCongTy.ListUser>();
                foreach (OOP.clsNhanVienThuocCongTy.ListUser item in lsvListSaff.SelectedItems)
                {
                    list.Add(item.idQLC.ToString());
                    listus.Add(item);
                }
                Main.grShowPopup.Children.Add(new ucThoiGianApDungBHNhanVien(Main, this, list,IdbH, listus));
                this.Visibility = Visibility.Collapsed;
            }
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
        Thread search;
        private void textTuCanTim_TextChanged(object sender, TextChangedEventArgs e)
        {
            string x = textTuCanTim.Text;
            Search(x);
            
        }
        private void Search(string textSearch)
        {
            if(search != null && search.IsAlive) 
            {
                search.Abort();
            }
            search = new Thread(() =>
            {
                Thread.Sleep(500);
                if (string.IsNullOrEmpty(textSearch.Trim()))
                {
                    lstNVSearch = lstNV;
                }
                else
                {
                    var lists = lstNV.Where(x => x.userName.ToLower().Contains(textSearch.Trim().ToLower()));
                    if (lists != null)
                    {
                        lstNVSearch = lists.ToList();
                    }
                    else
                    {
                        lstNVSearch = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
                    }
                }
            });
            search.Start();
        }
    }
}
