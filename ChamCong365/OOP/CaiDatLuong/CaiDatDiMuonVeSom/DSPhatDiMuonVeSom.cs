using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.CaiDatDiMuonVeSom
{
    public class DSPhatDiMuonVeSom
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class PhatMuonInfo
        {
            public string _id { get; set; }
            public int pm_id { get; set; }
            public int pm_id_com { get; set; }
            public int pm_shift { get; set; }
            public int pm_type { get; set; }
            public string pm_type_str { get; set; }
            public int pm_minute { get; set; }
            public int pm_type_phat { get; set; }
            public DateTime pm_time_begin { get; set; }
            public DateTime pm_time_end { get; set; }
            public string pm_time_end_str { get; set; }
            public string pm_monney { get; set; }
            public string pm_monney_str { get; set; }
            public DateTime pm_time_created { get; set; }
            public Shift shift { get; set; }
        }

        public class RelaxTime
        {
            public object start_time_relax { get; set; }
            public object end_time_relax { get; set; }
            public string _id { get; set; }
        }

        public class Root
        {
            public bool data { get; set; }
            public string message { get; set; }
            public List<PhatMuonInfo> phat_muon_info { get; set; }
        }

        public class Shift
        {
            public string _id { get; set; }
            public int shift_id { get; set; }
            public int com_id { get; set; }
            public string shift_name { get; set; }
            public string start_time { get; set; }
            public string start_time_latest { get; set; }
            public string end_time { get; set; }
            public string end_time_earliest { get; set; }
            public DateTime create_time { get; set; }
            public int over_night { get; set; }
            public int shift_type { get; set; }
            //public double num_to_calculate { get; set; }
            public int num_to_money { get; set; }
            public int is_overtime { get; set; }
            public int status { get; set; }
            public List<RelaxTime> relaxTime { get; set; }
            public int? flex { get; set; }
        }


    }
}
