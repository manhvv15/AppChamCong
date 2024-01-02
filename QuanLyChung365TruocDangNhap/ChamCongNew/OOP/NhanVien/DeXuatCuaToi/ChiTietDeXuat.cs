using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi
{
    public class ChiTietDeXuat
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class BoNhiem
        {
            public int? thanhviendc_bn { get; set; }
            public int? organizeDetailId { get; set; }
            public int? chucvu_hientai { get; set; }
            public int? chucvu_dx_bn { get; set; }
            public int? new_organizeDetailId { get; set; }
            public string ly_do { get; set; }
        }

        public class CapPhatTaiSan
        {
            public string cap_phat_taisan { get; set; }
            public string ly_do { get; set; }
        }
        public class DSTS
        {
            public string[][] ds_ts { get; set; }
        }
        public class TaiSan
        {
            public int? id { get; set; }
            public int? soLuong { get; set; }
        }

        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public List<DetailDeXuat> detailDeXuat { get; set; }
        }

        public class DetailDeXuat
        {
            public int id_de_xuat { get; set; }
            public string ten_de_xuat { get; set; }
            public string nguoi_tao { get; set; }
            public int id_nguoi_tao { get; set; }
            public int nhom_de_xuat { get; set; }
            public long Time_create { get; set; }

            public string thoi_gian_tao
            {
                set
                {
                    Time_create = long.Parse(value);
                }
                get
                {
                    return DateTimeOffset.FromUnixTimeMilliseconds(Time_create).ToLocalTime().ToString("HH:mm tt dd/MM/yyyy");
                }
            }
            public int loai_de_xuat { get; set; }
            public int cap_nhat { get; set; }
            public ThongTinChung thong_tin_chung { get; set; }
            private string Kieu_phe_duyet { get; set; }
            public string kieu_phe_duyet
            {
                get
                {
                    switch (Kieu_phe_duyet)
                    {
                        case "0":
                            return "Phê duyệt đồng thời";
                        case "1":
                            return "Phê duyệt lần lượt";
                        default:
                            return "";
                    }
                }
                set { Kieu_phe_duyet = value; }
            }
            public List<LanhDaoDuyet> lanh_dao_duyet { get; set; }
            public List<LanhDaoDuyet> nguoi_theo_doi { get; set; }
            public List<FileKem> file_kem { get; set; }
            public int type_duyet { get; set; }
            public double thoi_gian_duyet { get; set; }
            public double thoi_gian_tiep_nhan { get; set; }
            public List<LichSuDuyet> lich_su_duyet { get; set; }
            public int del_type { get; set; }
            public bool qua_han_duyet { get; set; }
            public int tam_ung_status { get; set; }
            public int thanh_toan_status { get; set; }
            public bool? confirm_status { get; set; }
            public bool? confirmed { get; set; }
        }
        public class FileKem
        {
            public string file { get; set; }
        }
        public class LichSuDuyet
        {
            public string ng_duyet { get; set; }
            public string Type_duyet { get; set; }
            public string type_duyet
            {
                get
                {
                    switch (Type_duyet)
                    {
                        case "1":
                            return "đã tiếp nhận đề xuất";
                        case "2":
                            return "đã duyệt đề xuất";
                        case "3":
                            return "đã từ chối đề xuất";
                        case "4":
                            return "đã hủy duyệt đề xuất";
                        default:
                            return "";
                    }
                }
                set { Type_duyet = value; }
            }
            public DateTime time { get; set; }
            public string formatTime { get { return time.ToString("dd/MM/yyyy hh:mm:ss tt"); } }  
        }
        public class DiMuonVeSom
        {
            public int? ngay_di_muon_ve_som { get; set; }
            public string time_batdau { get; set; }
            public string time_ketthuc { get; set; }
            public int? ca_lam_viec { get; set; }
            public string ly_do { get; set; }
        }

        public class DoiCa
        {
            public int? ngay_can_doi { get; set; }
            public int? ca_can_doi { get; set; }
            public int? ngay_muon_doi { get; set; }
            public int? ca_muon_doi { get; set; }
            public string ly_do { get; set; }
        }

        public class HoaHong
        {
            public string chu_ky { get; set; }
            public DateTime? time_hh { get; set; }
            public string item_mdt_date { get; set; }
            public string dt_money { get; set; }
            public string name_dt { get; set; }
            public string ly_do { get; set; }
        }

        public class KhieuNai
        {
            public string ly_do { get; set; }
        }

        public class LanhDaoDuyet
        {
            public string userName { get; set; }
            private string AvatarUser { get; set; }
            public string avatarUser
            {
                get
                {
                    if (AvatarUser != null)
                    {
                        return "https://chamcong.24hpay.vn/upload/employee/" + AvatarUser;

                    }
                    else
                    {
                        return "https://tinhluong.timviec365.vn/img/add.png";
                    }

                }
                set { AvatarUser = value; }
            }
            public int idQLC { get; set; }
        }

        public class LichLamViec
        {
            public int? lich_lam_viec { get; set; }
            public int? thang_ap_dung { get; set; }
            public int? ngay_bat_dau { get; set; }
            public string ca_la_viec { get; set; }
            public string ngay_lam_viec { get; set; }
            public string ly_do { get; set; }
        }

        public class LuanChuyenCongTac
        {
            public string cv_nguoi_lc { get; set; }
            public string pb_nguoi_lc { get; set; }
            public string noi_cong_tac { get; set; }
            public string noi_chuyen_den { get; set; }
            public string ly_do { get; set; }
        }

        public class Nd
        {
            public string bd_nghi { get; set; }
            public string kt_nghi { get; set; }
            public int? ca_nghi { get; set; }
            public string _id { get; set; }
        }

        public class NghiPhep
        {
            public List<Nd> nd { get; set; }
            public int? loai_np { get; set; }
            //public string ng_ban_giao_CRM { get; set; }
            public string ly_do { get; set; }
            public int? ng_ban_giao_CRM { get; set; }

        }

        public class NghiPhepRaNgoai
        {
            public int? type_nghi { get; set; }
            public int? bd_nghi { get; set; }
            public int? ca_nghi { get; set; }
            public string time_bd_nghi { get; set; }
            public string time_kt_nghi { get; set; }
            public string ly_do { get; set; }
        }

        public class NghiThaiSan
        {
            public long? ngaybatdau_nghi_ts { get; set; }
            public long? ngayketthuc_nghi_ts { get; set; }
            public string ly_do { get; set; }
        }

        public class NhapNgayNhanLuong
        {
            public string thang_ap_dung { get; set; }
            public int? ngay_bat_dau { get; set; }
            public int? ngay_ket_thuc { get; set; }
            public string ly_do { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

        public class SuaChuaCoSoVatChat
        {
            public int? tai_san { get; set; }
            public int? so_luong { get; set; }
            public double? ngay_sc { get; set; }
            public decimal? so_tien { get; set; }
            public string ly_do { get; set; }
        }
        public class NoiDung
        {
            public string name_taisan { get; set; }
            public int? so_tien { get; set; }
            public string ly_do { get; set; }
            public string _id { get; set; }
        }

        public class SuDungPhongHop
        {
            public long? bd_hop { get; set; }
            public long? end_hop { get; set; }
            public string phong_hop { get; set; }
            public string ly_do { get; set; }
        }

        public class SuDungXeCong
        {
            public double? bd_xe { get; set; }
            public double? end_xe { get; set; }
            public int? soluong_xe { get; set; }
            public string local_di { get; set; }
            public string local_den { get; set; }
            public string ly_do { get; set; }
        }

        public class TamUng
        {
            public long? ngay_tam_ung { get; set; }
            public decimal? sotien_tam_ung { get; set; }
            public string ly_do { get; set; }
        }

        public class TangCa
        {
            public long? time_tc { get; set; }
            public int? shift_id { get; set; }
            public string ly_do { get; set; }
        }

        public class TangLuong
        {
            public int? mucluong_ht { get; set; }
            public int? mucluong_tang { get; set; }
            public long? date_tang_luong { get; set; }
            public string ly_do { get; set; }
        }

        public class ThamGiaDuAn
        {
            public string cv_nguoi_da { get; set; }
            public string pb_nguoi_da { get; set; }
            public string dx_da { get; set; }
            public string ly_do { get; set; }
        }

        public class ThanhToan
        {
            public decimal? so_tien_tt { get; set; }
            public string ly_do { get; set; }
        }

        public class ThoiViec
        {
            public DateTime? ngaybatdau_tv { get; set; }
            public int? ca_bdnghi { get; set; }
            public string ly_do { get; set; }
        }

        public class ThongTinChung
        {
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
            public XacNhanCong xac_nhan_cong { get; set; }
            public LichLamViec lich_lam_viec { get; set; }
            public HoaHong hoa_hong { get; set; }
            public KhieuNai khieu_nai { get; set; }
            public ThanhToan thanh_toan { get; set; }
            public ThuongPhat thuong_phat { get; set; }
            public DiMuonVeSom di_muon_ve_som { get; set; }
            public NghiPhepRaNgoai nghi_phep_ra_ngoai { get; set; }
            public NhapNgayNhanLuong nhap_ngay_nhan_luong { get; set; }
            public XinTaiTaiLieu xin_tai_tai_lieu { get; set; }
        }

        public class ThuongPhat
        {
            public decimal? so_tien_tp { get; set; }
            public DateTime? time_tp { get; set; }
            public string nguoi_tp { get; set; }
            public int? type_tp { get; set; }
            public string ly_do { get; set; }
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
            public string ten_tai_lieu { get; set; }
            public string ly_do { get; set; }
        }


    }
}
