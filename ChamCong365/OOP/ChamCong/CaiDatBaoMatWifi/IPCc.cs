using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamCong365.OOP.ChamCong.CaiDatBaoMatWifi
{
    public class ListIP
    {
        public bool result { get; set; }
        public string message { get; set; }
        public string _id { get; set; }
        public int id_acc { get; set; }
        public string ip_access { get; set; }
        public string from_site { get; set; }
        public string created_time { get; set; }
        public string update_time { get; set; }
    }

    public class RootIp
    {
        public DataIp data { get; set; }
        public object error { get; set; }
    }
    public class DataIp
    {
        public bool result { get; set; }
        public string message { get; set; }
        public List<ListIP> data { get; set; }
    }
    
}
