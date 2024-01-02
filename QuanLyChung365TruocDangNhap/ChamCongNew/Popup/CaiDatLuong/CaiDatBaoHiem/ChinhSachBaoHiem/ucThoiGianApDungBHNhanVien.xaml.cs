using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using RestSharp;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ChinhSachBaoHiem
{
    /// <summary>
    /// Interaction logic for ucNextSaffInsurance.xaml
    /// </summary>
    public partial class ucThoiGianApDungBHNhanVien : UserControl
    {
        MainWindow Main;
        public static int static_month, static_year;
        int month, year;
        BrushConverter bc = new BrushConverter();
        private string Month = "";
        private List<string> IdNV;
        private List<clsNhanVienThuocCongTy.ListUser> lstUS;
        private int IdBaoHiem = 0;
        ucThemNhanVienBaoHiem ucThemNVBH;
        public ucThoiGianApDungBHNhanVien(MainWindow main, ucThemNhanVienBaoHiem uc, List<string> Id, int IdBH, List<clsNhanVienThuocCongTy.ListUser> lstus)
        {
            InitializeComponent();
            //txbInputMoney.Focus();
            Main = main;
            ucThemNVBH = uc;
            IdNV = Id;
            lstUS = lstus;
            IdBaoHiem = IdBH;
            LoadThongTinNVAdd();
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

        public void LoadThongTinNVAdd()
        {
            if (lstUS != null)
            {
                lsvThongTinNVTGAD.ItemsSource = lstUS;
                lsvThongTinNVTGAD.Items.Refresh();
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
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

        private async void bodSaveTime_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if(textHienThiTGBatDau.Text == "---- --- ----")
            {
                allow = false;
                validateTGAD.Visibility = Visibility.Visible;
            }
            else validateTGAD.Visibility = Visibility.Collapsed;
            if (string.IsNullOrEmpty(textNhapTienBH.Text.Trim()))
            {
                allow = false;
                validateTienBH.Visibility = Visibility.Visible;
            }
            else validateTienBH.Visibility = Visibility.Collapsed;
            string monthEnd, yearEnd;
            
            if(allow)
                using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/them_nv_nhom_insrc")))
                {
                    foreach (var item in IdNV)
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("cls_id_user", item);
                        request.AddParameter("cls_id_com", Main.IdAcount);
                        string[] day = textHienThiTGBatDau.Text.Split('/');
                        string y = day[day.Length - 1];
                        string mo = day[0];
                        request.AddParameter("cls_day", y + "-" + mo + "-" + "01");
                        if (textHienThiTGKetThuc.Text == "---- --- ----")
                            request.AddParameter("cls_day_end", "");
                        else
                        {
                            string[] dayEnd = textHienThiTGKetThuc.Text.Split('/');
                            yearEnd = dayEnd[dayEnd.Length - 1];
                            monthEnd = dayEnd[0];
                            request.AddParameter("cls_day_end", yearEnd + "-" + monthEnd + "-" + "01");
                        }
                        request.AddParameter("salaryBH", textNhapTienBH.Text);
                        request.AddParameter("cls_id_cl", IdBaoHiem);
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = await restclient.ExecuteAsync(request);
                        var b = resAlbum.Content;
                        this.Visibility = Visibility.Collapsed;
                        ucThemNVBH.Visibility = Visibility.Collapsed;
                    }
                }
            //else
            //{
            //    using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/them_nv_nhom_insrc")))
            //    {
            //        RestRequest request = new RestRequest();
            //        request.Method = Method.Post;
            //        request.AlwaysMultipartFormData = true;
            //        request.AddParameter("cls_id_cl", IdBaoHiem);
            //        request.AddParameter("cls_id_user", IdNV);
            //        request.AddParameter("cls_id_com", Main.IdAcount);
            //        string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
            //        string y = day[day.Length - 1];
            //        string[] day1 = day[0].Split(new[] { "Tháng" }, StringSplitOptions.None);
            //        string mo = day1[day1.Length - 1].Trim();
            //        request.AddParameter("cls_day", y + "-" + mo + "-" + "1");
            //        //request.AddParameter("cls_salary", textNhapTienBH.Text);
            //        request.AddParameter("token", Properties.Settings.Default.Token);
            //        RestResponse resAlbum = restclient.Execute(request);
            //        var b = resAlbum.Content;
            //        this.Visibility = Visibility.Collapsed;
            //        ucThemNVBH.Visibility = Visibility.Collapsed;
            //    }

            //}
        }

        private void closePopup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void ExitNextSaff_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BrushConverter brus = new BrushConverter();
            ucThemNVBH.bor.Fill = (Brush)brus.ConvertFrom("#000000");
            ucThemNVBH.bor.Opacity = 0.5;
            this.Visibility = Visibility.Collapsed; 
        }
        private void tb_Luong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                validateTienBH.Visibility = Visibility.Visible;
            }
            else
            {
                validateTienBH.Visibility = Visibility.Collapsed;
            }
        }
        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        private void tb_Luong_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!IsNumeric(tb.Text))
            {
                validateTienBH.Visibility = Visibility.Visible;
            }
            else
            {
                validateTienBH.Visibility = Visibility.Collapsed;
            }
        }
    }
}
