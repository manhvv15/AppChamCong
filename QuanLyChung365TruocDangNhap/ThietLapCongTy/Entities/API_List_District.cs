using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ThietLapCongTy.Entities
{
    public class API_List_District
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class City
        {
            public int? cit_id { get; set; }
            public string cit_name { get; set; }
            public int? cit_order { get; set; }
            public int? cit_type { get; set; }
            public int? cit_count { get; set; }
            public int? cit_count_vl { get; set; }
            public int? cit_count_vlch { get; set; }
            public string postcode { get; set; }
        }

        public class District
        {
            public int? cit_id { get; set; }
            public string cit_name { get; set; }
            public int? cit_order { get; set; }
            public object cit_type { get; set; }
            public int? cit_count { get; set; }
            public int? cit_parent { get; set; }
        }

        public class Root
        {
            public List<City> city { get; set; }
            public List<District> district { get; set; }
        }


    }
}
