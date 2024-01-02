using QuanLyChung365TruocDangNhap.ChamCongNew.Popup.ChamCong;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using RestSharp;
using Newtonsoft.Json;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Linq;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatPhucLoi
{
    /// <summary>
    /// Interaction logic for ucThemMoiPhucLoi.xaml
    /// </summary>
    public partial class ucThemMoiPhucLoi : UserControl
    {
        BrushConverter br = new BrushConverter();
        public static int static_month, static_year;
        int month, year;
        private string Month = "";
        private MainWindow Main;
        private ucCaiDatPhucLoi frmSettingPL;
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf PL;
        public ucThemMoiPhucLoi(MainWindow main, ucCaiDatPhucLoi uc)
        {
            InitializeComponent();
            Main = main;
            frmSettingPL = uc;
            textTieuDe.Text = "Thêm mới phúc lợi";
            textDieuKien.Text = "Thêm mới";
            LoadDatePicker();
        }
        public ucThemMoiPhucLoi(MainWindow main, ucCaiDatPhucLoi uc, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf pl)
        {
            InitializeComponent();
            Main = main;
            frmSettingPL = uc;
            PL = pl;
            textTieuDe.Text = "Chỉnh sửa phúc lợi";
            textDieuKien.Text = "Cập nhật";
            textNhapTenPL.Text = pl.cl_name;
            textNhapSoTienPL.Text = pl.cl_salary.ToString();
            cboTypeTN.Text = pl.cl_type_tax_s;
            LoadDatePicker();
            textHienThiTGBatDau.Text = pl.cl_day.ToString("MM/yyyy");
            if(pl.Display_cl_day_end != "Chưa cập nhật")
            {
                textHienThiTGKetThuc.Text = pl.cl_day_end.ToString("MM/yyyy");
            }
            textNhapGhiChu.Text = pl.cl_note;
        }

        public void LoadDatePicker()
        {
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
        #region Popup Lich
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
        #endregion

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodThoat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void bodChonLoaiPhucLoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void lsvLoadInsurance_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void bodCancel_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodCancel_MouseEnter(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbHuyBo.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodCancel_MouseLeave(object sender, MouseEventArgs e)
        {
            bodCancel.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbHuyBo.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }


        private void bodThemMoiPhucLoi_MouseEnter(object sender, MouseEventArgs e)
        {
            bodThemMoiPhucLoi.BorderThickness = new Thickness(1);
        }

        private void bodThemMoiPhucLoi_MouseLeave(object sender, MouseEventArgs e)
        {
            bodThemMoiPhucLoi.BorderThickness = new Thickness(0);
        }

        private async void bodThemMoiPhucLoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (string.IsNullOrEmpty(textNhapTenPL.Text))
            {
                allow = false;
                validateName.Visibility= Visibility.Visible;
            }
            else validateName.Visibility = Visibility.Collapsed;
            if (string.IsNullOrEmpty(textNhapSoTienPL.Text))
            {
                validateTien.Text = "Vui lòng nhập tiền phúc lợi!";
                allow = false;
                validateTien.Visibility = Visibility.Visible;
            }
            else if (!IsNumeric(textNhapSoTienPL.Text))
            {
                validateTien.Text = "Tiền phúc lợi phải là số!";
                allow = false;
                validateTien.Visibility = Visibility.Visible;
            }
            else validateTien.Visibility = Visibility.Collapsed;
            if(string.IsNullOrEmpty(textHienThiTGBatDau.Text) || textHienThiTGBatDau.Text == "---- --- ----")
            {
                allow = false;
                validateTime.Visibility= Visibility.Visible;
            }
            else validateTime.Visibility = Visibility.Collapsed;
            if(cboTypeTN.SelectedIndex < 0)
            {
                allow= false;
                validateLoai.Visibility= Visibility.Visible;
            }
            else validateLoai.Visibility = Visibility.Collapsed;
            if (allow)
            {
                if (textTieuDe.Text == "Thêm mới phúc lợi")
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_phuc_loi")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("cl_com", Main.IdAcount);
                            request.AddParameter("cl_name", textNhapTenPL.Text);
                            request.AddParameter("cl_salary", textNhapSoTienPL.Text);
                            string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                            string y = day[day.Length - 1];
                            string m = day[0].Trim();
                            request.AddParameter("cl_day", y + "-" + m + "-" + "01T00:00:00.000+00:00");
                            if(textHienThiTGKetThuc.Text != "---- --- ----")
                            {
                                string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                string ykt = daykt[daykt.Length - 1];
                                string mkt = daykt[0].Trim();
                                request.AddParameter("cl_day_end", ykt + "-" + mkt + "-" + "01T00:00:00.000+00:00");
                            }
                            //else request.AddParameter("cl_day_end", "1970-01-01T00:00:00.000+00:00");
                            request.AddParameter("cl_active", 1);
                            request.AddParameter("cl_note", textNhapGhiChu.Text);
                            request.AddParameter("cl_type", 3);
                            if (cboTypeTN.Text == "Thu nhập chịu thuế")
                            {
                                request.AddParameter("cl_type_tax", 0);

                            }
                            else if (cboTypeTN.Text == "Thu nhập miễn thuế")
                            {
                                request.AddParameter("cl_type_tax", 1);
                            }
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            RestResponse resAlbum = restclient.Execute(request);
                            var b = resAlbum.Content;
                            OOP.CaiDatLuong.PhucLoi.AddPhucLoi.Root PhucLoi = JsonConvert.DeserializeObject<OOP.CaiDatLuong.PhucLoi.AddPhucLoi.Root>(b);
                            if (PhucLoi.newobj != null)
                            {
                                OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf pv = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelf();
                                pv._id = PhucLoi.newobj._id;
                                pv.cl_id = PhucLoi.newobj.cl_id;
                                pv.cl_name = PhucLoi.newobj.cl_name;
                                pv.cl_salary = PhucLoi.newobj.cl_salary;
                                pv.cl_day = PhucLoi.newobj.cl_day;
                                pv.cl_day_end = PhucLoi.newobj.cl_day_end;
                                pv.cl_active = PhucLoi.newobj.cl_active.ToString();
                                pv.cl_note = PhucLoi.newobj.cl_note;
                                pv.cl_type = PhucLoi.newobj.cl_type;
                                pv.cl_type_tax = PhucLoi.newobj.cl_type_tax;
                                if (PhucLoi.newobj.cl_type_tax == 0)
                                {
                                    pv.cl_type_tax_s = "Thu nhập chịu thuế";
                                }
                                else if (PhucLoi.newobj.cl_type_tax == 1)
                                {
                                    pv.cl_type_tax_s = "Thu nhập miễn thuế";
                                }
                                pv.cl_com = PhucLoi.newobj.cl_com;
                                frmSettingPL.lstPhucLoi.Insert(0,pv);
                                frmSettingPL.dgvCaiDatPhucLoi.ItemsSource = null;
                                frmSettingPL.dgvCaiDatPhucLoi.ItemsSource = frmSettingPL.lstPhucLoi;
                                this.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    catch(Exception ex)
                    {

                    }
                }
                else if (textTieuDe.Text == "Chỉnh sửa phúc lợi")
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/sua_phuc_loi")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("cl_id", PL.cl_id);
                            request.AddParameter("cl_name", textNhapTenPL.Text);
                            request.AddParameter("cl_salary", textNhapSoTienPL.Text);
                            string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                            string y = day[day.Length - 1].Trim();
                            string m = day[0].Trim();
                            if (int.Parse(m) < 10)
                            {
                                request.AddParameter("cl_day", y + "-" + "0" + m);
                            }
                            else
                            {
                                request.AddParameter("cl_day", y + "-" + m);
                            }
                            if(textHienThiTGKetThuc.Text != "---- --- ----")
                            {
                                string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                string ykt = daykt[daykt.Length - 1];
                                string mkt = daykt[0].Trim();
                                if (int.Parse(mkt) < 10)
                                {
                                    request.AddParameter("cl_day_end", ykt + "-" + "0" + mkt);
                                }
                                else
                                {
                                    request.AddParameter("cl_day_end", ykt + "-" + mkt);
                                }
                            }
                            else request.AddParameter("cl_day_end", "1970-01");
                            request.AddParameter("cl_active", 1);
                            request.AddParameter("cl_note", textNhapGhiChu.Text);
                            request.AddParameter("cl_type", 3);
                            if (cboTypeTN.Text == "Thu nhập chịu thuế")
                            {
                                request.AddParameter("cl_type_tax", 0);

                            }
                            else if (cboTypeTN.Text == "Thu nhập miễn thuế")
                            {
                                request.AddParameter("cl_type_tax", 1);
                            }
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            RestResponse resAlbum = await restclient.ExecuteAsync(request);
                            var b = resAlbum.Content;
                            foreach (var item in frmSettingPL.lstPhucLoi)
                            {
                                if (item.cl_id == PL.cl_id)
                                {
                                    item._id = PL._id;

                                    item.cl_name = textNhapTenPL.Text;
                                    item.cl_salary = int.Parse(textNhapSoTienPL.Text);
                                    item.cl_day = Convert.ToDateTime(m + "/" + y);
                                    if(textHienThiTGKetThuc.Text != "---- --- ----")
                                    {
                                        string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                        string ykt = daykt[daykt.Length - 1];
                                        string mkt = daykt[0].Trim();
                                        item.cl_day_end = Convert.ToDateTime(mkt + "/" + ykt);
                                    }
                                    item.cl_active = "1";
                                    item.cl_note = textNhapGhiChu.Text;
                                    item.cl_type = 3;
                                    if (cboTypeTN.Text == "Thu nhập chịu thuế")
                                    {
                                        item.cl_type_tax = 0;

                                    }
                                    else if (cboTypeTN.Text == "Thu nhập miễn thuế")
                                    {
                                        item.cl_type_tax = 1;
                                    }
                                    item.cl_type_tax_s = cboTypeTN.Text;
                                }
                            }
                            frmSettingPL.dgvCaiDatPhucLoi.ItemsSource = null;
                            frmSettingPL.dgvCaiDatPhucLoi.ItemsSource = frmSettingPL.lstPhucLoi;
                            this.Visibility = Visibility.Collapsed;
                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void tb_Luong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            TextBox tb = sender as TextBox;
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                validateTien.Text = "Tiền phúc lợi phải là số!";
                validateTien.Visibility = Visibility.Visible;
            }
            else
            {
                validateTien.Visibility = Visibility.Collapsed;
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
                validateTien.Text = "Tiền phúc lợi phải là số!";
                validateTien.Visibility = Visibility.Visible;
            }
            else
            {
                validateTien.Visibility = Visibility.Collapsed;
            }
        }
    }
}
