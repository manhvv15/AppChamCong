using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using static QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.ThuongPhat.clsAddTP;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.ThuongPhat
{
    public class clsDSThuongPhat
    {
        public class Account
        {
            public double? birthday { get; set; }
            public int? gender { get; set; }
            public int? married { get; set; }
            public int? experience { get; set; }
            public int? education { get; set; }
            public string _id { get; set; }
        }

        public class ConfigChat
        {
            public string userNameNoVn { get; set; }
            public int? doubleVerify { get; set; }
            public int? active { get; set; }
            public string status { get; set; }
            public int? acceptMessStranger { get; set; }
            public int? notificationAcceptOffer { get; set; }
            public int? notificationAllocationRecall { get; set; }
            public int?  notificationChangeSalary { get; set; }
            public int? notificationCommentFromRaoNhanh { get; set; }
            public int? notificationCommentFromTimViec { get; set; }
            public int? notificationDecilineOffer { get; set; }
            public int? notificationMissMessage { get; set; }
            public int? notificationNTDExpiredPin { get; set; }
            public int? notificationNTDExpiredRecruit { get; set; }
            public int? notificationNTDPoint { get; set; }
            public int? notificationSendCandidate { get; set; }
            public int? notificationTag { get; set; }
            public List<object> HistoryAccess { get; set; }
            public List<object> removeSugges { get; set; }
        }

        public class Data
        {
            public List<DataFinal> data_final { get; set; }
        }

        public class DataFinal
        {
            public InforUser inforUser { get; set; }
            public TtThuong tt_thuong { get; set; }
            public TtPhat tt_phat { get; set; }
            public TtPhatCong tt_phat_cong { get; set; }
        }

        public class Employee
        {
            public int com_id { get; set; }
            public int? dep_id { get; set; }
            public double? start_working_time { get; set; }
            public int? position_id { get; set; }
            public int? team_id { get; set; }
            public int? group_id { get; set; }
            public int? time_quit_job { get; set; }
            public string ep_description { get; set; }
            public string ep_status { get; set; }
            public int? ep_signature { get; set; }
            public int? allow_update_face { get; set; }
            public int? version_in_use { get; set; }
            public string _id { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public int? organizeDetailId { get; set; }
            public int? role_id { get; set; }
        }
        public class InForPerson
        {
            public int scan { get; set; }
            public Account account { get; set; }
            public Employee employee { get; set; }
            public string _id { get; set; }
        }

        public class InforUser
        {
            public int _id { get; set; }
            public string email { get; set; }
            public string phoneTK { get; set; }
            public string userName { get; set; }
            public object alias { get; set; }
            public string phone { get; set; }
            public string emailContact { get; set; }
            public string avatarUser { get; set; }
            public BitmapImage avatarUser_format
            {
                get
                {
                    BitmapImage img = null;
                    if (!string.IsNullOrEmpty(avatarUser))
                    {
                        img = new Uri("https://chamcong.24hpay.vn/upload/employee/" + avatarUser).GetThumbnail(100);
                    }
                    if (img == null) img = new Uri("https://chamcong.timviec365.vn/images/ep_logo.png").GetThumbnail(100);
                    return img;
                }
            }
            public int type { get; set; }
            public int? city { get; set; }
            public int? district { get; set; }
            public string address { get; set; }
            public object otp { get; set; }
            public int? authentic { get; set; }
            public int? isOnline { get; set; }
            public string fromWeb { get; set; }
            public int? fromDevice { get; set; }
            public double createdAt { get; set; }
            public double updatedAt { get; set; }
            public DateTime? lastActivedAt { get; set; }
            public int? time_login { get; set; }
            public int? role { get; set; }
            public object latitude { get; set; }
            public object longtitude { get; set; }
            public int idQLC { get; set; }
            public int idTimViec365 { get; set; }
            public int idRaoNhanh365 { get; set; }
            public string chat365_secret { get; set; }
            public int? chat365_id { get; set; }
            public int? scan_base365 { get; set; }
            public int? check_chat { get; set; }
            public List<object> sharePermissionId { get; set; }
            public InForPerson inForPerson { get; set; }
            public object inForCompany { get; set; }
            public object inforRN365 { get; set; }
            public ConfigChat configChat { get; set; }
            public int? scan { get; set; }
            public int? scanElacticAdmin { get; set; }
            public int? scan_ai365 { get; set; }
            public int? isAdmin { get; set; }
            public bool? emotion_active { get; set; }
            public int? idGiaSu { get; set; }
            public int? idVLTG { get; set; }
            public InforVLTG inforVLTG { get; set; }
        }
        public class InforVLTG
        {
            public object uv_day { get; set; }
            public int luot_xem { get; set; }
        }

        public class ListOrganizeDetailId
        {
            public int level { get; set; }
            public int organizeDetailId { get; set; }
        }
        public class Root
        {
            public Data data { get; set; }
            public string message { get; set; }
        }
        public class DsPhat
        {
            public string _id { get; set; }
            public int pay_id { get; set; }
            public int pay_id_user { get; set; }
            public int pay_id_com { get; set; }
            public long pay_price { get; set; }
            public string pay_price_formatted
            {
                get { return pay_price.ToString("N0"); }
                set
                {
                    if (int.TryParse(value.Replace(",", ""), out int parsedLuong))
                    {
                        pay_price = parsedLuong;
                    }
                }
            }
            public int pay_status { get; set; }
            public string pay_case { get; set; }
            public string pay_day { get; set; }
            public string pay_day_format { get; set; }
            public int pay_month { get; set; }
            public int pay_year { get; set; }
            public object pay_group { get; set; }
            public object pay_nghi_le { get; set; }
            public DateTime pay_time_created { get; set; }
        }
        public class DsThuong
        {
            public string _id { get; set; }
            public int pay_id { get; set; }
            public int pay_id_user { get; set; }
            public int pay_id_com { get; set; }
            public long pay_price { get; set; }
            public string pay_price_formatted
            {
                get { return pay_price.ToString("N0"); }
                set
                {
                    if (int.TryParse(value.Replace(",", ""), out int parsedLuong))
                    {
                        pay_price = parsedLuong;
                    }
                }
            }
            public int pay_status { get; set; }
            public string pay_case { get; set; }
            public string pay_day { get; set; }
            public int pay_month { get; set; }
            public int pay_year { get; set; }
            public int pay_group { get; set; }
            public int pay_nghi_le { get; set; }
            public DateTime pay_time_created { get; set; }
        }

        public class DsPhatCong
        {
            public string _id { get; set; }
            public int id_phatcong { get; set; }
            public int ep_id { get; set; }
            public int com_id { get; set; }
            public string phatcong_time { get; set; }
            public string phatcong_time_format { get; set; }
            public int phatcong_shift { get; set; }
            public string ly_do { get; set; }
            public int month { get; set; }
            public int year { get; set; }
            public Shifts shifts { get; set; }
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
            public string nums_day { get; set; }
            public int shift_type { get; set; }
            public NumToCalculate num_to_calculate { get; set; }
            public int num_to_money { get; set; }
            public int? money_per_hour { get; set; }
            public int is_overtime { get; set; }
            public int status { get; set; }
            public List<RelaxTime> relaxTime { get; set; }
            public int flex { get; set; }
            public DateTime create_time { get; set; }
        }
        public class RelaxTime
        {
            public object start_time_relax { get; set; }
            public object end_time_relax { get; set; }
            public string _id { get; set; }
        }
        public class NumToCalculate
        {
            [JsonProperty("$numberDecimal")]
            public string numberDecimal { get; set; }
        }
        public class TtPhat
        {
            public long tong_phat { get; set; }
            public string tong_phat_formatted
            {
                get { return tong_phat.ToString("N0"); }
                set
                {
                    if (int.TryParse(value.Replace(",", ""), out int parsedLuong))
                    {
                        tong_phat = parsedLuong;
                    }
                }
            }
            public List<DsPhat> ds_phat { get; set; }
        }
        public class TtPhatCong
        {
            public string tong_phat_cong { get; set; }
            public List<DsPhatCong> ds_phat_cong { get; set; }
        }

        public class TtThuong
        {
            public long tong_thuong { get; set; }
            public string tong_thuong_formatted
            {
                get { return tong_thuong.ToString("N0"); }
                set
                {
                    if (int.TryParse(value.Replace(",", ""), out int parsedLuong))
                    {
                        tong_thuong = parsedLuong;
                    }
                }
            }
            public List<DsThuong> ds_thuong { get; set; }
        }


    }
}
