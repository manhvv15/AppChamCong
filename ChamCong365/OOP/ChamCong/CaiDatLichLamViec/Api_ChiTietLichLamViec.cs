using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatLichLamViec
{
    public class ChiTietLichLamViec
    {
        public string begin { get; set; }
        public string cycle_name { get; set; }
        public List<ListShift> list_shift { get; set; }
        public List<Show> show { get; set; }
    }

    public class DataChiTietLichLamViec
    {
        public ChiTietLichLamViec calendar { get; set; }
        public string message { get; set; }
    }

    public class ListShift
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

    public class API_ChiTietLichLamViec
    {
        public bool result { get; set; }
        public int code { get; set; }
        public DataChiTietLichLamViec data { get; set; }
        public object error { get; set; }
    }

    public class Show
    {
        public string date { get; set; }
        public string shift { get; set; }
    }
}
