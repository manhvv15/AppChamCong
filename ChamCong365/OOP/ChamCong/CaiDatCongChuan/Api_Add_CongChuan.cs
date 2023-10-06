using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatCongChuan
{
    public class Data_Add_CongChuan
    {
        public bool result { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }

    public class Root_Add_CongChuan
    {
        public Data_Add_CongChuan data { get; set; }
        public object error { get; set; }
    }
}
