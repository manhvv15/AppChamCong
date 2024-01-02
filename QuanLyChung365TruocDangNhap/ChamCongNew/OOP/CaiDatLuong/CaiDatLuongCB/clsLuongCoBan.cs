using QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.DeXuatCuaToi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.clsNhanVienThuocCongTy;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.CaiDatLuongCB
{
    public class clsLuongCoBan
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class ListResult_CDL
        {
            public string userName { get; set; }
            public string avatarUser { get; set; }
            public string OrganizeName { get; set; }
            public string positionName { get; set; }
            public int ep_id { get; set; }
            public long _luong_co_ban;
            public long luong_co_ban { get; set; }
            public string luong_co_ban_Format
            {
                get { return luong_co_ban.ToString("N0") != null ? luong_co_ban.ToString("N0") : 0 + " VNĐ"; }
                set
                {
                    if (int.TryParse(value.Replace(",", ""), out int parsedLuongdc))
                    {
                        _luong_co_ban = parsedLuongdc;
                    }
                }
            }
            public string phan_tram_hop_dong { get; set; }
            public int cong_chuan { get; set; }
            public string cong_chuan_display
            {
                get
                {
                    return cong_chuan.ToString("N0") != null ? cong_chuan.ToString("N0") : "0";
                }
                set { }
            }
            public int tongTienTheoGio { get; set; }
            public string tongTienTheoGio_display
            {
                get
                {
                    return tongTienTheoGio.ToString("N0") != null ? tongTienTheoGio.ToString("N0") + " VNĐ" : 0 + " VNĐ";
                }
                set { }
            }
            public string cong_thuc { get; set; }
            public string cong_sau_phat { get; set; }
            public string cong_theo_tien { get; set; }
            public string cong_ghi_nhan { get; set; }
            public string cong_nghi_phep { get; set; }
            public string tong_cong_nhan { get; set; }
            public int luong_thuc { get; set; }
            public string luong_thuc_display
            {
                get
                {
                    return luong_thuc.ToString("N0") + " VNĐ";
                }
                set { }
            }
            public long luong_sau_phat { get; set; }
            public string luong_sau_phat_display
            {
                get
                {
                    return luong_sau_phat.ToString("N0") + " VNĐ";
                }
            }
            public int luong_bao_hiem { get; set; }

            public string luong_bao_hiem_display
            {
                get
                {
                    return luong_bao_hiem.ToString("N0") + " VNĐ";
                }
                set { value.ToString(); }
            }
            public int tien_phat_muon { get; set; }
            public string tien_phat_muon_display
            {
                get
                {
                    return tien_phat_muon.ToString("N0") + " VNĐ";
                }
            }
            public double cong_phat_di_muon_ve_som { get; set; }
            public long tong_hoa_hong { get; set; }
            public string tong_hoa_hong_display
            {
                get
                {
                    return tong_hoa_hong.ToString("N0") + " VNĐ";
                }
            }
            public int tien_tam_ung { get; set; }
            public string tien_tam_ung_display
            {
                get
                {
                    return tien_tam_ung.ToString("N0") + " VNĐ";
                }
            }
            public int thuong { get; set; }
            public string thuong_display
            {
                get
                {
                    return thuong.ToString("N0") + " VNĐ";
                }
            }
            public int luong_nghi_le { get; set; }
            public string luong_nghi_le_display
            {
                get
                {
                    return luong_nghi_le.ToString("N0") + " VNĐ";
                }
            }
            public int phat { get; set; }
            public string phat_display
            {
                get
                {
                    return phat.ToString("N0") + " VNĐ";
                }
            }
            public int tien_phat_nghi_khong_phep { get; set; }
            public string tien_phat_nghi_khong_phep_display
            {
                get
                {
                    return tien_phat_nghi_khong_phep.ToString("N0") + " VNĐ";
                }
            }
            public int phat_nghi_sai_quy_dinh { get; set; }
            public string phat_nghi_sai_quy_dinh_display
            {
                get
                {
                    return phat_nghi_sai_quy_dinh.ToString("N0") + " VNĐ";
                }
            }
            public int tien_phuc_loi { get; set; }
            public string tien_phuc_loi_display
            {
                get
                {
                    return tien_phuc_loi.ToString("N0") + " VNĐ";
                }
            }
            public int? tien_phu_cap { get; set; }
            public string tien_phu_cap_display
            {
                get
                {
                    if (tien_phu_cap != null)
                        return int.Parse(tien_phu_cap.ToString()).ToString("N0");
                    else return "0 VNĐ";
                }
            }
            public int phu_cap_theo_ca { get; set; }
            public string phu_cap_theo_ca_display
            {
                get
                {
                    return phu_cap_theo_ca.ToString("N0") + " VNĐ";
                }
            }
            public long? tong_bao_hiem { get; set; }
            public string tong_bao_hiem_display
            {
                get
                {
                    if (tong_bao_hiem != null)
                        return tong_bao_hiem.Value.ToString("N0") + " VNĐ";
                    else
                        return "0 VNĐ";
                }
            }
            public int tien_khac { get; set; }
            public string tien_khac_display
            {
                get
                {
                    return tien_khac.ToString("N0") + " VNĐ";
                }
            }
            public int tong_luong { get; set; }
            public string tong_luong_display
            {
                get
                {
                    return tong_luong.ToString("N0") + " VNĐ";
                }
            }
            public int thue { get; set; }
            public string thue_display
            {
                get
                {
                    return thue.ToString("N0") + " VNĐ";
                }
            }
            public int tien_thuc_nhan { get; set; }
            public string tien_thuc_nhan_display
            {
                get
                {
                    return tien_thuc_nhan.ToString("N0") + " VNĐ";
                }
            }
            public int luong_da_tra { get; set; }
            public string luong_da_tra_display
            {
                get
                {
                    return luong_da_tra.ToString("N0") + " VNĐ";
                }
            }
            public List<object> data_ko_cc_detail { get; set; }
            public List<object> data_circle { get; set; }
            public List<object> data_time_sheet { get; set; }
            public List<object> count_real_works { get; set; }
            public List<object> data_late_early { get; set; }
            public string organizeDetailName { get; set; }
            public int caTinhTien { get; set; }
           
        }
        public class OrganizeDetail
        {
            public string _id { get; set; }
            public int id { get; set; }
            public int comId { get; set; }
            public int parentId { get; set; }
            public string organizeDetailName { get; set; }
            public int level { get; set; }
            public int range { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public List<Content> content { get; set; }
            public int created_time { get; set; }
            public int update_time { get; set; }
        }
        public class Pos
        {
            public string _id { get; set; }
            public int id { get; set; }
            public int comId { get; set; }
            public string positionName { get; set; }
            public int level { get; set; }
            public int isManager { get; set; }
            public int created_time { get; set; }
            public int update_time { get; set; }
        }
        public class Root_CaiDatLuong
        {
            public bool data { get; set; }
            public string message { get; set; }
            public int total { get; set; }
            public int pageNumber { get; set; }
            public int pageSize { get; set; }
            public int current { get; set; }
            public long luong_tong_cong_ty { get; set; }

            public long _luong_tong_cong_ty;
            public string luong_tong_cong_ty_Format
            {
                get { return luong_tong_cong_ty.ToString("N0") != null ? luong_tong_cong_ty.ToString("N0") : 0 + " VNĐ"; }
                set
                {
                    if (int.TryParse(value.Replace(",", ""), out int parsedLuongdc))
                    {
                        _luong_tong_cong_ty = parsedLuongdc;
                    }
                }
            }
            public List<ListResult_CDL> listResult { get; set; }
        }


    }
}
