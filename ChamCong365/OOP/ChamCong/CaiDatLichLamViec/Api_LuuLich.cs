using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatLichLamViec
{
    public class Data_LuuLich
    {
        public bool result { get; set; }
        public string message { get; set; }
    }

    public class Root_LuuLich
    {
        public Data_LuuLich data { get; set; }
        public object error { get; set; }
    }
}
