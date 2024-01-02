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
    public partial class ucDetailThuongPhat : UserControl
    {
        string filePatch = "";
        public ucDetailThuongPhat(ChiTietDeXuat.DetailDeXuat detailDeXuat)
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
                txb2.Text = detailDeXuat.thong_tin_chung.thuong_phat.so_tien_tp + " đ";
                txb3.Text = detailDeXuat.thong_tin_chung.thuong_phat.time_tp?.ToLocalTime().ToString("dd/MM/yyyy , hh:mm tt");
                //txb4.Text = detailDeXuat.thong_tin_chung.thuong_phat.nguoi_tp;
                if (detailDeXuat.thong_tin_chung.thuong_phat.type_tp == 1)
                    txb5.Text = "Đề xuất thưởng";
                else txb5.Text = "Đề xuất phạt";
                txbLyDo.Text = detailDeXuat.thong_tin_chung.thuong_phat.ly_do;


            }
            catch { }
        }
  
        private void LinkToFile(object sender, MouseButtonEventArgs e)
        {
            try{System.Diagnostics.Process.Start(filePatch);}catch{}
        }
    }
}
