using ChamCong365.OOP.CaiDatLuong.CaiDatLuongCB;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ChamCong365.Popup.CaiDatLuong.CaiDatNhapLuongCoBan
{
    /// <summary>
    /// Interaction logic for ucThemLuongCoBan.xaml
    /// </summary>
    public partial class ucThemLuongCoBan : UserControl
    {
        BrushConverter br = new BrushConverter();
        private string Month;
        private clsLuongCoBan.ListResult clsLuongCB = new clsLuongCoBan.ListResult();
        private ucHoSoNhanVien frmHoSoNV;
        private MainWindow Main;
        private clsLuongBaoHiem.DataSalary LuongBH = new clsLuongBaoHiem.DataSalary();
        public ucThemLuongCoBan(MainWindow main, clsLuongCoBan.ListResult cls, ucHoSoNhanVien uc)
        {
            InitializeComponent();
            clsLuongCB = cls;
            frmHoSoNV = uc;
            Main = main;
            
        }
        public ucThemLuongCoBan(MainWindow main, clsLuongCoBan.ListResult cls, ucHoSoNhanVien uc, clsLuongBaoHiem.DataSalary lbh)
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
            if(textTieuDe.Text== "Chỉnh sửa lương cơ bản")
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
                        frmHoSoNV.LoadDLLuongBH();
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
                        request.AddParameter("sb_id_com", clsLuongCB.com_id);
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
                        frmHoSoNV.LoadDLLuongBH();
                        this.Visibility = Visibility.Collapsed;

                    }

                }
                catch
                {

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
    }
}
