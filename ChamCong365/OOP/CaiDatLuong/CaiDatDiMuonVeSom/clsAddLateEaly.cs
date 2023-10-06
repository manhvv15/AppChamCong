using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatDiMuonVeSom
{
    public class clsAddLateEaly
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Newobj
        {
            public int pm_id { get; set; }
            public int pm_id_com { get; set; }
            public int pm_shift { get; set; }
            public int pm_type { get; set; }
            public int pm_minute { get; set; }
            public int pm_type_phat { get; set; }
            public DateTime pm_time_begin { get; set; }
            public DateTime pm_time_end { get; set; }
            public int pm_monney { get; set; }
            public DateTime pm_time_created { get; set; }
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
