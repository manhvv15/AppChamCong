using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong
{
    public class clsShift
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
            public int shift_id { get; set; }
            public int com_id { get; set; }
            public string shift_name { get; set; }
            public string start_time { get; set; }
            public string start_time_latest { get; set; }
            public string end_time { get; set; }
            public string end_time_earliest { get; set; }
            public string hour_agree_propose { get; set; }
            public string minute_agree_propose { get; set; }
            public int over_night { get; set; }
            public int shift_type { get; set; }
            //public string num_to_calculate { get; set; }
            public int num_to_money { get; set; }
            public int is_overtime { get; set; }
            public int status { get; set; }
            //public List<RelaxTime> relaxTime { get; set; }
            public int flex { get; set; }
            public DateTime create_time { get; set; }
        }

        //public class RelaxTime
        //{
        //    public object start_time_relax { get; set; }
        //    public object end_time_relax { get; set; }
        //    public string _id { get; set; }
        //}

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
