using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
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
    public partial class ucDetailSuDungXeCong : UserControl
    {
        string filePatch = "";
        public ucDetailSuDungXeCong(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {

                txb1.Text = detailDeXuat.nguoi_tao;
                txb2.Text = DateTimeOffset.FromUnixTimeMilliseconds((long)detailDeXuat.thong_tin_chung.su_dung_xe_cong.bd_xe).ToLocalTime().ToString("dd/MM/yyyy");
                txb3.Text = DateTimeOffset.FromUnixTimeMilliseconds((long)detailDeXuat.thong_tin_chung.su_dung_xe_cong.end_xe).ToLocalTime().ToString("dd/MM/yyyy");
                txb4.Text = detailDeXuat.thong_tin_chung.su_dung_xe_cong.soluong_xe.ToString();
                txb5.Text = detailDeXuat.thong_tin_chung.su_dung_xe_cong.local_di.ToString();
                txb6.Text = detailDeXuat.thong_tin_chung.su_dung_xe_cong.local_den.ToString();
                txb7.Text = detailDeXuat.thong_tin_chung.su_dung_xe_cong.ly_do.ToString();
                if (detailDeXuat?.file_kem.Count > 0) { filePatch = detailDeXuat?.file_kem[0]?.file;}

            }
            catch { }
        }
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try{System.Diagnostics.Process.Start(filePatch);}catch{}
        }
    }
}
