using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.PhuCap
{
    /// <summary>
    /// Interaction logic for PopUpThemMoiChinhSuaPhuCap.xaml
    /// </summary>
    public partial class PopUpThemMoiChinhSuaPhuCap : UserControl
    {
        private string Month;
        private MainWindow Main;
        private frmDanhSachPhuCap frmDSPC;
        private OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa phucap = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa();
        public PopUpThemMoiChinhSuaPhuCap(MainWindow main, string TieuDe, frmDanhSachPhuCap pc)
        {
            InitializeComponent();
            Main = main;
            frmDSPC = pc;
            textTieuDe.Text = TieuDe;
            LoadDatePicker();
        }
        public PopUpThemMoiChinhSuaPhuCap(MainWindow main, string TieuDe, frmDanhSachPhuCap pc, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa phuc)
        {
            InitializeComponent();
            Main = main;
            frmDSPC = pc;
            textTieuDe.Text = TieuDe;
            phucap = phuc;
            textTenPC.Text = phuc.cl_name;
            textTienPC.Text = phuc.cl_salary.ToString();
            cboTypeTN.Text = phuc.cl_type_tax_s;
            LoadDatePicker();
            string[] dtp = phuc.cl_day.ToString().Split(Convert.ToChar("/"));
            string[] dtpY = dtp[2].Split(new string[] { "12:00:00 AM" }, StringSplitOptions.None);
            string Y = dtpY[0].Trim();
            if (int.Parse(dtp[0]) < 10)
                textHienThiTGBatDau.Text = "0" + dtp[0] + "/" + Y;
            else 
                textHienThiTGBatDau.Text = dtp[0] + "/" + Y;
            string[] dtpkt = phuc.cl_day_end.ToString().Split(Convert.ToChar("/"));
            string[] dtpYkt = dtpkt[2].Split(new string[] { "12:00:00 AM" }, StringSplitOptions.None);
            string Ykt = dtpYkt[0].Trim();
            if (int.Parse(dtpkt[0]) < 10)
                textHienThiTGKetThuc.Text = "0" + dtpkt[0] + "/" + Ykt;
            else 
                textHienThiTGKetThuc.Text = dtpkt[0] + "/" + Ykt;
            textGhiChu.Text = phuc.cl_note;
            textTMCS.Text = "Cập nhật";
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

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnThemMoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnThemMoiChinhSua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (string.IsNullOrEmpty(textTenPC.Text))
            {
                allow = false;
                validateName.Visibility= Visibility.Visible;
            }
            else validateName.Visibility = Visibility.Collapsed;
            if (string.IsNullOrEmpty(textTienPC.Text))
            {
                validateTien.Text = "Vui lòng nhập tiền phụ cấp!";
                allow = false;
                validateTien.Visibility = Visibility.Visible;
            }
            else if (!IsNumeric(textTienPC.Text))
            {
                validateTien.Text = "Tiền phụ cấp phải là số!";
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
                if (textTieuDe.Text == "Thêm mới phụ cấp")
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_phuc_loi")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("cl_com", Main.IdAcount);
                            request.AddParameter("cl_name", textTenPC.Text);
                            request.AddParameter("cl_salary", textTienPC.Text);
                            string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                            string y = day[day.Length - 1];
                            string m = day[0].Trim();
                            request.AddParameter("cl_day", y + "-" + m + "-" + "01T00:00:00.000+00:00");
                            if (textHienThiTGKetThuc.Text != "---- --- ----")
                            {
                                string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                string ykt = daykt[daykt.Length - 1].Trim();
                                string mkt = daykt[0].Trim();
                                request.AddParameter("cl_day_end", ykt + "-" + mkt + "-" + "01T00:00:00.000+00:00");
                            }
                            request.AddParameter("cl_active", 1);
                            request.AddParameter("cl_note", textGhiChu.Text);
                            request.AddParameter("cl_type", 4);
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
                                OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa pv = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.ListWelfa();
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
                                frmDSPC.lstPC.Insert(0,pv);
                                frmDSPC.dgv.ItemsSource = null;
                                frmDSPC.dgv.ItemsSource = frmDSPC.lstPC;
                                this.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    catch
                    {

                    }
                }
                else if (textTieuDe.Text == "Chỉnh sửa phụ cấp")
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/sua_phuc_loi")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("cl_id", phucap.cl_id);
                            request.AddParameter("cl_name", textTenPC.Text);
                            request.AddParameter("cl_salary", textTienPC.Text);
                            string[] day = textHienThiTGBatDau.Text.Split(Convert.ToChar("/"));
                            string y = day[day.Length - 1].Trim();
                            string m = day[0].Trim();
                            request.AddParameter("cl_day", y + "-" + m + "-" + "01T00:00:00.000+00:00");
                            if (textHienThiTGKetThuc.Text != "---- --- ----")
                            {
                                string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                string ykt = daykt[daykt.Length - 1];
                                string mkt = daykt[0].Trim();
                                request.AddParameter("cl_day_end", ykt + "-" + mkt + "-" + "01T00:00:00.000+00:00");
                            }
                            request.AddParameter("cl_active", 1);
                            request.AddParameter("cl_note", textGhiChu.Text);
                            request.AddParameter("cl_type", 4);
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
                            foreach (var item in frmDSPC.lstPC)
                            {
                                if (item.cl_id == phucap.cl_id)
                                {
                                    item._id = phucap._id;

                                    item.cl_name = textTenPC.Text;
                                    item.cl_salary = int.Parse(textTienPC.Text);
                                    item.cl_day = Convert.ToDateTime(m + "/" + y);
                                    if (textHienThiTGKetThuc.Text != "---- --- ----")
                                    {
                                        string[] daykt = textHienThiTGKetThuc.Text.Split(Convert.ToChar("/"));
                                        string ykt = daykt[daykt.Length - 1];
                                        string mkt = daykt[0].Trim();
                                        item.cl_day_end = Convert.ToDateTime(mkt + "/" + ykt);
                                    }
                                    item.cl_active = "1";
                                    item.cl_note = textGhiChu.Text;
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
                            frmDSPC.dgv.ItemsSource = null;
                            frmDSPC.dgv.ItemsSource = frmDSPC.lstPC;
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
                validateTien.Text = "Tiền phụ cấp phải là số!";
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
                validateTien.Text = "Tiền phụ cấp phải là số!";
                validateTien.Visibility = Visibility.Visible;
            }
            else
            {
                validateTien.Visibility = Visibility.Collapsed;
            }
        }

        private void huyBo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }
    }
}
