using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien.TinhLuong
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data_LuongNV
    {
        public long luong_co_ban { get ; set; }
        public string luong_co_ban_format
        {
            get { return luong_co_ban.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    luong_co_ban = parsedLuong;
                }
            }
        }
        public string phan_tram_hop_dong { get; set; }
        public long tongTienTheoGio { get; set; }    
        public string tongTienTheoGio_format
        {
            get { return tongTienTheoGio.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tongTienTheoGio = parsedLuong;
                }
            }
        }
        public long caTinhTien { get; set; }
        public string caTinhTien_format
        {
            get { return caTinhTien.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    caTinhTien = parsedLuong;
                }
            }
        }
        public string cong_chuan { get; set; }
        public string cong_thuc { get; set; }
        public string cong_sau_phat { get; set; }
        public string tong_phat_cong { get; set; }
        public string cong_theo_tien { get; set; }
        public string cong_ghi_nhan { get; set; }
        public string cong_nghi_phep { get; set; }
        public string tong_cong_nhan { get; set; }

        public long luong_thuc { get; set; }
        public string luong_thuc_format
        {
            get { return luong_thuc.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    luong_thuc = parsedLuong;
                }
            }
        }
        public long luong_sau_phat { get; set; }
        public string luong_sau_phat_format
        {
            get { return luong_sau_phat.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    luong_sau_phat = parsedLuong;
                }
            }
        }

        public long luong_bao_hiem { get; set; }    
        public string luong_bao_hiem_format
        {
            get { return luong_bao_hiem.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    luong_bao_hiem = parsedLuong;
                }
            }
        }
        public long tien_phat_muon { get; set; }
        public string tien_phat_muon_format
        {
            get { return tien_phat_muon.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tien_phat_muon = parsedLuong;
                }
            }
        }
        public string cong_phat_di_muon_ve_som { get; set; }

        public long tong_hoa_hong { get; set; }  
        public string tong_hoa_hong_format
        {
            get { return tong_hoa_hong.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tong_hoa_hong = parsedLuong;
                }
            }
        }
        public long tien_tam_ung { get; set; }
        public string tien_tam_ung_format
        {
            get { return tien_tam_ung.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tien_tam_ung = parsedLuong;
                }
            }
        }
        public long thuong { get; set; }
        public string thuong_format
        {
            get { return thuong.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    thuong = parsedLuong;
                }
            }
        }
        public long luong_nghi_le { get; set; }
        public string luong_nghi_le_format
        {
            get { return luong_nghi_le.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    luong_nghi_le = parsedLuong;
                }
            }
        }
        public long phat { get; set; }   
        public string phat_format
        {
            get { return phat.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    phat = parsedLuong;
                }
            }
        }
        public long tien_phat_nghi_khong_phep { get; set; }
        public string tien_phat_nghi_khong_phep_format
        {
            get { return tien_phat_nghi_khong_phep.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tien_phat_nghi_khong_phep = parsedLuong;
                }
            }
        }
        public long phat_nghi_sai_quy_dinh { get; set; }
        public string phat_nghi_sai_quy_dinh_formar
        {
            get { return phat_nghi_sai_quy_dinh.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    phat_nghi_sai_quy_dinh = parsedLuong;
                }
            }
        }
        public long tien_phuc_loi { get; set; }
        public string tien_phuc_loi_format
        {
            get { return tien_phuc_loi.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tien_phuc_loi = parsedLuong;
                }
            }
        }
        public long tien_phu_cap { get; set; }
        public string tien_phu_cap_format
        {
            get { return tien_phu_cap.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tien_phu_cap = parsedLuong;
                }
            }
        }


        public long phu_cap_theo_ca { get; set; }
        public string phu_cap_theo_ca_format
        {
            get { return phu_cap_theo_ca.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    phu_cap_theo_ca = parsedLuong;
                }
            }
        }
        public long tong_bao_hiem { get; set; }
        public string tong_bao_hiem_format
        {
            get { return tong_bao_hiem.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tong_bao_hiem = parsedLuong;
                }
            }
        }
        public long tien_khac { get; set; }
        public string tien_khac_format
        {
            get { return tien_khac.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tien_khac = parsedLuong;
                }
            }
        }
        public long tong_luong { get; set; }
        public string tong_luong_format
        {
            get { return tong_luong.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tong_luong = parsedLuong;
                }
            }
        }
        public long thue { get; set; }
        public string thue_format
        {
            get { return thue.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    thue = parsedLuong;
                }
            }
        }
        public long tien_thuc_nhan { get; set; }
        public string tien_thuc_nhan_format
        {
            get { return tien_thuc_nhan.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    tien_thuc_nhan = parsedLuong;
                }
            }
        }
        public long luong_da_tra { get; set; }
        public string luong_da_tra_format
        {
            get { return luong_da_tra.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    luong_da_tra = parsedLuong;
                }
            }
        }
        public List<object> count_real_works { get; set; }
        public List<object> listDexuatCongcongDetail { get; set; }
        public List<DataKoCcDetail> data_ko_cc_detail { get; set; }
        public List<object> listNoiDungNghiPhep { get; set; }
        public List<object> data_time_sheet { get; set; }
        public List<DataCircle> data_circle { get; set; }
        public List<object> hoa_hong_ca_nhan { get; set; }
        public DateTime temp_start_hoahong { get; set; }
        public DateTime temp_end_hoahong { get; set; }
        public List<object> data_late_early { get; set; }
        public List<ThuongPhatDatum> thuong_phat_data { get; set; }
        public List<object> de_xuat_nghi { get; set; }
        public List<object> listDexuat { get; set; }
        public List<object> cong_phat_muon { get; set; }
        public List<object> data_late_early_cong { get; set; }
        public List<object> data_phu_cap { get; set; }
        public List<object> listDexuatCongcongDetail_pre { get; set; }
        public object Resign_Infor { get; set; }
        public List<object> data_bao_hiem { get; set; }
        public List<object> test_realwork { get; set; }
        public List<ListPhatCong> listPhatCong { get; set; }
    }

    public class DataCircle
    {
        public int shift_id { get; set; }
        public DateTime date { get; set; }
        public Detail detail { get; set; }
    }

    public class DataKoCcDetail
    {
        public int shift_id { get; set; }
        public DateTime date { get; set; }
        public string date_format { get; set; }
        public string Shift_Name { get; set; }
        public Detail detail { get; set; }
    }

    public class Detail
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
        public int nums_day { get; set; }
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

    public class ListPhatCong
    {
        public string _id { get; set; }
        public int id_phatcong { get; set; }
        public int ep_id { get; set; }
        public int com_id { get; set; }
        public DateTime phatcong_time { get; set; }
        public int phatcong_shift { get; set; }
        public string ly_do { get; set; }
        public int month { get; set; }
        public int year { get; set; }
        public Shifts shifts { get; set; }
    }

    public class NumToCalculate
    {
        [JsonProperty("$numberDecimal")]
        public string numberDecimal { get; set; }
    }

    public class RelaxTime
    {
        public object start_time_relax { get; set; }
        public object end_time_relax { get; set; }
        public string _id { get; set; }
    }

    public class Root_LuongNV
    {
        public Data_LuongNV data { get; set; }
        public string message { get; set; }
    }

    public class Shifts
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
        public int nums_day { get; set; }
        public int shift_type { get; set; }
        public double num_to_calculate { get; set; }
        public int num_to_money { get; set; }
        public int money_per_hour { get; set; }
        public int is_overtime { get; set; }
        public int status { get; set; }
        public List<RelaxTime> relaxTime { get; set; }
        public int flex { get; set; }
        public DateTime create_time { get; set; }
    }

    public class ThuongPhatDatum
    {
        public string _id { get; set; }
        public int pay_id { get; set; }

        public long pay_price { get; set; }
        public string pay_price_format
        {
            get { return pay_price.ToString("N0"); }
            set
            {
                if (long.TryParse(value.Replace(",", ""), out long parsedLuong))
                {
                    pay_price = parsedLuong;
                }
            }
        }
        public int pay_status { get; set; }
        public string pay_case { get; set; }
    }


}
