using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatLichLamViec
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class Data_Calendar
    {
        public bool result { get; set; }
        public string message { get; set; }
        public NewCalendar newCalendar { get; set; }
    }

    public class NewCalendar
    {
        public int cy_id { get; set; }
        public int com_id { get; set; }
        public string cy_name { get; set; }
        public DateTime apply_month { get; set; }
        public string cy_detail { get; set; }
        public int status { get; set; }
        public int is_personal { get; set; }
        public string _id { get; set; }
    }

    public class Root_CoppyCalendar
    {
        public Data_Calendar data { get; set; }
        public object error { get; set; }
    }


}
