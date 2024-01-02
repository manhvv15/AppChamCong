﻿using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.funcQuanLyCongTy;
using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
using Newtonsoft.Json;
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

namespace QuanLyChung365TruocDangNhap.ChamCongNew.NhanVien.DetailOfDon
{
    /// <summary>
    /// Interaction logic for ucDetailSuDungPhongHop.xaml
    /// </summary>
    public partial class ucDetailDeXuatTangLuong : UserControl
    {
        string filePatch = "";
        public ucDetailDeXuatTangLuong(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file; }
                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                txbLuongHienTai.Text = detailDeXuat.thong_tin_chung.tang_luong.mucluong_ht?.ToString("#,##0.00 đ");
                txbLuongMuonTang.Text = detailDeXuat.thong_tin_chung.tang_luong.mucluong_tang?.ToString("#,##0.00 đ");
                txbNgayBatDauTang.Text = DateTimeOffset.FromUnixTimeMilliseconds((long)detailDeXuat.thong_tin_chung.tang_luong.date_tang_luong).ToLocalTime().ToString("dd/MM/yyyy");
                txbLyDo.Text = detailDeXuat.thong_tin_chung.tang_luong.ly_do;

            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try { System.Diagnostics.Process.Start(filePatch); } catch { }
        }
    }
}
