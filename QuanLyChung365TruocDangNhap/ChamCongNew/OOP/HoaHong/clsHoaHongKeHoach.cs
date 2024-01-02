// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

public class Account_kh
{
    public int birthday { get; set; }
    public int gender { get; set; }
    public int married { get; set; }
    public int experience { get; set; }
    public int education { get; set; }
    public string _id { get; set; }
}

public class Datum_kh
{
    public string _id { get; set; }
    public string ro_name_user { get; set; }
    public string ro_name_tl { get; set; }
    public string ro_time_format { get; set; }
    public int ro_id { get; set; }
    public int ro_id_user { get; set; }
    public int ro_id_group { get; set; }
    public int ho_id_group { get; set; }
    public int ro_id_com { get; set; }
    public int ro_id_lr { get; set; }
    public int ro_id_tl { get; set; }
    public int ro_so_luong { get; set; }
    public DateTime ro_time { get; set; }
    public DateTime ro_time_end { get; set; }
    public string ro_note { get; set; }
    public int ro_price { get; set; }
    public int ro_kpi_active { get; set; }
    public DateTime ro_time_created { get; set; }
    public TinhluongThietLap TinhluongThietLap { get; set; }
    public Detail_HHKH detail { get; set; }
}

public class Detail_HHKH
{
    public int _id { get; set; }
    public object email { get; set; }
    public string phoneTK { get; set; }
    public string userName { get; set; }
    public object alias { get; set; }
    public string phone { get; set; }
    public object emailContact { get; set; }
    public object avatarUser { get; set; }
    public int type { get; set; }
    public int city { get; set; }
    public int district { get; set; }
    public string address { get; set; }
    public object otp { get; set; }
    public int authentic { get; set; }
    public int isOnline { get; set; }
    public string fromWeb { get; set; }
    public int fromDevice { get; set; }
    public int createdAt { get; set; }
    public int updatedAt { get; set; }
    public DateTime? lastActivedAt { get; set; }
    public int time_login { get; set; }
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
    public int scan_ai365 { get; set; }
    public int scan_apply { get; set; }
    public int scan_audio { get; set; }
}

public class Employee_kh
{
    public int com_id { get; set; }
    public int organizeDetailId { get; set; }
    public int dep_id { get; set; }
    public double start_working_time { get; set; }
    public int position_id { get; set; }
    public int team_id { get; set; }
    public int group_id { get; set; }
    public int time_quit_job { get; set; }
    public object ep_description { get; set; }
    public object ep_featured_recognition { get; set; }
    public string ep_status { get; set; }
    public int ep_signature { get; set; }
    public int allow_update_face { get; set; }
    public int version_in_use { get; set; }
    public int role_id { get; set; }
    public string _id { get; set; }
    public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
}

public class InForPerson
{
    public int scan { get; set; }
    public Account_kh account { get; set; }
    public Employee_kh employee { get; set; }
    public object candidate { get; set; }
    public string _id { get; set; }
}

public class ListOrganizeDetailId
{
    public int level { get; set; }
    public int organizeDetailId { get; set; }
}

public class Root_kh
{
    public List<Datum_kh> data { get; set; }
    public string message { get; set; }
}

public class TinhluongThietLap
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

public class TlPhanTram
{
    [JsonProperty("$numberDecimal")]
    public string numberDecimal { get; set; }
}

