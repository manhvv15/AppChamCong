using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.PhucLoi
{
    public class AddPhucLoi
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Newobj
        {
            public int cl_id { get; set; }
            public string cl_name { get; set; }
            public int cl_salary { get; set; }
            public DateTime cl_day { get; set; }
            public DateTime cl_day_end { get; set; }
            public long cl_active { get; set; }
            public string cl_note { get; set; }
            public int cl_type { get; set; }
            public int cl_type_tax { get; set; }
            public int cl_id_form { get; set; }
            public int cl_com { get; set; }
            public DateTime cl_time_created { get; set; }
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
