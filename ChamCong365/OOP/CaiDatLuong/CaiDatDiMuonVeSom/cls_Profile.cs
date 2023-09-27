using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatDiMuonVeSom
{
    public class cls_Profile
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        //public class Account
        //{
        //    //public int birthday { get; set; }
        //    //public int gender { get; set; }
        //    //public int married { get; set; }
        //    //public int experience { get; set; }
        //    //public int education { get; set; }
        //    //public string _id { get; set; }
        //}

        //public class Candidate
        //{
        //    public int use_type { get; set; }
        //    public int user_reset_time { get; set; }
        //    public int use_view { get; set; }
        //    public int use_noti { get; set; }
        //    public int use_show { get; set; }
        //    public int use_show_cv { get; set; }
        //    public int use_active { get; set; }
        //    public int use_td { get; set; }
        //    public int use_check { get; set; }
        //    public int use_test { get; set; }
        //    public int use_badge { get; set; }
        //    public int point_time_active { get; set; }
        //    public string cv_title { get; set; }
        //    public object cv_muctieu { get; set; }
        //    public object cv_kynang { get; set; }
        //    public object cv_giai_thuong { get; set; }
        //    public object cv_hoat_dong { get; set; }
        //    public object cv_so_thich { get; set; }
        //    public List<object> cv_city_id { get; set; }
        //    public List<object> cv_cate_id { get; set; }
        //    public int cv_capbac_id { get; set; }
        //    public int cv_money_id { get; set; }
        //    public int cv_loaihinh_id { get; set; }
        //    public int cv_time { get; set; }
        //    public int cv_time_dl { get; set; }
        //    //public int um_type { get; set; }
        //    public object um_min_value { get; set; }
        //    public object um_max_value { get; set; }
        //    //public int um_unit { get; set; }
        //    public object cv_tc_name { get; set; }
        //    public object cv_tc_cv { get; set; }
        //    public object cv_tc_dc { get; set; }
        //    public object cv_tc_phone { get; set; }
        //    public object cv_tc_email { get; set; }
        //    public object cv_tc_company { get; set; }
        //    public object cv_video { get; set; }
        //    public int cv_video_type { get; set; }
        //    public int cv_video_active { get; set; }
        //    public object use_ip { get; set; }
        //    public int percents { get; set; }
        //    public int vip { get; set; }
        //    public int check_create_usc { get; set; }
        //    public int emp_id { get; set; }
        //    public string _id { get; set; }
        //    public List<object> profileDegree { get; set; }
        //    public List<object> profileNgoaiNgu { get; set; }
        //    public List<object> profileExperience { get; set; }
        //}

        //public class Cds
        //{
        //    public object com_parent_id { get; set; }
        //    public string type_timekeeping { get; set; }
        //    public string id_way_timekeeping { get; set; }
        //    public int com_role_id { get; set; }
        //    public string com_qr_logo { get; set; }
        //    public int enable_scan_qr { get; set; }
        //    public int com_vip { get; set; }
        //    public int com_ep_vip { get; set; }
        //    public int com_vip_time { get; set; }
        //    public int ep_crm { get; set; }
        //    public int ep_stt { get; set; }
        //}

        ////public class Company
        ////{
        ////    public int _id { get; set; }
        ////    public string email { get; set; }
        ////    public string phoneTK { get; set; }
        ////    public string userName { get; set; }
        ////    public object alias { get; set; }
        ////    public string phone { get; set; }
        ////    public string emailContact { get; set; }
        ////    public string avatarUser { get; set; }
        ////    public int type { get; set; }
        ////    public string password { get; set; }
        ////    public object city { get; set; }
        ////    public object district { get; set; }
        ////    public object address { get; set; }
        ////    public object otp { get; set; }
        ////    public int authentic { get; set; }
        ////    public int isOnline { get; set; }
        ////    public string fromWeb { get; set; }
        ////    public int fromDevice { get; set; }
        ////    public int createdAt { get; set; }
        ////    public int updatedAt { get; set; }
        ////    public DateTime lastActivedAt { get; set; }
        ////    public int time_login { get; set; }
        ////    public int role { get; set; }
        ////    public object latitude { get; set; }
        ////    public object longtitude { get; set; }
        ////    public int idQLC { get; set; }
        ////    public int idTimViec365 { get; set; }
        ////    public int idRaoNhanh365 { get; set; }
        ////    public string chat365_secret { get; set; }
        ////    public int chat365_id { get; set; }
        ////    public int scan_base365 { get; set; }
        ////    public int check_chat { get; set; }
        ////    public List<object> sharePermissionId { get; set; }
        ////    public object inForPerson { get; set; }
        ////    public InForCompany inForCompany { get; set; }
        ////    public object inforRN365 { get; set; }
        ////    public ConfigChat configChat { get; set; }
        ////    public int scan { get; set; }
        ////}

        //public class ConfigChat
        //{
        //    public int notificationAcceptOffer { get; set; }
        //    public int notificationAllocationRecall { get; set; }
        //    public int notificationChangeSalary { get; set; }
        //    public int notificationCommentFromRaoNhanh { get; set; }
        //    public int notificationCommentFromTimViec { get; set; }
        //    public int notificationDecilineOffer { get; set; }
        //    public int notificationMissMessage { get; set; }
        //    public int notificationNTDExpiredPin { get; set; }
        //    public int notificationNTDExpiredRecruit { get; set; }
        //    public int notificationNTDPoint { get; set; }
        //    public int notificationSendCandidate { get; set; }
        //    public int notificationTag { get; set; }
        //    public List<object> removeSugges { get; set; }
        //    public string userNameNoVn { get; set; }
        //    public int doubleVerify { get; set; }
        //    public int active { get; set; }
        //    public string status { get; set; }
        //    public int acceptMessStranger { get; set; }
        //    public List<object> HistoryAccess { get; set; }
        //}

        //public class Data
        //{
        //    public InfoDepCom info_dep_com { get; set; }
        //    public object info_emp_start { get; set; }
        //    public List<object> info_basic_salary { get; set; }
        //    public List<object> info_contract { get; set; }
        //    public List<object> info_family { get; set; }
        //    public List<object> info_donate { get; set; }
        //    public List<object> info_class { get; set; }
        //}

        ////public class Employee
        ////{
        ////    public int com_id { get; set; }
        ////    public int dep_id { get; set; }
        ////    public int start_working_time { get; set; }
        ////    public int position_id { get; set; }
        ////    public int team_id { get; set; }
        ////    public int group_id { get; set; }
        ////    public int time_quit_job { get; set; }
        ////    public object ep_description { get; set; }
        ////    public string ep_featured_recognition { get; set; }
        ////    public string ep_status { get; set; }
        ////    public int ep_signature { get; set; }
        ////    public int allow_update_face { get; set; }
        ////    public int version_in_use { get; set; }
        ////    public string _id { get; set; }
        ////}

        //public class InfoDepCom
        //{
        //    public User user { get; set; }
        //    //public Company company { get; set; }
        //    public Department department { get; set; }
        //    public object postion { get; set; }
        //}

        ////public class InForCompany
        ////{
        ////    public int scan { get; set; }
        ////    public int usc_kd { get; set; }
        ////    public int usc_kd_first { get; set; }
        ////    public object description { get; set; }
        ////    public int com_size { get; set; }
        ////    public Timviec365 timviec365 { get; set; }
        ////    public Cds cds { get; set; }
        ////    public string _id { get; set; }
        ////}

        ////public class InForPerson
        ////{
        ////    public int scan { get; set; }
        ////    public Account account { get; set; }
        ////    public Employee employee { get; set; }
        ////    public Candidate candidate { get; set; }
        ////    public string _id { get; set; }
        ////}
        //public class Department
        //{
        //    public string _id { get; set; }
        //    public int dep_id { get; set; }
        //    public int com_id { get; set; }
        //    public string dep_name { get; set; }
        //    public string dep_create_time { get; set; }
        //}
        //public class Root
        //{
        //    public Data data { get; set; }
        //    public string message { get; set; }
        //}

        //public class Timviec365
        //{
        //    public int usc_update_new { get; set; }
        //    public object usc_name { get; set; }
        //    public object usc_name_add { get; set; }
        //    public object usc_name_phone { get; set; }
        //    public object usc_name_email { get; set; }
        //    public object usc_canonical { get; set; }
        //    public object usc_md5 { get; set; }
        //    public int usc_type { get; set; }
        //    public int usc_size { get; set; }
        //    public object usc_website { get; set; }
        //    public int usc_view_count { get; set; }
        //    public int usc_active { get; set; }
        //    public int usc_show { get; set; }
        //    public int usc_mail { get; set; }
        //    public int usc_stop_mail { get; set; }
        //    public int usc_utl { get; set; }
        //    public int usc_ssl { get; set; }
        //    public object usc_mst { get; set; }
        //    public object usc_security { get; set; }
        //    public object usc_ip { get; set; }
        //    public int usc_loc { get; set; }
        //    public int usc_mail_app { get; set; }
        //    public object usc_video { get; set; }
        //    public int usc_video_type { get; set; }
        //    public int usc_video_active { get; set; }
        //    public int usc_block_account { get; set; }
        //    public int usc_stop_noti { get; set; }
        //    public int otp_time_exist { get; set; }
        //    public int use_test { get; set; }
        //    public int usc_badge { get; set; }
        //    public int usc_star { get; set; }
        //    public int usc_vip { get; set; }
        //    public object usc_manager { get; set; }
        //    public object usc_license { get; set; }
        //    public int usc_active_license { get; set; }
        //    public object usc_map { get; set; }
        //    public object usc_dgc { get; set; }
        //    public object usc_dgtv { get; set; }
        //    public int usc_dg_time { get; set; }
        //    public object usc_skype { get; set; }
        //    public object usc_video_com { get; set; }
        //    public object usc_lv { get; set; }
        //    public object usc_zalo { get; set; }
        //    public int usc_cc365 { get; set; }
        //    public int usc_crm { get; set; }
        //    public object usc_images { get; set; }
        //    public int usc_active_img { get; set; }
        //    public int usc_founded_time { get; set; }
        //    public List<object> usc_branches { get; set; }
        //}

        //public class User
        //{
        //    public int _id { get; set; }
        //    public string email { get; set; }
        //    public object phoneTK { get; set; }
        //    public string userName { get; set; }
        //    public object alias { get; set; }
        //    public string phone { get; set; }
        //    public object emailContact { get; set; }
        //    public string avatarUser { get; set; }
        //    public int type { get; set; }
        //    //public int city { get; set; }
        //    //public int district { get; set; }
        //    public string address { get; set; }
        //    public object otp { get; set; }
        //    public int authentic { get; set; }
        //    public int isOnline { get; set; }
        //    public string fromWeb { get; set; }
        //    public int fromDevice { get; set; }
        //    public int createdAt { get; set; }
        //    //public int updatedAt { get; set; }
        //    public object lastActivedAt { get; set; }
        //    public int time_login { get; set; }
        //    public int role { get; set; }
        //    public string latitude { get; set; }
        //    public string longtitude { get; set; }
        //    public int idQLC { get; set; }
        //    public int idTimViec365 { get; set; }
        //    public int idRaoNhanh365 { get; set; }
        //    public string chat365_secret { get; set; }
        //    public int chat365_id { get; set; }
        //    public int scan_base365 { get; set; }
        //    public int check_chat { get; set; }
        //    public List<object> sharePermissionId { get; set; }
        //    //public InForPerson inForPerson { get; set; }
        //    public object inForCompany { get; set; }
        //    public object inforRN365 { get; set; }
        //    public ConfigChat configChat { get; set; }
        //    public int scan { get; set; }
        //}
        //Tiếp tiếp

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Account
        {
            //public object birthday { get; set; }
            //public int gender { get; set; }
            //public int married { get; set; }
            //public int experience { get; set; }
            //public int education { get; set; }
            //public string _id { get; set; }
        }

        public class Cds
        {
            public int com_role_id { get; set; }
            public object com_parent_id { get; set; }
            public string type_timekeeping { get; set; }
            public string id_way_timekeeping { get; set; }
            public string com_qr_logo { get; set; }
            public int enable_scan_qr { get; set; }
            public int com_vip { get; set; }
            public int com_ep_vip { get; set; }
            public int com_vip_time { get; set; }
            public int ep_crm { get; set; }
            public int ep_stt { get; set; }
        }

        //public class Company
        //{
        //    public int _id { get; set; }
        //    public string email { get; set; }
        //    public string phoneTK { get; set; }
        //    public string userName { get; set; }
        //    public string alias { get; set; }
        //    public string phone { get; set; }
        //    public string emailContact { get; set; }
        //    public object avatarUser { get; set; }
        //    public int type { get; set; }
        //    public string password { get; set; }
        //    public int city { get; set; }
        //    public int district { get; set; }
        //    public string address { get; set; }
        //    public string otp { get; set; }
        //    public int authentic { get; set; }
        //    public int isOnline { get; set; }
        //    public string fromWeb { get; set; }
        //    public int fromDevice { get; set; }
        //    public int createdAt { get; set; }
        //    public int updatedAt { get; set; }
        //    public DateTime lastActivedAt { get; set; }
        //    public int time_login { get; set; }
        //    public int role { get; set; }
        //    public string latitude { get; set; }
        //    public string longtitude { get; set; }
        //    public int idQLC { get; set; }
        //    public int idTimViec365 { get; set; }
        //    public int idRaoNhanh365 { get; set; }
        //    public string chat365_secret { get; set; }
        //    public int chat365_id { get; set; }
        //    public int scan_base365 { get; set; }
        //    public int check_chat { get; set; }
        //    public List<object> sharePermissionId { get; set; }
        //    public object inForPerson { get; set; }
        //    public InForCompany inForCompany { get; set; }
        //    public object inforRN365 { get; set; }
        //    public ConfigChat configChat { get; set; }
        //    public int scan { get; set; }
        //}

        public class ConfigChat
        {
            public int notificationAcceptOffer { get; set; }
            public int notificationAllocationRecall { get; set; }
            public int notificationChangeSalary { get; set; }
            public int notificationCommentFromRaoNhanh { get; set; }
            public int notificationCommentFromTimViec { get; set; }
            public int notificationDecilineOffer { get; set; }
            public int notificationMissMessage { get; set; }
            public int notificationNTDExpiredPin { get; set; }
            public int notificationNTDExpiredRecruit { get; set; }
            public int notificationNTDPoint { get; set; }
            public int notificationSendCandidate { get; set; }
            public int notificationTag { get; set; }
            public List<object> removeSugges { get; set; }
            public string userNameNoVn { get; set; }
            public int doubleVerify { get; set; }
            public int active { get; set; }
            public string status { get; set; }
            public int acceptMessStranger { get; set; }
            public List<object> HistoryAccess { get; set; }
        }

        public class Data
        {
            public InfoDepCom info_dep_com { get; set; }
            public object info_emp_start { get; set; }
            public List<InfoBasicSalary> info_basic_salary { get; set; }
            public List<InfoContract> info_contract { get; set; }
            public List<object> info_family { get; set; }
            public List<object> info_donate { get; set; }
            public List<object> info_class { get; set; }
        }

        public class Department
        {
            public string _id { get; set; }
            public int dep_id { get; set; }
            public int com_id { get; set; }
            public string dep_name { get; set; }
            public DateTime dep_create_time { get; set; }
            public object manager_id { get; set; }
            public int dep_order { get; set; }
        }

        //public class Employee
        //{
        //    public int com_id { get; set; }
        //    public int dep_id { get; set; }
        //    public int start_working_time { get; set; }
        //    public int position_id { get; set; }
        //    public int team_id { get; set; }
        //    public int group_id { get; set; }
        //    public int time_quit_job { get; set; }
        //    public object ep_description { get; set; }
        //    public object ep_featured_recognition { get; set; }
        //    public string ep_status { get; set; }
        //    public int ep_signature { get; set; }
        //    public int allow_update_face { get; set; }
        //    public int version_in_use { get; set; }
        //    public string _id { get; set; }
        //}

        public class InfoBasicSalary
        {
            public string _id { get; set; }
            public int sb_id { get; set; }
            public int sb_id_user { get; set; }
            public int sb_id_com { get; set; }
            public int sb_salary_basic { get; set; }
            public int sb_salary_bh { get; set; }
            public int sb_pc_bh { get; set; }
            public DateTime sb_time_up { get; set; }
            public int sb_location { get; set; }
            public string sb_lydo { get; set; }
            public string sb_quyetdinh { get; set; }
            public int sb_first { get; set; }
            public DateTime sb_time_created { get; set; }
            public int sb_type { get; set; }
        }

        public class InfoContract
        {
            public string _id { get; set; }
            public int con_id { get; set; }
            public int con_id_user { get; set; }
            public string con_name { get; set; }
            public DateTime con_time_up { get; set; }
            public DateTime con_time_end { get; set; }
            public string con_file { get; set; }
            public int con_salary_persent { get; set; }
            public DateTime con_time_created { get; set; }
        }

        public class InfoDepCom
        {
            public User user { get; set; }
            //public Company company { get; set; }
            public Department department { get; set; }
            public object postion { get; set; }
        }

        //public class InForCompany
        //{
        //    public int scan { get; set; }
        //    public int usc_kd { get; set; }
        //    public int usc_kd_first { get; set; }
        //    public string description { get; set; }
        //    public int com_size { get; set; }
        //    public Timviec365 timviec365 { get; set; }
        //    public Cds cds { get; set; }
        //    public string _id { get; set; }
        //}

        //public class InForPerson
        //{
        //    public int scan { get; set; }
        //    public Account account { get; set; }
        //    public Employee employee { get; set; }
        //    public object candidate { get; set; }
        //    public string _id { get; set; }
        //}

        public class Root
        {
            public Data data { get; set; }
            public string message { get; set; }
        }

        public class Timviec365
        {
            public string usc_name { get; set; }
            public string usc_name_add { get; set; }
            public string usc_name_phone { get; set; }
            public string usc_name_email { get; set; }
            public int usc_update_new { get; set; }
            public string usc_canonical { get; set; }
            public string usc_md5 { get; set; }
            public string usc_redirect { get; set; }
            public int usc_type { get; set; }
            public int usc_size { get; set; }
            public string usc_website { get; set; }
            public int usc_view_count { get; set; }
            public int usc_active { get; set; }
            public int usc_show { get; set; }
            public int usc_mail { get; set; }
            public int usc_stop_mail { get; set; }
            public int usc_utl { get; set; }
            public int usc_ssl { get; set; }
            public string usc_mst { get; set; }
            public string usc_security { get; set; }
            public string usc_ip { get; set; }
            public int usc_loc { get; set; }
            public int usc_mail_app { get; set; }
            public string usc_video { get; set; }
            public int usc_video_type { get; set; }
            public int usc_video_active { get; set; }
            public int usc_block_account { get; set; }
            public int usc_stop_noti { get; set; }
            public int otp_time_exist { get; set; }
            public int use_test { get; set; }
            public int usc_badge { get; set; }
            public int usc_star { get; set; }
            public int usc_vip { get; set; }
            public string usc_manager { get; set; }
            public string usc_license { get; set; }
            public int usc_active_license { get; set; }
            public string usc_map { get; set; }
            public string usc_dgc { get; set; }
            public string usc_dgtv { get; set; }
            public int usc_dg_time { get; set; }
            public string usc_skype { get; set; }
            public string usc_video_com { get; set; }
            public string usc_lv { get; set; }
            public object usc_zalo { get; set; }
            public int usc_cc365 { get; set; }
            public int usc_crm { get; set; }
            public object usc_images { get; set; }
            public int usc_active_img { get; set; }
            public int usc_founded_time { get; set; }
            public List<object> usc_branches { get; set; }
        }

        public class User
        {
            public int _id { get; set; }
            public string email { get; set; }
            public string phoneTK { get; set; }
            public string userName { get; set; }
            public object alias { get; set; }
            public string phone { get; set; }
            public string emailContact { get; set; }
            public string avatarUser { get; set; }
            public int type { get; set; }
            //public int city { get; set; }
            //public int district { get; set; }
            public string address { get; set; }
            public object otp { get; set; }
            public int authentic { get; set; }
            public int isOnline { get; set; }
            public string fromWeb { get; set; }
            public int fromDevice { get; set; }
            public int createdAt { get; set; }
            //public int updatedAt { get; set; }
            public object lastActivedAt { get; set; }
            public int time_login { get; set; }
            public int role { get; set; }
            public object latitude { get; set; }
            public object longtitude { get; set; }
            public int idQLC { get; set; }
            public int idTimViec365 { get; set; }
            public int idRaoNhanh365 { get; set; }
            public string chat365_secret { get; set; }
            public List<object> sharePermissionId { get; set; }
            //public InForPerson inForPerson { get; set; }
            public object inForCompany { get; set; }
            public object inforRN365 { get; set; }
            public ConfigChat configChat { get; set; }
            public int scan { get; set; }
        }



    }
}
