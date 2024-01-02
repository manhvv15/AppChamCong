using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLyChung365TruocDangNhap.ChamCongNew.OOP.ChamCong.CaiDatBaoMatWifi
{
    public class ListQREntities
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
        public class Data
        {
            public bool result { get; set; }
            public string message { get; set; }
            public List<QRInfo> data { get; set; }

        }
        public class QRInfo
        {
            public int STT { get; set; }
            public string _id { get; set; }
            public int id { get; set; }
            public int comId { get; set; }
            public string QRCodeName { get; set; }
            public string QRCodeUrl { get; set; }
            public int QRstatus { get; set; }
            public int created_time { get; set; }
            public int update_time { get; set; }
        }
        public class Root
        {
            public Data data { get; set; }
            public object error { get; set; }
        }

    }
}
