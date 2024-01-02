using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.HoaHong
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Account_DoanhThu
    {
        //public double? birthday { get; set; }
        public int? gender { get; set; }
        public int? married { get; set; }
        public int? experience { get; set; }
        public int? education { get; set; }
        public string _id { get; set; }
    }

    public class Candidate_DoanhThu
    {
        public int? anhsao_badge { get; set; }
    }

    public class List_Rose_DoanhThu
    {

        public string _id { get; set; }
        public int ID { get; set; }
        public string Name { get; set; }
        private string _TongTien;
        public string TongTien
        {
            get { return _TongTien; }
            set
            {
                // Xử lý định dạng số tiền
                decimal amount;
                if (decimal.TryParse(value, out amount))
                {
                    // Định dạng số tiền thành chuỗi dạng "1.000.000"
                    _TongTien = amount.ToString("#,##0", CultureInfo.InvariantCulture).Replace(",", ".");
                }
                else
                {
                    // Xử lý nếu giá trị không hợp lệ
                    _TongTien = "Invalid value";
                }
            }
        }
        public double ro_Rose_dt_long { get; set; }
        public int ro_id { get; set; }
        private string _ro_Rose_dt;
        public string ro_Rose_dt
        {
            get { return _ro_Rose_dt; }
            set
            {
                // Xử lý định dạng số tiền
                decimal amount;
                if (decimal.TryParse(value, out amount))
                {
                    // Định dạng số tiền thành chuỗi dạng "1.000.000"
                    _ro_Rose_dt = amount.ToString("#,##0", CultureInfo.InvariantCulture).Replace(",", ".");
                }
                else
                {
                    // Xử lý nếu giá trị không hợp lệ
                    _ro_Rose_dt = "Invalid value";
                }
            }
        }
        public int ro_id_user { get; set; }
        public string userName { get; set; }
        public int ro_id_group { get; set; }
        public int ho_id_group { get; set; }
        public int ro_id_com { get; set; }
        public int ro_id_lr { get; set; }
        public int ro_id_tl { get; set; }
        public int ro_so_luong { get; set; }
        public DateTime ro_time { get; set; }
        public string ro_time_format { get; set; }
        public DateTime ro_time_end { get; set; }
        public string ro_note { get; set; }
        private long _ro_price;
        public long ro_price
        {
            get { return _ro_price; }
            set
            {
                _ro_price = value;
                ro_price_formatted = _ro_price.ToString("#,##0", System.Globalization.CultureInfo.InvariantCulture).Replace(",", ".");
            }
        }
        public string ro_price_formatted { get; set; }
        public int ro_kpi_active { get; set; }
        public string ro_time_created { get; set; }

        public System.DateTime Ro_Time_Cr
    {
            get
            {
                DateTime aa;
                if (!string.IsNullOrEmpty(ro_time_created) && DateTime.TryParse(ro_time_created, out aa)) return aa;
                return new DateTime(9999, 12, 30);
            }
        }
        public string time
        {
            get
            {
                var x = "";
                DateTime tt;
                if (!string.IsNullOrEmpty(ro_time_created) && DateTime.TryParse(ro_time_created, out tt))
                {
                    x = tt.ToString("H:mm tt");
                }
                return x;
            }
            set { ro_time_created = value; }
        }
        public string date
        {
            get
            {
                var x = "";
                DateTime tt;
                if (!string.IsNullOrEmpty(ro_time_created) && DateTime.TryParse(ro_time_created, out tt))
                {
                    x = tt.ToString("dd-MM-yyyy");
                }
                return x;
            }
            set { ro_time_created = value; }
        }
        public TinhluongThietLap TinhluongThietLap { get; set; }
        public Detail detail { get; set; }
    }

    public class Detail_DoanhThu
    {
        public int _id { get; set; }
        public object email { get; set; }
        public string phoneTK { get; set; }
        public string userName { get; set; }
        public object alias { get; set; }
        public object phone { get; set; }
        public object emailContact { get; set; }
        public object avatarUser { get; set; }
        public int type { get; set; }
        public object city { get; set; }
        public object district { get; set; }
        public string address { get; set; }
        public object otp { get; set; }
        public int? authentic { get; set; }
        public int? isOnline { get; set; }
        public string fromWeb { get; set; }
        public int fromDevice { get; set; }
        public int? createdAt { get; set; }
        public int? updatedAt { get; set; }
        public object lastActivedAt { get; set; }
        public int? time_login { get; set; }
        public int role { get; set; }
        public object latitude { get; set; }
        public object longtitude { get; set; }
        public int idQLC { get; set; }
        public int idTimViec365 { get; set; }
        public int idRaoNhanh365 { get; set; }
        public string chat365_secret { get; set; }
        public int chat365_id { get; set; }
        public int scan_base365 { get; set; }
        public int check_chat { get; set; }
        public List<object> sharePermissionId { get; set; }
        public InForPerson inForPerson { get; set; }
        public object inForCompany { get; set; }
        public object inforRN365 { get; set; }
        public int scan { get; set; }
        public bool emotion_active { get; set; }
        public int isAdmin { get; set; }
        public int scanElacticAdmin { get; set; }
        public int scan_ai365 { get; set; }
    }

    public class Employee_DoanhThu
    {
        public int com_id { get; set; }
        public int organizeDetailId { get; set; }
        public int dep_id { get; set; }
        public int? start_working_time { get; set; }
        public int position_id { get; set; }
        public int? team_id { get; set; }
        public int? group_id { get; set; }
        public int? time_quit_job { get; set; }
        public object ep_description { get; set; }
        public object ep_featured_recognition { get; set; }
        public string ep_status { get; set; }
        public int ep_signature { get; set; }
        public int? allow_update_face { get; set; }
        public int? version_in_use { get; set; }
        public int role_id { get; set; }
        public string _id { get; set; }
        public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
    }

    public class InForPerson_DoanhThu
    {
        public int scan { get; set; }
        public Account account { get; set; }
        public Employee employee { get; set; }
        public Candidate candidate { get; set; }
        public string _id { get; set; }
    }

    public class ListOrganizeDetailId_DoanhThu
    {
        public int level { get; set; }
        public int organizeDetailId { get; set; }
    }

    public class Root_DoanhThu
    {
        public List<List_Rose_DoanhThu> data { get; set; }
        public string message { get; set; }
    }

    public class TinhluongThietLap_DoanhThu
    {
        public string _id { get; set; }
        public int tl_id { get; set; }
        public int tl_id_com { get; set; }
        public int tl_id_rose { get; set; }
        public string tl_name { get; set; }
        public int tl_money_min { get; set; }
        public int tl_money_max { get; set; }
        public TlPhanTram tl_phan_tram { get; set; }
        public int tl_chiphi { get; set; }
        public int tl_hoahong { get; set; }
        public int tl_kpi_yes { get; set; }
        public int tl_kpi_no { get; set; }
        public string tl_time_created { get; set; }
    }

    public class TlPhanTram_DoanhThu
    {
        [JsonProperty("$numberDecimal")]
        public string numberDecimal { get; set; }
    }


}
