using ChamCong365.APIs;
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
    public partial class ucDetailLuanChuyen : UserControl
    {
        List<Employee> AllEmployeeList = new List<Employee>();
        List<Position > ListPosition = new List<Position>();  
        
        string filePatch = "";
        public ucDetailLuanChuyen(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            InitializeComponent();
            ShowData(detailDeXuat);
        }

        async void ShowData(ChiTietDeXuat.DetailDeXuat detailDeXuat)
        {
            try
            {

                txbHoVaTen.Text = detailDeXuat.nguoi_tao;
                int id = (int)detailDeXuat.thong_tin_chung.bo_nhiem.thanhviendc_bn;
                txbChucVu.Text = detailDeXuat.thong_tin_chung.luan_chuyen_cong_tac.cv_nguoi_lc;
                txbPhongBan.Text = detailDeXuat.thong_tin_chung.luan_chuyen_cong_tac.pb_nguoi_lc;
                txbNoiDangCong.Text = detailDeXuat.thong_tin_chung.luan_chuyen_cong_tac.noi_cong_tac;
                txbNoiCanChuyenDen.Text = detailDeXuat.thong_tin_chung.luan_chuyen_cong_tac.noi_chuyen_den;
                txbLyDo.Text = detailDeXuat.thong_tin_chung.luan_chuyen_cong_tac.ly_do;
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
