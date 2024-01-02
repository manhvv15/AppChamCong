using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.HoaHong
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Account_LoiNhuan
    {
        public int birthday { get; set; }
        public int gender { get; set; }
        public int married { get; set; }
        public int experience { get; set; }
        public int education { get; set; }
        public string _id { get; set; }
    }

    public class Candidate_LoiNhuan
    {
        public int? use_type { get; set; }
        public int? user_reset_time { get; set; }
        public int? use_view { get; set; }
        public int? use_noti { get; set; }
        public int? use_show { get; set; }
        public int? time_hide { get; set; }
        public int? use_show_cv { get; set; }
        public int? use_active { get; set; }
        public int? use_td { get; set; }
        public int? use_check { get; set; }
        public int? use_test { get; set; }
        public int? use_badge { get; set; }
        public int? point_time_active { get; set; }
        public string cv_title { get; set; }
        public object cv_muctieu { get; set; }
        public object cv_kynang { get; set; }
        public object cv_giai_thuong { get; set; }
        public object cv_hoat_dong { get; set; }
        public object cv_so_thich { get; set; }
        public List<object> cv_city_id { get; set; }
        public List<object> cv_district_id { get; set; }
        public object cv_address { get; set; }
        public List<object> cv_cate_id { get; set; }
        public int? cv_capbac_id { get; set; }
        public int? cv_money_id { get; set; }
        public int? cv_loaihinh_id { get; set; }
        public int? cv_time { get; set; }
        public int? cv_time_dl { get; set; }
        public int? um_type { get; set; }
        public object um_min_value { get; set; }
        public object um_max_value { get; set; }
        public int? um_unit { get; set; }
        public object cv_tc_name { get; set; }
        public object cv_tc_cv { get; set; }
        public object cv_tc_dc { get; set; }
        public object cv_tc_phone { get; set; }
        public object cv_tc_email { get; set; }
        public object cv_tc_company { get; set; }
        public object cv_video { get; set; }
        public int cv_video_type { get; set; }
        public int? cv_video_active { get; set; }
        public object use_ip { get; set; }
        public int? percents { get; set; }
        public int? vip { get; set; }
        public int? check_create_use { get; set; }
        public int? emp_id { get; set; }
        public int? scan_audio { get; set; }
        public object audio { get; set; }
        public int? scan_elastic { get; set; }
        public int? anhsao_badge { get; set; }
        public int? star3 { get; set; }
        public int? time_active_star3 { get; set; }
        public string _id { get; set; }
        public List<object> profileDegree { get; set; }
        public List<object> profileNgoaiNgu { get; set; }
        public List<object> profileExperience { get; set; }
    }

    public class List_Rose_LoiNhuan
    {
        public string _id { get; set; }
        public int ro_id { get; set; }
        public string ro_rose_ln { get; set; }
        public int ro_id_user { get; set; }
        public string userName { get; set; }
        public string ro_tl_name { get; set; }
        public int ro_id_group { get; set; }
        public int ho_id_group { get; set; }
        public int ro_id_com { get; set; }
        public int ro_id_lr { get; set; }
        public int ro_id_tl { get; set; }
        public int ro_so_luong { get; set; }
        public DateTime ro_time { get; set; }
        public DateTime ro_time_end { get; set; }
        public string ro_note { get; set; }
        public long ro_price { get; set; }
        public int? ro_kpi_active { get; set; }
        public DateTime ro_time_created { get; set; }
        public string ro_time_created_format { get; set; }
        public TinhluongThietLap TinhluongThietLap { get; set; }
        public Detail detail { get; set; }
    }

    public class Detail_LoiNhuan
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
        public int? authentic { get; set; }
        public int? isOnline { get; set; }
        public string fromWeb { get; set; }
        public int? fromDevice { get; set; }
        public double createdAt { get; set; }
        public double updatedAt { get; set; }
        public DateTime lastActivedAt { get; set; }
        public int? time_login { get; set; }
        public int? role { get; set; }
        public string latitude { get; set; }
        public string longtitude { get; set; }
        public int idQLC { get; set; }
        public int? idTimViec365 { get; set; }
        public int? idRaoNhanh365 { get; set; }
        public string chat365_secret { get; set; }
        public int? chat365_id { get; set; }
        public int? scan_base365 { get; set; }
        public int? scan_ai365 { get; set; }
        public int? scan_cv { get; set; }
        public int? check_chat { get; set; }
        public List<object> sharePermissionId { get; set; }
        public InForPerson inForPerson { get; set; }
        public object inForCompany { get; set; }
        public object inforRN365 { get; set; }
        public int? scan { get; set; }
        public bool emotion_active { get; set; }
        public int? scanElacticAdmin { get; set; }
    }

    public class Employee_LoiNhuan
    {
        public int com_id { get; set; }
        public int organizeDetailId { get; set; }
        public int dep_id { get; set; }
        public int? start_working_time { get; set; }
        public int position_id { get; set; }
        public int? team_id { get; set; }
        public int? group_id { get; set; }
        public int? time_quit_job { get; set; }
        public string ep_description { get; set; }
        public string ep_featured_recognition { get; set; }
        public string ep_status { get; set; }
        public int? ep_signature { get; set; }
        public int? allow_update_face { get; set; }
        public int? version_in_use { get; set; }
        public int? role_id { get; set; }
        public string _id { get; set; }
        public List<object> listOrganizeDetailId { get; set; }
    }

    public class InForPerson_LoiNhuan
    {
        public int scan { get; set; }
        //public Account account { get; set; }
        public Employee employee { get; set; }
        public Candidate candidate { get; set; }
        public string _id { get; set; }
    }

    public class Root_LoiNhuan
    {
        public List<List_Rose_LoiNhuan> data { get; set; }
        public string message { get; set; }
    }

    public class TinhluongThietLap_LoiNhuan
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

    public class TlPhanTram_LoiNhuan
    {
        [JsonProperty("$numberDecimal")]
        public string numberDecimal { get; set; }
    }


}
