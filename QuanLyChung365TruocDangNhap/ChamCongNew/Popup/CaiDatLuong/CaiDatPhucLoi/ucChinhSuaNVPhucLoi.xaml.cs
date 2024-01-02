using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Net;
using System.Text;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatPhucLoi
{
    /// <summary>
    /// Interaction logic for ucChinhSuaNVPhucLoi.xaml
    /// </summary>
    public partial class ucChinhSuaNVPhucLoi : UserControl, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public static int static_month, static_year;
        int month, year;
        BrushConverter br = new BrushConverter();
        MainWindow Main;
        public OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.ListUserFinal Data { get; set; }
        public ucDanhSachNVHuongPhucLoi DataPop { get; set; }
        public int type { get; set; }

        public ucChinhSuaNVPhucLoi(MainWindow main, OOP.CaiDatLuong.PhucLoi.clsNVTrongPL.ListUserFinal data, ucDanhSachNVHuongPhucLoi DataPop, int type = 0)
        {
            InitializeComponent();
            Main = main;
            Data = data;
            if(data.cl_day_end != "Chưa cập nhật")
            {
                textHienThiTGKetThuc.Text = data.cl_day_end;
            }
            textHienThiTGBatDau.Text = data.cl_day;
            
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
            this.DataPop = DataPop;
            this.type = type;
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
            }

            if (lsvChonThangKetThuc.Visibility == Visibility.Visible)
            {
                dteSelectedMonthKT.Visibility = dteSelectedMonthKT.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                lsvChonThangKetThuc.ItemsSource = clKT;
                flag = 1;
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
        private void bodSave_MouseEnter(object sender, MouseEventArgs e)
        {
            bodSave.BorderThickness = new Thickness(1);
        }

        private void bodSave_MouseLeave(object sender, MouseEventArgs e)
        {
            bodSave.BorderThickness = new Thickness(0);
        }

        private void bodCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbCancel.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbCancel.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void bodLoadImageSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility =Visibility.Collapsed;
        }

        private void bodSave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                if (textHienThiTGBatDau.Text == "---- --- ----")
                {
                    allow = false;
                    validateTime.Visibility = Visibility.Visible;
                }
                else validateTime.Visibility = Visibility.Collapsed;
                if (allow)
                {
                    using (WebClient web = new WebClient())
                    {
                        string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                        string y = day[day.Length - 1];
                        string m = day[0].Trim();
                        web.QueryString.Add("cls_day", y + "-" + m + "-" + "01T00:00:00.000+00:00");
                        if (textHienThiTGKetThuc.Text != "---- --- ----")
                        {
                            string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                            string ykt = daykt[daykt.Length - 1];
                            string mkt = daykt[0].Trim();
                            web.QueryString.Add("cls_day_end", ykt + "-" + mkt + "-" + "01T00:00:00.000+00:00");
                        }
                        web.QueryString.Add("cls_id_cl", Data.cl_id);
                        web.QueryString.Add("cls_id_com", Data.inForPerson.employee.com_id.ToString());
                        web.QueryString.Add("cls_id_user", Data.idQLC.ToString());
                        web.QueryString.Add("token", Properties.Settings.Default.Token);
                        web.UploadValuesAsync(new Uri("https://api.timviec365.vn/api/tinhluong/congty/edit_nv_nhom"), "POST", web.QueryString);
                        web.UploadValuesCompleted += (s1, e1) =>
                        {
                            try
                            {
                                var check = UTF8Encoding.UTF8.GetString(e1.Result);
                                if(type == 0)
                                    DataPop.LoadDLNhanVienPL();
                                else
                                    DataPop.LoadDLNhanVienPC();
                                this.Visibility = Visibility.Collapsed;
                            }
                            catch { }
                        };
                    }
                }
            }
            catch { }
        }

        private void ExitInsuranceSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
