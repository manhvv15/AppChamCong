using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatCongChuan
{
    public class Data
    {
        public bool result { get; set; }
        public string message { get; set; }
        public ListCongChuan data { get; set; }
    }

    public class ListCongChuan
    {
        public string _id { get; set; }
        public int cw_id { get; set; }
        public int num_days { get; set; }
        public string apply_month { get; set; }
        public int com_id { get; set; }
    }


    public class RootCongChuan
    {
        public Data data { get; set; }
        public object error { get; set; }
    }

}
