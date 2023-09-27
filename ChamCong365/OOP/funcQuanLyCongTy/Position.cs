using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class PositionData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<Position> data { get; set; }

    }

    public class Position
    {
        public int positionId { get; set; }
        public string positionName { get; set; }
        public string mission { get; set; }
        public List<string> users { get; set; }
        public int? tong_nv { get; set; }
    }
    public class PositionRoot
    {
        public PositionData data { get; set; }
        public object error { get; set; }
    }
}
