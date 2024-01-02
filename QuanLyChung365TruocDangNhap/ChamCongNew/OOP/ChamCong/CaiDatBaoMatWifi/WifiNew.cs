using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi
{
    public class WifiNew
    {
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public int? total { get; set; }
            public List<Item> data { get; set; }

        }
        public class Item
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? id_com { get; set; }
            public string ip_access { get; set; }
            public string name_wifi { get; set; }
            public string id_loc { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }
    }
}
