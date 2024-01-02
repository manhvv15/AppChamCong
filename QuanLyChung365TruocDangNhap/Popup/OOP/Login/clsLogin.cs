using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.Popup.OOP.Login
{
    public class clsLogin
    {
        public class ComInfo
        {
            public int com_id { get; set; }
            public string com_email { get; set; }
        }

        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public Data data { get; set; }
            public int _id { get; set; }
            public ComInfo com_info { get; set; }
            public int authentic { get; set; }
            public UserInfo user_info { get; set; }
            public string access_token { get; set; }
            public string refresh_token { get; set; }
            public string type { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

        public class UserInfo
        {
            public int ep_id { get; set; }
            public int? ep_gender { get; set; }
            public int? ep_married { get; set; }
            public int? ep_education { get; set; }
            public int? ep_exp { get; set; }
            public int? ep_authentic { get; set; }
            public int? role_id { get; set; }
            public int? group_id { get; set; }
            public int? position_id { get; set; }
            public int? ep_signature { get; set; }
            public int? allow_update_face { get; set; }
            public int? version_in_use { get; set; }
            public long? ep_id_tv365 { get; set; }
            public long create_time { get; set; }
            public long update_time { get; set; }
            public long start_working_time { get; set; }
            public string ep_status { get; set; }
            public string ep_email { get; set; }
            public string ep_phone_tk { get; set; }
            public string ep_name { get; set; }
            public string ep_phone { get; set; }
            public string ep_email_lh { get; set; }
            public string ep_pass { get; set; }
            public string ep_address { get; set; }
            public string ep_birth_day { get; set; }
            public string ep_image { get; set; }
            public string ep_description { get; set; }
            public string ep_featured_recognition { get; set; }
            

            public int com_id { get; set; }
            public string com_name { get; set; }
            public string com_email { get; set; }
            public string com_phone_tk { get; set; }
            public string type_timekeeping { get; set; }
            public string id_way_timekeeping { get; set; }
            public object com_logo { get; set; }
            public string com_pass { get; set; }
            public object com_address { get; set; }
            public int com_role_id { get; set; }
            public int com_size { get; set; }
            public string com_description { get; set; }
            public int com_create_time { get; set; }
            public long com_update_time { get; set; }
            public int com_authentic { get; set; }
            public object com_lat { get; set; }
            public object com_lng { get; set; }
            public string com_qr_logo { get; set; }
            public int enable_scan_qr { get; set; }
            public int com_vip { get; set; }
            public int com_ep_vip { get; set; }
            public int ep_crm { get; set; }
            public int scan { get; set; }
            public string com_pass_encrypt { get; set; }
            public string com_path { get; set; }
            public string base36_path { get; set; }
            public string from_source { get; set; }
            public string com_id_tv365 { get; set; }
            public string com_quantity_time { get; set; }
            public string com_kd { get; set; }
            public string check_phone { get; set; }
        }
    }
}
