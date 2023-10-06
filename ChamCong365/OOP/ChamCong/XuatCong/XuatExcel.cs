using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.XuatCong
{
    public class ExcelData
    {
        public string ep_id { get; set; }
        public string ep_name { get; set; }
        public string ts_date { get; set; }
        public string day_of_week { get; set; }
        public int late { get; set; }
        public int early { get; set; }
        public double num_to_calculate { get; set; }
        public int num_overtime { get; set; }
        public int total_time { get; set; }
        public List<string> lst_time { get; set; }
        public List<string> shift_name { get; set; }
    }

    public class API_XuatExcel
    {
        public List<ExcelData> list_total { get; set; }
        public int max_count_time { get; set; }
    }
}
