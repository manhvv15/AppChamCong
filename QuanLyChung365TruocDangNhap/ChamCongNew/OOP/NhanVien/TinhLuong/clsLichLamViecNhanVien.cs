using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatDiMuonVeSom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.TinhLuong
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class BoNhiem
    {
        public object thanhviendc_bn { get; set; }
        public object organizeDetailId { get; set; }
        public object chucvu_hientai { get; set; }
        public object chucvu_dx_bn { get; set; }
        public object new_organizeDetailId { get; set; }
        public object ly_do { get; set; }
    }

    public class CapPhatTaiSan
    {
        public object cap_phat_taisan { get; set; }
        public object ly_do { get; set; }
    }

    public class CountRealWork
    {
        public int ep_id { get; set; }
        public string ep_name { get; set; }
        public DateTime ts_date { get; set; }
        public string day_of_week { get; set; }
        public int total_time { get; set; }
        public string late { get; set; }
        public string early { get; set; }
        public double num_to_calculate { get; set; }
        public int money_per_hour { get; set; }
        public int num_to_money { get; set; }
        public int num_overtime { get; set; }
        public List<DateTime> lst_time { get; set; }
        public List<int> listShiftCount { get; set; }
    }

    public class CountRealWorksOr
    {
        public int ep_id { get; set; }
        public string ep_name { get; set; }
        public DateTime ts_date { get; set; }
        public string day_of_week { get; set; }
        public int total_time { get; set; }
        public string late { get; set; }
        public string early { get; set; }
        public double num_to_calculate { get; set; }
        public int money_per_hour { get; set; }
        public int num_to_money { get; set; }
        public int num_overtime { get; set; }
        public List<DateTime> lst_time { get; set; }
        public List<int> listShiftCount { get; set; }
    }

    public class Data_LichNhanVien
    {
        public string userName { get; set; }
        public int ep_id { get; set; }
        public double cong_thuc { get; set; }
        public double cong_xn_them { get; set; }
        public int tongTienTheoGio { get; set; }
        public int count_standard_works { get; set; }
        public List<object> get_dx_cong_tl365 { get; set; }
        public List<object> count_late_early { get; set; }
        public List<CountRealWork> count_real_works { get; set; }
        public List<ListCycle> list_cycle { get; set; }
        public List<DataChamCong> data_cham_cong { get; set; }
        public List<DataFinal_LuongNV> data_final { get; set; }
        public List<DataFinalPre> data_final_pre { get; set; }
        public List<object> data_ko_cc_detail { get; set; }
        public List<DataKoCcDetailPre> data_ko_cc_detail_pre { get; set; }
        public List<object> listNoiDungNghiPhep { get; set; }
        public List<object> listDexuatCongcongDetail { get; set; }
        public List<ListDexuat> listDexuat { get; set; }
        public List<DeXuatNghi> de_xuat_nghi { get; set; }
        public List<CountRealWorksOr> count_real_works_or { get; set; }
    }

    public class DataChamCong
    {
        public string _id { get; set; }
        public int sheet_id { get; set; }
        public int ep_id { get; set; }
        public DateTime at_time { get; set; }
        public int shift_id { get; set; }
        public List<Shift> shift { get; set; }
    }

    public class DataFinal_LuongNV
    {
        public int id_final { get; set; }
        public DetailCycle detail_cycle { get; set; }
        public string content { get; set; }
        public int type { get; set; }
    }

    public class DataFinalPre
    {
        public int id_final { get; set; }
        public DetailCycle detail_cycle { get; set; }
        public string content { get; set; }
        public int type { get; set; }
    }

    public class DataKoCcDetailPre
    {
        public int shift_id { get; set; }
        public DateTime date { get; set; }
        public Detail detail { get; set; }
    }

    public class Detail_LichNhanVien
    {
        public string _id { get; set; }
        public int shift_id { get; set; }
        public int com_id { get; set; }
        public string shift_name { get; set; }
        public string start_time { get; set; }
        public string start_time_latest { get; set; }
        public string end_time { get; set; }
        public string end_time_earliest { get; set; }
        public int over_night { get; set; }
        public int shift_type { get; set; }
        public NumToCalculate num_to_calculate { get; set; }
        public int num_to_money { get; set; }
        public int money_per_hour { get; set; }
        public int is_overtime { get; set; }
        public int status { get; set; }
        public List<RelaxTime> relaxTime { get; set; }
        public int flex { get; set; }
        public DateTime create_time { get; set; }
    }

    public class DetailCycle
    {
        public int shift_id { get; set; }
        public DateTime date { get; set; }
        public Detail detail { get; set; }
    }

    public class DeXuatNghi
    {
        public int _id { get; set; }
        public string name_dx { get; set; }
        public int type_dx { get; set; }
        public NoiDung noi_dung { get; set; }
        public string name_user { get; set; }
        public int id_user { get; set; }
        public int com_id { get; set; }
        public int kieu_duyet { get; set; }
        public string id_user_duyet { get; set; }
        public string id_user_theo_doi { get; set; }
        public List<object> file_kem { get; set; }
        public int type_duyet { get; set; }
        public int type_time { get; set; }
        public int time_create { get; set; }
        public long time_tiep_nhan { get; set; }
        public long time_duyet { get; set; }
        public int active { get; set; }
        public int del_type { get; set; }
        public int tam_ung_status { get; set; }
        public int thanh_toan_status { get; set; }
        public string organizeDetailName { get; set; }
        public string refuse_reason { get; set; }
        public int __v { get; set; }
    }

    public class DiMuonVeSom
    {
        public object ngay_di_muon_ve_som { get; set; }
        public object time_batdau { get; set; }
        public object time_ketthuc { get; set; }
        public object ca_lam_viec { get; set; }
        public object ly_do { get; set; }
    }

    public class DoiCa
    {
        public object ngay_can_doi { get; set; }
        public object ca_can_doi { get; set; }
        public object ngay_muon_doi { get; set; }
        public object ca_muon_doi { get; set; }
        public object ly_do { get; set; }
    }

    public class HoaHong
    {
        public object chu_ky { get; set; }
        public object time_hh { get; set; }
        public object item_mdt_date { get; set; }
        public object dt_money { get; set; }
        public object name_dt { get; set; }
        public object ly_do { get; set; }
    }

    public class LichLamViec
    {
        public object lich_lam_viec { get; set; }
        public object thang_ap_dung { get; set; }
        public object ngay_bat_dau { get; set; }
        public object ca_la_viec { get; set; }
        public object ngay_lam_viec { get; set; }
        public object ly_do { get; set; }
    }

    public class ListCycle
    {
        public int shift_id { get; set; }
        public DateTime date { get; set; }
        public Detail detail { get; set; }
    }

    public class ListDexuat
    {
        public int _id { get; set; }
        public string name_dx { get; set; }
        public int type_dx { get; set; }
        public NoiDung noi_dung { get; set; }
        public string name_user { get; set; }
        public int id_user { get; set; }
        public int com_id { get; set; }
        public int kieu_duyet { get; set; }
        public string id_user_duyet { get; set; }
        public string id_user_theo_doi { get; set; }
        public List<object> file_kem { get; set; }
        public int type_duyet { get; set; }
        public int type_time { get; set; }
        public int time_create { get; set; }
        public object time_tiep_nhan { get; set; }
        public object time_duyet { get; set; }
        public int active { get; set; }
        public int del_type { get; set; }
        public int tam_ung_status { get; set; }
        public int thanh_toan_status { get; set; }
        public string organizeDetailName { get; set; }
        public string refuse_reason { get; set; }
        public int __v { get; set; }
    }

    public class LuanChuyenCongTac
    {
        public object cv_nguoi_lc { get; set; }
        public object pb_nguoi_lc { get; set; }
        public object noi_cong_tac { get; set; }
        public object noi_chuyen_den { get; set; }
        public object ly_do { get; set; }
    }

    public class Nd
    {
        public string bd_nghi { get; set; }
        public string kt_nghi { get; set; }
        public object ca_nghi { get; set; }
        public string _id { get; set; }
    }

    public class NghiPhep
    {
        public List<Nd> nd { get; set; }
        public int? loai_np { get; set; }
        public string ly_do { get; set; }
        public object ng_ban_giao_CRM { get; set; }
    }

    public class NghiPhepRaNgoai
    {
        public object type_nghi { get; set; }
        public object bd_nghi { get; set; }
        public object ca_nghi { get; set; }
        public object time_bd_nghi { get; set; }
        public object time_kt_nghi { get; set; }
        public object ly_do { get; set; }
    }

    public class NghiThaiSan
    {
        public object ngaybatdau_nghi_ts { get; set; }
        public object ngayketthuc_nghi_ts { get; set; }
        public object ly_do { get; set; }
    }

    public class NhapNgayNhanLuong
    {
        public object thang_ap_dung { get; set; }
        public object ngay_bat_dau { get; set; }
        public object ngay_ket_thuc { get; set; }
        public object ly_do { get; set; }
    }

    public class NoiDung
    {
        public XacNhanCong xac_nhan_cong { get; set; }
        public NghiPhep nghi_phep { get; set; }
        public DoiCa doi_ca { get; set; }
        public TamUng tam_ung { get; set; }
        public CapPhatTaiSan cap_phat_tai_san { get; set; }
        public ThoiViec thoi_viec { get; set; }
        public TangLuong tang_luong { get; set; }
        public BoNhiem bo_nhiem { get; set; }
        public LuanChuyenCongTac luan_chuyen_cong_tac { get; set; }
        public ThamGiaDuAn tham_gia_du_an { get; set; }
        public TangCa tang_ca { get; set; }
        public NghiThaiSan nghi_thai_san { get; set; }
        public SuDungPhongHop su_dung_phong_hop { get; set; }
        public SuDungXeCong su_dung_xe_cong { get; set; }
        public SuaChuaCoSoVatChat sua_chua_co_so_vat_chat { get; set; }
        public LichLamViec lich_lam_viec { get; set; }
        public HoaHong hoa_hong { get; set; }
        public ThanhToan thanh_toan { get; set; }
        public ThuongPhat thuong_phat { get; set; }
        public DiMuonVeSom di_muon_ve_som { get; set; }
        public NghiPhepRaNgoai nghi_phep_ra_ngoai { get; set; }
        public NhapNgayNhanLuong nhap_ngay_nhan_luong { get; set; }
        public XinTaiTaiLieu xin_tai_tai_lieu { get; set; }
    }

    public class NumToCalculate_LichNhanVien
    {
        [JsonProperty("$numberDecimal")]
        public string numberDecimal { get; set; }
    }

    public class RelaxTime_LichNhanVien
    {
        public object start_time_relax { get; set; }
        public object end_time_relax { get; set; }
        public string _id { get; set; }
    }

    public class Root_LichNhanVien
    {
        public Data_LichNhanVien data { get; set; }
        public string message { get; set; }
    }

    public class Shift_NhanVien
    {
        public string start_time { get; set; }
        public double start_time_latest { get; set; }
        public string end_time { get; set; }
        public string end_time_earliest { get; set; }
        public int over_night { get; set; }
        public int shift_type { get; set; }
        public NumToCalculate num_to_calculate { get; set; }
        public int num_to_money { get; set; }
        public int is_overtime { get; set; }
    }

    public class SuaChuaCoSoVatChat
    {
        public object tai_san { get; set; }
        public object so_luong { get; set; }
        public object ngay_sc { get; set; }
        public object so_tien { get; set; }
    }

    public class SuDungPhongHop
    {
        public object bd_hop { get; set; }
        public object end_hop { get; set; }
        public object phong_hop { get; set; }
        public object ly_do { get; set; }
    }

    public class SuDungXeCong
    {
        public object bd_xe { get; set; }
        public object end_xe { get; set; }
        public object soluong_xe { get; set; }
        public object local_di { get; set; }
        public object local_den { get; set; }
    }

    public class TamUng
    {
        public object ngay_tam_ung { get; set; }
        public object sotien_tam_ung { get; set; }
        public object ly_do { get; set; }
    }

    public class TangCa
    {
        public object time_tc { get; set; }
        public object ly_do { get; set; }
    }

    public class TangLuong
    {
        public object mucluong_ht { get; set; }
        public object mucluong_tang { get; set; }
        public object date_tang_luong { get; set; }
        public object ly_do { get; set; }
    }

    public class ThamGiaDuAn
    {
        public object cv_nguoi_da { get; set; }
        public object pb_nguoi_da { get; set; }
        public object dx_da { get; set; }
        public object ly_do { get; set; }
    }

    public class ThanhToan
    {
        public object so_tien_tt { get; set; }
        public object ly_do { get; set; }
    }

    public class ThoiViec
    {
        public object ngaybatdau_tv { get; set; }
        public object ca_bdnghi { get; set; }
        public object ly_do { get; set; }
    }

    public class ThuongPhat
    {
        public object so_tien_tp { get; set; }
        public object time_tp { get; set; }
        public object nguoi_tp { get; set; }
        public object type_tp { get; set; }
        public object ly_do { get; set; }
    }

    public class XacNhanCong
    {
        public DateTime? time_xnc { get; set; }
        public string time_vao_ca { get; set; }
        public string time_het_ca { get; set; }
        public string ca_xnc { get; set; }
        public int? id_ca_xnc { get; set; }
        public string ly_do { get; set; }
    }

    public class XinTaiTaiLieu
    {
        public object ten_tai_lieu { get; set; }
        public object ly_do { get; set; }
    }


}
