using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Text.RegularExpressions;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB.clsLuongCoBan;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for ucThemLuongCoBan.xaml
    /// </summary>
    public partial class ucThemLuongCoBan : UserControl
    {
        BrushConverter br = new BrushConverter();
        private string Month;
        //private OOP.clsNhanVienThuocCongTy.ListUser clsLuongCB = new OOP.clsNhanVienThuocCongTy.ListUser();
        private ListResult_CDL clsLuongCB = new ListResult_CDL();
        private ucHoSoNhanVien frmHoSoNV;
        private MainWindow Main;
        private clsLuongBaoHiem.DataSalary LuongBH = new clsLuongBaoHiem.DataSalary();
        public ucThemLuongCoBan(MainWindow main, ListResult_CDL cls, ucHoSoNhanVien uc)
        {
            InitializeComponent();
            clsLuongCB = cls;
            frmHoSoNV = uc;
            Main = main;
            
        }
        public ucThemLuongCoBan(MainWindow main, ListResult_CDL cls, ucHoSoNhanVien uc, clsLuongBaoHiem.DataSalary lbh)
        {
            InitializeComponent();
            clsLuongCB = cls;
            frmHoSoNV = uc;
            Main = main;
            LuongBH = lbh;
            textNhapLuongCB.Text = lbh.sb_salary_basic.ToString();
            textNhapLuongDongBH.Text = lbh.sb_salary_bh.ToString();
            textNhapPhuCapBH.Text = lbh.sb_pc_bh.ToString();
            dtpNgayAD.Text = lbh.sb_time_up.ToString();
            textNhapLyDo.Text = lbh.sb_lydo;
            textCanCuQD.Text = lbh.sb_quyetdinh;
            textTieuDe.Text = "Chỉnh sửa lương cơ bản";
            textThemSuaLCB.Text = "Cập nhật";
        }
        private void bodThemLuongCoBan_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            bool allow = true;
            if (string.IsNullOrEmpty(textNhapLuongCB.Text.Trim()))
            {
                textThongBaoNhapLuongCB.Visibility = Visibility.Visible;
                textThongBaoNhapLuongCB.Text = "Vui lòng nhập lương cơ bản!";
                allow = false;
            }
            else
            {
                if (!IsNumeric(textNhapLuongCB.Text.Trim()))
                {
                    textThongBaoNhapLuongCB.Visibility = Visibility.Visible;
                    textThongBaoNhapLuongCB.Text = "Lương cơ bản phải là số!";
                    allow = false;
                }
                else
                    textThongBaoNhapLuongCB.Visibility = Visibility.Collapsed;
            }
            if(!string.IsNullOrEmpty(textNhapLuongDongBH.Text) && !IsNumeric(textNhapLuongDongBH.Text))
            {
                validateLBH.Visibility = Visibility.Visible;
                allow = false;
            }
            else validateLBH.Visibility = Visibility.Collapsed;
            if(!string.IsNullOrEmpty(textNhapPhuCapBH.Text) && !IsNumeric(textNhapPhuCapBH.Text))
            {
                validatePCBH.Visibility = Visibility.Visible;
                allow = false;
            }
            else validatePCBH.Visibility = Visibility.Collapsed;
            if (dtpNgayAD.Text == "")
            {
                textThongBaoNhapTGAD.Visibility = Visibility.Visible;
                allow = false;
            }
            if (allow)
            {
                if (textTieuDe.Text == "Chỉnh sửa lương cơ bản")
                {
                    try
                    {
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/update_basic_salary")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("sb_id", LuongBH.sb_id);
                            request.AddParameter("sb_salary_basic", textNhapLuongCB.Text);
                            int days = dtpNgayAD.SelectedDate.Value.Day;
                            int months = dtpNgayAD.SelectedDate.Value.Month;
                            int years = dtpNgayAD.SelectedDate.Value.Year;
                            string[] date = dtpNgayAD.Text.Split(Convert.ToChar("/"));
                            if (months < 10 && days < 10)
                            {
                                request.AddParameter("sb_time_up",$"{years}-0{months}-0{days}");
                            }
                            else if (months >= 10 && days < 10)
                            {
                                request.AddParameter("sb_time_up", $"{years}-{months}-0{days}");
                            }
                            else if (months < 10 && days >= 10)
                            {
                                request.AddParameter("sb_time_up", $"{years}-0{months}-{days}");
                            }
                            else
                            {
                                request.AddParameter("sb_time_up", $"{years}-{months}-{days}"); ;
                            }
                            request.AddParameter("sb_salary_bh", textNhapLuongDongBH.Text);
                            request.AddParameter("sb_pc_bh", textNhapPhuCapBH.Text);
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            request.AddParameter("sb_lydo", textNhapLyDo.Text);
                            request.AddParameter("sb_quyetdinh", textCanCuQD.Text);
                            RestResponse resAlbum = restclient.Execute(request);
                            var b = resAlbum.Content;
                            frmHoSoNV.LoadLuongAndBaoHiem();
                            this.Visibility = Visibility.Collapsed;

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
                        using (RestClient restclient = new RestClient(new Uri("http://210.245.108.202:3009/api/tinhluong/congty/insert_basic_salary")))
                        {
                            RestRequest request = new RestRequest();
                            request.Method = Method.Post;
                            request.AlwaysMultipartFormData = true;
                            request.AddParameter("sb_id_user", clsLuongCB.ep_id);
                            request.AddParameter("sb_id_com", Main.IdAcount);
                            request.AddParameter("sb_salary_basic", textNhapLuongCB.Text);
                            string[] date = dtpNgayAD.Text.Split(Convert.ToChar("/"));
                            if (int.Parse(date[1]) < 10 && int.Parse(date[0]) < 10)
                            {
                                request.AddParameter("sb_time_up", date[2] + "-0" + date[0] + "-0" + date[1] + "T00:00:00.000+00:00");
                            }
                            else if (int.Parse(date[1]) >= 10 && int.Parse(date[0]) < 10)
                            {
                                request.AddParameter("sb_time_up", date[2] + "-0" + date[0] + "-" + date[1] + "T00:00:00.000+00:00");
                            }
                            else if (int.Parse(date[1]) < 10 && int.Parse(date[0]) >= 10)
                            {
                                request.AddParameter("sb_time_up", date[2] + "-" + date[0] + "-0" + date[1] + "T00:00:00.000+00:00");

                            }
                            else
                            {
                                request.AddParameter("sb_time_up", date[2] + "-" + date[0] + "-" + date[1] + "T00:00:00.000+00:00");
                            }
                            request.AddParameter("sb_salary_bh", textNhapLuongDongBH.Text);
                            request.AddParameter("sb_pc_bh", textNhapPhuCapBH.Text);
                            request.AddParameter("token", Properties.Settings.Default.Token);
                            request.AddParameter("sb_lydo", textNhapLyDo.Text);
                            request.AddParameter("sb_quyetdinh", textCanCuQD.Text);
                            RestResponse resAlbum = restclient.Execute(request);
                            var b = resAlbum.Content;
                            frmHoSoNV.LoadLuongAndBaoHiem();
                            this.Visibility = Visibility.Collapsed;

                        }
                    }
                    catch
                    {

                    }
                }
            }
        }

            private void bodHuyBoThemNhanVien_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

      

        private void bodThemLuongCoBan_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void bodThemLuongCoBan_MouseLeave(object sender, MouseEventArgs e)
        {
            bodThemLuongCoBan.BorderThickness = new Thickness(0);
        }

        private void bodThoatThemLuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        private void bodHuyBoThemLuong_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.Visibility= Visibility.Collapsed;
        }

        private void bodHuyBoThemLuong_MouseLeave(object sender, MouseEventArgs e)
        {
            bodHuyBoThemLuong.Background = (Brush)br.ConvertFrom("#FFFFFF");
            txbHuyBoThemLuong.Foreground = (Brush)br.ConvertFrom("#4C5BD4");
        }

        private void bodHuyBoThemLuong_MouseEnter(object sender, MouseEventArgs e)
        {
            bodHuyBoThemLuong.Background = (Brush)br.ConvertFrom("#4C5BD4");
            txbHuyBoThemLuong.Foreground = (Brush)br.ConvertFrom("#FFFFFF");
        }

        private void textNhapLuongCB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void tb_Luong_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                validateLBH.Visibility = Visibility.Visible;
            }
            else
            {
                validateLBH.Visibility = Visibility.Collapsed;
            }
        }
        private bool IsNumeric(string text)
        {
            return double.TryParse(text, out _);
        }

        private void tb_Luong_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!IsNumeric(textNhapLuongDongBH.Text))
            {
                e.Handled = false;
                validateLBH.Visibility = Visibility.Visible;
            }
            else
            {
                validateLBH.Visibility = Visibility.Collapsed;
            }
        }
        private void tb_Luong_PreviewTextInput1(object sender, TextCompositionEventArgs e)
        {
            if (!IsNumeric(e.Text))
            {
                e.Handled = true;
                validatePCBH.Visibility = Visibility.Visible;
            }
            else
            {
                validatePCBH.Visibility = Visibility.Collapsed;
            }
        }
        private void tb_Luong_TextChanged1(object sender, TextChangedEventArgs e)
        {
            if (!IsNumeric(textNhapPhuCapBH.Text))
            {
                e.Handled = false;
                validatePCBH.Visibility = Visibility.Visible;
            }
            else
            {
                validatePCBH.Visibility = Visibility.Collapsed;
            }
        }
        private void tb_Luong_TextChanged2(object sender, TextChangedEventArgs e)
        {
            if (!IsNumeric(textNhapLuongCB.Text))
            {
                e.Handled = false;
                textThongBaoNhapLuongCB.Visibility = Visibility.Visible;
                textThongBaoNhapLuongCB.Text = "Lương cơ bản phải là số!";
            }
            else
            {
                validatePCBH.Visibility = Visibility.Collapsed;
            }
        }

    }
}
