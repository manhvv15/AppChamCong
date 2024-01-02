using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.HoaHong;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings.HoaHong;
using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.HoaHong.CaiDatHoaHong
{
    /// <summary>
    /// Interaction logic for ucThemMoiCacLoaiHoaHong.xaml
    /// </summary>
    public partial class ucThemMoiCacLoaiHoaHong : UserControl
    {
        MainWindow Main;
        ucCaiDatHoaHongDoanhThu ucDoanhThu;
        ucCaiDatHoaHong ucCaiDatHH;
        public ucThemMoiCacLoaiHoaHong(MainWindow main, ucCaiDatHoaHongDoanhThu ucDoanhThu, ucCaiDatHoaHong uccaidathh)
        {
            InitializeComponent();
            Main = main;
            this.ucDoanhThu = ucDoanhThu;
            this.ucCaiDatHH = uccaidathh;
            textTieuDe.Text = "Thêm mới hoa hồng doanh thu";
            tb_textButton.Text = "Lưu";
            tb_NhapTien1.PreviewTextInput += tb_NhapTien1_PreviewTextInput;
            tb_NhapTien2.PreviewTextInput += tb_NhapTien2_PreviewTextInput;
            tb_NhapTien3.PreviewTextInput += tb_NhapTien3_PreviewTextInput;
        }
        ListThietLap lstRose2 = new ListThietLap();
        public ucThemMoiCacLoaiHoaHong(MainWindow main, ListThietLap lstrose2, ucCaiDatHoaHong uccaidathh, ucCaiDatHoaHongDoanhThu ucdoanhthu)
        {
            InitializeComponent();
            Main = main;
            ucCaiDatHH = uccaidathh;
            ucDoanhThu = ucdoanhthu;
            this.lstRose2 = lstrose2;
            tb_NhapTen.Text = lstrose2.tl_name;
            tb_NhapTien1.Text = lstrose2.tl_money_min.ToString();
            tb_NhapTien2.Text = lstrose2.tl_money_max.ToString();
            tb_NhapTien3.Text = lstrose2.tl_phan_tram.numberDecimal.ToString();
            tb_NhapTien1.PreviewTextInput += tb_NhapTien1_PreviewTextInput;
            tb_NhapTien2.PreviewTextInput += tb_NhapTien2_PreviewTextInput;
            tb_NhapTien3.PreviewTextInput += tb_NhapTien3_PreviewTextInput;
            textTieuDe.Text = "Chỉnh sửa hoa hồng doanh thu";
            tb_textButton.Text = "Cập nhật";
        }

        ucCaiDatHoaHongLoiNhuan ucLoiNhuan;
        public ucThemMoiCacLoaiHoaHong(MainWindow main, ucCaiDatHoaHongLoiNhuan ucLoiNhuan, ucCaiDatHoaHong uccaidathh)
        {
            InitializeComponent();
            Main = main;
            this.ucLoiNhuan = ucLoiNhuan;
            this.ucCaiDatHH = uccaidathh;
            tb_NhapTien1.PreviewTextInput += tb_NhapTien1_PreviewTextInput;
            stp_NhapTien2.Visibility = Visibility.Collapsed;
            stp_NhapTien3.Visibility = Visibility.Collapsed;
            stp_NhapTien1.Margin = new Thickness(20, 0, 20, 20);
            textTieuDe.Text = "Thêm mới hoa hồng lợi nhuận";
            tb_Tien1.Text = "Hoa hồng (VNĐ)";
            tb_textButton.Text = "Lưu";
        }
        string Tl_ChiPhi;
        string Tl_Name;
        string Tl_Id;
        public ucThemMoiCacLoaiHoaHong(MainWindow main, string tl_chiphi, string tl_Name, string tl_Id, ucCaiDatHoaHong uccaidathh)
        {
            InitializeComponent();
            Main = main;
            this.Tl_ChiPhi = tl_chiphi;
            this.Tl_Name = tl_Name;
            this.Tl_Id = tl_Id;
            this.ucCaiDatHH = uccaidathh;
            tb_NhapTen.Text = tl_Name;
            tb_NhapTien1.Text = tl_chiphi;
            tb_NhapTien1.PreviewTextInput += tb_NhapTien1_PreviewTextInput;
            stp_NhapTien2.Visibility = Visibility.Collapsed;
            stp_NhapTien3.Visibility = Visibility.Collapsed;
            stp_NhapTien1.Margin = new Thickness(20, 0, 20, 20);
            textTieuDe.Text = "Chỉnh sửa hoa hồng lợi nhuận";
            tb_textButton.Text = "Cập nhật";
            tb_Tien1.Text = "Chi phí";
           
        }

        ucCaiDatHoaHongLePhiViTri ucViTri;
        public ucThemMoiCacLoaiHoaHong(MainWindow main, ucCaiDatHoaHongLePhiViTri ucvitri, ucCaiDatHoaHong uccaidathh)
        {
            InitializeComponent();
            Main = main;
            this.ucViTri = ucvitri;
            this.ucCaiDatHH = uccaidathh;
            tb_NhapTien1.PreviewTextInput += tb_NhapTien1_PreviewTextInput;
            stp_NhapTien2.Visibility = Visibility.Collapsed;
            stp_NhapTien3.Visibility = Visibility.Collapsed;
            stp_NhapTien1.Margin = new Thickness(20, 0, 20, 20);
            textTieuDe.Text = "Thêm mới hoa hồng lệ phí vị trí";
            tb_Tien1.Text = "Hoa hồng (VNĐ)";
            tb_textButton.Text = "Lưu";
        }

        int Tl_HoaHong;
        string Tl_Name_ViTri;
        string Tl_Id_ViTri;
        public ucThemMoiCacLoaiHoaHong(MainWindow main, int tl_hoahong, string tl_Name_vitri, string tl_id_vitri, ucCaiDatHoaHong uccaidathh, ucCaiDatHoaHongLePhiViTri ucvitri)
        {
            InitializeComponent();
            Main = main;
            this.Tl_Id_ViTri = tl_id_vitri;
            this.Tl_HoaHong = tl_hoahong;
            this.Tl_Name_ViTri = tl_Name_vitri;
            ucCaiDatHH = uccaidathh;
            ucViTri = ucvitri;
            tb_NhapTen.Text = tl_Name_vitri;
            tb_NhapTien1.Text = tl_hoahong.ToString();
            tb_NhapTien1.PreviewTextInput += tb_NhapTien1_PreviewTextInput;
            stp_NhapTien2.Visibility = Visibility.Collapsed;
            stp_NhapTien3.Visibility = Visibility.Collapsed;
            stp_NhapTien1.Margin = new Thickness(20, 0, 20, 20);
            textTieuDe.Text = "Chỉnh sửa hoa hồng vị trí";
            tb_textButton.Text = "Cập nhật";
            tb_Tien1.Text = "Hoa hồng (VNĐ)";
        }
        ucCaiDatHoaHongKeHoach ucKeHoach;
        public ucThemMoiCacLoaiHoaHong(MainWindow main, ucCaiDatHoaHongKeHoach uckehoach, ucCaiDatHoaHong uccaidathh)
        {
            InitializeComponent();
            Main = main;
            this.ucKeHoach = uckehoach;
            tb_NhapTien1.PreviewTextInput += tb_NhapTien1_PreviewTextInput;
            tb_NhapTien2.PreviewTextInput += tb_NhapTien2_PreviewTextInput;
            this.ucCaiDatHH = uccaidathh;
            stp_NhapTien3.Visibility = Visibility.Collapsed;
            textTieuDe.Text = "Thêm mới hoa hồng kế hoạch";
            tb_Ten.Text = "Tên kế hoạch";
            tb_ChuAn.Text = "Nhập tên kế hoạch";
            tb_Tien1.Text = "Đạt KPI";
            tb_Tien2.Text = "Không đạt KPI";
            tb_textButton.Text = "Lưu";
        }

        int Tl_Kpi_No;
        int Tl_Kpi_Yes;
        string Tl_Name_KeHoach;
        string Tl_Id_KeHoach;
        public ucThemMoiCacLoaiHoaHong(MainWindow main, int tl_kpi_yes, int tl_kpi_no, string tl_Name_kehoach, string tl_Id_KeHoach, ucCaiDatHoaHong uccaidathh, ucCaiDatHoaHongKeHoach uckehoach)
        {
            InitializeComponent();
            Main = main;
            this.Tl_Kpi_Yes = tl_kpi_yes;
            this.Tl_Kpi_No = tl_kpi_no;
            Tl_Id_KeHoach = tl_Id_KeHoach;
            ucCaiDatHH = uccaidathh;
            ucKeHoach = uckehoach;
            this.Tl_Name_KeHoach = tl_Name_kehoach;
            tb_NhapTen.Text = tl_Name_kehoach;
            tb_NhapTien1.Text = tl_kpi_yes.ToString();
            tb_NhapTien2.Text = tl_kpi_no.ToString();
            tb_NhapTien1.PreviewTextInput += tb_NhapTien1_PreviewTextInput;
            tb_NhapTien2.PreviewTextInput += tb_NhapTien2_PreviewTextInput;
            stp_NhapTien3.Visibility = Visibility.Collapsed;
            textTieuDe.Text = "Chỉnh sửa hoa hồng kế hoạch";
            tb_textButton.Text = "Cập nhật";
            tb_Ten.Text = "Tên kế hoạch";
            tb_Tien1.Text = "Đạt KPI";
            tb_Tien2.Text = "Không đại KPI";
            
        }

        private void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility=Visibility.Collapsed;
        }
        string ErrorSytem;
        private async void btnThemMoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                if (tb_NhapTen.Text == "" || string.IsNullOrEmpty(tb_NhapTen.Text))
                {
                    tb_validateTen.Visibility = Visibility.Visible;
                    tb_validateTen.Text = "Bạn vui lòng nhập tên!";
                    allow = false;
                }
                else
                {
                    tb_validateTen.Visibility = Visibility.Collapsed;
                }
                if (tb_NhapTien1.Text == "" || string.IsNullOrEmpty(tb_NhapTien1.Text))
                {
                    tb_validateTien1.Visibility = Visibility.Visible;
                    tb_validateTien1.Text = "Bạn vui lòng nhập vào số tiền!";
                    allow = false;
                }
                else
                {
                    tb_validateTien1.Visibility = Visibility.Collapsed;
                }
                if (stp_NhapTien2.Visibility == Visibility.Visible)
                {
                    if (tb_NhapTien2.Text == "" || string.IsNullOrEmpty(tb_NhapTien2.Text))
                    {
                        tb_validateTien2.Visibility = Visibility.Visible;
                        tb_validateTien2.Text = "Bạn vui lòng nhập vào số tiền!";
                        allow = false;
                    }
                    else
                    {
                        tb_validateTien2.Visibility = Visibility.Collapsed;
                    }
                }
                if (stp_NhapTien3.Visibility == Visibility.Visible)
                {
                    if (tb_NhapTien3.Text == "" || tb_NhapTien3.Text == null)
                    {
                        tb_validateTien3.Visibility = Visibility.Visible;
                        tb_validateTien3.Text = "Bạn vui lòng nhập vào số tiền!";
                        allow = false;
                    }
                    else
                    {
                        tb_validateTien3.Visibility = Visibility.Collapsed;
                    }
                }
                if (ucDoanhThu != null)
                {
                    int.TryParse(tb_NhapTien1.Text, out int value1);
                    int.TryParse(tb_NhapTien2.Text, out int value2);
                    if (value1 > value2)
                    {
                        tb_validateTienTong.Visibility = Visibility.Visible;
                        tb_validateTienTong.Text = "Doanh thu nhỏ nhất không được lớn hơn doanh thu lớn nhất";
                        allow = false;
                    }
                    else
                    {
                        tb_validateTienTong.Visibility = Visibility.Collapsed;
                    }
                }
                if (allow)
                {
                    if (tb_textButton.Text == "Lưu")
                    {
                        try
                        {
                            var client = new HttpClient();
                            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/them_thiet_lap");
                            var content = new MultipartFormDataContent();
                            content.Add(new StringContent(Main.IdAcount.ToString()), "tl_id_com");
                            if (ucDoanhThu != null)
                            {
                                content.Add(new StringContent(tb_NhapTen.Text), "tl_name");
                                content.Add(new StringContent(tb_NhapTien1.Text), "tl_money_min");
                                content.Add(new StringContent(tb_NhapTien2.Text), "tl_money_max");
                                content.Add(new StringContent(tb_NhapTien3.Text), "tl_phan_tram");
                                content.Add(new StringContent("2"), "tl_id_rose");
                                content.Add(new StringContent("0"), "tl_chiphi");
                                content.Add(new StringContent("0"), "tl_hoahong");
                                content.Add(new StringContent("0"), "tl_kpi_yes");
                                content.Add(new StringContent("0"), "tl_kpi_no");
                               
                            }
                            else if (ucLoiNhuan != null)
                            {
                                content.Add(new StringContent("3"), "tl_id_rose");
                                content.Add(new StringContent(tb_NhapTen.Text), "tl_name");
                                content.Add(new StringContent("0"), "tl_money_min");
                                content.Add(new StringContent("0"), "tl_money_max");
                                content.Add(new StringContent("0"), "tl_phan_tram");
                                content.Add(new StringContent(tb_NhapTien1.Text), "tl_chiphi");
                                content.Add(new StringContent("0"), "tl_hoahong");
                                content.Add(new StringContent("0"), "tl_kpi_yes");
                                content.Add(new StringContent("0"), "tl_kpi_no");
                            }
                            else if (ucViTri != null)
                            {
                                content.Add(new StringContent("4"), "tl_id_rose");
                                content.Add(new StringContent(tb_NhapTen.Text), "tl_name");
                                content.Add(new StringContent("0"), "tl_money_min");
                                content.Add(new StringContent("0"), "tl_money_max");
                                content.Add(new StringContent("0"), "tl_phan_tram");
                                content.Add(new StringContent("0"), "tl_chiphi");
                                content.Add(new StringContent(tb_NhapTien1.Text), "tl_hoahong");
                                content.Add(new StringContent("0"), "tl_kpi_yes");
                                content.Add(new StringContent("0"), "tl_kpi_no");
                            }
                            if (ucKeHoach != null)
                            {
                                content.Add(new StringContent("5"), "tl_id_rose");
                                content.Add(new StringContent(tb_NhapTen.Text), "tl_name");
                                content.Add(new StringContent("0"), "tl_money_min");
                                content.Add(new StringContent("0"), "tl_money_max");
                                content.Add(new StringContent("0"), "tl_phan_tram");
                                content.Add(new StringContent("0"), "tl_chiphi");
                                content.Add(new StringContent("0"), "tl_hoahong");
                                content.Add(new StringContent(tb_NhapTien1.Text), "tl_kpi_yes");
                                content.Add(new StringContent(tb_NhapTien2.Text), "tl_kpi_no");
                            }
                            content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                            request.Content = content;
                            var response = await client.SendAsync(request);
                            var resConten = await response.Content.ReadAsStringAsync();
                            if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                            {
                                if (ucDoanhThu != null)
                                {
                                    ucDoanhThu.LoadDoanhThu();
                                }
                                else if (ucLoiNhuan != null)
                                {
                                    ucLoiNhuan.LoadHHLoiNhuan();
                                }
                                else if (ucViTri != null)
                                {
                                    ucViTri.LoadHHViTri();
                                }
                                else if (ucKeHoach != null)
                                {
                                    ucKeHoach.LoadHHKeHoach();
                                }
                                Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, ucCaiDatHH));
                                this.Visibility = Visibility.Collapsed;
                            }
                        }
                        catch (Exception)
                        {
                            ErrorSytem = "Error";
                            Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                        }
                    }
                    else
                    {
                        if (textTieuDe.Text == "Chỉnh sửa hoa hồng doanh thu")
                        {
                            try
                            {
                                var client = new HttpClient();
                                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/capnhat_thiet_lap_minmaxphantram");
                                var content = new MultipartFormDataContent();
                                content.Add(new StringContent(lstRose2.tl_id.ToString()), "tl_id");
                                content.Add(new StringContent(tb_NhapTien1.Text), "tl_money_min");
                                content.Add(new StringContent(tb_NhapTien2.Text), "tl_money_max");
                                content.Add(new StringContent(tb_NhapTen.Text), "tl_name");
                                content.Add(new StringContent(tb_NhapTien3.Text), "tl_phan_tram");
                                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                                request.Content = content;
                                var response = await client.SendAsync(request);
                                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                                {
                                    var resConten = await response.Content.ReadAsStringAsync();
                                    ucDoanhThu.LoadDoanhThu();
                                    this.Visibility = Visibility.Collapsed;
                                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, ucDoanhThu));
                                }
                            }
                            catch (Exception)
                            {
                                ErrorSytem = "Error";
                                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                            }
                        }
                        else if(textTieuDe.Text == "Chỉnh sửa hoa hồng lợi nhuận")
                        {
                            try
                            {
                                var client = new HttpClient();
                                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/capnhat_thiet_lap_chiphi");
                                var content = new MultipartFormDataContent();
                                content.Add(new StringContent(tb_NhapTien1.Text), "tl_chiphi");
                                content.Add(new StringContent("0"), "tl_hoahong");
                                content.Add(new StringContent(Tl_Id), "tl_id");
                                content.Add(new StringContent(tb_NhapTen.Text), "tl_name");
                                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                                request.Content = content;
                                var response = await client.SendAsync(request);
                                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                                {
                                    var resContent = await response.Content.ReadAsStringAsync();
                                    ucLoiNhuan.LoadHHLoiNhuan();    
                                    this.Visibility = Visibility.Collapsed;
                                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, ucLoiNhuan));
                                }
                            }
                            catch (Exception)
                            {
                                ErrorSytem = "Error";
                                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                            }
                        }
                        else if (textTieuDe.Text == "Chỉnh sửa hoa hồng vị trí")
                        {
                            try
                            {
                                var client = new HttpClient();
                                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/capnhat_thiet_lap_chiphi");
                                var content = new MultipartFormDataContent();
                                content.Add(new StringContent("0"), "tl_chiphi");
                                content.Add(new StringContent(tb_NhapTien1.Text), "tl_hoahong");
                                content.Add(new StringContent(Tl_Id_ViTri), "tl_id");
                                content.Add(new StringContent(tb_NhapTen.Text), "tl_name");
                                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                                request.Content = content;
                                var response = await client.SendAsync(request);
                                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                                {
                                    var resContent = await response.Content.ReadAsStringAsync();
                                    ucViTri.LoadHHViTri();
                                    this.Visibility = Visibility.Collapsed;
                                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, ucViTri));
                                }
                            }
                            catch (Exception)
                            {
                                ErrorSytem = "Error";
                                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                            }
                        }
                        else
                        {
                            try
                            {
                                var client = new HttpClient();
                                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/capnhat_thiet_lap_kpi");
                                var content = new MultipartFormDataContent();
                                content.Add(new StringContent(Tl_Id_KeHoach), "tl_id");
                                content.Add(new StringContent(tb_NhapTien2.Text), "tl_kpi_no");
                                content.Add(new StringContent(tb_NhapTien1.Text), "tl_kpi_yes");
                                content.Add(new StringContent(tb_NhapTen.Text), "tl_name");
                                content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                                request.Content = content;
                                var response = await client.SendAsync(request);
                                if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                                {
                                   var resContent = await response.Content.ReadAsStringAsync();
                                    ucKeHoach.LoadHHKeHoach();
                                    this.Visibility = Visibility.Collapsed;
                                    Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, ucKeHoach));
                                }
                            }
                            catch (Exception)
                            {
                                ErrorSytem = "Error";
                                Main.grShowPopup.Children.Add(new ucThongBaoAll(ErrorSytem));
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {}
        }

        private void tb_NhapTien1_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tb_NhapTien2_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void tb_NhapTien3_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

    }
}
