using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities
{
    public class API_ShifNew_List
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<Shift> data { get; set; }
        }
        public class Shift
        {
            public int STT { get; set; } = 0;
            public string _id { get; set; }
            public int? shiftNew_id { get; set; }
            public int? com_id { get; set; }
            public string shift_name { get; set; }
            public int? type_shift { get; set; }
            public string time_flex { get; set; }
            public ShiftDay shift_day { get; set; }
            public List<object> shift_broken { get; set; }
            public int? type_timeSheet { get; set; }
            public double? num_to_calculate { get; set; }
            public int? num_to_money { get; set; }
            public int? num_to_hours { get; set; }
            public int? status { get; set; }
            public string time_zone { get; set; }
            public DateTime? created_time { get; set; }
            public DateTime? update_time { get; set; }

            public Shift()
            {
                // Increment the STT when creating a new instance
                STT = STT++;
            }
        }
        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

        public class ShiftDay
        {
            public string start_time { get; set; }
            public string start_time_latest { get; set; }
            public string end_time { get; set; }
            public string end_time_earliest { get; set; }
            public int? num_day { get; set; }
            public string _id { get; set; }
        }




    }
}
