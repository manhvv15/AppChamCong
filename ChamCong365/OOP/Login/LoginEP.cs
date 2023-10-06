using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.Login
{
    public class LoginEP
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
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
            public int type { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

        public class UserInfo
        {
            public int com_id { get; set; }
            public string com_name { get; set; }
            public string com_email { get; set; }
            public string com_phone_tk { get; set; }
            public string com_logo { get; set; }
            public string com_address { get; set; }
            public int com_create_time { get; set; }
            public int com_update_time { get; set; }
            public int com_authentic { get; set; }
            public object com_lat { get; set; }
            public object com_lng { get; set; }
            public int scan { get; set; }
            public string com_pass { get; set; }
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
