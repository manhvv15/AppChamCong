using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatDiMuonVeSom
{
    public class clsAddNghiSaiQD
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Newobj
        {
            public int pc_id { get; set; }
            public int pc_money { get; set; }
            public DateTime pc_time { get; set; }
            public int pc_shift { get; set; }
            public int pc_com { get; set; }
            public int pc_type { get; set; }
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
