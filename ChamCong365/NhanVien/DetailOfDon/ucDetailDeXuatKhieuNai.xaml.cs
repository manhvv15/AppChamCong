﻿using ChamCong365.APIs;
using ChamCong365.OOP.funcQuanLyCongTy;
using ChamCong365.OOP.NhanVien.DeXuatCuaToi;
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

namespace ChamCong365.NhanVien.DetailOfDon
{
    /// <summary>
    /// Interaction logic for ucDetailSuDungPhongHop.xaml
    /// </summary>
    public partial class ucDetailDeXuatKhieuNai : UserControl
    {
        string filePatch = "";
        public ucDetailDeXuatKhieuNai(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {

                txb1.Text = detailDeXuat.nguoi_tao;
                txb2.Text = detailDeXuat.thong_tin_chung.khieu_nai.ly_do;
                filePatch = detailDeXuat.file_kem[0].file;

            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(filePatch);
        }
    }
}
