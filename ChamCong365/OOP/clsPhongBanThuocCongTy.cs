using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP
{
    public class clsPhongBanThuocCongTy
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public int totalItems { get; set; }
            public List<Item> items { get; set; }
        }

        public class Item
        {
            public string _id { get; set; }
            public int dep_id { get; set; }
            public int com_id { get; set; }
            public string dep_name { get; set; }
            public DateTime dep_create_time { get; set; }
            public object manager_id { get; set; }
            public int dep_order { get; set; }
            public int total_emp { get; set; }
            public string manager { get; set; }
            public string deputy { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
