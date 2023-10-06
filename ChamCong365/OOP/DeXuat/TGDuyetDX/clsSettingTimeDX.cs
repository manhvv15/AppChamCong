using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.DeXuat.TGDuyetDX
{
    public class clsSettingTimeDX
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public SettingDx settingDx { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

        public class SettingDx
        {
            public string list_user_2cap { get; set; }
            public string list_user_3cap { get; set; }
            public int _id { get; set; }
            public int com_id { get; set; }
            public int type_setting { get; set; }
            public int type_browse { get; set; }
            public int time_limit { get; set; }
            public int shift_id { get; set; }
            public string time_limit_l { get; set; }
            public string list_user { get; set; }
            public int time_tp { get; set; }
            public int time_hh { get; set; }
            public DateTime time_created { get; set; }
            public DateTime update_time { get; set; }
            public int __v { get; set; }
            public int kieu_duyet { get; set; }
        }


    }
}
