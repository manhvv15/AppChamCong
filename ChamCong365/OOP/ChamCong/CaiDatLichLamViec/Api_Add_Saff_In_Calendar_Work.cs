using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatLichLamViec
{
    public class DataAddSaffInCalendarWork
    {
        public bool result { get; set; }
        public string message { get; set; }
    }

    public class RootAddSaffInCalendarWork
    {
        public DataAddSaffInCalendarWork data { get; set; }
        public object error { get; set; }
    }

}
