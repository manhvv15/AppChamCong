using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.NhanVien.LuongNhanVien
{
    
    public class Data_LuongNhanVien
    {
        public int luong_co_ban { get; set; }
        public int phan_tram_hop_dong { get; set; }
        public int tongTienTheoGio { get; set; }
        public int cong_chuan { get; set; }
        public int cong_thuc { get; set; }
        public int cong_sau_phat { get; set; }
        public int cong_theo_tien { get; set; }
        public int cong_ghi_nhan { get; set; }
        public int cong_nghi_phep { get; set; }
        public int tong_cong_nhan { get; set; }
        public int luong_thuc { get; set; }
        public int luong_sau_phat { get; set; }
        public int luong_bao_hiem { get; set; }
        public int tien_phat_muon { get; set; }
        public int cong_phat_di_muon_ve_som { get; set; }
        public int tong_hoa_hong { get; set; }
        public int tien_tam_ung { get; set; }
        public int thuong { get; set; }
        public int luong_nghi_le { get; set; }
        public int phat { get; set; }
        public int tien_phat_nghi_khong_phep { get; set; }
        public int phat_nghi_sai_quy_dinh { get; set; }
        public int tien_phuc_loi { get; set; }
        public int tien_phu_cap { get; set; }
        public int phu_cap_theo_ca { get; set; }
        public int tong_bao_hiem { get; set; }
        public int tien_khac { get; set; }
        public int tong_luong { get; set; }
        public int thue { get; set; }
        public int tien_thuc_nhan { get; set; }
        public int luong_da_tra { get; set; }
        public List<object> count_real_works { get; set; }
        public List<object> listDexuatCongcongDetail { get; set; }
        public List<object> data_ko_cc_detail { get; set; }
        public List<object> listNoiDungNghiPhep { get; set; }
        public List<object> data_time_sheet { get; set; }
        public List<object> data_circle { get; set; }
        public List<object> hoa_hong_ca_nhan { get; set; }
        public DateTime temp_start_hoahong { get; set; }
        public DateTime temp_end_hoahong { get; set; }
        public List<object> data_late_early { get; set; }
        public List<object> thuong_phat_data { get; set; }
        public List<object> de_xuat_nghi { get; set; }
        public List<object> listDexuat { get; set; }
        public List<object> cong_phat_muon { get; set; }
        public List<object> data_late_early_cong { get; set; }
        public List<object> data_phu_cap { get; set; }
        public List<object> listDexuatCongcongDetail_pre { get; set; }
        public object Resign_Infor { get; set; }
        public List<object> data_bao_hiem { get; set; }
        public List<object> count_real_works_or { get; set; }
    }

    public class Root_LuongNhanVien
    {
        public Data_LuongNhanVien data { get; set; }
        public string message { get; set; }
    }


}
