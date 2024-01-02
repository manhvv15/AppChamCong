using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
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
    public partial class ucDetailDonTamUng : UserControl
    {
        string filePatch = "";
        public ucDetailDonTamUng(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file;}
                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                txbNgayTamUng.Text = DateTimeOffset.FromUnixTimeSeconds((long)detailDeXuat.thong_tin_chung.tam_ung.ngay_tam_ung).ToLocalTime().ToString("dd/MM/yyyy");
                txbSoTienTamUng.Text = detailDeXuat.thong_tin_chung.tam_ung.sotien_tam_ung?.ToString("#,##0.00 đ");
                txbLyDo.Text = detailDeXuat.thong_tin_chung.tam_ung.ly_do;


            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try{System.Diagnostics.Process.Start(filePatch);}catch{}
        }
    }
}
