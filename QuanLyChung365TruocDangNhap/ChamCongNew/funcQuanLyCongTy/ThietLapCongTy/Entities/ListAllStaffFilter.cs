using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.funcQuanLyCongTy.ThietLapCongTy.Entities
{
    public class ListAllStaffFilter
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? total { get; set; }
            public List<Item> items { get; set; }
        }

        public class DepId
        {
            public int? level { get; set; }
            public int? organizeDetailId { get; set; }
        }

        public class Item
        {
            public int? ep_id { get; set; }
            public string ep_name { get; set; }
            public string ep_phone { get; set; }
            public string ep_phoneTK { get; set; }
            public object ep_email { get; set; }
            public object ep_emailContact { get; set; }
            public int? com_id { get; set; }
            public int? position_id { get; set; }
            public List<DepId> dep_id { get; set; }
            public int? team_id { get; set; }
            public int? group_id { get; set; }
            public string dep_name { get; set; }
            public string ep_address { get; set; }
            public int? ep_birth_day { get; set; }
            public int? ep_gender { get; set; }
            public int? ep_education { get; set; }
            public int? ep_married { get; set; }
            public int? ep_exp { get; set; }
            public double? start_working_time { get; set; }
            public string positionName { get; set; }
            public List<object> basicSal { get; set; }
            public object avatar { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
