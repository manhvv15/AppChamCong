using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatLichLamViec
{
    public class DataSaff
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<ListSaff> list { get; set; }
        public int count { get; set; }
    }

    public class ListSaff
    {
        public string _id { get; set; }
        public int ep_id { get; set; }
        public string ep_name { get; set; }
        public int dep_id { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string avatarUser { get; set; }
        public string dep_name { get; set; }
    }

    public class RootSaff
    {
        public DataSaff data { get; set; }
        public object error { get; set; }
    }
}
