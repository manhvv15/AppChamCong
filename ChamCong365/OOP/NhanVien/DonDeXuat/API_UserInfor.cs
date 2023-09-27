using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.NhanVien.DonDeXuat
{
    public class API_UserInfor
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class DataAPI_UerInfor
        {
            public bool result { get; set; }
            public string message { get; set; }
            public DataAPI_UerInfor data { get; set; }
            public int _id { get; set; }
            public UserInfo user_info { get; set; }
            public string access_token { get; set; }
            public string refresh_token { get; set; }
            public int type { get; set; }
        }

        public class API_UerInfor
        {
            public DataAPI_UerInfor data { get; set; }
            public object error { get; set; }
        }

        public class UserInfo
        {
            public int ep_id { get; set; }
            public object ep_mail { get; set; }
            public string ep_phone_tk { get; set; }
            public string ep_name { get; set; }
            public string ep_phone { get; set; }
            public object ep_email_lh { get; set; }
            public string ep_pass { get; set; }
            public int com_id { get; set; }
            public int dep_id { get; set; }
            public string ep_address { get; set; }
            public object ep_birth_day { get; set; }
            public int ep_gender { get; set; }
            public int ep_married { get; set; }
            public int ep_education { get; set; }
            public int ep_exp { get; set; }
            public int ep_authentic { get; set; }
            public int role_id { get; set; }
            public object ep_image { get; set; }
            public int create_time { get; set; }
            public int update_time { get; set; }
            public int start_working_time { get; set; }
            public int position_id { get; set; }
            public int group_id { get; set; }
            public object ep_description { get; set; }
            public object ep_featured_recognition { get; set; }
            public string ep_status { get; set; }
            public int ep_signature { get; set; }
            public int allow_update_face { get; set; }
            public int version_in_use { get; set; }
            public int ep_id_tv365 { get; set; }
            public int scan { get; set; }
            public string dep_name { get; set; }
            public string ep_pass_encrypt { get; set; }
            public string from_source { get; set; }
            public string com_name { get; set; }
        }


    }
}
