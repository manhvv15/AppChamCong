using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using QuanLyChung365TruocDangNhap.ChamCongNew.TimeKeeping;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
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
    /// Interaction logic for PopUpThemMoiChinhSuaPhuCapTheoCa.xaml
    /// </summary>
    public partial class PopUpThemMoiChinhSuaPhuCapTheoCa : UserControl
    {
        private string Month;
        private MainWindow Main;
        public List<OOP.CaiDatLuong.clsShift.Item> lstShift = new List<OOP.CaiDatLuong.clsShift.Item>();
        private frmDanhSachPhuCap frmDSPC;
        public OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift wfShift { get; set; }
        public PopUpThemMoiChinhSuaPhuCapTheoCa(MainWindow main, string TieuDe, frmDanhSachPhuCap pc)
        {
            InitializeComponent();
            Main = main;
            textTieuDe.Text = TieuDe;
            frmDSPC = pc;
            LoadDLCaLV();
            LoadDatePicker();
        }
        public PopUpThemMoiChinhSuaPhuCapTheoCa(MainWindow main, string TieuDe, frmDanhSachPhuCap pc, OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift wfShift)
        {
            InitializeComponent();
            Main = main;
            textTieuDe.Text = TieuDe;
            frmDSPC = pc;
            //LoadDLCaLV();
            LoadDatePicker();
            this.wfShift = wfShift;
            textHienThiTGBatDau.Text = wfShift.Display_wf_time;
            if(wfShift.Display_wf_time_end != "Chưa cập nhật")
            {
                textHienThiTGKetThuc.Text = wfShift.Display_wf_time_end;
            }
            textTienPC.Text = wfShift.wf_money.ToString();
            stGhiChu.Visibility = Visibility.Collapsed;
            stCaLamViec.Visibility = Visibility.Collapsed;
            //cboCaLVApDung.SelectedIndex = lstShift.FindIndex(x => x.shift_id == wfShift.shift.shift_id);
            textThemMoiCS.Text = "Chỉnh sửa";
        }

        private void LoadDLCaLV()
        {
            try
            {
                using (HttpClient httpClient = new HttpClient())
                {
                    //string url = Properties.Resources.URL + "listGroupCustomer";
                    string url = "http://210.245.108.202:3000/api/qlc/shift/list";
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                    var kq = httpClient.GetAsync(url);
                    OOP.CaiDatLuong.clsShift.Root CaLV = JsonConvert.DeserializeObject<OOP.CaiDatLuong.clsShift.Root>(kq.Result.Content.ReadAsStringAsync().Result);
                    if (CaLV.data != null)
                    {
                        foreach (var calv in CaLV.data.items)
                        {
                            lstShift.Add(calv);
                            cboCaLVApDung.Items.Add("(" + calv.shift_id + ")" + "-" + calv.shift_name);
                        }
                    }
                }
            }
            catch
            {

            }
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

        private void btnHuyBo_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private async void btnThemMoiCS_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                if(cboCaLVApDung.SelectedIndex < 0 && wfShift == null)
                {
                    allow = false;
                    validateCa.Visibility = Visibility.Visible;
                }
                else validateCa.Visibility = Visibility.Collapsed;
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
                if (string.IsNullOrEmpty(textHienThiTGBatDau.Text) || textHienThiTGBatDau.Text == "---- --- ----")
                {
                    allow = false;
                    validateTime.Visibility = Visibility.Visible;
                }
                else validateTime.Visibility = Visibility.Collapsed;
                if (allow)
                {
                    if(wfShift == null)
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_wf_shift")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            string month, year;
                            month = textHienThiTGBatDau.Text.Split('/')[0];
                            year = textHienThiTGBatDau.Text.Split('/')[1];
                            request.AddParameter("wf_time", year + "-" + month);
                            if (textHienThiTGKetThuc.Text != "---- --- ----")
                            {
                                string monthKT, yearKT;
                                monthKT = textHienThiTGKetThuc.Text.Split('/')[0];
                                yearKT = textHienThiTGKetThuc.Text.Split('/')[1];
                                if (int.Parse(monthKT) < 10)
                                {
                                    request.AddParameter("wf_time_end", yearKT + "-0" + (int.Parse(monthKT)).ToString());
                                }
                                else
                                {
                                    request.AddParameter("wf_time_end", yearKT + "-" + (int.Parse(monthKT)).ToString());

                                }
                            }
                            else
                            {
                                request.AddParameter("wf_time_end", "1970-01-01T00:00:00.000+00:00");
                            }
                            string[] shifts = cboCaLVApDung.Text.Split(Convert.ToChar(")"));
                            string s = shifts[0];
                            string[] str = s.Split(Convert.ToChar("("));
                            string shift = str[str.Length - 1];
                            request.AddParameter("wf_shift", shift);
                            request.AddParameter("wf_money", textTienPC.Text);
                            request.AddParameter("wf_com", Main.IdAcount);
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            RestResponse resAlbum = await restclient.ExecuteAsync(request);
                            var b = resAlbum.Content;
                            OOP.CaiDatLuong.PhucLoi.AddPCShift.Root DSPL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.PhucLoi.AddPCShift.Root>(b);
                            if (DSPL.newobj != null)
                            {
                                OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift obj = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.WfShift();
                                obj.wf_com = DSPL.newobj.wf_com;
                                obj.wf_id = DSPL.newobj.wf_id;
                                obj.wf_money = DSPL.newobj.wf_money;
                                obj.wf_shift = DSPL.newobj.wf_shift.ToString();
                                obj.wf_time = DSPL.newobj.wf_time;
                                obj.wf_time_end = DSPL.newobj.wf_time_end;
                                obj._id = DSPL.newobj._id;
                                obj.shift = new OOP.CaiDatLuong.PhucLoi.clsDSPhucLoi.Shift();
                                obj.shift.shift_id = int.Parse(shift);
                                obj.shift.shift_name = cboCaLVApDung.Text.Split('-')[1];
                                frmDSPC.lstPCTC.Insert(0, obj);
                                frmDSPC.dgvTheoCa.ItemsSource = null;
                                frmDSPC.dgvTheoCa.ItemsSource = frmDSPC.lstPCTC;
                                this.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                    else
                    {
                        using (RestClient restclient = new RestClient(new Uri("https://api.timviec365.vn/api/tinhluong/congty/edit_wf_shift")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            string month, year;
                            month = textHienThiTGBatDau.Text.Split('/')[0];
                            year = textHienThiTGBatDau.Text.Split('/')[1];
                            request.AddParameter("wf_time", year + "-" + month);
                            if (textHienThiTGKetThuc.Text != "---- --- ----")
                            {
                                string monthKT, yearKT;
                                monthKT = textHienThiTGKetThuc.Text.Split('/')[0];
                                yearKT = textHienThiTGKetThuc.Text.Split('/')[1];
                                if (int.Parse(monthKT) < 10)
                                {
                                    request.AddParameter("wf_time_end", yearKT + "-0" + (int.Parse(monthKT)).ToString());
                                }
                                else
                                {
                                    request.AddParameter("wf_time_end", yearKT + "-" + (int.Parse(monthKT)).ToString());

                                }
                            }
                            else
                            {
                                request.AddParameter("wf_time_end", "1970-01-01T00:00:00.000+00:00");
                            }
                            request.AddParameter("wf_money", textTienPC.Text);
                            request.AddParameter("wf_id", wfShift.wf_id);
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            RestResponse resAlbum = await restclient.ExecuteAsync(request);
                            var b = resAlbum.Content;
                            OOP.CaiDatLuong.PhucLoi.AddPCShift.Root DSPL = JsonConvert.DeserializeObject<OOP.CaiDatLuong.PhucLoi.AddPCShift.Root>(b);
                            if (DSPL.newobj != null)
                            {
                                var wf = frmDSPC.lstPCTC.Find(x => x.wf_id == wfShift.wf_id);
                                wf.wf_money = DSPL.newobj.wf_money;
                                wf.wf_time = DSPL.newobj.wf_time;
                                wf.wf_time_end = DSPL.newobj.wf_time_end;
                                frmDSPC.dgvTheoCa.ItemsSource = null;
                                frmDSPC.dgvTheoCa.ItemsSource = frmDSPC.lstPCTC;
                                this.Visibility = Visibility.Collapsed;
                            }
                        }
                    }
                }
            }
            catch
            {

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
    }
}
