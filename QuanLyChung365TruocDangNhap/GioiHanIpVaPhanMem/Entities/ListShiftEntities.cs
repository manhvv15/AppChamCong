using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities
{
    public class ListShiftEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? totalItems { get; set; }
            public List<Item> items { get; set; }
        }

        public class Item
        {
            public string _id { get; set; }
            public int? shift_id { get; set; }
            public int? com_id { get; set; }
            public string shift_name { get; set; }
            public string start_time { get; set; }
            public string start_time_latest { get; set; }
            public string end_time { get; set; }
            public string end_time_earliest { get; set; }
            public string over_night { get; set; }
            public string shift_type { get; set; }
            public string num_to_calculate { get; set; }
            public string num_to_money { get; set; }
            public string money_per_hour { get; set; }
            public string is_overtime { get; set; }
            public string status { get; set; }
            public List<RelaxTime> relaxTime { get; set; }
            public string flex { get; set; }
            public string create_time { get; set; }
            public string start_time_relax
            {
                get
                {
                    return this.relaxTime[0].start_time_relax;
                }
            }
            public string end_time_relax { get { return this.relaxTime[0].end_time_relax; } }
            public bool isSelect { get; set; }
        }

        public class RelaxTime
        {
            public string start_time_relax { get; set; }
            public string end_time_relax { get; set; }
            public string _id { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
