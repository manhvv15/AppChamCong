using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.PhucLoi
{
    public class clsAddNVinPL
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Newobj
        {
            public int cls_id { get; set; }
            public int cls_id_cl { get; set; }
            public int cls_id_user { get; set; }
            public int cls_id_group { get; set; }
            public int cls_id_com { get; set; }
            public DateTime cls_day { get; set; }
            public DateTime cls_day_end { get; set; }
            public int cls_salary { get; set; }
            public int cls_custom { get; set; }
            public string cls_lydo { get; set; }
            public string cls_quyetdinh { get; set; }
            public int cls_phu_cap_bh { get; set; }
            public DateTime cls_time_created { get; set; }
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
