using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatLichLamViec
{
    public class List_shift
    {
        public int itemsPerPage { get; set; }
        public string totalItems { get; set; }
        public List<Item_shift> items { get; set; }
    }

    public class Item_shift
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
        public int shift_type { get; set; }
        public double num_to_calculate { get; set; }
        public int num_to_money { get; set; }
        public int is_overtime { get; set; }
        public int status { get; set; }
        public List<RelaxTime> relaxTime { get; set; }
        public int flex { get; set; }
        public DateTime create_time { get; set; }
    }

    public class RelaxTime
    {
        public string start_time_relax { get; set; }
        public string end_time_relax { get; set; }
        public string _id { get; set; }
    }
    public class API_List_shift
    {
        public List_shift data { get; set; }
        public object error { get; set; }
    }
}
