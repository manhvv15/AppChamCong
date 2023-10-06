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
                filePatch = detailDeXuat.file_kem[0].file;
                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                txbNgayTamUng.Text = DateTimeOffset.FromUnixTimeSeconds((long)detailDeXuat.thong_tin_chung.tam_ung.ngay_tam_ung).ToLocalTime().ToString("dd/MM/yyyy");
                txbSoTienTamUng.Text = detailDeXuat.thong_tin_chung.tam_ung.sotien_tam_ung?.ToString("#,##0.00 đ");
                txbLyDo.Text = detailDeXuat.thong_tin_chung.doi_ca.ly_do;


            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process.Start(filePatch);
        }
    }
}
