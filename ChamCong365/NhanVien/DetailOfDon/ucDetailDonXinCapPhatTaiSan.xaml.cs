using ChamCong365.APIs;

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
    public partial class ucDetailDonXinCapPhatTaiSan : UserControl
    {
        string filePatch = "";
        public ucDetailDonXinCapPhatTaiSan(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {
                filePatch = detailDeXuat.file_kem[0].file;
                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                txbDsTaiSan.Text = detailDeXuat.thong_tin_chung.cap_phat_tai_san.danh_sach_tai_san;
                txbSoLuongTaiSan.Text += detailDeXuat.thong_tin_chung.cap_phat_tai_san.so_luong_tai_san;
                txbLyDo.Text = detailDeXuat.thong_tin_chung.cap_phat_tai_san.ly_do;

            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(filePatch);
        }
    }
}
