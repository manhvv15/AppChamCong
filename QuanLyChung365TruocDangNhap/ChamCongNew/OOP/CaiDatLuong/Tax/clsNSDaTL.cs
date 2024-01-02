using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.Tax
{
    public class clsNSDaTL
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Account
        {
            public double birthday { get; set; }
            public int gender { get; set; }
            public int married { get; set; }
            public int experience { get; set; }
            public int education { get; set; }
            public string _id { get; set; }
        }

        public class Candidate
        {
            public int? use_type { get; set; }
            public int? user_reset_time { get; set; }
            public int? use_view { get; set; }
            public int? use_noti { get; set; }
            public int? use_show { get; set; }
            public int? use_show_cv { get; set; }
            public int? use_active { get; set; }
            public int? use_td { get; set; }
            public int? use_check { get; set; }
            public int? use_test { get; set; }
            public int? point_time_active { get; set; }
            public string cv_title { get; set; }
            public object cv_muctieu { get; set; }
            public List<object> cv_city_id { get; set; }
            public List<object> cv_cate_id { get; set; }
            public int? cv_capbac_id { get; set; }
            public int? cv_money_id { get; set; }
            public int? cv_loaihinh_id { get; set; }
            public int? cv_time { get; set; }
            public int? cv_time_dl { get; set; }
            public object cv_kynang { get; set; }
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
            public int? cv_video_type { get; set; }
            public int? cv_video_active { get; set; }
            public object use_ip { get; set; }
            public int? time_send_vl { get; set; }
            public int? percents { get; set; }
            public int? vip { get; set; }
            public int? check_create_usc { get; set; }
            public int? emp_id { get; set; }
            public string _id { get; set; }
            public List<object> profileDegree { get; set; }
            public List<object> profileNgoaiNgu { get; set; }
            public List<object> profileExperience { get; set; }
            public int? anhsao_badge { get; set; }
            public int? use_badge { get; set; }
            public object cv_giai_thuong { get; set; }
            public object cv_hoat_dong { get; set; }
            public object cv_so_thich { get; set; }
            public object cv_address { get; set; }
            public int? um_type { get; set; }
            public int? check_create_use { get; set; }
            public int? scan_audio { get; set; }
            public object audio { get; set; }
            public int? scan_elastic { get; set; }
            public int? star3 { get; set; }
            public int? time_active_star3 { get; set; }
        }

        public class Cds
        {
            public int com_role_id { get; set; }
            public object com_parent_id { get; set; }
            public string type_timekeeping { get; set; }
            public string id_way_timekeeping { get; set; }
            public object com_qr_logo { get; set; }
            public int? enable_scan_qr { get; set; }
            public int? com_vip { get; set; }
            public int? com_ep_vip { get; set; }
            public int? com_vip_time { get; set; }
            public int? ep_crm { get; set; }
            public int? ep_stt { get; set; }
        }

        public class ConfigChat
        {
            public int? notificationAcceptOffer { get; set; }
            public int? notificationAllocationRecall { get; set; }
            public int? notificationChangeSalary { get; set; }
            public int? notificationCommentFromRaoNhanh { get; set; }
            public int? notificationCommentFromTimViec { get; set; }
            public int? notificationDecilineOffer { get; set; }
            public int? notificationMissMessage { get; set; }
            public int? notificationNTDExpiredPin { get; set; }
            public int? notificationNTDExpiredRecruit { get; set; }
            public int? notificationNTDPoint { get; set; }
            public int? notificationSendCandidate { get; set; }
            public int? notificationTag { get; set; }
            public List<object> removeSugges { get; set; }
            public string userNameNoVn { get; set; }
            public int? doubleVerify { get; set; }
            public int? active { get; set; }
            public string status { get; set; }
            public int? acceptMessStranger { get; set; }
            public List<object> HistoryAccess { get; set; }

        }

        public class Content
        {
            public string key { get; set; }
            public string value { get; set; }
            public int image { get; set; }
        }

        public class Detail
        {
            public string _id { get; set; }
            public string email { get; set; }
            public string phoneTK { get; set; }
            public string userName { get; set; }
            public string alias { get; set; }
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
            public int? type { get; set; }
            public int? city { get; set; }
            public int? district { get; set; }
            public string address { get; set; }
            public string otp { get; set; }
            public int? authentic { get; set; }
            public int? isOnline { get; set; }
            public string fromWeb { get; set; }
            public int? fromDevice { get; set; }
            public double createdAt { get; set; }
            public double updatedAt { get; set; }
            public DateTime? lastActivedAt { get; set; }
            public int? time_login { get; set; }
            public int? role { get; set; }
            public string latitude { get; set; }
            public string longtitude { get; set; }
            public int idQLC { get; set; }
            public int idTimViec365 { get; set; }
            public int? idRaoNhanh365 { get; set; }
            public string chat365_secret { get; set; }
            public List<object> sharePermissionId { get; set; }
            public InForPerson inForPerson { get; set; }
            public List<InForCompany> inForCompany { get; set; }
            public object inforRN365 { get; set; }
            public ConfigChat configChat { get; set; }
            public int? scan { get; set; }
            public int? chat365_id { get; set; }
            public int? scan_base365 { get; set; }
            public int? check_chat { get; set; }
            public int? isAdmin { get; set; }
            public int? scanElacticAdmin { get; set; }
            public bool? emotion_active { get; set; }
            public int? scan_ai365 { get; set; }
            public int? scan_cv { get; set; }
        }

        public class Employee
        {
            public string com_id { get; set; }
            public int dep_id { get; set; }
            public string start_working_time { get; set; }
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
            public string _id { get; set; }
            public int role_id { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public int organizeDetailId { get; set; }
        }

        public class InForCompany
        {
            public int? scan { get; set; }
            public int? usc_kd { get; set; }
            public int? usc_kd_first { get; set; }
            public string description { get; set; }
            public int? com_size { get; set; }
            public Timviec365 timviec365 { get; set; }
            public Cds cds { get; set; }
            public string _id { get; set; }
        }

        public class InForPerson
        {  
            public int? scan { get; set; }
            public Account account { get; set; }
            public Employee employee { get; set; }
            public Candidate candidate { get; set; }
            public string _id { get; set; }
        }
        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int organizeDetailId { get; set; }
        }
        public class ListU
        {
            public string userName { get; set; }
            public string dep_Id { get; set; }
            public string organizelName { get; set; }
            public string CSThue { get; set; }
            public string CongThuc { get; set; }
            public string Avatar_Us { get; set; }
            public string ToChuc { get; set; }

            public string _id { get; set; }
            public int cls_id { get; set; }
            public int cls_id_cl { get; set; }
            public int cls_id_user { get; set; }
            public DateTime cls_day { get; set; }
            public string cls_day_format { get; set; }
            public DateTime cls_day_end { get; set; }
            public string cls_day_end_format { get; set; }
            public List<TinhluongListClass> TinhluongListClass { get; set; }
            public List<TinhluongFormSalary> TinhluongFormSalary { get; set; }
            public Detail Detail { get; set; }
            public OrganizeDetail organizeDetail { get; set; }
        }

        public class OrganizeDetail
        {
            public string _id { get; set; }
            public int id { get; set; }
            public int comId { get; set; }
            public int? parentId { get; set; }
            public string organizeDetailName { get; set; }
            public int? level { get; set; }
            public int? range { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
            public List<Content> content { get; set; }
            public double created_time { get; set; }
            public double update_time { get; set; }
        }
        public class Root
        {
            public bool data { get; set; }
            public string message { get; set; }
            public List<ListU> list_us { get; set; }
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
            public int? usc_type { get; set; }
            public int? usc_size { get; set; }
            public string usc_website { get; set; }
            public int? usc_view_count { get; set; }
            public int? usc_active { get; set; }
            public int? usc_show { get; set; }
            public int? usc_mail { get; set; }
            public int? usc_stop_mail { get; set; }
            public int? usc_utl { get; set; }
            public int? usc_ssl { get; set; }
            public string usc_mst { get; set; }
            public string usc_security { get; set; }
            public string usc_ip { get; set; }
            public int? usc_loc { get; set; }
            public int?  usc_mail_app { get; set; }
            public string usc_video { get; set; }
            public int? usc_video_type { get; set; }
            public int? usc_video_active { get; set; }
            public int? usc_block_account { get; set; }
            public int? usc_stop_noti { get; set; }
            public int? otp_time_exist { get; set; }
            public int? use_test { get; set; }
            public int? usc_badge { get; set; }
            public int? usc_star { get; set; }
            public int? usc_vip { get; set; }
            public string usc_manager { get; set; }
            public string usc_license { get; set; }
            public int? usc_active_license { get; set; }
            public string usc_map { get; set; }
            public string usc_dgc { get; set; }
            public string usc_dgtv { get; set; }
            public int? usc_dg_time { get; set; }
            public string usc_skype { get; set; }
            public string usc_video_com { get; set; }
            public string usc_lv { get; set; }
            public object usc_zalo { get; set; }
            public int? usc_cc365 { get; set; }
            public int? usc_crm { get; set; }
            public object usc_images { get; set; }
            public int? usc_active_img { get; set; }
            public int? usc_founded_time { get; set; }
            public List<object> usc_branches { get; set; }
        }

        public class TinhluongFormSalary
        {
            public string _id { get; set; }
            public int fs_id { get; set; }
            public int fs_id_com { get; set; }
            public int fs_type { get; set; }
            public string fs_name { get; set; }
            public int fs_data { get; set; }
            public string fs_repica { get; set; }
            public string fs_note { get; set; }
            public DateTime fs_time_created { get; set; }
        }

        public class TinhluongListClass
        {
            public string cl_name { get; set; }
            public int cl_active { get; set; }
            public int cl_type { get; set; }
            public int cl_id_form { get; set; }
        }


    }
}
