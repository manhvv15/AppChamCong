using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using MaterialDesignThemes.Wpf;
using Newtonsoft.Json;
using RestSharp;
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
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatPhucLoi
{
    /// <summary>
    /// Interaction logic for ucThemNVHuongPhucLoi.xaml
    /// </summary>
    public partial class ucThemNVHuongPhucLoi : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //List<Saff> saffs = new List<Saff>();
        public static int static_month, static_year;
        int month, year;
        BrushConverter bc = new BrushConverter();
        private string Month = "";
        private MainWindow Main;
        private string IdUs;
        private int TypePLPC;
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf PhucL = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf();
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa PhuCap = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa();
        private List<OOP.clsNhanVienThuocCongTy.ListUser> _lstNhanVienThuocCongTy = new List<OOP.clsNhanVienThuocCongTy.ListUser>();

        public List<OOP.clsNhanVienThuocCongTy.ListUser> lstNhanVien
        {
            get { return _lstNhanVienThuocCongTy; }
            set { _lstNhanVienThuocCongTy = value; OnPropertyChanged(); }
        }

        public ucThemNVHuongPhucLoi(MainWindow main, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf pl)
        {
            InitializeComponent();
            Main = main;
            foreach (var item in main.lstNhanVienThuocCongTy)
            {
                if(string.IsNullOrEmpty(item.AvatarUser))
                    item.AvatarUser = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=48&q=75";
                item.isSelected = "False";
            }
            lstNhanVien = main.lstNhanVienThuocCongTy.ToList();
            PhucL = pl;
            TypePLPC = 3;
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();

            dteSelectedMonthKT = new Calendar();
            dteSelectedMonthKT.Visibility = Visibility.Collapsed;
            dteSelectedMonthKT.DisplayMode = CalendarMode.Year;
            dteSelectedMonthKT.MouseLeftButtonDown += borTGKetThuc_MouseLeftButtonUp;
            dteSelectedMonthKT.DisplayModeChanged += dteSelectedMonthKT_DisplayModeChanged;
            clKT = new List<Calendar>();
            clKT.Add(dteSelectedMonthKT);
            clKT = clKT.ToList();
        }
        public ucThemNVHuongPhucLoi(MainWindow main, frmDanhSachPhuCap frm, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa pc)
        {
            InitializeComponent();
            Main = main;
            foreach (var item in main.lstNhanVienThuocCongTy)
            {
                if(string.IsNullOrEmpty(item.AvatarUser))
                    item.AvatarUser = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=48&q=75";
                item.isSelected = "False";
            }
            lstNhanVien = main.lstNhanVienThuocCongTy.ToList();
            PhuCap = pc;
            TypePLPC = 4;
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();

            dteSelectedMonthKT = new Calendar();
            dteSelectedMonthKT.Visibility = Visibility.Collapsed;
            dteSelectedMonthKT.DisplayMode = CalendarMode.Year;
            dteSelectedMonthKT.MouseLeftButtonDown += borTGKetThuc_MouseLeftButtonUp;
            dteSelectedMonthKT.DisplayModeChanged += dteSelectedMonthKT_DisplayModeChanged;
            clKT = new List<Calendar>();
            clKT.Add(dteSelectedMonthKT);
            clKT = clKT.ToList();
        }
        private void borTGBatDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lsvChonThangBatDau.Visibility == Visibility.Collapsed)
            {
                lsvChonThangBatDau.Visibility = Visibility.Visible;
            }

            if (lsvChonThangBatDau.Visibility == Visibility.Visible)
            {
                dteSelectedMonthBD.Visibility = dteSelectedMonthBD.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                lsvChonThangBatDau.ItemsSource = clBD;
                flag = 1;
            }
        }

        private void borTGKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lsvChonThangKetThuc.Visibility == Visibility.Collapsed)
            {
                lsvChonThangKetThuc.Visibility = Visibility.Visible;
                Scroll.ScrollToEnd();
            }

            if (lsvChonThangKetThuc.Visibility == Visibility.Visible)
            {
                dteSelectedMonthKT.Visibility = dteSelectedMonthKT.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                lsvChonThangKetThuc.ItemsSource = clKT;
                flag = 1;
                Scroll.ScrollToEnd();
            }
        }
        private void lsvChonThang_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            lsvChonThangBatDau.Visibility = Visibility.Collapsed;
        }
       
        int flag = 0;
        private void dteSelectedMonthBD_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            lsvChonThangBatDau.Visibility = Visibility.Collapsed;
            var x = dteSelectedMonthBD.DisplayDate.ToString("MM/yyyy");
            if (flag == 0)
                x = "";
            else
                x = dteSelectedMonthBD.DisplayDate.ToString("MM/yyyy");
            if (textHienThiTGBatDau != null && !string.IsNullOrEmpty(x))
            {
                textHienThiTGBatDau.Text = x;
                DateTime a = DateTime.Parse(x);
                //dpEnd.SelectedDate = a;
            }
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonthBD.DisplayDate != null && flag > 0)
            {
                dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }

        private void dteSelectedMonthKT_DisplayModeChanged(object sender, CalendarModeChangedEventArgs e)
        {
            lsvChonThangBatDau.Visibility = Visibility.Collapsed;
            var x = dteSelectedMonthKT.DisplayDate.ToString("MM/yyyy");
            if (flag == 0)
                x = "";
            else
                x = dteSelectedMonthKT.DisplayDate.ToString("MM/yyyy");
            if (textHienThiTGKetThuc != null && !string.IsNullOrEmpty(x))
            {
                textHienThiTGKetThuc.Text = x;
                DateTime a = DateTime.Parse(x);
                //dpEnd.SelectedDate = a;
            }
            dteSelectedMonthKT.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonthKT.DisplayDate != null && flag > 0)
            {
                dteSelectedMonthKT.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }
        Calendar dteSelectedMonthBD { get; set; }
        Calendar dteSelectedMonthKT { get; set; }

        private List<Calendar> _clBD;

        public List<Calendar> clBD
        {
            get { return _clBD; }
            set
            {
                _clBD = value; OnPropertyChanged();
            }
        }

        private List<Calendar> _clKT;

        public List<Calendar> clKT
        {
            get { return _clKT; }
            set
            {
                _clKT = value; OnPropertyChanged();
            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void ExitInsuranceSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void bodSave_MouseEnter(object sender, MouseEventArgs e)
        {
            bodSave.BorderThickness = new Thickness(1);
        }

        private void lsvDSNhanVien_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private async void bodSave_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if(textHienThiTGBatDau.Text == "---- --- ----")
            {
                allow = false;
                validateTime.Visibility= Visibility.Visible;
            }
            else validateTime.Visibility= Visibility.Collapsed;
            if (lstNhanVien.Find(x => x.isSelected == "True") == null)
            {
                allow = false;
                validateNhanVien.Visibility = Visibility.Visible;
            }
            else validateNhanVien.Visibility = Visibility.Collapsed;
            if (allow)
            {
                if (TypePLPC == 3)
                {
                    try
                    {
                        foreach(var item in lstNhanVien.FindAll(x => x.isSelected == "True" && x.idQLC != 0))
                        {
                            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/them_nv_nhom")))
                            {
                                RestRequest request = new RestRequest();
                                request.Method = Method.Post;
                                request.AlwaysMultipartFormData = true;
                                request.AddParameter("cls_id_cl", PhucL.cl_id);
                                request.AddParameter("cls_id_com", Main.IdAcount);
                                request.AddParameter("cls_id_user", item.idQLC);
                                string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                                string y = day[day.Length - 1];
                                string m = day[0].Trim();
                                request.AddParameter("cls_day", y + "-" + m);
                                if (textHienThiTGKetThuc.Text != "---- --- ----")
                                {
                                    string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                    string ykt = daykt[daykt.Length - 1];
                                    string mkt = daykt[0].Trim();
                                    request.AddParameter("cls_day_end", ykt + "-" + mkt);
                                }
                                request.AddParameter("token", Properties.Settings.Default.Token);
                                RestResponse resAlbum = await restclient.ExecuteAsync(request);
                                var b = resAlbum.Content;
                                if (b.Contains("success"))
                                {
                                    this.Visibility = Visibility.Collapsed;
                                }

                            }
                        }
                    }
                    catch
                    {

                    }
                }
                else if (TypePLPC == 4)
                {
                    try
                    {
                        foreach(var item in lstNhanVien.FindAll(x => x.isSelected == "True" && x.idQLC != 0))
                        {
                            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/them_nv_nhom")))
                            {
                                RestRequest request = new RestRequest();
                                request.Method = Method.Post;
                                request.AlwaysMultipartFormData = true;
                                request.AddParameter("cls_id_cl", PhuCap.cl_id);
                                request.AddParameter("cls_id_com", Main.IdAcount);
                                request.AddParameter("cls_id_user", item.idQLC);
                                string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                                string y = day[day.Length - 1];
                                string m = day[0].Trim();
                                request.AddParameter("cls_day", y + "-" + m + "-" + "01T00:00:00.000+00:00");
                                if(textHienThiTGKetThuc.Text != "---- --- ----")
                                {
                                    string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                    string ykt = daykt[daykt.Length - 1];
                                    string mkt = daykt[0].Trim();
                                    request.AddParameter("cls_day_end", ykt + "-" + mkt + "-" + "01T00:00:00.000+00:00");
                                }
                                request.AddParameter("token", Properties.Settings.Default.Token);
                                RestResponse resAlbum = await restclient.ExecuteAsync(request);
                                var b = resAlbum.Content;
                                this.Visibility = Visibility.Collapsed;

                            }
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }
        private void borNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OOP.clsNhanVienThuocCongTy.ListUser us = (sender as Border).DataContext as OOP.clsNhanVienThuocCongTy.ListUser;
            if (us != null)
            {
                IdUs = us.idQLC.ToString();
            }
        }

        private void DockPanel_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            OOP.clsNhanVienThuocCongTy.ListUser us = (sender as DockPanel).DataContext as OOP.clsNhanVienThuocCongTy.ListUser;
            us.isSelected = us.isSelected == "True"?"False":"True";
            if(us.idQLC == 0)
            {
                foreach(var item in lstNhanVien)
                {
                    item.isSelected = us.isSelected;
                }
            }
            else
            {
                if (us.isSelected == "False" && lstNhanVien.Find(x => x.idQLC == 0).isSelected == "True")
                    lstNhanVien.Find(x => x.idQLC == 0).isSelected = "False";
                else if(us.isSelected == "True" && lstNhanVien.Find(x => x.isSelected == "False" && x.idQLC != 0) == null)
                    lstNhanVien.Find(x => x.idQLC == 0).isSelected = "True";
            }
        }

        private void textTuCanTim_TextChanged(object sender, TextChangedEventArgs e)
        {
            string x = textTenPC.Text;
            Search(x);
        }
        Thread search;
        private void Search(string textSearch)
        {
            if (search != null && search.IsAlive)
            {
                search.Abort();
            }
            search = new Thread(() =>
            {
                Thread.Sleep(500);
                if (string.IsNullOrEmpty(textSearch.Trim()))
                {
                    lstNhanVien = Main.lstNhanVienThuocCongTy;
                }
                else
                {
                    var lists = Main.lstNhanVienThuocCongTy.Where(x => x.userName.ToLower().Contains(textSearch.Trim().ToLower()) || x.idQLC == 0);
                    if (lists != null)
                    {
                        lstNhanVien = lists.ToList();
                    }
                    else
                    {
                        lstNhanVien = new List<OOP.clsNhanVienThuocCongTy.ListUser>();
                    }
                }
            });
            search.Start();
        }


        private void bodSave_MouseLeave(object sender, MouseEventArgs e)
        {
            bodSave.BorderThickness = new Thickness(0);
        }

        private void lsvLoadInsurance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
