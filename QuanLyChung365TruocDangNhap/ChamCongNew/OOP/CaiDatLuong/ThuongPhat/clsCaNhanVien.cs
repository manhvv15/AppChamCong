using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.CaiDatLuong.ThuongPhat
{
    
    public class List_Shift_Nv
    {
        public string _id { get; set; }
        public int shift_id { get; set; }
        public int com_id { get; set; }
        public string shift_name { get; set; }
        public string start_time { get; set; }
        public string start_time_latest { get; set; }
        public string end_time { get; set; }
        public string end_time_earliest { get; set; }
        public int over_night { get; set; }
        public int nums_day { get; set; }
        public int shift_type { get; set; }
        public NumToCalculate num_to_calculate { get; set; }
        public int num_to_money { get; set; }
        public int? money_per_hour { get; set; }
        public int is_overtime { get; set; }
        public int status { get; set; }
        public List<RelaxTime> relaxTime { get; set; }
        public int flex { get; set; }
        public DateTime create_time { get; set; }
    }

    public class NumToCalculate
    {
        [JsonProperty("$numberDecimal")]
        public string numberDecimal { get; set; }
    }

    public class RelaxTime
    {
        public object start_time_relax { get; set; }
        public object end_time_relax { get; set; }
        public string _id { get; set; }
    }

    public class Root_Shift_Nv
    {
        public List<List_Shift_Nv> list { get; set; }
    }


}
