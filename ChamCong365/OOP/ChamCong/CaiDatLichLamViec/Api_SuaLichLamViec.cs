using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatLichLamViec
{
    public class Data_SuaLich
    {
        public bool result { get; set; }
        public string message { get; set; }
    }

    public class Root_SuaLich
    {
        public Data_SuaLich data { get; set; }
        public object error { get; set; }
    }

}
