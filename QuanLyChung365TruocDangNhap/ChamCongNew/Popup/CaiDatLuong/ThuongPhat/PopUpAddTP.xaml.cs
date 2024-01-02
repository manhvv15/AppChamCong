using QuanLyChung365TruocDangNhap.ChamCongNew.Common;
using QuanLyChung365TruocDangNhap.ChamCongNew.SalarySettings;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.clsNhanVienThuocCongTy;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.ThuongPhat
{
    /// <summary>
    /// Interaction logic for PopUpAddTP.xaml
    /// </summary>
    public partial class PopUpAddTP : UserControl
    {
        private MainWindow Main;
        private frmThuongPhat frmTP;
         List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> dataFinals = new List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal>();
        public PopUpAddTP(MainWindow main, frmThuongPhat frm, List<OOP.CaiDatLuong.ThuongPhat.clsDSThuongPhat.DataFinal> dataf)
        {
            InitializeComponent();
            Main = main;
            frmTP = frm;
            dataFinals = dataf;
            dp_ThoiGianThuongPhat.SelectedDate = DateTime.Now;

            LoadDLNhanVien();
        }
        private string IdNV;
        private void Staff_SelectionChange(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cboNV.SelectedItem != null)
                {
                    cboNV.PlaceHolderForground = "#474747";
                    var chonca = ((ListUser)cboNV.SelectedItem).idQLC.ToString();
                    IdNV = chonca;
                }
                else
                {
                    cboNV.PlaceHolderForground = "#ACACAC";
                }
            }
            catch (Exception)
            {}
        }
        private void LoadDLNhanVien()
        {
            Main.lstNhanVienThuocCongTy.RemoveAt(0);
            cboNV.ItemsSource = Main.lstNhanVienThuocCongTy;
        }
        private void textTienTP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                tb_validateSoTien.Visibility = Visibility.Visible;
                tb_validateSoTien.Text = "Hãy nhập đúng định dạng số tiền!";
            }
            else
            {
                tb_validateSoTien.Visibility = Visibility.Collapsed;
            }
        }
        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }
        private  void Rectangle_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private async void btnThemMoi_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                if (cboNV.Text == null && cboNV.PlaceHolder == "Chọn nhân viên" || cboNV.Text == "")
                {
                    tb_validateChonNhanVien.Visibility = Visibility.Visible;
                    tb_validateChonNhanVien.Text = "Bạn vui lòng chọn nhân viên!";
                    allow = false;
                }
                else
                {
                    tb_validateChonNhanVien.Visibility = Visibility.Collapsed;
                }
                if (cboTP.Text == null || cboNV.SelectedItem == null)
                {
                    tb_validateLoaiThuongPhat.Visibility = Visibility.Visible;
                    tb_validateLoaiThuongPhat.Text = "Bạn vui lòng chọn loại thưởng phạt!";
                    allow = false;
                }
                else
                {
                    tb_validateLoaiThuongPhat.Visibility = Visibility.Collapsed;
                }
                if (string.IsNullOrEmpty(textTienTP.Text) || textTienTP.Text == "")
                {
                    tb_validateSoTien.Visibility = Visibility.Visible;
                    tb_validateSoTien.Text = "Bạn vui lòng nhập số tiền thưởng phạt!";
                    allow = false;
                }
                else
                {
                    tb_validateSoTien.Visibility = Visibility.Collapsed;
                }
                if (string.IsNullOrEmpty(textLyDo.Text) || textLyDo.Text == "")
                {
                    tb_validateLyDo.Visibility = Visibility.Visible;
                    tb_validateLyDo.Text = "Bạn vui lòng nhập lý do thưởng / phạt!";
                    allow = false;
                }
                else
                {
                    tb_validateLyDo.Visibility = Visibility.Collapsed;
                }
                if (allow)
                {
                    try
                    {
                        var client = new HttpClient();
                        var request = new HttpRequestMessage(HttpMethod.Post, "https://api.timviec365.vn/api/tinhluong/congty/insert_thuong_phat");
                        var content = new MultipartFormDataContent();
                        content.Add(new StringContent(IdNV), "pay_id_user");
                        content.Add(new StringContent(Main.IdAcount.ToString()), "pay_id_com");
                        content.Add(new StringContent(textTienTP.Text), "pay_price");
                        if (cboTP.Text == "Thưởng")
                        {
                            content.Add(new StringContent("1"), "pay_status");
                        }
                        else if (cboTP.Text == "Phạt")
                        {
                            content.Add(new StringContent("2"), "pay_status");

                        }
                        content.Add(new StringContent(textLyDo.Text), "pay_case");
                        string day = dp_ThoiGianThuongPhat.SelectedDate.Value.Day.ToString();
                        string months = dp_ThoiGianThuongPhat.SelectedDate.Value.Month.ToString();
                        string years = dp_ThoiGianThuongPhat.SelectedDate.Value.Year.ToString();
                        content.Add(new StringContent($"{years}-{months}-{day}"), "pay_day");
                        content.Add(new StringContent(months), "pay_month");
                        content.Add(new StringContent(years), "pay_year");
                        content.Add(new StringContent(Properties.Settings.Default.Token), "token");
                        request.Content = content;
                        var response = await client.SendAsync(request);
                        if (response.EnsureSuccessStatusCode().IsSuccessStatusCode)
                        {
                            var resConten = await response.Content.ReadAsStringAsync();
                            this.Visibility = Visibility.Collapsed;
                            frmTP.LoadDLThuongPhat();
                            Main.grShowPopup.Children.Add(new ucThongBaoAll(Main, dataFinals, IdNV));
                        }
                    }
                    catch (Exception)
                    { }
                }
            }
            catch (Exception)
            {}
        }

        private void btnClose_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void btn_HuyBo_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }
    }
}
