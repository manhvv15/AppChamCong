using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCong365.Entities.Company
{
    public class API_Vip_Company
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public VipInfo data { get; set; }

        }
        public class VipInfo {
            public bool isVip { get; set; }
            public bool can_add_more { get; set; }
            public int max_emps { get; set; }
            public int current_emps { get; set; }
        }


        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }
    }
}
