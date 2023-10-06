using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.PhucLoi
{
    public class AddPCShift
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Newobj
        {
            public int wf_id { get; set; }
            public int wf_money { get; set; }
            public DateTime wf_time { get; set; }
            public DateTime wf_time_end { get; set; }
            public int wf_shift { get; set; }
            public int wf_com { get; set; }
            public string _id { get; set; }
        }

        public class Root
        {
            public bool data { get; set; }
            public string message { get; set; }
            public Newobj newobj { get; set; }
        }


    }
}
