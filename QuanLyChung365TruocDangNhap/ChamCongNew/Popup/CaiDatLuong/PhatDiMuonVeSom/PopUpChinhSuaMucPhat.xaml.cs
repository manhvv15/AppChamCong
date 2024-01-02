﻿using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.DiMuonVeSom;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using System.Linq;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy.DescriptionEntities;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.PhatDiMuonVeSom
{
    /// <summary>
    /// Interaction logic for PopUpChinhSuaMucPhat.xaml
    /// </summary>
    public partial class PopUpChinhSuaMucPhat : UserControl,INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private int _IsSmallSize;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        BrushConverter br = new BrushConverter();
        private string Month;
        private MainWindow Main;
        private frmCaiDatDiMuonVeSom frmDMVS;
        private List<OOP.CaiDatLuong.clsShift.Item> lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();
        private OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo info;
        private string _Clv;
        public string CLV
        {
            get { return _Clv; }
            set { _Clv = value; OnPropertyChanged(); }
            
        }

        public PopUpChinhSuaMucPhat(string TieuDe, MainWindow main, frmCaiDatDiMuonVeSom frm)
        {
            InitializeComponent();
            Main = main;
            frmDMVS = frm;
            textTieuDe.Text = TieuDe;
            LoadDLCaLamViec();
            textNamTruocBD.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiBD.Text = DateTime.Now.Year.ToString();
            textNamTruocKT.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiKT.Text = DateTime.Now.Year.ToString();
            if (textTieuDe.Text == "Thêm mới mức phạt đi muộn về sớm")
            {
                tb_Name.Text = "Lưu";
                //borNamBatDauAD.Margin = new Thickness(20, 0, 0, 70);
            }
            loadCalendar();
        }
        string caApdung;
        public PopUpChinhSuaMucPhat(string TieuDe, MainWindow main, frmCaiDatDiMuonVeSom frm, OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo inf)
        {
            InitializeComponent();
            this.DataContext = this;
            Main = main;
            frmDMVS = frm;
            textTieuDe.Text = TieuDe;
            info = inf;
            textNamTruocBD.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiBD.Text = DateTime.Now.Year.ToString();
            textNamTruocKT.Text = (int.Parse(DateTime.Now.Year.ToString()) - 1).ToString();
            textNamHienTaiKT.Text = DateTime.Now.Year.ToString();
           if (textTieuDe.Text == "Chỉnh sửa mức phạt đi muộn về sớm")
            {
                tb_Name.Text = "Cập nhật";
                borNamBatDauAD.Margin = new Thickness(20, 0, 0, 115);
                //stp_TGKetThuc.Visibility = Visibility.Visible;
            }
            if (inf.pm_type == 1)
            {
                cboLyDoPhat.Text = "Đi muộn";
            }
            else if (inf.pm_type == 2)
            {
                cboLyDoPhat.Text = "Về sớm";
            }

            //cboCaLVApDung.ItemsSource = frmDMVS.lstShift;
           
            foreach (var item in frm.lstShift)
            {
                if (inf.pm_shift == item.shift_id)
                {
                    caApdung = item.shift_name;
                }
            }
            cboCaLVApDung.Text = caApdung.ToString();
            LoadDLCaLamViec();
            loadCalendar();
            textThoiGian.Text = inf.pm_minute.ToString();
            if (inf.pm_type_phat == 1)
            {
                cboLoaiPhat.Text = "Phạt tiền/ca";
            }
            else if (inf.pm_type_phat == 2)
            {
                cboLoaiPhat.Text = "Phạt công/ca";
            }
            if (inf.pm_type_phat == 1)
            {
               textTienPhat.Text = inf.pm_monney;
            }
            else if (inf.pm_type_phat == 2)
            {
                textTienPhat.Text = inf.pm_monney;
            }
            textHienThiTGBatDau.Text = "Tháng " + inf.pm_time_begin.Month + "/" + inf.pm_time_begin.Year;
        }

        public void loadCalendar()
        {
            dteSelectedMonthBD = new Calendar();
            dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            dteSelectedMonthBD.MouseLeftButtonDown += borTGBatDau_MouseLeftButtonUp;
            dteSelectedMonthBD.DisplayModeChanged += dteSelectedMonthBD_DisplayModeChanged;
            clBD = new List<Calendar>();
            clBD.Add(dteSelectedMonthBD);
            clBD = clBD.ToList();
        }
        private async void LoadDLCaLamViec()
        {
            lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "http://210.245.108.202:3000/api/qlc/shift/list");
            request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            OOP.CaiDatLuong.clsShift.Root CaLV = JsonConvert.DeserializeObject<OOP.CaiDatLuong.clsShift.Root>( await response.Content.ReadAsStringAsync());
            if (CaLV.data != null)
            {
                foreach (var calv in CaLV.data.items)
                {
                    lstShift.Add(calv);
                    cboCaLVApDung.Items.Add("(" + calv.shift_id + ")" + "-" + calv.shift_name);
                }
            }
        }

        private void Rectangle_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void borTGKetThuc_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (borNamKT.Visibility == Visibility.Visible)
            {
                borNamKT.Visibility = Visibility.Collapsed;
                closePopup.Visibility = Visibility.Collapsed;
            }
            else
            {
                borNamKT.Visibility = Visibility.Visible;
                closePopup.Visibility = Visibility.Visible;
            }
        }

        private void borTGBatDau_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (lsvChonThangBatDau.Visibility == Visibility.Collapsed)
            {
                lsvChonThangBatDau.Visibility = Visibility.Visible;
                scrollPop.ScrollToEnd();
            }

            if (lsvChonThangBatDau.Visibility == Visibility.Visible)
            {
                dteSelectedMonthBD.Visibility = dteSelectedMonthBD.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;
                lsvChonThangBatDau.ItemsSource = clBD;
                flag = 1;
                if(dteSelectedMonthBD.Visibility == Visibility.Visible)
                    scrollPop.ScrollToEnd();
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
                x = "Tháng " + dteSelectedMonthBD.DisplayDate.ToString("MM/yyyy");
            if (textHienThiTGBatDau != null && !string.IsNullOrEmpty(x))
            {
                textHienThiTGBatDau.Text = x;
                //dpEnd.SelectedDate = a;
            }
            dteSelectedMonthBD.DisplayMode = CalendarMode.Year;
            if (dteSelectedMonthBD.DisplayDate != null && flag > 0)
            {
                dteSelectedMonthBD.Visibility = Visibility.Collapsed;
            }
            flag += 1;
        }
        Calendar dteSelectedMonthBD { get; set; }
        private List<Calendar> _clBD;

        public List<Calendar> clBD
        {
            get { return _clBD; }
            set
            {
                _clBD = value; OnPropertyChanged();
            }
        }

        private void closePopup_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void borNamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocKT.Visibility = Visibility.Visible;
            DSThangNamSauKT.Visibility = Visibility.Collapsed;
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
        }

        private void borNamHienTaiKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocKT.Visibility = Visibility.Collapsed;
            DSThangNamSauKT.Visibility = Visibility.Visible;
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
        }

        
        private void NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocBatDau.Visibility = Visibility.Visible;
            DSThangNamSauBatDau.Visibility = Visibility.Collapsed;
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
        }

        private void NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            DSThangNamTruocBatDau.Visibility = Visibility.Collapsed;
            DSThangNamSauBatDau.Visibility = Visibility.Visible;
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
        }

        private void Thang1NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamTruocBD.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocBD.Background = Brushes.Transparent;
            Thang2NamTruocBD.Background = Brushes.Transparent;
            Thang3NamTruocBD.Background = Brushes.Transparent;
            Thang4NamTruocBD.Background = Brushes.Transparent;
            Thang5NamTruocBD.Background = Brushes.Transparent;
            Thang6NamTruocBD.Background = Brushes.Transparent;
            Thang7NamTruocBD.Background = Brushes.Transparent;
            Thang8NamTruocBD.Background = Brushes.Transparent;
            Thang9NamTruocBD.Background = Brushes.Transparent;
            Thang10NamTruocBD.Background = Brushes.Transparent;
            Thang11NamTruocBD.Background = Brushes.Transparent;
            Thang12NamTruocBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void Thang1NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamSauBD.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauBD.Background = Brushes.Transparent;
            Thang2NamSauBD.Background = Brushes.Transparent;
            Thang3NamSauBD.Background = Brushes.Transparent;
            Thang4NamSauBD.Background = Brushes.Transparent;
            Thang5NamSauBD.Background = Brushes.Transparent;
            Thang6NamSauBD.Background = Brushes.Transparent;
            Thang7NamSauBD.Background = Brushes.Transparent;
            Thang8NamSauBD.Background = Brushes.Transparent;
            Thang9NamSauBD.Background = Brushes.Transparent;
            Thang10NamSauBD.Background = Brushes.Transparent;
            Thang11NamSauBD.Background = Brushes.Transparent;
            Thang12NamSauBD.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void btnChonThangNayNamSauBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGBatDau.Text = Month + "/" + textNamHienTaiBD.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;

        }

        private void btnChonThangNayNamTruocBD_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGBatDau.Text = Month + "/" + textNamTruocBD.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void Thang1NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamTruocKT.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamTruocKT.Background = Brushes.Transparent;
            Thang2NamTruocKT.Background = Brushes.Transparent;
            Thang3NamTruocKT.Background = Brushes.Transparent;
            Thang4NamTruocKT.Background = Brushes.Transparent;
            Thang5NamTruocKT.Background = Brushes.Transparent;
            Thang6NamTruocKT.Background = Brushes.Transparent;
            Thang7NamTruocKT.Background = Brushes.Transparent;
            Thang8NamTruocKT.Background = Brushes.Transparent;
            Thang9NamTruocKT.Background = Brushes.Transparent;
            Thang10NamTruocKT.Background = Brushes.Transparent;
            Thang11NamTruocKT.Background = Brushes.Transparent;
            Thang12NamTruocKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void Thang1NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 1";
        }

        private void Thang2NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 2";
        }

        private void Thang3NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 3";
        }

        private void Thang4NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 4";
        }

        private void Thang5NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 5";
        }

        private void Thang6NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 6";
        }

        private void Thang7NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 7";
        }

        private void Thang8NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 8";
        }

        private void Thang9NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 9";
        }

        private void Thang10NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 10";
        }

        private void Thang11NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Thang12NamSauKT.Background = Brushes.Transparent;
            Month = "Tháng 11";
        }

        private void Thang12NamSauKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            Thang1NamSauKT.Background = Brushes.Transparent;
            Thang2NamSauKT.Background = Brushes.Transparent;
            Thang3NamSauKT.Background = Brushes.Transparent;
            Thang4NamSauKT.Background = Brushes.Transparent;
            Thang5NamSauKT.Background = Brushes.Transparent;
            Thang6NamSauKT.Background = Brushes.Transparent;
            Thang7NamSauKT.Background = Brushes.Transparent;
            Thang8NamSauKT.Background = Brushes.Transparent;
            Thang9NamSauKT.Background = Brushes.Transparent;
            Thang10NamSauKT.Background = Brushes.Transparent;
            Thang11NamSauKT.Background = Brushes.Transparent;
            Thang12NamSauKT.Background = (Brush)bc.ConvertFrom("#4C5BD4");
            Month = "Tháng 12";
        }

        private void textThangNayNamHienTaiKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGKetThuc.Text = Month + "/" + textNamHienTaiKT.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void textHienThiThangNayNamTruocKT_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            textHienThiTGKetThuc.Text = Month + "/" + textNamTruocKT.Text;
            borNamBatDauAD.Visibility = Visibility.Collapsed;
            borNamKT.Visibility = Visibility.Collapsed;
            closePopup.Visibility = Visibility.Collapsed;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnTangTienPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textTienPhat.Text == "0")
            {
                textTienPhat.Text = "10000";

            }
            else if (textTienPhat.Text == "")
            {
                textTienPhat.Text = "10000";
            }
            else
            {
                textTienPhat.Text = (double.Parse(textTienPhat.Text) + 10000).ToString();
            }
            
        }

        private void btnGiamTienPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textTienPhat.Text == "0")
            {
                textTienPhat.Text = "0";

            }
            else if(textTienPhat.Text == "")
            {
                textTienPhat.Text = "0";
            }
            else
            {
                textTienPhat.Text = (double.Parse(textTienPhat.Text) - 10000).ToString();
            }
        }

        private void btnTangThoiGian_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textThoiGian.Text == "0")
            {
                textThoiGian.Text = "1";

            }
            else if (textThoiGian.Text == "")
            {
                textThoiGian.Text = "1";
            }
            else
            {
                textThoiGian.Text = (double.Parse(textThoiGian.Text) + 1).ToString();
            }
        }

        private void btnGiamThoiGian_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (textThoiGian.Text == "0")
            {
                textThoiGian.Text = "0";

            }
            else if (textThoiGian.Text == "")
            {
                textThoiGian.Text = "0";
            }
            else
            {
                textThoiGian.Text = (double.Parse(textThoiGian.Text) - 1).ToString();
            }
        }

        private async void btnCapNhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (cboLyDoPhat.Text==""|| cboLyDoPhat.Text == "Chọn lý do")
            {
                textThongBaoLDP.Visibility = Visibility.Visible;
                allow = false;
            }
            else
            {
                textThongBaoLDP.Visibility = Visibility.Collapsed;
            }
            if(cboCaLVApDung.Text == "" || cboCaLVApDung.SelectedIndex < 0)
            {
                textThongBaoCLV.Visibility = Visibility.Visible;
                allow = false;
            }
            else
            {
                textThongBaoCLV.Visibility = Visibility.Collapsed;
            }
            if (textThoiGian.Text == "")
            {
                textThongBaoTG.Visibility = Visibility.Visible;
                allow = false;
            }
            else
            {
                textThongBaoTG.Visibility = Visibility.Collapsed;
            }
            if (!IsNumeric(textThoiGian.Text))
            {
                allow = false;
                validateNumberTime.Visibility = Visibility.Visible;
            }
            else
            {
                validateNumberTime.Visibility = Visibility.Collapsed;
            }
            if (cboLoaiPhat.Text == "")
            {
                textThongBaoLP.Visibility = Visibility.Visible;
                allow = false;
            }
            else
            {
                textThongBaoLP.Visibility = Visibility.Collapsed;
            }
            if (textTienPhat.Text == "")
            {
                textThongBaoTienCongP.Visibility = Visibility.Visible;
                allow = false;
            }
            else
            {
                textThongBaoTienCongP.Visibility = Visibility.Collapsed;
            }
            if (!IsNumeric(textTienPhat.Text))
            {
                allow = false;
                validateNumberTP.Visibility = Visibility.Visible;
            }
            else
            {
                validateNumberTP.Visibility = Visibility.Collapsed;
            }
            if (textHienThiTGBatDau.Text == "---- -- --")
            {
                textThongBaoTGBatDau.Visibility = Visibility.Visible;
                allow = false;
            }
            else
            {
                textThongBaoTGBatDau.Visibility = Visibility.Collapsed;
            }
            if(allow)
            {
                if (textTieuDe.Text == "Thêm mới mức phạt đi muộn về sớm")
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_phat_muon")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            if (cboLyDoPhat.Text == "Đi muộn")
                            {
                                request.AddParameter("pm_type", "1");
                            }
                            else if (cboLyDoPhat.Text == "Về sớm")
                            {
                                request.AddParameter("pm_type", "2");
                            }
                            string[] calv = cboCaLVApDung.Text.Split(Convert.ToChar(")"));
                            string CaLV1 = calv[0];
                            string[] CaLV2 = CaLV1.Split(Convert.ToChar("("));
                            string CaLV3 = CaLV2[CaLV2.Length - 1];
                            request.AddParameter("pm_shift", CaLV3);
                            if (cboLoaiPhat.Text == "Phạt tiền/ca")
                            {
                                request.AddParameter("pm_type_phat", "1");
                            }
                            else if (cboLoaiPhat.Text == "Phạt công/ca")
                            {
                                request.AddParameter("pm_type_phat", "2");
                            }
                            string[] TGBatDau = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                            string NamBD = TGBatDau[TGBatDau.Length - 1];
                            string Thang = TGBatDau[0];
                            string[] ThangBD1 = Thang.Split(Convert.ToChar("g"));
                            string ThangBD = ThangBD1[ThangBD1.Length - 1].Trim();
                            request.AddParameter("pm_time_begin", $"{NamBD}-{ThangBD}-01T00:00:00.000+00:00");
                            request.AddParameter("pm_time_end", $"1970-01-01T00:00:00.000+00:00");
                            request.AddParameter("pm_monney", textTienPhat.Text);
                            request.AddParameter("pm_minute", textThoiGian.Text);
                            request.AddParameter("pm_id_com", Main.IdAcount.ToString());
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            RestResponse resAlbum = await restclient.ExecuteAsync(request);
                            var b = resAlbum.Content;
                            OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsAddLateEaly.Root Add = JsonConvert.DeserializeObject<OOP.CaiDatLuong.CaiDatDiMuonVeSom.clsAddLateEaly.Root>(b);
                            if (Add.newobj != null)
                            {
                                OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo inf = new OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo();
                                OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Shift clv = new OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Shift();
                                inf.shift = new OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Shift();

                                inf.pm_id = Add.newobj.pm_id;
                                inf.pm_id_com = Add.newobj.pm_id_com;
                                inf.pm_minute = Add.newobj.pm_minute;
                                inf.pm_monney = Add.newobj.pm_monney.ToString();

                                inf.pm_shift = Add.newobj.pm_shift;
                                clv.shift_id = Add.newobj.pm_shift;
                                foreach (var item in lstShift)
                                {
                                    if (clv.shift_id == item.shift_id)
                                    {
                                        clv.shift_name = item.shift_name;
                                    }
                                    if (Add.newobj.pm_shift == item.shift_id)
                                    {

                                        inf.shift.shift_name = item.shift_name;
                                    }
                                }
                                inf.pm_time_begin = Add.newobj.pm_time_begin;
                                inf.pm_time_begin_format = $"{inf.pm_time_begin.Month} - {inf.pm_time_begin.Year}";
                                inf.pm_time_created = Add.newobj.pm_time_created;
                                inf.pm_time_end = Add.newobj.pm_time_end;
                                inf.pm_type = Add.newobj.pm_type;
                                inf.pm_type_phat = Add.newobj.pm_type_phat;
                                inf._id = Add.newobj._id;
                                if (inf.pm_type == 1)
                                {
                                    inf.pm_type_str = "Đi muộn";
                                }
                                else if (inf.pm_type == 2)
                                {
                                    inf.pm_type_str = "Về sớm";
                                }
                                if (inf.pm_time_end == null)
                                {
                                    inf.pm_time_end_str = "Chưa cập nhât";
                                }
                                else
                                {
                                    inf.pm_time_end_str = inf.pm_time_end.ToString();
                                }
                                if (inf.pm_type_phat == 1)
                                {
                                    inf.pm_monney_str = inf.pm_monney + " VNĐ/ca";
                                }
                                else if (inf.pm_type_phat == 2)
                                {
                                    inf.pm_monney_str = inf.pm_monney + " công/ca";
                                }
                                if(!string.IsNullOrEmpty(frmDMVS.lsvThang.Text) && int.Parse(frmDMVS.lsvThang.Text.Split(' ')[1]) == int.Parse(ThangBD) && int.Parse(frmDMVS.lsvNam.Text.Split(' ')[1]) == int.Parse(NamBD) || string.IsNullOrEmpty(frmDMVS.lsvThang.Text) && int.Parse(ThangBD) == DateTime.Now.Month && int.Parse(NamBD) == DateTime.Now.Year)
                                {
                                    frmDMVS.lstPhatMuon.Add(inf);
                                    frmDMVS.dgv.ItemsSource = null;
                                    frmDMVS.dgv.ItemsSource = frmDMVS.lstPhatMuon;
                                }
                                this.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                else
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/update_phat_muon")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            if (cboLyDoPhat.Text == "Đi muộn")
                            {
                                request.AddParameter("pm_type", "1");
                            }
                            else if (cboLyDoPhat.Text == "Về sớm")
                            {
                                request.AddParameter("pm_type", "2");
                            }
                            string[] calv = cboCaLVApDung.Text.Split(Convert.ToChar(")"));
                            string CaLV1 = calv[0];
                            string[] CaLV2 = CaLV1.Split(Convert.ToChar("("));
                            string CaLV3 = CaLV2[CaLV2.Length - 1];
                            request.AddParameter("pm_shift", CaLV3);
                            if (cboLoaiPhat.Text == "Phạt tiền/ca")
                            {
                                request.AddParameter("pm_type_phat", "1");
                            }
                            else if (cboLoaiPhat.Text == "Phạt công/ca")
                            {
                                request.AddParameter("pm_type_phat", "2");
                            }
                            string[] TGBatDau = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                            string NamBD = TGBatDau[TGBatDau.Length - 1];
                            string Thang = TGBatDau[0];
                            string[] ThangBD1 = Thang.Split(Convert.ToChar("g"));
                            string ThangBD = ThangBD1[ThangBD1.Length - 1].Trim();
                            request.AddParameter("pm_time_begin", $"{NamBD}-{ThangBD}-01T00:00:00.000+00:00");
                            if(textHienThiTGKetThuc.Text != "---- -- --")
                            {
                                string[] TGKetThuc = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                string NamKT = TGKetThuc[TGKetThuc.Length - 1];
                                string Thangkt = TGKetThuc[0];
                                string[] ThangKT1 = Thangkt.Split(Convert.ToChar("g"));
                                string ThangKT = ThangKT1[ThangKT1.Length - 1].Trim();
                                request.AddParameter("pm_time_end", $"{NamKT}-{ThangKT}-01T00:00:00.000+00:00");
                            }
                            request.AddParameter("pm_monney", textTienPhat.Text);
                            request.AddParameter("pm_minute", textThoiGian.Text);
                            request.AddParameter("pm_id_com", Main.IdAcount.ToString());
                            request.AddParameter("pm_id", info.pm_id);
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            RestResponse resAlbum = await restclient.ExecuteAsync(request);
                            var b = resAlbum.Content;
                            foreach (var ms in frmDMVS.lstPhatMuon)
                            {
                                if (ms.pm_id == info.pm_id)
                                {
                                    //OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo inf = new OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.PhatMuonInfo();
                                    OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Shift clv = new OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Shift();
                                    //inf.shift = new OOP.CaiDatLuong.CaiDatDiMuonVeSom.DSPhatDiMuonVeSom.Shift();

                                    ms.pm_id = info.pm_id;
                                    ms.pm_id_com = Main.IdAcount;
                                    ms.pm_minute = int.Parse(textThoiGian.Text);
                                    ms.pm_monney = textTienPhat.Text;
                                    ms.pm_time_begin_format = $"{ms.pm_time_begin.Month} - {ms.pm_time_begin.Year}";
                                    //ms.pm_shift = CaLV3;
                                    clv.shift_id = ms.pm_shift;
                                    foreach (var item in lstShift)
                                    {
                                        if (clv.shift_id == item.shift_id)
                                        {
                                            clv.shift_name = item.shift_name;
                                        }
                                        if (int.Parse(CaLV3) == item.shift_id)
                                        {

                                            ms.shift.shift_name = item.shift_name;
                                        }
                                    }
                                    ms.pm_time_begin = Convert.ToDateTime(NamBD + "-" + ThangBD + "-01T00:00:00.000Z");

                                    ms.pm_time_created = info.pm_time_created;
                                    if(textHienThiTGKetThuc.Text != "---- -- --")
                                    {
                                        string[] TGKetThuc = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                        string NamKT = TGKetThuc[TGKetThuc.Length - 1];
                                        string Thangkt = TGKetThuc[0];
                                        string[] ThangKT1 = Thangkt.Split(Convert.ToChar("g"));
                                        string ThangKT = ThangKT1[ThangKT1.Length - 1].Trim();
                                        ms.pm_time_end = Convert.ToDateTime(NamKT + "-" + ThangKT + "-01T00:00:00.000Z");
                                    }
                                    if (cboLyDoPhat.Text == "Đi muộn")
                                    {
                                        ms.pm_type = 1;
                                    }
                                    else if (cboLyDoPhat.Text == "về sớm")
                                    {
                                        ms.pm_type = 2;
                                    }
                                    if (cboLoaiPhat.Text == "Phạt tiền/ca")
                                    {
                                        ms.pm_type_phat = 1;
                                    }
                                    else if (cboLyDoPhat.Text == "Phạt công/ca")
                                    {
                                        ms.pm_type_phat = 2;
                                    }

                                    ms._id = info._id;
                                    if (ms.pm_type == 1)
                                    {
                                        ms.pm_type_str = "Đi muộn";
                                    }
                                    else if (ms.pm_type == 2)
                                    {
                                        ms.pm_type_str = "Về sớm";
                                    }
                                    if (ms.pm_time_end == null)
                                    {
                                        ms.pm_time_end_str = "Chưa cập nhât";
                                    }
                                    else
                                    {
                                        if (textHienThiTGKetThuc.Text != "---- -- --")
                                        {
                                            string[] TGKetThuc = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                            string NamKT = TGKetThuc[TGKetThuc.Length - 1];
                                            string Thangkt = TGKetThuc[0];
                                            string[] ThangKT1 = Thangkt.Split(Convert.ToChar("g"));
                                            string ThangKT = ThangKT1[ThangKT1.Length - 1].Trim();
                                            ms.pm_time_end_str = Convert.ToDateTime(NamKT + "-" + ThangKT + "-01T00:00:00.000Z").ToString();
                                        }
                                    }
                                    if (ms.pm_type_phat == 1)
                                    {
                                        ms.pm_monney_str = ms.pm_monney + " VNĐ/ca";
                                    }
                                    else if (ms.pm_type_phat == 2)
                                    {
                                        ms.pm_monney_str = ms.pm_monney + " công/ca";
                                    }
                                    //frmDMVS.lstPhatMuon.Add(inf);
                                }
                            }
                            frmDMVS.dgv.ItemsSource = null;
                            frmDMVS.dgv.ItemsSource = frmDMVS.lstPhatMuon;
                            this.Visibility = Visibility.Collapsed;
                        }

                    }
                    catch
                    {

                    }
                }
            }
        }

        private void textThoiGian_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void textTienPhat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void tb_Luong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                if(tb.Name == "textThoiGian")
                {
                    validateNumberTime.Visibility = Visibility.Visible;
                }
                else
                {
                    validateNumberTP.Visibility = Visibility.Visible;
                }
                /*tb_ValidateLuong.Visibility = Visibility.Visible;
                tb_ValidateLuong.Text = "Bạn vui lòng nhập đúng % lương, không nhập ký tự khác!";*/
            }
            else
            {
                if(tb.Name == "textThoiGian")
                {
                    validateNumberTime.Visibility = Visibility.Collapsed;
                }
                else
                {
                    validateNumberTP.Visibility = Visibility.Collapsed;
                }
                //tb_ValidateLuong.Visibility = Visibility.Collapsed;
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
                if(tb.Name == "textThoiGian")
                {
                    validateNumberTime.Visibility = Visibility.Visible;
                }
                else
                {
                    validateNumberTP.Visibility = Visibility.Visible;
                }
                /*tb_ValidateLuong.Visibility = Visibility.Visible;
                tb_ValidateLuong.Text = "Bạn vui lòng nhập đúng % lương, không nhập ký tự khác!";*/
            }
            else
            {
                if(tb.Name == "textThoiGian")
                {
                    validateNumberTime.Visibility = Visibility.Collapsed;
                }
                else
                {
                    validateNumberTP.Visibility = Visibility.Collapsed;
                }
                //tb_ValidateLuong.Visibility = Visibility.Collapsed;
            }
        }
    }
}
