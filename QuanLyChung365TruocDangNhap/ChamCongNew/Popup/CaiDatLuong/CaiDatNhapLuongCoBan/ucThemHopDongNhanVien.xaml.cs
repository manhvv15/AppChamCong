using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Collections.ObjectModel;
using Microsoft.Win32;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using System.Linq;
using System;
using System.Net;
using System.Text;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongBaoHiem;
using Newtonsoft.Json;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for ucThemHopDongNhanVien.xaml
    /// </summary>
    public partial class ucThemHopDongNhanVien : UserControl
    {
        public class Attachment
        {
            public string FileName { get; set; }
        }
        ObservableCollection<Attachment> Attachments = new ObservableCollection<Attachment>();
        BrushConverter br = new BrushConverter();
        //private OOP.clsNhanVienThuocCongTy.ListUser clsLuongCB = new OOP.clsNhanVienThuocCongTy.ListUser();
        private ListResult_CDL clsLuongCB = new ListResult_CDL();
        private clsLuongBaoHiem.DataContract HopDongNV = new clsLuongBaoHiem.DataContract();
        private ucHoSoNhanVien frmHoSoNV;
        private MainWindow Main;
        public ucThemHopDongNhanVien(MainWindow main, ListResult_CDL cls, ucHoSoNhanVien uc)
        {
            InitializeComponent();
            clsLuongCB = cls;
            frmHoSoNV = uc;
            Main = main;
            tb_NgayHieuLuc.SelectedDate = DateTime.Now;
        }

        public ucThemHopDongNhanVien(MainWindow main, ListResult_CDL cls, ucHoSoNhanVien uc, clsLuongBaoHiem.DataContract hdnv)
        {
            InitializeComponent();
            clsLuongCB = cls;
            frmHoSoNV = uc;
            Main = main;
            HopDongNV = hdnv;
            tb_TieuDe.Text = "Chỉnh sửa hợp đồng nhân viên";
            tb_NameButton.Text = "Cập nhật";
            tb_TenHopDong.Text = hdnv.con_name;
            tb_Luong.Text = hdnv.con_salary_persent.ToString();
            tb_NgayHieuLuc.SelectedDate = hdnv.con_time_up;
            lsvTepDuocChon.ItemsSource = hdnv.con_file.ToList();
            tb_TepDinhKem.Text = hdnv.con_file.ToString();
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodThoatThemHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyThemHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyThemHopDong_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuyThemHopDong.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbHuyThemHopDong.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void bodHuyThemHopDong_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuyThemHopDong.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbHuyThemHopDong.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }
        public class DataInsertContract
        {
            public bool data { get; set; }
            public string message { get; set; }
            public DataContract newobj { get; set; }
        }
        private void bodThemMoiHopDong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            try
            {
                bool allow = true;
                if (string.IsNullOrEmpty(tb_TenHopDong.Text))
                {
                    tb_ValidateTenHopDong.Visibility = Visibility.Visible;
                    tb_ValidateTenHopDong.Text = "Vui lòng nhập tên hợp đồng của bạn";
                    allow = false;
                }
                else
                {
                    tb_ValidateTenHopDong.Visibility = Visibility.Collapsed;
                }
                if (string.IsNullOrEmpty(tb_Luong.Text))
                {
                    tb_ValidateLuong.Visibility = Visibility.Visible;
                    tb_ValidateLuong.Text = "Vui lòng nhập phần trăm lương của bạn!";
                    allow = false;
                }
                else
                {
                    if (!IsNumeric(tb_Luong.Text))
                    {
                        tb_ValidateLuong.Visibility = Visibility.Visible;
                        tb_ValidateLuong.Text = "Phần trăm lương phải là số!";
                        allow = false;
                    }
                    else
                        tb_ValidateLuong.Visibility = Visibility.Collapsed;
                }
               if (allow)
                {
                    using(WebClient web = new WebClient())
                    {
                        web.QueryString.Add("con_id_user", clsLuongCB.ep_id.ToString());
                        web.QueryString.Add("con_name", tb_TenHopDong.Text.ToString());
                        web.QueryString.Add("con_salary_persent", int.Parse(tb_Luong.Text).ToString());
                        web.QueryString.Add("con_time_up", tb_NgayHieuLuc.SelectedDate.Value.ToString("yyyy-MM-dd"));
                        web.QueryString.Add("con_time_end", "1970-1-1");
                        web.QueryString.Add("token", Properties.Settings.Default.Token);
                        web.QueryString.Add("con_file", "");
                        if(tb_TieuDe.Text == "Chỉnh sửa hợp đồng nhân viên")
                        {
                            web.QueryString.Add("con_id", HopDongNV.con_id.ToString());
                            web.UploadValuesAsync(new Uri("https://api.timviec365.vn/api/tinhluong/congty/edit_contract"), "POST", web.QueryString);
                        }    
                        else
                            web.UploadValuesAsync(new Uri("https://api.timviec365.vn/api/tinhluong/congty/insert_contract"), "POST", web.QueryString);
                        web.UploadValuesCompleted += (s1, e1) =>
                        {
                            try
                            {
                                var check = UTF8Encoding.UTF8.GetString(e1.Result);
                                if (tb_TieuDe.Text == "Chỉnh sửa hợp đồng nhân viên" && check.Contains("success"))
                                {
                                    var data = frmHoSoNV.lstHopDongNv.Find(x => x.con_id == HopDongNV.con_id);
                                    data.con_name = tb_TenHopDong.Text;
                                    data.con_salary_persent = int.Parse(tb_Luong.Text);
                                    data.con_time_up_date = tb_NgayHieuLuc.SelectedDate.Value.ToString("dd/MM/yyyy");
                                    frmHoSoNV.dgDanhSachHopDong.ItemsSource = frmHoSoNV.lstHopDongNv;
                                    frmHoSoNV.dgDanhSachHopDong.Items.Refresh();
                                    this.Visibility = Visibility.Collapsed;

                                }
                                else
                                {
                                    DataInsertContract api = JsonConvert.DeserializeObject<DataInsertContract>(UTF8Encoding.UTF8.GetString(e1.Result));
                                    if (api != null && api.newobj != null)
                                    {
                                        api.newobj.con_time_end_date = "-";
                                        api.newobj.con_time_up_date = tb_NgayHieuLuc.SelectedDate.Value.ToString("dd/MM/yyyy");
                                        frmHoSoNV.lstHopDongNv.Insert(0, api.newobj);
                                        frmHoSoNV.dgDanhSachHopDong.ItemsSource = frmHoSoNV.lstHopDongNv;
                                        frmHoSoNV.dgDanhSachHopDong.Items.Refresh();
                                        this.Visibility = Visibility.Collapsed;
                                    }
                                }
                                
                            }
                            catch { }
                        };
                    }
                }
            }
            catch (System.Exception)
            {}
           
        }

        private void bodThemMoiHopDong_MouseEnter(object sender, MouseEventArgs e)
        {
            bodThemMoiHopDong.BorderThickness = new Thickness(2);
        }

        private void bodThemMoiHopDong_MouseLeave(object sender, MouseEventArgs e)
        {
            bodThemMoiHopDong.BorderThickness = new Thickness(0);
        }
        private void bod_TepDinhKem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true; // Cho phép chọn nhiều tệp.
            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    
                    Attachments.Add(new Attachment { FileName = System.IO.Path.GetFileName(filePath) });
                    // Thêm filePath vào danh sách lưu trữ tệp nếu cần.
                    lsvTepDuocChon.Visibility = Visibility.Visible;
                    lsvTepDuocChon.ItemsSource = Attachments;
                }
            }
        }

        private void bod_TepDinhKem_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void bod_TepDinhKem_MouseLeave(object sender, MouseEventArgs e)
        {

        }

        private void tb_TepDinhKem_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void btn_ChonTepDinhKem_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void xoaTepDuocChon_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Attachment index = (Attachment)lsvTepDuocChon.SelectedItem;
            if (index != null)
            {
                Attachments.Remove(index);
                lsvTepDuocChon.ClearValue(ItemsControl.ItemsSourceProperty);
                lsvTepDuocChon.ItemsSource = Attachments;
                //shouldProcessEvent = false;
            }
            //shouldProcessEvent = true;
            if (Attachments.Count == 0)
            {
                lsvTepDuocChon.Visibility = Visibility.Collapsed;

            }
        }

        private void bod_Luong_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_Luong.Text))
            {
                gr_TangGiamPhanTram.Visibility = Visibility.Visible;
            }
        }

        private void bod_Luong_MouseLeave(object sender, MouseEventArgs e)
        {
            gr_TangGiamPhanTram.Visibility = Visibility.Collapsed;
        }

        private void bod_GiamTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void bod_TangTien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
           
        }

        private void tb_Luong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                tb_ValidateLuong.Visibility = Visibility.Visible;
                tb_ValidateLuong.Text = "Bạn vui lòng nhập đúng % lương, không nhập ký tự khác!";
            }
            else
            {
                tb_ValidateLuong.Visibility = Visibility.Collapsed;
            }
        }
        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        private void tb_Luong_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsNumeric(tb_Luong.Text))
            {
                e.Handled = false;
                tb_ValidateLuong.Visibility = Visibility.Visible;
                tb_ValidateLuong.Text = "Bạn vui lòng nhập đúng % lương, không nhập ký tự khác!";
            }
            else
            {
                tb_ValidateLuong.Visibility = Visibility.Collapsed;
            }
        }
    }
}
