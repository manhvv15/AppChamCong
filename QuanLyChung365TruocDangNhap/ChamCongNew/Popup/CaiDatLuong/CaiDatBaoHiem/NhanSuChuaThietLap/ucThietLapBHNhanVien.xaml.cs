using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.BaoHiem;
using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ListTabInsurance
{
    /// <summary>
    /// Interaction logic for ucSetupInsurance.xaml
    /// </summary>
    /// 

    public partial class ucThietLapBHNhanVien : UserControl
    {
        BrushConverter bru = new BrushConverter();
        public static int static_month, static_year;
        int month, year;
        List<string> insurance = new List<string>(){ "BHXH tính theo lương cơ bản", "BHXH tính theo lương nhập vào","BHXH mới"};
        public List<OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList> lstBH = new List<OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList>();
        public OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal NV { get; set; }
        MainWindow Main { get; set; }
        public ucThietLapBHNhanVien(MainWindow main, OOP.CaiDatLuong.BaoHiem.clsNSuChuaThietLap.ListUserFinal nhanvien)
        {
            InitializeComponent();
            
            if (string.IsNullOrEmpty(nhanvien.avatarUser)) nhanvien.avatarUser = "https://hungha365.com/_next/image?url=%2Favt_365.png&w=64&q=75";
            Main = main;
            NV = nhanvien;
            GetData();
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
        private void GetData()
        {
            try
            {
                using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/takeinfo_insrc")))
                {
                    RestRequest request = new RestRequest();
                    request.Method = Method.Post;
                    request.AlwaysMultipartFormData = true;

                    request.AddParameter("cl_com", Main.IdAcount);
                    request.AddParameter("token", Properties.Settings.Default.Token);
                    RestResponse resAlbum = restclient.Execute(request);
                    var b = resAlbum.Content;
                    OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.Root Bh = JsonConvert.DeserializeObject<OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.Root>(b);
                    if (Bh.tax_list != null)
                    {
                        foreach (var item in Bh.tax_list)
                        {
                            if (item.TinhluongFormSalary != null)
                            {
                                foreach (var ct in item.TinhluongFormSalary)
                                {
                                    item.calculation_formula = item.calculation_formula + " " + ct.fs_repica;
                                }
                            }
                            lstBH.Add(item);

                        }
                        lstBH.Insert(0, new OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList() { cl_id = 5, cl_name = "BHXH tính theo lương nhập vào"});
                        lstBH.Insert(0, new OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList() { cl_id = 4, cl_name = "BHXH tính theo lương cơ bản" });
                        lstBH.Insert(0, new OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList() { cl_id = 3, cl_name = "Nhập tiền bảo hiểm"  });
                        cboBH.ItemsSource = lstBH;
                        cboBH.DisplayMemberPath = "cl_name";
                    }
                }
            }
            catch
            {

            }
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodSave_MouseEnter(object sender, MouseEventArgs e)
        {
            bodSave.BorderThickness = new Thickness(1);
        }

        private void bodSave_MouseLeave(object sender, MouseEventArgs e)
        {
            bodSave.BorderThickness = new Thickness(0);
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)bru.ConvertFrom("#4C5BD4");
            txbCancel.Foreground = (Brush)bru.ConvertFrom("#FFFFFF");
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)bru.ConvertFrom("#FFFFFF");
            txbCancel.Foreground = (Brush)bru.ConvertFrom("#4C5BD4");
        }

        private void ExitInsuranceSaff_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void bodCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
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

        private void cboBH_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(cboBH.SelectedIndex == 0)
            {
                stNhapBH.Visibility = Visibility.Visible;
            }
            else
                stNhapBH.Visibility = Visibility.Collapsed;
        }

        private async void bodSave_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if(textHienThiTGBatDau.Text == "---- --- ----")
            {
                allow = false;
                validateTGAD.Visibility = Visibility.Visible;
            }
            else validateTGAD.Visibility = Visibility.Collapsed;
            if(cboBH.SelectedItem == null)
            {
                if (string.IsNullOrEmpty(textNhapTienBH.Text.Trim()))
                {
                    allow = false;
                    validateBH.Visibility = Visibility.Visible;
                }
                else validateBH.Visibility = Visibility.Collapsed;
            }
            string monthEnd, yearEnd;

            if (allow)
            {
                try
                {
                    using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/them_nv_nhom_insrc")))
                    {
                        RestRequest request = new RestRequest();
                        request.Method = Method.Post;
                        request.AlwaysMultipartFormData = true;
                        request.AddParameter("cls_id_user", NV.idQLC);
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
                        if (cboBH.SelectedIndex == 0)
                        {
                            request.AddParameter("salaryBH", textNhapTienBH.Text);
                        }
                        request.AddParameter("cls_id_cl", ((OOP.CaiDatLuong.BaoHiem.clsDSBaoHiem.TaxList)cboBH.SelectedItem).cl_id);
                        request.AddParameter("token", Properties.Settings.Default.Token);
                        RestResponse resAlbum = await restclient.ExecuteAsync(request);
                        var b = resAlbum.Content;
                        this.Visibility = Visibility.Collapsed;
                    }
                }
                catch (Exception)
                {
                }
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
