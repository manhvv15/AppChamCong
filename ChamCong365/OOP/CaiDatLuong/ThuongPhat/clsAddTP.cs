using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.CaiDatLuong.ThuongPhat
{
    public class clsAddTP
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public Newobj1 newobj1 { get; set; }
        }

        public class Newobj1
        {
            public int pay_id { get; set; }
            public int pay_id_user { get; set; }
            public int pay_id_com { get; set; }
            public int pay_price { get; set; }
            public int pay_status { get; set; }
            public string pay_case { get; set; }
            public DateTime pay_day { get; set; }
            public int pay_month { get; set; }
            public int pay_year { get; set; }
            public int pay_group { get; set; }
            public int pay_nghi_le { get; set; }
            public DateTime pay_time_created { get; set; }
            public string _id { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public string message { get; set; }
        }


    }
}
