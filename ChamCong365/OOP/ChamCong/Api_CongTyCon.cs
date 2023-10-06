using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class DataCongTyCon
    {
        public bool result { get; set; }
        public string message { get; set; }
        public int totalItems { get; set; }
        public List<Item_CtyCon> items { get; set; }
    }

    public class Deputy
    {
        public string ep_id { get; set; }
        public string ep_name { get; set; }
    }

    public class Item_CtyCon
    {
        public int com_id { get; set; }
        public int com_parent_id { get; set; }
        public string com_name { get; set; }
        public string com_email { get; set; }
        public object com_phone_tk { get; set; }
        public string id_way_timekeeping { get; set; }
        public string com_phone { get; set; }
        public object com_logo { get; set; }
        public string com_address { get; set; }
        public int com_create_time { get; set; }
    }

    public class Manager
    {
        public string ep_id { get; set; }
        public string ep_name { get; set; }
    }

    public class Root_CongTyCon
    {
        public DataCongTyCon data { get; set; }
        public object error { get; set; }
    }


}
