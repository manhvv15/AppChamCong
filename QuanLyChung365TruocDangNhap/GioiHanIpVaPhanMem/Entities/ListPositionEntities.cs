using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.GioiHanIpVaPhanMem.Entities
{
    public class ListPositionEntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool? result { get; set; }
            public string message { get; set; }
            public List<PositionData> data { get; set; }

        }
        public class PositionData
        {
            public string _id { get; set; }
            public int? id { get; set; }
            public int? comId { get; set; }
            public string positionName { get; set; }
            public int? level { get; set; }
            public int? isManager { get; set; }
            public int? created_time { get; set; }
            public int? update_time { get; set; }
        }
        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }


    }
}
