using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class GiamBienChe
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public int totalCount { get; set; }
            public List<Item> data { get; set; }

        }
        public class Item
        {
            public int ep_id { get; set; }
            public string ep_name { get; set; }
            public string dep_name { get; set; }
            public string position_name { get; set; }
            public string shift_name { get; set; }
            public int type { get; set; }
            public string note { get; set; }
            public int? shift_id { get; set; }
            public int? decision_id { get; set; }
            public int? dep_id { get; set; }
            public string time { get; set; }


        }
        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }

}
