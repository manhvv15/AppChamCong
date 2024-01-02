using QuanLyChung365TruocDangNhap.ChamCongNew.APIs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.Tax
{
    public class clsNSuChuaTL
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Account
        {
            public double? birthday { get; set; }
            public int gender { get; set; }
            public int married { get; set; }
            public int? experience { get; set; }
            public int? education { get; set; }
            public string _id { get; set; }
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

        public class Content
        {
            public string key { get; set; }
            public string value { get; set; }
            public int? image { get; set; }
        }

        public class Employee
        {
            public int com_id { get; set; }
            public int dep_id { get; set; }
            public double? start_working_time { get; set; }
            public int? position_id { get; set; }
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
            public int? organizeDetailId { get; set; }
            public int? role_id { get; set; }
            public List<ListOrganizeDetailId> listOrganizeDetailId { get; set; }
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
            public string _id { get; set; }
        }

        public class ListOrganizeDetailId
        {
            public int? level { get; set; }
            public int organizeDetailId { get; set; }
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
            public int? created_time { get; set; }
            public int? update_time { get; set; }
        }
        
        public class ListUserFinal
        {
            public int _id { get; set; }
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
            public int type { get; set; }
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
            public int? idQLC { get; set; }
            public int? idTimViec365 { get; set; }
            public int? idRaoNhanh365 { get; set; }
            public string chat365_secret { get; set; }
            public List<object> sharePermissionId { get; set; }
            public InForPerson inForPerson { get; set; }
            public InForCompany inForCompany { get; set; }
            public object inforRN365 { get; set; }
            public int? scan { get; set; }
            public int? scanElacticAdmin { get; set; }
            public int? chat365_id { get; set; }
            public int? scan_base365 { get; set; }
            public int? check_chat { get; set; }
            public bool? emotion_active { get; set; }
            public OrganizeDetail organizeDetail { get; set; }
            public int? isAdmin { get; set; }
            public int? scan_ai365 { get; set; }
        }

        public class Root
        {
            public bool data { get; set; }
            public string message { get; set; }
            public List<ListUserFinal> listUserFinal { get; set; }
        }
        public class Timviec365
        {
            public string usc_name { get; set; }
            public string usc_name_add { get; set; }
            public string usc_name_email { get; set; }
            public int? usc_type { get; set; }
            public string usc_lv { get; set; }
            public string usc_name_phone { get; set; }
            public int? usc_update_new { get; set; }
            public object usc_canonical { get; set; }
            public object usc_md5 { get; set; }
            public int? usc_size { get; set; }
            public string usc_website { get; set; }
            public int? usc_view_count { get; set; }
            public int? usc_active { get; set; }
            public int? usc_show { get; set; }
            public int? usc_mail { get; set; }
            public int?  usc_stop_mail { get; set; }
            public int usc_utl { get; set; }
            public int? usc_ssl { get; set; }
            public string usc_mst { get; set; }
            public object usc_security { get; set; }
            public string usc_ip { get; set; }
            public int? usc_loc { get; set; }
            public int? usc_mail_app { get; set; }
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
            public object usc_map { get; set; }
            public object usc_dgc { get; set; }
            public object usc_dgtv { get; set; }
            public int? usc_dg_time { get; set; }
            public string usc_skype { get; set; }
            public object usc_video_com { get; set; }
            public string usc_zalo { get; set; }
            public int? usc_cc365 { get; set; }
            public int? usc_crm { get; set; }
            public object usc_images { get; set; }
            public int? usc_active_img { get; set; }
            public int? usc_founded_time { get; set; }
            public List<object> usc_branches { get; set; }
            public int? usc_xacthuc_email { get; set; }
            public string usc_note { get; set; }
            public int? usc_test { get; set; }
        }
       

        //bỏ
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

    }
}
