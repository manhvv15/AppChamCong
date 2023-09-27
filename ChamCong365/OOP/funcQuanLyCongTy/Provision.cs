using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.funcQuanLyCongTy
{
    public class ProvisionData
    {
        public int total { get; set; }
        public List<Provision> data { get; set; }

    }
    public class Provision
    {
        public string _id { get; set; }
        public int id { get; set; }
        public string description { get; set; }
        public int isDelete { get; set; }
        public string name { get; set; }
        public DateTime timeStart { get; set; }
        public int comId { get; set; }
        public string file { get; set; }
        public string supervisor_name { get; set; }
        public string created_at { get; set; }
        public object deletedAt { get; set; }
    }

    public class ProvisionRoot
    {
        public bool result { get; set; }
        public int code { get; set; }
        public ProvisionSuccess success { get; set; }
        public object error { get; set; }
    }

    public class ProvisionSuccess
    {
        public ProvisionData data { get; set; }
        public string message { get; set; }
    }
}
