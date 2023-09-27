using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class GroupData
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<Group> data { get; set; }

    }
    public class Group
    {
        public int STT { get; set; }
        public int gr_id { get; set; }
        public string gr_name { get; set; }
        public int team_id { get; set; }
        public string team_name { get; set; }
        public int dep_id { get; set; }
        public string dep_name { get; set; }
        public int total_emp { get; set; }
        public string manager { get; set; }
        public string deputy { get; set; }
    }
    public class GroupRoot
    {
        public GroupData data { get; set; }
        public object error { get; set; }
    }


}
