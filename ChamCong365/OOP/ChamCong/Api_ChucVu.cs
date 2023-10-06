using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong
{
    public class Position_Data
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<List_Position> data { get; set; }

    }

    public class List_Position
    {
        public int positionId { get; set; }
        public string positionName { get; set; }
        public string mission { get; set; }
        public List<string> users { get; set; }
        public int? tong_nv { get; set; }
    }
    public class Root_Position
    {
        public Position_Data data { get; set; }
        public object error { get; set; }
    }
}
