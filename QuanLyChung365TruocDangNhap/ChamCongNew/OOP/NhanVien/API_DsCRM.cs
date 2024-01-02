using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.NhanVien
{
    public class API_DsCRM
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data_DS_CRM
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? totalItems { get; set; }
            public List<Item> items { get; set; }
        }

        public class Item
        {
            public int? _id { get; set; }
            public int? ep_id { get; set; }
            public string ep_email { get; set; }
            public string ep_phone { get; set; }
            public string ep_name { get; set; }
            public string ep_image { get; set; }
            public int? role_id { get; set; }
            public int? allow_update_face { get; set; }
            public int? ep_education { get; set; }
            public int? ep_exp { get; set; }
            public string ep_address { get; set; }
            public int? ep_gender { get; set; }
            public int? ep_married { get; set; }
            public long? ep_birth_day { get; set; }
            public int? group_id { get; set; }
            public double? start_working_time { get; set; }
            public int? position_id { get; set; }
            public int? com_id { get; set; }
            public int? dep_id { get; set; }
        }

        public class DS_CRM
        {
            public Data_DS_CRM data { get; set; }
            public object error { get; set; }
        }


    }
}
