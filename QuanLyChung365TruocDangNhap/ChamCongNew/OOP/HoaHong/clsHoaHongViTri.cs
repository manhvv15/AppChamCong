using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.HoaHong
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Account_Vitri
    {
        public object birthday { get; set; }
        public int gender { get; set; }
        public int married { get; set; }
        public int experience { get; set; }
        public int education { get; set; }
        public string _id { get; set; }
    }

    public class Candidate_Vitri
    {
        public int anhsao_badge { get; set; }
    }

    public class List_Rose_ViTri
    {
        public string _id { get; set; }
        public int ro_id { get; set; }
        public string ro_rose_vt { get; set; }
        public int ro_id_user { get; set; }
        public string userName { get; set; }
        public string productName { get; set; }
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
        public int ro_price { get; set; }
        public int ro_kpi_active { get; set; }
        public DateTime ro_time_created { get; set; }
        public Detail detail { get; set; }
        public TinhluongThietLap TinhluongThietLap { get; set; }
        public long money { get; set; }
    }

    public class Detail_Vitri
    {
        public int _id { get; set; }
        public string email { get; set; }
        public string phoneTK { get; set; }
        public string userName { get; set; }
        public object alias { get; set; }
        public string phone { get; set; }
        public string emailContact { get; set; }
        public string avatarUser { get; set; }
        public int? type { get; set; }
        public int? city { get; set; }
        public int? district { get; set; }
        public string address { get; set; }
        public object otp { get; set; }
        public int authentic { get; set; }
        public int isOnline { get; set; }
        public string fromWeb { get; set; }
        public int? fromDevice { get; set; }
        public int? createdAt { get; set; }
        public int? updatedAt { get; set; }
        public DateTime lastActivedAt { get; set; }
        public int? time_login { get; set; }
        public int role { get; set; }
        public object latitude { get; set; }
        public object longtitude { get; set; }
        public int idQLC { get; set; }
        public int idTimViec365 { get; set; }
        public int idRaoNhanh365 { get; set; }
        public string chat365_secret { get; set; }
        public List<object> sharePermissionId { get; set; }
        public InForPerson inForPerson { get; set; }
        public object inForCompany { get; set; }
        public object inforRN365 { get; set; }
        public int scan { get; set; }
        public int scanElacticAdmin { get; set; }
        public int scan_ai365 { get; set; }
    }

    public class Employee_Vitri
    {
        public int com_id { get; set; }
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
        public string _id { get; set; }
    }

    public class InForPerson_Vitri
    {
        public int scan { get; set; }
        //public Account account { get; set; }
        public Employee employee { get; set; }
        public Candidate candidate { get; set; }
        public string _id { get; set; }
    }

    public class Root_Vitri
    {
        public List<List_Rose_ViTri> data { get; set; }
        public string message { get; set; }
    }

    public class TinhluongThietLap_Vitri
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

    public class TlPhanTram_Vitri
    {
        [JsonProperty("$numberDecimal")]
        public string numberDecimal { get; set; }
    }


}
