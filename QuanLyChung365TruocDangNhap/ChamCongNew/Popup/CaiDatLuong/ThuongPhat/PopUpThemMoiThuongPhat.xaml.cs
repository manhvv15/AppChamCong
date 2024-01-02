using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.ThuongPhat;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
//using Irony;
using Newtonsoft.Json;
//using NPOI.XWPF.UserModel;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Windows.Threading;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ThuongPhat
{
    /// <summary>
    /// Interaction logic for PopUpThemMoiThuongPhat.xaml
    /// </summary>
    public partial class PopUpThemMoiThuongPhat : UserControl
    {
        private MainWindow Main;
        private int stt;
       
        BrushConverter br = new BrushConverter();
        public OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal thuongP = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal();
        private OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong Thuong = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong();
        private OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat Phat = new clsDSThuongPhat.DsPhat();
        private frmThuongPhat FrmTP;
        public PopUpThemMoiThuongPhat(MainWindow main, OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp, OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong thuong, clsDSThuongPhat.DsPhat phat, frmThuongPhat frmTP)
        {
            InitializeComponent();
            Main = main;
            FrmTP = frmTP;
            Thuong = thuong;
            Phat = phat;
            thuongP = tp;
            LoadDSThuong();
            LoadDSPhat();
            dgvThuong.Visibility = Visibility.Visible;
            dgvPhat.Visibility = Visibility.Visible;
            dgvPhatCong.Visibility = Visibility.Collapsed;
            stp_TienThuongPhat.Visibility = Visibility.Visible;
            stp_LoaiThuongPhat.Visibility = Visibility.Visible;
            stp_TongSoCong.Visibility = Visibility.Collapsed;
            stp_ChonCaPhat.Visibility = Visibility.Collapsed;
            stp_ChiTietCongPhat.Visibility = Visibility.Collapsed;
            dtpNgayAD.SelectedDate = DateTime.Now;
            stt = 1;
            tb_TextThuongPhat.Text = "Tiền thưởng";
            tb_ButTonThuongPhat.Text = "Lưu";
            if (thuong != null)
            {
                textTienTP.Text = thuong.pay_price_formatted.ToString();
                textGhiChu.Text = thuong.pay_case.ToString();
                dtpNgayAD.Part_TextBox.Text = thuong.pay_day.ToString();
                stt = 1; tb_TextThuongPhat.Text = "Tiền thưởng";
                tb_ButTonThuongPhat.Text = "Cập nhật";
                btnHuy.Visibility = Visibility.Visible;
            }
            if (phat != null)
            {
                textTienTP.Text = phat.pay_price_formatted.ToString();
                textGhiChu.Text = phat.pay_case.ToString();
                dtpNgayAD.Part_TextBox.Text = phat.pay_day.ToString();
                stt = 1; tb_TextThuongPhat.Text = "Tiền phạt";
                tb_ButTonThuongPhat.Text = "Cập nhật";
                btnHuy.Visibility = Visibility.Visible;
            }
            FrmTP = frmTP;
        }


        private OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong PhatCong = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong();
        public PopUpThemMoiThuongPhat(MainWindow main, OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal tp, OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong phatcong, frmThuongPhat frmTP)
        {
            InitializeComponent();
            Main = main;
            FrmTP = frmTP;
            PhatCong = phatcong;
            thuongP = tp;
            dgvThuong.Visibility = Visibility.Collapsed;
            dgvPhat.Visibility = Visibility.Collapsed;
            stp_TienThuongPhat.Visibility = Visibility.Collapsed;
            stp_LoaiThuongPhat.Visibility = Visibility.Collapsed;
            stp_TongSoCong.Visibility = Visibility.Collapsed;
            stp_ChonCaPhat.Visibility = Visibility.Visible;
            dtpNgayAD.SelectedDate = DateTime.Now;
            stt = 1;
            LoadDSCaNhanVien();
            LoadDSPhatCong();
            if (phatcong == null)
            {
                tb_ButTonThuongPhat.Text = "Thêm Phạt";
                stt = 1;
                textTieuDe.Text = "Phạt công";
                btnHuy.Visibility = Visibility.Collapsed;
                cboCaNV.SelectedIndex = 0;
                if (cboCaNV.SelectedIndex > 0)
                {
                    stp_TongSoCong.Visibility = Visibility.Visible;
                }
            }
            else
            {
                stt = 3;
                tb_ButTonThuongPhat.Text = "Cập nhật phạt";
                btnHuy.Visibility = Visibility.Collapsed;
                stp_ChiTietCongPhat.Visibility = Visibility.Collapsed;
                stp_ChonCaPhat.Visibility = Visibility.Visible;
                stp_TongSoCong.Visibility = Visibility.Visible;
                Shift_Id_Edit = phatcong.shifts.shift_id.ToString();
                ID_PhatCong = phatcong.id_phatcong.ToString();
                cboCaNV.Text = phatcong.shifts.shift_id.ToString();
                textGhiChu.Text = phatcong.ly_do;
                dtpNgayAD.Part_TextBox.Text = phatcong.phatcong_time;
                tb_TongPhatCOng.Text = phatcong.shifts.num_to_calculate.numberDecimal;
            }
        }

        private void dtp_ChonNgayApDung(object sender, SelectionChangedEventArgs e)
        {
            LoadDSCaNhanVien();
        }
        List<List_Shift_Nv> list_Shifts = new List<List_Shift_Nv>();
        string ep_id;
        public async void LoadDSCaNhanVien()
        {
            try
            {
                dayss = dtpNgayAD.SelectedDate.Value.Day;
                monthss = dtpNgayAD.SelectedDate.Value.Month;
                yearss = dtpNgayAD.SelectedDate.Value.Year;
                list_Shifts.Clear();
                var client = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/vanthu/dexuat/empShiftInDay");
                request.Headers.Add("Authorization", "Bearer " + Properties.Settings.Default.Token);
                var content = new MultipartFormDataContent();
                
                ep_id = thuongP.inforUser.idQLC.ToString();
                
                content.Add(new StringContent(ep_id), "ep_id");
                if (dayss < 10)
                {
                    content.Add(new StringContent($"{yearss}-{monthss}-0{dayss}"), "day");
                } 
                else
                {
                    content.Add(new StringContent($"{yearss}-{monthss}-{dayss}"), "day");
                }
               
                request.Content = content;
                var response = await client.SendAsync(request);
                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                {
                    var resConten =  await response.Content.ReadAsStringAsync();
                    Root_Shift_Nv DsCaNv = JsonConvert.DeserializeObject<Root_Shift_Nv>(resConten);
                    if (DsCaNv.list != null)
                    {
                        list_Shifts = DsCaNv.list;
                        lsvDanhSachCaNV.ItemsSource = DsCaNv.list;
                        List_Shift_Nv canv = new List_Shift_Nv();
                        canv.shift_id = 0;
                        canv.shift_name = "Cả ngày";
                        list_Shifts.Insert(0, canv);
                        cboCaNV.ItemsSource = list_Shifts;
                    }
                   
                }
            }
            catch (Exception)
            { 
            }
        } 

        clsDSThuongPhat.DsThuong ThuongIsForm = new clsDSThuongPhat.DsThuong();
        public void LoadDSThuong()
        {
            try
            {
                DateTime aDateTime1;
                foreach (var item in thuongP.tt_thuong.ds_thuong)
                {
                    DateTime.TryParse(item.pay_day, out aDateTime1);
                    item.pay_day = aDateTime1.ToString("dd-MM-yyyy");
                }
                dgvThuong.ItemsSource = thuongP.tt_thuong.ds_thuong;
            }
            catch (Exception)
            {}
        }
        public void LoadDSPhat()
        {
            try
            {
                DateTime aDateTime2;
                foreach (var item in thuongP.tt_phat.ds_phat)
                {
                    DateTime.TryParse(item.pay_day, out aDateTime2);
                    item.pay_day = aDateTime2.ToString("dd-MM-yyyy");
                }
                dgvPhat.ItemsSource = thuongP.tt_phat.ds_phat;
            }
            catch (Exception)
            {} 
        }
        public void LoadDSPhatCong()
        {
            try
            {
                DateTime aDateTime2;
                foreach (var item in thuongP.tt_phat_cong.ds_phat_cong)
                {
                    DateTime.TryParse(item.phatcong_time, out aDateTime2);
                    item.phatcong_time = aDateTime2.ToString("dd-MM-yyyy");
                }
                dgvPhatCong.ItemsSource = thuongP.tt_phat_cong.ds_phat_cong;
            }
            catch (Exception)
            { }
        }
        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void RadioTienThuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            borVienTT.BorderBrush = (Brush)bc.ConvertFrom("#4c5bd4");
            borTienThuong.Background= (Brush)bc.ConvertFrom("#4c5bd4");
            borTienPhat.Background = (Brush)bc.ConvertFrom("#c4c4c4");
            borVienTP.BorderBrush = (Brush)bc.ConvertFrom("#c4c4c4");
            tb_TextThuongPhat.Text = "Tiền thưởng";
            stt = 1;
            //btnChinhSuaThuong_MouseLeftButtonUp(sender, e);
        }

        private void RadioTienPhat_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            borVienTT.BorderBrush = (Brush)bc.ConvertFrom("#c4c4c4");
            borTienThuong.Background = (Brush)bc.ConvertFrom("#c4c4c4");
            borTienPhat.Background = (Brush)bc.ConvertFrom("#4c5bd4");
            borVienTP.BorderBrush = (Brush)bc.ConvertFrom("#4c5bd4");
            tb_TextThuongPhat.Text = "Tiền phạt";
            stt = 2;
            //btnChinhSua_MouseLeftButtonUp(sender, e);
        }
        
        string days; string months; string years;
        int dayss; int monthss; int yearss;
        string Notication;
        string date_time;
        private async void btnThemTP_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                if (stp_TienThuongPhat.Visibility == Visibility.Visible)
                {
                    if (textTienTP.Text == "" || string.IsNullOrEmpty(textTienTP.Text))
                    {
                        errorTextBox.Visibility = Visibility.Visible;
                        errorTextBox.Text = "Bạn vui lòng nhập số tiền!";
                        allow = false;
                    }
                    else
                    {
                        errorTextBox.Visibility = Visibility.Collapsed;
                    }
                }
                if (stp_LyDo.Visibility == Visibility.Visible)
                {
                    if (textGhiChu.Text == "" || string.IsNullOrEmpty(textGhiChu.Text))
                    {
                        tb_varidateGhiChu.Visibility = Visibility.Visible;
                        tb_varidateGhiChu.Text = "Bạn vui lòng nhập lý do!";
                        allow = false;
                    }
                    else
                    {
                        tb_varidateGhiChu.Visibility = Visibility.Collapsed;
                    }
                }
                if (allow) 
                {
                    if (tb_ButTonThuongPhat.Text == "Lưu")
                    {
                        try
                        {
                            using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_thuong_phat")))
                            {
                                RestRequest request = new RestRequest();
                                request.Method = Method.Post;
                                request.AlwaysMultipartFormData = true;
                                request.AddParameter("pay_id_user", thuongP.inforUser.idQLC);
                                request.AddParameter("pay_id_com", Main.IdAcount);
                                request.AddParameter("pay_price", textTienTP.Text);
                                request.AddParameter("pay_status", stt);
                                request.AddParameter("pay_case", textGhiChu.Text);
                                days = dtpNgayAD.SelectedDate.Value.Day.ToString();
                                months = dtpNgayAD.SelectedDate.Value.Month.ToString();
                                years = dtpNgayAD.SelectedDate.Value.Year.ToString();
                                if (int.Parse(days) < 10 && int.Parse(months) < 10)
                                {
                                    request.AddParameter("pay_day", $"{years}-{months}-{days}");

                                }
                                else if (int.Parse(days) > 10 && int.Parse(months) < 10)
                                {
                                    request.AddParameter("pay_day", $"{years}-{months}-{days}");
                                }
                                else if (int.Parse(days) < 10 && int.Parse(months) > 10)
                                {
                                    request.AddParameter("pay_day", $"{years}-{months}-{days}");
                                }
                                else
                                {
                                    request.AddParameter("pay_day", $"{years}-{months}-{days}");

                                }
                                request.AddParameter("pay_month", months);
                                request.AddParameter("pay_year", years);
                                request.AddParameter("token", Properties.Settings.Default.Token);
                                RestResponse resAlbum = restclient.Execute(request);
                                var b = resAlbum.Content;
                                OOP.CaiDatLuong.ThuongPhat.clsAddTP.Root add = JsonConvert.DeserializeObject<OOP.CaiDatLuong.ThuongPhat.clsAddTP.Root>(b);
                                if (add.data != null)
                                {
                                    if (add.data.newobj1.pay_status == 1)
                                    {
                                        OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong dst = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong();
                                        dst.pay_id = add.data.newobj1.pay_id;
                                        dst.pay_case = add.data.newobj1.pay_case;
                                        dst.pay_day = add.data.newobj1.pay_day;
                                        dst.pay_group = add.data.newobj1.pay_group;
                                        dst.pay_id_com = add.data.newobj1.pay_id_com;
                                        dst.pay_id_user = add.data.newobj1.pay_id_user;
                                        dst.pay_month = add.data.newobj1.pay_month;
                                        dst.pay_nghi_le = add.data.newobj1.pay_nghi_le;
                                        dst.pay_price = add.data.newobj1.pay_price;
                                        dst.pay_status = add.data.newobj1.pay_status;
                                        dst.pay_time_created = add.data.newobj1.pay_time_created;
                                        dst.pay_year = add.data.newobj1.pay_year;
                                        dst._id = add.data.newobj1._id;
                                        thuongP.tt_thuong.ds_thuong.Add(dst);
                                        dgvThuong.ItemsSource = null;
                                        DateTime aDateTime1;
                                        foreach (var item in thuongP.tt_thuong.ds_thuong)
                                        {
                                            DateTime.TryParse(item.pay_day, out aDateTime1);
                                            item.pay_day = aDateTime1.ToString("dd-MM-yyyy");
                                        }
                                        dgvThuong.ItemsSource = thuongP.tt_thuong.ds_thuong;
                                        this.Visibility = Visibility.Collapsed;
                                       
                                    }
                                    else if (add.data.newobj1.pay_status == 2)
                                    {
                                        OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat dtp = new OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat();
                                        dtp.pay_id = add.data.newobj1.pay_id;
                                        dtp.pay_case = add.data.newobj1.pay_case;
                                        dtp.pay_day = add.data.newobj1.pay_day;
                                        dtp.pay_group = add.data.newobj1.pay_group;
                                        dtp.pay_id_com = add.data.newobj1.pay_id_com;
                                        dtp.pay_id_user = add.data.newobj1.pay_id_user;
                                        dtp.pay_month = add.data.newobj1.pay_month;
                                        dtp.pay_nghi_le = add.data.newobj1.pay_nghi_le;
                                        dtp.pay_price = add.data.newobj1.pay_price;
                                        dtp.pay_status = add.data.newobj1.pay_status;
                                        dtp.pay_time_created = add.data.newobj1.pay_time_created;
                                        dtp.pay_year = add.data.newobj1.pay_year;
                                        dtp._id = add.data.newobj1._id;
                                        thuongP.tt_phat.ds_phat.Add(dtp);
                                        dgvPhat.ItemsSource = null;
                                        DateTime aDateTime2;
                                        foreach (var item in thuongP.tt_phat.ds_phat)
                                        {
                                            DateTime.TryParse(item.pay_day, out aDateTime2);
                                            item.pay_day = aDateTime2.ToString("dd-MM-yyyy");
                                        }
                                        dgvPhat.ItemsSource = thuongP.tt_phat.ds_phat;
                                        this.Visibility = Visibility.Collapsed;
                                        
                                    }
                                    FrmTP.LoadLaiThemThuongPhat();
                                }
                            }

                        }
                        catch
                        { }
                    }
                    else if (tb_ButTonThuongPhat.Text == "Thêm Phạt")
                    {
                        try
                        {
                            days = dtpNgayAD.SelectedDate.Value.Day.ToString();
                            months = dtpNgayAD.SelectedDate.Value.Month.ToString();
                            years = dtpNgayAD.SelectedDate.Value.Year.ToString();
                            if (int.Parse(days) < 10 && int.Parse(days) < 10)
                            {
                                 date_time = $"{years}-0{months}-0{days}";
                            }
                            else if (int.Parse(months) >= 10 && int.Parse(days) < 10)
                            {
                                date_time = $"{years}-{years}-0{years}";
                            }
                            else if (int.Parse(months) < 10 && int.Parse(days) >= 10)
                            {
                                date_time = $"{years}-0{months}-{days}";
                            }
                            else
                            {
                                date_time = $"{years}-{months}-{days}"; ;
                            }
                            
                            string Ly_Do = textGhiChu.Text;
                            string Com_Id = Main.IdAcount.ToString();
                            var client = new HttpClient();
                            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/phatcong");
                            var DataObject = new
                            {
                                token = Properties.Settings.Default.Token,
                                phatcong_time = date_time,
                                list_shift_phatcong = Shift_Id_List,
                                ly_do = Ly_Do,
                                ep_id = ep_id,
                                com_id = Com_Id
                            };
                            string json = JsonConvert.SerializeObject(DataObject);
                            var content = new StringContent(json, Encoding.UTF8, "application/json"); 
                            request.Content = content;
                            var response = await client.SendAsync(request);
                            var resConten = await response.Content.ReadAsStringAsync();
                            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                            {
                          
                                //FrmTP.LoadDLThuongPhat();
                                Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this, thuongP.inforUser.userName));
                                this.Visibility = Visibility.Collapsed;
                                FrmTP.LoadLaiThemThuongPhat();
                            }
                        }
                        catch (Exception)
                        {
                            string ErrorSytem = "";
                            Main.gridPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                        }
                    }
                    else if (tb_ButTonThuongPhat.Text == "Cập nhật phạt")
                    {
                        try
                        {
                            days = dtpNgayAD.SelectedDate.Value.Day.ToString();
                            months = dtpNgayAD.SelectedDate.Value.Month.ToString();
                            years = dtpNgayAD.SelectedDate.Value.Year.ToString();
                            var client = new HttpClient();
                            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/edit_phatcong");
                            var content = new MultipartFormDataContent();
                            content.Add(new StringContent(ID_PhatCong), "id_phatcong");
                            content.Add(new StringContent(textGhiChu.Text), "ly_do");
                            content.Add(new StringContent(Shift_Id_Edit), "phatcong_shift");
                            content.Add(new StringContent($"{years}-{months}-{days}"), "phatcong_time");
                            content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                            request.Content = content;
                            var response = await client.SendAsync(request);
                            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                            {
                                var resConten = await response.Content.ReadAsStringAsync();
                                //FrmTP.LoadDLThuongPhat();
                                dgvPhatCong.Items.Refresh();
                                Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, this, thuongP.inforUser.userName, Notication));
                                this.Visibility = Visibility.Collapsed;
                                FrmTP.LoadLaiThemThuongPhat();
                            }
                        }
                        catch (Exception)
                        {
                            string ErrorSytem = "";
                            Main.gridPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                        }
                    }
                    else
                    {
                        try
                        {
                            var client = new HttpClient();
                            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/edit_thuong_phat");
                            var content = new MultipartFormDataContent();
                            content.Add(new StringContent(textTienTP.Text.Replace(",","")), "pay_price");
                            content.Add(new StringContent(stt.ToString()), "pay_status");
                            content.Add(new StringContent(textGhiChu.Text), "pay_case");
                            days = dtpNgayAD.SelectedDate.Value.Day.ToString();
                            months = dtpNgayAD.SelectedDate.Value.Month.ToString();
                            years = dtpNgayAD.SelectedDate.Value.Year.ToString();
                            if (int.Parse(days) < 10 && int.Parse(months) < 10)
                            {
                                content.Add(new StringContent($"{years}-{months}-{days}"), "pay_day");

                            }
                            else if (int.Parse(days) > 10 && int.Parse(months) < 10)
                            {
                                content.Add(new StringContent($"{years}-{months}-{days}"), "pay_day");
                            }
                            else if (int.Parse(days) < 10 && int.Parse(months) > 10)
                            {
                                content.Add(new StringContent($"{years}-{months}-{days}"), "pay_day");
                            }
                            else
                            {
                                content.Add(new StringContent($"{years}-{months}-{days}"), "pay_day");

                            }
                            content.Add(new StringContent(months), "pay_month");
                            content.Add(new StringContent(years), "pay_year");
                            if (Thuong != null)
                            {
                                content.Add(new StringContent(Thuong.pay_id.ToString()), "pay_id");
                            }
                            else if (Phat != null)
                            {
                                content.Add(new StringContent(Phat.pay_id.ToString()), "pay_id");
                            }
                            else
                            {
                                if (Pay_Id_Thuong != null) 
                                {
                                    content.Add(new StringContent(Pay_Id_Thuong), "pay_id"); 
                                }
                                else if(Pay_Id_Phat != null)
                                {
                                    content.Add(new StringContent(Pay_Id_Phat), "pay_id");
                                }
                                else
                                {
                                    Pay_Id_Phat = Pay_Id_Thuong;
                                    content.Add(new StringContent(Pay_Id_Phat), "pay_id");
                                }
                            }
                            content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                            request.Content = content;
                            var response = await client.SendAsync(request);
                            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                            {
                                var resConten = await response.Content.ReadAsStringAsync();
                                if (Thuong != null)
                                {
                                    LoadDSThuong();
                                    FrmTP.borChiTietThuongNV.Visibility = Visibility.Collapsed;
                                    this.Visibility = Visibility.Collapsed;
                                    FrmTP.popupTP.Visibility = Visibility.Collapsed;
                                    FrmTP.popup.Visibility = Visibility.Collapsed;
                                }
                                else
                                {
                                    LoadDSPhat();
                                    FrmTP.borChiTietPhatNV.Visibility = Visibility.Collapsed;
                                    this.Visibility = Visibility.Collapsed;
                                    FrmTP.popupTP.Visibility = Visibility.Collapsed;
                                    FrmTP.popup.Visibility = Visibility.Collapsed;
                                }
                                FrmTP.LoadLaiThemThuongPhat();
                            }
                        }
                        catch (Exception)
                        { }
                    }
                }
            }
            catch (Exception)
            {}
        }

        private void dgvThuong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }

        private void dgvPhat_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }
        private void dgvPhatCong_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            scroll.ScrollToVerticalOffset(scroll.VerticalOffset - e.Delta);
        }
        private void btnXoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat phat = (sender as Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhat;
                if (phat != null)
                {
                    Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaThuongPhat(Main, phat, "Bạn có chắc chăn muốn xoá mức phạt này không?", this, null));
                }
            }
            catch (Exception)
            {}
        }
        private void bod_Xoa_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong thuong = (sender as Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsThuong;
                if (thuong != null)
                {
                    Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaThuongPhat(Main, thuong, "Bạn có chắc chăn muốn xoá mức thưởng này không?", this, null));
                }
            }
            catch (Exception)
            { }
        }

        private void btnXoaPhatCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong phatcong = (sender as Border).DataContext as OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DsPhatCong;
                if (phatcong != null)
                {
                    Main.grShowPopup.Children.Add(new PopUpHoiTruocKhiXoaThuongPhat(Main, phatcong, "Bạn có chắc chăn muốn xoá phạt công này không?", this, null));
                }
            }
            catch (Exception)
            { }
        }
        private void textTienTP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                errorTextBox.Visibility = Visibility.Visible;
                errorTextBox.Text = "Hãy nhập số tiền không được nhập chữ!";
            }
            else
            {
                errorTextBox.Visibility = Visibility.Collapsed;
            }
        }
        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        string Pay_Id_Thuong; string Pay_Id_Phat;
        private void btnChinhSuaThuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);
                stt = 1;
                clsDSThuongPhat.DsThuong isThuong = (sender as Border).DataContext as clsDSThuongPhat.DsThuong;
                //if (stt == 1)
                //{
                //    Pay_Id_Thuong = isThuong.pay_id.ToString();
                //}
                Pay_Id_Thuong = isThuong.pay_id.ToString();
                if (row != null)
                {
                    TextBlock tb_Pay_Price = FindChild<TextBlock>(row, "tb_Pay_Price");
                    TextBlock tb_Pay_Day = FindChild<TextBlock>(row, "tb_Pay_Day");
                    TextBlock tb_Pay_Case = FindChild<TextBlock>(row, "tb_Pay_Case");
                    if (tb_Pay_Price != null && tb_Pay_Day != null && tb_Pay_Case != null)
                    {
                        textTienTP.Text = tb_Pay_Price.Text;
                        textGhiChu.Text = tb_Pay_Case.Text;
                        dtpNgayAD.Part_TextBox.Text = tb_Pay_Day.Text;
                    }
                }
                borTienThuong.Background = (Brush)br.ConvertFrom("#4c5bd4");
                borTienPhat.Background = (Brush)br.ConvertFrom("#C4C4C4");
                tb_ButTonThuongPhat.Text = "Cập nhật";
                stp_ChiTietCongPhat.Visibility = Visibility.Collapsed;
                tb_TextThuongPhat.Text = "Tiền thưởng";
                btnHuy.Visibility = Visibility.Visible;
            }
            catch (Exception)
            { }
        }
        private void btnChinhSua_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);
                stt = 2;
                clsDSThuongPhat.DsPhat isThuong = (sender as Border).DataContext as clsDSThuongPhat.DsPhat;
                Pay_Id_Phat = isThuong.pay_id.ToString();
                if (row != null)
                {
                    TextBlock tb_Pay_Price_Phat = FindChild<TextBlock>(row, "tb_Pay_Price_Phat");
                    TextBlock tb_Pay_Day_Phat = FindChild<TextBlock>(row, "tb_Pay_Day_Phat");
                    TextBlock tb_Pay_Case_Phat = FindChild<TextBlock>(row, "tb_Pay_Case_Phat");
                    if (tb_Pay_Price_Phat != null && tb_Pay_Day_Phat != null && tb_Pay_Case_Phat != null)
                    {
                        textTienTP.Text = tb_Pay_Price_Phat.Text;
                        textGhiChu.Text = tb_Pay_Case_Phat.Text;
                        dtpNgayAD.Part_TextBox.Text = tb_Pay_Day_Phat.Text;
                    }
                }
                borTienPhat.Background = (Brush)br.ConvertFrom("#4c5bd4");
                borTienThuong.Background = (Brush)br.ConvertFrom("#C4C4C4");
                stp_ChiTietCongPhat.Visibility = Visibility.Collapsed;
                tb_ButTonThuongPhat.Text = "Cập nhật";
                tb_TextThuongPhat.Text = "Tiền phạt";
                btnHuy.Visibility = Visibility.Visible;
            }
            catch (Exception)
            {}  
        }

        string ID_PhatCong;
        string Shift_Id_Edit;
        private void btnChinhSuaPhatCong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                stp_ChiTietCongPhat.Visibility = Visibility.Collapsed;
                stp_ChonCaPhat.Visibility = Visibility.Visible;
                stp_TongSoCong.Visibility = Visibility.Visible;
                DataGridRow row = FindAncestor<DataGridRow>((DependencyObject)e.OriginalSource);
                clsDSThuongPhat.DsPhatCong isPhatCong = (sender as Border).DataContext as clsDSThuongPhat.DsPhatCong;
                ID_PhatCong = isPhatCong.id_phatcong.ToString();
                Shift_Id_Edit = isPhatCong.shifts.shift_id.ToString();
                if (row != null)
                {
                    TextBlock tb_shift_name = FindChild<TextBlock>(row, "tb_shift_name");
                    TextBlock tb_phatcong_time = FindChild<TextBlock>(row, "tb_phatcong_time");
                    TextBlock tb_ly_do = FindChild<TextBlock>(row, "tb_ly_do");
                    if (tb_shift_name != null && tb_phatcong_time != null && tb_ly_do != null)
                    {
                        cboCaNV.Text = tb_shift_name.Text;
                        textGhiChu.Text = tb_ly_do.Text;
                        dtpNgayAD.Part_TextBox.Text = tb_phatcong_time.Text;
                        tb_TongPhatCOng.Text = isPhatCong.shifts.num_to_calculate.numberDecimal;
                    }
                }
                stt = 3;
                tb_ButTonThuongPhat.Text = "Cập nhật phạt";
                btnHuy.Visibility = Visibility.Collapsed;
            }
            catch (Exception)
            { }
        }
        private void btnHuy_MouseLeftButtonUp_1(object sender, MouseButtonEventArgs e)
        {
            tb_ButTonThuongPhat.Text = "Lưu";
            btnHuy.Visibility = Visibility.Collapsed;
            textTienTP.Text = "";
            textGhiChu.Text = "";
            dtpNgayAD.SelectedDate = DateTime.Now;
        }

        #region Comons
        // Hàm giúp tìm kiếm đối tượng con trong VisualTree
        private T FindChild<T>(DependencyObject parent, string childName) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);

                if (child is T typedChild && (child as FrameworkElement)?.Name == childName)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindChild<T>(child, childName);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }
        //// Hàm giúp tìm cha của một đối tượng trong VisualTree
        private T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }



        #endregion

      

      

        int Shift_Id;
        double SumCong;
        List<int> Shift_Id_List = new List<int>();
        string List_Id_Shift;
        private void cboCaNV_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List_Shift_Nv caNv = (List_Shift_Nv)cboCaNV.SelectedItem;
            Shift_Id_List.Clear();
            if (caNv != null)
            {
                Shift_Id = caNv.shift_id;
                if (Shift_Id != 0)
                {
                    stp_TongSoCong.Visibility = Visibility.Visible;
                    stp_ChiTietCongPhat.Visibility = Visibility.Collapsed;
                    tb_TongPhatCOng.Text = "Số công: " + caNv.num_to_calculate.numberDecimal;
                    //List_Id_Shift = "[" + string.Join(", ", caNv.shift_id) + "]";
                    Shift_Id_List.Add(caNv.shift_id);
                }
                else
                {
                    foreach (var item in list_Shifts)
                    {
                        if (item.shift_id != 0)
                        {
                            SumCong += double.Parse(item.num_to_calculate.numberDecimal);
                            Shift_Id_List.Add(item.shift_id);
                        }
                    }
                    //List_Id_Shift = "[" + string.Join(", ", Shift_Id_List) + "]";
                    tb_TongPhatCOng.Text = "Tổng số công: " + SumCong.ToString();
                    stp_TongSoCong.Visibility = Visibility.Visible;
                    stp_ChiTietCongPhat.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
