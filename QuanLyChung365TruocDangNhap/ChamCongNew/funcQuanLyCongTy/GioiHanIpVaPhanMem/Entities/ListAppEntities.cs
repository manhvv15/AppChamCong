using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.GioiHanIpVaPhanMem.Entities
{
    public class ListAppEntities
    {
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<AppData> data { get; set; }

        }
        public class AppData
        {
            public int? app_id { get; set; }
            public string app_name { get; set; }
            public int? app_type { get; set; }
            public string type_name { get; set; }
            public bool isSelect { get; set; }
        }

        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }
    }
}
